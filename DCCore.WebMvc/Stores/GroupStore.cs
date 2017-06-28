using DCCore.Domain;
using DCCore.Domain.Entities;
using DCCore.WebMvc.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DCCore.WebMvc.Stores
{
    public class GroupStore
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Group> Groups
        {
            get
            {
                return _unitOfWork.GroupRepository
                    .GetAll()                    
                    .AsQueryable();
            }
        }

        public int AddToGroup(string userid, string groupid)
        {
            if (userid == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(groupid))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _unitOfWork.UserRepository.FindById(userid);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            var r = _unitOfWork.GroupRepository.FindById(groupid);
            if (r == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

            u.Groups.Add(r);
            _unitOfWork.UserRepository.Update(u);

            return _unitOfWork.SaveChanges();
        }

        public int CreateGroup(Group group, IdentityUser user)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            var g = getGroup(group);
            _unitOfWork.GroupRepository.Add(g);

            _unitOfWork.SaveChanges();

            var u = _unitOfWork.UserRepository.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("No existe el usuario", "user");
           
            var r = _unitOfWork.GroupRepository.FindById(g.GroupId);
            if (r == null)
                throw new ArgumentException("El id de grupo no corresponde a una entidad de Group", "groupName");
                                                   
             u.Groups.Add(r);
            _unitOfWork.UserRepository.Update(u);

            return _unitOfWork.SaveChanges();
        }


        public Task AddToGroupAsync(string userid, string groupid)
        {
            if (userid == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(groupid))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _unitOfWork.UserRepository.FindById(userid);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            var r = _unitOfWork.GroupRepository.FindById(groupid);
            if (r == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");
            
            u.Groups.Add(r);
            _unitOfWork.UserRepository.Update(u);

            return _unitOfWork.SaveChangesAsync();
        }
        public Task RemoveFromGroupAsync(IdentityUser user, Guid groupid)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (groupid==null)
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _unitOfWork.UserRepository.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var g = u.Groups.FirstOrDefault(x => x.GroupId == groupid);
            u.Groups.Remove(g);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        public int RemoveFromGroup(IdentityUser user, Guid groupid)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (groupid == null)
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _unitOfWork.UserRepository.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var g = u.Groups.FirstOrDefault(x => x.GroupId == groupid);
            u.Groups.Remove(g);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChanges();
        }

        #region Private Methods
        private Group getGroup(Group group)
        {
            if (group == null)
                return null;
            return new Group
            {
                GroupId = group.GroupId,
                Name = group.Name
            };
        }
        #endregion
    }
}