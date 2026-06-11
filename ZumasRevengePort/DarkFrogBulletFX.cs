using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000C8 RID: 200
	public class DarkFrogBulletFX
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x0008F0C1 File Offset: 0x0008D2C1
		public DarkFrogBulletFX()
		{
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0008F0E6 File Offset: 0x0008D2E6
		public DarkFrogBulletFX(int id)
		{
			this.mBulletId = id;
		}

		// Token: 0x04001715 RID: 5909
		public PIEffect mBallEffect;

		// Token: 0x04001716 RID: 5910
		public PIEffect mBallExplosion;

		// Token: 0x04001717 RID: 5911
		public float mTwirlAngle;

		// Token: 0x04001718 RID: 5912
		public float mX = -1000f;

		// Token: 0x04001719 RID: 5913
		public float mY = -1000f;

		// Token: 0x0400171A RID: 5914
		public bool mExploding;

		// Token: 0x0400171B RID: 5915
		public int mBulletId = -1;
	}
}
