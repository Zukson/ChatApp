using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Files
{
    interface  IFileManager
    {
        bool TemporaryFileExists(string fileName);

        Task SaveTemporaryFile(string fileName);

        Task DeleteTemporaryFile(string fileName);




        


    }
}
