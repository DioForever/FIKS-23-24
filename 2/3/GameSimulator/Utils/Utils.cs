using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Instructions;
namespace Utils
{
    public class Utils
    {

        public static Player[] ParseInput(string path)
        {
            string[,] playerInstructions = new string[2, 256];
            int playerId = 0;
            string[] lines = System.IO.File.ReadAllLines(path);
            int lineMin = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line == "")
                {
                    playerId++;
                    lineMin = i + 1;
                    continue;
                }
                playerInstructions[playerId, i - lineMin] = line;
            }
            System.Console.WriteLine(playerInstructions.Length / 256);
            Player[] players = new Player[playerInstructions.GetLength(0)];
            for (int i = 0; i < playerInstructions.GetLength(0); i++)
            {
                players[i] = new Player(i);
                for (int j = 0; j < playerInstructions.GetLength(1); j++)
                {
                    players[i].instructionsInitial[j] = playerInstructions[i, j];
                }
            }
            return players;
        }
    }


    public class Player
    {
        public string[] registers = new string[16];
        public bool alive = true;
        public int pc;
        public int id;
        public string[] instructionsInitial = new string[256];
        public Player(int id)
        {
            this.id = id;
            registers = new string[16];
            for (int i = 0; i < 16; i++)
            {
                registers[i] = "00000000";
            }
        }
    }

    public class Memory
    {
        public string[] memory;
        public Player[] players;
        private int[] playerAwaitingPC;
        public Memory(Player[] players)
        {
            this.players = players;
            this.playerAwaitingPC = new int[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                playerAwaitingPC[i] = -1;
            }
            memory = new string[256 + 256 * players.Length];
            for (int p = 0; p < players.Length; p++)
            {
                players[p].pc = p * 256 + 256;
                for (int k = 0; k < 256; k++)
                {
                    memory[p * 256 + 256 + k] = players[p].instructionsInitial[k] != null ? players[p].instructionsInitial[k] : "00000000";
                    System.Console.WriteLine("Player " + p + " instruction " + k + " is " + memory[p * 256 + 256 + k]);
                }
            }
        }

        public int Run()
        {
            int playersAlive = players.Length;
            while (playersAlive > 1)
            {
                foreach (Player player in players)
                {
                    Instruction instruction = new Instruction(memory[player.pc], player, this);
                    System.Console.WriteLine("Player " + player.id + " is at " + player.pc + " with instruction " + instruction.ToString());
                    if (!instruction.Run())
                    {
                        System.Console.WriteLine("Player " + player.id + " died! by instruction " + instruction.ToString() + " at " + player.pc);
                        playersAlive--;
                    }

                }
            }
            return 0;
        }
    }



}