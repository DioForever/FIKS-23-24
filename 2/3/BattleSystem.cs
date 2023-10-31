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
    public class Register
    {
        public string value = "0";
        public Register(string value)
        {
            this.value = "0";
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
    }

    public class NOP : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;

        public NOP(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
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
        public ADD(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;

            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"ADD 01 {reg1.value}{reg2.value} 00 0{imm}");
            int reg1Value = Convert.ToInt32(reg1.value, 16);
            int reg2Value = Convert.ToInt32(reg2.value, 16);
            int immValue = Convert.ToInt32(imm, 16);
            string reg1Hex = (reg1Value + reg2Value + immValue).ToString("X");
            reg1.value = reg1Hex;
            pc++;
            return pc;
        }
    }
    public class SUB : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public SUB(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;

            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"SUB 02 {reg1.value}{reg2.value} 00 0{imm}");
            int reg1Value = Convert.ToInt32(reg1.value, 16);
            int reg2Value = Convert.ToInt32(reg2.value, 16);
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

        public MUL(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            Work(pc);

            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"MUL 03 {reg1.value}{reg2.value} 00 0{imm}");
            int reg1Value = Convert.ToInt32(reg1.value, 16);
            int reg2Value = Convert.ToInt32(reg2.value, 16);
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
        public LOAD(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"LOAD 05 {reg1.value}{reg2.value} 00 0{imm}");

            pc++;
            return pc;
        }
    }
    public class STORE : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public STORE(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"STORE 06 {reg1.value}{reg2.value} 00 0{imm}");

            pc++;
            return pc;
        }
    }
    public class MOV : Instruction
    {
        private Register reg1;
        private Register reg2;
        private string imm;
        public MOV(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"MOV 07 {reg1.value}{reg2.value} 00 0{imm}");
            int reg2Value = Convert.ToInt32(reg2.value, 16);
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
        public JUMP(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"JUMP 10 {reg1.value}{reg2.value} 00 0{imm}");
            int reg1Value = Convert.ToInt32(reg1.value, 16);
            int reg2Value = Convert.ToInt32(reg2.value, 16);
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
        public REVJUMP(Register reg1, Register reg2, string imm, InstructionPlan plan, int pc, int opcode = 1) : base(opcode, reg1, reg2, imm)
        {
            this.reg1 = reg1;
            this.reg2 = reg2;
            this.imm = imm;
            plan.Add(this);
        }
        public override int Work(int pc)
        {
            System.Console.WriteLine($"REVJUMP 11 {reg1.value}{reg2.value} 00 0{imm}");
            int reg1Value = Convert.ToInt32(reg1.value, 16);
            int reg2Value = Convert.ToInt32(reg2.value, 16);
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