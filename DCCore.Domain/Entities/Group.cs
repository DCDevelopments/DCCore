using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.Domain.Entities
{
    public class Group
    {
        #region Fields
        private ICollection<User> _users;
        #endregion

        #region Scalar Properties
        public Guid GroupId { get; set; }
        public string Name { get; set; }        
        #endregion

        #region Navigation Properties
        public ICollection<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            set { _users = value; }
        }
        #endregion

        #region constructors
        public Group() { }
        #endregion
    }
}
