using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200003C RID: 60
	public class ZumaSlideBox : Widget, ScrollWidgetListener
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0004D594 File Offset: 0x0004B794
		public ZumaSlideBox(DialogEx theDialog, int id, string label)
		{
			this.mLabel = label;
			this.mDialog = theDialog;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_RED_LIGHT);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			Rect rect = default(Rect);
			rect.mX = 0;
			rect.mY = 0;
			rect.mWidth = imageByID.GetWidth() * 2;
			rect.mHeight = imageByID.GetHeight();
			this.mLabelFrame = default(Rect);
			this.mLabelFrame.mWidth = imageByID2.GetWidth() - rect.mWidth - Common._S(9);
			this.mLabelFrame.mHeight = imageByID2.GetHeight();
			this.mLabelFrame.mX = 0;
			this.mLabelFrame.mY = (int)((float)(this.mLabelFrame.mHeight - fontByID.GetHeight()) * 0.5f);
			this.mSlideBoxButton = new ZumaSlideBoxButton(this);
			this.mSlideBoxButton.Resize(rect);
			this.mScrollBox = new ScrollWidget(this);
			this.mScrollBox.Resize(this.mLabelFrame.mWidth, (this.mLabelFrame.mHeight - rect.mHeight) / 2, rect.mWidth, rect.mHeight);
			this.mScrollBox.AddWidget(this.mSlideBoxButton);
			this.mScrollBox.SetScrollMode((ScrollWidget.ScrollMode)1);
			this.mScrollBox.EnablePaging(true);
			this.AddWidget(this.mScrollBox);
			Insets insets = new Insets();
			insets.mLeft = 0;
			insets.mRight = this.mSlideBoxButton.mWidth / 2;
			insets.mTop = 0;
			insets.mBottom = 0;
			this.mScrollBox.SetScrollInsets(insets);
			this.mScrollBox.SetPageHorizontal(0, false);
			this.mScrollBox.EnableBounce(false);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0004D764 File Offset: 0x0004B964
		~ZumaSlideBox()
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0004D78C File Offset: 0x0004B98C
		public override void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			g.DrawImage(imageByID, 0, 0);
			g.SetFont(fontByID);
			g.SetColor(255, 255, 45, 255);
			g.WriteWordWrapped(this.mLabelFrame, this.mLabel, -1, 0);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		public override void DrawOverlay(SexyGraphics g)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0004D7EA File Offset: 0x0004B9EA
		public void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			this.mIsOff = (scrollWidget.GetPageHorizontal() == 1);
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0004D813 File Offset: 0x0004BA13
		public void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0004D815 File Offset: 0x0004BA15
		public void SetOnOff(bool isOn)
		{
			this.mIsOff = !isOn;
			this.mScrollBox.SetPageHorizontal(this.mIsOff ? 1 : 0, false);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0004D839 File Offset: 0x0004BA39
		public bool IsOn()
		{
			return !this.mIsOff;
		}

		// Token: 0x04000D6A RID: 3434
		public Rect mLabelFrame;

		// Token: 0x04000D6B RID: 3435
		public string mLabel;

		// Token: 0x04000D6C RID: 3436
		public bool mIsOff;

		// Token: 0x04000D6D RID: 3437
		public ScrollWidget mScrollBox;

		// Token: 0x04000D6E RID: 3438
		public ZumaSlideBoxButton mSlideBoxButton;

		// Token: 0x04000D6F RID: 3439
		public DialogEx mDialog;
	}
}
