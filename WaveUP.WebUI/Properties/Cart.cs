using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveUP.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Instrument instrument, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Instrument.InstrumentId == instrument.InstrumentId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Instrument = instrument,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Instrument instrument)
        {
            lineCollection.RemoveAll(l => l.Instrument.InstrumentId == instrument.InstrumentId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Instrument.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
    public class CartLine
    {
        public Instrument Instrument { get; set; }
        public int Quantity { get; set; }
    }
}
