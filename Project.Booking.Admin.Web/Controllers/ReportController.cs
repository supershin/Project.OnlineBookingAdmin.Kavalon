using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Configurations;
using Project.Booking.Admin.Constants;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReport _servicereport;
        private readonly IWebHostEnvironment _hosting;
        public ReportController(
            IMaster serviceMaster,
            IReport servicereport,
            IWebHostEnvironment hosting,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ApplicationSetting> config) : base(serviceMaster, config, httpContextAccessor)
        {
            _servicereport = servicereport;
            _hosting = hosting;
        }

        #region Unit
        public IActionResult Index()
        {
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectPermissionSelectList = projects;
            ViewBag.UnitStatusSelectList = GetMasterUnitStatusLists();
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID) ?? projects.FirstOrDefault().Value;
            return View();
        }
        public IActionResult DownloadFileTempExcel(string fileGuid, string fileName)
        {
            var path = $"{_hosting.ContentRootPath}\\TempFile\\{fileGuid}.xlsx";
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                return PhysicalFile(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
        [HttpPost]
        public JsonResult GetUnitData(ReportViewModel criteria)
        {
            try
            {
                validateProject(criteria);
                var data = _servicereport.GetUnitData(criteria);

                string filename = string.Format("{0}_{1}.{2}", "Report_Unit_", DateTime.Now.ToString("yyyy-MM-dd"), "xlsx");
                var workbook = WriteExcelWithNPOI(data, "xlsx", filename);
                string handle = FileTempExcel(workbook);

                return Json(new
                {
                    success = true,
                    fileGuid = handle,
                    fileName = filename
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = InnerException(ex)
                });
            }
        }
        private void validateProject(ReportViewModel criteria) {
            if (criteria.ProjectID == Guid.Empty)
                throw new Exception(Constant.Message.Error.PLEASE_INPUT_DATA);
        }
        #endregion

        #region Booking & Payment
        public IActionResult BookingPayment()
        {
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectPermissionSelectList = projects;            
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID) ?? projects.FirstOrDefault().Value;
            return View();
        }        
        [HttpPost]
        public JsonResult GetBookingPaymentData(ReportViewModel criteria)
        {
            try
            {
                validateProject(criteria);
                var data = _servicereport.GetBookingPaymentData(criteria);

                string filename = string.Format("{0}_{1}.{2}", "Report_BookingPayment_", DateTime.Now.ToString("yyyy-MM-dd"), "xlsx");
                var workbook = WriteExcelWithNPOI(data, "xlsx", filename);
                string handle = FileTempExcel(workbook);

                return Json(new
                {
                    success = true,
                    fileGuid = handle,
                    fileName = filename
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = InnerException(ex)
                });
            }
        }
        #endregion

        #region Register
        public IActionResult Register()
        {          
            return View();
        }
        [HttpPost]
        public JsonResult GetRegisterData(ReportViewModel criteria)
        {
            try
            {
                var data = _servicereport.GetRegisterData(criteria);

                string filename = string.Format("{0}_{1}.{2}", "Report_Register_", DateTime.Now.ToString("yyyy-MM-dd"), "xlsx");
                var workbook = WriteExcelWithNPOI(data, "xlsx", filename);
                string handle = FileTempExcel(workbook);

                return Json(new
                {
                    success = true,
                    fileGuid = handle,
                    fileName = filename
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = InnerException(ex)
                });
            }
        }
        #endregion

        #region Transfer Payment
        public IActionResult TransferPayment()
        {
            var projects = GetMasterProjectLists(false);
            ViewBag.ProjectPermissionSelectList = projects;
            ViewBag.SelectProjectID = GetCookie(CookieKey.General.ProjectID) ?? projects.FirstOrDefault().Value;
            return View();
        }
        [HttpPost]
        public JsonResult GetTransferPaymentData(ReportViewModel criteria)
        {
            try
            {
                validateProject(criteria);
                var data = _servicereport.GetTransferPaymentData(criteria);

                string filename = string.Format("{0}_{1}.{2}", "Report_TransferPayment_", DateTime.Now.ToString("yyyy-MM-dd"), "xlsx");
                var workbook = WriteExcelWithNPOI(data, "xlsx", filename);
                string handle = FileTempExcel(workbook);

                return Json(new
                {
                    success = true,
                    fileGuid = handle,
                    fileName = filename
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = InnerException(ex)
                });
            }
        }
        #endregion

        private string FileTempExcel(IWorkbook workbook)
        {
            string handle = Guid.NewGuid().ToString();
            var path = _hosting.ContentRootPath;
            path += "\\TempFile\\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var fs = new FileStream($"{path}{handle}.xlsx", FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
            return handle;
        }
        private IWorkbook WriteExcelWithNPOI(DataTable dt, String extension, string fileName)
        {

            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet 1");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();

                    double result;
                    if (double.TryParse(dt.Rows[i][columnName].ToString(), out result))
                    {
                        cell.SetCellValue(result);
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }

                }
            }

            return workbook;
        }
    }
}
