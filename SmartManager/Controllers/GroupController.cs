using Microsoft.AspNetCore.Mvc;
using SmartManager.Models.Applicants;
using SmartManager.Models.Groups;
using SmartManager.Services.Proccessings.Applicants;
using SmartManager.Services.Proccessings.Groups;
using System;
using System.Linq;

namespace SmartManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupProcessingService groupProcessingService;
        private readonly IApplicantProcessingService applicantProcessingService;

        public GroupController(IGroupProcessingService groupProcessingService, IApplicantProcessingService applicantProcessingService)
        {
            this.groupProcessingService = groupProcessingService;
            this.applicantProcessingService = applicantProcessingService;
        }

        public IActionResult ShowGroups()
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            return View(groups);
        }


        // get
        public IActionResult PostGroup()
        {
            return View();
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostGroup(Group group)
        {
            group.GroupId = Guid.NewGuid();

            this.groupProcessingService.AddGroupAsync(group);

            return RedirectToAction("ShowGroups");
        }

        // getById
        [HttpGet]
        public IActionResult PutGroup(Guid groupId)
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            Group group = groups.SingleOrDefault(a => a.GroupId == groupId);

            return View(group);
        }

        [HttpPost]
        public IActionResult PutGroup(Group group)
        {
            IQueryable<Applicant> putApplicants = this.applicantProcessingService.RetrieveAllApplicants();

            foreach (Applicant applicant in putApplicants.Where(a => a.GroupId == group.GroupId))
            {
                applicant.GroupName = group.GroupName;

                this.applicantProcessingService.ModifyApplicantWithGroupAsync(applicant);
            }

            if (ModelState.IsValid)
            {
                this.groupProcessingService.ModifyGroupAsync(group);

                return RedirectToAction("ShowGroups");
            }
            return View("Error");
        }

        // getById
        [HttpGet]
        public IActionResult DeleteGroup(Guid groupId)
        {
            IQueryable<Group> groups = this.groupProcessingService.RetrieveAllGroups();

            Group group = groups.SingleOrDefault(a => a.GroupId == groupId);

            this.groupProcessingService.RemoveGroupAsync(group.GroupId);

            return RedirectToAction("ShowGroups");
        }
    }
}
