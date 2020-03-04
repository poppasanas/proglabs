using System;
using System.IO;
using System.Collections.Generic;

namespace newlab3{
    public class ConfigReader{
        Dictionary<string, Section> configs = new Dictionary<string, Section>();
        public ConfigReader(StreamReader configFile){
            string line;
            int indexBufer;
            char[] sectionSeparators = { '[', ']', ' ', '/', '\\', '.' };
            char[] configSeparators = { '=', ' ', };
            string[] section = { };
            bool sectionCorrect = true;
            while ((line = configFile.ReadLine()) != null){
                indexBufer = line.IndexOf('[');
                if (indexBufer != -1 && line.IndexOf(']', indexBufer) != -1){
                    section = line.Split(sectionSeparators, StringSplitOptions.RemoveEmptyEntries);
                    if (section.Length != 1){
                        sectionCorrect = false;
                        Console.WriteLine("Wrong section name");
                        continue;
                    }
                    else{
                        sectionCorrect = true;
                        configs.Add(section[0], new Section(section[0]));
                        continue;
                    }
                }

                string[] config;
                if (sectionCorrect){
                    indexBufer = line.IndexOf(';');
                    if (indexBufer == 0){
                        continue;
                    }
                    if (indexBufer != -1){
                        line = line.Substring(0, indexBufer);
                    }
                    config = line.Split(configSeparators, StringSplitOptions.RemoveEmptyEntries);
                    if (config.Length != 2){
                        Console.WriteLine("Wrong config string");
                        continue;
                    }
                    else{
                        if (configs.TryGetValue(section[0], out Section configsOut)){
                            configsOut.Add(config[0], config[1]);
                        }
                    }
                }
            }
        }
        public Section GetSection(string name){
            if(configs.TryGetValue(name,out Section section)){
                return section;
            }
            else{
                throw new NotFoundException("Section Not Found");
            }
        }
    }
}