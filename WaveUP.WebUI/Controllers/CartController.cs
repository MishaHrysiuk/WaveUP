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
    public class CartController : Controller
    {
        private IInstrumentRepository repository;
        public CartController(IInstrumentRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int instrumentId, string returnUrl)
        {
            Instrument instrument = repository.Instruments
                .FirstOrDefault(g => g.InstrumentId == instrumentId);

            if (instrument != null)
            {
                cart.AddItem(instrument, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int instrumentId, string returnUrl)
        {
            Instrument instrument = repository.Instruments
                .FirstOrDefault(g => g.InstrumentId == instrumentId);

            if (instrument != null)
            {
                cart.RemoveLine(instrument);
            }
            return RedirectToAction("Index", new { returnUrl });
            
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}