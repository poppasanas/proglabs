using System.Collections.Generic;
namespace newlab2{
    public class Album : KatalogItem{
        string genre;
        public string Genre{
            set => genre = value;
            get => genre;   
        }
        int year;
        public int Year{
            get => year;
            set => year = value;
        }
        Artist artist;
        public Artist Artist{
            get => artist;
            set => artist = value;
        }
        public List<song> songs = new List<song>();
        public Album(string name, ref Artist artist, katalogTypes type = katalogTypes.album) : base(name, type){
            this.artist = artist;
        }
        public Album(string name, string genre, ref Artist artist, katalogTypes type = katalogTypes.album) : base(name, type){
            this.genre = genre;
            this.artist = artist;
        }
        public Album(string name, string genre, ref Artist artist, List<song> songs, katalogTypes type = katalogTypes.album) : base(name, type){
            this.genre = genre;
            this.artist = artist;
            this.songs = songs;
        }
        public Album(int year, string name, string genre, ref Artist artist, List<song> songs, katalogTypes type = katalogTypes.album) : base(name, type){
            this.year = year;
            this.genre = genre;
            this.artist = artist;
            this.songs = songs;
        }
        override public string ToString(){
            return $"[ALBUM]: {Name.ToString()} ";
        }
    }
}