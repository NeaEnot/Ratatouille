using System;
using System.Linq;
using System.Text;

namespace Ratatouille.FileStorage
{
    internal static class IdHelper
    {
        private static string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        internal static string GetId()
        {
            string answer = "";
            int i = 2;
            Random random = new Random();

            while (true)
            {
                StringBuilder builder = new StringBuilder();

                alphabet
                    .ToArray()
                    .OrderBy(e => Guid.NewGuid())
                    .Take(random.Next(1, i))
                    .ToList().ForEach(e => builder.Append(e));

                answer = builder.ToString();

                if (Context.Instanse.Recipes.Count(req => req.Id == answer) == 0)
                    break;

                i++;
            }

            return answer;
        }
    }
}
