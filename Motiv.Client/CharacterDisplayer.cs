using Motiv.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motiv.Client
{

    public class CharacterDisplayer : IDisplayer
    {
        private ICharMapper Mapper;
        public CharacterDisplayer(ICharMapper mapper)
        {
            this.Mapper = mapper;
        }
        //ran out of time So im just piggy backing off of ICharMapper.display....should move out into its own concrete classes
        public void Display(bool isDescending)
        {
            this.Mapper.display(isDescending);
        }
    }
}
