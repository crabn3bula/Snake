﻿using System;
using Core;

namespace LauncherGL
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SnakeGame())
            {
                game.Run();
            }
        }
    }
}
