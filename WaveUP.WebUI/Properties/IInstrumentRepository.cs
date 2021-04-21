using System;
using System.Collections.Generic;
using WaveUP.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveUP.Domain.Abstract
{
    public interface IInstrumentRepository
    {
        IEnumerable<Instrument> Instruments { get; }
        void SaveInstrument(Instrument instrument);
        Instrument DeleteInstrument(int instrumentId);
    }
}
