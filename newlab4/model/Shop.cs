using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newlab4{
    public class Shop : IEquatable<Shop>{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public Shop() { }
        public Shop(int ShopId, string Name){
            this.Id = ShopId;
            this.Name = Name;
        }
        public override int GetHashCode(){
            return Id.GetHashCode();
        }
        override public bool Equals(object other){
            Shop second = other as Shop;
            return this.Id == second.Id
                 ? true
                 : false;
        }
        bool IEquatable<Shop>.Equals(Shop other){
            return this.Id == other.Id
                  ? true
                  : false;
        }
        public override string ToString(){
            return $"{this.Id},{this.Name}";
        }
    }
}