using Drone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.Services.Interface;
using Common;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
namespace Drone.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
        }

        public string GetWebApiPath()
        {
            return Common.ConfigSettingsReader.GetConfigurationValues(Constants.DroneNavigationService);
        }
    }
}
