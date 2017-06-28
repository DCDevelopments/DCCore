using DCCore.Domain.Entities;
using DCCore.EntityFramework;
using DCCore.WebMvc.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCManagerUsers.WebMvc.Controllers
{
    [Authorize (Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleStore _roleManager;

        public RoleController(RoleStore roleManager)
        {
            _roleManager = roleManager;
        }


        // GET: Role
        public ActionResult Index()
        {
            var Roles = _roleManager.Roles;
            return View(Roles);
        }
        /// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            _roleManager.Create(role);
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