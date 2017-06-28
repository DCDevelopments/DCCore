using DCCore.Domain;
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

        public Task<IList<string>> GetGroupsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.UserRepository.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<string>>(u.Groups.Select(x => x.Name).ToList());
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
    }
}