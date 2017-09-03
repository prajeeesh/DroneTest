using System;
using Drone.Model;

namespace MockDroneNavigation
{
    class Program
    {
        static int GridTopX = 5;
        static int GridTopY = 5;
        static void Main(string[] args)
        {
            DroneInputModel inputLocation = new DroneInputModel();

            //inputLocation.Position.PositionX = 1;
            //inputLocation.Position.PositionY = 2;
            //inputLocation.Position.Heading = "N";
            //string moveDirection = "LMLMLMLMM";

            inputLocation.Position.PositionX = 3;
            inputLocation.Position.PositionY = 3;
            inputLocation.Position.Heading = "E";
            string moveDirection = "MMRMMRMRRM";


            var output = ExecuteCommand(inputLocation, moveDirection);
            Console.Read();

        }

        //public string ProcessDroneInstructions(DroneInputModel InputLocation, string Instructions)
        //{
        //    var commands = Instructions.ToCharArray();
        //    string moveDirection = string.Empty;
        //    foreach (char command in commands)
        //    {
        //        if (command == 'L' || command == 'R')
        //        {
        //            moveDirection = "Sidewise";

        //        }

        //        else
        //            moveDirection = "Updown";
        //    }
        //    return "";
        //}

        //string CalculateDroneDirection(string InitialDirection, string MoveDirection)
        //{
        //    string FinalDirection = string.Empty;

        //    return FinalDirection;
        //}

        public static DroneInputModel Move(DroneInputModel inputLocation)
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
                else if(move_direction == "Y" && currentPositionY > 0)
                       inputLocation.Position.PositionY -= 1;
            }

            return inputLocation;
        }

        public static DroneInputModel ExecuteCommand(DroneInputModel inputLocation, string moveDirection)
        {
            var commands = moveDirection.ToCharArray();
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        switch (inputLocation.Position.Heading)
                        {
                            case "E":
                                inputLocation.Position.Heading = "N";
                                break;
                            case "N":
                                inputLocation.Position.Heading = "W";
                                break;
                            case "W":
                                inputLocation.Position.Heading = "S";
                                break;
                            case "S":
                                inputLocation.Position.Heading = "E";
                                break;
                        }
                        break;
                    case 'R':
                        switch (inputLocation.Position.Heading)
                        {
                            case "E":
                                inputLocation.Position.Heading = "S";
                                break;
                            case "N":
                                inputLocation.Position.Heading = "E";
                                break;
                            case "W":
                                inputLocation.Position.Heading = "N";
                                break;
                            case "S":
                                inputLocation.Position.Heading = "W";
                                break;
                        }
                        break;
                    case 'M':
                        inputLocation = Move(inputLocation);
                        break;
                }

                Console.WriteLine("OutPut : " + inputLocation.Position.PositionX + " " + inputLocation.Position.PositionY + " " + inputLocation.Position.Heading);
            }
            return inputLocation;
        }
    }
}