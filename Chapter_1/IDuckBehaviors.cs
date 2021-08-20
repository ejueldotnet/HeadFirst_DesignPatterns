using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{
    public interface IFlyBehavior
    {
        public bool CanFly { get; }
        public string Fly();
    }
    public interface IQuackBehavior
    {
        bool CanMakeNoise { get; }
        public string Quack();
    }
    public interface ISwimBehavior
    {
        bool CanSwim { get; }
        public string Swim();
    }
}

