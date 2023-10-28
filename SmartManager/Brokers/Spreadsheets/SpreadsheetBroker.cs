//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections.Generic;
using System.IO;
using Bytescout.Spreadsheet;
using SmartManager.Models.ExternalApplicants;

namespace SmartManager.Brokers.Spreadsheets
{
    public class SpreadsheetBroker : ISpreadsheetBroker
    {
        public List<ExternalApplicant> ImportApplicants(MemoryStream stream)
        {
            var importApplicants = new List<ExternalApplicant>();
            var externalApplicant = new ExternalApplicant();

            Spreadsheet document = new Spreadsheet();

            document.LoadFromStream(stream);

            Worksheet worksheet = document.Workbook.Worksheets[0];

            for (int row = 1; row <= worksheet.UsedRangeRowMax; row++)
            {
                externalApplicant.ExternalApplicantId = Guid.NewGuid();
                externalApplicant.FirstName = worksheet.Cell(row, 1).ToString();
                externalApplicant.LastName = worksheet.Cell(row, 2).ToString();
                externalApplicant.PhoneNumber = worksheet.Cell(row, 3).ToString();
                externalApplicant.Email = worksheet.Cell(row, 4).ToString();

                string dateString = worksheet.Cell(row, 5).ToString();
                if (DateTimeOffset.TryParse(dateString, out DateTimeOffset date))
                {
                    externalApplicant.BirthDate = date;
                }

                externalApplicant.GroupName = worksheet.Cell(row, 6).ToString();

                importApplicants.Add(externalApplicant);
            }

            return importApplicants;
        }
    }
}
