using Ratatouille.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ratatouille.FileStorage
{
    public class RecipeLogic : IRecipeLogic
    {
        private Context context = Context.Instanse;

        public void Create(Recipe model)
        {
            model.Id = IdHelper.GetId();

            model.Thumbnail = ImageSaver.SaveImage(model.Thumbnail);
            for (int i = 0; i < model.Images.Count; i++)
                model.Images[i] = ImageSaver.SaveImage(model.Images[i]);

            Recipe recipe = new Recipe
            {
                Id = model.Id,
                Name = model.Name ?? "",
                Thumbnail = model.Thumbnail ?? "",
                Tags = model.Tags ?? "",
                ApproxTime = model.ApproxTime ?? "",
                Ingredients = model.Ingredients ?? "",
                Tools = model.Tools ?? "",
                Instruction = model.Instruction ?? "",
                Notes = model.Notes ?? "",
                Links = new List<string>(model.Links),
                Images = new List<string>(model.Images)
            };

            context.Recipes.Add(recipe);
            context.Save();
        }

        public void Delete(string id)
        {
            Recipe model = context.Recipes.FirstOrDefault(req => req.Id == id);

            if (model == null)
                throw new Exception("Рецепта с указанным Id не найдено");

            context.Recipes.Remove(model);
            context.Save();
        }

        public List<Recipe> Find(string query)
        {
            if (query == "")
            {
                return
                    context.Recipes
                    .Select(req => new Recipe
                    {
                        Id = req.Id,
                        Name = req.Name,
                        Thumbnail = req.Thumbnail,
                        Tags = req.Tags,
                        ApproxTime = req.ApproxTime,
                        Ingredients = req.Ingredients,
                        Tools = req.Tools,
                        Instruction = req.Instruction,
                        Notes = req.Notes,
                        Links = new List<string>(req.Links),
                        Images = new List<string>(req.Images)
                    })
                    .ToList();
            }

            string[] tokens = query.Split();
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            Action<Func<Recipe, string>> addRecipes =
                (field) =>
                {
                    foreach (string token in tokens)
                        foreach (Recipe recipe in context.Recipes.Where(req => field(req).ToLower().Contains(token.ToLower())))
                            recipes.Add(recipe);
                };

            addRecipes(recipe => recipe.Name);
            addRecipes(recipe => recipe.Tags);
            addRecipes(recipe => recipe.Ingredients);
            addRecipes(recipe => recipe.Tools);
            addRecipes(recipe => recipe.Instruction);
            addRecipes(recipe => recipe.Notes);

            return
                recipes.Select(req => new Recipe
                {
                    Id = req.Id,
                    Name = req.Name,
                    Thumbnail = req.Thumbnail,
                    Tags = req.Tags,
                    ApproxTime = req.ApproxTime,
                    Ingredients = req.Ingredients,
                    Tools = req.Tools,
                    Instruction = req.Instruction,
                    Notes = req.Notes,
                    Links = new List<string>(req.Links),
                    Images = new List<string>(req.Images)
                })
                .ToList();
        }

        public Recipe Read(string id)
        {
            Recipe model = context.Recipes.FirstOrDefault(req => req.Id == id);

            if (model == null)
                throw new Exception("Рецепта с указанным Id не найдено");

            Recipe recipe = new Recipe
            {
                Id = model.Id,
                Name = model.Name,
                Thumbnail = model.Thumbnail,
                Tags = model.Tags,
                ApproxTime = model.ApproxTime,
                Ingredients = model.Ingredients,
                Tools = model.Tools,
                Instruction = model.Instruction,
                Notes = model.Notes,
                Links = new List<string>(model.Links),
                Images = new List<string>(model.Images)
            };

            return recipe;
        }

        public void Update(Recipe model)
        {
            Recipe recipe = context.Recipes.FirstOrDefault(req => req.Id == model.Id);

            if (recipe == null)
                throw new Exception("Рецепта с указанным Id не найдено");

            recipe.Name = model.Name ?? "";
            recipe.Thumbnail = model.Thumbnail ?? "";
            recipe.Tags = model.Tags ?? "";
            recipe.ApproxTime = model.ApproxTime ?? "";
            recipe.Ingredients = model.Ingredients ?? "";
            recipe.Tools = model.Tools ?? "";
            recipe.Instruction = model.Instruction ?? "";
            recipe.Notes = model.Notes ?? "";
            recipe.Links = new List<string>(model.Links);
            recipe.Images = new List<string>(model.Images);

            recipe.Thumbnail = ImageSaver.SaveImage(recipe.Thumbnail);
            for (int i = 0; i < model.Images.Count; i++)
                recipe.Images[i] = ImageSaver.SaveImage(recipe.Images[i]);

            context.Save();
        }
    }
}
