using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000086 RID: 134
	public class FrogBody
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x00085202 File Offset: 0x00083402
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mAlpha);
			sync.SyncLong(ref this.mCel);
		}

		// Token: 0x04001513 RID: 5395
		public Image mShadow;

		// Token: 0x04001514 RID: 5396
		public Image mLegs;

		// Token: 0x04001515 RID: 5397
		public Image mMouth;

		// Token: 0x04001516 RID: 5398
		public Image mBody;

		// Token: 0x04001517 RID: 5399
		public Image mEyes;

		// Token: 0x04001518 RID: 5400
		public Image mTongue;

		// Token: 0x04001519 RID: 5401
		public Image mLazerEyeLoop;

		// Token: 0x0400151A RID: 5402
		public Point mLegsOffset = new Point();

		// Token: 0x0400151B RID: 5403
		public Point mMouthOffset = new Point();

		// Token: 0x0400151C RID: 5404
		public Point mBodyOffset = new Point();

		// Token: 0x0400151D RID: 5405
		public Point mEyesOffset = new Point();

		// Token: 0x0400151E RID: 5406
		public FrogType mType;

		// Token: 0x0400151F RID: 5407
		public int mTongueX;

		// Token: 0x04001520 RID: 5408
		public int mCX;

		// Token: 0x04001521 RID: 5409
		public int mCY;

		// Token: 0x04001522 RID: 5410
		public int mNextBallX;

		// Token: 0x04001523 RID: 5411
		public int mNextBallY;

		// Token: 0x04001524 RID: 5412
		public int mAlpha;

		// Token: 0x04001525 RID: 5413
		public int mCel;
	}
}
