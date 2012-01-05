using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e4dnd2obsidian;

namespace e4dnd2obsidian
{
    public abstract class IStateObsidian
    {
        public virtual void SetCampaign(ContextObsidian context, string id){}
        public virtual void SetCharacter(ContextObsidian context, string id) { }
        public virtual void SetUpdateData(ContextObsidian context, character chr) { }
        public virtual void DoUpdate(ContextObsidian context) { }
    }
}