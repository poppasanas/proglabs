using System.Collections.Generic;
using System.IO;
using System;
namespace newlab2{
    class Genre{
        private Dictionary<string, List<string>> smallgenres = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Smallgenres{
            get => smallgenres;
        }
        private List<string> genres = new List<string>();
        public List<string> Genres{
            get => genres;
        }
        public Genre(StreamReader sr){
            try{
                string[] separator = { "-" };
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] infoFromFile = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < infoFromFile.Length; i++)
                    {
                        infoFromFile[i] = infoFromFile[i].Trim(' ');
                    }
                    if (infoFromFile[0] == "")
                    {
                        continue;
                    }
                    List<string> listSmallgenres = new List<string>();
                    genres.Add(infoFromFile[0].ToLower());
                    for(var i = 1; i < infoFromFile.Length; i++){
                        listSmallgenres.Add(infoFromFile[i].ToLower());
                    }
                    smallgenres.Add(infoFromFile[0].ToLower(), listSmallgenres);
                }
            }
            catch(System.Exception e){
                Console.WriteLine("nah, bad file mate");
                Console.WriteLine(e.Message);
            }
        }
            public bool IsSmallgenre(string genre, string smallgenre){
                List<string> OutGenres;
                if(!smallgenres.TryGetValue(genre.ToLower(), out OutGenres)){
                    return false;
                }
            foreach(var item in OutGenres){
                if(item.ToLower() == smallgenre.ToLower()){
                    return true;
                }
                if(IsSmallgenre(item.ToLower(), smallgenre.ToLower())){
                    return true;
                }
            }
            return false;
        }
    } 
}