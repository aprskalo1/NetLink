namespace NetLink.Session
{
    internal class DeveloperSessionManager
    {
        private string? _devToken;

        public void AddDevTokenAuthentication(string devToken)
        {
            CheckDevToken(devToken);
            _devToken = devToken;
        }

        internal void CheckDevToken(string devToken)
        {
            string tokenCheckEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.DevTokenValidationUrl, devToken)}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = httpClient.GetAsync(tokenCheckEndpoint).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("DevToken is invalid or your account is deactivated, please check DevToken in your configuration file or account status.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while validating DevToken. Please check your network connection and try again.", ex);
                }
            }
        }
    }
}
