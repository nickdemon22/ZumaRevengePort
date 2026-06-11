using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000024 RID: 36
	public class GenericHelp : DialogEx, IDisposable
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public GenericHelp() : base(null, null, 9, true, "", "", "", 0)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_POWERUPS_RED);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUPS_YELLOW);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_POWERUPS_GREEN_CBM);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_POWERUPS_GREEN);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_POWERUPS_PURPLE_CBM);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_POWERUPS_PURPLE);
			Image imageByID7 = Res.GetImageByID(ResID.IMAGE_POWERUPS_BLUE);
			Image imageByID8 = Res.GetImageByID(ResID.IMAGE_POWERUPS_WHITE);
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN);
			int num = Common._DS(Common._M(1500));
			int num2 = Common._DS(Common._M(1100));
			this.Resize((GameApp.gApp.mWidth - num) / 2, (GameApp.gApp.mHeight - num2) / 2, num, num2);
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mClip = false;
			this.mOKBtn = Common.MakeButton(0, this, TextManager.getInstance().getString(483));
			int num3 = Common._DS(Common._M(304));
			int num4 = Common._DS(Common._M(162));
			this.mOKBtn.SetFont(fontByID);
			this.mOKBtn.Resize((this.mWidth - num3) / 2, Common._DS(Common._M1(900)), num3, num4);
			this.AddWidget(this.mOKBtn);
			string[] array = new string[]
			{
				TextManager.getInstance().getString(465),
				TextManager.getInstance().getString(466),
				TextManager.getInstance().getString(467),
				TextManager.getInstance().getString(468),
				TextManager.getInstance().getString(469),
				TextManager.getInstance().getString(470)
			};
			Image[] array2 = new Image[]
			{
				imageByID,
				imageByID2,
				GameApp.gApp.mColorblind ? imageByID3 : imageByID4,
				GameApp.gApp.mColorblind ? imageByID5 : imageByID6,
				imageByID7,
				imageByID8,
				imageByID2
			};
			int[] array3 = new int[]
			{
				3,
				4,
				2,
				1,
				5,
				6
			};
			string[] array4 = new string[]
			{
				TextManager.getInstance().getString(474),
				TextManager.getInstance().getString(475),
				TextManager.getInstance().getString(476),
				TextManager.getInstance().getString(477),
				TextManager.getInstance().getString(478),
				TextManager.getInstance().getString(479)
			};
			for (int i = 0; i < array3.Length; i++)
			{
				this.mHelpItems.Add(new GenericHelpItem());
				GenericHelpItem genericHelpItem = Enumerable.Last<GenericHelpItem>(this.mHelpItems);
				genericHelpItem.mCel = array3[i];
				genericHelpItem.mHeader = array[i];
				genericHelpItem.mImage = array2[i];
				genericHelpItem.mText = array4[i];
			}
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0003D00B File Offset: 0x0003B20B
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0003D015 File Offset: 0x0003B215
		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			this.mOKBtn = null;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0003D028 File Offset: 0x0003B228
		public override void Draw(SexyGraphics g)
		{
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
			Font fontByID3 = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BASE);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_MARQUE_BOX);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BALL_SHADOW);
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight);
			g.SetFont(fontByID);
			g.SetColor(205, 151, 57);
			g.WriteString(TextManager.getInstance().getString(473), 0, Common._DS(Common._M(130)), this.mWidth, 0);
			int num = Common._DS(Common._M(184));
			int num2 = Common._DS(Common._M(28));
			int num3 = Common._DS(Common._M(74));
			int num4 = Common._DS(Common._M(50));
			int num5 = Common._DS(Common._M(650));
			int num6 = Common._DS(Common._M(212));
			Rect rect;
			rect = new Rect(0, 0, Common._DS(Common._M(500)), 1000);
			for (int i = 0; i < Enumerable.Count<GenericHelpItem>(this.mHelpItems); i++)
			{
				int num7 = (i < 3) ? (num + i * (num6 + num2)) : (num + (i - 3) * (num2 + num6));
				int num8 = (i < 3) ? num3 : (num3 + num5 + num4);
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, Common._M(160));
				g.DrawImageBox(new Rect(num8, num7, num5, num6), imageByID);
				g.PopState();
				rect.mX = num8 + Common._DS(Common._M(136));
				rect.mY = num7 + Common._DS(Common._M(64));
				g.SetFont(fontByID2);
				g.SetColor(213, 213, 213);
				g.WriteWordWrapped(rect, this.mHelpItems[i].mText, -1, -1);
				g.SetFont(fontByID3);
				g.SetColor(205, 151, 57);
				g.DrawString(this.mHelpItems[i].mHeader, rect.mX + Common._DS(Common._M(0)), num7 + Common._DS(Common._M1(54)));
				int num9 = num8 + Common._DS(Common._M(20));
				int num10 = num7 + (num6 - this.mHelpItems[i].mImage.GetCelHeight()) / 2 + Common._DS(Common._M(0));
				g.DrawImage(imageByID2, num9 + Common._DS(Common._M(0)), num10 + Common._DS(Common._M1(0)), Common._DS(Common._M2(78)), Common._DS(Common._M3(78)));
				g.DrawImageCel(this.mHelpItems[i].mImage, num9, num10, this.mHelpItems[i].mCel);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0003D334 File Offset: 0x0003B534
		public override void DrawAll(ModalFlags theFlags, SexyGraphics g)
		{
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.DrawAll(theFlags, g);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
		public override void ButtonPress(int inButtonID)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			base.ButtonPress(inButtonID);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0003D3C1 File Offset: 0x0003B5C1
		public override void ButtonDepress(int inButtonID)
		{
			this.ForceCloseDialog();
			GameApp.gApp.HideHelp();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0003D3D3 File Offset: 0x0003B5D3
		public override void MouseDrag(int x, int y)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0003D3D5 File Offset: 0x0003B5D5
		public override void CloseDialog()
		{
			GameApp.gApp.GenericHelpClosed();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0003D3E1 File Offset: 0x0003B5E1
		public void ForceCloseDialog()
		{
			this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
		}

		// Token: 0x04000BAC RID: 2988
		protected DialogButton mOKBtn;

		// Token: 0x04000BAD RID: 2989
		protected List<GenericHelpItem> mHelpItems = new List<GenericHelpItem>();

		// Token: 0x04000BAE RID: 2990
		protected bool mAllowDrag;
	}
}
