using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using e4dnd2obsidian;

namespace e4dnd2obsidian
{
    public class ContextObsidian
    {
        public IStateObsidian state { get; set; }
        public ObsidianDataHandler datahandler { get; set; }
        public campaign selectedCampaign { get; set; }
        public character selectedCharacter { get; set; }
        public character uploadedCharacterData { get; set; }

        public void setCharacter(string chrid)
        {
            state.SetCharacter(this, chrid);
        }

        public void setCampaign(string cmpid)
        {
            state.SetCampaign(this, cmpid);
        }

        public void setUpdateData(XElement updatedata)
        {
            // XElement in ein character Struct konvertieren
            e4dnd2obsidian.e4DndCharakter cnv = new e4dnd2obsidian.e4DndCharakter(updatedata);
            character chrdata = cnv.getCharakterData();
            state.SetUpdateData(this, chrdata);
        }

        public void doUpdate()
        {
            state.DoUpdate(this);
        }
    }

    
}