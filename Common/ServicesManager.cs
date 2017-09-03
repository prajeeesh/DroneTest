namespace Common
{
    public static class ServicesManager
    {
        private static string apiEndPoint = Constants.APIEndPoint;
        private static string baseAddress = Common.ConfigSettingsReader.GetConfigurationValues(apiEndPoint);

        /// <summary>
        /// Constructs the uri by appending the service name to the BaseAdderess
        /// </summary>
        /// <param name="serviceName">Name of the service to access</param>
        /// <returns>Client uri string</returns>
        public static string GetClientUri(string serviceName)
        {
            string uri = baseAddress + Common.ConfigSettingsReader.GetConfigurationValues(serviceName);
            return uri;
        }
    }


}
