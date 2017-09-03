using Drone.Model;
using Drone.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Drone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDroneNavigationService droneNavigationService;
        public HomeController(IDroneNavigationService _droneNavigationService)
        {
            droneNavigationService = _droneNavigationService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string droneInputModel)
        {
            List<DroneOutputModel> FinalDroneCoordinates = new List<DroneOutputModel>();
            FinalDroneCoordinates = await droneNavigationService.GetDroneCoordinatesFromApi(droneInputModel);

            return View(FinalDroneCoordinates);
        }
    }
}