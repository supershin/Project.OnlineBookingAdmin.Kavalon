using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Booking.Admin.Business.Interfaces;
using Project.Booking.Admin.Data.Models;
using Project.Booking.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Business.Services
{

    public class ReportService : IReport
    {
        private readonly OnlineBookingDbContext _context;

        public ReportService(OnlineBookingDbContext context)
        {
            _context = context;
        }
        public DataTable GetUnitData(ReportViewModel criteria)
        {
            
            SqlParameter[] parameters =
                {
                  new SqlParameter("@projectID", SqlDbType.UniqueIdentifier) { Value = criteria.ProjectID} ,
                  new SqlParameter("@unitStatusIDStr", SqlDbType.VarChar) { Value = criteria.UnitStatusIDs},
                  new SqlParameter("@startDate", SqlDbType.DateTime) { Value = criteria.StartDate},
                  new SqlParameter("@endDate", SqlDbType.DateTime) { Value = criteria.EndDate}
                  };
            DataTable dt = ExecuteDataTable(_context.Database.GetConnectionString(), "sp_Report_GetUnit", parameters);
            return dt;
        }
        public DataTable GetBookingPaymentData(ReportViewModel criteria)
        {

            SqlParameter[] parameters =
                {
                  new SqlParameter("@projectID", SqlDbType.UniqueIdentifier) { Value = criteria.ProjectID} ,                  
                  new SqlParameter("@startDate", SqlDbType.DateTime) { Value = criteria.StartDate},
                  new SqlParameter("@endDate", SqlDbType.DateTime) { Value = criteria.EndDate}
                  };
            DataTable dt = ExecuteDataTable(_context.Database.GetConnectionString(), "sp_Report_GetBookingPayment", parameters);
            return dt;
        }
        public DataTable GetRegisterData(ReportViewModel criteria)
        {

            SqlParameter[] parameters =
                {                  
                  new SqlParameter("@startDate", SqlDbType.DateTime) { Value = criteria.StartDate},
                  new SqlParameter("@endDate", SqlDbType.DateTime) { Value = criteria.EndDate}
                  };
            DataTable dt = ExecuteDataTable(_context.Database.GetConnectionString(), "sp_Report_GetRegister", parameters);
            return dt;
        }
        public DataTable GetTransferPaymentData(ReportViewModel criteria)
        {

            SqlParameter[] parameters =
                {
                  new SqlParameter("@projectID", SqlDbType.UniqueIdentifier) { Value = criteria.ProjectID} ,
                  new SqlParameter("@startDate", SqlDbType.DateTime) { Value = criteria.StartDate},
                  new SqlParameter("@endDate", SqlDbType.DateTime) { Value = criteria.EndDate}
                  };
            DataTable dt = ExecuteDataTable(_context.Database.GetConnectionString(), "sp_Report_GetTransferPayment", parameters);
            return dt;
        }
        private DataTable ExecuteDataTable(string connectionString, string storedProcedureName, params SqlParameter[] arrParam)
        {

            DataTable dt = new DataTable();
            // Open the connection  
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                // Define the command 
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandTimeout = 3600;
                    // Handle the parameters 
                    if (arrParam != null)
                    {
                        foreach (SqlParameter param in arrParam)
                            cmd.Parameters.Add(param);
                    }

                    // Define the data adapter and fill the dataset 
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
