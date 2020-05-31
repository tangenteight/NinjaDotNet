using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;

namespace NinjaDotNet.Client.Contracts
{
    public interface IFileUpload
    {
        public Task UploadFile(IFileListEntry file, MemoryStream msFile, string picName);
    }
}
