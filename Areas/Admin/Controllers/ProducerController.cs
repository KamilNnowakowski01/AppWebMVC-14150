using AppModel.Models;
using AppWebMVC.Services;
using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace AppWebMVC.Areas.Admin.Controllers
{
    [Area(RolesTypes.Admin)]
    [Authorize(Roles = RolesTypes.Admin)]
    public class ProducerController : Controller
    {
        private readonly IProducerServices _producerServices;
        public ProducerController(IProducerServices producerServices)
        {
            _producerServices = producerServices;
        }
        // GET: ProducerController
        public ActionResult Index()
        {
            var producers = _producerServices.GetAll();
            return View(producers);
        }

        // GET: ProducerController/Details/5
        public ActionResult Details(int id)
        {
            var producer = _producerServices.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // GET: ProducerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producer producer)
        {
            if (ModelState.IsValid)
            {
                _producerServices.Create(producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        // GET: ProducerController/Edit/5
        public ActionResult Edit(int id)
        {
            var producer = _producerServices.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: ProducerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _producerServices.Update(producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: ProducerController/Delete/5
        public ActionResult Delete(int id)
        {
            var producer = _producerServices.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: ProducerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _producerServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
