using AppModel.Models;
using AppWebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Areas.User.Controllers
{
    [Area(RolesTypes.User)]
    [Authorize(Roles = RolesTypes.User + "," + RolesTypes.Admin)]
    public class ComputerController : Controller
    {
        private readonly IComputerServices _computerServices;


        public ComputerController(IComputerServices computerServices)
        {
            _computerServices = computerServices;
        }

        public ActionResult Index()
        {
            var computers = _computerServices.GetAll();
            return View(computers);
        }
    }
}
