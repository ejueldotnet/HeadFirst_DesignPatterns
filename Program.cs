using HeadFirst_DesignPatterns.Chapter_1;
using HeadFirst_DesignPatterns.Templates;
using HeadFirst_DesignPatterns.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            ProofOfConceptManager poc = new ProofOfConceptManager("root");
            int result = 0;
            do
            {
                result = poc.Run(AppSettings.defaultOption_Main);
            } while (result != 0);
        }
    }
    class ProofOfConceptManager : ConceptManager
    {
        public ProofOfConceptManager(string dir) : base(dir)
        {   
            //Each new proof of concept should inherit the interface IProofOfConcept
            MyConsole.WriteLine(ConsoleColor.Gray, $"Building proof of concepts for {ManagerName}...");
            proofOfConcepts.Add(new ChapterOne(dir));
        }
    }
}
