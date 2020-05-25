using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Client.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Content { get; set; }
    }
}
