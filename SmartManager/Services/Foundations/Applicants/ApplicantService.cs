//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.DateTimes;
using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Storages;
using SmartManager.Models.Applicants;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SmartManager.Services.Foundations.Applicants
{
    public class ApplicantService : IApplicantService
    {
        private readonly IStorageBroker storageBroker;

        public ApplicantService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
            await this.storageBroker.InsertApplicantAsync(applicant);

        public async ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid) =>
            await this.storageBroker.SelectApplicantByIdAsync(applicantid);

        public IQueryable RetrieveAllApplicants() =>
            this.storageBroker.SelectAllApplicants();

        public async ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            await this.storageBroker.UpdateAppolicantAsync(applicant);

        public async ValueTask<Applicant> RemoveApplicantAsync(Guid guid)
        {
            Applicant applicant = await this.storageBroker.SelectApplicantByIdAsync(guid);

            return await this.storageBroker.DeleteApplicantAsync(applicant);
        }
    }
}
