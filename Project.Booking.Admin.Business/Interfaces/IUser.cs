using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IUser
    {
        IEnumerable<UserProfile> GetUserAll();
        UserProfile GetUserByID(Guid ID);
        UserProfile VerifyLogin(string username, string password);
        UserProfile GetUserProfile(string username);
        void SaveUser(UserProfile model);
    }
}
