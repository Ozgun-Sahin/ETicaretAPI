using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class PassowrdChangeFailedException : Exception
    {
        public PassowrdChangeFailedException() : base("Şifre güncellenirken bir sorun oluştu.")
        {
        }

        public PassowrdChangeFailedException(string? message) : base(message)
        {
        }

        public PassowrdChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
