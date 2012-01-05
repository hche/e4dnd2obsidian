using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e4dnd2obsidian;

namespace e4dnd2obsidian
{
    public class ObsidianStateLoggedIn : IStateObsidian
    {
        public override void SetCampaign(ContextObsidian context, string id)
        {
            List<campaign> cmplist = context.datahandler.getCampaignsForUser();
            IEnumerable<campaign> selCmp = from c in cmplist where c.id == id select c;
            if (selCmp.Count() > 0)
            {
                context.selectedCampaign = selCmp.First();
                context.selectedCharacter = new character();        // Wenn die Kampagne geändert wurde muss der zu aktualisierende Charakter neu ausgewählt werden.
                context.state = new ObsidianStateLoggedIn();        // Status nach Kampagnenauswahl immer LoggedIn
            }
        }

        public override void SetCharacter(ContextObsidian context, string id)
        {
            List<character> chrlist = context.datahandler.getCharactersForCampaign(context.selectedCampaign);
            IEnumerable<character> selChr = from c in chrlist where c.id == id select c;
            if (selChr.Count() > 0)
            {
                context.selectedCharacter = selChr.First();

                // Status prüfen: Wenn alle Daten vorhanden sind, bereit für Update. Kampagne wird nicht abgeprüft.
                if ((!String.IsNullOrEmpty(context.uploadedCharacterData.id)))
                    context.state = new ObsidianStateUpdateDataComplete();
                else
                    context.state = new ObsidianStateLoggedIn();
            }
        }

        public override void SetUpdateData(ContextObsidian context, character chr)
        {
            context.uploadedCharacterData = chr;

            // Status prüfen: Wenn alle Daten vorhanden sind, bereit für Update.
            if ((!String.IsNullOrEmpty(context.selectedCampaign.id)) &&
                (!String.IsNullOrEmpty(context.selectedCharacter.id)))
                context.state = new ObsidianStateUpdateDataComplete();
            else
                context.state = new ObsidianStateLoggedIn();
        }

        public override void DoUpdate(ContextObsidian context)
        {
            // Im Status LoggedIn nicht möglich (Daten nicht komplett)
        }
    }

    public class ObsidianStateUpdateDataComplete : IStateObsidian
    {
        public override void SetCampaign(ContextObsidian context, string id)
        {
            List<campaign> cmplist = context.datahandler.getCampaignsForUser();
            IEnumerable<campaign> selCmp = from c in cmplist where c.id == id select c;
            if (selCmp.Count() > 0)
            {
                context.selectedCampaign = selCmp.First();
                context.selectedCharacter = new character();        // Wenn die Kampagne geändert wurde muss der zu aktualisierende Charakter neu ausgewählt werden.
                context.state = new ObsidianStateLoggedIn();        // Status nach Kampagnenauswahl immer LoggedIn
            }
        }

        public override void SetCharacter(ContextObsidian context, string id)
        {
            List<character> chrlist = context.datahandler.getCharactersForCampaign(context.selectedCampaign);
            IEnumerable<character> selChr = from c in chrlist where c.id == id select c;
            if (selChr.Count() > 0)
            {
                context.selectedCharacter = selChr.First();

                // Status prüfen: Wenn alle Daten vorhanden sind, bereit für Update. Kampagne wird nicht abgeprüft.
                if ((!String.IsNullOrEmpty(context.uploadedCharacterData.id)))
                    context.state = new ObsidianStateUpdateDataComplete();
                else
                    context.state = new ObsidianStateLoggedIn();
            }
            else
            {
                // Charakter wurde in der Kampagnenliste nicht gefunden: Zurücksetzen
                context.selectedCharacter = new character();
                context.state = new ObsidianStateLoggedIn();
            }
        }

        public override void SetUpdateData(ContextObsidian context, character chr)
        {
            context.uploadedCharacterData = chr;

            // Status prüfen: Nicht erforderlich, da bereits im Ready-Zustand
        }

        public override void DoUpdate(ContextObsidian context)
        {
            character updateData = context.uploadedCharacterData;
            updateData.campaign_id = context.selectedCampaign.id;

            context.datahandler.updateCharakter(updateData);
            context.state = new ObsidianStateLoggedIn();
        }
    }
     
}