﻿using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Realizations
{
    public class FieldService : IFieldService
    {
        public string GetFields()
        {
            return "Fields";
        }
    }
}
