using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{
    class FlyWithWings : IFlyBehavior
    {
        public bool CanFly => true;

        public string Fly()
        {
            return "I can fly";
        }
    }
    class FlyNoWay : IFlyBehavior
    {
        public bool CanFly => false;

        public string Fly()
        {
            return "I can't fly";
        }
    }

    class Quack : IQuackBehavior
    {
        public bool CanMakeNoise => true;

        string IQuackBehavior.Quack()
        {
            return "I can quack";
        }
    }

    class Squeak : IQuackBehavior
    {
        public bool CanMakeNoise => true;

        string IQuackBehavior.Quack()
        {
            return "I can squeak";
        }
    }

    class MuteQuack : IQuackBehavior
    {
        public bool CanMakeNoise => false;

        string IQuackBehavior.Quack()
        {
            return "I can't quack";
        }
    }
    class Swim : ISwimBehavior
    {
        public bool CanSwim => true;

        string ISwimBehavior.Swim()
        {
            return "I can swim";
        }
    }
    class SinkInWater : ISwimBehavior
    {
        public bool CanSwim => false;

        string ISwimBehavior.Swim()
        {
            return "I can't swim";
        }
    }
}
