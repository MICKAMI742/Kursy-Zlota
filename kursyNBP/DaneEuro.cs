using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursyNBP
{
    public class TablicaDanychEuro
    {
        // Data is storaged in the list so i need to do list of DaneEuro object
        public DaneEuro[] rates { get; set; }

    }
    public class DaneEuro
    {
        public DateTime? EffectiveDate { get; set; }
        public double? ask { get; set; }
    }
}
