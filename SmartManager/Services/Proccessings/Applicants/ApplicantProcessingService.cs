//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================
using SmartManager.Models.Applicants;
using SmartManager.Services.Foundations.Applicants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Proccessings.Applicants
{
    public class ApplicantProcessingService : IApplicantProcessingService
    {
        private readonly IApplicantService applicantService;

        public ApplicantProcessingService(IApplicantService applicantService)
        {
            this.applicantService = applicantService;
        }

        public async ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
           await this.applicantService.AddApplicantAsync(applicant);

        public async ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid) =>
            await this.applicantService.RetrieveApplicantByIdAsync(applicantid);

        public IQueryable RetrieveAllApplicants() =>
            this.applicantService.RetrieveAllApplicants();

        public async ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            await this.applicantService.ModifyApplicantAsync(applicant);

        public async ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid) =>
            await this.applicantService.RemoveApplicantAsync(applicantid);
    }
}
