using Inwentaryzacja.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Licencje
    {
        #region pomocnicze wlasciwosci z foreignkey
        public string? nazwaFaktura { get; set; }
        #endregion

        public int IdLic { get; set; }
        public string Nazwa { get; set; }
        public string? Klucz { get; set; }
        public DateTime? DataWygLic { get; set; }
        public int? LiczbaLicencji { get; set; }        
        public int? IdFaktura { get; set; }

        public virtual Faktury? IdFakturaNavigation { get; set; }
        public virtual List<UzyciaLicencji>? UzyciaLicencji { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w licencji odwoluje sie do statycznej metody <see cref="ValidLicence(Licencje)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidLicence()
        {
            return ValidLicence(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w licencji
        /// </summary>
        /// <param name="licencja"> obiekt licencji do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidLicence(Licencje licencja)
        {
            if(licencja.Nazwa == null)
            {
                return false;
            }

            if (licencja.Nazwa.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
