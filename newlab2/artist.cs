using System.Collections.Generic;

namespace newlab2{
    public class Artist : KatalogItem{
        List<Album> albums;
        string genre;
        public string Genre{
            get => genre;
        }
        public Artist(string name, string genre) : base(name, katalogTypes.artist){
            albums = new List<Album>();
            this.genre = genre;
        }
        public Artist(string name) : base(name, katalogTypes.artist){
            albums = new List<Album>();
        }
        public void AddAlbum(ref Album album){
            if (album.Genre != null)
                this.albums.Add(album);
            else{
                album.Genre = this.genre;
            }    
        }
        public override string ToString(){
            return $"[Artist]: {Name.ToString()} ";
        }
    }
}