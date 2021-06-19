using System.Web.Mvc;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;
using System.Linq;
using System.Web;

namespace WaveUP.WebUI.Controllers
{
    [Authorize]
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

        public ViewResult Create()
        {
            return View("Edit", new Instrument());
        }

        public ViewResult Edit(int instrumentId)
        {
            Instrument instrument = repository.Instruments
                .FirstOrDefault(g => g.InstrumentId == instrumentId);
            return View(instrument);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Instrument instrument, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    instrument.ImageMimeType = image.ContentType;
                    instrument.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(instrument.ImageData, 0, image.ContentLength);
                }
                repository.SaveInstrument(instrument);
                TempData["message"] = string.Format("Зміна товару \"{0}\" були збережені", instrument.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(instrument);
            }
        }

        [HttpPost]
        public ActionResult Delete(int instrumentId)
        {
            Instrument deletedInstrument = repository.DeleteInstrument(instrumentId);
            if (deletedInstrument != null)
            {
                TempData["message"] = string.Format("Інструмент \"{0}\" був видалений",
                    deletedInstrument.Name);
            }
            return RedirectToAction("Index");
        }
    }
}