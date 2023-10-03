using System.Net;
using API.Dtos;
using AutoMapper;
using iText.Html2pdf.Attach.Impl.Tags;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UploadFilesController : BaseApiController
    {
        // Define the constructor and initialize the IWebHostEnvironment variable, which
        // provides information about the web hosting environment in which an application runs,
        // in order to get the root path of the content.
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper mapper;

        public UploadFilesController(IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UploadRestoreDto>>> PostUploadFile(List<IFormFile> formFiles)
        {
            // Create a directory for storing uploaded files if it doesn't exist, and obtain the current working directory of the application.
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\files");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a list instance.
            List<UploadRestoreDto> uploadRestoreDtos = new();

            // Iterate through the list of uploaded files.
            foreach (var file in formFiles)
            {
                if (file.Length > 0)
                {
                    // Create a new instance of the UploadRestoreDto object.
                    UploadRestoreDto uploadRestoreDto = new();
                    string fileNameAleatorio; // Define a name to save the new uploaded file.

                    // Get the original file name.
                    var fileNameOriginal = file.FileName;

                    // Store the original file name in the object.
                    uploadRestoreDto.FileName = fileNameOriginal;

                    // Encode the original file name for displaying it.
                    var fileNameMostarOriginal = WebUtility.HtmlEncode(fileNameOriginal);

                    // Generate a random name for the uploaded file to prevent overwriting.
                    fileNameAleatorio = Path.GetRandomFileName();

                    // Create the file path where the file will be stored.
                    var ruta = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads\\files", fileNameAleatorio);

                    // Create the file and save it at the specified path.
                    using (var crearArchivo = new FileStream(ruta, FileMode.Create))
                    {
                        await file.CopyToAsync(crearArchivo); // Copy bytes from the current stream to another stream.
                    }

                    // Add the newly created file name to the object and then store it in the list.
                    uploadRestoreDto.StoredFileName = fileNameAleatorio;
                    uploadRestoreDtos.Add(uploadRestoreDto);
                }
            }

            // Return the list of UploadRestoreDto objects.
            return this.mapper.Map<List<UploadRestoreDto>>(uploadRestoreDtos);
        }
    }
}
