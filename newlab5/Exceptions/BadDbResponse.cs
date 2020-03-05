using System;
namespace newlab5{
    public class BadDbResponse : Exception{
        public BadDbResponse() : base("Bad response"){}
    }
}