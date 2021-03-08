using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Types
{
    [Serializable]
    public class Product
    {
        public int ProductID{get;set;}

        public string ProductName{get;set;}

        public double? Cost {get;set;}
        public short? Stock{get;set;}
        public bool Discontinued{get;set;}

        // these two define the foreign key relationship
        public int CategoryID{get;set;}
        
        [NonSerialized()]
        public virtual Category Categort {get;set;}
    }
}