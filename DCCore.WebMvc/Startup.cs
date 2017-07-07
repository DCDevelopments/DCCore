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


            var existAdmin = userStore.FindByNameAsync("admin").Result;

            if (existAdmin == null)
            {
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                roleStore.Create(roleAdmin);

                var roleUsuario = new IdentityRole();
                roleUsuario.Name = "Usuario";
                roleStore.Create(roleUsuario);

                var userAdmin = new IdentityUser();
                userAdmin.UserName = "admin";
                userAdmin.Email = "admin@admin.com";
                //12345678
                userAdmin.PasswordHash = "AKN/vFTx1ENubiEyo8C8sdZI3eMrcAFVApbyVoTHqRV5JW8qFbi1H9R78a9NOfSdxA==";
                userAdmin.SecurityStamp = "a8755046-a700-4291-96bf-b66a6db1f73b";
                userStore.Create(userAdmin);
                userStore.AddToRole(userAdmin, "Admin");

            }

        }

    }
}
