namespace newlab2{
    public class song  : KatalogItem{
        Album album;
        public Album Album{
            get => album;
        }
        Artist artist;
        public Artist Artist{
            get => artist;
        }
        string genre;
        public string Genre{
            get => genre;
        }
        int year;
        public int Year{
            get => year;
            private set => year = value;
        }
        public song(string name) : base(name, katalogTypes.song){

        }
        public song(string name, Artist artist, Album album, string genre) : base(name, katalogTypes.song){
            this.artist = artist;
            this.genre = genre;
            this.album = album;
        }
        public song(int year, string name, Artist artist, Album album, string genre) : base(name, katalogTypes.song){
            this.year = year;
            this.artist = artist;
            this.genre = genre;
            this.album = album;
        }
        public override string ToString(){
            string str = "";
            if (artist != null){
                str += $"{artist.ToString()}";
            }
            if (album != null){
                str += $"{artist.ToString()}";
            }
            return str += $"[Song:]: {Name.ToString()}";
        }
    }
}