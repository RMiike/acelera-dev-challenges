using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private readonly CodenationContext _context;
        public AccelerationService(CodenationContext context)
        {
            _context = context;

        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            var xxxx = _context.Candidates.Include(c => c.Acceleration)
                                      .Where(c => c.CompanyId.Equals(companyId))
                                      .Select(c => c.Acceleration).
                                      Distinct().ToList();

            return xxxx;
        }   

        public Acceleration FindById(int id)
           => _context
                .Accelerations
                .Where(a => a.Id == id)
                .FirstOrDefault();

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id == 0)
                _context.Add(acceleration);
            else
                _context.Entry(acceleration).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return acceleration;

            throw new ArgumentException("Acceleration cannot be saved.");
        }
    }
}
