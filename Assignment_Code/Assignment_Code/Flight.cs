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
    public abstract class Flight
    {
        //attributes
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        //constructors
        public Flight() {}
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = "On Time";            //I think its supposed to be "On Time" by default
        }

        public virtual double CalculateFee()
        {
            double fee = 300; //boarding fee 
            if (Destination == "SIN")
            {
                fee += 500; //arriving fee
            }
            if (Origin == "SIN")
            {
                fee += 800; //arriving fee
            }
            return fee;
        }

        public override string ToString()
        {
            return $"{FlightNumber} from {Origin} to {Destination}"; 
        }
        
    }
}
