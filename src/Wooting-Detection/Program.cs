﻿using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Linq;

namespace Wooting_Detection
{

    class MainClass
    {
        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_kbd_connected")]
        public static extern bool Kbd_Connected();

        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_send_feature")]
        public static extern bool SwitchProfile(int commandId, int parameter0, int parameter1, int parameter2, int parameter3);

        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_direct_set_key")]
        public static extern bool SetKey(byte row, byte column, byte red, byte green, byte blue);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static void Main(string[] args)
        {
            Console.WriteLine(Kbd_Connected());
            int chars = 256;
            int present = 0;
            string[] Game1 = { "Forza Horizon 4", "Forza Horizon 3" };
            string[] Game2 = { "TheCrew2" };
            string[] Game3 = { "Spotify" };
            int GameChange = 0;

            int countcol = 20;
            int countrow = 5;
            byte ic = 0;
            byte ir = 0;

            StringBuilder buff = new StringBuilder(chars);
            while (true)
            {
                IntPtr handle = GetForegroundWindow();

                if (GetWindowText(handle, buff, chars) > 0)
                {
                    Console.WriteLine(buff.ToString());
                }

                if (Game1.Contains(buff.ToString()))
                {
                    present = 1;
                }
                else if (Game2.Contains(buff.ToString()))
                {
                    present = 2;
                }
                else if (Game3.Contains(buff.ToString()))
                {
                    present = 3;
                }
                else
                {
                    present = 0;
                }

                switch (present)
                {
                    case 1:
                        Console.WriteLine("Profile 1");
                        if (!(GameChange == 1))
                        {
                            Console.WriteLine("Switch profile 1");

                            SwitchProfile(23, 0, 0, 0, 1);

                            for (ic = 0; ic <= countcol; ic++)
                            {
                                SetKey(ir, ic, 0, 0, 255);
                                for (ir = 0; ir <= countrow; ir++)
                                {
                                    SetKey(ir, ic, 0, 0, 255);
                                }
                            }
                            GameChange = 1;
                        }

                        break;
                    case 2:
                        Console.WriteLine("Profile 2");
                        if (!(GameChange == 2))
                        {
                            Console.WriteLine("Switch profile 2");

                            SwitchProfile(23, 0, 0, 0, 2);

                            for (ic = 0; ic <= countcol; ic++)
                            {
                                SetKey(ir, ic, 255, 0, 0);
                                for (ir = 0; ir <= countrow; ir++)
                                {
                                    SetKey(ir, ic, 255, 0, 0);
                                }
                            }
                            GameChange = 2;
                        }

                        break;
                    case 3:
                        Console.WriteLine("Profile 3");
                        if (!(GameChange == 3))
                        {
                            Console.WriteLine("Switch profile 3");

                            SwitchProfile(23, 0, 0, 0, 3);

                            for (ic = 0; ic <= countcol; ic++)
                            {
                                SetKey(ir, ic, 0, 255, 0);
                                for (ir = 0; ir <= countrow; ir++)
                                {
                                    SetKey(ir, ic, 0, 255, 0);
                                }
                            }
                            GameChange = 3;
                        }

                            break;

                    default:
                        Console.WriteLine("Default Profile");
                        if (!(GameChange == 0))
                        {
                            Console.WriteLine("Switch profile default");
                            Console.WriteLine("Not Running");
                            SwitchProfile(23, 0, 0, 0, 0);

                            for (ic = 0; ic <= countcol; ic++)
                            {
                                SetKey(ir, ic, 255, 255, 255);
                                for (ir = 0; ir <= countrow; ir++)
                                {
                                    SetKey(ir, ic, 255, 255, 255);
                                }
                            }
                            GameChange = 0;
                        }

                        break;
                }

                Thread.Sleep(1000);
            }


        }
    }
}