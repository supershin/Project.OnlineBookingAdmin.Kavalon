using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Interfaces
{
    public interface IRegister
    {        
        IEnumerable<RegisterViewModel> GetRegisterAll();
        RegisterViewModel GetRegisterByID(Guid id);
        void SaveRegister(RegisterViewModel model);
        ProjectRegisterQuota GetProjectQuotaByID(int ID);
        void SaveProjectQuota(ProjectRegisterQuota model);
        void SaveProjectQuotaData(ProjectRegisterQuota model);
    }
}
