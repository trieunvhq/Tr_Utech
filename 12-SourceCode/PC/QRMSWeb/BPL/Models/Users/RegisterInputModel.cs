using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPL.Models.Users
{
    public class RegisterInputModel
    {
        public string Ctype { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WarehouseCode { get; set; }
        public string CreateUser { get; set; }
    }
}
