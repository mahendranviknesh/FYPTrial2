using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace DisasterRecoverySystem.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Documents/Upload
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Documents/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                ViewBag.Message = "File uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return View();
        }

        // GET: Documents/ViewFiles
        [HttpGet]
        public IActionResult ViewFiles()
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var files = Directory.GetFiles(uploadPath).Select(Path.GetFileName).ToList();
            return View(files);
        }
    }
}
