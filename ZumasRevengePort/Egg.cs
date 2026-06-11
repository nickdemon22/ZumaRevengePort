using System;

namespace ZumasRevenge
{
	// Token: 0x020000DF RID: 223
	public class Egg
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0009A36F File Offset: 0x0009856F
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mSize);
		}

		// Token: 0x040017EA RID: 6122
		public float mAngle = 1.570795f;

		// Token: 0x040017EB RID: 6123
		public float mSize;
	}
}
