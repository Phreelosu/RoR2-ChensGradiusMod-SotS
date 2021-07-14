﻿namespace Chen.GradiusMod.Drones.LaserDrone
{
    internal class CoreFlicker : SineFlicker
    {
        public float _baseValue = 1.2f;
        public float _amplitude = .2f;
        public float _frequency = .3f;

        public override float baseValue => _baseValue;
        public override float amplitude => _amplitude;
        public override float frequency => _frequency;
    }
}