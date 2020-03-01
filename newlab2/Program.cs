using System;
using System.IO;

namespace newlab2
{
    class Program
    {
        static void Main(string[] args)
        {
        try
            {
                StreamReader tracksFile = new StreamReader("input.txt");
                StreamReader genresFile = new StreamReader("genres.txt");
                Katalog catalog = new Katalog(tracksFile, genresFile);
                catalog.Search(new SearchEngine(type: katalogTypes.album));
                Console.WriteLine();

                catalog.Search(new SearchEngine(type: katalogTypes.compilation));
                Console.WriteLine();

                catalog.Search(new SearchEngine("ASTROWORLD"));
                catalog.Search(new SearchEngine("Believer"));
                catalog.Search(new SearchEngine("imagine dragons"));
                catalog.Search(new SearchEngine("electroHELL"));
                Console.WriteLine();

                catalog.Search(new SearchEngine(name: "Believer",genre: "Rock"));
                catalog.Search(new SearchEngine(year: 2010));
                Console.WriteLine();
                
                catalog.Search(new SearchEngine(name: "hip hop", type: katalogTypes.genre));
            }
            catch (System.Exception e)
            {
                Console.WriteLine("File error: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
//we'll have for search: year, artist, songname, genre, smallgenre(поджанр), album, compilations(сборники)
//songs will go in that way ^
//for example: 2009 - The Quick Brown Fox - Gotsta Terrify - EDM - Speedcore - First Album - SpeedcoreMusic