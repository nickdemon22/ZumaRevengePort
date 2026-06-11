using System;

namespace ZumasRevenge
{
	// Token: 0x0200004F RID: 79
	public class GauntletHSInfo
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x000572FE File Offset: 0x000554FE
		public GauntletHSInfo()
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00057311 File Offset: 0x00055511
		public GauntletHSInfo(int score, string n)
		{
			this.mScore = score;
			this.mProfileName = n;
		}

		// Token: 0x04001190 RID: 4496
		public int mScore;

		// Token: 0x04001191 RID: 4497
		public string mProfileName = "";
	}
}
