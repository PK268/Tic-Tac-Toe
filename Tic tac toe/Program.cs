using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.IO.Pipes;
using System.Threading;

namespace Tic_tac_toe
{

    class Program
    {
        public static int tempGen;
        public static Random gen = new Random();
        
        static bool[,] possibleArray =
        {
            //combinations\/
            /*1*/{true,true,true,false,false,false,false,false,false},
            /*2*/{false,false,false,true,true,true,false,false,false},
            /*3*/{false,false,false,false,false,false,true,true,true},
            /*4*/{true,false,false,true,false,false,true,false,false},
            /*5*/{false,true,false,false,true,false,false,true,false},
            /*6*/{false,false,true,false,false,true,false,false,true},
            /*7*/{true,false,false,false,true,false,false,false,true},
            /*8*/{false,false,true,false,true,false,true,false,false}
        };
        public static int[] boardSlots = new int[9];
        
        static int checkAlmostWin(int player)
        {
            
            int count = 0;
            int missing = 0;
            for (int combinationSlot = 0; combinationSlot < 8; combinationSlot++)
            {
                count = 0;
                for (int position = 0; position < 8; position++)
                {
                    if (possibleArray[combinationSlot, position] == true && boardSlots[position] == player)
                    {
                        count++;
                    }
                    if (count == 2)
                    {
                        //if (position + 1 < 9)
                        //{
                            if (possibleArray[combinationSlot, position + 1] == true)
                            {
                                missing = position+1;
                                Console.Clear();
                                if (boardSlots[missing] == 3)
                                {
                                    return missing;
                                }
                            }
                        //}
                    }
                }
            }
            return -1;
        }
        
        public static void oTurn()
        {
            
            int checAlmostwinResult1 = checkAlmostWin(1);
            int checAlmostwinResult2 = checkAlmostWin(2);
            if (checAlmostwinResult2 != -1)
            {
                boardSlots[checAlmostwinResult2] = 2;
            }
            if (checAlmostwinResult1 != -1)
            {
                boardSlots[checAlmostwinResult1] = 2;
            }
            else
            {
                tempGen = gen.Next(1, 9);
                while (boardSlots[tempGen] != 3)
                {
                    tempGen = gen.Next(1, 9);
                }
                boardSlots[tempGen] = 2;
            }
        }
        static bool checkWin(int type)
        {

            int count = 0;
            for (int combinationSlot = 0; combinationSlot < 8; combinationSlot++)
            {
                count = 0;
                for (int position = 0; position < 9; position++)
                {
                    if (possibleArray[combinationSlot, position] == true && boardSlots[position] == type)
                    {
                        count++;
                    }

                    else
                    {

                    }
                }
                if (count == 3)
                {
                    return true;
                }

            }
            return false;
        }
        public static void fillBoardSlots()
        {
            
            for (int i = 0; i < boardSlots.Length; i++)
            {
                boardSlots[i] = 3;
            }
        }

        static bool ifFBSUsed = false;
        public static void checkSlot(int position)
        {

            if (boardSlots[position] == 1)
            {
                Console.Write("X");
            }
            else if (boardSlots[position] == 2)
            {
                Console.Write("O");
            }
            else
            {
                Console.Write($"{position + 1}");
            }
        }
        static void drawGame()
        {
            if (ifFBSUsed == false)
            {
                fillBoardSlots();
                ifFBSUsed = true;
            }


            for (int i = 0; i < 9; i = i + 3)
            {
                checkSlot(i);
                Console.Write("|");
                checkSlot(i + 1);
                Console.Write("|");
                checkSlot(i + 2);
                Console.WriteLine();
                Console.WriteLine("-----");
            }
        }
        public static bool gameEnd = false;
        static void Main(string[] args)
        {

            while (gameEnd == false)
            {
                int playerNumber = 1;
                int slot;

                ifFBSUsed = false;

                Console.WriteLine("Play with computer (cpu), another player (plyr), or exit (exit)");
                string ans = Console.ReadLine();
                if (ans == "cpu")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        
                        Console.Clear();
                        Console.WriteLine();
                        drawGame();
                        Console.WriteLine();
                        Console.WriteLine("Where do you want to place");
                        slot = int.Parse(Console.ReadLine());
                        while (boardSlots[slot - 1] == 2)
                        {
                            Console.WriteLine("O has gone here already, Where do you want to place");
                            slot = int.Parse(Console.ReadLine());
                        }
                        while (boardSlots[slot-1] == 1)
                        {
                            Console.WriteLine("You have already gone here, Where do you want to place");
                            slot = int.Parse(Console.ReadLine());
                        }
                        boardSlots[slot - 1] = 1;
                        Console.Clear();
                        drawGame();

                        oTurn();
                        
                        if (checkWin(1) == true)
                        {
                            Console.WriteLine("You win!");
                            Thread.Sleep(3000);
                            Console.Clear();
                            Console.WriteLine("Play with computer (cpu), another player (plyr), or exit (exit)");
                            ans = Console.ReadLine();
                        }
                        else if (checkWin(2) == true)
                        {
                            Console.WriteLine("CPU wins!");
                            Thread.Sleep(3000);
                            Console.Clear();
                            Console.WriteLine("Play with computer (cpu), another player (plyr), or exit (exit)");
                            ans = Console.ReadLine();
                        }
                    }
                    Console.WriteLine("Tie!");
                }
                else if (ans == "plyr")
                {
                    fillBoardSlots();
                    for (int i = 0; i < 9; i++)
                    {

                        Console.Clear();
                        Console.WriteLine();
                        drawGame();
                        Console.WriteLine();
                        if (playerNumber % 2 != 0)
                        {

                            Console.WriteLine("Player 1, where do you want to place");
                            slot = int.Parse(Console.ReadLine());
                            boardSlots[slot - 1] = 1;
                            Console.Clear();
                            drawGame();


                            for (int x = 0; x < 10000; x++)
                            {

                            }
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Player 2, where do you want to place");
                            slot = int.Parse(Console.ReadLine());
                            boardSlots[slot - 1] = 2;
                            Console.Clear();
                            drawGame();


                            for (int x = 0; x < 10000; x++)
                            {

                            }
                            Console.Clear();
                        }

                        playerNumber++;
                    }
                    if (checkWin(1) == true)
                    {
                        Console.WriteLine("You win!");
                    }
                    else
                    {
                        Console.WriteLine("CPU wins!");
                    }
                }
                else if (ans == "exit")
                {
                    gameEnd = true;
                }
                else
                {
                    Console.WriteLine("Please type a valid answer");
                }
            }
            
        }
        
    }
}
