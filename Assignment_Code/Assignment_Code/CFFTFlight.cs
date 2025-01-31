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
    class CFFTFlight : Flight
    {
        public double RequestFees { get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFees):base(flightNumber,origin, destination, expectedTime, status)
        {
            RequestFees = requestFees;
        }
        public override double CalculateFee()
        {
            //return base.CalculateFee() + RequestFees;
            return RequestFees + 500 + 800;                 // arrive + leave + special code
        }
        public override string ToString()
        {
            return base.ToString() + "Special Request Code: CFFT";
        }
    }
}
