using DocumentFormat.OpenXml.InkML;
using Inwentaryzacja.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inwentaryzacja.Server.Controllers
{
    public class ExportInwentaryzacjaController : ExportController
    {
        private readonly inwentaryzacjaContext _context;

        public ExportInwentaryzacjaController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        [HttpGet("/export/numery/excel")]
        public FileStreamResult ExportNumeryToExcel()
        {
            return ToExcel(ApplyQuery(_context.NumeryInwentaryzacyjne, Request.Query));
        }

        [HttpGet("/export/sprzet/excel")]
        public FileStreamResult ExportSprzetToExcel()
        {
            var sprzety = from s in _context.Sprzet
                          select new
                          {
                              s.IdSprzet,
                              s.NazwaSprzet,
                              s.NumerSeryjny,
                              s.Comment,
                              s.Model,
                              s.Marka,
                              s.IdFakturaNavigation.NazwaFaktura,
                              s.IdNrInwentaryzacyjnyNavigation.Numer,
                              s.IdNrInwentaryzacyjnyNewNavigation.NumerNew,
                              s.IdTypSprzetNavigation.Typ,
                              s.IdSpolkaNavigation.NazwaSpolka,
                              s.IdStanNavigation.Stan,
                              s.IdOddzialyNavigation.OddzialNazwa
                          };

            return ToExcel(ApplyQuery(sprzety, Request.Query));
        }

    }
}
