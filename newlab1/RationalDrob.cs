using System;

namespace newlab1
{
    public class RationalDrob
    {
       int m;
       int n;
       public RationalDrob(int m, int n)
       {
           this.m = m;
           this.n = n;
        }
        public override string ToString(){
            return $"{m}/{n}";
        }
        public static bool operator >(RationalDrob firstFraction, RationalDrob secondFraction){
            bool result;
            if((firstFraction.m * secondFraction.n) > (secondFraction.m * firstFraction.n)){
                result = true;
            }
            else{
                result = false;
            }
            return result;
        }
        public static bool operator <(RationalDrob firstFraction, RationalDrob secondFraction){
            bool result;
            if((firstFraction.m * secondFraction.n) < (secondFraction.m * firstFraction.n)){
                result = true;
            }
            else{
                result = false;
            }
            return result;
        }
        public static RationalDrob operator +(RationalDrob firstFraction, RationalDrob secondFraction){
            int resultM, resultN;
            resultN = firstFraction.m * secondFraction.m;
            resultM = firstFraction.m * secondFraction.n + firstFraction.n * secondFraction.m;
            return new RationalDrob(resultM, resultN);
        }
    }
}
