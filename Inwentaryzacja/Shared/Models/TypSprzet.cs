using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class TypSprzet
    {
        //public TypSprzet()
        //{
        //    Sprzet = new HashSet<Sprzet>();
        //}

        public int IdTypSprzet { get; set; }
        public string? Typ { get; set; }

        public virtual List<Sprzet>? Sprzet { get; set; }
    }
}
