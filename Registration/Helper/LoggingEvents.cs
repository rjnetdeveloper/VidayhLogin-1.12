using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Helper
{
    public class LoggingEvents
    {
        public const int Login = 1000;
        public const int Logout = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;
      
        public const int UsernameNotFound = 2000;
        public const int PasswordNotFound = 2001;

        public const int ValidToken = 3000;
        public const int InvalidTOken = 3001;

        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;

        public const int UserNameFound = 5000;
        public const int EmailFound = 5001;
        public const int SomethingWentWrong = 5002;
    }
}
