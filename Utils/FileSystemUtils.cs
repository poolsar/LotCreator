using System.IO;
using System.Linq;

namespace Utils
{
    public class FileSystemUtils
    {
        public static string GetFileName(FileInfo file, DirectoryInfo folder)
        {
            string fileName = file.Name;

            var sameNameExist = folder.GetFiles().ToList().Any(f => f.Name == file.Name);

            if (sameNameExist)
            {
                var ext = file.Extension;
                fileName = fileName.ExeptString(ext);

                var lastSymbol = fileName.Last();
                int FileNumber = 0;
                if (int.TryParse(lastSymbol.ToString(), out FileNumber))
                {
                    FileNumber++;
                    fileName += FileNumber + ext;
                }
            }
            return fileName;
        }



        public static DirectoryInfo GetFolder(string path)
        {
            var res = new DirectoryInfo(path);
            if (!res.Exists)
            {
                res.Create();
            }
            return res;
        }

    }
}