using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursyNBP
{
    public class TablicaDanychEuro
    {
        public DaneEuro[] rates { get; set; }

    }
    public class DaneEuro
    {
        public DateTime EffectiveDate { get; set; }
        public double ask { get; set; }
    }
}
