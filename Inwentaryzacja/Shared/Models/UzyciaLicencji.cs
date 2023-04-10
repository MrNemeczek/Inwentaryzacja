using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class UzyciaLicencji
    {
        #region pomocnicze wlasciwosci z foreignkey
        public string? kompNazwaDomena { get; set; }
        #endregion
        public int IdUzyciaLicencji { get; set; }
        public int IdKomp { get; set; }
        public int IdLic { get; set; }

        public virtual Komp? IdKompNavigation { get; set; } = null!;
        public virtual Licencje? IdLicNavigation { get; set; } = null!;
    }
}
