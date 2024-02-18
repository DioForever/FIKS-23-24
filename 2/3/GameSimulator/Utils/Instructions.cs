using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Utils;

namespace Instructions
{
    enum InstructionType
    {
        NO = 69,
        ADD = 1,
        SUB = 2,
        MUL = 3,
        LOAD = 5,
        STORE = 6,
        MOV = 7,
        JUMP = 10,
        REVJUMP = 11,
        LTJUMP = 12,
        REVLTJUMP = 13,
        NEQJUMP = 14,
        REVNEQJUMP = 15,
        SETIMMLOW = 20,
        SETIMMHIGH = 21,
        TELEPORT = 42,
        BOMB = 50
    }

    public class Instruction
    {
        public Player player;
        public Memory memory;
        public string args;
        public Instruction(string args, Player player, Memory memory)
        {
            this.args = args;
            this.player = player;
            this.memory = memory;
        }

        public override string ToString()
        {
            return args;
        }
        public bool Run()
        {
            string instructionType = args.Substring(0, 2);
            string instructionReg1 = args.Substring(2, 1);
            string instructionReg2 = args.Substring(3, 1);
            string instructionArgs = args.Substring(4, 4);
            switch (instructionType)
            {
                case "69":
                    // NOP
                    player.pc += 1;
                    return true;
                case "01":
                    return Add.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "02":
                    return Sub.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "03":
                    return Mul.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "05":
                    return Load.Run(instructionReg1, instructionReg2, instructionArgs, player, memory);
                case "06":
                    return Store.Run(instructionReg1, instructionReg2, instructionArgs, player, memory);
                case "07":
                    return Mov.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "10":
                    return Jump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "11":
                    return RevJump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "12":
                    return LtJump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "13":
                    return RevLtJump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "14":
                    return NeqJump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "15":
                    return RevNeqJump.Run(instructionReg1, instructionReg2, instructionArgs, player);
                case "20":
                    return SetImmLow.Run(instructionReg1, instructionArgs, player);
                case "21":
                    return SetImmHigh.Run(instructionReg1, instructionArgs, player);
                case "42":
                    return Teleport.Run(instructionReg1, instructionReg2, instructionArgs, player, memory);
                case "50":
                    return Bomb.Run(instructionReg1, instructionReg2, instructionArgs, player, memory);
                default:
                    return false;
            }
        }
        public static bool CheckInstructionValidty(string instructionType, string instructionReg1, string instructionReg2, string instructionArgs)
        {
            System.Console.WriteLine(instructionType);
            if (instructionType == "00")
            {
                return false;
            }
            System.Console.WriteLine(int.Parse(instructionReg1) + " " + instructionReg1 + " " + int.Parse(instructionReg1) + " " + instructionReg2);
            if (int.Parse(instructionReg1) < 8 || int.Parse(instructionReg2) < 8)
            {
                return false;
            }
            return true;
        }
    }

    public class Add
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("01", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            int sum = reg1Val + reg2Val + argsVal;
            player.registers[reg1Int] = Convert.ToString(sum, 4);

            player.pc += 1;
            return true;
        }
    }

    public class Sub
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("02", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            int sub = reg1Val - reg2Val - argsVal;
            player.registers[reg1Int] = Convert.ToString(sub, 4);

            player.pc += 1;
            return true;
        }
    }
    public class Mul
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("03", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            int mul = reg1Val * (reg2Val + argsVal);
            player.registers[reg1Int] = Convert.ToString(mul, 4);

            player.pc += 1;
            return true;
        }
    }

    public class Load
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player, Memory memory)
        {
            if (!Instruction.CheckInstructionValidty("05", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);
            memory.memory[reg2Val + argsVal] = player.registers[reg1Int];

            player.pc += 1;
            return true;
        }
    }

    public class Store
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player, Memory memory)
        {
            if (!Instruction.CheckInstructionValidty("06", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);
            memory.memory[reg2Val + argsVal] = player.registers[reg1Int];

            player.pc += 1;
            return true;
        }
    }
    public class Mov
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("07", reg1, reg2, "0000"))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            player.registers[reg1Int] = player.registers[reg2Int] + argsVal;

            player.pc += 1;
            return true;
        }
    }
    public class Jump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("10", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val == reg2Val)
            {
                player.pc -= argsVal;
            }
            return true;
        }
    }
    public class RevJump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("11", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val == reg2Val)
            {
                player.pc -= argsVal;
            }
            return true;
        }
    }
    public class LtJump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("12", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val < reg2Val)
            {
                player.pc += argsVal;
            }
            return true;
        }
    }
    public class RevLtJump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("13", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val < reg2Val)
            {
                player.pc -= argsVal;
            }
            return true;
        }
    }
    public class NeqJump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("14", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val != reg2Val)
            {
                player.pc += argsVal;
            }
            return true;
        }
    }
    public class RevNeqJump
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("15", reg1, reg2, instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg2Int = Convert.ToInt32(reg2, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            int reg2Val = Convert.ToInt32(player.registers[reg2Int], 4);
            int argsVal = Convert.ToInt32(instructionArgs, 4);

            if (reg1Val != reg2Val)
            {
                player.pc -= argsVal;
            }
            return true;
        }
    }
    public class SetImmLow
    {
        public static bool Run(string reg1, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("20", reg1, "0", instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            string newReg1 = instructionArgs + player.registers[reg1Int].Substring(3, 4);

            player.registers[reg1Int] = newReg1;
            player.pc += 1;
            return true;
        }
    }
    public class SetImmHigh
    {
        public static bool Run(string reg1, string instructionArgs, Player player)
        {
            if (!Instruction.CheckInstructionValidty("21", reg1, "0", instructionArgs))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            string newReg1 = player.registers[reg1Int].Substring(0, 4) + instructionArgs;

            player.registers[reg1Int] = newReg1;
            player.pc += 1;
            return true;
        }
    }
    public class Teleport
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player, Memory memory)
        {
            if (!Instruction.CheckInstructionValidty("42", reg1, reg2, "0000"))
            {
                return false;
            }
            if (instructionArgs == "0000")
            {
                player.pc += 1;
            }
            else
            {
                memory.memory[player.pc] = (Convert.ToInt32(memory.memory[player.pc]) - 1).ToString("4");
            }
            return true;
        }
    }
    public class Bomb
    {
        public static bool Run(string reg1, string reg2, string instructionArgs, Player player, Memory memory)
        {
            if (!Instruction.CheckInstructionValidty("50", reg1, reg2, "0000"))
            {
                return false;
            }
            int reg1Int = Convert.ToInt32(reg1, 4);
            int reg1Val = Convert.ToInt32(player.registers[reg1Int], 4);
            if (instructionArgs == "0000")
            {
                player.alive = false;
                return false;
            }
            else
            {
                player.registers[reg1Int] = (Convert.ToInt32(memory.memory[player.pc]) - 1).ToString("4");
                player.pc += 1;
            }
            return true;
        }
    }



    public static class ImmString
    {
        public static string sort(string imm)
        {
            string immString = "";
            int immRemLength = 5 - imm.Length;
            for (int i = 0; i < immRemLength; i++)
            {
                if (i == 2)
                {
                    immString += " ";
                    continue;
                }
                immString += "0";

            }
            immString += imm;

            return immString;
        }

    }

    public static class HexToString
    {
        public static string sort(string hex)
        {
            string hexString = "";
            int hexRemLength = 8 - hex.Length;
            for (int i = 0; i < hexRemLength; i++)
            {
                hexString += "0";

            }
            hexString += hex;

            return hexString;
        }

    }
}