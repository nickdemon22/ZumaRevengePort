using System;

namespace ZumasRevenge
{
	// Token: 0x020000E1 RID: 225
	public class EggFragment
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x0009A494 File Offset: 0x00098694
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mDecVX);
			sync.SyncFloat(ref this.mDecVY);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mCol);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
		}

		// Token: 0x040017F6 RID: 6134
		public float mVX;

		// Token: 0x040017F7 RID: 6135
		public float mVY;

		// Token: 0x040017F8 RID: 6136
		public float mDecVX;

		// Token: 0x040017F9 RID: 6137
		public float mDecVY;

		// Token: 0x040017FA RID: 6138
		public float mAlpha;

		// Token: 0x040017FB RID: 6139
		public int mCol;

		// Token: 0x040017FC RID: 6140
		public float mX;

		// Token: 0x040017FD RID: 6141
		public float mY;
	}
}
