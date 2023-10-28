//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.EntityFrameworkCore;
using SmartManager.Models.Applicants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Applicant> Applicants { get; set; }

        public async ValueTask<Applicant> InsertApplicantAsync(Applicant applicant) =>
            await InsertAsync(applicant);

        public IQueryable<Applicant> SelectAllApplicants() =>
            SelectAll<Applicant>().AsQueryable();

        public async ValueTask<Applicant> SelectApplicantByIdAsync(Guid applicantId) =>
            await SelectAsync<Applicant>(applicantId);

        public async ValueTask<Applicant> UpdateAppolicantAsync(Applicant applicant) =>
            await UpdateAsync(applicant);

        public async ValueTask<Applicant> DeleteApplicantAsync(Applicant applicant) =>
            await DeleteAsync(applicant);
    }
}

