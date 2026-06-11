using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000F0 RID: 240
	public class Steam
	{
		// Token: 0x06000F07 RID: 3847 RVA: 0x0009AE0C File Offset: 0x0009900C
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mAlphaDec);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mAngleInc);
			sync.SyncFloat(ref this.mXOff);
			sync.SyncFloat(ref this.mYOff);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mImgNum);
			if (sync.isRead())
			{
				this.mImage = ((this.mImage == null) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
			}
		}

		// Token: 0x04001843 RID: 6211
		public float mAlpha = 255f;

		// Token: 0x04001844 RID: 6212
		public float mAlphaDec;

		// Token: 0x04001845 RID: 6213
		public float mAngle;

		// Token: 0x04001846 RID: 6214
		public float mAngleInc;

		// Token: 0x04001847 RID: 6215
		public float mXOff;

		// Token: 0x04001848 RID: 6216
		public float mYOff;

		// Token: 0x04001849 RID: 6217
		public float mSize = 0.1f;

		// Token: 0x0400184A RID: 6218
		public float mVX;

		// Token: 0x0400184B RID: 6219
		public float mVY;

		// Token: 0x0400184C RID: 6220
		public int mImgNum;

		// Token: 0x0400184D RID: 6221
		public Image mImage;
	}
}
