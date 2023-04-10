using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    public partial class Sprzet
    {
        #region pomocnicze wlasciwosci z foreignkey
        public string? typ { get; set; }
        public string? nazwaFaktura { get; set; }
        public string? stan { get; set; }
        public string? oddzialNazwa { get; set; }
        public string? nazwaSpolka { get; set; }
        public string? numer { get; set; }
        public string? numerNew { get; set; }
        #endregion

        public int IdSprzet { get; set; }
        public string? NazwaSprzet { get; set; } = null!;
        public int? IdFaktura { get; set; }
        public int IdTypSprzet { get; set; }
        public int IdStan { get; set; }
        public int IdOddzialy { get; set; }
        public string? NumerSeryjny { get; set; }
        public int? IdSpolka { get; set; }
        public int? IdNrInwentaryzacyjny { get; set; }
        public int? IdNrInwentaryzacyjnyNew { get; set; }
        public string? Comment { get; set; }
        public string? Model { get; set; }
        public string? Marka { get; set; }

        public virtual Faktury? IdFakturaNavigation { get; set; }
        public virtual Oddzialy? IdOddzialyNavigation { get; set; }
        public virtual Spolki? IdSpolkaNavigation { get; set; }
        public virtual StanSprzet? IdStanNavigation { get; set; } 
        public virtual TypSprzet? IdTypSprzetNavigation { get; set; }
        public virtual NumeryInwentaryzacyjne? IdNrInwentaryzacyjnyNavigation { get; set; }
        public virtual NumeryInwentaryzacyjneNew? IdNrInwentaryzacyjnyNewNavigation { get; set; }
        public virtual List<Komp>? Komps { get; set; }

        #region pomocnicze metody

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w sprzecie odwoluje sie do statycznej metody <see cref="ValidSprzet(Sprzet)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidSprzet()
        {
            return ValidSprzet(this);
        }

        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w sprzecie
        /// </summary>
        /// <param name="sprzet"> obiekt sprzetu do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidSprzet(Sprzet sprzet)
        {
            if(sprzet.IdNrInwentaryzacyjnyNew != null)
            {
                if(sprzet.IdSpolka == null)
                {
                    return false;
                }

            }

            if (sprzet.NazwaSprzet == null || sprzet.IdStan == 0 || sprzet.IdTypSprzet == 0 || sprzet.IdOddzialy == 0)
            {
                return false;
            }

            if (sprzet.NazwaSprzet.Trim() == "")
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
