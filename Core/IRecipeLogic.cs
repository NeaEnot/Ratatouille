using System.Collections.Generic;

namespace Ratatouille.Core
{
    /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/IRecipeLogic/*'/>
    public interface IRecipeLogic
    {
        /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/Create/*'/>
        public void Create(Recipe model);

        /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/Read/*'/>
        public Recipe Read(string id);

        /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/Find/*'/>
        public List<Recipe> Find(string query);

        /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/Update/*'/>
        public void Update(Recipe model);

        /// <include file='Docs.xml' path='docs/members[@name="IRecipeLogic"]/Delete/*'/>
        public void Delete(string id);
    }
}
