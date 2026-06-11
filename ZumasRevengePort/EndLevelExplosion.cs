using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000063 RID: 99
	public class EndLevelExplosion : IDisposable
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0005CEAC File Offset: 0x0005B0AC
		public EndLevelExplosion()
		{
			this.mPIEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_END_LEVEL_EXPLOSION).Duplicate();
			Common.SetFXNumScale(this.mPIEffect, GlobalMembers.gSexyAppBase.Is3DAccelerated() ? 1f : Common._M(0.5f));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		public virtual void Dispose()
		{
			if (this.mPIEffect != null)
			{
				this.mPIEffect.Dispose();
			}
			this.mPIEffect = null;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0005CF18 File Offset: 0x0005B118
		public void SetPos(int x, int y)
		{
			this.mPIEffect.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mPIEffect.mDrawTransform.Scale(num, num);
			this.mPIEffect.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
		}

		// Token: 0x04001279 RID: 4729
		public int mDelay;

		// Token: 0x0400127A RID: 4730
		public int mX;

		// Token: 0x0400127B RID: 4731
		public int mY;

		// Token: 0x0400127C RID: 4732
		public PIEffect mPIEffect;
	}
}
