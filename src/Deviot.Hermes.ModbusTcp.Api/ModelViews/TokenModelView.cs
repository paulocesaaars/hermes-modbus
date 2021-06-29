namespace Deviot.Hermes.ModbusTcp.Api.ModelViews
{
    public class TokenModelView
    {
        public string AccessToken { get; set; }

        public UserInfoModelView User { get; set; }
    }
}
