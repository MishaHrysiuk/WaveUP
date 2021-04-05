using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaveUP.Domain.Entities;

namespace WaveUP.WebUI.Models
{
    public class InstrumentsListViewModel
    {
        public IEnumerable<Instrument> Instruments { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}