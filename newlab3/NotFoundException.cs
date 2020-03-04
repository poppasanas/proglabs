using System;
namespace newlab3{
    public class NotFoundException : Exception{
        public NotFoundException(string message):base(message){}
    }
}