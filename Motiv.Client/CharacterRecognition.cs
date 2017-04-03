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
                var charInfoList = GetListWithMaxOrMin(itemList.Value, isMax);

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
        /// <param name="charInfo"></param>
        /// <param name="numList"></param>
        /// <param name="MaxOrMinCount"></param>
        /// <returns></returns>
        public ICharMapper GetMinOrMaxAllLines(List<CharInfo> nonNumList, List<CharInfo> numList, bool isMax)
        {
            var mapper = MapperFactory.Create<CharacterMapper>();

            List<CharInfo> joinedList = nonNumList
                .Concat(numList)
                .ToList();

            var charInfoList = GetListWithMaxOrMin(joinedList, isMax);

            foreach (var item in charInfoList)
            {
                mapper.AddToMap(item.Character, item.Count);
            }

            return mapper;
        }

        private List<CharInfo> GetListWithMaxOrMin(List<CharInfo> charInfoList, bool isMax)
        {
            var maxOrMin = charInfoList
                            .MaxOrMin(x => x.Count, isMax);

            return charInfoList
                    .Where(x => x.Count == maxOrMin)
                    .ToList();
        }
    }
}
