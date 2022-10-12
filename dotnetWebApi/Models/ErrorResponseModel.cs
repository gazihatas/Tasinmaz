using dotnetWebApi.Enums;

namespace dotnetWebApi.Models
{
     public class ErrorResponseModel
     {
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public ErrorResponseModel(ResponseCode responseCode, string message)
        {
          ResponseCode = responseCode;
          Message = message;
        }
     }
}