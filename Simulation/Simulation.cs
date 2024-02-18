using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemorySystem;
using InstructionSystem;

namespace Simulation
{
    public class SimulationSystem
    {
        private Memory memory;

        public SimulationSystem()
        {
            string[] cmds = File.ReadAllText("plans.txt").Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries);
            System.Console.WriteLine(string.Join("\n", cmds));
            string[][] playersCmds = new string[cmds.Length][];
            for (int i = 0; i < cmds.Length; i++)
            {
                playersCmds[i] = cmds[i].Split("\n");
                for (int k = 0; k < playersCmds[i].Length; k++)
                {
                    playersCmds[i][k] = playersCmds[i][k].Replace("\n", "");
                }
                System.Console.WriteLine("Player " + i);
                System.Console.WriteLine(string.Join("\n", playersCmds[i]));
            }

            Player[] playersCreation = new Player[cmds.Length];
            for (int i = 0; i < cmds.Length; i++)
            {
                playersCreation[i] = new Player(i, playersCmds[i]);
            }
            this.memory = new Memory(playersCreation);
            // for(int i = 0;)
            Player[] players = memory.players;
            int deadCount = 0;
            for (int instructionCount = 0; instructionCount < 10000; instructionCount++)
            {
                for (int pIndex = 0; pIndex < players.Length; pIndex++)
                {
                    // If player is dead, skip
                    if (!players[pIndex].isAlive)
                    {
                        continue;
                    }
                    // Lets fail if pc is out of bounds
                    if (players[pIndex].pcStarting - players[pIndex].pcCurrent >= memory.memory.Length)
                    {
                        System.Console.WriteLine("Error: pc out of bounds");
                        players[pIndex].isAlive = false;
                        deadCount += 1;
                        continue;
                    }
                    // Lets check if the player is freezed = waiting for teleportation
                    if (players[pIndex].freezeCounter > 0)
                    {
                        players[pIndex].freezeCounter--;
                        continue;
                    }
                    // Lets do the instruction
                    string instruction = memory.memory[players[pIndex].pcCurrent];


                    // Lets check if the instruction repeats
                    // foreach (string instructionHistory in players[pIndex].instructionHistory)
                    // {
                    //     System.Console.WriteLine($"Player {players[pIndex].id} instruction {instructionHistory}");
                    //     players[pIndex].isAlive = false;
                    //     deadCount += 1;
                    //     continue;
                    // }
                    if (CheckRepetetion(players[pIndex].instructionHistory))
                    {
                        players[pIndex].isAlive = false;
                        deadCount += 1;
                        System.Console.WriteLine($"Player {players[pIndex].id} died: Repetetion {instructionCount}");
                    }

                    if (deadCount >= players.Length)
                    {
                        System.Console.WriteLine("Game over");
                        return;
                    }


                }
            }
        }

        public bool CheckRepetetion(string[] instructionHistory)
        {
            if (instructionHistory.Length < 3) return false;
            for (int length = 1; length <= instructionHistory.Length; length++)
            {
                // System.Console.WriteLine("Checking length " + length);
                string checkInstructions = String.Join("", instructionHistory);
                // System.Console.WriteLine($"Checking {length}");
                List<string> segements = SplitString(checkInstructions, length * 8);
                // System.Console.WriteLine($"Checking {String.Join(", ", segements)}");

                for (int i = 0; i < segements.Count; i++)
                {
                    if (i + 2 >= segements.Count) continue;
                    // System.Console.WriteLine($"Checking {segements[i]}, {segements[i + 1]}, {segements[i + 2]}");
                    if (segements[i] == segements[i + 1] && segements[i] == segements[i + 2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static List<string> SplitString(string input, int segmentLength)
        {
            List<string> segments = new List<string>();
            for (int i = 0; i < input.Length; i += segmentLength)
            {
                // Take a substring of the specified length
                segments.Add(input.Replace(" ", "").Substring(i, Math.Min(segmentLength, input.Length - i)));
            }
            return segments;
        }
    }


}