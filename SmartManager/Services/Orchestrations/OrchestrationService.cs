//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SmartManager.Models.ExternalApplicants;
using SmartManager.Services.Proccessings;

namespace SmartManager.Services.Orchestrations
{
    public class OrchestrationService : IOrchestrationService
    {
        private readonly ISpreadsheetsProcessingService spreadsheetProcessingService;

        public OrchestrationService(ISpreadsheetsProcessingService spreadsheetProcessingService)
        {
            this.spreadsheetProcessingService = spreadsheetProcessingService;
        }

        public async Task ProcessImportRequest(MemoryStream stream)
        {
            List<ExternalApplicant> validExternalApplicants =
                this.spreadsheetProcessingService.ReadExternalApplicants(stream);
        }
    }
}
