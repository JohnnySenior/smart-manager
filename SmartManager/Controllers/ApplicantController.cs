using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Applicants;
using SmartManager.Services.Proccessings.Applicants;
using System.Linq;

namespace SmartManager.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantProcessingService applicantProcessingService;

        public ApplicantController(IApplicantProcessingService applicantProcessingService)
        {
            this.applicantProcessingService = applicantProcessingService;
        }

        public IActionResult ShowApplicants()
        {
            IQueryable<Applicant> applicants = this.applicantProcessingService.RetrieveAllApplicants();

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
    }
}
