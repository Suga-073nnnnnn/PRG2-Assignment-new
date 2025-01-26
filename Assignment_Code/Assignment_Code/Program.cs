//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee
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
}