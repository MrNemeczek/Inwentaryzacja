using System;
using System.Collections.Generic;

namespace Inwentaryzacja.Shared.Models
{
    //public enum FactureType
    //{
    //    Program = 1,
    //    Sprzet = 2,
    //    SprzetIProgram = 4
    //}
    public partial class Faktury
    {

        #region pomocnicze wlasciwosci z foreignkey
        public string? nazwaTypFaktury { get; set; }
        public string? nazwaSpolka { get; set; }
        //public FactureType FactureType => (FactureType)IdTypFaktura;
        #endregion

        public int IdFaktura { get; set; }
        public string NazwaFaktura { get; set; }
        public DateTime? DataFaktura { get; set; }
        public decimal KwotaFaktura { get; set; }
        public int IdSpolka { get; set; }
        public int IdTypFaktura { get; set; }
        public byte[]? PlikFaktura { get; set; }
        public string? PlikNazwa { get; set; }

        public virtual Spolki? IdSpolkaNavigation { get; set; } 
        public virtual TypFaktury? IdTypFakturaNavigation { get; set; }
        public virtual List<Sprzet>? Sprzet { get; set; }
        public virtual List<Licencje>? Licencjes { get; set; }

        #region pomocnicze metody
        /// <summary>
        /// statyczna metoda do sprawdzania poprawnosci danych w fakturze
        /// </summary>
        /// <param name="faktura"> obiekt faktury do sprawdzenia </param>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public static bool ValidFaktury(Faktury faktura)
        {
            if (faktura.NazwaFaktura == null || faktura.DataFaktura == null || faktura.IdSpolka == 0 || faktura.IdTypFaktura == 0)
            {
                return false;
            }

            if (faktura.NazwaFaktura.Trim() == "" )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// metoda do sprawdzania poprawnosci danych w fakturze odwoluje sie do statycznej metody <see cref="ValidFaktury(Faktury)"/>
        /// </summary>
        /// <returns> 
        /// false - jesli dane sa niepoprawne
        /// true - jesli dane sa poprawne
        /// </returns>
        public bool ValidFaktury()
        {
            return ValidFaktury(this);
        }
        #endregion
    }
}
