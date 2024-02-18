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
        public Player[] players;
        public Memory(Player[] players, int playerCount = 2, bool output = false)
        {
            this.players = players;
            // Create memory
            memory = new string[256 + 256 * playerCount];
            // Chose a starting point for each player
            System.Console.WriteLine(playerCount);
            // foreach (Player player in players)
            // {
            //     System.Console.WriteLine(player);
            // }
            // System.Console.WriteLine(players[1]);
            for (int i = 0; i < playerCount; i++)
            {
                Player player = players[i];
                player.pcStarting = 256 + 256 * i;
                player.pcCurrent = 256 + 256 * i;
                memory[256 + 256 * i] = "";
                if (!(player.cmdStartPlan.Length > 0 && player.cmdStartPlan[0] != null && player.cmdStartPlan.Length < 256))
                {
                    System.Console.WriteLine("Error: player " + player.id + " has no start plan or start plan is too long");
                    continue;
                }
                for (int j = 0; j < player.cmdStartPlan.Length; j++)
                {
                    memory[256 + 256 * i + j] = player.cmdStartPlan[j].ToString();
                }
            }

            for (int i = 0; i < memory.Length; i++)
            {
                if (memory[i] == null)
                {
                    memory[i] = "0";
                }
            }

            if (output)
            {
                for (int i = 0; i < memory.Length; i++)
                {
                    System.Console.WriteLine($"{i.ToString("X")}: 0x{memory[i].Replace(" ", "")}");
                }
            }
        }

    }

    public class Player
    {
        public int id;
        public bool isAlive = true;
        public int pcStarting;
        public int pcCurrent;
        public string[] registers;
        public string[] cmdStartPlan;
        public int instructionLength;
        public int freezeCounter = 0;
        public string[] instructionHistory;

        public Player(int id, string[] cmdStartPlan)
        {
            this.instructionHistory = new string[10000];
            this.id = id;
            this.pcStarting = 0;
            this.pcCurrent = 0;
            this.registers = new string[6] { "0", "0", "0", "0", "0", "0" };
            this.cmdStartPlan = cmdStartPlan;
            this.instructionLength = cmdStartPlan.Length;
        }
    }
}