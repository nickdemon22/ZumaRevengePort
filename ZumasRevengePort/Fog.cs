using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200012A RID: 298
	public class Fog : Effect
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x000A2266 File Offset: 0x000A0466
		private static float GetAlphaTimeRange()
		{
			return (float)Common.IntRange(Common._M(150), Common._M1(500));
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000A2282 File Offset: 0x000A0482
		private static float GetSizeTimeRange()
		{
			return (float)Common.IntRange(Common._M(200), Common._M1(500));
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000A229E File Offset: 0x000A049E
		private static float GetSizeRange()
		{
			return Common.FloatRange(Common._M(0.75f), Common._M1(1.25f));
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000A22BC File Offset: 0x000A04BC
		protected override void Init()
		{
			this.mFogElements.Clear();
			Rect[] array = new Rect[]
			{
				new Rect(0, 0, Common._S(Common._M(50)), GameApp.gApp.mHeight),
				new Rect(0, 0, GameApp.gApp.mWidth, Common._S(Common._M1(50))),
				new Rect(GameApp.gApp.mWidth - Common._S(Common._M(50)), 0, Common._S(Common._M1(50)), GameApp.gApp.mHeight),
				new Rect(0, GameApp.gApp.mHeight - Common._S(Common._M2(50)), GameApp.gApp.mWidth, Common._S(Common._M3(50)))
			};
			for (int i = 0; i < 4; i++)
			{
				this.SetupSide(array[i]);
			}
			this.mFogElements.Sort(new ImageSort());
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000A23DB File Offset: 0x000A05DB
		protected void SetupSide(Rect r)
		{
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000A23DD File Offset: 0x000A05DD
		protected void DoDraw(SexyGraphics g, bool under)
		{
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000A23DF File Offset: 0x000A05DF
		public Fog()
		{
			this.mResGroup = "Boss6_StoneHead";
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000A23FD File Offset: 0x000A05FD
		public override string GetName()
		{
			return "Fog";
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000A2404 File Offset: 0x000A0604
		public override void DrawUnderBackground(SexyGraphics g)
		{
			if (!g.Is3D() || this.mForceAllDrawOverBalls)
			{
				return;
			}
			this.DoDraw(g, true);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000A241F File Offset: 0x000A061F
		public override void DrawAboveBalls(SexyGraphics g)
		{
			if (!g.Is3D() || this.mForceAllDrawOverBalls)
			{
				return;
			}
			this.DoDraw(g, true);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000A243A File Offset: 0x000A063A
		public override void Update()
		{
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000A243C File Offset: 0x000A063C
		public override void SetParams(string key, string value)
		{
		}

		// Token: 0x040019E4 RID: 6628
		private static int MAX_ALPHA = 220;

		// Token: 0x040019E5 RID: 6629
		protected List<FogElement> mFogElements = new List<FogElement>();

		// Token: 0x040019E6 RID: 6630
		protected bool mForceAllDrawOverBalls;
	}
}
