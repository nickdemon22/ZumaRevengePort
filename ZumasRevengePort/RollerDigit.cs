using System;

namespace ZumasRevenge
{
	// Token: 0x02000110 RID: 272
	public class RollerDigit
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x000A0558 File Offset: 0x0009E758
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mNum);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mBounceState);
			sync.SyncLong(ref this.mRestingY);
		}

		// Token: 0x04001932 RID: 6450
		public int mNum = -1;

		// Token: 0x04001933 RID: 6451
		public float mX;

		// Token: 0x04001934 RID: 6452
		public float mY;

		// Token: 0x04001935 RID: 6453
		public float mVY;

		// Token: 0x04001936 RID: 6454
		public int mDelay;

		// Token: 0x04001937 RID: 6455
		public int mBounceState;

		// Token: 0x04001938 RID: 6456
		public int mRestingY;
	}
}
