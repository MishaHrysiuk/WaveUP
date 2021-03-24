using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveUP.Domain.Entities
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
