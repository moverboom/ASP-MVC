using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInviteOpdracht.Models;
using Ninject;

namespace WebInviteOpdracht.Controllers
{
    public class HomeController : Controller {
        private IRepository repository;

        //Controller injection
        public HomeController(IRepository aRepository) {
            repository = aRepository;
        }

        // GET: Home
        public ViewResult Index() {
            //Default view
            ViewBag.Responses = repository.GetAllResponses().Count();
            return View();
        }

        //Action method to page which shows responses
        //View uses the IRepository
        public ViewResult Responses() {
            if (repository.GetAllResponses().Count() > 0) {
                ViewResult result = View(repository.GetAllResponses().ToArray());
                result.ViewName = "Responses";
                return result;
            } else {
                return View("NoResponses");
            }
        }

        [HttpGet]
        public ViewResult RsvpForm() {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse) {
            if (ModelState.IsValid) {
                //TODO: Email response to the party host
                //Result of AddResponse() is stored in a Viewbag
                ViewBag.Added = repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            } else {
                //validation error
                return View();
            }
        }

        
        public ViewResult Editdata(GuestResponse guestResponse) { 
            if (ModelState.IsValid) {
                ViewResult result = View(repository.GetResponse(guestResponse.Email));
                result.ViewName = "EditData";
                return result;
            } else {
                return View();
            }
        }

        
        public ViewResult SaveData(GuestResponse guestResponse) {
            if (ModelState.IsValid) {
                if (repository.EditResponse(guestResponse)) {
                    ViewBag.EditSuccess = true;
                    return View("EditData", guestResponse);
                } else {
                    ViewBag.EditSuccess = false;
                    return View("EditData", guestResponse);
                }
            } else {
                return View("EditData", guestResponse);
            }
        }
    }
}