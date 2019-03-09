using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGpsTracker.Interfaces
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
