using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer
{
    public class FileHelper
    {
        public const string PESSOA_DIRECTORY = "\\imgPessoa\\";
        public const string EVENTO_DIRECTORY = "\\imgEvento\\";
        public const string EXTENSION = ".jpg";


        public static bool IsValidExtension(string fileName)
        {
            return fileName.Contains(".jpg") || fileName.Contains(".png") || fileName.Contains(".jpeg") || fileName.Contains(".gif");
        }



    }
}
