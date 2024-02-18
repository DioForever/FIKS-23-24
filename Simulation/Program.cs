using InstructionSystem;
using MemorySystem;
using Simulation;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            // InstructionPlan plan = new InstructionPlan();
            // Register reg0 = new Register("00000000", 0);
            // Register reg1 = new Register("00000000", 1);
            // Register reg2 = new Register("00000000", 2);
            // Register reg3 = new Register("00000000", 3);
            // Register reg4 = new Register("00000000", 4);
            // Register reg5 = new Register("00000000", 5);
            // RegistersPrinter printer = new RegistersPrinter(ref reg0, ref reg1, ref reg2, ref reg3, ref reg4, ref reg5);



            // // ATTACK SEQUENCE - CLOSE ENEMY
            // Instruction i1 = new LOAD(reg4, reg0, "00 2a", plan);
            // Instruction i2 = new SETIMMLOW(reg3, reg0, "00 00", plan);
            // Instruction i3 = new SETIMMHIGH(reg4, reg0, "00 00", plan);
            // Instruction i4 = new STORE(reg3, reg4, "00 01", plan);
            // // ATTACK SEQUENCE - SECOND CLOSE ENEMY
            // Instruction i5 = new LOAD(reg4, reg0, "00 2b", plan);
            // Instruction i6 = new SETIMMLOW(reg3, reg0, "00 00", plan);
            // Instruction i7 = new SETIMMHIGH(reg4, reg0, "00 00", plan);
            // Instruction i8 = new STORE(reg3, reg4, "00 01", plan);
            // // REPAIR SEQUENCE

            // SimulationSystem simulationSystem = new SimulationSystem();
            string[] emptyPlan = new string[10];
            Player player = new Player(0, emptyPlan);
            Player[] players = new Player[2] { player, player };
            Memory memory = new Memory(players, 2);
            int totalInstructions = 0;

            List<string> instructions = new List<string>();
            // Loading BOMB into reg3
            // totalInstructions += LoadBombSequence(instructions, "3", player, memory);
            // BOMB has been loaded
            // Loading Enemy position into reg4
            // totalInstructions += LoadEnemySequence(instructions, "3", player, memory);
            // Enemy position has been loaded
            // totalInstructions += AttackEnemySequence(instructions, "3", "4", player, memory);
            int count = 0;
            int shootCount = 0;
            int typeCount = 0;
            for (int i = 0; totalInstructions <= 255 - 10; i++)
            {
                // totalInstructions += LoadBombSequence(instructions, "3", player, memory);
                count++;
                if (count % 2 == 0)
                {
                    if (shootCount % 2 == 0)
                    {
                        totalInstructions += LoadEnemySequence(instructions, "4", player, memory);
                    }
                    else
                    {
                        if (typeCount % 2 == 0)
                        {
                            totalInstructions += AttackEnemySequenceNoPredict(instructions, "4", "3", player, memory);
                        }
                        else
                        {
                            totalInstructions += AttackEnemySequencePredict(instructions, "4", "3", player, memory);
                        }
                        typeCount++;
                    }
                    shootCount++;
                }
                else
                {
                    totalInstructions += JumpSequenceOne(instructions, "3", player, memory);
                }
            }
            string code = (totalInstructions - 1).ToString("X").PadLeft(2, '0');
            // System.Console.WriteLine(code);
            instructions.Add(new REVJUMP(player, memory, $"00 00 {code}").GetCode());

            // Write it to file
            File.WriteAllLines("planCode.txt", instructions.ToArray()); // 




        }

        public static int JumpSequenceOne(List<string> instructions, string regId, Player player, Memory memory)
        {
            instructions.Add(new JUMP(player, memory, "00 00 05").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            return 5;
        }
        public static int JumpSequenceTwo(List<string> instructions, string regId, Player player, Memory memory)
        {
            instructions.Add(new JUMP(player, memory, "00 00 05").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            return 5;
        }
        public static int LoadEnemySequence(List<string> instructions, string regId, Player player, Memory memory)
        {
            // Loading Enemy position into reg4
            instructions.Add(new JUMP(player, memory, "00 00 04").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new LOAD(player, memory, $"{regId}0 00 2b").GetCode());
            // Enemy position has been loaded
            return 5;
        }

        public static int LoadBombSequence(List<string> instructions, string regId, Player player, Memory memory)
        {
            // Loading Enemy position into reg4
            instructions.Add(new JUMP(player, memory, "00 00 04").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new SETIMMHIGH(player, memory, $"{regId}0 00 00").GetCode());
            instructions.Add(new JUMP(player, memory, "00 00 04").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new SETIMMLOW(player, memory, $"{regId}0 50 00").GetCode());
            // Enemy position has been loaded
            return 10;
        }
        public static int AttackEnemySequenceNoPredict(List<string> instructions, string regIdLocation, string regIdBomb, Player player, Memory memory)
        {
            // Loading Enemy position into reg4
            instructions.Add(new JUMP(player, memory, "00 00 04").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new STORE(player, memory, $"{regIdBomb}{regIdLocation} 00 00").GetCode());
            // Enemy position has been loaded
            return 5;
        }

        public static int AttackEnemySequencePredict(List<string> instructions, string regIdLocation, string regIdBomb, Player player, Memory memory)
        {
            // Loading Enemy position into reg4
            instructions.Add(new JUMP(player, memory, "00 00 04").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new NOP(player, memory, "00 00 00").GetCode());
            instructions.Add(new MOV(player, memory, "00 00 00").GetCode());
            instructions.Add(new STORE(player, memory, $"{regIdBomb}{regIdLocation} 00 03").GetCode());
            // Enemy position has been loaded
            return 5;
        }



    }
}