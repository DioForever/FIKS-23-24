using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Game
{
    public class GameSimulator
    {
        public GameSimulator(string path)
        {
            Console.WriteLine("Simulation begins!");
            Player[] players = Utils.Utils.ParseInput(path);
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine("Player " + i + " has " + players[i].instructionsInitial.Length + " instructions");
                for (int j = 0; j < players[i].instructionsInitial.Length; j++)
                {
                    if (players[i].instructionsInitial[j] != null) Console.WriteLine(i + " - " + j + " " + players[i].instructionsInitial[j]);
                }
                System.Console.WriteLine("-------");
            }
            Memory memory = new Memory(players);
            System.Console.WriteLine(memory.Run());

        }

    }

}