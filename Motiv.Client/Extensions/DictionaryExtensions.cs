using System;
using System.Collections.Generic;

namespace Motiv.Client.Extensions
{
    public static class DictionaryExtensions
    {
        public static void DisplayCharacterCount(this Dictionary<int, List<CharInfo>> charDict)
        {
            foreach (var item in charDict)
            {
                Console.WriteLine("Line {0} \n", item.Key, item.Value.Count);
                foreach (var listItem in item.Value)
                {
                    Console.WriteLine("Character: {0} Count: {1}", listItem.Character, listItem.Count);
                }
                Console.WriteLine("");
            }
        }
    }
}
