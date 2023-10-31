//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Models.ExternalApplicants;
using SmartManager.Services.Foundations.Spreadsheets;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Proccessings.Spreadsheets
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
                spreadsheetService.GetExternalApplicants(stream);

            return validExternalApplicants;
        }
    }
}
