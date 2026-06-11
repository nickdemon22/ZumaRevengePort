using System;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	// Token: 0x02000112 RID: 274
	public class SpawnEffect
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x000A05E8 File Offset: 0x0009E7E8
		public SpawnEffect(bool create)
		{
			if (create)
			{
				this.mRings = new PILSystem(100, 50);
				this.mRings.mParticleScale2D = 0.3f;
				this.mRings.mScale = Common._S(1f);
				this.mRings.mHighWatermark = Common._M(80);
				this.mRings.mLowWatermark = Common._M(60);
				this.mRings.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mRings.WaitForEmitters(true);
				this.mSwirl = new PILSystem(100, 50);
				this.mSwirl.mHighWatermark = Common._M(80);
				this.mSwirl.mLowWatermark = Common._M(60);
				this.mSwirl.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mSwirl.mParticleScale2D = 0.3f;
				this.mSwirl.mScale = Common._S(1f);
				this.mSwirl.WaitForEmitters(true);
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000A06F7 File Offset: 0x0009E8F7
		public SpawnEffect() : this(true)
		{
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x000A0700 File Offset: 0x0009E900
		public virtual void Dispose()
		{
			if (this.mRings != null)
			{
				this.mRings.Dispose();
				this.mRings = null;
			}
			if (this.mSwirl != null)
			{
				this.mSwirl.Dispose();
				this.mSwirl = null;
			}
		}

		// Token: 0x0400193C RID: 6460
		public PILSystem mRings;

		// Token: 0x0400193D RID: 6461
		public PILSystem mSwirl;
	}
}
