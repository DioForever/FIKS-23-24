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

        public SimulationSystem(Memory memory)
        {
            this.memory = memory;
        }
    }
}