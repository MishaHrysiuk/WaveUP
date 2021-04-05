using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;
using WaveUP.WebUI.Models;

namespace WaveUP.WebUI.Controllers
{
    public class InstrumentController : Controller
    {
        private IInstrumentRepository repository;
        public int pageSize = 4;
        public InstrumentController(IInstrumentRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string category, int page = 1)
        {
            InstrumentsListViewModel model = new InstrumentsListViewModel
            {
                Instruments = repository.Instruments
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(Instrument => Instrument.InstrumentId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        repository.Instruments.Count() :
        repository.Instruments.Where(instrument => instrument.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}