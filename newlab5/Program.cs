using System;
using System.IO;

namespace newlab5{
    class Program{
        static void Main(){
            Triangle firstTriangle = new Triangle(new Vector2D(0, 0), new Vector2D(0, 5), new Vector2D(5, 0));
            StreamWriter xmlFileWrite = new StreamWriter("./Triangle.xml");
            firstTriangle.XmlSerialize(xmlFileWrite);
            xmlFileWrite.Close();
            Triangle deserializedTriangle = new Triangle();
            StreamReader xmlFileRead = new StreamReader("./Triangle.xml");
            deserializedTriangle.XmlDeserialize(xmlFileRead);
            xmlFileRead.Close();
            string equal = "Objects Equal", notEqual = "Objects Not Equal";
            Console.WriteLine(firstTriangle == deserializedTriangle
                                    ? equal
                                    : notEqual);
            FileStream binFileWrite = new FileStream("./Triangle.bin", FileMode.Create);
            firstTriangle.BinSerialize(binFileWrite);
            binFileWrite.Close();
            deserializedTriangle = new Triangle();
            FileStream binFileRead = new FileStream("./Triangle.bin", FileMode.Open);
            deserializedTriangle.BinDeserialize(binFileRead);
            binFileRead.Close();
            Console.WriteLine(firstTriangle == deserializedTriangle
                                    ? equal
                                    : notEqual);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SerializeDB;";
           //firstTriangle.DBSerialize(connectionString, 2);
            deserializedTriangle = new Triangle();
            deserializedTriangle.DBDeserialize(connectionString, 2);
            Console.WriteLine(firstTriangle == deserializedTriangle
                                   ? equal
                                   : notEqual);
        }
    }
}
