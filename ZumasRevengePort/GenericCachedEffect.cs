using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200001A RID: 26
	public class GenericCachedEffect
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0003A530 File Offset: 0x00038730
		public GenericCachedEffect(PIEffect e)
		{
			this.mInUse = false;
			this.mEffect = e;
		}

		// Token: 0x04000B63 RID: 2915
		public bool mInUse;

		// Token: 0x04000B64 RID: 2916
		public PIEffect mEffect;
	}
}
