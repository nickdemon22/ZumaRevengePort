using System;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	// Token: 0x02000113 RID: 275
	public class TriggeredEffect
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x000A0738 File Offset: 0x0009E938
		public TriggeredEffect(bool create)
		{
			if (create)
			{
				this.mRings = new PILSystem(50, 50);
				this.mRings.mHighWatermark = Common._M(80);
				this.mRings.mLowWatermark = Common._M(60);
				this.mRings.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mRings.mScale = Common._S(1f);
				this.mRings.WaitForEmitters(true);
				this.mRainbow = new PILSystem(50, 50);
				this.mRainbow.mHighWatermark = Common._M(80);
				this.mRainbow.mLowWatermark = Common._M(60);
				this.mRainbow.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mRainbow.mScale = Common._S(1f);
				this.mRainbow.WaitForEmitters(true);
				this.mGas = new PILSystem(50, 50);
				this.mGas.mHighWatermark = Common._M(80);
				this.mGas.mLowWatermark = Common._M(60);
				this.mGas.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mGas.mScale = Common._S(1f);
				this.mGas.WaitForEmitters(true);
				this.mFlare = new PILSystem(50, 50);
				this.mFlare.mHighWatermark = Common._M(80);
				this.mFlare.mLowWatermark = Common._M(60);
				this.mFlare.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mFlare.mScale = Common._S(1f);
				this.mFlare.WaitForEmitters(true);
				this.mTrail = new PILSystem(150, 50);
				this.mTrail.mHighWatermark = Common._M(80);
				this.mTrail.mLowWatermark = Common._M(60);
				this.mTrail.mFPSCallback = new PILSystem.FPSCallback(PILSystem.FadeParticlesFPSCallback);
				this.mTrail.mScale = Common._S(1f);
				this.mTrail.WaitForEmitters(true);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000A096B File Offset: 0x0009EB6B
		public TriggeredEffect() : this(true)
		{
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000A0974 File Offset: 0x0009EB74
		public virtual void Dispose()
		{
			if (this.mRings != null)
			{
				this.mRings.Dispose();
				this.mRings = null;
			}
			if (this.mRainbow != null)
			{
				this.mRainbow.Dispose();
				this.mRainbow = null;
			}
			if (this.mGas != null)
			{
				this.mGas.Dispose();
				this.mGas = null;
			}
			if (this.mFlare != null)
			{
				this.mFlare.Dispose();
				this.mFlare = null;
			}
			if (this.mTrail != null)
			{
				this.mTrail.Dispose();
				this.mTrail = null;
			}
		}

		// Token: 0x0400193E RID: 6462
		public PILSystem mRings;

		// Token: 0x0400193F RID: 6463
		public PILSystem mRainbow;

		// Token: 0x04001940 RID: 6464
		public PILSystem mGas;

		// Token: 0x04001941 RID: 6465
		public PILSystem mFlare;

		// Token: 0x04001942 RID: 6466
		public PILSystem mTrail;
	}
}
