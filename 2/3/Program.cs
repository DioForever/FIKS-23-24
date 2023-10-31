using InstructionSystem;
using MemorySystem;
using Simulation;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            InstructionPlan plan = new InstructionPlan();
            Register reg0 = new Register("00000000", 0);
            Register reg1 = new Register("00000000", 1);
            Register reg2 = new Register("00000000", 2);
            Register reg3 = new Register("00000000", 3);
            Register reg4 = new Register("00000000", 4);
            Register reg5 = new Register("00000000", 5);
            RegistersPrinter printer = new RegistersPrinter(ref reg0, ref reg1, ref reg2, ref reg3, ref reg4, ref reg5);



            // ATTACK SEQUENCE - CLOSE ENEMY
            Instruction i1 = new LOAD(reg4, reg0, "00 2a", plan);
            Instruction i2 = new SETIMMLOW(reg3, reg0, "00 00", plan);
            Instruction i3 = new SETIMMHIGH(reg4, reg0, "00 00", plan);
            Instruction i4 = new STORE(reg3, reg4, "00 01", plan);
            // ATTACK SEQUENCE - SECOND CLOSE ENEMY
            Instruction i5 = new LOAD(reg4, reg0, "00 2b", plan);
            Instruction i6 = new SETIMMLOW(reg3, reg0, "00 00", plan);
            Instruction i7 = new SETIMMHIGH(reg4, reg0, "00 00", plan);
            Instruction i8 = new STORE(reg3, reg4, "00 01", plan);
            // REPAIR SEQUENCE




            Player player1 = new Player(0, plan);
            Player player2 = new Player(1, plan);

            Memory memory = new Memory(new Player[] { player1, player2 });

            SimulationSystem simulationSystem = new SimulationSystem(memory);

            // int counter = 0;
            // for (int pc = 0; pc < plan.instructions.Count;)
            // {
            //     counter++;
            //     if (pc >= plan.instructions.Count && pc < 0)
            //     {
            //         System.Console.WriteLine("Error: pc out of bounds");
            //     }
            //     pc = plan.instructions[pc].Work(pc);

            //     printer.print();
            //     if (counter > 50)
            //     {
            //         System.Console.WriteLine("Error: infinite loop");
            //         break;
            //     }
            // }

            // if (true)
            // {
            //     // Here we save the plan into a file
            //     System.IO.StreamWriter file = new System.IO.StreamWriter("planCode.txt");
            //     foreach (Instruction instruction in plan.instructions)
            //     {
            //         file.WriteLine(instruction.ToString());
            //     }
            //     file.Close();
            // }



        }
    }
}