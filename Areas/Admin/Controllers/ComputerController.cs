using AppModel.Models;
using AppModel.ViewModels;
using AppWebMVC.Services;
using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Areas.Admin.Controllers
{
    [Area(RolesTypes.Admin)]
    [Authorize(Roles = RolesTypes.Admin)]
    public class ComputerController : Controller
    {
        private readonly IComputerServices _computerServices;
        private readonly IGraphicsServices _graphicsServices;
        private readonly IProducerServices _producerServices;
        private readonly IComputersGraphicsServices _computersGraphicsServices;


        public ComputerController(IComputerServices computerServices, IGraphicsServices graphicsServices, IProducerServices producerServices, IComputersGraphicsServices computersGraphicsServices)
        {
            _computerServices = computerServices;
            _graphicsServices = graphicsServices;
            _producerServices = producerServices;
            _computersGraphicsServices = computersGraphicsServices;
        }

        // GET: ComputerController
        public ActionResult Index()
        {
            var computers = _computerServices.GetAll();
            return View(computers);
        }
        // GET: ComputerController/Details/5
        public ActionResult Details(int id)
        {
            var computer = _computerServices.GetById(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }
        // GET: ComputerController/Create
        public ActionResult Create()
        {
            var graphics = _graphicsServices.GetAll();
            var producers = _producerServices.GetAll();

            var model = new ComputerVM();

            model.Computer = new Computer();
            model.Graphics = graphics;
            model.Producers = producers;

            return View(model);
        }

        // POST: ComputerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ComputerVM model)
        {
            if (ModelState.IsValid)
            {
                _computerServices.Create(model.Computer);

                if (model.GraphicsIds != null && model.GraphicsIds.Length > 0)
                {
                    foreach (var graphicsId in model.GraphicsIds)
                    {
                        await _computersGraphicsServices.Create(model.Computer.ComputerId, graphicsId);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: ComputerController/Edit/5
        public ActionResult Edit(int id)
        {
            var graphics = _graphicsServices.GetAll();
            var producers = _producerServices.GetAll();

            var model = new ComputerVM();

            model.Computer = _computerServices.GetById(id);
            model.Graphics = graphics;
            model.Producers = producers;

            if (model.Computer == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: ComputerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ComputerVM model)
        {
            if (ModelState.IsValid)
            {
                await _computersGraphicsServices.Delete(model.Computer.ComputerId);
                _computerServices.Update(model.Computer);

                if (model.GraphicsIds != null && model.GraphicsIds.Length > 0)
                {
                    foreach (var graphicsId in model.GraphicsIds)
                    {
                        await _computersGraphicsServices.Create(model.Computer.ComputerId, graphicsId);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ComputerController/Delete/5
        public ActionResult Delete(int id)
        {
            var computer = _computerServices.GetById(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: ComputerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _computerServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
