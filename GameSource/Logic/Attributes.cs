using MinuteBattle.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinuteBattle.Logic
{
    public class Attributes
    {
        public float _health;
        public float _movementSpeed;
        public float _fireRate;
        public float _fireRange;
        public float _accuracy;
        public float _damage;
        public Attributes(float health, float movementSpeed, float fireRate, float fireRange, float accuracy, float damage) {
            _health = health;
            _movementSpeed = movementSpeed;
            _fireRate = fireRate;
            _fireRange = fireRange;
            _accuracy = accuracy;
            _damage = damage;
        }
    }
}
