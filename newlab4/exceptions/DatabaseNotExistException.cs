using System;
namespace newlab4.Exceptions{
    public class DatabaseNotExistException : Exception{
        public DatabaseNotExistException() : base("Database doesn't exist"){}
    }
}