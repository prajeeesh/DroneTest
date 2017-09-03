using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.Model;
namespace Drone.Services.Interface
{
    public interface IDroneNavigationService
    {
        List<DroneOutputModel> ExecuteDroneNavigation(DroneControlModel droneControlModel);
        Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinates(DroneControlModel droneControlModel);
        Task<List<Drone.Model.DroneOutputModel>> GetDroneCoordinatesFromApi(string droneControlJsonModel);
    }
}
