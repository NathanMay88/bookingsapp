using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FrameWork.Utilities
{
    class Logger
    {
        private string LogPath { get; private set; }

        public Logger(string LogFilePath)
        {
            this.LogPath = LogFilePath;
        }

        public void Log(string message)
        {
           
        }
    }
}
