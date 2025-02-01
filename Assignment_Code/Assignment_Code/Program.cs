//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee
//==========================================================
using S10270022_PRG2Assignment;

class Program
{

    static Terminal terminal;

    static void Main(string[] args)
    {
        LoadData();
        DisplayMenu();
    }

    static void LoadData()
    {
        Console.WriteLine("Loading Airlines...");
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
                    //AssignBoardingGate();
                    break;
                case "4":
                    //CreateFlight();
                    break;
                case "5":
                    //DisplayAirlineFlights();
                    break;
                case "6":
                    //ModifyFlightDetails();
                    break;
                case "7":
                    //DisplayFlightSchedule();
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


    // Basic Feature 2
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











}
