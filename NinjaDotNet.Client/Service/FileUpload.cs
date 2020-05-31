using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using NinjaDotNet.Client.Contracts;

namespace NinjaDotNet.Client.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _env;

        public FileUpload(IWebHostEnvironment e)
        {
            _env = e;
        }
        public async Task UploadFile(IFileListEntry file, MemoryStream msFile, string picName)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, "uploads", picName);
                await using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    msFile.WriteTo(fs);
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
