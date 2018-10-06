using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


namespace Wooting_Detection
{

    class MainClass
    {
        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_kbd_connected")]
        public static extern bool Kbd_Connected();

        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_send_feature")]
        public static extern bool SwitchProfile(int commandId, int parameter0, int parameter1, int parameter2, int parameter3);

        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_reset")]
        public static extern bool rgb_reset();

        [DllImport("wootingrgb", EntryPoint = "wooting_rgb_direct_set_key")]
        public static extern bool SetKey(byte row, byte column, byte red, byte green, byte blue);



        public static void Main(string[] args)
        {
            Console.WriteLine(Kbd_Connected());
            //SetKey(0, 0, 255, 255, 255);
            //SwitchProfile(23, 0, 0, 0, 1);

            int present = 0;

            for (int i = 0; i < 64; i++)
            {

                if (Process.GetProcessesByName("ForzaHorizon4").Length > 0)
                {
                    present = 1;
                }
                else if (Process.GetProcessesByName("ForzaHorizon3").Length > 0)
                {
                    present = 2;
                }
                else if (Process.GetProcessesByName("TheCrew2").Length > 0)
                {
                    present = 3;
                }
                else
                {
                    present = 0;
                }

                int countcol = 20;
                int countrow = 5;
                byte ic = 0;
                byte ir = 0;

                    switch (present)
                {
                    case 1:
                            Console.WriteLine("Switch profile 1");

                            SwitchProfile(23, 1, 0, 0, 1);

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
                            SetKey(ir, ic, 0, 255, 0);
                            for (ir = 0; ir <= countrow; ir++)
                            {
                                SetKey(ir, ic, 0, 255, 0);
                            }
                        }

                        break;
                    case 3:
                            Console.WriteLine("Switch profile 3");
                            SwitchProfile(23, 0, 0, 0, 3);
                        
                        for (ic = 0; ic <= countcol; ic++)
                        {
                            SetKey(ir, ic, 255, 0, 0);
                            for (ir = 0; ir <= countrow; ir++)
                            {
                                SetKey(ir, ic, 255, 0, 0);
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
                Thread.Sleep(1500);
            }

        }

    }

}
