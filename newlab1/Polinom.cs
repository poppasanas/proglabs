using System.Collections.Generic;

namespace newlab1{
    public class Polinom{
        public int countOfElements = 0;
        int constant = 0;

        RationalDrob[] polinom;
        public Polinom(SetOfDrobi set, int constant = 0){
            this.constant = constant;
            this.countOfElements = set.Set.Count;
            polinom = new RationalDrob[countOfElements];
            int i = 0;
            foreach (var fract in set.Set){
                polinom[i++] = fract;
            }
        }
        public Polinom(RationalDrob[] polinom, int constant = 0){
            this.polinom = polinom;
            this.countOfElements = polinom.Length;
        }
        public static Polinom operator +(Polinom firstPolinom, Polinom secondPolinom){
            int minLength, maxLength;
            Polinom biggerPolinom;
            if(firstPolinom.countOfElements > secondPolinom.countOfElements){
                maxLength = firstPolinom.countOfElements;
                minLength = secondPolinom.countOfElements;
                biggerPolinom = firstPolinom;
            }
            else{
                maxLength = secondPolinom.countOfElements;
                minLength = firstPolinom.countOfElements;
                biggerPolinom = secondPolinom;
            }
            RationalDrob[] resultPolinom = new RationalDrob[maxLength];
            for(var i = 0; i < minLength; i++){
                resultPolinom[i] = firstPolinom.polinom[i] + secondPolinom.polinom[i];
            }
            for(var i = minLength; i < maxLength; i++){
                resultPolinom[i] = biggerPolinom.polinom[i];
            }
            return new Polinom(resultPolinom);
        }
        public override string ToString()
        {
            
            List<string> elements = new List<string>();
            for (var i = 0; i < countOfElements; i++)
            {
                elements.Add($"{polinom[i]}x^{i + 1}");
            }
            return string.Join(" + ", elements);
        }
    }
}