using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000002 RID: 2
	public class AboutInfo : DialogEx, SliderListener
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000021D0 File Offset: 0x000003D0
		public AboutInfo() : base(null, null, 12, true, "", "", "", 0)
		{
			this.FONT_SHAGLOUNGE28_GREEN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN);
			this.FONT_SHAGEXOTICA68_BASE = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.FONT_SHAGLOUNGE28_BROWN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_BROWN);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			this.mOKBtn = null;
			int num = Common._DS(Common._M(304));
			int num2 = Common._DS(Common._M(162));
			Common._DS(85);
			Common._DS(25);
			Common._DS(75);
			int num3 = 840;
			int num4 = 640;
			this.Resize((GameApp.gApp.mWidth - num3) / 2, (GameApp.gApp.GetScreenRect().mHeight - num4) / 2, num3, num4);
			this.mVersionTextY = 530;
			this.mOKBtn = Common.MakeButton(0, this, TextManager.getInstance().getString(483));
			this.mOKBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			int num5 = 10;
			this.mOKBtn.Resize((this.mWidth - num) / 2, this.mHeight - num2 - num5, num, num2);
			this.AddWidget(this.mOKBtn);
			this.mMetricsSharingText = TextManager.getInstance().getString(861);
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mClip = false;
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
			this.mCurrentLanguage = Localization.GetCurrentLanguage();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002369 File Offset: 0x00000569
		public override void Dispose()
		{
			this.RemoveAllWidgets(false, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002373 File Offset: 0x00000573
		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			this.mOKBtn = null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002384 File Offset: 0x00000584
		public override void Draw(SexyGraphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight);
			g.SetFont(this.FONT_SHAGEXOTICA68_BASE);
			g.SetColor(new Color(205, 151, 57));
			g.WriteString(TextManager.getInstance().getString(862), 0, 60, this.mWidth, 0);
			g.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			string text = "";
			string text2 = TextManager.getInstance().getString(485) + " " + GameApp.gApp.mProductVersion + text;
			g.WriteString(text2, 0, this.mVersionTextY, this.mWidth, 0);
			int num = Common._DS(30);
			int num2 = 0;
			int num3 = 0;
			g.GetWordWrappedHeight(num * 2, this.mMetricsSharingText, -1, ref num2, ref num3);
			Common._DS(50);
			Rect rect;
			rect = new Rect(15, 70, 810, 430);
			g.DrawImageBox(rect, this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			Rect rect2;
			rect2 = new Rect(rect.mX + num, rect.mY + num, rect.mWidth - num * 2, rect.mHeight - num * 2);
			g.SetColor(Color.White);
			g.WriteWordWrapped(rect2, this.mMetricsSharingText);
			g.SetFont(this.FONT_SHAGLOUNGE28_BROWN);
			g.SetColor(Color.White);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024E2 File Offset: 0x000006E2
		public override void ButtonPress(int inButtonID)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			base.ButtonPress(inButtonID);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024FF File Offset: 0x000006FF
		public void ProcessHardwareBackButton()
		{
			this.ButtonDepress(3);
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002512 File Offset: 0x00000712
		public override void ButtonDepress(int inButtonID)
		{
			this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
			GameApp.gApp.HideAbout();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002547 File Offset: 0x00000747
		public override void MouseDrag(int x, int y)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002549 File Offset: 0x00000749
		public void SliderVal(int theId, double theVal)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000254B File Offset: 0x0000074B
		public void SliderReleased(int theId, double theVal)
		{
		}

		// Token: 0x04000001 RID: 1
		private DialogButton mOKBtn;

		// Token: 0x04000002 RID: 2
		private int mVersionTextY;

		// Token: 0x04000003 RID: 3
		private string mMetricsSharingText;

		// Token: 0x04000004 RID: 4
		private Font FONT_SHAGLOUNGE28_GREEN;

		// Token: 0x04000005 RID: 5
		private Font FONT_SHAGEXOTICA68_BASE;

		// Token: 0x04000006 RID: 6
		private Font FONT_SHAGLOUNGE28_BROWN;

		// Token: 0x04000007 RID: 7
		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX;

		// Token: 0x04000008 RID: 8
		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK;

		// Token: 0x04000009 RID: 9
		private Localization.LanguageType mCurrentLanguage;
	}
}
