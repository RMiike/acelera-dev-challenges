using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            _context = context;

        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
            => HandleCandidateSelect(c => c.AccelerationId == accelerationId)
                    .ToList();

        public IList<Candidate> FindByCompanyId(int companyId)
            => HandleCandidateSelect(x => x.CompanyId == companyId)
                    .ToList();

        public Candidate FindById(int userId, int accelerationId, int companyId)
            => HandleCandidateSelect(
                c => c.UserId == userId &&
                c.AccelerationId == accelerationId &&
                c.CompanyId == companyId)
                .FirstOrDefault();

        public Candidate Save(Candidate candidate)
        {
            if (FindById(candidate.UserId, candidate.AccelerationId, candidate.CompanyId) == null)
                _context.Add(candidate);
            else
                _context.Entry(candidate).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return candidate;

            throw new ArgumentException("Candidate cannot be saved.");
        }
        private IQueryable<Candidate> HandleCandidateSelect(Expression<Func<Candidate, bool>> condition)
           => _context
                   .Candidates
                   .Where(condition)
                   .AsNoTracking();
    }
}
