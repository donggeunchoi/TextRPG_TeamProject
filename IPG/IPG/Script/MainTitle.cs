using System;

namespace IPG
{
    internal static class MainTitle
    {
        public static void Title()
        {
            while (true)
            {
                Console.Clear();
                ShowMainTitle();
                Console.WriteLine();
                
                Console.WriteLine("                                                1. 새로운 모험\n\n");
                Console.WriteLine("                                                 2. 게임 종료");
                Console.Write("\n>> ");

                string input = Console.ReadLine()?.Trim();

                if (input == "1")
                {
                    GameManager.PlayerController.StartStory();
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("다음에 또 만나요.");
                    Environment.Exit(0);
                }
            }
        }

        public static void ShowMainTitle()
        {
            string[] MainTitle = new string[]
            {
                        "                                                                                                                    ",
                        "                                                                                                                    ",
                        "                                                                                                                    ",
                        "                                                                                                                    ",
                        "                -=.     .===-:   +#-   .==:                 :=*+=.    :==.                                          ",
                        "                @@:     =@@@@@%. #@=   +@@%                +@@@@@@    %@@+                                          ",
                        "                @@:     =@%::%@* *@=   %@@@: .-.   -:     =@@-..:=   :@@@%   .-.:=- .=-    :==:                     ",
                        "                @@:     =@%  +@# *@=  :@#+@+ :@%  =@*     %@+        +@+#@:  +@%@@@*@@@*  +@@@@*                    ",
                        "                @@:     =@%--%@* *@=  *@=.@%  #@: #@:    .@@: .***.  %@.=@*  +@@:+@@--@@ :@%. #@-                   ",
                        "                @@:     =@@@@@#. *@= .@@: %@- -@*.@%     .@@: :%@@: -@% :@@  +@* :@%  @@ =@@%%@@+                   ",
                        "                @@:     =@%--:   *@= =@@%%@@*  %@+@=      %@+   %@: *@@%%@@- +@* :@% .@@ =@%++==:                   ",
                        "                @@:     =@%      *@= #@%###@@. +@@@.      =@@=..%@: @@###%@# +@* :@% .@@ :@%.  -.                   ",
                        "                @@:     =@%      #@-.@@.   %@= .@@*        +@@@@@@.=@#   .@@.=@* :@% .@@. +@@@@@:                   ",
                        "                :-      .-:      :-..-:    .-:  #@-         .-==-  :-.    :-..-.  -:  ::   :=+=.                    ",
                        "                                              :+@%                                                                  ",
                        "                                              =%#:                                                                  ",
                        "                                                                                                                    ",
                        "                                                                                                                    ",
                        "                                                                                                                    "
            };
            foreach (string line in MainTitle)
            {
                Console.WriteLine(line);
            }
        }
    }
}