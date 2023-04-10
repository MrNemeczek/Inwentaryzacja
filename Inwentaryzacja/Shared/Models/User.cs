using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class User
    {
        public int? IdUsers { get; set; }
        public string? UserName { get; set; } 
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Mail { get; set; }
        public DateTime? LastLogged { get; set; }
        public virtual List<Komp>? Komps { get; set; }
    }
}
