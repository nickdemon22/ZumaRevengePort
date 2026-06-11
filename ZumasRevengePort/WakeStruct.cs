using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000107 RID: 263
	public class WakeStruct
	{
		// Token: 0x04001902 RID: 6402
		public uint mBallId;

		// Token: 0x04001903 RID: 6403
		public SexyVector2 mVel = default(SexyVector2);

		// Token: 0x04001904 RID: 6404
		public float mX;

		// Token: 0x04001905 RID: 6405
		public float mY;

		// Token: 0x04001906 RID: 6406
		public float mAngle;

		// Token: 0x04001907 RID: 6407
		public float mSize = 1f;

		// Token: 0x04001908 RID: 6408
		public float mAlpha = 255f;

		// Token: 0x04001909 RID: 6409
		public float mAlphaInc;

		// Token: 0x0400190A RID: 6410
		public bool mAdditive;

		// Token: 0x0400190B RID: 6411
		public bool mExpanding;

		// Token: 0x0400190C RID: 6412
		public bool mIsHead;

		// Token: 0x0400190D RID: 6413
		public int mUpdateCount;

		// Token: 0x0400190E RID: 6414
		public Image mImage;
	}
}
