//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee S10259250
//==========================================================
using S10270022_PRG2Assignment;
//>>>>>>> 47c2373d0f2ad76b59b81c630801e4ddec9c8bf6

class Program
{

    static Terminal terminal;

    //Program Start
    static void Main(string[] args)
    {
        LoadData();
        DisplayMenu();
    }

    /*Might have to uodate entire code if found a way to call the 
    terminal object from the class instead of creating a new onject here
    *remove this line if done*/


    // Basic Feature 1 and 2 - Load airlines.csv, boardinggates.csv, flights.csv
    static void LoadData()
    {
        Console.WriteLine("Loading Airlines...");
        terminal = new Terminal("Changi Airport Terminal 5", new Dictionary<string, Airline>(), new Dictionary<string, Flight>(), new Dictionary<string, BoardingGate>(), new Dictionary<string, double>());

        terminal.Airlines = LoadAirlineFromCSV("airlines.csv");
        Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");

        Console.WriteLine("Loading Boarding Gates...");
        terminal.BoardingGates = LoadGateFromCSV("boardinggates.csv");
        Console.WriteLine($"{terminal.BoardingGates.Count} Boarding Gates Loaded!");

        Console.WriteLine("Loading Flights...");
        terminal.Flights = LoadFlightFromCSV("flights.csv");
        Console.WriteLine($"{terminal.Flights.Count} Flights Loaded!");
    }

    // Load Airlines from CSV
    static Dictionary<string, Airline> LoadAirlineFromCSV(string filename)
    {
        var airlines = new Dictionary<string, Airline>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string name = fields[0].Trim();
            string code = fields[1].Trim();
            var airline = new Airline(code, name, new Dictionary<string, Flight>());
            airlines.Add(code, airline);
        }
        return airlines;
    }

    // Load Boarding Gates from CSV
    static Dictionary<string, BoardingGate> LoadGateFromCSV(string filename)
    {
        var gates = new Dictionary<string, BoardingGate>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string gateName = fields[0].Trim();
            bool supportsDDJB = Convert.ToBoolean(fields[1].Trim());
            bool supportsCFFT = Convert.ToBoolean(fields[2].Trim());
            bool supportsLWTT = Convert.ToBoolean(fields[3].Trim());

            var gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, null);
            gates.Add(gateName, gate);
        }
        return gates;
    }

    // Load Flights from CSV
    static Dictionary<string, Flight> LoadFlightFromCSV(string filename)
    {
        var flights = new Dictionary<string, Flight>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string flightNumber = fields[0].Trim();
            string origin = fields[1].Trim();
            string destination = fields[2].Trim();
            DateTime expectedTime = DateTime.Parse(fields[3].Trim());
            string specialRequest = fields.Length > 4 ? fields[4].Trim() : "None";

            Flight flight;
            switch (specialRequest)
            {
                case "DDJB":
                    flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time", 300);
                    break;
                case "CFFT":
                    flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "On Time", 150);
                    break;
                case "LWTT":
                    flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "On Time", 500);
                    break;
                default:
                    flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time");
                    break;
            }

            flights.Add(flight.FlightNumber, flight);
            var airlineCode = flightNumber.Split(' ')[0];
            if (terminal.Airlines.ContainsKey(airlineCode))
            {
                terminal.Airlines[airlineCode].AddFlight(flight);
            }
        }
        return flights;
    }


    // Main Program Menu Loop
    static void DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");

            Console.Write("Please select your option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListAllFlights();
                    break;
                case "2":
                    ListBoardingGates();
                    break;
                case "3":
                    AssignBoardingGate();
                    break;
                case "4":
                    CreateFlight();
                    break;
                case "5":
                    DisplayAirlineFlights();
                    break;
                case "6":
                    ModifyFlightDetails();
                    break;
                case "7":
                    DisplayFlightSchedule();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }


    // Basic Feature 3 - List All Flights
    static void ListAllFlights()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Time");
        foreach (var flight in terminal.Flights.Values)
        {
            var airline = terminal.GetAirlineFromFlight(flight);
            Console.WriteLine($"{flight.FlightNumber,-15}{airline?.Name,-20}{flight.Origin,-22}{flight.Destination,-22}{flight.ExpectedTime}");
        }
    }

    //listing boarding gates      //Basic Feature 4
    static void ListBoardingGates()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Gate Name       DDJB                   CFFT                   LWTT");

        foreach (var gate in terminal.BoardingGates.Values)
        {
            Console.WriteLine($"{gate.GateName,-15}{gate.SupportsDDJB,-22}{gate.SupportsCFFT,-22}{gate.SupportsLWT,-22}");
        }
    }



    // Basic Feature 5 - Assign Boarding Gate to Flight

    static void AssignBoardingGate()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");

        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();

        if (terminal.Flights.TryGetValue(flightNumber, out Flight flight))
        {
            Console.Write("Enter Boarding Gate Name: ");
            string gateName = Console.ReadLine();

            if (terminal.BoardingGates.TryGetValue(gateName, out BoardingGate gate) && gate.Flight == null)
            {
                gate.Flight = flight;

                Console.Write("Would you like to update the status of the flight? (Y/N): ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    Console.WriteLine("1. Delayed\n2. Boarding\n3. On Time");
                    Console.Write("Please select the new status of the flight: ");
                    switch (Console.ReadLine())
                    {
                        case "1": flight.Status = "Delayed"; break;
                        case "2": flight.Status = "Boarding"; break;
                        case "3": flight.Status = "On Time"; break;
                    }
                }
                Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {gate.GateName}!");
            }
            else
            {
                Console.WriteLine("Boarding Gate is already assigned or doesn't exist.");
            }
        }
        else
        {
            Console.WriteLine("Flight not found.");
        }
    }


    // Basic Feature 6 - Create new Flight             *  Loop to prompt user to ask if want to add again is not implemented yet
                                                     //    * After Creating new flight, Airline Name not shown  - due to the getting airline name code being only in the loading data part. 
                                                       //     remove when done / errorfix 
    static void CreateFlight()
    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();


        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();

        Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        DateTime expectedTime;
        while (!DateTime.TryParse(Console.ReadLine(), out expectedTime))
        {
            Console.Write("Invalid date format. Please re-enter: ");
        }

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string specialRequest = Console.ReadLine().ToUpper();

        Flight newFlight;
        switch (specialRequest)
        {

            case "DDJB":
                newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "On Time", 300);
                break;
            case "CFFT":
                newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "On Time", 150);
                break;
            case "LWTT":
                newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "On Time", 500);
                break;
            default:
                newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time");
                break;
        }

        terminal.Flights.Add(newFlight.FlightNumber, newFlight);
        Console.WriteLine($"Flight {newFlight.FlightNumber} has been added!");
    }




    // new basic 7                  - Updated and fit with rest of code
    static void DisplayAirlineFlights()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");

        foreach (var airline in terminal.Airlines.Values)
        {
            Console.WriteLine($"{airline.Code} - {airline.Name}");
        }

        Console.Write("Enter Airline Code: ");
        string code = Console.ReadLine();

        if (terminal.Airlines.TryGetValue(code, out Airline airlineFound))
        {
            Console.WriteLine($"=============================================");
            Console.WriteLine($"List of Flights for {airlineFound.Name}");
            Console.WriteLine($"=============================================");

            foreach (var flight in airlineFound.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber} - {flight.Origin} to {flight.Destination} at {flight.ExpectedTime}");
            }
        }
        else
        {
            Console.WriteLine("Airline not found.");
        }
    }



    //Basic 8 - modify flight details               *Updated it (error fix and match with rest of code)

    static void ModifyFlightDetails()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Modify Flight Details");
            Console.WriteLine("=============================================");

            // Display Airlines and prompt user
            foreach (var mAirline in terminal.Airlines.Values)
            {
                Console.WriteLine($"{mAirline.Code} - {mAirline.Name}");
            }

            Console.Write("Enter a 2-Letter Airline Code: ");
            string airlineCode = Console.ReadLine().ToUpper();

            if (terminal.Airlines.TryGetValue(airlineCode, out Airline airline))
            {
                Console.WriteLine($"\nFlights for {airline.Name}:");
                foreach (var flight in airline.Flights.Values)
                {
                    Console.WriteLine($"{flight.FlightNumber} - {flight.Origin} to {flight.Destination} at {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
                }

                Console.WriteLine("\n[1] Choose an existing flight to modify");
                Console.WriteLine("[2] Choose an existing flight to delete");
                Console.Write("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    ModifyExistingFlight(airline);
                }
                else if (choice == 2)
                {
                    DeleteExistingFlight(airline);
                }
            }
            else
            {
                Console.WriteLine("Airline not found.");
            }
        }

        static void ModifyExistingFlight(Airline airline)
        {
            Console.Write("Enter the Flight Number to modify: ");
            string flightNumber = Console.ReadLine();

            if (airline.Flights.TryGetValue(flightNumber, out Flight flight))
            {
                Console.WriteLine("\n[1] Modify Origin");
                Console.WriteLine("[2] Modify Destination");
                Console.WriteLine("[3] Modify Expected Departure/Arrival Time");
                Console.WriteLine("[4] Modify Special Request Code");
                Console.Write("Enter an option to modify: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter New Origin: ");
                        flight.Origin = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter New Destination: ");
                        flight.Destination = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Enter New Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                        DateTime newTime;
                        if (DateTime.TryParse(Console.ReadLine(), out newTime))
                        {
                            flight.ExpectedTime = newTime;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Date Format.");
                        }
                        break;
                    case 4:
                        Console.Write("Enter New Special Request Code (CFFT/DDJB/LWTT/None): ");
                        string newCode = Console.ReadLine().ToUpper();
                        UpdateSpecialRequestCode(flight, newCode);
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("Flight details updated successfully!");
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }

        static void UpdateSpecialRequestCode(Flight flight, string newCode)
        {
            switch (newCode)
            {
                case "DDJB":
                    flight = new DDJBFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 300);
                    break;
                case "CFFT":
                    flight = new CFFTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 150);
                    break;
                case "LWTT":
                    flight = new LWTTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 500);
                    break;
                default:
                    flight = new NORMFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status);
                    break;
            }
        }

        static void DeleteExistingFlight(Airline airline)
        {
            Console.Write("Enter the Flight Number to delete: ");
            string flightNumber = Console.ReadLine();

            if (airline.Flights.ContainsKey(flightNumber))
            {
                Console.Write("Are you sure you want to delete this flight? (Y/N): ");
                string confirmation = Console.ReadLine().ToUpper();

                if (confirmation == "Y")
                {
                    airline.Flights.Remove(flightNumber);
                    terminal.Flights.Remove(flightNumber);
                    Console.WriteLine("Flight deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Flight deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }



    // Basic Feature 9: Display Scheduled Flights Chronologically / IComeparable Interface (in flight class)
    static void DisplayFlightSchedule()
    {
        var sortedFlights = terminal.Flights.Values.OrderBy(f => f.ExpectedTime);
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Time     Status");
        foreach (var flight in sortedFlights)
        {
            Airline airline = terminal.GetAirlineFromFlight(flight);
            string airlineName = airline != null ? airline.Name : "Unknown";
            Console.WriteLine($"{flight.FlightNumber,-15}{airlineName,-20}{flight.Origin,-22}{flight.Destination,-22}{flight.ExpectedTime,-20}{flight.Status}");
        }
    }


    
    //advance feature attempt (a) Gabriel
    static void ProcessUnasignBulk(Dictionary<string, BoardingGate> gateDict,Dictionary<string, Flight> flights)
    {
        //for each flight 
        Queue<string> queue1 = new Queue<string>();
        foreach (var flight in flights.Values)
        {
            foreach (var gate in gateDict.Values)
            {
                if (gate == null)
                {
                    queue1.Enqueue(flight.FlightNumber);
                }
            }
        }
        Console.WriteLine("The total number of Flights that do not have a boarding gates assigned is  " + queue1.Count);

        //for each boarding gate
        Queue<string> queue = new Queue<string>();

        foreach (var flight in flights.Values)
        {
            foreach (var gate in gateDict.Values)
            {
                if (gate.Flight == null)
                {
                    queue.Enqueue(flight.FlightNumber);
                }
            }
        }
        Console.WriteLine("The total number of Boarding Gates that do not have a flight number assigned is "+queue.Count);

        //dequeueing
        foreach (var e in queue)
        {
            queue.Dequeue();
            
        }

    }

}



