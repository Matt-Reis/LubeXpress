using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace LubeX;

public class ServiceMenu {

    public static async Task<string> ServiceMainMenu(string[] args)
    {
        //SERVICE MENU DISPLAY
        Console.WriteLine("\n\x1B\nMWelcome to the Service Menu! Please enter the keys for the following services you would like to perform:");
        Console.WriteLine("\n[A] Oil Change\n[B] Tire Rotation");
        Console.WriteLine("[1] Engine Air Filter\n[2] Cabin Air Filter");
        Console.WriteLine("[3] Coolant Flush\n[4] Transmission Flush\n[5] Differential Fluid Exchange\n[6] Top Off All Fluids");
        Console.WriteLine("[7] Wiper Blades\n[8] Vacuum Floors\n");
        //END OF SERVICE MENU DISPLAY

        //READ SERVICE MENU INPUT
        string? ServiceInput = Console.ReadLine()?.ToUpper();
        string? ServiceKey = @"[A-B][1-9]"; //defines the acceptable ServiceInput parameters

        Regex regex = new Regex(ServiceKey, RegexOptions.IgnoreCase); //This will check to see if the user input matches the
                                                                      // Service key/ valid input

        if (regex.IsMatch(ServiceInput))
        {
            Console.WriteLine("\nThe service code you entered is " + ServiceInput + " . Press the ENTER key to confirm your selection, or press ESC \nto return to the service menu.");
        
            int count = 0;
            do {    
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                    if (keyInfo.Key == ConsoleKey.Enter){
                        count++;
                    }

                    //If the ESC key is pressed, restart the Service menu
                    else if (keyInfo.Key == ConsoleKey.Escape) {
                        return await ServiceMainMenu(args);
                    }
            }
            while (count < 1);
        }

        else
        {
            Console.WriteLine("Invalid user entry. Please enter characters [A-B][1-9]: "); //Incorrect input will display this message
            return await ServiceMainMenu(args);
        }

        return ServiceInput;
    }

}