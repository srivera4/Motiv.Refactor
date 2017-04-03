using System;
using System.Collections.Generic;
using Motiv.Client.Extensions;
using Motiv.Client.Interfaces;
using System.Linq;

namespace Motiv.Client
{
    public class CharacterMapper : ICharMapper
    {
        //private Dictionary<char, int> CharMapping;
        private List<CharInfo> CharInfoList;

        public CharacterMapper()
        {
            this.CharInfoList = new List<CharInfo>();
            //this.CharMapping  = new Dictionary<char, int>();
        }

        public void AddToMap(dynamic key,dynamic count)
        {
            var charInfo = CharInfoList
                .Where(x => x.Character == key)
                .FirstOrDefault();

            if ( charInfo == null)
            {
                CharInfoList.Add(new CharInfo(key,count));
            }
            else
            {
                charInfo.Count += count;                                
            }     
        }

        public dynamic GetMapping()
        {
            return CharInfoList;
        }
    
        //ran out of time i created a IDisplayer interface so logic should move into concrete class there
        public void display(bool isDescending)
        {
            var orderedQuery = CharInfoList
                .OrderByWithDirection(x => x.Character, isDescending);

            foreach (var item in orderedQuery)
            {
                Console.WriteLine("Character: {0} Count: {1}", item.Character, item.Count);
            }

            Console.WriteLine("");
        }
    }
}
