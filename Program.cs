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
            ProofOfConceptManager poc = new ProofOfConceptManager();
            poc.Run(args);
        }
    }
    class ProofOfConceptManager
    {
        List<IProofOfConcept> proofOfConcepts = new List<IProofOfConcept>();
        public ProofOfConceptManager()
        {
            //Each new proof of concept should inherit the interface IProofOfConcept
            Console.WriteLine("Building proof of concepts...\n");
            proofOfConcepts.Add(new Chapter1.SimUDuck());
        }
        public void Run(String[] args)
        {
            int action = 0;

            if (args.Length == 0)
            {
                Console.WriteLine("press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                if (Int32.TryParse(args[0], out action))
                {
                    if (action >= 0 && action < proofOfConcepts.Count)
                    {
                        //received application argument, execute specific PoC
                        Console.WriteLine($"Executing: {proofOfConcepts[action].ConceptName()}...");
                        TryCatch_RunDemo(action);

                        Console.WriteLine("press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("invalid number input");
                    }
                }
            }

            var input = "";
            while (!string.Equals(input.Trim(), "q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Choose one of the following proof of concept demos: \n{ListDemos()}q - Quit Demo");
                

                Console.Write("Choice: ");
                input = Console.ReadLine();

                if (Int32.TryParse(input, out action))
                {
                    if (action >= 0 && action < proofOfConcepts.Count)
                    {
                        Console.WriteLine($"Executing: {proofOfConcepts[action].ConceptName()}...");
                        TryCatch_RunDemo(action);

                        Console.WriteLine("press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("invalid number input");
                    }
                }
                else if (input.ToLower() != "q")
                {
                    Console.WriteLine("Invalid string input");
                }

            }
        }

        private string ListDemos()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < proofOfConcepts.Count; i++)
            {
                sb.Append($"{i} - {proofOfConcepts[i].ConceptName()}\n");
            }
            return sb.ToString();
        }

        private void TryCatch_RunDemo(int action)
        {
            try
            {
                proofOfConcepts[action].RunDemo();
            }
            catch (Exception ex)
            {
                MyConsole.WriteLine(ConsoleColor.Red, $"Exception occured: {ex.Message}");
            }
        }
    }
}
