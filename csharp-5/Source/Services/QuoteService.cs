using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class QuoteService : IQuoteService
    {
        private ScriptsContext _context;
        private IRandomService _randomService;

        public QuoteService(ScriptsContext context, IRandomService randomService)
        {
            this._context = context;
            this._randomService = randomService;
        }

        public Quote GetAnyQuote()
        {
            var result = _context
                            .Quotes
                            .AsNoTracking()
                            .ToList();
            var x = result.Count();
            var results = result.Count;
            return result[_randomService.RandomInteger(result.Count())];

        }

        public Quote GetAnyQuote(string actor)
        {
            var result = _context
                            .Quotes
                            .Where(x => x.Actor == actor)
                            .AsNoTracking()
                            .ToList();

            if (result.Count() == 0)
                return result.FirstOrDefault();

            var max = _randomService.RandomInteger(result.Count());

            return result[max];
        }
    }
}