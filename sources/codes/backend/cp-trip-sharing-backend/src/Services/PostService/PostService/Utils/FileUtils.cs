using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Utils
{
    public class FileUtils
    {
        public static string CHARS = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static string RandomImageName()
        {
            var now = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var randomString = FileUtils.RandomString(16);
            return string.Join("-", new string[] { "image", now, randomString });
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Sample imageType: image/jpeg
        public static string GetImageExtension(string imageType)
        {
            var arr = imageType.Split("/");
            var extension = "";
            if (arr.Length >= 2)
            {
                switch (arr[1])
                {
                    case "jpeg":
                        extension = ".jpg";
                        break;
                    case "png":
                        extension = ".png";
                        break;
                }
            }
            return extension;
        }
    }
}
