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

namespace Assignment_Code
{
    public class LWTTFlight: flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFees) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 500;
        }
        public override double CalculateFee()
        {
            return 500 + 800 + 300 + RequestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
