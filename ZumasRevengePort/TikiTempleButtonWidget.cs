using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000157 RID: 343
	public class TikiTempleButtonWidget : ExtraSexyButton
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x000A85DD File Offset: 0x000A67DD
		public TikiTempleButtonWidget(int theId, TikiTemple theListener) : base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mTikiTemple = theListener;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000A85F5 File Offset: 0x000A67F5
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
		}

		// Token: 0x04001AEC RID: 6892
		public TikiTemple mTikiTemple;
	}
}
