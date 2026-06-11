using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200013F RID: 319
	public class ZumaSlideBoxButton : Widget
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x000A38A2 File Offset: 0x000A1AA2
		public ZumaSlideBoxButton(ZumaSlideBox theSlideBox)
		{
			this.mSlideBox = theSlideBox;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000A38B4 File Offset: 0x000A1AB4
		public override void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_GREEN_LIGHT);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_ON);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_RED_LIGHT);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_OFF);
			if (this.mSlideBox.IsOn())
			{
				g.DrawImage(imageByID, imageByID.GetWidth(), 0);
				g.DrawImage(imageByID2, imageByID.GetWidth() + (imageByID.GetWidth() - imageByID2.GetWidth()) / 2, (imageByID.GetHeight() - imageByID2.GetHeight()) / 2);
				return;
			}
			g.DrawImage(imageByID3, imageByID3.GetWidth(), 0);
			g.DrawImage(imageByID4, imageByID3.GetWidth() + (imageByID3.GetWidth() - imageByID4.GetWidth()) / 2, (imageByID3.GetHeight() - imageByID4.GetHeight()) / 2);
		}

		// Token: 0x04001A64 RID: 6756
		public ZumaSlideBox mSlideBox;
	}
}
