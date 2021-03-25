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
    }
}