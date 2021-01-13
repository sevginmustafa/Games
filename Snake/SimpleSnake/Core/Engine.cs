using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Threading;

namespace SimpleSnake.Core
{
    public class Engine
    {
        private Direction direction;
        private Point[] directionPoints;
        private Snake snake;

        public Engine(Snake snake)
        {
            this.snake = snake;
            directionPoints = new Point[]
            {
                new Point(1,0),
                new Point(-1,0),
                new Point(0,1),
                new Point(0,-1)
            };
        }

        public void Run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = snake.IsMoving(directionPoints[(int)direction]);

                if (!isMoving)
                {
                    Console.SetCursorPosition(61, 1);
                    Console.WriteLine($"Result: {snake.ResultPoints}");
                    AskUserForRestart();
                }

                Thread.Sleep(100);
            }
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo input = Console.ReadKey();

            if (input.Key == ConsoleKey.RightArrow)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            else if (input.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            else if (input.Key == ConsoleKey.DownArrow)
            {
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
            }
            else if (input.Key == ConsoleKey.UpArrow)
            {
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
            }
        }

        private void AskUserForRestart()
        {
            Console.SetCursorPosition(61, 2);

            Console.Write("Would you like to continue? y/n");

            Console.SetCursorPosition(61, 3);
            string input = Console.ReadLine();

            if (input == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                Console.SetCursorPosition(61, 4);
                Console.WriteLine("Game Over!");
                Environment.Exit(0);
            }
        }
    }
}
