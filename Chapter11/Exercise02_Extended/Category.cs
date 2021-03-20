using System;

namespace Northwind.Types
{
    // For binary serialization class should have Serializable Attribute 
    [Serializable]
    public class Category{
        public int CategoryID{get;set;}
        public string CategoryName {get;set;}
        public string Description{get;set;}
    }
}