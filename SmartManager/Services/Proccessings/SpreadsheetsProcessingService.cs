//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using SmartManager.Models.ExternalApplicants;
using SmartManager.Services.Foundations.Spreadsheets;

namespace SmartManager.Services.Proccessings
{
    public class SpreadsheetsProcessingService : ISpreadsheetsProcessingService
    {
        private readonly ISpreadsheetService spreadsheetService;

        public SpreadsheetsProcessingService(ISpreadsheetService spreadsheetService)
        {
            this.spreadsheetService = spreadsheetService;
        }

        public List<ExternalApplicant> ReadExternalApplicants(MemoryStream stream)
        {
            List<ExternalApplicant> validExternalApplicants =
                this.spreadsheetService.GetExternalApplicants(stream);

            return validExternalApplicants;
        }
    }
}
