using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomException(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
