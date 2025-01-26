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
    public class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFees) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 300;
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
