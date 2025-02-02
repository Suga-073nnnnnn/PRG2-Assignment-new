//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee S10259250
//==========================================================
using S10270022_PRG2Assignment;
//>>>>>>> 47c2373d0f2ad76b59b81c630801e4ddec9c8bf6

class Program
{
        



    //part 7     //Basic 7
    static void DisplayAirlineFlights(Dictionary<string, Flight> flights,Dictionary<string, Airline>airlines)
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

    //Gabriel's 
    //load files for airline    //Basic 1
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
    }
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