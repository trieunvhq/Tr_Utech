
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QRMS.interfaces
{
    public interface IFile
    {
        Task<string> SaveFile(Stream stream, string fileName);
    }
}
