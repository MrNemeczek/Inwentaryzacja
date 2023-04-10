using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inwentaryzacja.Shared.Models
{
    public class NumeryInwentaryzacyjneNew
    {
        #region pomocnicze wlasciwosci
        public string? nazwaSpolka { get; set; }
        #endregion
        public int IdNrInwentaryzacyjny { get; set; }
        public int? IdSpolka { get; set; }
        public string NumerNew { get; set; } = null!;

        public virtual Spolki? IdSpolkaNavigation { get; set; }
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
        public static bool ValidNR(NumeryInwentaryzacyjneNew nr)
        {
            if (nr.NumerNew == null)
            {
                return false;
            }

            if (nr.NumerNew.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
