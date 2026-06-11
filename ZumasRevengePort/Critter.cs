using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200012D RID: 301
	public class Critter
	{
		// Token: 0x040019FE RID: 6654
		public float mInitVel;

		// Token: 0x040019FF RID: 6655
		public float mVX;

		// Token: 0x04001A00 RID: 6656
		public float mVY;

		// Token: 0x04001A01 RID: 6657
		public float mAX;

		// Token: 0x04001A02 RID: 6658
		public float mAY;

		// Token: 0x04001A03 RID: 6659
		public int mTimer;

		// Token: 0x04001A04 RID: 6660
		public float mAngle;

		// Token: 0x04001A05 RID: 6661
		public float mTargetAngle;

		// Token: 0x04001A06 RID: 6662
		public float mAngleInc;

		// Token: 0x04001A07 RID: 6663
		public float mX;

		// Token: 0x04001A08 RID: 6664
		public float mY;

		// Token: 0x04001A09 RID: 6665
		public Image mImage;

		// Token: 0x04001A0A RID: 6666
		public int mCel;

		// Token: 0x04001A0B RID: 6667
		public int mState;

		// Token: 0x04001A0C RID: 6668
		public int mAnimRate;

		// Token: 0x04001A0D RID: 6669
		public float mAlpha;

		// Token: 0x04001A0E RID: 6670
		public bool mFadeOut;

		// Token: 0x04001A0F RID: 6671
		public float mSize;

		// Token: 0x04001A10 RID: 6672
		public int mRotateDelay;

		// Token: 0x04001A11 RID: 6673
		public int mUpdateCount;

		// Token: 0x04001A12 RID: 6674
		public CommonColorFader mFader = new CommonColorFader();
	}
}
