using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class App
    {

        public int IdApp { get; set; }
        public string NazwaApp { get; set; } = null!;
        public string Wersja { get; set; } = null!;
        public int? Blacklist { get; set; }

        public virtual List<Appkomp>? Appkomps { get; set; }
    }
}
