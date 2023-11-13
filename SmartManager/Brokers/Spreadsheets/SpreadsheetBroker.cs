//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Bytescout.Spreadsheet;
using SmartManager.Models.ExternalApplicants;
using System;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Brokers.Spreadsheets
{
    public class SpreadsheetBroker : ISpreadsheetBroker
    {
        public List<ExternalApplicant> ImportApplicants(MemoryStream stream)
        {
            var importApplicants = new List<ExternalApplicant>();

            Spreadsheet document = new Spreadsheet();

            document.LoadFromStream(stream);

            Worksheet worksheet = document.Workbook.Worksheets[0];

            for (int row = 1; row <= worksheet.UsedRangeRowMax; row++)
            {
                var externalApplicant = new ExternalApplicant();

                externalApplicant.ExternalApplicantId = Guid.NewGuid();
                externalApplicant.FirstName = worksheet.Cell(row, 0).ToString();
                externalApplicant.LastName = worksheet.Cell(row, 1).ToString();
                externalApplicant.PhoneNumber = worksheet.Cell(row, 2).ToString();
                externalApplicant.Email = worksheet.Cell(row, 3).ToString();
                externalApplicant.GroupName = worksheet.Cell(row, 4).ToString();
                externalApplicant.CreatedDate = DateTimeOffset.Now;

                importApplicants.Add(externalApplicant);
            }

            return importApplicants;
        }
    }
}
