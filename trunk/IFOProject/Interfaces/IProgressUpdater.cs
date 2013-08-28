using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFOProject
{
    public interface IProgressUpdater
    {
        void SetProgress(int percents);
    }
}
