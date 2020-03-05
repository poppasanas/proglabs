using System;

namespace newlab4
{
    class Program
    {
        static void Main()
        {
            newlab4.Tests.CsvTest.Start();
            newlab4.Tests.DbTest.Start();
            return;
            switch (Settings.Default.WorkerType)
            {
                case "CSV":
                    newlab4.Tests.CsvTest.Start();
                    break;
                case "DB":
                    newlab4.Tests.DbTest.Start();
                    break;
                default:
                    return;
            }
        }
    }
}
