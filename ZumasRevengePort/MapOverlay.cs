using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000097 RID: 151
	public class MapOverlay
	{
		// Token: 0x040015CE RID: 5582
		public float mAlpha;

		// Token: 0x040015CF RID: 5583
		public bool mUnlocked;

		// Token: 0x040015D0 RID: 5584
		public FPoint[] mCloudPoints = new FPoint[]
		{
			new FPoint(),
			new FPoint(),
			new FPoint()
		};

		// Token: 0x040015D1 RID: 5585
		public FPoint[] mCloudSizes = new FPoint[]
		{
			new FPoint(),
			new FPoint(),
			new FPoint()
		};
	}
}
