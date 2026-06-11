using System;

namespace ZumasRevenge
{
	// Token: 0x020000B8 RID: 184
	public class BossWall
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0008E6B8 File Offset: 0x0008C8B8
		public BossWall()
		{
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0008E6C0 File Offset: 0x0008C8C0
		public BossWall(BossWall rhs)
		{
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mId = rhs.mId;
			this.mAlphaFadeDir = rhs.mAlphaFadeDir;
			this.mAlpha = rhs.mAlpha;
		}

		// Token: 0x040016A6 RID: 5798
		public int mX;

		// Token: 0x040016A7 RID: 5799
		public int mY;

		// Token: 0x040016A8 RID: 5800
		public int mWidth;

		// Token: 0x040016A9 RID: 5801
		public int mHeight;

		// Token: 0x040016AA RID: 5802
		public int mId;

		// Token: 0x040016AB RID: 5803
		public int mAlphaFadeDir;

		// Token: 0x040016AC RID: 5804
		public int mAlpha;
	}
}
