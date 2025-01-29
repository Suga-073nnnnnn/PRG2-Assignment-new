//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee S10259250
//==========================================================
class Program
{
    static void Main(string[] args)
    {
        string filePath = "flights.csv";
        Dictionary<string, Flight> flights = LoadFlightsFromCsv(filePath);

        DisplayFlightInformation(flights);
    }

    static Dictionary<string, Flight> LoadFlightsFromCsv(string filePath)
    {
        var flights = new Dictionary<string, Flight>();

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines.Skip(1)) // Skip header line
        {
            var fields = line.Split(',');

            if (fields.Length != 5)
                throw new FormatException("CSV file format is incorrect.");

            string flightNumber = fields[0].Trim();
            string origin = fields[1].Trim();
            string destination = fields[2].Trim();
            DateTime expectedTime = DateTime.Parse(fields[3].Trim());
            string specialRequestCode = fields[4].Trim();

            Flight newFlight = CreateFlightInstance(flightNumber, origin, destination, expectedTime, specialRequestCode);
            flights[flightNumber] = newFlight;
        }

        return flights;
    }

    static Flight CreateFlightInstance(string flightNumber, string origin, string destination, DateTime expectedTime, string specialRequestCode)
    {
        return specialRequestCode switch
        {
            "CFFT" => new CFFTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 150),
            "DDJB" => new DDJBFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 300),
            "LWTT" => new LWTTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 500),
            _ => new NORMFlight(flightNumber, origin, destination, expectedTime, "Scheduled")
        };
    }

    static void DisplayFlightInformation(Dictionary<string, Flight> flights)
    {
        Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}{4,-25}",
            "Flight No.", "Origin", "Destination", "Expected Time", "Special Request");
        Console.WriteLine(new string('-', 87));

        foreach (var flight in flights.Values)
        {
            string specialRequest = GetSpecialRequestCode(flight);
            Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}{4,-25}",
                flight.FlightNumber,
                flight.Origin,
                flight.Destination,
                flight.ExpectedTime.ToString("hh:mm tt"),
                specialRequest);
        }
    }

    static string GetSpecialRequestCode(Flight flight)
    {
        return flight switch
        {
            CFFTFlight => "CFFT",
            DDJBFlight => "DDJB",
            LWTTFlight => "LWTT",
            _ => "NORM"
        };
    }
    //Gabriel's 
    Dictionary<Airline> AirlineDict = new Dictionary<Airline>;
    static Dictionary<string, Airline> LoadAirlineFromCSV(string filename)
    {
        var airlines = new Dictionary<string, Airline>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(',');
            string Name = fields[0].Trim();
            string Code = fields[1].Trim();
        }
    }
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
            bool SupportsLWWT = Convert.ToBoolean(fields[3].Trim());
        }
    }
    static ListBoardingGate(Dictionary<string, BoardingGate> gateDict)
    {
        Console.WriteLine($"{0,20} {1,10} {1,10} {1,10}", "Flight No.", "DDJB", "CFFT", "LWTT");
        foreach (var gate in gateDict.Values)
        {
            if (gate == null) continue;
            Console.WriteLine($"{0,20} {1,10} {1,10} {1,10}", gate.Gate, gate.SupportsDDJB, gate.SupportsCFFT, gate.Supports.LWTT);
        }
    }

    static void modification(Dictionary<string, Flight> flights)
    {
        DisplayFlightInformation(Dictionary<string, Flight> flights);
        Console.Write("Enter a 2-Letter Airline Code:");
        string inputcode = Console.ReadLine();
        foreach (var flight in flights.Values)
        {
            if (inputcode in flight.FlightNumber) //idk :cry:
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
                if (flight.Flight = inputNum)
                {
                    if (option1 == 1)
                    {
                        Console.Write("Enter New Origin:");
                        string newe = Console.ReadLine();
                        flights[flight.Origin] = newe;
                    }
                    if (option1 == 2)
                    {
                        Console.Write("Enter new Destination:");
                        string newe = Console.ReadLine();
                        flights[flight.Destination] = newe;
                    }
                    if (option1 == 3)
                    {
                        Console.Write("Enter new Expected Departure/Arrival:");
                        string newe = Console.ReadLine();
                        flights[flight.ExpectedTime] = newe;
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
        if (option == 2)
        {
            Console.Write("Enter a flight number to delete:");
            string delete = Console.ReadLine();
            foreach (var flight in flights.Values)
            {
                if (flight.FlightNumber = delete)
                {
                    flight.Delete();
                    Console.WriteLine("Deletion Complete");
                }
            }
        }
        DisplayFlightInformation(Dictionary<string, Flight>flights);
    }
}