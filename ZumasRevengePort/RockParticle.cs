using System;

namespace ZumasRevenge
{
	// Token: 0x020000E5 RID: 229
	public class RockParticle
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x0009A531 File Offset: 0x00098731
		public RockParticle()
		{
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0009A53C File Offset: 0x0009873C
		public RockParticle(RockParticle rhs)
		{
			this.mAlpha = rhs.mAlpha;
			this.mCel = rhs.mCel;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mVX = rhs.mVX;
			this.mVY = rhs.mVY;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0009A598 File Offset: 0x00098798
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCel);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
		}

		// Token: 0x04001807 RID: 6151
		public float mAlpha;

		// Token: 0x04001808 RID: 6152
		public int mCel;

		// Token: 0x04001809 RID: 6153
		public float mX;

		// Token: 0x0400180A RID: 6154
		public float mY;

		// Token: 0x0400180B RID: 6155
		public float mVX;

		// Token: 0x0400180C RID: 6156
		public float mVY;
	}
}
