//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Orchestrations;
using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Controllers
{
    public class SpreadsheetController : Controller
    {
        private readonly IOrchestrationService orchestrationService;

        public SpreadsheetController(IOrchestrationService orchestrationService)
        {
            this.orchestrationService = orchestrationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportFile(IFormFile formFile)
        {
            IFormFile importFile = Request.Form.Files[0];

            using (MemoryStream stream = new MemoryStream())
            {
                importFile.CopyTo(stream);
                stream.Position = 0;
                await this.orchestrationService.ProcessImportRequest(stream);
            }

            return RedirectToAction("ShowGroups", "Group");

        }
    }
}
