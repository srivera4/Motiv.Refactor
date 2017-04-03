
using Motiv.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motiv.Client
{
    public class CharMapperFactory 
    {
        public ICharMapper Create<t>()
            where t : ICharMapper, new()
        {
            return new t();
        }

    }
}
