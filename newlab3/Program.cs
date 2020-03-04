using System;
using System.IO;

namespace newlab3
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader configFile = new StreamReader("config.ini");
            ConfigReader cf = new ConfigReader(configFile);
            Console.WriteLine(cf.GetSection("ADC_DEV").GetStringConfig("SampleRate"));
            try{
                Console.WriteLine(cf.GetSection("ADC_DEV").GetIntConfig("SampleRate"));
            }
            catch (NotParsedException e){
                Console.WriteLine(e.Message);
            }
            try{
                Console.WriteLine(cf.GetSection("ABCD_DEV").GetIntConfig("BufferLenSeconds"));
            }
            catch (NotFoundException e){
                Console.WriteLine(e.Message);
            }  
            Console.WriteLine(cf.GetSection("NCMD").GetDoubleConfig("SampleRate"));
            Console.WriteLine(cf.GetSection("NCMD").GetStringConfig("SampleRate"));
            Console.WriteLine(cf.GetSection("NCMD").GetIntConfig("SampleRate"));
        }
    }
}
