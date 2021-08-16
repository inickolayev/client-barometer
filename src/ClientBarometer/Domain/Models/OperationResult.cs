using System;

namespace ClientBarometer.Domain.Models
{
    public class OperationResult<TResult>
        where TResult : class
    {
        public bool IsSuccess { get; set; }
        public TResult Result { get; set; }
        public string Error { get; set; }

        public OperationResult(TResult result)
        {
            IsSuccess = true;
            Result = result;
        }

        public OperationResult(Exception e)
        {
            IsSuccess = false;
            Error = e.Message;
        }
    }
}