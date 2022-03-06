using System.Collections.Generic;

namespace Ratatouille.Core
{
    /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Recipe/*'/>
    public class Recipe
    {
        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Id/*'/>
        public string Id { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Name/*'/>
        public string Name { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Thumbnail/*'/>
        public string Thumbnail { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Tags/*'/>
        public string Tags { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/ApproxTime/*'/>
        public string ApproxTime { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Ingredients/*'/>
        public string Ingredients { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Tools/*'/>
        public string Tools { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Instruction/*'/>
        public string Instruction { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Links/*'/>
        public List<string> Links { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Recipe"]/Images/*'/>
        public List<string> Images { get; set; }

        public Recipe()
        {
            Links = new List<string>();
            Images = new List<string>();
        }
    }
}
