using System;
using System.Collections.Generic;
using Motiv.Client.Interfaces;

namespace Motiv.Client
{
    public class DisplayerFactory : ICharacterController
    {
        private CharacterRecognition CharRecognition;
        private Dictionary<int, List<CharInfo>> UnOrderedDictionary;

        public DisplayerFactory(CharacterRecognition charRecognition)
        {
            this.CharRecognition = charRecognition;
            this.UnOrderedDictionary = charRecognition.GetCharDict(true);
        }

        public IDisplayer CreateAllNonNumeric()
        {
            //I seperated out numerical and nonnumerical to have more flexibilty could easy add them to same function
            var nonNumericList = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => !char.IsDigit(x));

            return new CharacterDisplayer(nonNumericList);
        }

        public IDisplayer CreateAllNumeric()
        {
            //I seperated out numerical and nonnumerical to have more flexibilty could easy add them to same function
            var nonNumericList = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => char.IsDigit(x));

            return new CharacterDisplayer(nonNumericList);
        }

        public IDisplayer CreateHighestOccurrPerRow()
        {
            return new CharacterDisplayer(CharRecognition.CharcterCountPerRow(UnOrderedDictionary, true));
        }

        public IDisplayer CreateLowestOccurrPerRow()
        {
            return new CharacterDisplayer(CharRecognition.CharcterCountPerRow(UnOrderedDictionary, false));
        }

        public IDisplayer CreateMaxCharCountInList()
        {
            //Join numerical nonnumeral and get Max-------refactor to change code duplication with method below
            var mapperNumbers = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => char.IsDigit(x));
            var mapperLetters = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => !char.IsDigit(x));
 
            var charMapper = CharRecognition.GetMinOrMaxAllLines(mapperLetters.GetMapping(), mapperNumbers.GetMapping(),true);

            return new CharacterDisplayer(charMapper);
        }

        public IDisplayer CreateMinCharCountInList()
        {
            //Join numerical nonnumeral and get Min occur 
            var mapperNumbers = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => char.IsDigit(x));
            var mapperLetters = CharRecognition.GetCountInAllLines(UnOrderedDictionary, x => !char.IsDigit(x));

            var charMapper = CharRecognition.GetMinOrMaxAllLines(mapperLetters.GetMapping(), mapperNumbers.GetMapping(),false);

            return new CharacterDisplayer(charMapper);
        }
    }
}
