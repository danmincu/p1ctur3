using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Types
{
    public interface ILogger
    {
        void Write(string message);
        void Write(Exception exception);
    }
}
