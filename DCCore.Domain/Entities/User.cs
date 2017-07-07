using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.Domain.Entities
{
    public class User
    {
        #region Fields
        private ICollection<Claim> _claims;
        private ICollection<ExternalLogin> _externalLogins;
        private ICollection<Role> _roles;
        private ICollection<Group> _groups;
        private ICollection<Mail> _mails;
        #endregion

        #region Scalar Properties
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }

        #endregion

        #region Navigation Properties
        public virtual ICollection<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        public virtual ICollection<ExternalLogin> Logins
        {
            get { return _externalLogins ?? (_externalLogins = new List<ExternalLogin>()); }
            set { _externalLogins = value; }
        }

        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
        public virtual ICollection<Group> Groups
        {
            get { return _groups ?? (_groups = new List<Group>()); }
            set { _groups = value; }
        }
        public virtual ICollection<Mail> Mails
        {
            get { return _mails ?? (_mails = new List<Mail>()); }
            set { _mails = value; }
        }

        #endregion

        #region constructors
        public User() { }
        #endregion
    }
}
