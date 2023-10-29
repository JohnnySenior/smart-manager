//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Applicants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Applicants
{
    public interface IApplicantService
    {
        ValueTask<Applicant> AddApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid applicantid);
        IQueryable<Applicant> RetrieveAllApplicants();
        ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RemoveApplicantAsync(Guid applicantid);
    }
}
