export interface RequestResult {
	success: boolean
	errorCode: string
	errorDetails: string
	errors: ValidationError[]
}

export interface ValidationError {
	field: string
	code: string
}

export type ErrorResult = {
	success: false
	errorCode: string | null
	errorDetails: string | null
	errors: ValidationError[]
}

export const isValidationError = (obj: ValidationError): obj is ValidationError =>
	typeof obj.field === 'string' && typeof obj.code === 'string'

export const isErrorResult = (obj: ErrorResult | RequestResult): obj is ErrorResult =>
	typeof obj.success === 'boolean' && !obj.success && Array.isArray(obj.errors) && obj.errors.every(isValidationError)


export type CommonResult = {
	success: boolean
}
export type DataResult<T = void> = {
	success: true
	data: T
}
export type NoDataResult = ErrorResult | CommonResult;
export type Result<T = void> = ErrorResult | DataResult<T>;
