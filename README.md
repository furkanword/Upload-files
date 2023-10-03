
Settings

{
  "ConnectionStrings": {
    "ConexSqlServer" : "Data Source = localhost\\sqlexpress; Initial Catalog = dB; Integrate Security = True",
    "ConexMysql" : "server = localhost; user = root; database = Upload_file"
  },
  
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  
  "AllowedHosts": "*",

  "JWT" : {
    "Key" : "Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContextcontext",
    "Issuer" : "API",
    "Audience" : "API",
    "DurationInMinutes" : 20
  }
}

appsettings.Development

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "ConexSqlServer":"Data Source=localhost\\sqlexpress;Initial Catalog=dB;Integrate Security=True",
    "ConexMysql":"server=localhost;user=root;database=Upload-data"
  }
}


🧠 "ConnectionStrings": Define las cadenas de conexión a bases de datos,tanto para SQL Server como para MySQL.
 Esto permite que la aplicación se conecte a estas bases de datos utilizando estas cadenas de conexión.


🧠 "Logging": Establece el nivel de registro para diferentes componentes de la aplicación.
 En este caso, el nivel de registro predeterminado es "Information",
  pero se reduce a "Warning" para los componentes de Microsoft.AspNetCore.


🧠 "AllowedHosts": Permite que la aplicación sea accesible desde cualquier host. Esto es útil durante el desarrollo,
 pero se debe configurar adecuadamente en un entorno de producción para aumentar la seguridad.


🧠 "JWT": Configura la autenticación y autorización mediante tokens JWT. Define la clave, el emisor,
 la audiencia y la duración de los tokens JWT utilizados en la aplicación.

Go to the project directory

Run

dotnet run

Controllador

```bash
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

🧠 Constructor: En el constructor, se inyecta una instancia de IWebHostEnvironment (que proporciona información sobre el entorno de alojamiento de la aplicación)
 y una instancia de IMapper (utilizada para mapear objetos). Esto permite acceder al entorno de alojamiento y al mapeo de objetos dentro del controlador.

🧠 Método PostUploadFile: Este método se invoca cuando se realiza una solicitud HTTP POST en la ruta del controlador. 
Acepta una lista de archivos (formFiles) como entrada. Aquí está el flujo de trabajo:

⚡️ Se crea un directorio paraalmacenar los archivos cargados si aún no existe.

⚡️ Se inicializa una lista de objetos UploadRestoreDto para almacenar información sobre los archivos cargados.

⚡️ Se itera a través de la lista de archivos cargados. Para cada archivo, se realiza lo siguiente:

⚡️ Se crea un objeto UploadRestoreDto para mantener información sobre el archivo.

⚡️ Se obtiene el nombre original del archivo.

⚡️ Se codifica el nombre original del archivo para su visualización.

⚡️ Se genera un nombre aleatorio para el archivo cargado para evitar sobrescribir archivos existentes.

⚡️ Se crea una ruta de archivo donde se guardará el archivo.

⚡️ Se crea y guarda el archivo en la ubicación especificada.

⚡️ Se agrega el nombre del archivo almacenado en el objeto UploadRestoreDto.

⚡️ Se agrega el objeto UploadRestoreDto a la lista.

⚡️ Se devuelve la lista de objetos UploadRestoreDto mapeados a una lista de la misma forma mediante AutoMapper.

  
```

Start the server

```bash
 ⚡️⚡️⚡️ Gracias espero que te sirva mi Repositorio ⚡️⚡️⚡️
```

