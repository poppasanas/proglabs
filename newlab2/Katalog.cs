using System;
using System.Collections.Generic;
using System.IO;

namespace newlab2{
    public class Katalog{
        Dictionary<int, Artist> artists = new Dictionary<int, Artist>();
        Dictionary<int, Album> albums = new Dictionary<int, Album>();
        Dictionary<int, song> songs = new Dictionary<int, song>();

        Genre genres;
        public Katalog(StreamReader tracksFile, StreamReader genresFile){
            genres = new Genre(genresFile);
            try{
                string[] separator = { "-" };
                string line;
                while ((line = tracksFile.ReadLine()) != null)
                {
                    string[] infoFromFile = line.Split(separator, StringSplitOptions.None);
                    for (var i = 0; i < infoFromFile.Length; i++)
                    {
                        infoFromFile[i] = infoFromFile[i].Trim(' ');
                    }
                    if (infoFromFile.Length < 3 || infoFromFile[0] == "" || infoFromFile[2] == "")
                    {
                        continue;
                    }

                    string genre = infoFromFile[3] == "" ? "none" : infoFromFile[3];
                    Artist artist = new Artist(infoFromFile[0], genre);
                    Album album;
                    if (infoFromFile[1] == "")
                    {
                        infoFromFile[1] = infoFromFile[2];
                    }
                    if (albums.TryGetValue(infoFromFile[1].ToLower().GetHashCode(), out var outAlbum))
                    {
                        if (outAlbum.Artist.Name.ToLower() != infoFromFile[0] ||
                         outAlbum.Artist.Name.ToLower() == "various artists")
                        {
                            artist.AddAlbum(ref outAlbum);
                            if (outAlbum.Artist.Name != "various artists")
                            {
                                outAlbum.Type = katalogTypes.compilation;
                                outAlbum.Artist = new Artist("various artists");
                            }
                        }
                        album = outAlbum;
                    }
                    else
                    {
                        album = new Album(infoFromFile[1], ref artist);
                        artist.AddAlbum(ref album);
                    }

                    int year;
                    Int32.TryParse(infoFromFile[4], out year);
                    song Song = new song(year, infoFromFile[2], artist, album, genre);
                    Addsong(Song);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }    
        }
        public void Addsong(song Song)
        {
            if (Song.Album == null || Song.Artist == null || Song.Genre == null)
            {
                Console.WriteLine("Bad song metainfo \nnot aded");
                return;
            }
            if (!this.songs.TryGetValue(Song.GetHashCode(), out var outSong))
            {
                AddAlbum(Song.Album);
                outSong = Song;
                if (this.albums.TryGetValue(Song.Album.GetHashCode(), out var outAlbum))
                {
                    outAlbum.songs.Add(Song);
                }
                this.songs.Add(Song.GetHashCode(), outSong);

            }
            else
            {
                Console.WriteLine("track already in catalogue");
            }
        }
        private void AddAlbum(Album album)
        {


            AddArtist(album.Artist);
            Album outAlbum;
            if (this.albums.TryGetValue(album.GetHashCode(), out outAlbum))
            {
                return;
            }
            else
            {
                Artist outArtist;
                if (this.artists.TryGetValue(album.Artist.GetHashCode(), out outArtist))
                {

                    album.Artist = outArtist;
                    outArtist.AddAlbum(ref album);
                }
                else
                {
                    System.Console.WriteLine("artist not found");
                }
                this.albums.Add(album.GetHashCode(), album);

            }
        }
        private void AddArtist(Artist artist)
        {
            Artist outArtist;
            if (!this.artists.TryGetValue(artist.GetHashCode(), out outArtist))
            {
                this.artists.Add(artist.GetHashCode(), artist);
            }
        }
        public void Search(SearchEngine options)
        {
            List<KatalogItem> result = new List<KatalogItem>();
            string genre = options.genre;
            int year = options.year;
            string name = options.name;
            if (name != null)
            {
                if (options.type == katalogTypes.genre && genres.Genres.Contains(name.ToLower()))
                {
                    Console.WriteLine("[GENRE]: " + genres.Genres.Find(x => x == name));
                }

                if ((options.type == katalogTypes.artist || options.type == katalogTypes.all) && year == -1)
                {
                    if (artists.TryGetValue(name.ToLower().GetHashCode(), out var artistsResultByWord))
                    {
                        System.Console.WriteLine(artistsResultByWord);
                    }
                }

                if (options.type == katalogTypes.album || options.type == katalogTypes.compilation ||
                 options.type == katalogTypes.all)
                {
                    if (albums.TryGetValue(name.ToLower().GetHashCode(), out var albumsResultByWord))
                    {
                        if ((genre == null || albumsResultByWord.Genre.ToLower() == genre.ToLower()
                        || genres.IsSmallgenre(genre, albumsResultByWord.Genre)) &&
                        (year == -1 || albumsResultByWord.Year == year))
                        {
                            if (options.type == albumsResultByWord.Type || options.type != katalogTypes.compilation)
                            { 
                                System.Console.WriteLine(albumsResultByWord); 
                            }
                        }
                    }
                }

                if (options.type == katalogTypes.song || options.type == katalogTypes.all)
                {
                    if (songs.TryGetValue(name.ToLower().GetHashCode(), out var tracksResultByWord))
                    {
                        if ((genre == null || tracksResultByWord.Genre.ToLower() == genre.ToLower()
                        || genres.IsSmallgenre(genre, tracksResultByWord.Genre)) &&
                        (year == -1 || tracksResultByWord.Year == year))
                        {
                            System.Console.WriteLine(tracksResultByWord);
                        }

                    }
                }
            }
            else
            {
                if (options.type == katalogTypes.artist || options.type == katalogTypes.all)
                {
                    foreach (var artist in artists)
                    {
                        if ((genre == null || artist.Value.Genre.ToLower() == genre.ToLower()
                        || genres.IsSmallgenre(genre, artist.Value.Genre)) && year == -1)
                        {
                            Console.WriteLine(artist.Value);
                        }
                    }
                }

                if (options.type == katalogTypes.album || options.type == katalogTypes.compilation ||
                 options.type == katalogTypes.all)
                {
                    foreach (var album in albums)
                    {
                        if ((genre == null || album.Value.Genre.ToLower() == genre.ToLower()
                        || genres.IsSmallgenre(genre, album.Value.Genre))
                        && (year == -1 || album.Value.Year == year))
                        {
                            if (options.type == album.Value.Type || options.type != katalogTypes.compilation)
                            { 
                                System.Console.WriteLine(album.Value); 
                            }
                        }
                    }

                    if (options.type == katalogTypes.song || options.type == katalogTypes.all)
                    {
                        foreach (var track in songs)
                        {

                            if ((genre == null || track.Value.Genre.ToLower() == genre.ToLower()
                            || genres.IsSmallgenre(genre, track.Value.Genre)) &&
                            (year == -1 || track.Value.Year == year))
                            {
                                System.Console.WriteLine(track.Value);
                            }
                        }

                    }
                }

        }
    }
    }
}