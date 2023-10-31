using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Applicants;
using SmartManager.Services.Proccessings.Applicants;
using SmartManager.Services.Proccessings.Groups;
using System;
using System.Linq;

namespace SmartManager.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantProcessingService applicantProcessingService;
        private readonly IGroupProcessingService groupProcessingService;

        public ApplicantController(
            IApplicantProcessingService applicantProcessingService,
            IGroupProcessingService groupProcessingService)
        {
            this.applicantProcessingService = applicantProcessingService;
            this.groupProcessingService = groupProcessingService;
        }

        public IActionResult ShowApplicants()
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

            return View(applicants);
        }

        public IActionResult ShowApplicantWithGroup(Guid groupId)
        {
            IQueryable<Applicant> applicants =
                this.applicantProcessingService.RetrieveAllApplicants().Where(a => a.GroupId == groupId);

            return View(applicants);
        }

        // get
        public IActionResult PostApplicant()
        {
            return View();
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostApplicant(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                this.applicantProcessingService.AddApplicantAsync(applicant);

                return RedirectToAction("ShowApplicants");
            }
            return View(applicant);
        }

        // getById
        [HttpGet]
        public IActionResult PutApplicant(Guid applicantId)
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

            Applicant applicant = applicants.SingleOrDefault(a => a.ApplicantId == applicantId);

            return View(applicant);
        }
        // getById
        [HttpGet]
        public IActionResult PutApplicantInGroup(Guid applicantId)
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

            Applicant applicant = applicants.SingleOrDefault(a => a.ApplicantId == applicantId);

            return View(applicant);
        }

        // put
        [HttpPost]
        public IActionResult PutApplicant(Applicant applicant)
        {
            applicantProcessingService.ModifyApplicantAsync(applicant);

            return RedirectToAction("ShowApplicants");
        }
        // put
        [HttpPost]
        public IActionResult PutApplicantInGroup(Applicant applicant)
        {
            applicantProcessingService.ModifyApplicantAsync(applicant);

            return RedirectToAction("ShowApplicantWithGroup");
        }

        // getById
        [HttpGet]
        public IActionResult DeleteApplicant(Guid applicantId)
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

            Applicant applicant = applicants.SingleOrDefault(a => a.ApplicantId == applicantId);

            this.applicantProcessingService.RemoveApplicantAsync(applicant.ApplicantId);

            return RedirectToAction("ShowApplicants");
        }
        // getById
        [HttpGet]
        public IActionResult DeleteApplicantInGroup(Guid applicantId)
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

            Applicant applicant = applicants.SingleOrDefault(a => a.ApplicantId == applicantId);

            this.applicantProcessingService.RemoveApplicantAsync(applicant.ApplicantId);

            return RedirectToAction("ShowApplicantWithGroup");
        }
    }
}
