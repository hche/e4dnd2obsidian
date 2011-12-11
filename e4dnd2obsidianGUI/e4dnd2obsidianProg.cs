using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using e4dnd2obsidianGUI;
using e4dnd2obsidian;
using OAuth;

namespace e4dnd2obsidianGUI
{
    class e4dnd2obsidianProg
    {
        string CONSUMER_KEY;
        string CONSUMER_SECRET;
        private e4dnd2obsidian.ObsidianDataHandler chrHandler = null;

        public campaign selectedCampaign { get; set; }
        public character selectedCharacter { get; set; }
        public XElement loadedFileData { get; set; }
        public string loadedFilename { get; set; }

        private bool _loggedIn;
        public bool isLoggedIn
        {
            get
            {
                return _loggedIn;
            }
        }

        public e4dnd2obsidianProg()
        {
            CONSUMER_KEY = Properties.Settings.Default.CONSUMER_KEY;
            CONSUMER_SECRET = Properties.Settings.Default.CONSUMER_SECRET;
        }

        public void doLogin()
        {
            // OAuth Objekt erzeugen
            ObsidianOAuth oauth = new ObsidianOAuth();
            oauth.ConsumerKey = CONSUMER_KEY;
            oauth.ConsumerSecret = CONSUMER_SECRET;

            AuthWindow ie = new AuthWindow(oauth.GetAuthorizationUrl());
            if (ie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                oauth.OAuthVerifier = ie.Pin;
                oauth.GetAccessToken();
                chrHandler = new e4dnd2obsidian.ObsidianDataHandler(oauth);     // ObsidianDataHandler erzeugen und OAuth-Objekt übergeben.

                _loggedIn = true;
            }
            else
            {
                _loggedIn = false;
            }
        }

        public List<campaign> getCampaigns()
        {
            List<campaign> cmplist = chrHandler.getCampaignsForUser();
            return cmplist;
        }

        public List<character> getCharacters()
        {
            List<character> chrlist = chrHandler.getCharactersForCampaign(this.selectedCampaign);
            return chrlist;
        }

        // ToDo: Intern Liste mit Kampagnen und Charakteren vorhalten.
        public void setCharacter(string characterName)
        {
            List<character> chrs = getCharacters();
            foreach (character chr in chrs)
            {
                if (chr.name == characterName)
                {
                    this.selectedCharacter = chr;
                    break;
                }
            }
        }

        public void setCampaign(string campaignName)
        {
            List<campaign> cmps = getCampaigns();
            foreach (campaign cmp in cmps)
            {
                if (cmp.name == campaignName)
                {
                    this.selectedCampaign = cmp;
                    break;
                }
            }
        }

        public void setFile(string filename, XElement filedata)
        {
            this.loadedFilename = filename;
            this.loadedFileData = filedata;
        }

        public void updateCharacter()
        {
            e4dnd2obsidian.e4DndCharakter cnv = new e4dnd2obsidian.e4DndCharakter(this.loadedFileData);
            character chr = cnv.getCharakterData();
            chr.campaign_id = this.selectedCampaign.id;

            chrHandler.updateCharakter(chr);

        }

        public void createCharacter()
        {
            e4dnd2obsidian.e4DndCharakter cnv = new e4dnd2obsidian.e4DndCharakter(this.loadedFileData);
            character chr = cnv.getCharakterData();
            chr.campaign_id = this.selectedCampaign.id;

            chrHandler.createNewCharakter(chr);
        }

        public void test(string json)
        {

            chrHandler.test(json);

        }

        //public static bool CertValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //{
        //    return true;
        //}
    }
}
