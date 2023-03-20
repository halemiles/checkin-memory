namespace Checkin.Models
{
    public enum ResultCode
    {
        SUCCESS,
        NOTFOUND,
        INTERNALERROR,
        EXTERNALERROR
    }

    public class ApiResponse<T>
    {
        public bool Success { get{ return ResultCode == ResultCode.SUCCESS;}}
        public string Message { get; set; }
        public T Payload { get; set; }
        public ResultCode ResultCode {get; set;}

        public ApiResponse(string message, T payload, ResultCode resultcode)
        {
            Message = message;
            Payload = payload;
            this.ResultCode = resultcode;
        }
    }

    
}