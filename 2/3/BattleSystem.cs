using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSystem
{
    public class BattleSystem
    {

        public BattleSystem(InstructionPlan plan)
        {
            int pc = 0;
            while (pc < plan.instructions.Count)
            {
                pc = plan.instructions[pc].Work(pc);
            }

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

    public class RegistersPrinter
    {
        private Register[] registers;
        public RegistersPrinter(ref Register reg1, ref Register reg2, ref Register reg3, ref Register reg4, ref Register reg5, ref Register reg6)
        {
            Register[] registers = { reg1, reg2, reg3, reg4, reg5, reg6 };
            this.registers = registers;
        }
        public void print()
        {
            foreach (Register register in registers)
            {
                System.Console.Write($"{register.number} - {register.value} ");
            }
            System.Console.WriteLine();
        }
    }

    public class Register
    {
        public string value = "00 00 00 00";
        public int number;

        public Register(string value, int number = 0)
        {
            this.value = value;
            this.number = number;
        }
    }

    public class Instruction
    {
        int opcode;
        Register reg1;
        Register reg2;
        string imm;

        public Instruction(int opcode, Register reg1, Register reg2, string imm)
        {
            this.opcode = opcode;
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
        }

        public virtual int Work(int pc)
        {
            return pc;
        }

        public override string ToString()
        {
            string opcodeString = opcode.ToString();
            if (opcode < 10)
            {
                opcodeString = "0" + opcodeString;
            }
            return $"{opcodeString} {reg1.number}{reg2.number} {ImmString.sort(imm)}";
        }
    }



    public class NOP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;

        public NOP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(69, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"NOP 69 {0}{0} 00 00");
            pc++;
            return pc;
        }
    }
    public class ADD : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public ADD(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(01, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;

            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"ADD 01 {reg1.number}{reg2.number} {immString}");
            imm = imm.Replace(" ", "");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            string reg1Hex = (reg1Value + reg2Value + immValue).ToString("X");
            reg1.value = HexToString.sort(reg1Hex);
            pc++;
            return pc;
        }
    }
    public class SUB : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public SUB(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(02, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;

            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"SUB 02 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            string reg1Hex = (reg1Value - (reg2Value + immValue)).ToString("X");
            reg1.value = reg1Hex;
            pc++;
            return pc;
        }
    }
    public class MUL : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;

        public MUL(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(03, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"MUL 03 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            string reg1Hex = (reg1Value * (reg2Value + immValue)).ToString("X");
            reg1.value = reg1Hex;
            pc++;
            return pc;
        }
    }
    public class LOAD : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public LOAD(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(05, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"LOAD 05 {reg1.number}{reg2.number} {immString}");

            pc++;
            return pc;
        }
    }
    public class STORE : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public STORE(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(06, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"STORE 06 {reg1.number}{reg2.number} {immString}");

            pc++;
            return pc;
        }
    }
    public class MOV : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public MOV(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(07, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"MOV 07 {reg1.number}{reg2.number} {immString}");
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            string reg1Hex = (reg2Value + immValue).ToString("X");
            reg1.value = reg1Hex;
            pc++;
            return pc;
        }
    }
    public class JUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public JUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(10, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"JUMP 10 {reg1.number}{reg2.number} 00 0{immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value == reg2Value)
            {
                pc += Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class REVJUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public REVJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(11, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"REVJUMP 11 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value == reg2Value)
            {
                pc -= Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class LTJUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public LTJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(12, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"LTJUMP 12 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value < reg2Value)
            {
                pc += Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class REVLTJUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public REVLTJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(13, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"REVLTJUMP 13 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value < reg2Value)
            {
                pc -= Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class NEQJUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public NEQJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(14, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"NEQJUMP 14 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value != reg2Value)
            {
                pc += Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class REVNEQJUMP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public REVNEQJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(15, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"REVNEQJUMP 15 {reg1.number}{reg2.number} {immString}");
            int reg1Value = Convert.ToInt32(reg1.value.Replace(" ", ""), 16);
            int reg2Value = Convert.ToInt32(reg2.value.Replace(" ", ""), 16);
            int immValue = Convert.ToInt32(imm, 16);
            if (reg1Value != reg2Value)
            {
                pc -= Convert.ToInt32(imm, 16);
            }
            else
            {
                pc++;
            }
            return pc;
        }
    }
    public class SETIMMLOW : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public SETIMMLOW(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(20, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"SETIMMLOW 20 {reg1.number}{reg2.number} {immString}");
            imm = imm.Replace(" ", "");
            // System.Console.WriteLine(reg1.value);
            // System.Console.WriteLine(HexToString.sort(imm + reg1.value.Substring(4, 4)));
            reg1.value = HexToString.sort(imm + reg1.value.Substring(4, 4));
            pc++;
            return pc;
        }
    }
    public class SETIMMHIGH : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public SETIMMHIGH(Register reg1, Register reg2, string imm, InstructionPlan plan) : base(21, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            string immString = ImmString.sort(imm);
            System.Console.WriteLine($"SETIMMHIGH 21 {reg1.number}{reg2.number} {immString}");
            imm = imm.Replace(" ", "");
            // System.Console.WriteLine(reg1.value);
            // System.Console.WriteLine(HexToString.sort(imm + reg1.value.Substring(4, 4)));
            string tempRegValue = HexToString.sort(imm + reg1.value.Substring(4, 4));
            reg1.value = reg1.value.Substring(0, 4) + tempRegValue.Substring(4, 4);
            pc++;
            return pc;
        }
    }


    public class InstructionPlan
    {
        public List<Instruction> instructions = new List<Instruction>();
        public InstructionPlan()
        {

        }
        public void Add(Instruction instruction)
        {
            instructions.Add(instruction);
        }
    }


}