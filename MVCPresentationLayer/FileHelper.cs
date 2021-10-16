using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void SavePicture(List<IFormFile> arquivos, string fullFileName)
        {
            int count = 1;
            foreach (var item in arquivos)
            {
                using (FileStream stream = new FileStream(fullFileName + "\\"+count+".jpg", FileMode.Create))
                {
                    item.CopyToAsync(stream);
                }
                count++;
            }
        }

    }
}
