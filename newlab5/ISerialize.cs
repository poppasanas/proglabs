using System.IO;

namespace newlab5{
    public interface ISerialize{
        public void XmlSerialize(StreamWriter filename);
        public void XmlDeserialize(StreamReader filename);
        public void BinSerialize(FileStream filename);
        public void BinDeserialize(FileStream filename);
    }
}