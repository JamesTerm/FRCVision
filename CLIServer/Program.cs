using System;
using VisionServer;

namespace CLIServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server(@"10.34.81.2", @"VISION_2019", 0, 0, 120, 480, 600, true);
            Console.ReadLine();
        }
    }
}
