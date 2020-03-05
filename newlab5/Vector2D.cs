using System;

namespace newlab5{
    [Serializable]
    public class Vector2D{
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2D(){}
        public Vector2D(float x, float y){
            X = x;
            Y = y;
        }
        public static bool operator !=(Vector2D left, Vector2D right){
            if (left.X == right.X && left.Y == right.Y){
                return false;
            }
            else{
                return true;
            }
        }
        public static bool operator ==(Vector2D left, Vector2D right){
            if (left.X == right.X && left.Y == right.Y){
                return true;
            }
            else{
                return false;
            }
        }
    }
}