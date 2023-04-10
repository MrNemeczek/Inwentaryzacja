using System;
using System.Collections.Generic;
using Inwentaryzacja.Shared.Models;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Spolki
    {
        public int IdSpolka { get; set; }
        public string NazwaSpolka { get; set; } = null!;
        public string? NIP { get; set; }
        public string? Symbol { get; set; }

        public virtual List<Faktury>? Faktury { get; set; }
        public virtual List<Sprzet>? Sprzets { get; set; }
        public virtual List<NumeryInwentaryzacyjneNew>? NumeryInwentaryzacyjneNews { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w spolce odwoluje sie do statycznej metody <see cref="ValidSpolka(Spolki)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidSpolka()
        {
            return ValidSpolka(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w spolce
        /// </summary>
        /// <param name="spolka"> obiekt spolki do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidSpolka(Spolki spolka)
        {
            if (spolka.NazwaSpolka == null)
            {
                return false;
            }

            if (spolka.NazwaSpolka.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
