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
            List<Duck> ducks = new List<Duck>
            {
                new Duck(),
                new MallardDuck(),
                new RedheadDuck(),
                new RubberDuck(),
                new WoodenDuck()
            };

            foreach (Duck duck in ducks)
            {
                duck.PrintDuckInfo();
            }

            return 0;
        }
    }
}
