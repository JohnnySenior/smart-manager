//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using SmartManager.Models.Applicants;
using SmartManager.Services.Foundations.Applicants;
using SmartManager.Services.Proccessings.Groups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Proccessings.Applicants
{
    public class ApplicantProcessingService : IApplicantProcessingService
    {
        private readonly IApplicantService applicantService;
        private readonly IGroupProcessingService groupProcessingService;

        public ApplicantProcessingService(IApplicantService applicantService, IGroupProcessingService groupProcessingService)
        {
            this.applicantService = applicantService;
            this.groupProcessingService = groupProcessingService;
        }

        public async ValueTask<Applicant> AddApplicantAsync(Applicant applicant)
        {
            applicant.ApplicantId = Guid.NewGuid();

            var newGroup = await this.groupProcessingService.EnsureGroupExistsByName(applicant.GroupName);

            applicant.GroupId = newGroup.GroupId;

            return await this.applicantService.AddApplicantAsync(applicant);
        }

        public async ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid) =>
            await this.applicantService.RetrieveApplicantByIdAsync(applicantid);

        public IQueryable<Applicant> RetrieveAllApplicants() =>
            this.applicantService.RetrieveAllApplicants();

        public async ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            await this.applicantService.ModifyApplicantAsync(applicant);

        public async ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid) =>
            await this.applicantService.RemoveApplicantAsync(applicantid);
    }
}
