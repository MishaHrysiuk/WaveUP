using System.Web.Mvc;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;
using System.Linq;

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

        public ViewResult Edit(int instrumentId)
        {
            Instrument instrument = repository.Instruments
                .FirstOrDefault(g => g.InstrumentId == instrumentId);
            return View(instrument);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                repository.SaveInstrument(instrument);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", instrument.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(instrument);
            }
        }
    }
}