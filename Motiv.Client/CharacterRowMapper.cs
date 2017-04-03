using Motiv.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motiv.Client
{
    public class CharacterRowMapper : ICharMapper
    {
        private Dictionary<int, List<CharInfo>> CharRowMapping;
        public CharacterRowMapper()
        {
            this.CharRowMapping = new Dictionary<int, List<CharInfo>>();
        }
        public void AddToMap(dynamic key, dynamic value)
        {
            CharRowMapping.Add(key,value);
        }

        //ran out of time i created a IDisplayer interface so logic should move into concrete class there
        public void display(bool isDesending)
        {
            foreach (var item in CharRowMapping)
            {
                Console.WriteLine("Line {0} \n", item.Key, item.Value.Count);
                foreach (var listItem in item.Value)
                {
                    Console.WriteLine("Character: {0} Count: {1}", listItem.Character, listItem.Count);
                }
                Console.WriteLine("");
            }
        }

        public dynamic GetMapping()
        {
            return CharRowMapping;
        }
    }
}
