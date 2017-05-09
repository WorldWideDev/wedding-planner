using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private MyContext _context;
        public WeddingController(MyContext context)
        {
            _context = context;
        }

        private User GetLoggedUser()
        {
            return _context.Users.SingleOrDefault( user => user.Id == (int)HttpContext.Session.GetInt32("id"));
        }


        // =============================================================================
        // ===================  DASHBBOARD =============================================
        // =============================================================================
        
        [HttpGetAttribute]
        [RouteAttribute("dashboard")]
        public IActionResult Index()
        {
            User loggedUser = GetLoggedUser();
            if(loggedUser == null)
                return RedirectToAction("Logout", "Home");

            ResponseForm r = new ResponseForm() 
            {
                UserId = loggedUser.Id
            };

            DashboardViewBundle dash = new DashboardViewBundle()
            {
                AllWeddings = _context.Weddings
                            .Include( wedding => wedding.Host )
                            .Include( wedding => wedding.EventLocation )
                            .Include( wedding => wedding.Responses )
                            .ToList(),
                LogUser = loggedUser,
                NewResponse = r
            };

            return View(dash);
        }

        // =============================================================================
        // ===================  SHOW WEDDING ===========================================
        // =============================================================================

        [HttpGetAttribute]
        [RouteAttribute("wedding/{id}")]
        public IActionResult ShowWedding(int id)
        {
            Wedding w = _context.Weddings
                .Include( wedding => wedding.EventLocation )
                .SingleOrDefault( wedding => wedding.Id == id );
            System.Console.WriteLine(w.EventLocation.City);
            string a = w.EventLocation.Street + " " +
                       w.EventLocation.City + " " +
                       w.EventLocation.State + " " +                       
                       w.EventLocation.Zip.ToString();
            List<Response> y = _context.Responses
                .Include( res => res.User)
                .Where( res => res.IsGoing == true)
                .Where( res => res.WeddingId == id).ToList();
            ShowWeddingBundle bundle = new ShowWeddingBundle()
            {
                TheWedding = w,
                TheAddress = a,
                Yeses = y
            };
            return View("Show", bundle);
        }

        // =============================================================================
        // ===================  CREATE RESPONSE ========================================
        // =============================================================================

        [HttpPostAttribute]
        [RouteAttribute("respond/new")]
        public IActionResult RSVP(ResponseForm data)
        {
            User loggedUser = GetLoggedUser();
            System.Console.WriteLine(data);
            Response r = new Response()
            {
                IsGoing = (data.Result == "true") ? true : false,
                UserId = data.UserId,
                WeddingId = data.WeddingId
            };
            _context.Responses.Add(r);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // =============================================================================
        // ===================  UPDATE RESPONSE ========================================
        // =============================================================================

        [HttpGetAttribute]
        [RouteAttribute("respond/update/{uId}/{wId}/{rId}")]
        public IActionResult UpdateRSVP(int uId, int wId, int rId)
        {
            Response r = _context.Responses.SingleOrDefault( res => res.Id == rId);
            r.IsGoing = !r.IsGoing;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGetAttribute]
        [RouteAttribute("wedding/new")]
        public IActionResult NewWedding()
        {
            User loggedUser = GetLoggedUser();
            if(loggedUser == null)
                return RedirectToAction("Logout", "Home");
            return View("Wedding");
        }

        // =============================================================================
        // ===================  CREATE WEDDING  ========================================
        // =============================================================================

        [HttpPostAttribute]
        [RouteAttribute("wedding/create")]
        public IActionResult Create(NewWeddingForm formData)
        {
            User loggedUser = GetLoggedUser();

            if(!ModelState.IsValid)
                return View("Wedding");

            Address loc = new Address()
            {
                Street = formData.Street,
                City = formData.City,
                State = formData.State,
                Zip = formData.Zip
            };

            Wedding w = new Wedding()
            {
                WedderOne = formData.WedderOne,
                WedderTwo = formData.WedderTwo,
                Date = formData.Date,
                EventLocation = loc,
                Host = loggedUser
            };

            loc.Wedding = w;

            _context.Weddings.Add(w);
            _context.Addresses.Add(loc);
            _context.SaveChanges();

            return RedirectToAction("NewWedding");
        }

        // =============================================================================
        // ===================  DELETE WEDDING  ========================================
        // =============================================================================
        [HttpGetAttribute]
        [RouteAttribute("wedding/delete/{wedId}")]
        public IActionResult DeleteWedding(int wedId)
        {
            User logUser = GetLoggedUser();
            Wedding weddingToDelete = _context.Weddings
                .Where( wed => wed.Id == wedId )
                .Include( wed => wed.Host )
                .SingleOrDefault();
            if(logUser == null || logUser.Id != weddingToDelete.Host.Id)
            {
                return RedirectToAction("Logout", "Home");
            }

            _context.Weddings.Remove(weddingToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}