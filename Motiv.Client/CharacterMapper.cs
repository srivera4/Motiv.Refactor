using System;
using System.Collections.Generic;
using Motiv.Client.Extensions;
using Motiv.Client.Interfaces;

namespace Motiv.Client
{
    public class CharacterMapper : ICharMapper
    {
        private Dictionary<char, int> CharMapping;

        public CharacterMapper()
        {
            this.CharMapping  = new Dictionary<char, int>();
        }

        public void AddToMap(dynamic key,dynamic count)
        {
            if (CharMapping.ContainsKey(key))
            {
                CharMapping[key] += count;
            }
            else
            {
                CharMapping.Add(key, count);
            }                
        }

        public dynamic GetMapping()
        {
            return CharMapping;
        }
    
        //ran out of time i created a IDisplayer interface so logic should move into concrete class there
        public void display(bool isDescending)
        {
            var orderedQuery = CharMapping
                .OrderByWithDirection(x => x.Key, isDescending);

            foreach (var item in orderedQuery)
            {
                Console.WriteLine("Character: {0} Count: {1}", item.Key, item.Value);
            }

            Console.WriteLine("");
        }
    }
}
