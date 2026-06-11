using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000062 RID: 98
	public class BallExplosion : IDisposable
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0005CDAC File Offset: 0x0005AFAC
		public BallExplosion()
		{
			this.mPIEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_BALL_EXPLODE).Duplicate();
			Common.SetFXNumScale(this.mPIEffect, GlobalMembers.gSexyAppBase.Is3DAccelerated() ? 1f : Common._M(0.3f));
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0005CDFC File Offset: 0x0005AFFC
		public virtual void Dispose()
		{
			if (this.mPIEffect != null)
			{
				this.mPIEffect.Dispose();
			}
			this.mPIEffect = null;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0005CE18 File Offset: 0x0005B018
		public void Init()
		{
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0005CE1A File Offset: 0x0005B01A
		public void Release()
		{
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0005CE1C File Offset: 0x0005B01C
		public bool Update()
		{
			if (this.mPIEffect == null)
			{
				return true;
			}
			this.mPIEffect.Update();
			return !this.mPIEffect.IsActive();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0005CE43 File Offset: 0x0005B043
		public void Draw(SexyGraphics g)
		{
			this.mPIEffect.Draw(g);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0005CE54 File Offset: 0x0005B054
		public void SetPos(int x, int y)
		{
			this.mPIEffect.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mPIEffect.mDrawTransform.Scale(num, num);
			this.mPIEffect.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
		}

		// Token: 0x04001278 RID: 4728
		public PIEffect mPIEffect;
	}
}
