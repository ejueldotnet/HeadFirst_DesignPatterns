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

            foreach (Duck duck in ducks)
            {
                duck.printDuckInfo();
            }

            return 0;
        }
    }

    class Duck : IDuck
    {
        public string StrDisplay { get; set; }
        public string StrFly { get; set; }
        public string StrQuack { get; set; }
        public string StrSwim { get; set; }

        public Duck()
        {
            Console.Write("init Duck ");
            StrDisplay = "I am a duck";
            StrFly = "I can fly";
            StrSwim = "I can swim";
            StrQuack = "I can quack";
        }
        public string Display()
        {
            return StrDisplay;
        }

        public string Fly()
        { 
            return StrFly;
        }
        public string Swim()
        {
            return StrSwim;
        }

        public string Quack()
        {
            return StrQuack;
        }
        public void printDuckInfo()
        {

            Console.WriteLine($"\n{this.Display()}\n{this.Fly()}\n{this.Swim()}\n{this.Quack()}");
        }

    }

    class MallardDuck : Duck
    {
        public MallardDuck() : base()
        {
            Console.Write("init MallardDuck ");
            StrDisplay = "I'm a mallard";
        }
    }

    class RedheadDuck : Duck
    {
        public RedheadDuck()
        {
            Console.Write("init RedheadDuck ");
            StrDisplay = "I'm a red headed duck";
        }
    }
    class RubberDuck : Duck
    {
        public RubberDuck() : base()
        {
            Console.Write("init RubberDuck ");
            StrDisplay = "I'm a rubber duck";
            StrQuack = "I can squeak";
            StrFly = "I can't fly";
        }
    }
}
