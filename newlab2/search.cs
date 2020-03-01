using System;

namespace newlab2{
    public class SearchEngine{
       public int year;
       public katalogTypes type;
       public string genre;
       public string name;
       public SearchEngine(string name = null, katalogTypes type = katalogTypes.all, string genre = null, int year = -1){
           this.name = name;
           this.type = type;
           this.genre = genre;
           this.year = year;
       }
    }

}