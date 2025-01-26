using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class Airline
    {
        public string code { get; set; }
        public string name { get; set; }
        
        public Dictionary<string, Flight> flights { get; set; } = new Dictionary<string, Flight>();


        public Airline(string Code, string Name)
        {
            code = Code;
            name = Name;
        }

        public void AddFlight(Flight flight)
        {
            Flights[flight.flightNumber] = flight;
        }

        public void RemoveFlight(string flightNumber)
        {
            Flights.Remove(flightNumber);
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

}
