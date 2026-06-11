using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200003F RID: 63
	public class AchievementsButtonWidget : ExtraSexyButton
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x000544BA File Offset: 0x000526BA
		public AchievementsButtonWidget(int theId, Achievements theListener) : base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mAchievements = theListener;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000544D2 File Offset: 0x000526D2
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
		}

		// Token: 0x040010D3 RID: 4307
		public Achievements mAchievements;
	}
}
