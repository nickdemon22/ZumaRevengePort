using System;

namespace ZumasRevenge
{
	// Token: 0x0200008A RID: 138
	public struct ScoreTip
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x0008551B File Offset: 0x0008371B
		public ScoreTip(string t, int l)
		{
			this.mTip = t;
			this.mMinLevel = l;
			this.mTipId = -1;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00085532 File Offset: 0x00083732
		public ScoreTip(string t)
		{
			this.mTip = t;
			this.mMinLevel = -1;
			this.mTipId = -1;
		}

		// Token: 0x04001552 RID: 5458
		public string mTip;

		// Token: 0x04001553 RID: 5459
		public int mTipId;

		// Token: 0x04001554 RID: 5460
		public int mMinLevel;
	}
}
