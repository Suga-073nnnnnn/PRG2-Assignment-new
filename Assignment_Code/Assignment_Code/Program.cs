//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee
//==========================================================
using S10270022_PRG2Assignment;

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


}
