using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class CFFTFlight: flight
    {
        public double requestFees {  get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFees):base(flightNumber,origin, destination, expectedTime, status)
        {
            requestFees = 150;
        }
        public override double CalculateFee()
        {
            return 500 + 800 + 300 + requestFees;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
