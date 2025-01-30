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

namespace S10270022_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal(string terminalName, Dictionary<string, Airline> airline, Dictionary<string, Flight> flight, Dictionary<string, BoardingGate> boardingGate, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airline;
            Flights = flight;
            BoardingGates = boardingGate;
            GateFees = gateFees;
        }


        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }
            Airlines.Add(airline.Code, airline);
            return true;
        }


        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }
            BoardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            if(!Airlines.ContainsKey(flight.FlightNumber))
            {
                return null;
            }
            return Airlines[flight.FlightNumber];

            /* foreach (var airline in Airlines.Values)
             {
                 if (airline.Flights.ContainsKey(flight.flightNumber))
                 {
                     return airline;
                 }
             }
             return null;*/
        }

        public void PrintAirlineFees()
        {
            foreach (KeyValuePair<string, double> fee in GateFees)
            {
                Console.WriteLine(fee.Key+"\t"+fee.Value);  // temp formatting * edit when testing program
            }
        }

        public override string ToString()    // Wrong implementation, needs to be fixed after check * Unsure of what to return, thus temp random values
        {
            return $"Terminal: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Gates: {BoardingGates.Count}";
        }

    }
}   
