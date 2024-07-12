using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Constants
{
    public static class Constant
    {
        
        public static class Message
        {
            public static class Error
            {
                public static readonly string PLEASE_INPUT_DATA = "Please entry require data.";
                public static readonly string PLEASE_SELECT_PROJECT = "โปรดเลือกโครงการ";
                public static readonly string PASSWORD_CONFIRM_PASSWORD_NOT_EQUAL = "password & confirm password invalid";
                public static readonly string INVALID_LOGIN = "Email or password invalid";
                public static readonly string USERNAME_HAS_ALREADY = "username นี้มีในระบบแล้ว";
                public static readonly string SESSION_TIME_OUT = "Session ผู้ใช้งานหมดอายุ ระบบจะให้ Login อีกครั้ง";
                public static readonly string INVALID_UNIT_HAS_BOOKING = "ไม่สามารถบันทึกได้ เนื่องจาก Unit นี้ มีรายการจองอยู่ กรุณายกเลิกรายการจองก่อน";
                public static readonly string INVALID_UNIT_HAS_PERMISSION = "Save invalid. You not have permission.\r\nPlease Contract {0} or admin";
                public static readonly string INVALID_UNIT_NOT_HAS_PERMISSION = "Save invalid. You not has permission";
                public static readonly string INVALID_BOOKING_HAS_CANCEL = "ไม่สามารถบันทึกได้ เนื่องจากรายการจองนี้มีสถานะ ยกเลิกแล้ว";
                public static readonly string PROJECT_DOES_NOT_EXISTS = "ไม่พบโครงการนี้";
                public static readonly string PLEASE_INPUT_SELECT_BU = "โปรดระบุ BU Group";
                public static readonly string EMIAL_HAS_ALREADY = "email already exists.";
                public static readonly string PROJECT_QUOTA_INVALID = "Project quota invalid.";
                public static readonly string TF_PATYMENT_VERIFY_ANOTHER_USER = "Save fail.This payment verify by another user";
                public static readonly string TF_PATYMENT_APPROVE_QUOTA_INVALID = "Quota invalid.";
                public static readonly string TF_PATYMENT_APPROVE_AMOUNT_INVALID = "Approve amount invalid.";
                public static readonly string TF_PATYMENT_FAIL_QUOTA_INVALID = "Please input quota = 0.";
            }
            public static class Success
            {
                public static readonly string SAVE_SUCCESS = "Save successful.";                
            }

        }
        public static class ExtType
        {
            public static readonly int REGISTER_TYPE_ID = 4;
            public static readonly int PAYMENT_VERIFY_STATUS = 5;
        }
        public static class Ext
        {
            public static readonly string PLEASE_SELECT_ITEM = "-- Please Select --";
            public static readonly string SELECT_ALL = "-- Select All --";
            public static readonly int PAYMENT_VERIFY_STATUS_VERIFY = 8;
            public static readonly int PAYMENT_VERIFY_STATUS_APPROVE = 9;
            public static readonly int PAYMENT_VERIFY_STATUS_FAIL = 10;
        }
        public static class UnitStatus
        {
            public static readonly int CLOSE = 0;
            public static readonly int AVAILABLE = 1;
            public static readonly int BOOKING = 2;
            public static readonly int PAYMENT = 3;
            public static readonly int CONTRACT = 4;
        }
        public static class BookingStatus
        {
            public static readonly int CANCEL = 0;
            public static readonly int WAIT_PAYMENT = 1;
            public static readonly int PAYMENT_SUCCESS = 2;
        }
        public static class Department
        {
            public static readonly int INFORNATION_TECHNOLOGY = 1;            
            public static readonly int SEG1 = 2;
            public static readonly int SEG2 = 3;
            public static readonly int SEG3 = 4;
            public static readonly int SEG_TITLE = 8;
        }
        public static class Role
        {
            public static readonly int ADMIN = 1;
            public static readonly int VP = 2;
            public static readonly int MANAGEMENT = 3;
            public static readonly int OFFICER = 4;
        }
        public static class OmiseChargeStatus
        {
            public static readonly string FAILED = "Failed";
            public static readonly string PENDDING = "Pending";
            public static readonly string SUCCESSFUL = "Successful";
        }
    }
}
