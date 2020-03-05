using System;
namespace newlab4.Exceptions{
    public class ConfigWrongException : Exception{
        public ConfigWrongException() : base("Wrong configs"){}
    }
}