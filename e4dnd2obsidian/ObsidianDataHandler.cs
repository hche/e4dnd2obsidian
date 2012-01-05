using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OAuth;

namespace e4dnd2obsidian
{

    /// <summary>
    /// Kapselt die API-Aufrufe für das ObsidianPortal mit der OAuth-Schnittstelle. 
    /// </summary>
    public class ObsidianDataHandler
    {
        ObsidianOAuth auth;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        string URL_GET_USERINFO = "http://api.obsidianportal.com/v1/users/me.json";
        string URL_GET_CHARACTERS_FOR_CAMPAIGN = "http://api.obsidianportal.com/v1/campaigns/{0}/characters.json";
        string URL_CREATE_CHARACTER = "http://api.obsidianportal.com/v1/campaigns/{0}/characters.json";
        string URL_UPDATE_CHARACTER = "http://api.obsidianportal.com/v1/campaigns/{0}/characters/{1}.json";

        public ObsidianDataHandler(ObsidianOAuth _auth)
        {
            this.auth = _auth;
        }

        public List<campaign> getCampaignsForUser()
        {
            string str_userinfo = auth.ExecuteOAuth(ObsidianOAuth.HttpMethod.GET, URL_GET_USERINFO, "");
            user userdata = serializer.Deserialize<user>(str_userinfo);
            return userdata.campaigns;
        }

        public List<character> getCharactersForCampaign(campaign cmp)
        {
            List<character> character_list = new List<character>();
            if (String.IsNullOrEmpty(cmp.id))
                return character_list;

            string url = String.Format(URL_GET_CHARACTERS_FOR_CAMPAIGN, cmp.id);
            string str_characters = auth.ExecuteOAuth(ObsidianOAuth.HttpMethod.GET, url, "");
            
            Object o = serializer.Deserialize<Object>(str_characters);
            System.Object[] olist = (System.Object[])o;

            foreach (Object obj in olist)
            {
                Dictionary<string, object> chr_data = (Dictionary<string, object>)obj;
                character chr = new character();
                chr.id = (string)chr_data["id"];
                chr.name =  (string)chr_data["name"];

                Dictionary<string, object> campaign = (Dictionary<string, object>)chr_data["campaign"];
                chr.campaign_id = (string)campaign["id"];

                character_list.Add(chr);
            }

            return character_list;
        }

        public void createNewCharakter(character charakterData)
        {
            string url = String.Format(URL_CREATE_CHARACTER, charakterData.campaign_id);
            string str_character = serializer.Serialize(charakterData);
            str_character = str_character.Replace("e4class", "class");
            //str_character = Regex.Replace(str_character, @"""campaign_id"":""[a-z,0-9]*"",", "", RegexOptions.None);
            str_character = Regex.Replace(str_character, @"""id"":null,", "", RegexOptions.None);
            str_character = Regex.Replace(str_character, @"null", @"""""", RegexOptions.None);
            str_character = @"{""character"" : " + str_character + "}";
            
            File.WriteAllText("jsonout.txt", str_character);

            str_character = File.ReadAllText("jsonin.txt");
            //str_character = str_character.Replace('"','\'');

            

            auth.ExecuteOAuth(ObsidianOAuth.HttpMethod.POST, url, str_character);
        }
        
        public void updateCharakter(character characterData)
        {
            string url = String.Format(URL_UPDATE_CHARACTER, characterData.campaign_id, characterData.id);
            string str_character = serializer.Serialize(characterData);

            //str_character = File.ReadAllText("jsonin.txt");

            str_character = @"{""character"":" + str_character + "}";

            url = "http://api.obsidianportal.com/v1/campaigns/80c7c818f24411dfba8140403656340d/characters/ef04f3086d2511e0b36340403656340d.json";
            auth.ExecuteOAuth(ObsidianOAuth.HttpMethod.PUT, url, str_character);

        }


        public void test(string json)
        {                                                          
            //string u = "http://api.obsidianportal.com/v1/campaigns/80c7c818f24411dfba8140403656340d/characters.json";
            //auth.ExecuteOAuth(ObsidianAuth.HttpMethod.POST, u, json);


            // http://api.obsidianportal.com/v1/campaigns/80c7c818f24411dfba8140403656340d/characters/ef04f3086d2511e0b36340403656340d.json
            string u = "http://api.obsidianportal.com/v1/campaigns/80c7c818f24411dfba8140403656340d/characters/ef04f3086d2511e0b36340403656340d.json";
            auth.ExecuteOAuth(ObsidianOAuth.HttpMethod.PUT, u, json);

        }


   


    }
}
