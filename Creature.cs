using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Test
{
    public enum Stats
    {
        STRENGTH,
        DEXTERITY,
        CONSTITUTION,
        INTELLIGENCE,
        WISDOM,
        CHARISMA,
        NONE
    }

    public enum Difficulty
    {
        //Make your players feel strong
        EASY,
        //The average encounter in a dungeon, not life threatening but will consume their resources
        MEDIUM,
        //The party needs to spend their more powerful resources to succeed
        HARD,
        //At least 1 member of the party will die, unless everything is played perfectly
        DEADLY
    }

    public class Creature
    {
        //Array of all the stats in order of the Stats enum
        public List<int> Stats { get; private set; }

        //Array of all the saves in order of the Stats enum
        public List<int> Saves { get; private set; }

        public int ToHit { get; protected set; }

        //Average of damage dice, for casters takes average damage of highest, most numerous spell slot.
        public float AverageDamage { get; protected set; }

        //Flat damage bonuses
        public float FlatDamage { get; protected set; }

        //Healing over 5 rounds
        public float AverageHealing { get; protected set; }

        public int HitPoints { get; protected set; }

        //Average AC over the course of a fight, ignores 1 round buffs (i.e. Shield)
        public int ArmorClass { get; protected set; }

        public bool UsesSaves { get; protected set; }

        public bool HalfDamage { get; protected set; }

        public Stats PrimarySave { get; protected set; }

        public int SaveDC { get; protected set; }

        public Creature()
        {
            ArmorClass = 0;
            Stats = new List<int>(6);
            Saves = new List<int>(6);
            for (int i = 0; i < 6; i++)
            {
                Stats.Add(0);
                Saves.Add(0);
            }
            ToHit = 0;
            AverageDamage = 0;
            FlatDamage = 0;
            AverageHealing = 0;
            HitPoints = 0;
            ArmorClass = 0;
            UsesSaves = false;
            HalfDamage = false;
            PrimarySave = Test.Stats.NONE;
            SaveDC = 0;
        }

        public Creature(Party PlayerParty)
        {
            //Make these stats dependent on difficulty enum
            //Out of scope for now
            ArmorClass = (int) Math.Floor(PlayerParty.AverageToHit * 0.6);

            ToHit = (int) Math.Floor(PlayerParty.AverageArmorClass - 7.0);

            FlatDamage = PlayerParty.Level;

            AverageDamage = (float)(PlayerParty.AverageHP * 0.8 - FlatDamage);

            Saves = new List<int>(6);
            for (int i = 0; i < 6; i++)
            {
                Saves[i] = (int) Math.Floor(PlayerParty.Level / 3.0) + 1;
            }

            //Copy saves to stats, to ensure no stat spiking. Allow to be modifiable elsewhere to ensure proper creature design out of combat
            Stats= new List<int>(Saves);


            //This can result in a creature having 1 REALLY good save and 5 average saves, fix this later to have 2 good saves and 4 average saves
            Saves[Dice.Roll(1, 6)] += (int) Math.Floor(PlayerParty.Level / 3.0);
            Saves[Dice.Roll(1, 6)] += (int) Math.Floor(PlayerParty.Level / 3.0);

            SaveDC = (int) Math.Round(PlayerParty.AverageSaves[(int)PrimarySave]) + 13;

            switch (Dice.Roll(1, 7))
            {
                case 1:
                    PrimarySave = (Stats)1;
                    break;
                case 2:
                    PrimarySave = (Stats)2;
                    break;
                case 3:
                    PrimarySave = (Stats)3;
                    break;
                case 4:
                    PrimarySave = (Stats)4;
                    break;
                case 5:
                    PrimarySave = (Stats)5;
                    break;
                case 6:
                    PrimarySave = (Stats)6;
                    break;
                case 7:
                    PrimarySave = (Stats)7;
                    break;
            }

            if (Dice.Roll(1, 2) == 1)
            {
                UsesSaves = true;
                if (Dice.Roll(1, 2) == 1)
                {
                    HalfDamage = true;
                }
                else
                {
                    HalfDamage = false;
                }
            }
            else
            {
                UsesSaves = false;
                HalfDamage = false;
            }

            HitPoints = (int)Math.Ceiling(PlayerParty.DPR() * 6.0);

            AverageHealing = (int)Math.Floor(HitPoints / (2 * 6.0));
        }


        public float DPR(Party PlayerParty)
        {
            float DPR = 0;
            if (UsesSaves)
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (i + PlayerParty.AverageSaves[(int)PrimarySave] < SaveDC)
                    {
                        DPR += AverageDamage;
                    } else if (HalfDamage)
                    {
                        DPR += (float) Math.Floor(AverageDamage / 2);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (i + ToHit > PlayerParty.AverageArmorClass)
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

        public float DPR(Creature enemy)
        {
            float DPR = 0;
            if (UsesSaves)
            {
                for (int i = 1; i <= 20; i++)
                {
                    if (enemy.Saves[(int)PrimarySave] + i < SaveDC)
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
                    if (i + ToHit > enemy.ArmorClass + 10)
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

        override public string ToString() 
        {
            string output = "";
            output += "HP: " + HitPoints + "\n";
            output += "Armor Class: " + ArmorClass + "\n";
            for (int i = 0; i < 6; i++)
            {
                output += (Stats)i + ": " + Stats[i] + "\t\t";
                output += (Stats)i + " Save: " + Saves[i] + "\n";
            }
            output += "To-Hit: " + ToHit + "\n";
            output += "Average Damage: " + AverageDamage + "\n";
            output += "Flat Damage: " + FlatDamage + "\n";
            output += "Average Healing: " + AverageHealing + "\n";
            output += "Primary Save: " + PrimarySave + "\n";
            output += "Save DC: " + SaveDC + "\n";
            output += "Uses Saves: " + UsesSaves + "\n";
            output += "Half Damage: " + HalfDamage + "\n";
            return output;
        }
    }
}
