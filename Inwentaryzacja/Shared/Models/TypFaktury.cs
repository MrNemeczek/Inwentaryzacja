using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class TypFaktury
    {
        public int IdTypFaktury { get; set; }
        public string NazwaTypFaktury { get; set; } = null!;

        public virtual List<Faktury>? Fakturies { get; set; }
    }
}
