using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Appkomp
    {
        #region pomocnicze wlasciwosci z foreignkey
        public string? kompNazwaDomena { get; set; }
        public string? oddzialNazwa { get; set; }
        public string? nazwaApp { get; set; }
        public string? ip { get; set; }
        public int? blacklist { get; set; }

        #endregion
        public int IdAppkomp { get; set; }
        public int IdApp { get; set; }
        public int IdKomp { get; set; }

        public virtual App IdAppNavigation { get; set; } = null!;
        public virtual Komp IdKompNavigation { get; set; } = null!;
    }
}
