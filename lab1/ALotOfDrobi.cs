using System;
using System.Collections.Generic;
using System.IO;

namespace bog{
    public class ALotOfDrobi{
        bool created;
        public bool Created{
            private set => created = value;
            get => created;
        }
        List<Drobi> set;
        public List<Drobi> Set{
            get => set;
            protected set => set = value;
        }  

        Dictionary<int, int> bigger = new Dictionary<int, int>();
        Dictionary<int, int> smaller = new Dictionary<int, int>();
        Drobi maxDrob;
        Drobi minDrob;

        protected bool change;

        public ALotOfDrobi(){
            maxDrob = null;
            minDrob = null;
            change = false;
            created = true;
            Set = new List<Drobi>();
        }
        public ALotOfDrobi(StreamReader sr){
            maxDrob = null;
            minDrob = null;
            change = false;
            string input;
            Set = new List<Drobi>();
            try{
                input = sr.ReadToEnd();
            }
            catch(System.Exception exc){
                created = false;
                return;
            }
            char[] delimiterChars = { ' ', ':', '\r', '\n', '/', '\\' };
            string[] splitedFractions = input.Split(delimiterChars);
            int lastElementNumber = splitedFractions.Length - splitedFractions.Length % 2;
            for (var i = 0; i < lastElementNumber - 1; i += 2)
            {
            int firstnumber = 0;
            int secondnumber = 0;
            if (!Int32.TryParse(splitedFractions[i], out firstnumber))
                {
                    System.Console.WriteLine("Bad symbol in file!");
                    continue;
                }
                else
                {
                    if (!Int32.TryParse(splitedFractions[i + 1], out secondnumber))
                    {
                        System.Console.WriteLine("Bad symbol in file!");
                        continue;
                    }

                    if(firstnumber == 0){
                        continue;   
                    }
                    set.Add(new RationalFraction(firstnumber, secondnumber));
                }
            }
             created = true;
        }
         public int countHigher(Drobi drob){
             int count = 0;
             if(!change){
                 
             }
         }
    }
}