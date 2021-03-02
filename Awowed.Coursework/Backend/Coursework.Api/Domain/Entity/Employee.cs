using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework.Api.Domain
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public byte Id { get; set; }
        public string Rntrc { get; set; }
        public string PassId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string PhoneNum { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
