using AppWebMVC.Services;
using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppModel.Models;
using Microsoft.AspNetCore.Authorization;


namespace AppWebMVC.Areas.Admin.Controllers
{
    [Area(RolesTypes.Admin)]
    [Authorize(Roles = RolesTypes.Admin)]
    public class GraphicsController : Controller
    {
        private readonly IGraphicsServices _graphicsServices;
        public GraphicsController(IGraphicsServices graphicsServices)
        {
            _graphicsServices = graphicsServices;
        }
        // GET: GraphicsController
        public ActionResult Index()
        {
            var graphics = _graphicsServices.GetAll();
            return View(graphics);
        }

        // GET: GraphicsController/Details/5
        public ActionResult Details(int id)
        {
            var graphics = _graphicsServices.GetById(id);
            if (graphics == null)
            {
                return NotFound();
            }
            return View(graphics);
        }

        // GET: GraphicsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraphicsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Graphics graphics)
        {
            if (ModelState.IsValid)
            {
                _graphicsServices.Create(graphics);
                return RedirectToAction(nameof(Index));
            }

            return View(graphics);
        }

        // GET: GraphicsController/Edit/5
        public ActionResult Edit(int id)
        {
            var graphics = _graphicsServices.GetById(id);
            if (graphics == null)
            {
                return NotFound();
            }
            return View(graphics);
        }

        // POST: GraphicsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Graphics graphics)
        {
            if (id != graphics.GraphicsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _graphicsServices.Update(graphics);
                return RedirectToAction(nameof(Index));
            }
            return View(graphics);
        }

        // GET: GraphicsController/Delete/5
        public ActionResult Delete(int id)
        {
            var graphics = _graphicsServices.GetById(id);
            if (graphics == null)
            {
                return NotFound();
            }
            return View(graphics);
        }

        // POST: GraphicsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            {
                _graphicsServices.Delete(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
