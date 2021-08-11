using HeadFirst_DesignPatterns.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Chapter_1
{
    class ChapterOne : IProofOfConcept
    {
        public string parentDir = "";
        public ChapterOne(string dir)
        {
            parentDir = dir;
        }

        public string ConceptName()
        {
            return "HF - Chapter 1";
        }

        public string ParentDir()
        {
            return parentDir;
        }

        public int RunDemo()
        {
            Chapter1Manager Ch1 = new Chapter1Manager(ParentDir());
            return Ch1.Run(AppSettings.defaultOption_Ch1);
        }
    }

    class Chapter1Manager : ConceptManager
    {
        public Chapter1Manager(string parentDir) : base(parentDir + "/Chapter 1")
        {
            //Each new proof of concept should inherit the interface IProofOfConcept
            Console.WriteLine($"Building proof of concepts for {ManagerName}...");
            proofOfConcepts.Add(new SimUDuck());
        }
    }
}
