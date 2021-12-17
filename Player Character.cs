using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class Player_Character : Creature
    {
        public int Level { get; private set; }
        public float Avoidance { get; private set; }
        public string Name { get; private set; }

        public int BaseHitPoints { get; private set; }

        public Player_Character() : base()
        {
            Level = 0;
            Avoidance = 0;
            Name = "";
            BaseHitPoints = 0;
        }
        public Player_Character(string NewName) : base()
        {
            Name = NewName;
            Level = 1;
            Avoidance = 0;
            BaseHitPoints = 0;
        }

        internal float DPR()
        {
            float DPR = 0;
            if (UsesSaves)
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (Math.Floor(Level / 3.0) + i < SaveDC)
                    {
                        DPR += AverageDamage;
                    }
                    else if (HalfDamage)
                    {
                        DPR += (float)Math.Floor(AverageDamage / 2);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (i + ToHit > 10 + ToHit * 0.6)
                    {
                        if (i == 20)
                        {
                            DPR += AverageDamage;
                        }
                        DPR += AverageDamage + FlatDamage;
                    }
                }
            }
            return DPR;
        }
    }
}
