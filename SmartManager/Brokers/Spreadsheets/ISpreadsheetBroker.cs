//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using SmartManager.Models.ExternalApplicants;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Brokers.Spreadsheets
{
    public interface ISpreadsheetBroker
    {
        List<ExternalApplicant> ImportApplicants(MemoryStream stream);
    }
}
