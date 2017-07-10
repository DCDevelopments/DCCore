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
using System.Collections.Generic;

namespace DCCore.WebMvc.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupStore _groupManager;

        public GroupController(GroupStore groupManager)
        {
            _groupManager = groupManager;
        }

        // GET: Group/Index
        public ActionResult Index()
        {
            //obtengo el User.Identity de .Net
            var userIdentity = User.Identity;
            //creo nuestro objeto IdentityUser apartir del de .Net
            var user= new User()
            {
                UserName = userIdentity.Name,
                UserId = getGuid(userIdentity.GetUserId())
            };
            var Groups =_groupManager.Groups(user);
            return View(Groups);
        }

        // GET: Group/Add Users In Group by invitation in Email
        public ActionResult MailInvitationUsersInGroup()
        {
            //obtengo el User.Identity de .Net
            var userIdentity = User.Identity;
            //creo nuestro objeto IdentityUser apartir del de .Net
            var user = new User()
            {
                UserName = userIdentity.Name,
                UserId = getGuid(userIdentity.GetUserId())
            };
            var Groups = _groupManager.Users(user);
            return View(Groups);
        }

        [HttpPost]
        // POST: Group/Add Users In Group by invitation in Email
        public async Task<ActionResult> MailInvitationUsersInGroup(List<GroupCheckBoxListUserViewModel> list)
        {
            ExchangeEmailService serviceMail = new ExchangeEmailService();
            string body;

            foreach (var group in list)
            {
                if (group.IsSelected)
                {
                    body = string.Format(
                        "Dear {0} < BR /> Thank you for your registration, please click on the" +
                        "below link to complete your registration: < a href =\"{1}\" " +
                        "title =\"User Email Confirm\">{1}</a>",
                        group.UserName, Url.Action("ConfirmAddGroup", "Group",
                        new { Token = group.UserId, Email = group.Email }, Request.Url.Scheme));

                    await serviceMail.SendEmailAsync(group.Email, "Confirm Group", body);
                }
            }

            return View(list);
        }

        [AllowAnonymous]
        public ActionResult ConfirmAddGroup(string Token, string Email)
        { 
                return RedirectToAction("Index", "Home");               
        }


        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email; return View();
        }

        /// <summary>
        /// Create  a New Group
        /// </summary>
        /// <returns></returns>
        /// GET: Group/Create
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
        /// POST: Group/Create
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
        /// <summary>
        /// Method common in controllers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Guid getGuid(string value)
        {
            var result = default(Guid);
            Guid.TryParse(value, out result);
            return result;
        }
    }
}