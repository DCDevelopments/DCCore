using DCCore.Domain.Entities;
using DCCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.Domain.Entities
{
    public class Mail
    {
      

        private User _user;

        #region Fields
        private ICollection<Group> _groups;
        #endregion

        #region Scalar Properties
        public virtual Guid MailId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual int MailState { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _user = value;
                UserId = value.UserId;
            }
        }
        public ICollection<Group> Groups
        {
            get { return _groups ?? (_groups = new List<Group>()); }
            set { _groups = value; }
        }
        #endregion

        #region constructors
        public Mail() { }
        #endregion
    }
}
