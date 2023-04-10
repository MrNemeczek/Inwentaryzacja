using Inwentaryzacja.Shared.Models;
using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Komp
    {
        #region pomocnicze wlasciwosci z foreignkey
        public string? oddzialNazwa { get; set; }
        public string? nazwaSprzet { get; set; }
        #endregion

        public int IdKomp { get; set; }
        public string KompNazwaDomena { get; set; } 
        public DateTime? ZrzutCzas { get; set; }
        public string? Ip { get; set; }
        public int? IdSprzet { get; set; }
        public int? IdUser { get; set; }
        public int? IdOddzial { get; set; }

        public virtual Sprzet? IdSprzetNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
        public virtual Oddzialy? IdOddzialNavigation { get; set; }
        public virtual List<Appkomp>? Appkomps { get; set; }
        public virtual List<UzyciaLicencji>? UzyciaLicencji { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w komputerze odwoluje sie do statycznej metody <see cref="ValidKomp(Komp)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidKomp()
        {
            return ValidKomp(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w kompie
        /// </summary>
        /// <param name="komp"> obiekt komputera do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidKomp(Komp komp)
        {
            if (komp.KompNazwaDomena == null )
            {
                return false;
            }

            if (komp.KompNazwaDomena.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
