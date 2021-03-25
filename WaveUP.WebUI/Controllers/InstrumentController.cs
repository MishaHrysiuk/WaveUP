using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;

namespace WaveUP.WebUI.Controllers
{
    public class InstrumentController : Controller
    {
        private IInstrumentRepository repository;
        public InstrumentController(IInstrumentRepository repo)
        {
            repository = repo;
        }
        public ViewResult List()
        {
            return View(repository.Instruments);
        }
    }
}