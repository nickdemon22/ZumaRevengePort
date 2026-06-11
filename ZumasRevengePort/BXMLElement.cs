using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	// Token: 0x020000CC RID: 204
	public class BXMLElement
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x0008F2A3 File Offset: 0x0008D4A3
		public static bool GetAttribute(BXMLElement theElem, string theName, ref string theValue)
		{
			if (theElem.mAttributes.ContainsKey(theName))
			{
				theValue = theElem.mAttributes[theName];
				return true;
			}
			return false;
		}

		// Token: 0x04001727 RID: 5927
		public int mType;

		// Token: 0x04001728 RID: 5928
		public string mValue;

		// Token: 0x04001729 RID: 5929
		public Dictionary<string, string> mAttributes = new Dictionary<string, string>();
	}
}
