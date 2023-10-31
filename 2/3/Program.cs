using BattleSystem;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            InstructionPlan plan = new InstructionPlan();
            Register req1 = new Register("00000000", 0);
            Register req2 = new Register("00000000", 1);
            Register req3 = new Register("00000000", 2);
            Register req4 = new Register("00000000", 3);
            Register req5 = new Register("00000000", 4);
            Register req6 = new Register("00000000", 5);
            RegistersPrinter printer = new RegistersPrinter(ref req1, ref req2, ref req3, ref req4, ref req5, ref req6);

            Instruction i3 = new ADD(req1, req2, "01", plan);
            Instruction i4 = new ADD(req1, req2, "01", plan);
            Instruction i5 = new SETIMMLOW(req1, req2, "2a", plan);
            Instruction i6 = new SETIMMHIGH(req1, req2, "4b", plan);
            Instruction i7 = new REVJUMP(req2, req2, "04", plan);


            int counter = 0;
            for (int pc = 0; pc < plan.instructions.Count;)
            {
                counter++;
                if (pc >= plan.instructions.Count && pc < 0)
                {
                    System.Console.WriteLine("Error: pc out of bounds");
                }
                pc = plan.instructions[pc].Work(pc);
                printer.print();
                if (counter > 50)
                {
                    System.Console.WriteLine("Error: infinite loop");
                    break;
                }
            }

            if (true)
            {
                // Here we save the plan into a file
                System.IO.StreamWriter file = new System.IO.StreamWriter("planCode.txt");
                foreach (Instruction instruction in plan.instructions)
                {
                    file.WriteLine(instruction.ToString());
                }
                file.Close();
            }



        }
    }
}