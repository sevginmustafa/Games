using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private Random random;
        private Wall wall;
        private char foodSymbol;

        protected Food(Wall wall, char foodSymbol, int foodPoints)
            : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            FoodPoints = foodPoints;
            random = new Random();
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snake)
        {
            LeftX = random.Next(1, wall.LeftX - 1);
            TopY = random.Next(1, wall.TopY - 1);

            bool isSnakePoint = snake.Any(x => x.LeftX == LeftX && x.TopY == TopY);

            while (isSnakePoint)
            {
                LeftX = random.Next(1, wall.LeftX - 1);
                TopY = random.Next(1, wall.TopY - 1);

                isSnakePoint = snake.Any(x => x.LeftX == LeftX && x.TopY == TopY);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.LeftX == LeftX && snake.TopY == TopY;
        }
    }
}
