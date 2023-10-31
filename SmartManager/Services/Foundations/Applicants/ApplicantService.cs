//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Storages;
using SmartManager.Models.Applicants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Applicants
{
    public partial class ApplicantService : IApplicantService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ApplicantService(IStorageBroker storageBroker, ILoggingBroker loggingBroker, Brokers.DateTimes.IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            ValidateApplicantOnAdd(applicant);

            return await this.storageBroker.InsertApplicantAsync(applicant);
        });

        public ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid) =>
        TryCatch(async () =>
        {
            ValidateApplicantId(applicantid);

            var maybeApplicant =
              await this.storageBroker.SelectApplicantByIdAsync(applicantid);

            ValidateStorageApplicant(maybeApplicant, applicantid);

            return await this.storageBroker.SelectApplicantByIdAsync(applicantid);
        });
        public IQueryable<Applicant> RetrieveAllApplicants() =>
            TryCatch(() => this.storageBroker.SelectAllApplicants());

        public ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            ValidateApplicantOnModify(applicant);

            var maybeApplicant =
                await this.storageBroker.SelectApplicantByIdAsync(applicant.ApplicantId);

            ValidateAgainstStorageApplicantOnModify(inputAccount: applicant, storageApplicant: maybeApplicant);

            return await this.storageBroker.UpdateAppolicantAsync(applicant);
        });

        public async ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid)
        {
            ValidateApplicantId(applicantid);

            var maybeApplicant = await this.storageBroker.SelectApplicantByIdAsync(applicantid);

            ValidateStorageApplicant(maybeApplicant, applicantid);

            return await this.storageBroker.DeleteApplicantAsync(maybeApplicant);
        }
    }
}
