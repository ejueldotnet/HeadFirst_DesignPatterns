using Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeadFirst_DesignPatterns.Templates
{
    public class ConceptManager : IConceptManager
    {
        public List<IProofOfConcept> proofOfConcepts = new List<IProofOfConcept>();
        public string ManagerName;
        public ConceptManager(string managerName)
        {
            this.ManagerName = "/"+managerName;
        }
        public string GetManagerName()
        {
            return ManagerName;
        }
        public int Run(String[] args)
        {
            int action = 0;

            if (args.Length != 0)
            {
                if (Int32.TryParse(args[0], out action))
                {
                    if (action >= 0 && action < proofOfConcepts.Count)
                    {
                        //received application argument, execute specific PoC
                        MyConsole.PrintThenClear($"Executing: {proofOfConcepts[action].ConceptName()}...", clear: false);
                        TryCatch_RunDemo(action);
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
                Console.WriteLine($"{ManagerName}\nChoose one of the following proof of concept demos: \n{ListDemos()}q - Quit Demo");


                Console.Write("Choice: ");
                input = Console.ReadLine();

                if (Int32.TryParse(input, out action))
                {
                    if (action >= 0 && action < proofOfConcepts.Count)
                    {
                        MyConsole.PrintThenClear($"Executing: {proofOfConcepts[action].ConceptName()}...");
                        int result = TryCatch_RunDemo(action);
                        if (result > 0){
                            return result;
                        }
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
            return 0;
        }
        public string ListDemos()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < proofOfConcepts.Count; i++)
            {
                sb.Append($"{i}: {proofOfConcepts[i].ConceptName()}\n");
            }
            return sb.ToString();
        }

        public int TryCatch_RunDemo(int action)
        {
            try
            {
                return proofOfConcepts[action].RunDemo();
            }
            catch (Exception ex)
            {
                MyConsole.WriteLine(ConsoleColor.Red, $"Exception occured in {proofOfConcepts[action].ConceptName()}: {ex.Message}");
                return -1;
            }
        }

    }
}
