using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000119 RID: 281
	public class HeaderLetter
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x000A0A44 File Offset: 0x0009EC44
		public HeaderLetter(Image img)
		{
			this.mImage = img;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000A0A53 File Offset: 0x0009EC53
		public HeaderLetter()
		{
			this.mImage = null;
		}

		// Token: 0x04001961 RID: 6497
		public Image mImage;

		// Token: 0x04001962 RID: 6498
		public float mAngle;

		// Token: 0x04001963 RID: 6499
		public float mAngleInc;

		// Token: 0x04001964 RID: 6500
		public float mVX;

		// Token: 0x04001965 RID: 6501
		public float mVY;

		// Token: 0x04001966 RID: 6502
		public float mX;

		// Token: 0x04001967 RID: 6503
		public float mY;

		// Token: 0x04001968 RID: 6504
		public float mAngleAccel;

		// Token: 0x04001969 RID: 6505
		public bool mHinge;

		// Token: 0x0400196A RID: 6506
		public int mSwingCount;

		// Token: 0x0400196B RID: 6507
		public int mUpdateCount;
	}
}
