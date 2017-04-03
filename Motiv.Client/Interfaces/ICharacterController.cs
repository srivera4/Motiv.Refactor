using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motiv.Client.Interfaces
{
    public interface ICharacterController
    {
        IDisplayer CreateHighestOccurrPerRow();
        IDisplayer CreateLowestOccurrPerRow();
        IDisplayer CreateMaxCharCountInList();
        IDisplayer CreateMinCharCountInList();
        IDisplayer CreateAllNonNumeric();
        IDisplayer CreateAllNumeric();
    }
}
