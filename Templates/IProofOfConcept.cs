using System;

namespace HeadFirst_DesignPatterns.Templates
{
    public interface IConceptManager
    {
        public int Run(String[] args);
        public string GetManagerName();
    }
    public interface IProofOfConcept
    {
        string ParentDir();
        string ConceptName();
        int RunDemo();
    }
}
