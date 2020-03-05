using System;

namespace newlab4{
    public class Product : IComparable{
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public Product() {}
        public Product(string name, double price, int count){
            Name = name;
            Price = price;
            Count = count;
        }
        public int CompareTo(object comparing){
            if(!(comparing is Product comp)){
                throw new Exception("Couldn't compare!");
            }
            if(this.Price < comp.Price){
                return -1;
            }
            if(this.Price > comp.Price){
                return 1;
            }
            else{
            return 0;
            }
        }
        public override string ToString(){
            return $"{this.Name}";
        }
        public override int GetHashCode(){
            return Name.GetHashCode();
        }
    }
}