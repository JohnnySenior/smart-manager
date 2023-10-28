//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Models.ExternalApplicants;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Foundations.Spreadsheets
{
    public interface ISpreadsheetService
    {
        List<ExternalApplicant> GetExternalApplicants(MemoryStream stream);
    }
}
