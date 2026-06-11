using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	// Token: 0x0200012F RID: 303
	public class ImageSort : Comparer<FogElement>
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x000A365D File Offset: 0x000A185D
		public override int Compare(FogElement x, FogElement y)
		{
			if (x.mImage == y.mImage)
			{
				return 0;
			}
			return -1;
		}
	}
}
