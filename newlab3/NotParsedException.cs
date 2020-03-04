using System;
namespace newlab3{
    public class NotParsedException : Exception{
        public NotParsedException(string message):base(message){}
    }
}