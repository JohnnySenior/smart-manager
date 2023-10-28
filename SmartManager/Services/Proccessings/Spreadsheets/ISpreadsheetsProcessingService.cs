//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Models.ExternalApplicants;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Proccessings.Spreadsheets
{
    public interface ISpreadsheetsProcessingService
    {
        List<ExternalApplicant> ReadExternalApplicants(MemoryStream stream);
    }
}
