using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public class Snake : Point
    {
        private Queue<Point> snakeElements;
        private const char snakeSymbol = '\u25CF';
        private Food[] foods;
        private Wall wall;
        private int foodIndex;

        public Snake(Wall wall, int leftX, int topY)
            : base(leftX, topY)
        {
            this.wall = wall;
            snakeElements = new Queue<Point>();
            CreateSnake();
            foods = new Food[]
            {
                new FoodAsterisk(wall),
                new FoodDollar(wall),
                new FoodHash(wall)
            };
            foodIndex = new Random().Next(0, 3);
            foods[foodIndex].SetRandomPosition(snakeElements);
        }

        public int ResultPoints { get; private set; }

        private void CreateSnake()
        {
            for (int i = 1; i <= 6; i++)
            {
                snakeElements.Enqueue(new Point(i, 1));
                Draw(i, 1, snakeSymbol);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            LeftX = direction.LeftX + snakeHead.LeftX;
            TopY = direction.TopY + snakeHead.TopY;
        }

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = snakeElements.Last();

            GetNextPoint(direction, currentSnakeHead);

            Point newSnakeHead = new Point(LeftX, TopY);

            if (LeftX == 0 || TopY == 0 ||
                LeftX == wall.LeftX - 1 || TopY == wall.TopY)
            {
                return false;
            }

            if (snakeElements.Any(x => x.LeftX == LeftX && x.TopY == TopY))
            {
                return false;
            }

            snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(snakeSymbol);

            if (foods[foodIndex].IsFoodPoint(newSnakeHead))
            {
                ResultPoints += foods[foodIndex].FoodPoints;
                foodIndex = new Random().Next(0, 3);
                foods[foodIndex].SetRandomPosition(snakeElements);
            }
            else
            {
                Point tail = snakeElements.Dequeue();
                tail.Draw(' ');
            }

            return true;
        }
    }
}
