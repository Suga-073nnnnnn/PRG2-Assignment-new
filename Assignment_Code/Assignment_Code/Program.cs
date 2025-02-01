//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee S10259250
//==========================================================
using S10270022_PRG2Assignment;
//>>>>>>> 47c2373d0f2ad76b59b81c630801e4ddec9c8bf6

class Program
{
    static void Main(string[] args)
    {
        string filePath = "flights.csv";
        string filepath2 = "airlines.csv";
        string filepath3 = "boardinggates.csv";
        Dictionary<string, Flight> flights = LoadFlightsFromCsv(filePath);
        Dictionary<string, Airline> AirlineDict = LoadAirlineFromCSV(filepath2);
        Dictionary<string, BoardingGate> GateDict = LoadGateFromCSV(filepath3);

        //formating start
        bool eaweaw = true;
        while (eaweaw = true)
        {
            Console.WriteLine("=============================================");
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
            Console.WriteLine();
            Console.WriteLine("Please select your option: ");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                //part 3
            }
            else if (option == 2)
            {
                ListBoardingGate(GateDict);
            }
            else if (option == 3)
            {
                //part 5
            }
            else if (option == 4)
            {
                //part 6
            }
            else if (option == 5)
            {
                DisplayFlightInformation(flights,AirlineDict);
            }
            else if (option == 6)
            {
                modification(flights,AirlineDict);
            }
            else if (option == 7)
            {
                //part 9
            }
            else if (option == 0)
            {
                eaweaw = false;
                break;
            }
        }
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

    //part 7
    static void DisplayFlightInformation(Dictionary<string, Flight> flights,Dictionary<string, Airline>airlines)
    {
        Console.WriteLine("{0,-12}{1,-20}{2,-20}{3,-20}{4,-25}",
            "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time Departure/Arrival Time");
        Console.WriteLine(new string('-', 87));

        foreach (var flight in flights.Values)
        {
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


<<<<<<< HEAD
    //Gabriel's 
    //load files for airline 
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
        return airlines;
    }
    //load files for boarding gate
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
    }
    //listing boarding gates
    static void ListBoardingGate(Dictionary<string, BoardingGate> gateDict,Dictionary<string, Flight>flights)
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
    //modifying flight details
    static void modification(Dictionary<string, Flight> flights,Dictionary<string,Airline>airlines)
    {
        DisplayFlightInformation(flights,airlines);
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
=======
>>>>>>> 47c2373d0f2ad76b59b81c630801e4ddec9c8bf6
}