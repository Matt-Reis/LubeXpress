using System;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
namespace LubeX;

public class VinDecode {
    public static async Task<string[]> DecodedVin(string[] args) {
        string[] vehicleBuild = new string[5];

        string? vin = Console.ReadLine(); //This will change via input with a barcode scanner
                                         // Manual entry will still be an option
        
        //baseUrl: This variable holds the base URL for the NHTSA VIN Decoder API.
        // Base URL for the NHTSA VIN Decoder - Values Extended API
        string baseUrl = "https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesExtended/";

        //extendedUrl: This variable constructs the complete API URL by appending the VIN
        //and default year as query parameters.
        // Build the tail end of the URL request
        string extendedUrl = $"{baseUrl}{vin}?format=json";



        
        //HttpClient(included class in the System.Net.Http namespace/library) is used to send HTTP requests and receive HTTP responses from a 
        //resource identified by a URL. The using statement ensures that the HttpClient 
        //instance is disposed of properly after use. await client.GetAsync(apiUrl) 
        //sends a GET request to the specified URL asynchronously, and response stores 
        //the HTTP response.
        
        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Send a GET request to the API endpoint
                HttpResponseMessage response = await client.GetAsync(extendedUrl);

                
                //Checks if the HTTP response indicates success (status code 200).
                //If successful, reads the response content as a string asynchronously
                //using await response.Content.ReadAsStringAsync().
                
                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    
                    //JsonDocument.Parse(responseBody) parses the JSON response string into a 
                    //JsonDocument object, allowing access to its properties.
                    
                    // Deserialize the JSON response into a JsonDocument
                    using (JsonDocument document = JsonDocument.Parse(responseBody))
                    {
                        // Get the root element
                        JsonElement root = document.RootElement;


                        
                        //Checks if the JSON response contains a property named "Results".
                        //If present, retrieves the value associated with the "Results"
                        //property.
                        
                        // Check if the 'Results' property exists
                        if (root.TryGetProperty("Results", out JsonElement results))
                        {
                            
                            //Checks if the "Results" property value is an array and has at least 
                            //one element.
                            //If present, retrieves the first element of the array and
                            //accesses its properties to print vehicle information.
                            
                            // Check if 'Results' is an array and has at least one element
                            if (results.ValueKind == JsonValueKind.Array && results.GetArrayLength() > 0)
                            {
                                // Get the first element of the array
                                JsonElement result = results[0];

                                // Access/Parse the vehicle information from the new JSON object
                                string? make = result.GetProperty("Make").GetString();
                                string? model = result.GetProperty("Model").GetString();
                                string? modelYear = result.GetProperty("ModelYear").GetString();
                                string? origin = result.GetProperty("PlantCountry").GetString();
                                string? manufacturer = result.GetProperty("Manufacturer").GetString();

                                // Print the vehicle data from the API results/JSON object
                                Console.WriteLine("\nVehicle Information:");
                                Console.WriteLine($"Make: {make}");
                                Console.WriteLine($"Model: {model}");
                                Console.WriteLine($"Year: {modelYear}");
                                Console.WriteLine($"Origin: {origin}");
                                Console.WriteLine($"Manufacturer: {manufacturer}");

                                vehicleBuild[0] = make;
                                vehicleBuild[1] = model;
                                vehicleBuild[2] = modelYear;
                                vehicleBuild[3] = origin;
                                vehicleBuild[4] = manufacturer;
                                
                            }
                            
                            
                            //The only way these errors should occur is in the event of an incorrect VIN entry
                            //EXCEPTION HANDLING
                            else
                            {
                                Console.WriteLine("No results found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Results property not found.");
                        }
                    }
                }
                else
                {
                    // If the request was not successful, print the error status code
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            //END EXCEPTION HANDLING
        }
        return vehicleBuild;
    }    
    
}        