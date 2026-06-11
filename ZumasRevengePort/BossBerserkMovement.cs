using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000D3 RID: 211
	public class BossBerserkMovement
	{
		// Token: 0x06000EC3 RID: 3779 RVA: 0x00099DF8 File Offset: 0x00097FF8
		public BossBerserkMovement()
		{
			this.mStartX = 0;
			this.mStartY = 0;
			this.mEndX = 0;
			this.mEndY = 0;
			this.mHealthLimit = -1;
			this.mX = int.MaxValue;
			this.mY = int.MaxValue;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00099E50 File Offset: 0x00098050
		public BossBerserkMovement(BossBerserkMovement rhs)
		{
			this.mStartX = rhs.mStartX;
			this.mStartY = rhs.mStartY;
			this.mEndX = rhs.mEndX;
			this.mEndY = rhs.mEndY;
			this.mHealthLimit = rhs.mHealthLimit;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mPoints.Clear();
			for (int i = 0; i < rhs.mPoints.Count; i++)
			{
				this.mPoints.Add(new Point(rhs.mPoints[i]));
			}
		}

		// Token: 0x0400179E RID: 6046
		public int mStartX;

		// Token: 0x0400179F RID: 6047
		public int mEndX;

		// Token: 0x040017A0 RID: 6048
		public int mStartY;

		// Token: 0x040017A1 RID: 6049
		public int mEndY;

		// Token: 0x040017A2 RID: 6050
		public int mX;

		// Token: 0x040017A3 RID: 6051
		public int mY;

		// Token: 0x040017A4 RID: 6052
		public int mHealthLimit;

		// Token: 0x040017A5 RID: 6053
		public List<Point> mPoints = new List<Point>();
	}
}
