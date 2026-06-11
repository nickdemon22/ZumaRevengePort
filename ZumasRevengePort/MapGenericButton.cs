using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000096 RID: 150
	public class MapGenericButton : ExtraSexyButton
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0008B783 File Offset: 0x00089983
		public MapGenericButton(int theId, MapScreen theListener) : base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mMapScreen = theListener;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0008B79B File Offset: 0x0008999B
		public override void Draw(SexyGraphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(this.mMapScreen.mAlpha);
			base.Draw(g);
		}

		// Token: 0x040015CD RID: 5581
		public MapScreen mMapScreen;
	}
}
