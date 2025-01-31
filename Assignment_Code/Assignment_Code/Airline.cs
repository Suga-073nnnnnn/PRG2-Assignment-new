//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee S10259250
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270022_PRG2Assignment
{
    class Airline
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();


        public Airline(string code, string name, Dictionary<string, Flight> flight )
        {
            Code = code;
            Name = name;
            Flights = flight;
        }

        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber) == false)
            {
                return false;
            }
            Flights.Remove(flight.FlightNumber);
            return true;
        }

        public double CalculateFees()       // Need to test with Flight classes(Gabriel) / in main to see if calculation works correctly
        {
            double totalFees = 0.0;
            foreach (var flight in Flights.Values)      //Discuss on this part.
            {
                totalFees += flight.CalculateFee();
            }
            return totalFees;

        }

        public override string ToString()       // Temp ToString() method. *Update it per the sample output doc during testing.
        {
            return $"Airline Code: {Code}, Airline Name: {Name}, Number of Flights: {Flights.Count}";
        }

    }
}