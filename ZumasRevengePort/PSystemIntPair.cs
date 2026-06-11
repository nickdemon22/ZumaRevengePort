using System;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	// Token: 0x02000123 RID: 291
	public class PSystemIntPair
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x000A107F File Offset: 0x0009F27F
		public PSystemIntPair()
		{
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000A1087 File Offset: 0x0009F287
		public PSystemIntPair(PILSystem f, int s)
		{
			this.first = f;
			this.second = s;
		}

		// Token: 0x040019AD RID: 6573
		public PILSystem first;

		// Token: 0x040019AE RID: 6574
		public int second;
	}
}
