using System;

namespace ZumasRevenge
{
	// Token: 0x020000F1 RID: 241
	public class RockChunk
	{
		// Token: 0x06000F08 RID: 3848 RVA: 0x0009AEC0 File Offset: 0x000990C0
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCol);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mAlpha);
		}

		// Token: 0x0400184E RID: 6222
		public int mCol;

		// Token: 0x0400184F RID: 6223
		public float mX;

		// Token: 0x04001850 RID: 6224
		public float mY;

		// Token: 0x04001851 RID: 6225
		public float mVX;

		// Token: 0x04001852 RID: 6226
		public float mVY;

		// Token: 0x04001853 RID: 6227
		public float mAlpha;
	}
}
