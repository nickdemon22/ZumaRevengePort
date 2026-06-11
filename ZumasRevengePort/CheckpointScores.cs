using System;

namespace ZumasRevenge
{
	// Token: 0x02000050 RID: 80
	public class CheckpointScores
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x0005733A File Offset: 0x0005553A
		public void CopyFrom(CheckpointScores rhs)
		{
			this.mZoneStart = rhs.mZoneStart;
			this.mMidpoint = rhs.mMidpoint;
			this.mBoss = rhs.mBoss;
		}

		// Token: 0x04001192 RID: 4498
		public int mZoneStart;

		// Token: 0x04001193 RID: 4499
		public int mMidpoint;

		// Token: 0x04001194 RID: 4500
		public int mBoss;
	}
}
