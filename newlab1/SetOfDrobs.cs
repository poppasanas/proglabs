using System;
using System.Collections.Generic;
using System.IO;


namespace newlab1
{
    public class SetOfDrobi
    {
        bool created;
        public bool Created{
            private set => created = value;
            get => created;
        }
        List<RationalDrob> set;
        public List<RationalDrob> Set{
            get => set;
            protected set => set = value;
        }
        Dictionary<int, int> moreThan = new Dictionary<int, int>();
        Dictionary<int, int> lessThan = new Dictionary<int, int>();
        RationalDrob HighestDrob;
        RationalDrob LowestDrob;

        public SetOfDrobi()
        {
            HighestDrob = null;
            LowestDrob = null;
            Set = new List<RationalDrob>();
            created = true;
        }

        public SetOfDrobi(StreamReader sr)
        {
            HighestDrob = null;
            LowestDrob = null;
            string input;
            try
            {
                input = sr.ReadToEnd();
            }
            catch
            {
                created = false;
                return;
            }
            Set = new List<RationalDrob>();
            char[] DelimeterChars = {' ', ';','\r','\n','/','\\'};
            string[] splittedFractions = input.Split(DelimeterChars);
            int lastElementNumber = splittedFractions.Length - splittedFractions.Length % 2;
            for (var i = 0; i < lastElementNumber - 1; i+= 2){
                int m = 0;
                int n = 0;
                if (!Int32.TryParse(splittedFractions[i], out m)){
                    System.Console.Write("nah, bad line mate, ");
                    continue;
                }
                else{
                    if(!Int32.TryParse(splittedFractions[i+1], out n)){
                        System.Console.Write("nah, bad line mate; ");
                        continue;
                    }
                    if(n == 0){
                        continue;
                    }
                    set.Add(new RationalDrob(m, n));
                }
            }
            created = true;
        }

        public int countMoreThan(RationalDrob drob)
        {
            int count = 0;
            foreach(var fract in this.set)
            {
                if (fract > drob)
                {
                    count++;
                }
            }
            moreThan.Add(drob.GetHashCode(), count);
            return count;
        }
        public int countLessThan(RationalDrob drob){
            int count = 0;
            foreach(var fract in this.Set){
                if(fract < drob){
                    count++;
                }
            }
            lessThan.Add(drob.GetHashCode(), count);
            return count;       
        }
        public void add(RationalDrob drob){
            if(HighestDrob != null && LowestDrob != null){
                set.Add(drob);
                if(drob < LowestDrob){
                    LowestDrob = drob;
                }
                if(drob > HighestDrob){
                    HighestDrob = drob;
                }
            }
            else
            {
                set.Add(drob);
                HighestDrob = drob;
                LowestDrob = drob;
            }
        }
        
    }
}
