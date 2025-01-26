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
