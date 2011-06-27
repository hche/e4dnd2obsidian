using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace e4dnd2obsidian
{
    public class e4CharakterConverter
    {
        private XElement _e4charakter;

        public e4CharakterConverter(XElement e4charakter)
        {
            this._e4charakter = e4charakter;
        }

        public character getCharakterData()
        {
            character chr = new character();
            chr.name = _e4charakter.Element("CharacterSheet").Element("Details").Element("name").Value;
            chr.dynamic_sheet = createDynamicSheet();
            return chr;
        }
        
        private dynamicSheet createDynamicSheet()
        {
            dynamicSheet dynSheet = new dynamicSheet();

            var Details = _e4charakter.Element("CharacterSheet").Element("Details").Elements();
            var RulesElements = _e4charakter.Element("CharacterSheet").Element("RulesElementTally").Elements("RulesElement");

            dynSheet.race = RulesElements.First(rl => rl.Attribute("type").Value == "Race").Attribute("name").Value;
            dynSheet.e4class = RulesElements.First(rl => rl.Attribute("type").Value == "Class").Attribute("name").Value;
            dynSheet.level = Details.First(elem => elem.Name == "Level").Value;

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

            return dynSheet;
        }

        private string getStatValue(string statAlias)
        {
            string statValue = "";
            var elem = from stat in _e4charakter.Element("CharacterSheet").Element("StatBlock").Elements("Stat")
                       where stat.Elements("alias").FirstOrDefault(al => al.Attribute("name").Value == statAlias) != null
                       select stat.Attribute("value").Value;
            statValue = elem.First();
            return statValue;
        }
    }
}
