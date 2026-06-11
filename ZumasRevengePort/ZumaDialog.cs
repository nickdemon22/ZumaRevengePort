using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200001C RID: 28
	public class ZumaDialog : DialogEx
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0003A830 File Offset: 0x00038A30
		public ZumaDialog(int id, bool isModal, string header, string lines, string footer, int btn_mode) : base(Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BUTTON), Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BUTTON), id, isModal, header, lines, footer, btn_mode)
		{
			this.IMAGE_GUI_D11 = Res.GetImageByID(ResID.IMAGE_GUI_D11);
			this.IMAGE_GUI_D12 = Res.GetImageByID(ResID.IMAGE_GUI_D12);
			this.IMAGE_GUI_D13 = Res.GetImageByID(ResID.IMAGE_GUI_D13);
			this.IMAGE_GUI_D01 = Res.GetImageByID(ResID.IMAGE_GUI_D01);
			this.IMAGE_GUI_D02 = Res.GetImageByID(ResID.IMAGE_GUI_D02);
			this.IMAGE_GUI_D03 = Res.GetImageByID(ResID.IMAGE_GUI_D03);
			this.IMAGE_GUI_D04 = Res.GetImageByID(ResID.IMAGE_GUI_D04);
			this.IMAGE_GUI_D05 = Res.GetImageByID(ResID.IMAGE_GUI_D05);
			this.IMAGE_GUI_D06 = Res.GetImageByID(ResID.IMAGE_GUI_D06);
			this.IMAGE_GUI_D07 = Res.GetImageByID(ResID.IMAGE_GUI_D07);
			this.IMAGE_GUI_D08 = Res.GetImageByID(ResID.IMAGE_GUI_D08);
			this.IMAGE_GUI_D09 = Res.GetImageByID(ResID.IMAGE_GUI_D09);
			this.IMAGE_GUI_D10 = Res.GetImageByID(ResID.IMAGE_GUI_D10);
			this.mMinWidth = this.IMAGE_GUI_D10.mWidth + this.IMAGE_GUI_D12.mWidth + this.IMAGE_GUI_D02.mWidth;
			this.mMinHeight = this.IMAGE_GUI_D12.mHeight + this.IMAGE_GUI_D13.mHeight;
			this.mTargetWidth = this.mMinWidth;
			this.mTargetHeight = this.mMinHeight;
			this.mCenterInitially = true;
			this.mNumWidthSpacers = 0;
			this.mNumHeightSpacers = 0;
			this.mLastFocusWidget = null;
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.Shutdown();
			}
			this.mAllowDrag = false;
			this.mPriority = 2;
			this.mBackgroundInsets = new Insets(Common._S(Common._M(16)), Common._S(Common._M1(61)), Common._S(Common._M2(18)), Common._S(Common._M3(50)));
			this.mContentInsets = new Insets(Common._S(Common._M(14)), Common._S(Common._M1(50)), Common._S(Common._M2(14)), Common._S(Common._M3(10)));
			this.mHasAlpha = (this.mHasTransparencies = true);
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0003AA94 File Offset: 0x00038C94
		~ZumaDialog()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0003AABC File Offset: 0x00038CBC
		public override void Resize(int x, int y, int w, int h)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_D11);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_D09);
			int num = Math.Max(0, w - this.mMinWidth);
			int num2 = imageByID.mWidth * 2;
			this.mNumWidthSpacers = ((num % num2 == 0) ? (num / num2) : (num / num2 + 1));
			w = this.mMinWidth + this.mNumWidthSpacers * imageByID.mWidth * 2;
			num = Math.Max(0, h - this.mMinHeight);
			num2 = imageByID2.mHeight;
			this.mNumHeightSpacers = ((num % num2 == 0) ? (num / num2) : (num / num2 + 1));
			h = this.mMinHeight + this.mNumHeightSpacers * imageByID2.mHeight;
			if (this.mCenterInitially)
			{
				x = (GlobalMembers.gSexyApp.mWidth - w) / 2;
				y = (GlobalMembers.gSexyApp.mHeight - h) / 2;
				this.mCenterInitially = false;
			}
			this.mTargetWidth = w;
			this.mTargetHeight = h;
			this.mButtonSidePadding = Common._S(Common._M(30));
			this.mButtonHorzSpacing = Common._S(Common._M(100));
			base.Resize(x, y, w, h);
			this.SizeButtons();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0003ABDA File Offset: 0x00038DDA
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		public override void Draw(SexyGraphics g)
		{
			g.ClearClipRect();
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.Draw(g);
			if (Enumerable.Count<ZumaDialogLine>(this.mCustomLines) > 0)
			{
				this.mDialogLines = "";
				this.mDialogHeader = "";
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_FRAME_WOOD);
			g.ClearClipRect();
			g.ClipRect(this.IMAGE_GUI_D11.mWidth, this.IMAGE_GUI_D12.mHeight / 2 + 10, this.mWidth - this.IMAGE_GUI_D11.mWidth * 2, this.mHeight - this.IMAGE_GUI_D12.mHeight);
			int i = 0;
			int j = 0;
			bool flag = false;
			while (j <= this.mHeight)
			{
				while (i < this.mWidth)
				{
					if (flag)
					{
						g.DrawImageMirror(imageByID, i, j);
					}
					else
					{
						g.DrawImage(imageByID, i, j);
					}
					i += imageByID.GetWidth();
					flag = !flag;
				}
				i = 0;
				j += imageByID.GetHeight();
			}
			g.ClearClipRect();
			g.ClipRect(0, 0, this.mWidth, this.mHeight + Common._S(10));
			int num = (this.mWidth - this.IMAGE_GUI_D12.mWidth) / 2;
			g.DrawImage(this.IMAGE_GUI_D12, num, Common._S(Common._M(7)));
			g.DrawImage(this.IMAGE_GUI_D13, (this.mWidth - this.IMAGE_GUI_D13.mWidth) / 2, this.mHeight - this.IMAGE_GUI_D13.mHeight + Common._S(Common._M(8)));
			int num2 = this.mHeight - this.IMAGE_GUI_D13.mHeight - Common._S(Common._M(13));
			g.DrawImage(this.IMAGE_GUI_D06, num, num2);
			int num3 = num;
			for (int k = 0; k < this.mNumWidthSpacers; k++)
			{
				num3 -= this.IMAGE_GUI_D11.mWidth;
				g.DrawImage(this.IMAGE_GUI_D11, num3, Common._S(Common._M(54)));
				g.DrawImage(this.IMAGE_GUI_D07, num3, num2 + this.IMAGE_GUI_D06.mHeight - this.IMAGE_GUI_D07.mHeight);
			}
			g.DrawImage(this.IMAGE_GUI_D10, num3 - this.IMAGE_GUI_D10.mWidth, Common._S(Common._M(54)));
			g.DrawImage(this.IMAGE_GUI_D08, num3 - this.IMAGE_GUI_D08.mWidth, num2 + this.IMAGE_GUI_D06.mHeight - this.IMAGE_GUI_D08.mHeight);
			num3 = num + this.IMAGE_GUI_D12.mWidth;
			for (int l = 0; l < this.mNumWidthSpacers; l++)
			{
				g.DrawImage(this.IMAGE_GUI_D01, num3, Common._S(Common._M(54)));
				g.DrawImage(this.IMAGE_GUI_D05, num3, num2 + this.IMAGE_GUI_D06.mHeight - this.IMAGE_GUI_D05.mHeight);
				num3 += this.IMAGE_GUI_D01.mWidth;
			}
			g.DrawImage(this.IMAGE_GUI_D02, num3, Common._S(Common._M(54)));
			g.DrawImage(this.IMAGE_GUI_D04, num3, num2 + this.IMAGE_GUI_D06.mHeight - this.IMAGE_GUI_D04.mHeight);
			int num4 = Common._S(Common._M(54)) + this.IMAGE_GUI_D10.mHeight;
			for (int m = 0; m < this.mNumHeightSpacers; m++)
			{
				g.DrawImage(this.IMAGE_GUI_D09, 0, num4);
				g.DrawImage(this.IMAGE_GUI_D03, this.mWidth - this.IMAGE_GUI_D03.mWidth, num4);
				num4 += this.IMAGE_GUI_D09.mHeight;
			}
			if (Enumerable.Count<ZumaDialogLine>(this.mCustomLines) > 0)
			{
				int num5 = this.mContentInsets.mTop + this.mBackgroundInsets.mTop + Common._DS(Common._M(0));
				for (int n = 0; n < Enumerable.Count<ZumaDialogLine>(this.mCustomLines); n++)
				{
					ZumaDialogLine zumaDialogLine = this.mCustomLines[n];
					num5 += zumaDialogLine.mYPadding;
					g.SetFont(zumaDialogLine.mFont);
					g.SetColor(zumaDialogLine.mColor);
					g.WriteString(zumaDialogLine.mLine, this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft, num5 + zumaDialogLine.mFont.GetAscent(), this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, 0);
					num5 += zumaDialogLine.mFont.GetHeight();
				}
				return;
			}
			int num6 = this.mContentInsets.mTop + this.mBackgroundInsets.mTop;
			if (this.mDialogHeader.Length > 0)
			{
				num6 += this.mHeaderFont.GetAscent() - this.mHeaderFont.GetAscentPadding();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[0]);
				this.WriteCenteredLine(g, num6, this.mDialogHeader);
				num6 += this.mHeaderFont.GetHeight() - this.mHeaderFont.GetAscent();
				num6 += this.mSpaceAfterHeader;
			}
			g.SetFont(this.mLinesFont);
			g.SetColor(this.mColors[1]);
			Rect rect;
			rect = new Rect(this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + 2, num6, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - 4, 0);
			num6 += this.WriteWordWrapped(g, rect, this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset, this.mTextAlign);
			if (this.mDialogFooter.Length != 0 && this.mButtonMode != 3)
			{
				num6 += 8;
				num6 += this.mHeaderFont.GetLineSpacing();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[2]);
				this.WriteCenteredLine(g, num6, this.mDialogFooter);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0003B26C File Offset: 0x0003946C
		public override bool IsPointVisible(int x, int y)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_D12);
			int num = (this.mWidth - imageByID.mWidth) / 2;
			return (y >= Common._S(Common._M(54)) && y <= this.mHeight - Common._S(Common._M1(30))) || (x >= num && x <= num + imageByID.mWidth);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0003B2CC File Offset: 0x000394CC
		public virtual void GetSize(ref int w, ref int h)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BUTTON);
			int num = this.mContentInsets.mLeft + this.mContentInsets.mRight + this.mBackgroundInsets.mLeft + this.mBackgroundInsets.mRight + 4;
			int num2 = this.mBackgroundInsets.mTop + this.mBackgroundInsets.mBottom + this.mContentInsets.mTop + this.mContentInsets.mBottom + this.mSpaceAfterHeader + imageByID.GetCelHeight() + Common._S(Common._M(40));
			w += num;
			h += num2;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0003B36D File Offset: 0x0003956D
		public override void KeyDown(KeyCode key)
		{
			base.KeyDown(key);
			if (this.mButtonMode == 0)
			{
				return;
			}
			if ((int)key == 27)
			{
				this.ButtonDepress(1001);
				return;
			}
			if ((int)key == 13)
			{
				this.ButtonDepress(1000);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0003B3A0 File Offset: 0x000395A0
		public override void AddedToManager(WidgetManager wm)
		{
			base.AddedToManager(wm);
			this.mLastFocusWidget = wm.mFocusWidget;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0003B3B5 File Offset: 0x000395B5
		public override void RemovedFromManager(WidgetManager wm)
		{
			base.RemovedFromManager(wm);
			if (this.mLastFocusWidget != wm.mFocusWidget && !GlobalMembers.gSexyApp.mShutdown && this.mLastFocusWidget != null)
			{
				wm.SetFocus(this.mLastFocusWidget);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0003B3EC File Offset: 0x000395EC
		public override void MouseDrag(int x, int y)
		{
			if (this.mAllowDrag)
			{
				base.MouseDrag(x, y);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0003B3FE File Offset: 0x000395FE
		public override void ButtonPress(int inButtonID)
		{
			base.ButtonPress(inButtonID);
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0003B41B File Offset: 0x0003961B
		public void SetFocusWidgetToBoard()
		{
			this.mLastFocusWidget = ((GameApp)GlobalMembers.gSexyApp).GetBoard();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0003B434 File Offset: 0x00039634
		public void SizeButtons()
		{
			int inWidth = Common._S(Common._M(120));
			if (this.mYesButton != null)
			{
				this.EnsureButtonMeetsWidth(this.mYesButton, inWidth);
				if (this.mNoButton == null)
				{
					int num = Common._S(Common._M(120));
					this.mYesButton.Resize((this.mWidth - num) / 2, this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - this.mButtonHeight - Common._S(Common._M(7)), num, this.mButtonHeight);
				}
			}
			if (this.mNoButton != null)
			{
				this.EnsureButtonMeetsWidth(this.mNoButton, inWidth);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0003B4DA File Offset: 0x000396DA
		public void EnsureButtonMeetsWidth(DialogButton inButton, int inWidth)
		{
			if (inButton.mWidth < inWidth)
			{
				inButton.Resize((int)((float)inButton.mX - (float)(inWidth - inButton.mWidth) * 0.5f), inButton.mY, inWidth, inButton.mHeight);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0003B510 File Offset: 0x00039710
		public int GetLeft()
		{
			return this.mX + this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0003B530 File Offset: 0x00039730
		public int GetTop()
		{
			return this.mY + this.mContentInsets.mTop + this.mBackgroundInsets.mTop + Common._S(54);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0003B558 File Offset: 0x00039758
		public int GetWidth()
		{
			return this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0003B590 File Offset: 0x00039790
		public void Kill()
		{
			this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0003B5BB File Offset: 0x000397BB
		internal void WaitForResult()
		{
		}

		// Token: 0x04000B68 RID: 2920
		public bool mCenterInitially;

		// Token: 0x04000B69 RID: 2921
		public bool mAllowDrag;

		// Token: 0x04000B6A RID: 2922
		public List<ZumaDialogLine> mCustomLines = new List<ZumaDialogLine>();

		// Token: 0x04000B6B RID: 2923
		protected int mMinWidth;

		// Token: 0x04000B6C RID: 2924
		protected int mMinHeight;

		// Token: 0x04000B6D RID: 2925
		protected int mTargetWidth;

		// Token: 0x04000B6E RID: 2926
		protected int mTargetHeight;

		// Token: 0x04000B6F RID: 2927
		protected int mNumWidthSpacers;

		// Token: 0x04000B70 RID: 2928
		protected int mNumHeightSpacers;

		// Token: 0x04000B71 RID: 2929
		protected Widget mLastFocusWidget;

		// Token: 0x04000B72 RID: 2930
		private Image IMAGE_GUI_D11;

		// Token: 0x04000B73 RID: 2931
		private Image IMAGE_GUI_D12;

		// Token: 0x04000B74 RID: 2932
		private Image IMAGE_GUI_D13;

		// Token: 0x04000B75 RID: 2933
		private Image IMAGE_GUI_D01;

		// Token: 0x04000B76 RID: 2934
		private Image IMAGE_GUI_D02;

		// Token: 0x04000B77 RID: 2935
		private Image IMAGE_GUI_D03;

		// Token: 0x04000B78 RID: 2936
		private Image IMAGE_GUI_D04;

		// Token: 0x04000B79 RID: 2937
		private Image IMAGE_GUI_D05;

		// Token: 0x04000B7A RID: 2938
		private Image IMAGE_GUI_D06;

		// Token: 0x04000B7B RID: 2939
		private Image IMAGE_GUI_D07;

		// Token: 0x04000B7C RID: 2940
		private Image IMAGE_GUI_D08;

		// Token: 0x04000B7D RID: 2941
		private Image IMAGE_GUI_D09;

		// Token: 0x04000B7E RID: 2942
		private Image IMAGE_GUI_D10;
	}
}
