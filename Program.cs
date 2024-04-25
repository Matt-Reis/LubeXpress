using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace LubeX;

public class Program{
        static async Task Main(string[] args){
            
            CheckIn vehicleCheckIn = new CheckIn(); //Create an instance for the CheckIn class within the Program Main

            //var LaunchCheckIn = await CheckIn.CheckInMain(args); //Call the CheckInMain method from the CheckIn class *Launch Check-in*

            string[] vehicleDecoded = await CheckIn.CheckInMain(args);  // Retrieve vehicle information from VinDecode   
                                                                        // The vehicleDecoded string array stores the parameters of the JSON object retreived from the API

            Console.WriteLine("\nVehicle Build and Service Code Verified. Generating Repair Order . . .");
            RepairOrder newRepairOrder = new RepairOrder(); // Create and instance for RepairOrder class
                newRepairOrder.RepairOrderGenerator(vehicleDecoded[0], vehicleDecoded[1], vehicleDecoded[2], vehicleDecoded[3], vehicleDecoded[4]);  // Service menu class gathered all required information to generate the repair order
        }

        

}