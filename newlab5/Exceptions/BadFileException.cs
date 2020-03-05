using System;
namespace newlab5{
    public class BadFileException : Exception{
        public BadFileException() : base("Bad file"){}
    }
}