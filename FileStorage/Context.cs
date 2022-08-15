using Newtonsoft.Json;
using Ratatouille.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ratatouille.FileStorage
{
    internal class Context
    {
        private static Context instance;
        private Dictionary<string, int> hashes;

        internal List<Recipe> Recipes { get; set; }

        private Context()
        {
            DirectoryInfo dir = new DirectoryInfo("storage");
            if (!dir.Exists)
                dir.Create();

            Load();

            foreach (Recipe recipe in Recipes)
                hashes.Add(recipe.Id, recipe.GetHashCode());
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
            foreach (Recipe recipe in Recipes)
            {
                if (hashes.ContainsKey(recipe.Id) && hashes[recipe.Id] == recipe.GetHashCode())
                    continue;

                using (StreamWriter writer = new StreamWriter(@$"storage\{recipe.Id}.recipe"))
                {
                    string json = JsonConvert.SerializeObject(recipe);
                    writer.Write(json);
                }
            }
        }

        internal void Load()
        {
            DirectoryInfo dir = new DirectoryInfo("storage");
            if (!dir.Exists)
                dir.Create();

            List<Recipe> recipes = new List<Recipe>();

            foreach (FileInfo file in dir.EnumerateFiles().Where(req => req.Name.EndsWith(".recipe")))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        string json = reader.ReadToEnd();
                        Recipe recipe = JsonConvert.DeserializeObject<Recipe>(json);
                        recipes.Add(recipe);
                    }
                }
                catch (Exception ex)
                { }
            }

            Recipes = recipes;
        }
    }
}
