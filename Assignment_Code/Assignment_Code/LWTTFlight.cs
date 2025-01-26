using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class LWTTFlight: flight
    {
        public double requestFee { get; set; }
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFees) : base(flightNumber, origin, destination, expectedTime, status)
        {
            requestFee = 500;
        }
        public override double CalculateFee()
        {
            return 500 + 800 + 300 + requestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
