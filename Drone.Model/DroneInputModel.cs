namespace Drone.Model
{
    public class DroneInputModel
    {
        public DroneInputModel()
        {
            Position = new DroneOutputModel();
        }

       public DroneOutputModel Position { get; set; }
       public string Instructions { get; set; }

    }
}
