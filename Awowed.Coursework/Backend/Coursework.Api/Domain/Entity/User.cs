using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework.Api.Domain
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string PhoneNum { get; set; }
        public string Mail { get; set; }
        public bool AdminRights { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
