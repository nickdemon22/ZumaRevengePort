using System;
using JeffLib;

namespace ZumasRevenge
{
	// Token: 0x02000114 RID: 276
	public class AlphaFadeInfo
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x000A0A03 File Offset: 0x0009EC03
		public AlphaFadeInfo()
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000A0A0B File Offset: 0x0009EC0B
		public AlphaFadeInfo(AlphaFader f, bool s)
		{
			this.first = f;
			this.second = s;
		}

		// Token: 0x04001943 RID: 6467
		public AlphaFader first;

		// Token: 0x04001944 RID: 6468
		public bool second;
	}
}
