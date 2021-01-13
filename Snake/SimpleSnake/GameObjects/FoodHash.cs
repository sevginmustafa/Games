﻿namespace SimpleSnake.GameObjects
{
    public class FoodHash : Food
    {
        private const char foodSymbol = '#';
        private const int foodPoints = 3;

        public FoodHash(Wall wall)
            : base(wall, foodSymbol, foodPoints)
        {
        }
    }
}
