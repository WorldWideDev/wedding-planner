using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        private static string getSalt()
        {
            byte[] bytes = new byte[128/8];
            using(var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static string getHash(string toHash)
        {
            // SHA512 is disposable by inheritance.  
            using(var sha256 = SHA256.Create())  
            {  
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHash));  
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
            }  
        }


        private static string GenerateHash(string plainText, string salt)
        {
            return getHash(plainText + salt);
        }

        private HomeViewBundle InitializeHomeBundle()
        {
            NewUserForm _regForm = new NewUserForm();
            LoginUserForm _logForm = new LoginUserForm();
            return new HomeViewBundle(){
                RegisterUser = _regForm,
                LoginUser = _logForm
            };
        }

        private bool UserEmailExists(string controlEmail)
        {
            User testUser = _context.Users.SingleOrDefault( u => u.Email == controlEmail);
            return (testUser == null) ? false : true;
        }

        // using email here might not be the most secure, could use salt or hashed pw?
        private void LogUserIntoSession(string userEmail)
        {
            User user = _context.Users.SingleOrDefault( u => u.Email == userEmail);
            HttpContext.Session.SetInt32("id", user.Id);
            System.Console.WriteLine($"{user.FirstName} is logged in");
        }



        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(InitializeHomeBundle());
        }

        [HttpGetAttribute]
        [RouteAttribute("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPostAttribute]
        [RouteAttribute("user/create")]
        public IActionResult Register(NewUserForm regUser){
            System.Console.WriteLine("IM REGISTERING???");
            // check if email exists im db
            
            // NOTE:
            //     this should really exist as a custom validation in MyContext
            //     ideally as a override method of dbContext

            if(UserEmailExists(regUser.Email))
                ModelState.AddModelError("Email", "Email already in system");

            if(!ModelState.IsValid)
                return View("Index", InitializeHomeBundle());

            // create user
            string _salt = getSalt();
            string _pw = GenerateHash(regUser.Password, _salt);
            User newUser = new User() {
                FirstName = regUser.FirstName,
                LastName = regUser.LastName,
                Email = regUser.Email,
                Password = _pw,
                Salt = _salt,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Add(newUser);
            _context.SaveChanges();
           
            // add id to session (possibly seperate method)
            LogUserIntoSession(newUser.Email);
            // redirect to dashboard
            return RedirectToAction("Index", "Wedding");
        }

        [HttpPostAttribute]
        [RouteAttribute("user/login")]
        public IActionResult Login(LoginUserForm logUser)
        {
            // get user from email
            User user = _context.Users.SingleOrDefault( u => u.Email == logUser.EmailLog);
            if(user == null){
                ModelState.AddModelError("EmailLog", "No such email exists");
                return View("Index", InitializeHomeBundle());                
            }

            // compare hashed pws with salt from db
            if(user.Password != GenerateHash(logUser.PasswordLog, user.Salt))
                ModelState.AddModelError("PasswordLog", "Invalid Password");
                
            if(!ModelState.IsValid)
                return View("Index", InitializeHomeBundle());
            
            // log user in to session
            LogUserIntoSession(user.Email);
            // redirect to dashboard
            return RedirectToAction("Index", "Wedding");
        }
    }
}
