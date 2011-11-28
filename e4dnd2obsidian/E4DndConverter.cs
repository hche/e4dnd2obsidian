using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace e4dnd2obsidian
{
    public class e4DndCharakter
    {
        private XElement _e4charakterData;

        public e4DndCharakter(XElement characterData)
        {
            this._e4charakterData = characterData;
        }

        public character getCharakterData()
        {
            dynamicSheet dynSheet = new dynamicSheet();
            character chr = new character();
            chr.name = _e4charakterData.Element("CharacterSheet").Element("Details").Element("name").Value;
            
            var Details = _e4charakterData.Element("CharacterSheet").Element("Details").Elements();
            var RulesElements = _e4charakterData.Element("CharacterSheet").Element("RulesElementTally").Elements("RulesElement");

            dynSheet.race = RulesElements.First(rl => rl.Attribute("type").Value == "Race").Attribute("name").Value;
            dynSheet.e4class = RulesElements.First(rl => rl.Attribute("type").Value == "Class").Attribute("name").Value;
            dynSheet.level = Details.First(elem => elem.Name == "Level").Value;
            dynSheet.xp = Details.First(elem => elem.Name == "Experience").Value;
            dynSheet.speed = getStatValue("Speed");
            dynSheet.passive_perception = getStatValue("Passive Perception");
            dynSheet.passive_insight = getStatValue("Passive Insight");
            dynSheet.action_points = getStatValue("_BaseActionPoints");

            // Ability Scores
            dynSheet.strength = getStatValue("Strength");
            dynSheet.constitution = getStatValue("Constitution");
            dynSheet.dexterity = getStatValue("Dexterity");
            dynSheet.intelligence = getStatValue("Intelligence");
            dynSheet.wisdom = getStatValue("Wisdom");
            dynSheet.charisma = getStatValue("Charisma");

            dynSheet.strength_mod_plus_half = getStatValue("Strength modifier");
            dynSheet.constitution_mod_plus_half = getStatValue("Constitution modifier");
            dynSheet.dexterity_mod_plus_half = getStatValue("Dexterity modifier");
            dynSheet.intelligence_mod_plus_half = getStatValue("Intelligence modifier");
            dynSheet.wisdom_mod_plus_half = getStatValue("Wisdom modifier");
            dynSheet.charisma_mod_plus_half = getStatValue("Charisma modifier");

            // Defenses
            dynSheet.ac = getStatValue("AC");
            dynSheet.fortitude = getStatValue("Fortitude");
            dynSheet.reflex = getStatValue("Reflex");
            dynSheet.will = getStatValue("Will");

            // Skills
            dynSheet.acrobatics = getStatValue("Acrobatics");
            dynSheet.arcana = getStatValue("Arcana");
            dynSheet.athletics = getStatValue("Athletics");
            dynSheet.bluff = getStatValue("Bluff");
            dynSheet.diplomacy = getStatValue("Diplomacy");
            dynSheet.dungeoneering = getStatValue("Dungeoneering");
            dynSheet.endurance = getStatValue("Endurance");
            dynSheet.heal = getStatValue("Heal");
            dynSheet.history = getStatValue("History");
            dynSheet.insight = getStatValue("Insight");
            dynSheet.intimidate = getStatValue("Intimidate");
            dynSheet.nature = getStatValue("Nature");
            dynSheet.perception = getStatValue("Perception");
            dynSheet.religion = getStatValue("Religion");
            dynSheet.stealth = getStatValue("Stealth");
            dynSheet.streetwise = getStatValue("Streetwise");
            dynSheet.thievery = getStatValue("Thievery");

            return chr;
        }

        private string getStatValue(string statAlias)
        {
            string statValue = "";
            var elem = from stat in _e4charakterData.Element("CharacterSheet").Element("StatBlock").Elements("Stat")
                       where stat.Elements("alias").FirstOrDefault(al => al.Attribute("name").Value == statAlias) != null
                       select stat.Attribute("value").Value;
            statValue = elem.First();
            return statValue;
        }
    }
}
