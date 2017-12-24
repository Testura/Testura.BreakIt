﻿using System;

namespace Testura.FunkyValueTester.Combinations.CombinationLoggers
{
    public class ConsoleCombinationLogger : CombinationLogger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}