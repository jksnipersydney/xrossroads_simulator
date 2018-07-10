using System;

namespace XrossRoads
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to the XrossRoads program. Please enter an integer value for the number of levels you wish to simulate:");
            var levelInput = Console.ReadLine();

            var totalLevels = 0;

            while (totalLevels < 2)
            {
                if (!int.TryParse(levelInput, out totalLevels) || totalLevels < 2)
                {
                    Console.WriteLine("Entered value must be of type integer greater than 1");
                    levelInput = Console.ReadLine();
                }
            }

            var app = new XrossRoadsApp();
            app.Simulate(totalLevels);

            Console.ReadLine();
        }
    }
}
