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
    public class DroneNavigationService: IDroneNavigationService
    {
        private int GridTopX = 0;
        private int GridTopY = 0;
        //private string DroneService { get; set; }
        private readonly ISettingsService settingsService;

        public DroneNavigationService(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
            //DroneService = Constants.DroneNavigationService;
        }
        /// <summary>
        /// Access the service endpoints and retrieves the list of pets based on the type passed as the parameter.
        /// </summary>
        /// <param name="petType">Pet Type Enum Refer Common.PetTypes.Type</param>
        /// <returns>List of pets grouped based on the owners gender</returns>
        public async Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinates(DroneControlModel droneControlModel)
        {
            //string uri = Common.ServicesManager.GetClientUri(DroneService);
            string uri = "http://localhost/Drone.NavigationApi/api/DroneNavigation";
            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();
            
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    // FinalDroneCoordinates = JsonConvert.DeserializeObject<List<Drone.Model.DroneOutputModel>>(
                    //    await httpClient.GetStringAsync(uri)
                    //);
                    //var results = await httpClient.GetStringAsync(uri);
                    //httpClient.GetAsync()
                    var response = await httpClient.PostAsJsonAsync(uri, droneControlModel);
                    FinalDroneCoordinates = await response.Content.ReadAsAsync<List<Drone.Model.DroneOutputModel>>();
                    //FinalDroneCoordinates = JsonConvert.DeserializeObject<List<Drone.Model.DroneOutputModel>>(
                    //   await httpClient.PostAsJsonAsync(uri, droneControlModel).Result;
                    //);
                    return FinalDroneCoordinates;
                }

                
            }
            catch (Exception ex)
            {
                //Log Exception 
                Console.WriteLine(ex.Message);
                throw new Exception("Error occured while accessing the service API");

            }
            return FinalDroneCoordinates;

        }
        public async Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinatesFromApi(string droneInputModel)
        {
            //string uri = settingsService.GetWebApiPath();
            //string uri = Common.ServicesManager.GetClientUri(DroneService);
            string[] stringSeparators = new string[] { "\r\n" };
            var commands = droneInputModel.TrimEnd().Split(stringSeparators, StringSplitOptions.None);
            List<DroneInputModel> inputModels = new List<DroneInputModel>();
            var gridDimensions = commands[0].Split(' ');  // First line sets the Battlefield dimensoins

            DroneControlModel droneControlModel = new DroneControlModel();
            droneControlModel.GridTopX = int.Parse(gridDimensions[0]);
            droneControlModel.GridTopY = int.Parse(gridDimensions[1]);

            for (int i = 1; i < commands.Length; i += 2)
            {
                DroneInputModel model = new DroneInputModel();
                var postion = commands[i].Split(' ');
                model.Position.PositionX = int.Parse(postion[0]);
                model.Position.PositionY = int.Parse(postion[1]);
                model.Position.Heading = postion[2];
                model.Instructions = commands[i + 1];

                droneControlModel.DroneInputs.Add(model);
            }
            string inputJson = JsonConvert.SerializeObject(droneControlModel);
                  
            //var droneControlModel = JsonConvert.DeserializeObject<Drone.Model.DroneOutputModel>(droneControlJsonModel);
            string uri = "http://localhost/Drone.NavigationApi/api/DroneNavigation";
            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    // FinalDroneCoordinates = JsonConvert.DeserializeObject<List<Drone.Model.DroneOutputModel>>(
                    //    await httpClient.GetStringAsync(uri)
                    //);
                    //var results = await httpClient.GetStringAsync(uri);
                    //httpClient.GetAsync()

                    StringContent content = new StringContent(JsonConvert.SerializeObject(droneControlModel), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(uri, content);
                    FinalDroneCoordinates = await response.Content.ReadAsAsync<List<Drone.Model.DroneOutputModel>>();
                    //FinalDroneCoordinates = JsonConvert.DeserializeObject<List<Drone.Model.DroneOutputModel>>(
                    //   await httpClient.PostAsJsonAsync(uri, droneControlModel).Result;
                    //);
                    return FinalDroneCoordinates;
                }


            }
            catch (Exception ex)
            {
                //Log Exception 
                Console.WriteLine(ex.Message);
                throw new Exception("Error occured while accessing the service API");

            }
            return FinalDroneCoordinates;

        }
        private  DroneInputModel Move(DroneInputModel inputLocation)
        {

            string direction = inputLocation.Position.Heading;
            string move_direction = "";
            bool can_move = true;
            var currentPositionX = inputLocation.Position.PositionX;
            var currentPositionY = inputLocation.Position.PositionY;

            if (direction == "E" || direction == "W")
                move_direction = "X";
            else if (direction == "S" || direction == "N")
                move_direction = "Y";

            if (direction == "E" || direction == "N")
            {
                if (direction == "E")
                    if ((currentPositionX < 0 || (currentPositionX) > GridTopX))
                        can_move = false;

                if (direction == "N")
                    if (currentPositionY < 0 || (currentPositionY) > GridTopY)
                        can_move = false;

                if (can_move)
                {
                    if (move_direction == "X")
                        inputLocation.Position.PositionX += 1;
                    else
                        inputLocation.Position.PositionY += 1;
                }
            }
            else if (direction == "W" || direction == "S")
            {
                if (move_direction == "X" && currentPositionX > 0)
                    inputLocation.Position.PositionX -= 1;
                else if (move_direction == "Y" && currentPositionY > 0)
                    inputLocation.Position.PositionY -= 1;
            }

            return inputLocation;
        }

        private DroneOutputModel ExecuteCommand(DroneInputModel droneInputCommand)
        {
            var commands = droneInputCommand.Instructions.ToCharArray();
            DroneOutputModel finalCoodinates = new DroneOutputModel();
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        switch (droneInputCommand.Position.Heading)
                        {
                            case "E":
                                droneInputCommand.Position.Heading = "N";
                                break;
                            case "N":
                                droneInputCommand.Position.Heading = "W";
                                break;
                            case "W":
                                droneInputCommand.Position.Heading = "S";
                                break;
                            case "S":
                                droneInputCommand.Position.Heading = "E";
                                break;
                        }
                        break;
                    case 'R':
                        switch (droneInputCommand.Position.Heading)
                        {
                            case "E":
                                droneInputCommand.Position.Heading = "S";
                                break;
                            case "N":
                                droneInputCommand.Position.Heading = "E";
                                break;
                            case "W":
                                droneInputCommand.Position.Heading = "N";
                                break;
                            case "S":
                                droneInputCommand.Position.Heading = "W";
                                break;
                        }
                        break;
                    case 'M':
                        droneInputCommand = Move(droneInputCommand);
                        break;
                }
            }
                     
            finalCoodinates.PositionX = droneInputCommand.Position.PositionX;
            finalCoodinates.PositionY = droneInputCommand.Position.PositionY;
            finalCoodinates.Heading = droneInputCommand.Position.Heading;

            return finalCoodinates;
        }
        public List<DroneOutputModel> ExecuteDroneNavigation(DroneControlModel droneControlModel)
        {
            GridTopX = droneControlModel.GridTopX;
            GridTopY = droneControlModel.GridTopY;

            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();

            foreach (DroneInputModel input in droneControlModel.DroneInputs)
            {
                FinalDroneCoordinates.Add(ExecuteCommand(input));
            }
            return FinalDroneCoordinates;
        }
    }
}
