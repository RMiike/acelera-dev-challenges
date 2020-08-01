using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly CodenationContext _context;

        public CompanyService(CodenationContext context)
        {
            _context = context;

        }

        public IList<Company> FindByAccelerationId(int accelerationId)
            => _context.Candidates
                .Where(x => x.AccelerationId == accelerationId)
                .Select(x => x.Company)
                .AsNoTracking()
                .ToList();

        public Company FindById(int id)
            => _context
                .Companies
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault();

        public IList<Company> FindByUserId(int userId)
            => _context
                .Candidates
                .Where(x => x.UserId == userId)
                .Select(x => x.Company)
                .AsNoTracking()
                .Distinct()
                .ToList();

        public Company Save(Company company)
        {
            if (company.Id == 0)
                _context.Add(company);
            else
                _context.Entry(company).State = EntityState.Modified;

            if (_context.SaveChanges() >= 0)
                return company;

            throw new ArgumentException("Company cannot be saved.");
        }
    }
}