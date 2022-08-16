using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Ratatouille.FileStorage
{
    internal static class ImageSaver
    {
        internal static string SaveImage(string link)
        {
            if (string.IsNullOrWhiteSpace(link) || link.StartsWith(@"data:image"))
                return link;

            string path = @$"{AppContext.BaseDirectory}\storage\{Guid.NewGuid()}.jpg";

            if (link.StartsWith("http"))
                SaveFromWeb(link, path);
            else
                SaveFromLocal(link, path);

            string uri = ToUri(path);

            FileInfo file = new FileInfo(path);
            file.Delete();

            return uri;
        }

        private static void SaveFromWeb(string link, string path)
        {
            using (WebClient client = new WebClient())
                client.DownloadFile(link, path);
        }

        private static void SaveFromLocal(string link, string path)
        {
            File.Copy(link, path);
        }

        private static string ToUri(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                List<byte> bytes = new List<byte>();
                int b;
                while ((b = stream.ReadByte()) != -1)
                    bytes.Add((byte)b);
                byte[] arr = bytes.ToArray();
                string imgeBase64Data = Convert.ToBase64String(arr);
                string imgDataURL = $"data:image/jpg;base64,{imgeBase64Data}";
                return imgDataURL;
            }
        }
    }
}
