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
        public ViewResult List(int page = 1)
        {
            InstrumentsListViewModel model = new InstrumentsListViewModel
            {
                Instruments = repository.Instruments
                    .OrderBy(game => game.InstrumentId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Instruments.Count()
                }
            };
            return View(model);
        }
    }
}