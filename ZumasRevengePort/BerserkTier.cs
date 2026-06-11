using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	// Token: 0x020000B7 RID: 183
	public class BerserkTier
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x0008E627 File Offset: 0x0008C827
		public BerserkTier()
		{
			this.mHealthLimit = 0;
			this.mParams = new List<BerserkModifier>();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0008E641 File Offset: 0x0008C841
		public BerserkTier(int hl)
		{
			this.mHealthLimit = hl;
			this.mParams = new List<BerserkModifier>();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0008E65C File Offset: 0x0008C85C
		public BerserkTier(BerserkTier rhs)
		{
			this.mHealthLimit = rhs.mHealthLimit;
			this.mParams = new List<BerserkModifier>();
			for (int i = 0; i < rhs.mParams.Count; i++)
			{
				this.mParams.Add(new BerserkModifier(rhs.mParams[i]));
			}
		}

		// Token: 0x040016A4 RID: 5796
		public int mHealthLimit;

		// Token: 0x040016A5 RID: 5797
		public List<BerserkModifier> mParams;
	}
}
