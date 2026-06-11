using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000129 RID: 297
	public class WillOWisp : FlyingBug
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x000A1E68 File Offset: 0x000A0068
		protected static float GetVel()
		{
			return Common._M(0.05f) + (float)(Common.SafeRand() % Common._M1(10)) / Common._M2(100f);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000A1E90 File Offset: 0x000A0090
		protected static FColor GetTargetColor(FColor curr_color)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < WillOWisp.NUM_WISP_COLORS; i++)
			{
				if (WillOWisp.WISP_COLORS[i] != curr_color)
				{
					list.Add(i);
				}
			}
			return WillOWisp.WISP_COLORS[list[Common.SafeRand() % list.Count]];
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000A1EE4 File Offset: 0x000A00E4
		protected override void SetupBug(Critter d)
		{
			d.mImage = null;
			base.SetupBug(d);
			if (++WillOWisp.mSpawnCounter < Common._M(2) && this.mBugs.Count > 1)
			{
				Critter critter = this.mBugs[this.mBugs.Count - 2];
				d.mX = critter.mX;
				d.mY = critter.mY;
				d.mAngle = critter.mAngle;
				WillOWisp.mSpawnCounter = 0;
			}
			d.mInitVel = WillOWisp.GetVel();
			d.mVX = (float)Math.Cos((double)d.mAngle) * d.mInitVel;
			d.mVY = -(float)Math.Sin((double)d.mAngle) * d.mInitVel;
			d.mFader.mColor = (d.mFader.mMinColor = WillOWisp.GetTargetColor(new FColor(0f, 0f, 0f)));
			d.mFader.mMaxColor = WillOWisp.GetTargetColor(d.mFader.mColor);
			d.mFader.FadeOverTime(Common._M(200) + Common.SafeRand() % Common._M1(300));
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000A2018 File Offset: 0x000A0218
		public WillOWisp()
		{
			this.mMinBugs = Common._M(10);
			this.mMaxBugs = Common._M(15);
			this.mNoEnterRect = new Rect(Common._S(Common._M(100)), Common._S(Common._M1(100)), Common._S(Common._M2(600)), Common._S(Common._M3(400)));
			this.mReverseTimer = Common._M(100);
			this.mReverseRotateDelay = Common._M(100);
			this.mRotateMinTimer = Common._M(300);
			this.mRotateMaxTimer = Common._M(400);
			this.mFlyingMinTimer = Common._M(10);
			this.mFlyingMaxTimer = Common._M(25);
			this.mDefaultAnimRate = Common._M(12);
			this.mRestingMinTimer = Common._M(100);
			this.mRestingMaxTimer = Common._M(500);
			this.mMaxRotateDegrees = Common._M(360);
			this.mAllowRest = false;
			this.mNoRotateUntilOnScreen = true;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000A2124 File Offset: 0x000A0324
		public override void Update()
		{
			base.Update();
			for (int i = 0; i < this.mBugs.Count; i++)
			{
				Critter critter = this.mBugs[i];
				if (critter.mFader.Update())
				{
					if (critter.mFader.mForward)
					{
						critter.mFader.mMaxColor = WillOWisp.GetTargetColor(critter.mFader.mColor);
					}
					else
					{
						critter.mFader.mMinColor = WillOWisp.GetTargetColor(critter.mFader.mColor);
					}
					critter.mFader.FadeOverTime(Common._M(200) + Common.SafeRand() % Common._M1(300));
				}
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000A21D8 File Offset: 0x000A03D8
		public override void DrawBug(SexyGraphics g, Critter d, Transform t)
		{
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000A21DA File Offset: 0x000A03DA
		public override string GetName()
		{
			return "WillOWisp";
		}

		// Token: 0x040019E1 RID: 6625
		protected static int mSpawnCounter = 0;

		// Token: 0x040019E2 RID: 6626
		protected static int NUM_WISP_COLORS = 4;

		// Token: 0x040019E3 RID: 6627
		protected static FColor[] WISP_COLORS = new FColor[]
		{
			new FColor(255f, 255f, 0f),
			new FColor(141f, 141f, 255f),
			new FColor(0f, 0f, 255f),
			new FColor(255f, 179f, 179f)
		};
	}
}
