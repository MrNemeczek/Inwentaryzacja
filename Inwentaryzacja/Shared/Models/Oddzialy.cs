using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Oddzialy
    {
        public int IdOddzialy { get; set; }
        public string OddzialNazwa { get; set; } = null!;        

        public virtual List<Sprzet>? Sprzet { get; set; }
        public virtual List<Komp>? Komps { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w oddziale odwoluje sie do statycznej metody <see cref="ValidOddzial(Oddzialy)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidOddzial()
        {
            return ValidOddzial(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w oddziale
        /// </summary>
        /// <param name="oddzial"> obiekt oddzialu do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidOddzial(Oddzialy oddzial)
        {
            if (oddzial.OddzialNazwa == null)
            {
                return false;
            }

            if (oddzial.OddzialNazwa.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
