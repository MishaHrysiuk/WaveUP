using System.Web.Mvc;
using WaveUP.Domain.Abstract;

namespace WaveUP.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IInstrumentRepository repository;

        public AdminController(IInstrumentRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Instruments);
        }
    }
}