using Inwentaryzacja.Server.Models;
using Inwentaryzacja.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inwentaryzacja.Server.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class NrInwentaryzacjaNewController : Controller
    {
        private readonly inwentaryzacjaContext _context;

        public NrInwentaryzacjaNewController(inwentaryzacjaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// glowna metoda GET ktora zwraca wszystkie numery inwentaryzacyjne razem z nazwa spolki
        /// </summary>
        /// <returns> wszystkie numery inwentaryzacyjne razem z nazwa spolki </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var numeryInwentaryzacyjne = from ni in _context.NumeryInwentaryzacyjneNew
                                         select new
                                         {
                                             ni.IdNrInwentaryzacyjny,
                                             ni.NumerNew,
                                             ni.IdSpolka,
                                             ni.IdSpolkaNavigation.NazwaSpolka
                                         };

            return Ok(numeryInwentaryzacyjne);

        }

        /// <summary>
        /// sprawdza jaki jest ostatni numer przy danym <paramref name="symbol"/> i zwraca o jeden wiekszy numer
        /// </summary>
        /// <param name="symbol"> symbol numeru do sprawdzenia np. DC </param>
        /// <returns> sybol ktory trzeba dodac </returns>
        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetLastNrWithSymbol(string symbol)
        {
            var numeryInwentaryzacyjne = await (from ni in _context.NumeryInwentaryzacyjneNew
                                        where ni.IdSpolkaNavigation.Symbol == symbol
                                        orderby ni.NumerNew
                                        select ni).ToListAsync();

            //jesli to pierwszy rekord
            if (numeryInwentaryzacyjne.Count == 0 || numeryInwentaryzacyjne == null)
            {
                NumeryInwentaryzacyjneNew numer = new NumeryInwentaryzacyjneNew()
                {
                    NumerNew = symbol + 1.ToString("D5"),
                    IdSpolka = 0
                };

                return Ok(numer);
            }

            //szukanie brakujacego numeru w srodku
            string missingNumber = numeryInwentaryzacyjne
                .Select((n, i) => new { Number = n, Index = i })
                .Where(x => x.Number.NumerNew != symbol + (x.Index + 1).ToString("D5"))
                .Select(x => symbol + (x.Index + 1).ToString("D5"))
                .FirstOrDefault();

            if (missingNumber != null && missingNumber != "")
            {
                NumeryInwentaryzacyjneNew numer = new NumeryInwentaryzacyjneNew()
                {
                    NumerNew = missingNumber,
                    IdSpolka = 0
                };

                return Ok(numer);
            }

            //szukanie ostatniego najwiekszego numeru
            var nrInw = numeryInwentaryzacyjne.OrderByDescending(ni => ni.NumerNew).FirstOrDefault();

            string sufix = nrInw.NumerNew.Substring(symbol.Length);
            int number = Int32.Parse(sufix.TrimStart('0'));
            number++;

            nrInw.NumerNew = symbol + number.ToString().PadLeft(5, '0');

            return Ok(nrInw);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var numerInwentaryzacyjny = await _context.NumeryInwentaryzacyjneNew.FirstOrDefaultAsync(ni => ni.IdNrInwentaryzacyjny == id);
            return Ok(numerInwentaryzacyjny);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NumeryInwentaryzacyjneNew numerInwentaryzacyjny)
        {
            if(_context.NumeryInwentaryzacyjneNew.Select(nr => nr.NumerNew).Contains(numerInwentaryzacyjny.NumerNew))
            {
                return StatusCode(422);
            }

            _context.Add(numerInwentaryzacyjny);
            await _context.SaveChangesAsync();

            return Ok(numerInwentaryzacyjny.IdNrInwentaryzacyjny);
        }

        [HttpPut]
        public async Task<IActionResult> Put(NumeryInwentaryzacyjneNew numerInwentaryzacyjny)
        {
            if (_context.NumeryInwentaryzacyjneNew.Select(nr => nr.NumerNew).Contains(numerInwentaryzacyjny.NumerNew))
            {
                return StatusCode(422);
            }

            _context.Entry(numerInwentaryzacyjny).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var numerInwentaryzacyjny = new NumeryInwentaryzacyjneNew { IdNrInwentaryzacyjny = id };
            _context.Remove(numerInwentaryzacyjny);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
