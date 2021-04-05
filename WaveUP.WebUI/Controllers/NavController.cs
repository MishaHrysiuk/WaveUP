using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveUP.Domain.Abstract;


namespace WaveUP.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IInstrumentRepository repository;

        public NavController(IInstrumentRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string category = null)
        {
            IEnumerable<string> categories = repository.Instruments
                .Select(instrument => instrument.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}