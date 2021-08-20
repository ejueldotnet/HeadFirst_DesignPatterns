using HeadFirst_DesignPatterns.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{
    class SimUDuck : IProofOfConcept
    {
        public string ConceptName()
        {
            return "Ch1 - SimUDuck";
        }

        public string ParentDir()
        {
            throw new NotImplementedException();
        }

        public int RunDemo()
        {
            List<Duck> ducks = new List<Duck>();
            ducks.Add(new Duck());
            Console.WriteLine("");
            ducks.Add(new MallardDuck());
            Console.WriteLine("");
            ducks.Add(new RedheadDuck());
            Console.WriteLine("");
            ducks.Add(new RubberDuck());
            Console.WriteLine("");
            ducks.Add(new WoodenDuck());
            Console.WriteLine("");

            foreach (Duck duck in ducks)
            {
                duck.printDuckInfo();
            }

            return 0;
        }
    }
}
