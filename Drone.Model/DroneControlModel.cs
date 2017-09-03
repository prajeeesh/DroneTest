using System.Collections.Generic;

namespace Drone.Model
{
    public class DroneControlModel
    {
        /// <summary>
        /// This model is holds the complete input instructions 
        /// DroneInput has the list of drone instructions.Battle field cooridnates are stored in GridX,GridY. 
        /// </summary>
        public DroneControlModel()
        {
            DroneInputs = new List<DroneInputModel>();
        }
        public int GridTopX { get; set; }
        public int GridTopY { get; set; }
        public List<DroneInputModel> DroneInputs { get; set; }
        
    }
}
