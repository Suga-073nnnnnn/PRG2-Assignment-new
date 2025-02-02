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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWT = supportsLWT;
            Flight = flight;         //not sure, might change later, temp value (remove when decided/confirmed)
        }

        public double CalculateFees()
        {
            double fee = 300;
            return fee;
            /*if (Flight != null)     //Thinking of possibility to check if null in main before calling instead. *Need to discuss
            {
                return Flight.CalculateFee();
            }
            else
            {
                return 0.0;
            }*/
        }

        public override string ToString()       //Should be override. *Need to confirm with Gabriel but should work as intended.
        {
            // Format based on the spacing in the Sample output. *Might be changed / edited later (remove when confirmed)
            return string.Format("{0,-14}{1,-18}{2,-18}{3,-18}",
            GateName,
            SupportsDDJB,
            SupportsCFFT,
            SupportsLWT);

            /*string flightInfo = Flight != null ? Flight.FlightNumber : "Unassigned";              //Use this if want to do validation here instead of in main
            return string.Format("{0,-14}{1,-18}{2,-18}{3,-18}{4,-10}",
                GateName, SupportsDDJB, SupportsCFFT, SupportsLWT, flightInfo);*/

        }
    }
}
