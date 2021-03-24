using System;
using System.Collections.Generic;
using WaveUP.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveUP.Domain.Abstract
{
    interface IGameRepository
    {
        IEnumerable<Instrument> Instruments { get; }
    }
}
