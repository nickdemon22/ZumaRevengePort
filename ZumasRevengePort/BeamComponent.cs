using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200009C RID: 156
	public class BeamComponent
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0008B83C File Offset: 0x00089A3C
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mV0);
			sync.SyncFloat(ref this.mDistTraveled);
			sync.SyncBoolean(ref this.mAdditive);
			sync.SyncLong(ref this.mAlphaDelta);
			sync.SyncLong(ref this.mMinAlpha);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mColor.mAlpha);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
		}

		// Token: 0x040015E0 RID: 5600
		public MemoryImage mImage;

		// Token: 0x040015E1 RID: 5601
		public float mX;

		// Token: 0x040015E2 RID: 5602
		public float mY;

		// Token: 0x040015E3 RID: 5603
		public float mVX;

		// Token: 0x040015E4 RID: 5604
		public float mVY;

		// Token: 0x040015E5 RID: 5605
		public float mV0;

		// Token: 0x040015E6 RID: 5606
		public float mDistTraveled;

		// Token: 0x040015E7 RID: 5607
		public bool mAdditive;

		// Token: 0x040015E8 RID: 5608
		public int mAlphaDelta;

		// Token: 0x040015E9 RID: 5609
		public int mMinAlpha;

		// Token: 0x040015EA RID: 5610
		public int mCel;

		// Token: 0x040015EB RID: 5611
		public Color mColor = default(Color);
	}
}
