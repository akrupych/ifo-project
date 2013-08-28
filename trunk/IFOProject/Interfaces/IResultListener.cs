using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFOProject.DataStructures;

namespace IFOProject.Interfaces
{
    public interface IResultListener
    {
        void ProcessResult(Coefficients coefficients);
    }
}
