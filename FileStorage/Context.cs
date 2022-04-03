using Newtonsoft.Json;
using Ratatouille.Core;
using System.Collections.Generic;
using System.IO;

namespace Ratatouille.FileStorage
{
    internal class Context
    {
        private static Context instance;

        internal List<Recipe> Recipes { get; set; }

        private Context()
        {
            DirectoryInfo dir = new DirectoryInfo("storage");
            if (!dir.Exists)
                dir.Create();

            Load();
        }

        internal static Context Instanse
        {
            get
            {
                if (instance == null)
                    instance = new Context();

                return instance;
            }
        }

        internal void Save()
        {
            using (StreamWriter writer = new StreamWriter(@"storage\storage.json"))
            {
                string json = JsonConvert.SerializeObject(Recipes);
                writer.Write(json);
            }
        }

        internal void Load()
        {
            DirectoryInfo dir = new DirectoryInfo("storage");
            if (!dir.Exists)
                dir.Create();

            try
            {
                using (StreamReader reader = new StreamReader(@"storage\storage.json"))
                {
                    string json = reader.ReadToEnd();
                    Recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
                }
            }
            catch
            {
                Recipes = new List<Recipe>();
            }
        }
    }
}
