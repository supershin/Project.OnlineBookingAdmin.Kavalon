using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Booking.Admin.Business.Extensions;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Extensions;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Booking.Admin.Business.Services
{
    public class UnitService : IUnit
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<ApplicationSetting> _config;
        private readonly OnlineBookingDbContext _context;
        private readonly UserProfile _userProfile;
        public UnitService(OnlineBookingDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
            _userProfile = _httpContextAccessor.HttpContext.User.GetUserProfile();
        }
        public IEnumerable<UnitViewModel> GetUnitByProjectAll(Guid projectID)
        {
            var query = from u in _context.tm_Unit.Where(e => e.FlagActive == true && e.ProjectID == projectID)
                                          .Include(e => e.Build)
                                          .Include(e => e.Floor)
                                          .Include(e => e.ModelType)
                                          .Include(e => e.UnitStatus)
                                          .Include(e => e.UnitType)
                                          .Include(e => e.UserUpdateBy)
                        select new { u };
            return query.AsEnumerable().Select(e => new UnitViewModel
            {
                ID = e.u.ID,
                UnitCode = e.u.UnitCode,
                UnitTypeName = e.u.UnitType.Name,
                BuildName = e.u.Build.Name,
                FloorName = e.u.Floor.Name,
                ModelTypeName = e.u.ModelType.Name,
                Area = e.u.Area,
                SellingPrice = e.u.SellingPrice,
                Discount = e.u.Discount,
                SpecialPrice = e.u.SpecialPrice,
                BookingAmount = e.u.BookingAmount,
                UnitStatusID = e.u.UnitStatusID,
                UnitStatusName = e.u.UnitStatus.Name,
                UnitStatusColor = e.u.UnitStatus.Color,
                UserUpdateDate = e.u.UserUpdateDate,
                UserUpdateByName = (e.u.UserUpdateBy != null) ? e.u.UserUpdateBy.FirstName : null
            }).OrderByDescending(e => e.UserUpdateDate).ToList();

        }
        public UnitViewModel GetUnitByID(Guid unitID)
        {
            var query = from u in _context.tm_Unit.Where(e => e.FlagActive == true && e.ID == unitID)
                                                  .Include(e => e.Project)
                                                  .Include(e => e.UnitType)
                                                  .Include(e => e.Build)
                                                  .Include(e => e.Floor)
                                                  .Include(e => e.ModelType)
                                                  .Include(e => e.UnitStatus)
                        select new { u };
            return query.AsEnumerable().Select(e => new UnitViewModel
            {
                ID = e.u.ID,
                UnitID = e.u.ID,
                ProjectName = e.u.Project.ProjectNameTH,
                UnitCode = e.u.UnitCode,
                UnitTypeName = e.u.UnitType.Name,
                BuildName = e.u.Build.Name,
                FloorName = e.u.Floor.Name,
                ModelTypeName = e.u.ModelType.Name,
                Area = e.u.Area,
                AreaIncrease = e.u.AreaIncrease,
                SellingPrice = e.u.SellingPrice,
                Discount = e.u.Discount,
                SpecialPrice = e.u.SpecialPrice,
                BookingAmount = e.u.BookingAmount,
                UnitStatusID = e.u.UnitStatusID,
                UnitStatusName = e.u.UnitStatus.Name,
                UnitStatusColor = e.u.UnitStatus.Color
            }).SingleOrDefault();
        }

        public void SaveUnit(UnitViewModel model)
        {
            using (var scope = new TransactionScope())
            {
                saveUnitVIP(model.ID.AsGuid(),model.UnitStatusID.AsInt());
                SaveUnitData(model);
                
                //if unit status is change & chang to availabled
                if (model.isCancelBooking)
                    saveCancelUnitVIP(model.ID.AsGuid());

                SaveUnitBookingHistory(model.ID.AsGuid(), model.UnitStatusID.AsInt());
                scope.Complete();
            }
        }
        private void SaveUnitData(UnitViewModel model)
        {
            var item = _context.tm_Unit.Include(e => e.UserUpdateBy)
                .ThenInclude(r=>r.Role).SingleOrDefault(e => e.ID == model.ID);
            verifyUnitBooking(item, model);
            if (item != null)
            {
                //check if unit status is cancel booking 
                model.isCancelBooking = ((item.UnitStatusID == Constant.UnitStatus.BOOKING || item.UnitStatusID == Constant.UnitStatus.PAYMENT)
                                        && (model.UnitStatusID == Constant.UnitStatus.AVAILABLE || model.UnitStatusID == Constant.UnitStatus.CLOSE));

                item = SetUnitData(item, model);
                _context.Update(item);
                _context.SaveChanges();                
            }
        }
        private tm_Unit SetUnitData(tm_Unit item, UnitViewModel model)
        {
            item.UnitStatusID = model.UnitStatusID;
            item.UserUpdateDate = DateTime.Now;
            item.UserUpdateByID = _userProfile.ID;
            if (model.UnitStatusID == Constant.UnitStatus.CLOSE)
            {
                item.SellingPrice = model.SellingPrice;
                item.Discount = model.Discount;
                item.SpecialPrice = model.SpecialPrice;
                item.BookingAmount = model.BookingAmount;

            }
            return item;
        }
        private void verifyUnitBooking(tm_Unit item, UnitViewModel model)
        {
            //ตรวจสอบว่ามีรายการจองไหม
            if (_context.ts_Booking.Any(e => e.FlagActive == true && e.UnitID == model.ID
                    && e.BookingStatusID != Constant.BookingStatus.CANCEL))
                throw new Exception(Constant.Message.Error.INVALID_UNIT_HAS_BOOKING);

            verifyUserPermission(item);

        }
        private void verifyUserPermission(tm_Unit item)
        {
            if (_userProfile.DepartmentID != Constant.Department.SEG1
                    && _userProfile.DepartmentID != Constant.Department.SEG2
                    && _userProfile.DepartmentID != Constant.Department.SEG3
                    && _userProfile.DepartmentID != Constant.Department.SEG_TITLE
                    )
            {
                var msg = Constant.Message.Error.INVALID_UNIT_NOT_HAS_PERMISSION;
                throw new Exception(msg);
            }
            //ตรวจสอบสถานะว่าว่างไหม ถ้าไม่ว่างต้อง verify permission
            else if (item.UserUpdateByID != null && !item.UserUpdateByID.Equals(_userProfile.ID)
             && item.UnitStatusID != Constant.UnitStatus.AVAILABLE)
            {
                //check depart
                if (_userProfile.RoleLevel <= item.UserUpdateBy.Role.Level)                
                {
                    var msg = string.Format(Constant.Message.Error.INVALID_UNIT_HAS_PERMISSION, item.UserUpdateBy.FirstName);
                    throw new Exception(msg);
                }
            }
        }
        public void SaveUnitBookingHistory(Guid unitID, int unitStatusID, Guid? bookingID = null, int? bookingStatusID = null)
        {
            var item = new ts_Unitbooking_History()
            {
                UnitID = unitID,
                UnitStatusID = unitStatusID,
                BookingID = bookingID,
                BookingStatusID = bookingStatusID,
                CreateDate = DateTime.Now,
                CreateByID = _userProfile.ID
            };
            _context.Add(item);
            _context.SaveChanges();
        }

        public void UpdateUnitStatus(Guid unitID, int unitStatusID)
        {
            var unit = _context.tm_Unit.Single(e => e.ID == unitID && e.FlagActive == true);
            if (unit != null)
            {
                unit.UnitStatusID = unitStatusID;
                unit.UserUpdateByID = _userProfile.ID;
                unit.UserUpdateDate = DateTime.Now;
            }
        }

        public MatrixView GetUnitMatrix(Guid projectID, string builds)
        {
            var matrixView = new MatrixView();
            var project = _context.tm_Project.FirstOrDefault(e => e.ID == projectID && e.FlagActive == true);
            if (project == null)
                throw new Exception(Constant.Message.Error.PROJECT_DOES_NOT_EXISTS);
            matrixView.ProjectName = project.ProjectNameEN;

            var arrBuild = (!string.IsNullOrEmpty(builds.ToStringNullable())) ?
                                builds.Split(',').ToArray() : new string[] { };
            var query = from u in _context.tm_Unit.Where(e => e.ProjectID == project.ID && e.FlagActive == true)
                                   .Include(sta=>sta.UnitStatus)
                        join b in _context.tm_Build
                            on u.BuildID equals b.ID
                        join f in _context.tm_Floor
                            on u.FloorID equals f.ID
                        where (builds.Contains(b.Name) || builds == null)
                        select new { u, b, f };
            var data = query.AsEnumerable().Select(e => new UnitView
            {
                ID = e.u.ID,      
                ProjectID = (Guid)e.u.ProjectID,
                BuildID = (int)e.u.BuildID,
                Build = e.b.Name,
                Floor = e.f.ID,
                FloorName = e.f.Name,
                Room = e.u.Room.AsInt(),
                UnitCode = e.u.UnitCode,
                UnitStatusID = e.u.UnitStatusID.AsInt(),
                UnitStatusColor = e.u.UnitStatus.Color,
                UnitStatusName = e.u.UnitStatus.Name,
            }).OrderBy(e => e.Build).ThenByDescending(e => e.Floor).ThenBy(e => e.Room).ToList();

            foreach (var buildName in data.Select(e => e.Build).Distinct().OrderBy(e => e))
            {
                var buildID = data.Where(e => e.Build == buildName).FirstOrDefault().BuildID;

                var buildView = new BuildView();
                var buildItem = data.Where(e => e.Build == buildName).ToList();
                buildView.FloorMax = buildItem.Max(e => e.Floor);
                buildView.FloorMin = buildItem.Min(e => e.Floor);
                buildView.RoomMax = buildItem.Max(e => e.Room);
                buildView.RoomMin = buildItem.Min(e => e.Room);
                buildView.MatrixConfigs = getMatrixConfig(projectID, buildID);
                for (int floor = buildView.FloorMax; floor >= buildView.FloorMin; floor--)
                {
                    var floorView = new FloorView();
                    var floorItem = buildItem.Where(e => e.Floor == floor).ToList();
                    for (int room = buildView.RoomMin; room <= buildView.RoomMax; room++)
                    {
                        var unit = floorItem.Where(e => e.Room == room).FirstOrDefault() ?? new UnitView();
                        floorView.FloorName = floor;                        
                        floorView.UnitList.Add(unit);
                    }
                    buildView.BuildName = buildName;
                    buildView.FloorList.Add(floorView);
                }
                matrixView.BuildList.Add(buildView);
            }
            return matrixView;
        }
        public string GetBuilds(Guid projectID) {
            var builds =  _context.tm_Unit.Where(e => e.FlagActive == true && e.ProjectID == projectID)
                    .Select(e => e.Build.Name).OrderBy(e => e).Distinct();
            return string.Join(",", builds);
        }
        public List<MatrixConfig> getMatrixConfig(Guid projectID, int buildID)
        {
            return _context.tr_Matrix_Config.Where(e => e.ProjectID == projectID && e.BuildID == buildID && e.FlagActive == true)
                        .AsEnumerable().Select(e => new MatrixConfig
                        {
                            ID = e.ID,
                            ProjectID = e.ProjectID,
                            BuildID = e.BuildID,
                            Text = e.Text,
                            RowNo = e.RowNo,
                            ColSpan = e.ColSpan,
                            LineOrder = e.LineOrder,
                            Style = e.Style
                        }).OrderBy(e => e.RowNo).ThenBy(e => e.LineOrder).ToList();
        }

        private void saveCancelUnitVIP(Guid unitID) {
            var item = _context.tr_UnitVIP.Where(e => e.UnitID == unitID && e.FlagActive == true).FirstOrDefault();
            if (item != null)
            {
                item.FlagActive = false;
                item.UpdateDate = DateTime.Now;
                item.UpdateBy = _userProfile.ID;
                _context.Update(item);
                _context.SaveChanges();
            }
        }
        private void saveUnitVIP(Guid unitID,int unitStatusID) {
            var unit = _context.tm_Unit.Where(e => e.ID == unitID && e.FlagActive == true).FirstOrDefault();            
            
            //if available to booking is run statement
            if (unit.UnitStatusID == Constant.UnitStatus.AVAILABLE 
                    && ( unitStatusID == Constant.UnitStatus.BOOKING 
                        || unitStatusID == Constant.UnitStatus.PAYMENT)) {
                var item = _context.tr_UnitVIP.Where(e => e.UnitID == unitID && e.FlagActive == true).FirstOrDefault();
                if (item != null)
                {
                    unit.UnitStatusID = unitStatusID;
                    unit.UserUpdateDate = DateTime.Now;
                    unit.UserUpdateByID = new Guid(_config.Value.AdminTitleID);
                    _context.Update(unit);
                    _context.SaveChanges();
                }
            }            
        }
    }
}
