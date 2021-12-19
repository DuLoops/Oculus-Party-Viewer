using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Test
{
    public class Player_Character : Creature
    {
        public int Level { get; private set; }
        public float Avoidance { get; set; }
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

        public Player_Character(XmlReader PartyReader)
        {
            while (PartyReader.MoveToNextAttribute())
            {
                switch (PartyReader.Name)
                {
                    case "Name":
                        Name = PartyReader.Value;
                        break;
                    case "Str":
                        Stats[(int)Test.Stats.STRENGTH] = int.Parse(PartyReader.Value);
                        break;
                    case "Dex":
                        Stats[(int)Test.Stats.DEXTERITY] = int.Parse(PartyReader.Value);
                        break;
                    case "Con":
                        Stats[(int)Test.Stats.CONSTITUTION] = int.Parse(PartyReader.Value);
                        break;
                    case "Int":
                        Stats[(int)Test.Stats.INTELLIGENCE] = int.Parse(PartyReader.Value);
                        break;
                    case "Wis":
                        Stats[(int)Test.Stats.WISDOM] = int.Parse(PartyReader.Value);
                        break;
                    case "Cha":
                        Stats[(int)Test.Stats.CHARISMA] = int.Parse(PartyReader.Value);
                        break;
                    case "StrSave":
                        Saves[(int)Test.Stats.STRENGTH] = int.Parse(PartyReader.Value);
                        break;
                    case "DexSave":
                        Saves[(int)Test.Stats.DEXTERITY] = int.Parse(PartyReader.Value);
                        break;
                    case "ConSave":
                        Saves[(int)Test.Stats.CONSTITUTION] = int.Parse(PartyReader.Value);
                        break;
                    case "IntSave":
                        Saves[(int)Test.Stats.INTELLIGENCE] = int.Parse(PartyReader.Value);
                        break;
                    case "WisSave":
                        Saves[(int)Test.Stats.WISDOM] = int.Parse(PartyReader.Value);
                        break;
                    case "ChaSave":
                        Saves[(int)Test.Stats.CHARISMA] = int.Parse(PartyReader.Value);
                        break;
                    case "ToHit":
                        ToHit = int.Parse(PartyReader.Value);
                        break;
                    case "AvgDmg":
                        AverageDamage = float.Parse(PartyReader.Value);
                        break;
                    case "FlatDmg":
                        FlatDamage = float.Parse(PartyReader.Value);
                        break;
                    case "AvgHeal":
                        AverageHealing = float.Parse(PartyReader.Value);
                        break;
                    case "BaseHP":
                        BaseHitPoints = int.Parse(PartyReader.Value);
                        break;
                    case "HP":
                        HitPoints = int.Parse(PartyReader.Value);
                        break;
                    case "AC":
                        ArmorClass = int.Parse(PartyReader.Value);
                        break;
                    case "UsesSave":
                        UsesSaves = bool.Parse(PartyReader.Value);
                        break;
                    case "HalfDmg":
                        HalfDamage = bool.Parse(PartyReader.Value);
                        break;
                    case "PrimarySave":
                        PrimarySave = (Test.Stats) Enum.Parse(typeof(Test.Stats), PartyReader.Value);
                        break;
                    case "SaveDC":
                        SaveDC = int.Parse(PartyReader.Value);
                        break;
                    case "Level":
                        Level = int.Parse(PartyReader.Value);
                        break;
                    case "Avoidance":
                        Avoidance = float.Parse(PartyReader.Value);
                        break;
                }
            }
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

        public override string ToString()
        {
            string output = Name + "\n";
            output += "Level " + Level + "\n";
            output += "Base HP: " + BaseHitPoints + "\n";
            output += base.ToString();
            output += "Avoidance: " + (Avoidance * 100) + "%\n";
            return output;
        }

        public void Save(XmlWriter Writer)
        {
            Writer.WriteStartElement("Player");
            Writer.WriteAttributeString("Name", Name);
            Writer.WriteAttributeString("Str", Stats[0].ToString());
            Writer.WriteAttributeString("Dex", Stats[1].ToString());
            Writer.WriteAttributeString("Con", Stats[2].ToString());
            Writer.WriteAttributeString("Int", Stats[3].ToString());
            Writer.WriteAttributeString("Wis", Stats[4].ToString());
            Writer.WriteAttributeString("Cha", Stats[5].ToString());
            Writer.WriteAttributeString("StrSave", Saves[0].ToString());
            Writer.WriteAttributeString("DexSave", Saves[1].ToString());
            Writer.WriteAttributeString("ConSave", Saves[2].ToString());
            Writer.WriteAttributeString("IntSave", Saves[3].ToString());
            Writer.WriteAttributeString("WisSave", Saves[4].ToString());
            Writer.WriteAttributeString("ChaSave", Saves[5].ToString());
            Writer.WriteAttributeString("ToHit", ToHit.ToString());
            Writer.WriteAttributeString("AvgDmg", AverageDamage.ToString());
            Writer.WriteAttributeString("FlatDmg", FlatDamage.ToString());
            Writer.WriteAttributeString("AvgHeal", AverageHealing.ToString());
            Writer.WriteAttributeString("HP", HitPoints.ToString());
            Writer.WriteAttributeString("BaseHP", BaseHitPoints.ToString());
            Writer.WriteAttributeString("AC", ArmorClass.ToString());
            Writer.WriteAttributeString("UsesSave", UsesSaves.ToString());
            Writer.WriteAttributeString("HalfDmg", HalfDamage.ToString());
            Writer.WriteAttributeString("PrimarySave", PrimarySave.ToString());
            Writer.WriteAttributeString("SaveDC", SaveDC.ToString());
            Writer.WriteAttributeString("Level", Level.ToString());
            Writer.WriteAttributeString("Avoidance", Avoidance.ToString());
            Writer.WriteEndElement();
        }
    }
}
