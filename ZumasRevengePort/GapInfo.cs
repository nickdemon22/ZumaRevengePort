using System;

namespace ZumasRevenge
{
	// Token: 0x020000B0 RID: 176
	public class GapInfo
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x0008D767 File Offset: 0x0008B967
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCurve);
			sync.SyncLong(ref this.mDist);
			sync.SyncLong(ref this.mBallId);
		}

		// Token: 0x0400166E RID: 5742
		public int mCurve;

		// Token: 0x0400166F RID: 5743
		public int mDist;

		// Token: 0x04001670 RID: 5744
		public int mBallId;
	}
}
