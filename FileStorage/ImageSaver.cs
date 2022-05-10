using System;
using System.IO;
using System.Net;

namespace Ratatouille.FileStorage
{
    internal static class ImageSaver
    {
        internal static string SaveImage(string link)
        {
            if (link.StartsWith(@"storage\"))
                return link;

            string path = @"storage\" + Guid.NewGuid().ToString();

            if (link.StartsWith("http"))
                SaveFromWeb(link, path);
            else if (link.StartsWith("data:image"))
                SaveFromBase64(link, path);
            else
                SaveFromLocal(link, path);

            return path;
        }

        private static void SaveFromWeb(string link, string path)
        {
            using (WebClient client = new WebClient())
                client.DownloadFile(link, path);
        }

        private static void SaveFromBase64(string link, string path)
        {
            string base64 = link.Split(',')[1];

            byte[] bytes = Convert.FromBase64String(base64);

            using (var file = new FileStream(path, FileMode.Create))
            {
                file.Write(bytes, 0, bytes.Length);
                file.Flush();
            }
        }

        private static void SaveFromLocal(string link, string path)
        {
            File.Copy(link, path);
        }
    }
}
