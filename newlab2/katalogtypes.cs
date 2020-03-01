namespace newlab2{
    public enum katalogTypes{
        year,
        artist,
        song,
        genre,
        album,
        compilation,
        all
    }
    public class KatalogItem{
        string name;
        public string Name{
            get => name;
            private set => name = value;
        }
        katalogTypes type;
        public katalogTypes Type{
            get => type;
            set => type = value;
        }
        public override string ToString(){
            return $"{this.name}";
        }
        public override int GetHashCode(){
            return name.ToLower().GetHashCode();
        }
        public KatalogItem(string name, katalogTypes type){
            this.name = name;
            this.type = type;
        }
    }
}