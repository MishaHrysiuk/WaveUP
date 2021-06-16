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
        private IOrderProcessor orderProcessor;
        public CartController(IInstrumentRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
   
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Вибачте, ваш кошик порожній!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
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
        public RedirectToRouteResult MinusOneCart(Cart cart, int instrumentId, string returnUrl)
        {
            Instrument instrument = repository.Instruments
                .FirstOrDefault(g => g.InstrumentId == instrumentId);

            if (instrument != null)
            {
                cart.MinusItem(instrument, 1);
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