using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace AppWebMVC.Areas.User.Controllers
{
    [Area(RolesTypes.User)]
    [Authorize(Roles = RolesTypes.User + "," + RolesTypes.Admin)]
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
    }
}
