//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Brokers.Loggings;
using SmartManager.Models.Applicants;
using SmartManager.Models.ExternalApplicants;
using SmartManager.Models.Groups;
using SmartManager.Services.Proccessings.Applicants;
using SmartManager.Services.Proccessings.Groups;
using SmartManager.Services.Proccessings.Spreadsheets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Services.Orchestrations
{
    public partial class OrchestrationService : IOrchestrationService
    {
        private readonly ISpreadsheetsProcessingService spreadsheetProcessingService;
        private readonly IApplicantProcessingService applicantProcessingService;
        private readonly IGroupProcessingService groupProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public OrchestrationService(
            ISpreadsheetsProcessingService spreadsheetProcessingService,
            IApplicantProcessingService applicantProcessingService,
            IGroupProcessingService groupProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.spreadsheetProcessingService = spreadsheetProcessingService;
            this.applicantProcessingService = applicantProcessingService;
            this.groupProcessingService = groupProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public Task ProcessImportRequest(MemoryStream stream) =>
        TryCatch(async () =>
        {
            List<ExternalApplicant> validExternalApplicants =
                this.spreadsheetProcessingService.ReadExternalApplicants(stream);


            foreach (var externalApplicant in validExternalApplicants)
            {

                Group ensureGroup =
                    await groupProcessingService
                    .EnsureGroupExistsByName(externalApplicant.GroupName);

                Applicant applicant = MapToApplicant(externalApplicant, ensureGroup);

                await applicantProcessingService.AddApplicantAsync(applicant);
            }
        });
        private Applicant MapToApplicant(ExternalApplicant externalApplicant, Group ensureGroup)
        {
            return new Applicant
            {
                ApplicantId = Guid.NewGuid(),
                FirstName = externalApplicant.FirstName,
                LastName = externalApplicant.LastName,
                CreatedDate = externalApplicant.CreatedDate,
                Email = externalApplicant.Email,
                PhoneNumber = externalApplicant.PhoneNumber,
                GroupId = ensureGroup.GroupId,
                GroupName = ensureGroup.GroupName
            };
        }
    }
}
