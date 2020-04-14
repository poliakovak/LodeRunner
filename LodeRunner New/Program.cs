using System;

namespace LodeRunner_New
{
    class Program
    {
        static int CountGold(char[,] field)
        {
            int sum = 0;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == '@')
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
        static void Main(string[] args)
        {
            char player = 'I';
            char brick = '#';
            char stair = '|';
            char gold = '@';
            DateTime time = DateTime.Now;
            char[,] field = new char[,] { 
                                        {' ',' ',' ','@',' ',' ',' ',' ',' ',' ',' ','#'},
                                        {'#','#','|','#','#','#','#','#','#','#','#','#'},
                                        {' ',' ','|','@','#',' ',' ',' ','@',' ',' ','#'},
                                        {'#','#','#','#','#','#','#','|','#','#','#','#'},
                                        {' ','@',' ',' ',' ',' ',' ','|',' ',' ',' ','#'},
                                        {'#','#','#','#','#','#','#','#','#','#','#','#'}, 
                                                                                        };

            int width = field.GetLength(1);
            int height = field.GetLength(0);
            int numberOfGold = CountGold(field);
            int posX = 0;
            int posY = 0;

            field[posY, posX] = player;
            int points = 0;
            bool gameEnded = false;
            bool gameLeft = false;
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine("                          RULES");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"Collect @ to win\nUse arrow keys to move\nUse | to go up and down\nYou can dig # to jump down (Z - dig left; X - dig right)\nNumber of gold to get: {numberOfGold}\nGood luck!\nPress any key to start");
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write(field[i, j]);
                    }
                    Console.WriteLine();
                }

                if (gameEnded)
                {
                    Console.Beep();
                    Console.Clear();
                    Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("Congratulations, you won!\n");
                    Console.Write($"Your time: {DateTime.Now - time}");
                    Console.Write("\n\n\n\n\n\n\n\n");
                    Console.ReadKey();
                    break;
                }

                if(gameLeft)
                {
                    Console.Beep();
                    Console.Clear();
                    Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("You finished the game too early");
                    Console.ReadKey();
                    break;
                }

                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.RightArrow:
                        if (posX != width - 1 && field[posY, posX + 1] != brick)
                        {
                            if(field[posY+1,posX+1] == ' ')
                            {
                                field[posY, posX] = ' ';
                                posX += 1;
                                posY += 2;
                                if (field[posY, posX] == gold)
                                {
                                    points++;
                                    if (points == numberOfGold)
                                    {
                                        gameEnded = true;
                                    }
                                }
                                
                                field[posY, posX] = player;
                                
                                field[posY - 1, posX] = '#';
                            }
                            else
                            {
                                if(posY-1 >= 0 && field[posY-1, posX] == stair)
                                {
                                    field[posY, posX] = stair;
                                }
                                else
                                {
                                    field[posY, posX] = ' ';
                                }
                                
                                posX += 1;

                                if (field[posY, posX] == gold)
                                {
                                    points++;
                                    if (points == numberOfGold)
                                    {
                                        gameEnded = true;
                                    }
                                }
                                field[posY, posX] = player;
                            }
                            
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (posX != 0 && field[posY, posX - 1] != brick)
                        {

                            if (field[posY + 1, posX - 1] == ' ')
                            {
                                field[posY, posX] = ' ';
                                posX -= 1;
                                posY += 2;
                                if (field[posY, posX] == gold)
                                {
                                    points++;
                                    if (points == numberOfGold)
                                    {
                                        gameEnded = true;
                                    }
                                }
                                field[posY, posX] = player;
                                field[posY - 1, posX] = '#';
                            }
                            else
                            {
                                if (posY - 1 >= 0 && field[posY - 1, posX] == stair)
                                {
                                    field[posY, posX] = stair;
                                }
                                else
                                {
                                    field[posY, posX] = ' ';
                                }

                                posX -= 1;

                                if (field[posY, posX] == gold)
                                {
                                    points++;
                                    if (points == numberOfGold)
                                    {
                                        gameEnded = true;
                                    }
                                }
                                field[posY, posX] = player;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (posY != 0 && field[posY - 1, posX] == stair)
                        {
                            field[posY, posX] = '|';
                            posY -= 2;                     
                            field[posY, posX] = player;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (posY != height - 1 && field[posY + 1, posX] == stair)
                        {
                            field[posY, posX] = ' ';
                            posY += 2;
                            field[posY, posX] = player;
                        }
                        break;
                    case ConsoleKey.Z:
                        if(posX != 0 && field[posY+1, posX-1] == brick && posY + 1 < height - 1)
                        {
                            field[posY + 1, posX - 1] = ' ';
                            Console.Beep();
                        }

                        break;
                    case ConsoleKey.X:
                        if (posX != width-1 && field[posY + 1, posX + 1] == brick && posY + 1 < height - 1)
                        {
                            field[posY + 1, posX + 1] = ' ';
                            Console.Beep();
                        }
                        break;
                    case ConsoleKey.Escape:
                        gameLeft = true;
                        break;
                }

               
            }
        }
    }
}
