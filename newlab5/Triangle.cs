using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;

namespace newlab5{
    [Serializable]
    public class Triangle : ISerialize
    {
        //пофиксил вроде
        public Vector2D A { get; set; }
        public Vector2D B { get; set; }
        public Vector2D C { get; set; }
        public Triangle(){}
        public Triangle(Vector2D a, Vector2D b, Vector2D c){
            A = a;
            B = b;
            C = c;
        }
        public void Clone(Triangle triangle){
            A = triangle.A;
            B = triangle.B;
            C = triangle.C;
        }
        public void XmlSerialize(StreamWriter filename){
            XmlSerializer serializer = new XmlSerializer(typeof(Triangle));
            serializer.Serialize(filename, this);
        }
        //владос бля стой не добавляй ты файл
        //все заработало я пофиксил по идее
        public void XmlDeserialize(StreamReader filename){
            XmlSerializer serializer = new XmlSerializer(typeof(Triangle));
            if (!(serializer.Deserialize(filename) is Triangle serializedTriange)){
                throw new BadFileException();
            }
            Clone(serializedTriange);
        }
        public void BinSerialize(FileStream filename){
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(filename, this);
        }
        public void BinDeserialize(FileStream filename){
            BinaryFormatter formatter = new BinaryFormatter();
            if (!(formatter.Deserialize(filename) is Triangle triangle)){
                throw new BadFileException();
            }
            Clone(triangle);
        }
        public void DBSerialize(string connectionString, int id){
            using (SqlConnection connection = new SqlConnection(connectionString)){
                string query = $"INSERT INTO SerializeDB.dbo.Triangles (Id,A,B,C) VALUES ({id}, '{A.X},{A.Y}', '{B.X},{B.Y}', '{C.X},{C.Y}')";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            };
        }
        public void DBDeserialize(string connectionString, int id){
            using (SqlConnection connection = new SqlConnection(connectionString)){
                string query = $"SELECT A,B,C FROM SerializeDB.dbo.Triangles WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Vector2D[] points = new Vector2D[3];
                for (var i = 0; i < 3; i++){
                    string coordinates = reader[i].ToString();
                    string[] xy = coordinates.Split(',');
                    bool xWasParsed = float.TryParse(xy[0], out float x);
                    bool yWasParsed = float.TryParse(xy[1], out float y);
                    if (xy.Length != 2 || !xWasParsed || !yWasParsed){
                        throw new WrongDbData();
                    }
                    points[i] = new Vector2D(x, y);
                }
                Clone(new Triangle(points[0], points[1], points[2]));
            };
        }
        public static bool operator !=(Triangle left, Triangle right){
            if (left.A == right.A && left.B == right.B && left.C == right.C){
                return false;
            }
            else{
                return true;
            }
        }
        public static bool operator ==(Triangle left, Triangle right){
            if (left.A == right.A && left.B == right.B && left.C == right.C){
                return true;
            }
            else{
                return false;
            }
        }
    }
}