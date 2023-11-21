﻿using UnityEngine;

namespace _Game.Core.Services.Random
{
    public interface IRandomService : IService
    {
        int Next(int lootMin,int lootMax);
        float Next(float min, float max);
        float GetValue();
        Vector3 OnUnitSphere();
    }
}