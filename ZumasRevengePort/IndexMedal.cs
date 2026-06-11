using System;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000101 RID: 257
	public class IndexMedal : ButtonWidget
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0009E29C File Offset: 0x0009C49C
		public IndexMedal(bool theIsAced, int theId, ButtonListener theButtonListener) : base(theId, theButtonListener)
		{
			this.mIsAced = theIsAced;
			Common.SRand(Common.SexyTime());
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.mSparkles[i].mEffect = null;
				this.mSparkles[i].mOffsetX = -1f;
				this.mSparkles[i].mOffsetY = -1f;
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0009E320 File Offset: 0x0009C520
		public override void Dispose()
		{
			base.Dispose();
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.Dispose();
				}
				this.mSparkles[i].mEffect = null;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0009E370 File Offset: 0x0009C570
		public void FindRandomOffsetsInRadius(float theRadius, ref float theOffsetX, ref float theOffsetY)
		{
			float num = (float)Common.Rand() / (float)QRand.RAND_MAX * (theRadius * 0.9f);
			int num2 = (Common.Rand() % 2 == 0) ? -1 : 1;
			float num3 = (float)Common.Rand() / (float)QRand.RAND_MAX * 6.2831855f;
			float num4 = num * (float)Math.Cos((double)num3) * (float)num2;
			float num5 = num * (float)Math.Sin((double)num3) * (float)num2;
			theOffsetX = theRadius + num4;
			theOffsetY = theRadius + num5;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0009E3E0 File Offset: 0x0009C5E0
		public override void Update()
		{
			base.Update();
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.mDrawTransform.LoadIdentity();
					mEffect.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
					mEffect.mDrawTransform.Translate(this.mSparkles[i].mOffsetX, this.mSparkles[i].mOffsetY);
					mEffect.Update();
					if (Common.Rand(500) == 0 && mEffect.mCurNumParticles == 0 && MathUtils._geq(mEffect.mFrameNum, (float)mEffect.mLastFrameNum))
					{
						mEffect.ResetAnim();
						mEffect.mRandSeeds.Clear();
						mEffect.mRandSeeds.Add(Common.Rand(1000));
						this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
					}
				}
				else if (Common.Rand(500) == 0)
				{
					this.mSparkles[i].mEffect = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
					this.mSparkles[i].mEffect.mEmitAfterTimeline = false;
					Common.SetFXNumScale(this.mSparkles[i].mEffect, 3f);
					this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
				}
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0009E5C4 File Offset: 0x0009C7C4
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				PIEffect mEffect = this.mSparkles[i].mEffect;
				if (mEffect != null)
				{
					mEffect.Draw(g);
				}
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0009E610 File Offset: 0x0009C810
		public void SetAced()
		{
			this.mIsAced = true;
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.mSparkles[i].mEffect = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
				this.mSparkles[i].mEffect.mEmitAfterTimeline = true;
				Common.SetFXNumScale(this.mSparkles[i].mEffect, 3f);
				this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0009E6D0 File Offset: 0x0009C8D0
		public void Init()
		{
			if (!this.mIsAced)
			{
				return;
			}
			for (int i = 0; i < IndexMedal.MAX_NUM_BUTTON_SPARKLES; i++)
			{
				this.FindRandomOffsetsInRadius((float)this.mButtonImage.mWidth / 2f, ref this.mSparkles[i].mOffsetX, ref this.mSparkles[i].mOffsetY);
			}
		}

		// Token: 0x040018C4 RID: 6340
		private static int MAX_NUM_BUTTON_SPARKLES = 2;

		// Token: 0x040018C5 RID: 6341
		public IndexMedal.AceSparkle[] mSparkles = new IndexMedal.AceSparkle[IndexMedal.MAX_NUM_BUTTON_SPARKLES];

		// Token: 0x040018C6 RID: 6342
		public bool mIsAced;

		// Token: 0x040018C7 RID: 6343
		public float mRadius;

		// Token: 0x02000102 RID: 258
		public struct AceSparkle
		{
			// Token: 0x040018C8 RID: 6344
			public PIEffect mEffect;

			// Token: 0x040018C9 RID: 6345
			public float mOffsetX;

			// Token: 0x040018CA RID: 6346
			public float mOffsetY;
		}
	}
}
