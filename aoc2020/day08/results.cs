using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day8
    {

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day8_Task1");

            var comp = new Computer();
            var program = comp.LoadProgram(@"./day08/input.txt");

            comp.Execute(program);

            Console.WriteLine("Acc :  {0}", comp.GetReg_Acc());

        }


        public static void Task2()
        {
            Console.WriteLine("AOC2020_Day8_Task2");

            var comp = new Computer();

            var prog = comp.LoadProgram(@"./day08/input.txt");

            for (int i =0; i < prog.Count; i++)
            {
                var prog_fixed = prog.Clone();

                switch(prog_fixed[i].OpCode)
                {
                    case Computer.eOpCode.nop: prog_fixed[i] = new Computer.Instruction() { OpCode = Computer.eOpCode.jmp, OpValue = prog_fixed[i].OpValue }; break;
                    case Computer.eOpCode.jmp: prog_fixed[i] = new Computer.Instruction() { OpCode = Computer.eOpCode.nop, OpValue = prog_fixed[i].OpValue }; break;
                } 

                if (comp.Execute(prog_fixed))
                {
                    Console.WriteLine("Acc :  {0}", comp.GetReg_Acc());   
                    return;
                }  
            }
            
            Console.WriteLine("Not found ???");
                     

        }        


    }
}