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
        
        private string getStatValue(string statAlias)
        {
            string statValue = "";
            var elem = from stat in _e4charakter.Element("CharacterSheet").Element("StatBlock").Elements("Stat")
                       where stat.Elements("alias").FirstOrDefault(al => al.Attribute("name").Value == statAlias) != null
                       select stat.Attribute("value").Value;
            statValue = elem.First();
            return statValue;
        }

        public dynamic_sheet getDynamicSheet()
        {
            dynamic_sheet dynSheet = new dynamic_sheet();

            var Details = _e4charakter.Element("CharacterSheet").Element("Details").Elements();
            var RulesElements = _e4charakter.Element("CharacterSheet").Element("RulesElementTally").Elements("RulesElement");

            dynSheet.dsf_race = RulesElements.First(rl => rl.Attribute("type").Value == "Race").Attribute("name").Value;
            dynSheet.dsf_class = RulesElements.First(rl => rl.Attribute("type").Value == "Class").Attribute("name").Value;
            dynSheet.dsf_level = Details.First(elem => elem.Name == "Level").Value;

            // Aibility Scores
            dynSheet.dsf_strength = getStatValue("Strength");
            dynSheet.dsf_constitution = getStatValue("Constitution");
            dynSheet.dsf_dexterity = getStatValue("Dexterity");
            dynSheet.dsf_intelligence = getStatValue("Intelligence");
            dynSheet.dsf_wisdom = getStatValue("Wisdom");
            dynSheet.dsf_charisma = getStatValue("Charisma");

            dynSheet.dsf_strength_mod_plus_half = getStatValue("Strength modifier");
            dynSheet.dsf_constitution_mod_plus_half = getStatValue("Constitution modifier");
            dynSheet.dsf_dexterity_mod_plus_half = getStatValue("Dexterity modifier");
            dynSheet.dsf_intelligence_mod_plus_half = getStatValue("Intelligence modifier");
            dynSheet.dsf_wisdom_mod_plus_half = getStatValue("Wisdom modifier");
            dynSheet.dsf_charisma_mod_plus_half = getStatValue("Charisma modifier");

            // Defenses
            dynSheet.dsf_ac = getStatValue("AC");
            dynSheet.dsf_fortitude = getStatValue("Fortitude");
            dynSheet.dsf_reflex = getStatValue("Reflex");
            dynSheet.dsf_will = getStatValue("Will");

            return dynSheet;
        }
    }
    
    public struct charakter
    {
        public string id;
        public string campaign_id;
        public string name;
        public dynamic_sheet dyn_sheet;
    }

    public struct dynamic_sheet
    {
        public string dsf_level;
        public string dsf_class;
        public string dsf_paragon_path;
        public string dsf_epic_destiny;
        public string dsf_xp;
        public string dsf_race;
        public string dsf_size;
        public string dsf_age;
        public string dsf_gender;
        public string dsf_height;
        public string dsf_weight;
        public string dsf_alignment;
        public string dsf_deity;
        public string dsf_affiliation;
        public string dsf_initiative;
        public string dsf_initiative_dex;
        public string dsf_initiative_half;
        public string dsf_initiative_misc;
        public string dsf_initiative_modifier_;
        public string dsf_strength;
        public string dsf_strength_mod;
        public string dsf_strength_mod_plus_half;
        public string dsf_constitution;
        public string dsf_constitution_mod;
        public string dsf_constitution_mod_plus_half;
        public string dsf_dexterity;
        public string dsf_dexterity_mod;
        public string dsf_dexterity_mod_plus_half;
        public string dsf_intelligence;
        public string dsf_intelligence_mod;
        public string dsf_intelligence_mod_plus_half;
        public string dsf_wisdom;
        public string dsf_wisdom_mod;
        public string dsf_wisdom_mod_plus_half;
        public string dsf_charisma;
        public string dsf_charisma_mod;
        public string dsf_charisma_mod_plus_half;
        public string dsf_hit_points;
        public string dsf_bloodied;
        public string dsf_surge_value;
        public string dsf_surges;
        public string dsf_hit_points_current;
        public string dsf_hit_points_temporary;
        public string dsf_surges_used;
        public string dsf_death_saves;
        public string dsf_resist_;
        public string dsf_save_modifier_;
        public string dsf_acrobatics;
        public string dsf_acrobatics_ability_plus_half;
        public string dsf_acrobatics_training;
        public string dsf_acrobatics_armor;
        public string dsf_acrobatics_misc;
        public string dsf_arcana;
        public string dsf_arcana_ability_plus_half;
        public string dsf_arcana_training;
        public string dsf_arcana_armor;
        public string dsf_arcana_misc;
        public string dsf_athletics;
        public string dsf_athletics_ability_plus_half;
        public string dsf_athletics_training;
        public string dsf_athletics_armor;
        public string dsf_athletics_misc;
        public string dsf_bluff;
        public string dsf_bluff_ability_plus_half;
        public string dsf_bluff_training;
        public string dsf_bluff_armor;
        public string dsf_bluff_misc;
        public string dsf_diplomacy;
        public string dsf_diplomacy_ability_plus_half;
        public string dsf_diplomacy_training;
        public string dsf_diplomacy_armor;
        public string dsf_diplomacy_misc;
        public string dsf_dungeoneering;
        public string dsf_dungeoneering_ability_plus_half;
        public string dsf_dungeoneering_training;
        public string dsf_dungeoneering_armor;
        public string dsf_dungeoneering_misc;
        public string dsf_endurance;
        public string dsf_endurance_ability_plus_half;
        public string dsf_endurance_training;
        public string dsf_endurance_armor;
        public string dsf_endurance_misc;
        public string dsf_heal;
        public string dsf_heal_ability_plus_half;
        public string dsf_heal_training;
        public string dsf_heal_armor;
        public string dsf_heal_misc;
        public string dsf_history;
        public string dsf_history_ability_plus_half;
        public string dsf_history_training;
        public string dsf_history_armor;
        public string dsf_history_misc;
        public string dsf_insight;
        public string dsf_insight_ability_plus_half;
        public string dsf_insight_training;
        public string dsf_insight_armor;
        public string dsf_insight_misc;
        public string dsf_intimidate;
        public string dsf_intimidate_ability_plus_half;
        public string dsf_intimidate_training;
        public string dsf_intimidate_armor;
        public string dsf_intimidate_misc;
        public string dsf_nature;
        public string dsf_nature_ability_plus_half;
        public string dsf_nature_training;
        public string dsf_nature_armor;
        public string dsf_nature_misc;
        public string dsf_perception;
        public string dsf_perception_ability_plus_half;
        public string dsf_perception_training;
        public string dsf_perception_armor;
        public string dsf_perception_misc;
        public string dsf_religion;
        public string dsf_religion_ability_plus_half;
        public string dsf_religion_training;
        public string dsf_religion_armor;
        public string dsf_religion_misc;
        public string dsf_stealth;
        public string dsf_stealth_ability_plus_half;
        public string dsf_stealth_training;
        public string dsf_stealth_armor;
        public string dsf_stealth_misc;
        public string dsf_streetwise;
        public string dsf_streetwise_ability_plus_half;
        public string dsf_streetwise_training;
        public string dsf_streetwise_armor;
        public string dsf_streetwise_misc;
        public string dsf_thievery;
        public string dsf_thievery_ability_plus_half;
        public string dsf_thievery_training;
        public string dsf_thievery_armor;
        public string dsf_thievery_misc;
        public string dsf_note_;
        public string dsf_ac;
        public string dsf_ac_ten_plus_half;
        public string dsf_ac_ability;
        public string dsf_ac_class;
        public string dsf_ac_feat;
        public string dsf_ac_enhancement;
        public string dsf_ac_misc_a;
        public string dsf_ac_misc_b;
        public string dsf_AC_modifier_;
        public string dsf_fortitude;
        public string dsf_fortitude_ten_plus_half;
        public string dsf_fortitude_ability;
        public string dsf_fortitude_class;
        public string dsf_fortitude_feat;
        public string dsf_fortitude_enhancement;
        public string dsf_fortitude_misc_a;
        public string dsf_fortitude_misc_b;
        public string dsf_fortitude_modifier_;
        public string dsf_reflex;
        public string dsf_reflex_ten_plus_half;
        public string dsf_reflex_ability;
        public string dsf_reflex_class;
        public string dsf_reflex_feat;
        public string dsf_reflex_enhancement;
        public string dsf_reflex_misc_a;
        public string dsf_reflex_misc_b;
        public string dsf_reflex_modifier_;
        public string dsf_will;
        public string dsf_will_ten_plus_half;
        public string dsf_will_ability;
        public string dsf_will_class;
        public string dsf_will_feat;
        public string dsf_will_enhancement;
        public string dsf_will_misc_a;
        public string dsf_will_misc_b;
        public string dsf_will_modifier_;
        public string dsf_action_points;
        public string dsf_milestones;
        public string dsf_actionpoint_effect_;
        public string dsf_race_feature_;
        public string dsf_class_feature_;
        public string dsf_feat_;
        public string dsf_language_;
        public string dsf_background_;
        public string dsf_speed;
        public string dsf_speed_base;
        public string dsf_speed_armor;
        public string dsf_speed_item;
        public string dsf_speed_misc;
        public string dsf_movement_mode_;
        public string dsf_passive_insight;
        public string dsf_passive_insight_skill;
        public string dsf_passive_perception;
        public string dsf_passive_perception_skill;
        public string dsf_sense_;
        public string dsf_attack_type_;
        public string dsf_attack_hit_;
        public string dsf_attack_hit_half_level_;
        public string dsf_attack_hit_ability_;
        public string dsf_attack_hit_class_;
        public string dsf_attack_hit_proficiency_;
        public string dsf_attack_hit_feat_;
        public string dsf_attack_hit_enhancement_;
        public string dsf_attack_hit_misc_;
        public string dsf_attack_damage_;
        public string dsf_attack_damage_ability_;
        public string dsf_attack_damage_feat_;
        public string dsf_attack_damage_enhancement_;
        public string dsf_attack_damage_misc_a_;
        public string dsf_attack_damage_misc_b_;
        public string dsf_atwill_power_;
        public string dsf_encounter_power_;
        public string dsf_daily_power_;
        public string dsf_item_;
        public string dsf_avatar_image;
        public string dsf_bio;
        //public string dsf_player;
        //public string dsf_dst_author;
    }
}
