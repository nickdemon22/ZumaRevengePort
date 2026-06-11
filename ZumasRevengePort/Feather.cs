using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000E0 RID: 224
	public class Feather
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x0009A38C File Offset: 0x0009858C
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mDecVX);
			sync.SyncFloat(ref this.mDecVY);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mImgNum);
			if (sync.isRead())
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_FEATHER1 + (this.mImgNum - 1));
			}
			sync.SyncFloat(ref this.mAngleOsc.mVal);
			sync.SyncFloat(ref this.mAngleOsc.mMinVal);
			sync.SyncFloat(ref this.mAngleOsc.mMaxVal);
			sync.SyncFloat(ref this.mAngleOsc.mInc);
			sync.SyncFloat(ref this.mAngleOsc.mAccel);
			sync.SyncBoolean(ref this.mAngleOsc.mForward);
		}

		// Token: 0x040017EC RID: 6124
		public Image mImage;

		// Token: 0x040017ED RID: 6125
		public float mX;

		// Token: 0x040017EE RID: 6126
		public float mY;

		// Token: 0x040017EF RID: 6127
		public float mVX;

		// Token: 0x040017F0 RID: 6128
		public float mVY;

		// Token: 0x040017F1 RID: 6129
		public float mDecVX;

		// Token: 0x040017F2 RID: 6130
		public float mDecVY;

		// Token: 0x040017F3 RID: 6131
		public float mAlpha;

		// Token: 0x040017F4 RID: 6132
		public int mImgNum;

		// Token: 0x040017F5 RID: 6133
		public Oscillator mAngleOsc = new Oscillator();
	}
}
