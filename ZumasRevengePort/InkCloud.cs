using System;

namespace ZumasRevenge
{
	// Token: 0x020000EB RID: 235
	public class InkCloud
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x0009A8AC File Offset: 0x00098AAC
		public void SyncState(DataSync s)
		{
			s.SyncBoolean(ref this.mFadeIn);
			s.SyncFloat(ref this.mAlpha);
			s.SyncFloat(ref this.mSize);
			s.SyncFloat(ref this.mX);
			s.SyncFloat(ref this.mY);
		}

		// Token: 0x0400182C RID: 6188
		public bool mFadeIn;

		// Token: 0x0400182D RID: 6189
		public float mAlpha;

		// Token: 0x0400182E RID: 6190
		public float mSize;

		// Token: 0x0400182F RID: 6191
		public float mX;

		// Token: 0x04001830 RID: 6192
		public float mY;
	}
}
