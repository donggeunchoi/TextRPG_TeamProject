using System;

namespace IPG
{
    internal class Ending
    {
        public static void StartEnding()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeEffect($"\n고맙네! {GameManager.PlayerController.Name}, 자네 덕분에 드디어 IPG 세상은 평화를 되찾았소.");
            Thread.Sleep(1000);
            TypeEffect("\n지금까지 수고 많았네.");
            Thread.Sleep(1000);
            TypeEffect("\n보상은 이것이오.");
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.ReadKey(true);
            Credit();
        }

        static void TypeEffect(string text, int delay = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void Credit()
        {
            Console.Clear();

            static void Animation(string[] lines)
            {
                Console.SetCursorPosition(0, 0);
                foreach (var line in lines)
                    Console.WriteLine(line);
            }

            string[] frame1 = new[]
            {
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                 1                                                                                      ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                         1                              ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        "
            };

            string[] frame2 = new[]
            {
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                            1                           ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                 1                                                                                      ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        ",
                "                                                                                                                        "
            };

            while (true)
            {
                Animation(frame1);
                Thread.Sleep(1000);
                Animation(frame2);
                Thread.Sleep(1000);
            }
        }
    }
}