//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Brokers.Spreadsheets;
using SmartManager.Models.ExternalApplicants;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Foundations.Spreadsheets
{
    public class SpreadsheetService : ISpreadsheetService
    {
        private readonly ISpreadsheetBroker spreadsheetBroker;

        public SpreadsheetService(ISpreadsheetBroker spreadsheetBroker)
        {
            this.spreadsheetBroker = spreadsheetBroker;
        }

        public List<ExternalApplicant> GetExternalApplicants(MemoryStream stream)
        {
            List<ExternalApplicant> externalApplicants =
                            this.spreadsheetBroker.ImportApplicants(stream);

            return externalApplicants;
        }
    }
}
