using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SaveStudentImage
    {
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }
        public bool IsUpdate { get; set; } = false;
        public string OldFileName { get; set; }
    }
}
