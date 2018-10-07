using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Text;

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
            var Game1 = @"Forza Horizon 4";
            var Game2 = @"The Crew 2";
            var Game3 = @"Spotify";

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

                if (buff.ToString() == Game1)
                {
                    present = 1;
                }
                else if (buff.ToString() == Game2)
                {
                    present = 2;
                }
                else if (buff.ToString() == Game3)
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

                        break;
                    case 2:
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

                        break;
                    case 3:
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

                        break;

                    default:
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

                        break;
                }

                Thread.Sleep(1000);
            }


        }
    }
}