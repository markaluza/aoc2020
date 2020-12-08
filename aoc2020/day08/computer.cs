using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Aoc2020
{

    class Computer
    {
    private int reg_accumulator = 0;
    private int reg_instructionpointer = 0;

    public enum eOpCode 
    {
        acc, 
        nop, 
        jmp

    }

    public struct Instruction
    {
        public eOpCode OpCode;
        public int     OpValue;
    }

    public class Program : List<Instruction>
    {

        public Program Clone() 
        {
            Program copy = new Program();

            foreach(var v in this)
                copy.Add(v);

            return copy;
        }
    };

    public Program LoadProgram(string ProgramPath)
    {

        Program  program = new Program();
        program.Clear();

        var lines =  new List<string>(System.IO.File.ReadAllLines(ProgramPath));
        foreach (var line in lines)
        {
            var tmp = line.Split(" ");

            if (tmp.Length != 2) throw new Exception(string.Format("Err program : {0}", line));
            
            int opvalue = int.Parse(tmp[1]);

            switch(tmp[0])
            {

                case "jmp": program.Add( new Instruction { OpCode = eOpCode.jmp, OpValue = opvalue } );  break;
                case "acc": program.Add( new Instruction { OpCode = eOpCode.acc, OpValue = opvalue } );  break;
                case "nop": program.Add( new Instruction { OpCode = eOpCode.nop, OpValue = opvalue } );  break;
                default:
                    throw new Exception(string.Format("Err program unknown instruction : {0}", tmp[0]));
            }

        }

        return program;

    }

    public bool Execute(Program program)
    {
        reg_accumulator = 0;
        reg_instructionpointer = 0;

        var InstrExecuted = new List<int>();

        while(reg_instructionpointer < program.Count)        
        {

            var instr = program[reg_instructionpointer];

            if (InstrExecuted.Contains(reg_instructionpointer))
            {
                return false;
            }

            InstrExecuted.Add(reg_instructionpointer);

            switch(instr.OpCode)
            {
                case eOpCode.acc: 
                    reg_instructionpointer++;
                    reg_accumulator += instr.OpValue;
                break;

                case eOpCode.jmp:
                    reg_instructionpointer += instr.OpValue;
                break;

                case eOpCode.nop: 
                    reg_instructionpointer++;  
                break;
            }

        }

        return true;

    }

    public int  GetReg_Acc() { return reg_accumulator; }

    }
}