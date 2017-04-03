﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motiv.Client.Interfaces
{
    public interface ICharMapper
    {
        void AddToMap(dynamic key, dynamic value);
        dynamic GetMapping();
        void display(bool isDesending);
    }
}
