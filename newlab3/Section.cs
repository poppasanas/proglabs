using System.Collections.Generic;
using System;
using System.Globalization;

namespace newlab3{
    public class Section{
        public string Name { get; }
        Dictionary<string, string> configs = new Dictionary<string, string>();
        public Section(string name){
            Name = name;
        }
        public void Add(string config, string value){
            configs.Add(config, value);
        }
        public string GetStringConfig(string ConfigName){
            string ParametrOut;
            if(configs.TryGetValue(ConfigName, out ParametrOut)){
                return ParametrOut;
            }
            else{
                throw new NotFoundException("Config not found");
            }
        }
        public int GetIntConfig(string ConfigName){
            string value = GetStringConfig(ConfigName);
            if(Int32.TryParse(value, NumberStyles.Number, new CultureInfo("en-US"), out int outInt)){
                return outInt;
            }
            else{
                throw new NotParsedException("Can not be parsed");
            }
        }
        public double GetDoubleConfig(string ConfigName){
            string value = GetStringConfig(ConfigName);
            if(Double.TryParse(value, NumberStyles.Number, new CultureInfo("en-US"), out double outInt)){
                return outInt;
            }
            else{
                throw new NotParsedException("Can not be parsed");
            }
        }
    }
}