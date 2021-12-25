namespace QRMSWeb.Components.Common.AuthWrapper
{
    public class AuthTokenInstance
    {
        public AuthTokenInstance(){}

        public static readonly AuthTokenInstance Instance = new AuthTokenInstance();

        private string token = "";

        public void SetToken(string accessToken)
        {
            token = accessToken;
        }

        public string GetToken()
        {
            return this.token;
        }
    }
}