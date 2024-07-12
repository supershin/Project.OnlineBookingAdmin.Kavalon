
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Project.Booking.Admin.Business.Extensions;

namespace Project.Booking.Admin.Business.Services
{
    public class UserService : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OnlineBookingDbContext _context;
        private UserProfile _userProfile;
        
        public UserService(OnlineBookingDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userProfile = _httpContextAccessor.HttpContext.User.GetUserProfile();
        }

        public UserService(OnlineBookingDbContext context)
        {
            _context = context;            
        }
        
        public IEnumerable<UserProfile> GetUserAll() {
            var query = from u in _context.tm_User.Where(e => e.FlagActive == true)
                                    .Include(e=>e.Department)
                                    .Include(e => e.Role)
                                    .Include(e => e.UpdateByNavigation)
                        select new { u };
            return query.AsEnumerable().Select(e => new UserProfile
            {
                ID = e.u.ID,
                FirstName = e.u.FirstName,
                LastName=e.u.LastName,
                DepartmentName = e.u.Department.Name,
                RoleName = e.u.Role.Name,
                Email = e.u.Email,
                Mobile = e.u.Mobile,
                UpdateDate = e.u.UpdateDate,
                UpdateByName = e.u.UpdateByNavigation.FirstName
            }).OrderByDescending(e => e.UpdateDate).ToList();
        }
        public UserProfile GetUserByID(Guid ID) {
            var query = from u in _context.tm_User.Where(e => e.FlagActive == true && e.ID == ID)
                                    .Include(e=>e.Department)
                                    .Include(e => e.Role)
                        select new { u };
            return query.AsEnumerable().Select(e => new UserProfile
            {
                ID= e.u.ID,
                FirstName = e.u.FirstName,
                LastName = e.u.LastName,
                DepartmentID = e.u.DepartmentID,
                DepartmentName = e.u.Department.Name,               
                RoleID = e.u.RoleID,                
                RoleName = e.u.Role.Name,
                Mobile = e.u.Mobile,
                Username =e.u.Username
            }).SingleOrDefault();                        
        }
        public void SaveUser(UserProfile model)
        {
            using (var scope = new TransactionScope())
            {
                SaveUserData(model);
                scope.Complete();
            }
        }
        private void SaveUserData(UserProfile model)
        {
            var user = _context.tm_User.SingleOrDefault(e => e.ID == model.ID && e.FlagActive == true);
            verifyUsername(model.ID,model.Username);
            if (user == null)
            {
                var item = SetUserData(new tm_User(), model);
                _context.Add(item);

            }
            else
            {
                var item = SetUserData(user, model);
                _context.Update(item);
            }
            _context.SaveChanges();
        }
        private void verifyUsername(Guid userID,string username) {
            if (_context.tm_User.Any(e=>e.Username == username && e.ID != userID))
            {
                throw new Exception(Constant.Message.Error.USERNAME_HAS_ALREADY);
            }
        }
        private tm_User SetUserData(tm_User item, UserProfile model)
        {
            if (item.ID == Guid.Empty)
            {
                item.ID = Guid.NewGuid();
                item.FlagActive = true;
                item.CreateDate = DateTime.Now;
                item.CreateBy = _userProfile.ID;
            }
            item.FirstName = model.FirstName.ToStringEmpty();
            item.LastName = model.LastName.ToStringEmpty();

            if (!model.isUserProfile)
            {
                item.DepartmentID = model.DepartmentID;
                item.RoleID = model.RoleID;
            }
           
            item.Email = model.Username.ToStringEmpty();
            item.Mobile = model.Mobile.ToStringEmpty();
            item.Username = model.Username.ToStringEmpty();
            if (!string.IsNullOrEmpty(model.Password))
            {
                item.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }           
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = _userProfile.ID;
            return item;
        }             
        public UserProfile VerifyLogin(string username, string password)
        {
            var user = GetUserProfile(username);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))                
                    return user;
                else throw new Exception(Constant.Message.Error.INVALID_LOGIN);
            }
            else 
                throw new Exception(Constant.Message.Error.INVALID_LOGIN);
        }
        public UserProfile GetUserProfile(string username)
        {
            var user = _context.tm_User.Where(e => e.FlagActive == true && e.Username == username)
                                           .Include(dep => dep.Department)
                                           .Include(rol => rol.Role).SingleOrDefault();
            if (user != null)
            {
                return new UserProfile()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DepartmentID = user.Department.ID,
                    DepartmentName = user.Department.Name,
                    RoleID = user.Role.ID,
                    RoleName = user.Role.Name,
                    RoleLevel = user.Role.Level.AsInt(),
                    Username = user.Username ,
                    Password = user.Password
                };
            }
            else
                throw new Exception(Constant.Message.Error.INVALID_LOGIN);
        }
        

    }
}
