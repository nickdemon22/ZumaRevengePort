using System;

namespace ZumasRevenge
{
	// Token: 0x020000C7 RID: 199
	public class Boss_Param_Range
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x0008F059 File Offset: 0x0008D259
		public void Init()
		{
			this.mMin = 0f;
			this.mMax = 0f;
			this.mRatingMin = -1f;
			this.mRatingMax = -1f;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0008F087 File Offset: 0x0008D287
		public bool InRange(float amt)
		{
			return this.mRatingMin < 0f || this.mRatingMax < 0f || (amt >= this.mRatingMin && amt < this.mRatingMax);
		}

		// Token: 0x04001711 RID: 5905
		public float mMin;

		// Token: 0x04001712 RID: 5906
		public float mMax;

		// Token: 0x04001713 RID: 5907
		public float mRatingMin;

		// Token: 0x04001714 RID: 5908
		public float mRatingMax;
	}
}
