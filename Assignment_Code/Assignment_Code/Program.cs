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


    // Basic Feature 1 and 2 - Load Data
    static void LoadData()
    {
        Console.WriteLine("Loading Airlines...");

        // Slightly redundent to create a new terminal object here but idk how else to do it for now.
        terminal = new Terminal("Changi Airport Terminal 5", new Dictionary<string, Airline>(), new Dictionary<string, Flight>(), new Dictionary<string, BoardingGate>(), new Dictionary<string, double>());

        foreach (var line in File.ReadLines("airlines.csv").Skip(1))
        {
            var parts = line.Split(',');
            var airline = new Airline(parts[1], parts[0], new Dictionary<string, Flight>());
            terminal.AddAirline(airline);
        }
        Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");

        Console.WriteLine("Loading Boarding Gates...");
        foreach (var line in File.ReadLines("boardinggates.csv").Skip(1))
        {
            var parts = line.Split(',');
            var gate = new BoardingGate(parts[0], bool.Parse(parts[1]), bool.Parse(parts[2]), bool.Parse(parts[3]), null);
            terminal.AddBoardingGate(gate);
        }
        Console.WriteLine($"{terminal.BoardingGates.Count} Boarding Gates Loaded!");

        Console.WriteLine("Loading Flights...");
        foreach (var line in File.ReadLines("flights.csv").Skip(1))
        {
            var parts = line.Split(',');
            DateTime expectedTime = DateTime.Parse(parts[3]);
            Flight flight;

            switch (parts[4])
            {
                case "DDJB":
                    flight = new DDJBFlight(parts[0], parts[1], parts[2], expectedTime, "On Time", 300);
                    break;
                case "CFFT":
                    flight = new CFFTFlight(parts[0], parts[1], parts[2], expectedTime, "On Time", 150);
                    break;
                case "LWTT":
                    flight = new LWTTFlight(parts[0], parts[1], parts[2], expectedTime, "On Time", 500);
                    break;
                default:
                    flight = new NORMFlight(parts[0], parts[1], parts[2], expectedTime, "On Time");
                    break;
            }

            terminal.Flights.Add(flight.FlightNumber, flight);
            var airlineCode = parts[0].Split(' ')[0];
            if (terminal.Airlines.ContainsKey(airlineCode))
            {
                terminal.Airlines[airlineCode].AddFlight(flight);
            }
        }
        Console.WriteLine($"{terminal.Flights.Count} Flights Loaded!");
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
                    //ListBoardingGates();
                    break;
                case "3":
                    AssignBoardingGate();
                    break;
                case "4":
                    CreateFlight();
                    break;
                case "5":
                    //DisplayAirlineFlights();
                    break;
                case "6":
                    //ModifyFlightDetails();
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
  
  
    //part 7     //Basic 7    
    static void DisplayAirlineFlights(Dictionary<string, Flight> flights,Dictionary<string, Airline>airlines)
    {
        Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}{4,-25}",
            "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time Departure/Arrival Time");
        Console.WriteLine(new string('-', 87));
  
            foreach (var airline in airlines.Values)
            {
                string specialRequest = GetSpecialRequestCode(flight);
                Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}{4,-25}",
                    flight.FlightNumber,
                    airline.Name,
                    flight.Origin,
                    flight.Destination,
                    flight.ExpectedTime.ToString("hh:mm tt"));
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

    //Gabriel's 
    //load files for airline    //Basic 1
    /*static Dictionary<string, Airline> LoadAirlineFromCSV(string filename)
    {
        var airlines = new Dictionary<string, Airline>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string Name = fields[0].Trim();
            string Code = fields[1].Trim();
        }
        return airlines;
    }
    //load files for boarding gate         //Basic 1
    static Dictionary<string, BoardingGate> LoadGateFromCSV(string filename)
    {
        var gates = new Dictionary<string, BoardingGate>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string Gate = fields[0].Trim();
            bool SupportsDDJB = Convert.ToBoolean(fields[1].Trim());
            bool SupportsCFFT = Convert.ToBoolean(fields[2].Trim());
            bool SupportsLWTT = Convert.ToBoolean(fields[3].Trim());
        }
        return gates;
    }*/
      
    //listing boarding gates      //Basic 4
    static void ListBoardingGates(Dictionary<string, BoardingGate> gateDict,Dictionary<string, Flight>flights)
    {
        Console.WriteLine($"{0,20} {1,10} {1,10} {1,10}", "Flight No.", "DDJB", "CFFT", "LWTT");
        foreach (var gate in gateDict.Values)
        {
            foreach (var flight in flights.Values)
            {
                if (gate == null) continue;
                Console.WriteLine($"{0,20} {1,10} {1,10} {1,10}", flight.FlightNumber, gate.SupportsDDJB, gate.SupportsCFFT, gate.SupportsLWT);

            }
        }
    }
      
    //modifying flight details       //Basic 8
    static void ModifyFlightDetails(Dictionary<string, Flight> flights,Dictionary<string,Airline>airlines)
    {
        DisplayAirlineFlights(flights,airlines);
        Console.Write("Enter a 2-Letter Airline Code:");
        string inputcode = Console.ReadLine();
        foreach (var flight in flights.Values)
        {
            if (inputcode == flight.FlightNumber) 
            { 
                string specialRequest = GetSpecialRequestCode(flight);
                Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}",
                    flight.FlightNumber,
                    flight.Origin,
                    flight.Destination);
            }
        }
        Console.WriteLine("[1] Choose an existing flight to modify");
        Console.WriteLine("[2] Choose an existing flight to delete");
        Console.Write("Choose an option: ");
        int option = Convert.ToInt32(Console.ReadLine());
        //modify
        if (option == 1)
        {
            Console.Write("Choose a flght number");
            string inputNum = Console.ReadLine();
            Console.WriteLine("[1] Origin");
            Console.WriteLine("[2] Destination");
            Console.WriteLine("[3] Expected Departure/Arrival");
            Console.WriteLine("[4]Special Request Code");
            Console.Write("Enter an option to modify:");
            int option1 = Convert.ToInt32(Console.ReadLine());
            foreach (var flight in flights.Values)
            {
                if (flight.FlightNumber == inputNum)
                {
                    if (option1 == 1)
                    {
                        Console.Write("Enter New Origin:");
                        flight.Origin = Console.ReadLine();
                    }
                    if (option1 == 2)
                    {
                        Console.Write("Enter new Destination:");
                        flight.Destination = Console.ReadLine();
                    }
                    if (option1 == 3)
                    {
                        Console.Write("Enter new Expected Departure/Arrival:");
                        flight.ExpectedTime = Convert.ToDateTime(Console.ReadLine());
                    }
                    if (option1 == 4)
                    {
                        Console.Write("Enter new Special Request Code:");
                        string specialRequest = GetSpecialRequestCode(flight);
                        specialRequest = Console.ReadLine();
                    }
                }
            }
        }
        //delete
        if (option == 2)
        {
            Console.Write("Enter a flight number to delete:");
            string delete = Console.ReadLine();
            foreach (var flight in flights.Values)
            {
                Console.WriteLine("[Y] Yes");
                Console.WriteLine("[N] No");
                Console.WriteLine("Are you sure?");
                string input = Console.ReadLine();
                if (input == "Y")
                {
                    if (flight.FlightNumber == delete)
                    {
                        bool isRemoved = flights.Remove(delete);
                        if (isRemoved)
                        {
                            Console.WriteLine("Deletion Complete");
                        }
                        else
                        {
                            Console.WriteLine("Flight not found");
                        }

                    }
                }
                else { Console.WriteLine("ok"); }
            }
        }
        
    }
    //advance feature attempt (a)
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

}

