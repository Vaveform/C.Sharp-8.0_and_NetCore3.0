using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Northwind.Types
{
    [Serializable]
    public class Category
    {
        public int CategoryID {set; get;}

        public string CategoryName {set; get;}

        public string Description{set;get;}

        // navigation property - without virtual beacuse without proxy
        public List<Product> Products {set; get;}
    }
}