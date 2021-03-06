﻿using System;

namespace Ornette.Application.Infra
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _Random = new Random(new DateTime().Millisecond);

        public int Next(int maxValue)
        {
            return _Random.Next(maxValue);
        }
    }
}
