using dotnetWebApi.Enums;

namespace dotnetWebApi.Models
{
      public class ResponseModel
      {
        public ResponseModel(ResponseCode responseCode, string responseMessage, object dateSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DateSet = dateSet;
        }
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object DateSet { get; set; }
     }
}   