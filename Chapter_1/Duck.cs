using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{


    class Duck : IDuck
    {
        public string StrDisplay { get; set; }
        public IFlyBehavior fly = new FlyNoWay();
        public IQuackBehavior quack = new MuteQuack();
        public ISwimBehavior swim = new SinkInWater();
        public Duck()
        {
            Console.Write("\ninit Duck ");
            StrDisplay = "I am a duck";
        }
        public string Display()
        {
            return StrDisplay;
        }
        public void PrintDuckInfo()
        {

            Console.WriteLine($"\n{this.Display()}\n{this.fly.Fly()}\n{this.swim.Swim()}\n{this.quack.Quack()}");
        }

    }

    class MallardDuck : Duck
    {
        public MallardDuck()
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
        public RubberDuck()
        {
            Console.Write("init RubberDuck ");
            StrDisplay = "I'm a rubber duck";
            this.quack = new Squeak();
            fly = new FlyNoWay();
        }
    }
    class WoodenDuck : Duck
    {
        public WoodenDuck()
        {
            Console.Write("init WoodenDuck ");
            StrDisplay = "I'm a wooden duck";
        }
    }
}
