using System.Collections.Generic;
using System.Collections.DataAnnotations.Schema;

namespace Northwind.Types
{
    public class Category
    {
        public int CategoryID {get; set;}

        public string CategoryName {get; set;}

        public string Description{get;set;}

        [NonSerialized()]
        // navigation property
        public virtual ICollection<Product> Products {get; set;}

        public Category()
        {
            this.Products = new List<Product>();
        }
    }
}