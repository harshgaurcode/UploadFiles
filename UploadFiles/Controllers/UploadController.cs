using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace UploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
       
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadController(IWebHostEnvironment environment)
        {
            
            _hostingEnvironment = environment;
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            // Create a unique folder name for the uploaded file.
            var folderName = Guid.NewGuid().ToString();
            string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Files", folderName);

            // Check if the folder path is valid.
            if (folderPath == null)
            {
                return BadRequest("Path is Incorrect");
            }

            // Create the folder if it does not exist.
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate a unique file name for the uploaded file.
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Get the full file path.
            var filePath = Path.Combine(folderPath, fileName);

            // Save the uploaded file to the specified path.
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return a success response.
            return Ok("Files Uploaded successfully");
        }


    }
}
