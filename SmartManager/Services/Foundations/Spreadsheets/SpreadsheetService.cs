//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using SmartManager.Brokers.Spreadsheets;
using SmartManager.Models.ExternalApplicants;

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
