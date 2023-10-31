//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using SmartManager.Brokers.Loggings;
using SmartManager.Models.Applicants;
using SmartManager.Services.Foundations.Applicants;
using SmartManager.Services.Proccessings.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Proccessings.Applicants
{
    public partial class ApplicantProcessingService : IApplicantProcessingService
    {
        private readonly IApplicantService applicantService;
        private readonly IGroupProcessingService groupProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public ApplicantProcessingService(
            IApplicantService applicantService,
            IGroupProcessingService groupProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.applicantService = applicantService;
            this.groupProcessingService = groupProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            applicant.ApplicantId = Guid.NewGuid();
            applicant.CreatedDate = DateTime.Now;

            var newGroup = await this.groupProcessingService.EnsureGroupExistsByName(applicant.GroupName);

            applicant.GroupId = newGroup.GroupId;

            return await this.applicantService.AddApplicantAsync(applicant);
        });

        public ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid) =>
            TryCatch(async () => await this.applicantService.RetrieveApplicantByIdAsync(applicantid));

        public IQueryable<Applicant> RetrieveAllApplicants() =>
            TryCatch(() => this.applicantService.RetrieveAllApplicants());

        public ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
            {
                var newGroup = await this.groupProcessingService.EnsureGroupExistsByName(applicant.GroupName);

                applicant.GroupId = newGroup.GroupId;

                return await this.applicantService.ModifyApplicantAsync(applicant);
            });

        public ValueTask<Applicant> ModifyApplicantWithGroupAsync(Applicant applicant) =>
            TryCatch(async () => await this.applicantService.ModifyApplicantAsync(applicant));

        public ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid) =>
            TryCatch(async () => await this.applicantService.RemoveApplicantAsync(applicantid));
    }
}
