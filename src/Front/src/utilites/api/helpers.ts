import { ErrorResult, Result, isErrorResult, NoDataResult } from './commonContracts'

export type CommonTFunction = (key: string) => string

export const parseErrorResponse = (result: ErrorResult, t: CommonTFunction): string => {
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
export const camelCase = (s: string): string =>
	s
		.split('.')
		.map(part => part.charAt(0).toLowerCase() + part.slice(firstCharacter))
		.join('.')

export const reloadIfLogOnRequired = (response: Response): void => {
	const httpStatusUnauthorised = 401
	const httpStatusForbidden = 401
	if (response.status === httpStatusUnauthorised || response.status === httpStatusForbidden) {
		window.location.reload()
	}
}

const baseurl = process.env.REACT_APP_API_URL

export async function safeFetch<T = void>(input: RequestInfo, init?: RequestInit): Promise<Result<T>> {
	try {
		const url = `${baseurl}/${input}`
		const response = await fetch(url, init)
		reloadIfLogOnRequired(response)
		// eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
		const result = await response.json()
		if (response.ok) {
			return {
				success: true,
				data: result as T,
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

export async function safeCommonFetch(input: RequestInfo, init?: RequestInit): Promise<NoDataResult> {
	try {
		const url = `${baseurl}/${input}`
		const response = await fetch(url, init)
		reloadIfLogOnRequired(response)
		if (response.ok) {
			return {
				success: true,
			}
		}
	} catch(e) {
		console.error(e);
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

