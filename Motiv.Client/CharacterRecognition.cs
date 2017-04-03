using System;
using System.Collections.Generic;
using System.Linq;
using Motiv.Client.Extensions;
using Motiv.Client.Interfaces;

namespace Motiv.Client
{
    public class CharacterRecognition
    {
        //Not enough time to refactor for DRY and solid.....
        private List<string> StringList;
        private CharMapperFactory MapperFactory;  
        public CharacterRecognition(List<string> stringList, CharMapperFactory mapperFactory)
        {
            this.StringList = stringList;
            this.MapperFactory = mapperFactory;
        }

        /// <summary>
        /// Map list of string to dictionary so we can iterate on a row by row basis
        /// </summary>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public Dictionary<int, List<CharInfo>> GetCharDict(bool isDescending)
        {
            var dictMap = new Dictionary<int, List<CharInfo>>();
            int countKey = 0;

            foreach (var item in StringList)
            {
                var query = item
                    .GroupBy(x => x)
                    .Select(x => new { c = x.Key, count = x.Count() });

                var UnOrderedMap = new List<CharInfo>();

                foreach (var charItem in query)
                {
                    UnOrderedMap.Add(new CharInfo(charItem.c, charItem.count));
                }

                dictMap.Add(countKey, UnOrderedMap);
                countKey++;
            }
            return dictMap;
        }

        /// <summary>
        /// Get Max or Min Character count per row
        /// </summary>
        /// <param name="charDict"></param>
        /// <param name="MaxOrMinCount"></param>
        /// <returns></returns>
        public ICharMapper CharcterCountPerRow(Dictionary<int,List<CharInfo>> charDict, bool isMax)
        {
            var mapper = MapperFactory.Create<CharacterRowMapper>();
              
            foreach (var itemList in charDict)
            {
                var listChar = new List<CharInfo>();

                var maxOrMin = itemList.Value
                    .MaxOrMin(x => x.Count, isMax);

                var charInfoList = itemList.Value
                    .Where(x => x.Count == maxOrMin)
                    .ToList();

                mapper.AddToMap(itemList.Key, charInfoList);                            
            }
            return mapper;
        }

        /// <summary>
        /// Get Count of all characters provided in list
        /// </summary>
        /// <param name="charDict"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ICharMapper GetCountInAllLines(Dictionary<int, List<CharInfo>> charDict,Predicate<char> predicate)
        {
            var mapper = MapperFactory.Create<CharacterMapper>();

            foreach (var itemList in charDict)
            {
                var charInfoList = itemList.Value
                    .Where(x => predicate(x.Character))
                    .ToList();

                foreach (var item in charInfoList)
                {
                    mapper.AddToMap(item.Character, item.Count);
                }
            }

            return mapper;
        }

        /// <summary>
        /// Get Max or Min count from all characters provided in list
        /// </summary>
        /// <param name="charDict"></param>
        /// <param name="numDict"></param>
        /// <param name="MaxOrMinCount"></param>
        /// <returns></returns>
        public ICharMapper GetMinOrMaxAllLines(Dictionary<char, int>  charDict, Dictionary<char, int> numDict, bool isMax)
        {
            var mapper = MapperFactory.Create<CharacterMapper>();

            var joinedList = charDict
                .Concat(numDict);

            var maxOrMin = joinedList
                 .MaxOrMin(x => x.Value, isMax);

            var charInfoList = joinedList
                .Where(x => x.Value == maxOrMin)
                .ToList();

            foreach (var item in charInfoList)
            {
                mapper.AddToMap(item.Key, item.Value);
            }

            return mapper;

            //Since we have an OrderBy best possible runtime is O(n log n) Create Aggregate linq extension to reduce to O(N)
            //var query = charDict
            //    .Concat(numDict)
            //    .OrderByWithDirection(x => x.Value, MaxOrMinCount.Item2)
            //    .ToDictionary(x => x.Key, x => x.Value);

            //int count = 0;
            //var dictionary = new Dictionary<string, int>();

            //foreach (var item in query)
            //{
            //    if (MaxOrMinCount.Item1(item.Value,count))
            //    {
            //        mapper.AddToMap(item.Key, item.Value);
            //        count = item.Value;
            //    }
            //}
            //return mapper;
        }
    }
}
