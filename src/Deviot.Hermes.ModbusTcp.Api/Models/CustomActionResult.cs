namespace Deviot.Hermes.ModbusTcp.Api.Models
{
    public class CustomActionResult
    {
        public string Message { get; private set; }

        public object Data { get; private set; }

        public CustomActionResult()
        {

        }

        public CustomActionResult(string message, object data)
        {
            Message = message;
            Data = data;
        }
    }
}
