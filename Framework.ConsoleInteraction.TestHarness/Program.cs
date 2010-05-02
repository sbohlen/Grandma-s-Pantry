using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ConsoleInteraction.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Logo.Print();

            InteractiveConsole console = new InteractiveConsole();
            console.Run();
        }
    }
}
