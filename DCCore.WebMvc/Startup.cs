using DCCore.Domain;
using DCCore.Domain.Entities;
using DCCore.EntityFramework;
using DCCore.WebMvc.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DCCore.WebMvc.Startup))]
namespace DCCore.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          ConfigureAuth(app);
          createRolesUserAsync();
        }
        private  void createRolesUserAsync()
        {

            UnitOfWork _unitOfWork = new UnitOfWork("Development");
            UserStore userStore = new UserStore(_unitOfWork);
            RoleStore roleStore = new RoleStore(_unitOfWork);


            var existAdmin = userStore.FindByNameAsync("admin@admin.com").Result;

            if (existAdmin == null)
            {
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                roleStore.Create(roleAdmin);

                var roleUsuario = new IdentityRole();
                roleUsuario.Name = "Usuario";
                roleStore.Create(roleUsuario);

                var userAdmin = new IdentityUser();
                userAdmin.UserName = "admin@admin.com";
                userAdmin.PasswordHash = "AD3JTWDbk/9jMgSnP0OjwvwyhYrIbE3G1VyylFXYhyf9quu1l06Om2gsyNBYh86J6A==";
                userAdmin.SecurityStamp = "1baa6902-064e-4fcf-a006-46ddd6596fa5";
                userStore.Create(userAdmin);
                userStore.AddToRole(userAdmin, "Admin");

            }

        }

    }
}
