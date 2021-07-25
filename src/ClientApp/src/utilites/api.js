export const parseErrorResponse = (result, t) => {
	let errorDetails = t('SomethingWentWrongTryAgain')
	switch (result.errorCode) {
		case 'OutdatedVersion':
			errorDetails = t('OutdatedVersion')
			break
		case 'InvalidRequest':
			errorDetails = t('InvalidRequest')
			break
	}
	return errorDetails
}

const firstCharacter = 1
export const camelCase = (s) =>
	s
		.split('.')
		.map(part => part.charAt(0).toLowerCase() + part.slice(firstCharacter))
		.join('.')

export const isValidationError = (obj) =>
    typeof obj.field === 'string' && typeof obj.code === 'string'

export const isErrorResult = (obj) =>
    typeof obj.success === 'boolean' && !obj.success && Array.isArray(obj.errors) && obj.errors.every(isValidationError)

        
export const reloadIfLogOnRequired = (response) => {
	const httpStatusUnauthorised = 401
	const httpStatusForbidden = 401
	if (response.status === httpStatusUnauthorised || response.status === httpStatusForbidden) {
		window.location.reload()
	}
}

const baseUrl = process.env.REACT_APP_API_URL

export async function safeFetch(input, init) {
	try {
        console.log(process.env);
        const url = `${baseUrl}/${input}`
		const response = await fetch(url)
		reloadIfLogOnRequired(response)
		// eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
		const result = await response.json()
		if (response.ok) {
			return {
				success: true,
				data: result,
			}
		}
		if (isErrorResult(result)) {
			return result
		}
	} catch (e) {
		console.error(e)
		// empty catch because something went horribly wrong and we don't care what exactly broken
		// here goes moe advanced exception handling and logging
	}
	return {
		success: false,
		errorCode: 'Unknown',
		errorDetails: '',
		errors: [],
	}
}