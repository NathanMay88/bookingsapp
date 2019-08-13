using System;
using System.Collections.Generic;
using System.Text;

namespace FrameWork.Models
{
    interface IViewModel
    {
        private FrameWork.Utilities.Logger Logger { get; }
        public void Log();
    }
}
