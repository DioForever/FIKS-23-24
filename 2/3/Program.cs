using BattleSystem;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            InstructionPlan plan = new InstructionPlan();
            Register r1 = new Register("0");
            Register r2 = new Register("0");
            Register r3 = new Register("0");
            Register r4 = new Register("0");
            Register r5 = new Register("0");
            Register r6 = new Register("0");

            Instruction i1 = new NOP(r1, r2, "0", plan, 0);
            Instruction i2 = new ADD(r1, r2, "0", plan, 1);


            for (int pc = 0; pc < plan.instructions.Count;)
            {
                if (pc >= plan.instructions.Count && pc < 0)
                {
                    System.Console.WriteLine("Error: pc out of bounds");
                }
                pc = plan.instructions[pc].Work(pc);
            }



        }
    }
}