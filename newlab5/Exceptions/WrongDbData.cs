using System;
namespace newlab5{
    public class WrongDbData : Exception{
        public WrongDbData() : base("Wrong DB data"){}
    }
}