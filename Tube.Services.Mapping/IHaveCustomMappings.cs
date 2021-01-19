using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Tube.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
