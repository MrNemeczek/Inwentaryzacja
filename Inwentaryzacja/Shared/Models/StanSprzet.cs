using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class StanSprzet
    {
        //public StanSprzet()
        //{
        //    Sprzet = new HashSet<Sprzet>();
        //}

        public int IdStan { get; set; }
        public string Stan { get; set; } = null!;

        public virtual List<Sprzet>? Sprzet { get; set; }
    }
}
