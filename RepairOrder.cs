using System;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;
namespace LubeX;

public class RepairOrder {
    public void RepairOrderGenerator(string make, string model, string modelYear, string origin, string manufacturer) { //allow method to accept the vehicleBuild string array

        Console.WriteLine("\nREPAIR ORDER:");
        Console.WriteLine("\nVehicle Information:");
        Console.WriteLine($"Make: {make}");
        Console.WriteLine($"Model: {model}");
        Console.WriteLine($"Year: {modelYear}");
        Console.WriteLine($"Origin: {origin}");
        Console.WriteLine($"Manufacturer: {manufacturer}");


        
    }

}