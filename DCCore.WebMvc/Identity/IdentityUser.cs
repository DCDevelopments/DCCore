using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCCore.WebMvc.Identity
{
    public class IdentityUser : IUser<Guid>
    {

        public Guid Id { get; set; }

        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }
        

        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }

    }
}