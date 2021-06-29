using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuintessenceDataServer.Models {
    public class SkillRequirement {
        public int SkillID;
        public int ItemID;
        public int Skill;
        public int Level;
        public bool IsModifier;
        public SkillRequirement(int skillID) {

        }
        public SkillRequirement(int skillID, int itemID, int skill, int level, bool isModifier) {
            SkillID = skillID;
            ItemID = itemID;
            Skill = skill;
            Level = level;
            IsModifier = isModifier;
        }
    }
}