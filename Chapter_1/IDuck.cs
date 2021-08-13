using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{
    public interface IDuck
    {
        public string StrDisplay
        {
            get; set;
        }
        public string StrFly
        {
            get; set;
        }
        public string StrQuack
        {
            get; set;
        }
        public string StrSwim
        {
            get; set;
        }
        string Display();
        string Fly();
        string Quack();
        string Swim();
        //Other duck-like methods...
    }
}
