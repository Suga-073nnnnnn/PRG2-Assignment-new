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
        public double RequestFee { get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee):base(flightNumber,origin, destination, expectedTime, status)
        {
            RequestFee = 150;
        }
        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;               // changed to return base function
            //return RequestFees + 500 + 800;                
        }
        public override string ToString()
        {
            return base.ToString() + "Special Request Code: CFFT";
        }
    }
}
