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
    public class NORMFlight: flight
    {
        public override double CalculateFee()
        {
            return 500 + 800 + 300;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
