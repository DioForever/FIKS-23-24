using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstructionSystem;

namespace MemorySystem
{
    public class MemorySystem
    {

    }

    public class Memory
    {
        public string[] memory;
        public Memory(Player[] players, int playerCount = 2)
        {
            // Create memory
            memory = new string[256 + 256 * playerCount];
            // Chose a starting point for each player
            for (int i = 0; i < playerCount; i++)
            {
                Player player = players[i];
                player.pc = 256 + 256 * i;
                memory[256 + 256 * i] = "";
                if (!(player.cmdStartPlan.Length() > 0 && player.cmdStartPlan.instructions[0] != null && player.cmdStartPlan.Length() <= 256))
                {
                    System.Console.WriteLine("Error: player " + player.id + " has no start plan or start plan is too long");
                    continue;
                }
                for (int j = 0; j < player.cmdStartPlan.Length(); j++)
                {
                    memory[256 + 256 * i + j] = player.cmdStartPlan.instructions[j].ToString();
                }
            }

            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == null)
                {
                    memory[i] = "0";
                }
            }

            for (int i = 0; i < memory.Length; i++)
            {
                System.Console.WriteLine($"{i.ToString("X")}: 0x{memory[i].Replace(" ", "")}");
            }
        }

    }

    public class Player
    {
        public int id;
        public int pc;
        public string[] registers;
        public InstructionSystem.InstructionPlan cmdStartPlan;

        public Player(int id, InstructionSystem.InstructionPlan cmdStartPlan)
        {
            this.id = id;
            this.pc = 0;
            this.registers = new string[6];
            this.cmdStartPlan = cmdStartPlan;
        }
    }
}