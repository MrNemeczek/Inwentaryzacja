using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class NumeryInwentaryzacyjne
    {
        public int IdNrInwentaryzacyjny { get; set; }
        public string Numer { get; set; } = null!;

        public virtual List<Sprzet>? Sprzety { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w numerze inwentaryzacyjnym odwoluje sie do statycznej metody <see cref="ValidNR(NumeryInwentaryzacyjne)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidNR()
        {
            return ValidNR(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w numerze inwentaryzacyjnym
        /// </summary>
        /// <param name="nr"> obiekt numeru inwentaryzacyjnego do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidNR(NumeryInwentaryzacyjne nr)
        {
            if (nr.Numer == null)
            {
                return false;
            }

            if (nr.Numer.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
