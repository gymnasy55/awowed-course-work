using System;
using System.Collections.Generic;

#nullable disable

namespace TestWebApi.Domain
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
