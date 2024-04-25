using System;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
namespace LubeX;

/* 
System: This namespace provides fundamental classes and base classes that define 
commonly-used value and reference data types, events and event handlers, interfaces, 
attributes, and processing exceptions.
System.Net.Http: This namespace provides classes for sending HTTP requests 
and receiving HTTP responses from a resource identified by a URI.
System.Text.Json: This namespace contains classes for working with JSON data.
System.Threading.Tasks: This namespace contains types that simplify the work of 
writing concurrent and asynchronous code.
*/

public class CheckIn
{
    /*
    This is the entry point of the program, where the execution starts.
    The async keyword indicates that this method performs asynchronous operations.
    Task represents an asynchronous operation that can return a value.
    */
    public static async Task<string[]> CheckInMain(string[] args)
    {
        //HEADER
        Console.WriteLine("\n\x1BM\n**************LubeXpress**************");
        Console.WriteLine("\n*************Version 0.01*************");

        DateTime currentDateTime = DateTime.Now;  //Receive date and time, declare it as currentDateTime
        Console.WriteLine("\n*********" + currentDateTime + "*********"); //Display current date and time
        //END OF HEADER
        Console.WriteLine("\nPlease enter your username:"); //No authentication, simply putting a name onto who made the repair order
        string? userName = Console.ReadLine(); //Read user's username input

        Console.WriteLine("\nHello " + userName + "! Welcome to LubeXpress."); // Greeting message, verifying username
        Console.WriteLine("\nLet's walk through the vehicle check-in procedure.");
        Console.WriteLine("\nPlease enter the vehicle's VIN: "); //VIN prompt

        string[] vehicleDecoded = await VinDecode.DecodedVin(args);  //Call DevodedVin method from the VinDecode class. VinDecode.cs contains all code for API implementation
                                                                    // The vehicleDecoded string array stores the parameters of the JSON object retreived from the API

            Console.WriteLine("\nPress the ENTER key to enter the service menu, or ESC to restart the vehicle check-in procedure:");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); //the true parameter ensures that the key pressed by the user is not displayed into the terminal, whichever key is pressed is stored into keyInfo

            // If any other key besides Enter or ESC is pressed, the instructions will be reprinted until a valid key is pressed

            // If the enter key is pressed, launched the service menu
            if (keyInfo.Key == ConsoleKey.Enter){
                ServiceMenu vehicleService = new ServiceMenu();
                await ServiceMenu.ServiceMainMenu(args);
 
            }

            //If the ESC key is pressed, restart the check-in procedure
            else if (keyInfo.Key == ConsoleKey.Escape) {
                return await CheckInMain(args);
            }
            return vehicleDecoded;
        }

    }
