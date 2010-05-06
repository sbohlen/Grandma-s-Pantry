using System;

/*
 * ********************************************
 * YOU DO NOT NEED TO EDIT THIS FILE AT ALL
 * ********************************************
 */


namespace Grandma.Application
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
