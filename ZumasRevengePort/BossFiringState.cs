using System;

namespace ZumasRevenge
{
	// Token: 0x020000F2 RID: 242
	public class BossFiringState
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x0009AF1D File Offset: 0x0009911D
		public BossFiringState()
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0009AF30 File Offset: 0x00099130
		public BossFiringState(BossFiringState rhs)
		{
			this.mState = rhs.mState;
			this.mPawYOffset = rhs.mPawYOffset;
			this.mSkullXOffset = rhs.mSkullXOffset;
			this.mSkullYOffset = rhs.mSkullYOffset;
			this.mSkullAngle = rhs.mSkullAngle;
			this.mHeadAngle = rhs.mHeadAngle;
			this.mSkullGrowPct = rhs.mSkullGrowPct;
			this.mTargetSkullAngle = rhs.mTargetSkullAngle;
			this.mSkullAngleInc = rhs.mSkullAngleInc;
			this.mSwipeFrame = rhs.mSwipeFrame;
			this.mTimer = rhs.mTimer;
			this.mStreaksAlpha = rhs.mStreaksAlpha;
			this.mBulletId = rhs.mBulletId;
		}

		// Token: 0x04001854 RID: 6228
		public int mState;

		// Token: 0x04001855 RID: 6229
		public float mPawYOffset;

		// Token: 0x04001856 RID: 6230
		public float mSkullXOffset;

		// Token: 0x04001857 RID: 6231
		public float mSkullYOffset;

		// Token: 0x04001858 RID: 6232
		public float mSkullAngle;

		// Token: 0x04001859 RID: 6233
		public float mHeadAngle;

		// Token: 0x0400185A RID: 6234
		public float mSkullGrowPct = 1f;

		// Token: 0x0400185B RID: 6235
		public float mTargetSkullAngle;

		// Token: 0x0400185C RID: 6236
		public float mSkullAngleInc;

		// Token: 0x0400185D RID: 6237
		public int mSwipeFrame;

		// Token: 0x0400185E RID: 6238
		public int mTimer;

		// Token: 0x0400185F RID: 6239
		public float mStreaksAlpha;

		// Token: 0x04001860 RID: 6240
		public int mBulletId;
	}
}
