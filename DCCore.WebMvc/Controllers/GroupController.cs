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
using DCCore.WebMvc.Utils;
using static DCCore.WebMvc.Utils.ExchangeEmailService;
using DCCore.WebMvc.Stores;
using DCCore.Domain.Entities;

namespace DCCore.WebMvc.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupStore _groupManager;

        public GroupController(GroupStore groupManager)
        {
            _groupManager = groupManager;
        }

        // GET: Group
        public ActionResult Index()
        {
            var Groups =_groupManager.Groups;
            return View(Groups);
        }
        /// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
        {
            var Group = new Group();
            return View(Group);
        }
        /// <summary>
        /// Create a New Group
        /// </summary>
        /// <param name="Group"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Group group)
        {
           //obtengo el User.Identity de .Net
            var user = User.Identity;
            //creo nuestro objeto IdentityUser apartir del de .Net
            var userIdentityOwn = new IdentityUser()
            {
                UserName = user.Name,
                Id =getGuid(user.GetUserId())
            };

            //en este metodo creo el grupo y lo asigno al usuario
            _groupManager.CreateGroup(group, userIdentityOwn);
            return RedirectToAction("Index");
        }
        private Guid getGuid(string value)
        {
            var result = default(Guid);
            Guid.TryParse(value, out result);
            return result;
        }
    }
}