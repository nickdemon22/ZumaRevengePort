using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000AF RID: 175
	public class WayPoint
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x0008D6A2 File Offset: 0x0008B8A2
		public WayPoint()
		{
			this.mHavePerpendicular = false;
			this.mHaveAvgRotation = false;
			this.mInTunnel = false;
			this.mHavePerpendicular = false;
			this.mPriority = 0;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0008D6DC File Offset: 0x0008B8DC
		public WayPoint(float theX, float theY)
		{
			this.x = theX;
			this.y = theY;
			this.mHavePerpendicular = false;
			this.mHaveAvgRotation = false;
			this.mInTunnel = false;
			this.mHavePerpendicular = false;
			this.mPriority = 0;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0008D72C File Offset: 0x0008B92C
		public static float GetCanonicalAngle(float r)
		{
			if (r > 0f)
			{
				while (r > 3.1415927f)
				{
					r -= 6.2831855f;
				}
			}
			else if (r < 0f)
			{
				while (r < -3.1415927f)
				{
					r += 6.2831855f;
				}
			}
			return r;
		}

		// Token: 0x04001665 RID: 5733
		public float x;

		// Token: 0x04001666 RID: 5734
		public float y;

		// Token: 0x04001667 RID: 5735
		public bool mHavePerpendicular;

		// Token: 0x04001668 RID: 5736
		public bool mHaveAvgRotation;

		// Token: 0x04001669 RID: 5737
		public SexyVector3 mPerpendicular = default(SexyVector3);

		// Token: 0x0400166A RID: 5738
		public float mRotation;

		// Token: 0x0400166B RID: 5739
		public float mAvgRotation;

		// Token: 0x0400166C RID: 5740
		public bool mInTunnel;

		// Token: 0x0400166D RID: 5741
		public byte mPriority;
	}
}
