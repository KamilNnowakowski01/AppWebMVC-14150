using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AppModel.Models;
using Microsoft.AspNetCore.Authorization;


namespace AppWebMVC.Areas.User.Controllers
{
    [Area(RolesTypes.User)]
    [Authorize(Roles = RolesTypes.User + "," + RolesTypes.Admin)]
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
    }
}
