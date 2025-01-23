using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class flight
    {
        public string flightNumber { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime expectedTime { get; set; }
        public string status { get; set; }
        public flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            this.flightNumber = flightNumber;
            this.origin = origin;
            this.destination = destination;
            this.expectedTime = expectedTime;
            this.status = status;
        }
        public override double CalculateFee() { };
        public override string ToString()
        {
            return base.ToString();
        }
        
    }
}
