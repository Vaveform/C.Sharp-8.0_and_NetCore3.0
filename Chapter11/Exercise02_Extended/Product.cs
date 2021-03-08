using System;
// DataAnnotations for attributes, if using Fluent API it doesn't need
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Types
{
    // For binary serialization class should have Serializable Attribute 
    [Serializable]
    public class Product{
        public string ProductID {get; set;}
        public string ProductName {get; set;}

        // ? - nullable value - to this value can be assign null
        public double? Cost {get; set;}
        public short? Stock{get; set;}
        public string QuantityPerUnit{get; set;}
        public short? UnitsOnOrder{get; set;}
        public short? ReorderLevel{get;set;}
    }
}