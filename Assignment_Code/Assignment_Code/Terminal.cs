//==========================================================
// Student Number	: S10270022
// Student Name	: Suganesan
// Partner Name	: Gabriel Lee
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            BoardingGates = new Dictionary<string, BoardingGate>();
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, flight>();
            GateFees = new Dictionary<string, double>();
        }


        public void AddAirline(Airline airline)
        {
            Airlines[airline.AirlineCode] = airline;
            return true;
        }


        public void AddBoardingGate(BoardingGate gate)
        {
            BoardingGates[gate.GateName] = gate;
            return true;
        }

        public Airline GetAirlineFromFlight(flight flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.flightNumber))
                {
                    return airline;
                }
            }
            return null; // if flight not found
        }

        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines.Values)
            {
                Console.WriteLine($"{airline.Name} Fees: {airline.CalculateFees():C}");  // temp formatting * edit when testing program
            }
        }

        public override string ToString()    // Wrong implementation, needs to be fixed after check * Unsure of what to return, thus temp random values
        {
            return $"Terminal: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Gates: {BoardingGates.Count}";
        }

    }
}   
