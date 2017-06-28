using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DCCore.WebMvc.Models;
using DCCore.WebMvc.Identity;

namespace DCManagerUsers.WebMvc.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<IdentityUser, Guid> _userManager;

        public ManageController(UserManager<IdentityUser, Guid> userManager)
        {
            _userManager = userManager;
        }

        // GET: Manage/Index
        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
              
            };
            return View(model);
        }
        private bool HasPassword()
        {
            var user = _userManager.FindByNameAsync(User.Identity.GetUserName());
           if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}