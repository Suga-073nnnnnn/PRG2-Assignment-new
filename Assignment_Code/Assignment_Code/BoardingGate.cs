using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Code
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWT { get; set; }
        public flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWT = supportsLWT;
            Flight = null; //not sure, might change later, temp value (remove when decided/confirmed)
        }

        public double CalculateFees()
        {
            if (Flight != null)     //Thinking of possibility to check if null in main before calling instead. *Need to discuss
            {
                return Flight.CalculateFee();
            }
            else
            {
                return 0.0;
            }
        }

        public override string ToString()       //Should be override. *Need to confirm with Gabriel but should work as intended.
        {
            // Format based on the spacing in the Sample output. *Might be changed / edited later (remove when confirmed)
            return string.Format("{0,-14}{1,-18}{2,-18}{3,-18}",
            GateName,
            SupportsDDJB,
            SupportsCFFT,
            SupportsLWT);
        }
    }
}
