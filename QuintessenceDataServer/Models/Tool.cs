using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuintessenceDataServer.Models {
	public class Tool {
		public int ToolID;
		public int ItemID;
		public int ToolLevel;
		public int ToolType;
		public double Efficiency;
		public bool IsModifier;
		public Tool(int toolID) {

		}
		public Tool(int toolID, int itemID, int toolLevel, int toolType, double efficiency, bool isModifier){
			ToolID = toolID;
			ItemID = itemID;
			ToolLevel = toolLevel;
			ToolType = toolType;
			Efficiency = efficiency;
			IsModifier = isModifier;
		}
    }
}
