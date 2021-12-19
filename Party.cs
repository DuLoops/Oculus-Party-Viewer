using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Test
{
    public class Party
    {
        private int PartySize = 1;

        public List<Player_Character> Members { get; private set; }

        public List<float> AverageSaves { get; private set; }

        public string name { get; set; }

        public int Level { get; private set; }

        public int AverageArmorClass { get; private set; }

        public int AverageToHit { get; private set; }

        public int AverageHP { get; private set; }

        public float HealingPerRound { get; private set; }

        public int BaseHitPoints { get; private set; }

        public float EffectiveHitPoints { get; private set; }

        public Party()
        {
            Members = new List<Player_Character>();
            AverageSaves = new List<float>();
        }

        public Party(string FilePath)
        {
            Members = new List<Player_Character>();
            AverageSaves = new List<float>();
            XmlReader PartyReader = XmlReader.Create(FilePath);
            while (PartyReader.Read())
            {
                switch (PartyReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (PartyReader.Name == "Party")
                        {
                            break;
                        }
                        Player_Character player = new Player_Character(PartyReader);
                        Members.Add(player);
                        break;
                    default:
                        break;
                }
            }
            Update();
        }

        public float DPR(Creature enemy)
        {
            float total = 0;
            foreach (Player_Character player in Members) {
                total += player.DPR(enemy);
            }
            return total;
        }

        public float DPR()
        {
            float total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.DPR();
            }
            return total;
        }

        public void Update()
        {
            float total = 0;
            for (int i = 0; i < 6; i++)
            {
                foreach (Player_Character player in Members)
                {
                    total += player.Saves[i];
                }
                AverageSaves.Add(total / Members.Count);
            }

            Level = Members[0].Level;

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.ArmorClass;
            }
            AverageArmorClass = (int)(total / Members.Count);

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.ToHit;
            }
            AverageToHit = (int)(total / Members.Count);

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.BaseHitPoints;
            }
            AverageHP = (int)(total / Members.Count);

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.AverageHealing;
            }
            HealingPerRound = total;

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.BaseHitPoints;
            }
            BaseHitPoints = (int)total;

            total = 0;
            foreach (Player_Character player in Members)
            {
                total += player.Avoidance * AverageHP;
            }
            EffectiveHitPoints = BaseHitPoints + HealingPerRound - total;
        }

        public override string ToString()
        {
            string output = "";
            foreach (Player_Character player in Members)
            {
                output += player.ToString() + "\n\n";
            }
            return output;
        }

        public void Save()
        {
            Update();
            SaveFileDialog SaveDialog = new SaveFileDialog()
            {
                DefaultExt = ".xml",
                Filter = "Party Viewer XML|*.xml|All files (*.*)|*.*"
            };
            if (SaveDialog.ShowDialog() == true)
            {
                XmlWriter PartySaver = XmlWriter.Create(SaveDialog.FileName);
                PartySaver.WriteStartDocument();
                PartySaver.WriteStartElement("Party");
                foreach (Player_Character player in Members)
                {
                    player.Save(PartySaver);
                }
                PartySaver.WriteEndElement();
                PartySaver.WriteEndDocument();
                PartySaver.Close();
            } 
        }
    }
}
