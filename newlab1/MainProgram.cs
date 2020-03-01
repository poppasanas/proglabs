using System;
using System.IO;

namespace newlab1{
    class MainProgram{
        static void Main(string[] args){
            Console.WriteLine("Start");
            SetOfDrobi set = new SetOfDrobi();
            set.add(new RationalDrob(1, 5));
            set.add(new RationalDrob(1, 9));
            set.add(new RationalDrob(1, 5));
            set.add(new RationalDrob(12, 5));
            set.add(new RationalDrob(1, 10));
            Console.WriteLine(set.countLessThan(new RationalDrob(1, 100)));
            Console.WriteLine(set.countMoreThan(new RationalDrob(1, 5)));
            Console.WriteLine(set.countLessThan(new RationalDrob(1, 100)));
            Console.WriteLine(set.countMoreThan(new RationalDrob(1, 100)));
            set.add(new RationalDrob(4, 32));
            set.add(new RationalDrob(1, 3));
            Console.WriteLine(set.countLessThan(new RationalDrob(1, 100)));
            Console.WriteLine(set.countMoreThan(new RationalDrob(1, 100)));
            Console.WriteLine(set.countMoreThan(new RationalDrob(1, 100)));

            StreamReader sr = new StreamReader("input.txt");
            SetOfDrobi setFromFile = new SetOfDrobi(sr);
            Polinom firstPolinom;
            if (setFromFile.Created)
            {
                Console.WriteLine(setFromFile.countLessThan(new RationalDrob(1, 100)));
                Console.WriteLine(setFromFile.countMoreThan(new RationalDrob(1, 100)));
                Console.WriteLine(setFromFile.countLessThan(new RationalDrob(1, 100)));
                Console.WriteLine(setFromFile.countMoreThan(new RationalDrob(1, 100)));
                firstPolinom = new Polinom(setFromFile);
            }
            else
            {
                firstPolinom = new Polinom(set);
            }


            Polinom secondPolinom = new Polinom(set);
            Polinom resultPolinom = firstPolinom + secondPolinom;
            string strPolinom = resultPolinom.ToString();
            Console.WriteLine(strPolinom);
        }
    }
}