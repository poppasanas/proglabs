using System;

namespace bog
{
    public class Drob
    {
        private int firstnumb, secondnumb;
        private float bothnumb;
        public Drob(int firstnumber, int secondnumber)
        {

            this.firstnumb = firstnumber;
            this.secondnumb = secondnumber;
            this.bothnumb = (firstnumber / secondnumber);
        }
        public int FirstNumb
        {
            private set => firstnumb = value;
            get => firstnumb;
        }
        public int SecondNumb
        {
            private set => secondnumb = value;
            get => secondnumb;
        }
        public float Bothnumb
        {
            private set => bothnumb = value;
            get => bothnumb;
        }
        public override string ToString()
        {
            return $"{firstnumb}/{secondnumb}";
        }
    }

}