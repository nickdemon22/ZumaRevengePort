using System;

namespace ZumasRevenge
{
	// Token: 0x020000A0 RID: 160
	public class SkeletonPowerOrb
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0008BCD4 File Offset: 0x00089ED4
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlpha);
		}

		// Token: 0x04001602 RID: 5634
		public float mSize;

		// Token: 0x04001603 RID: 5635
		public float mAlpha = 255f;
	}
}
