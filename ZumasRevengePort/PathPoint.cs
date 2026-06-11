using System;

namespace ZumasRevenge
{
	// Token: 0x020000BF RID: 191
	public class PathPoint
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0008ED64 File Offset: 0x0008CF64
		public PathPoint(float tx, float ty, float dist)
		{
			this.x = tx;
			this.y = ty;
			this.mDist = dist;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0008EDBC File Offset: 0x0008CFBC
		public PathPoint(float tx, float ty)
		{
			this.x = tx;
			this.y = ty;
			this.mDist = 0f;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0008EE18 File Offset: 0x0008D018
		public PathPoint()
		{
			this.x = 0f;
			this.y = 0f;
			this.mDist = 0f;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		// Token: 0x040016D0 RID: 5840
		public float x;

		// Token: 0x040016D1 RID: 5841
		public float y;

		// Token: 0x040016D2 RID: 5842
		public float mDist;

		// Token: 0x040016D3 RID: 5843
		public float t;

		// Token: 0x040016D4 RID: 5844
		public byte mPriority;

		// Token: 0x040016D5 RID: 5845
		public bool mInTunnel;

		// Token: 0x040016D6 RID: 5846
		public bool mEndPoint;

		// Token: 0x040016D7 RID: 5847
		public bool mSplinePoint;

		// Token: 0x040016D8 RID: 5848
		public bool mSelected;
	}
}
