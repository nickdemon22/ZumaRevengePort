using System;
using SexyFramework.AELib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000EC RID: 236
	public class ShieldQuadrantData
	{
		// Token: 0x06000EFA RID: 3834 RVA: 0x0009A8F2 File Offset: 0x00098AF2
		public ShieldQuadrantData()
		{
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0009A8FA File Offset: 0x00098AFA
		public ShieldQuadrantData(CompositionMgr cm, PIEffect s)
		{
			this.mCompMgr = cm;
			this.mSparkles = s;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0009A910 File Offset: 0x00098B10
		public virtual void Dispose()
		{
			if (this.mCompMgr != null)
			{
				this.mCompMgr = null;
			}
			if (this.mSparkles != null)
			{
				this.mSparkles.Dispose();
				this.mSparkles = null;
			}
		}

		// Token: 0x04001831 RID: 6193
		public CompositionMgr mCompMgr;

		// Token: 0x04001832 RID: 6194
		public PIEffect mSparkles;

		// Token: 0x04001833 RID: 6195
		public bool mDoHitAnim;

		// Token: 0x04001834 RID: 6196
		public bool mDoExplodeAnim;
	}
}
