using System.Collections.Generic;
using WaveUP.Domain.Entities;
using WaveUP.Domain.Abstract;

namespace WaveUP.Domain.Concrete
{
    public class EFInstrumentRepository : IInstrumentRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Instrument> Instruments
        {
            get { return context.Instruments; }
        }

        public void SaveInstrument(Instrument instrument)
        {
            if (instrument.InstrumentId == 0)
                context.Instruments.Add(instrument);
            else
            {
                Instrument dbEntry = context.Instruments.Find(instrument.InstrumentId);
                if (dbEntry != null)
                {
                    dbEntry.Name = instrument.Name;
                    dbEntry.Description = instrument.Description;
                    dbEntry.Price = instrument.Price;
                    dbEntry.Category = instrument.Category;
                }
            }
            context.SaveChanges();
        }

        public Instrument DeleteInstrument(int instrumentId)
        {
            Instrument dbEntry = context.Instruments.Find(instrumentId);
            if (dbEntry != null)
            {
                context.Instruments.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}