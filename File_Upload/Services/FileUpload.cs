using Microsoft.AspNetCore.Components.Forms;
namespace File_Upload.Services
{
    public interface IFileUpload
    {
        Task UploadFile(IBrowserFile file);
    }

    public class FileUpload : IFileUpload
    {
        private IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<FileUpload> logger;

        public FileUpload(IWebHostEnvironment webHostEnvironment, ILogger<FileUpload> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

        public async Task UploadFile(IBrowserFile file)
        {
            if (file == null) 
            {
                try 
                {
                    var uploadPath = Path.Combine(this.webHostEnvironment.WebRootPath,"upload", file.Name);

                    using (var stream = file.OpenReadStream())
                    {
                        var fileStream = File.Create(uploadPath);
                        await stream.CopyToAsync(fileStream);
                        fileStream.Close();
                    }


                }catch(Exception ex) 
                {
                    this.logger.LogError(ex.ToString());
                }
            }
        }
    }
}
