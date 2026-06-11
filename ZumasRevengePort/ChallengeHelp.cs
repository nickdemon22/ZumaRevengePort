using System;
using System.Collections.Generic;
using System.Text;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000064 RID: 100
	public class ChallengeHelp : Widget, ButtonListener
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0005CF70 File Offset: 0x0005B170
		public ChallengeHelp(bool from_help)
		{
			this.mBoard = GameApp.gApp.mBoard;
			int num = Common._DS(Common._M(434));
			int num2 = Common._DS(Common._M(80));
			int num3 = Common._DS(Common._M(518)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX;
			int num4 = Common._DS(10);
			this.mFromHelp = from_help;
			this.mClip = false;
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mCutoutImage = new DeviceImage();
			this.mCutoutImage.SetImageMode(true, true);
			this.mCutoutImage.AddImageFlags(16U);
			this.mCutoutImage.Create(num, num2);
			SexyGraphics graphics = new SexyGraphics(this.mCutoutImage);
			graphics.Get3D().ClearColorBuffer(new Color(0, 0));
			float num5 = 128f;
			float num6 = num5 / 10f;
			int num7 = 0;
			while (num5 > 0f)
			{
				graphics.SetColor(new Color(0, 0, 0, (int)num5));
				graphics.FillRect(num7, num7, this.mCutoutImage.mWidth - num7 * 2, 1);
				graphics.FillRect(num7, num7 + 1, 1, this.mCutoutImage.mHeight - 1 - num7 * 2);
				graphics.FillRect(num7 + 1, this.mCutoutImage.mHeight - 1 - num7, this.mCutoutImage.mWidth - 1 - num7 * 2, 1);
				graphics.FillRect(this.mCutoutImage.mWidth - 1 - num7, num7 + 1, 1, this.mCutoutImage.mHeight - 2 - num7 * 2);
				num5 -= num6;
				num7++;
			}
			CommonGraphics.SetNonMaskedArea(num3, num4, num, num2, this.mMaskedRects, 128);
			this.mPriority = 2147483646;
			this.Resize(0, 0, GameApp.gApp.mWidth, GameApp.gApp.mHeight);
			this.mOKBtn = Common.MakeButton(0, this, from_help ? TextManager.getInstance().getString(483) : TextManager.getInstance().getString(455));
			this.mOKBtn.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN));
			this.AddWidget(this.mOKBtn);
			int num8 = Common._DS(Common._M(254));
			int num9 = Common._DS(Common._M(125));
			int num10 = Common._DS(Common._M(1000));
			this.mOKBtn.Resize((GameApp.gApp.mWidth - num8) / 2, num10, num8, num9);
			this.mMultFX = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_RPI").Duplicate();
			this.mMultFX.mEmitAfterTimeline = true;
			this.mDrawScale = new CurvedVal();
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
			this.mClosing = false;
			this.FONT_SHAGLOUNGE28_STROKE = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
			this.IMAGE_GUI_ARROW_RED = Res.GetImageByID(ResID.IMAGE_GUI_ARROW_RED);
			this.IMAGE_GUI_BARIMAGE = Res.GetImageByID(ResID.IMAGE_GUI_BARIMAGE);
			this.IMAGE_GUI_EQUALIMAGE = Res.GetImageByID(ResID.IMAGE_GUI_EQUALIMAGE);
			this.IMAGE_GUI_BALLIMAGE = Res.GetImageByID(ResID.IMAGE_GUI_BALLIMAGE);
			this.IMAGE_GUI_DIALOG_MARQUE_BOX = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_MARQUE_BOX);
			this.IMAGE_UI_CHALLENGE_GAUGE_EMPTY = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_GAUGE_EMPTY);
			this.IMAGE_UI_CHALLENGE_GAUGE_FILL = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_GAUGE_FILL);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0005D2E3 File Offset: 0x0005B4E3
		public override void Dispose()
		{
			this.mMultFX = null;
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0005D2F4 File Offset: 0x0005B4F4
		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			this.mOKBtn = null;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0005D308 File Offset: 0x0005B508
		public override void Update()
		{
			base.Update();
			if (!GameApp.gApp.Is3DAccelerated())
			{
				return;
			}
			if (!this.mDrawScale.HasBeenTriggered())
			{
				this.MarkDirty();
			}
			if (!this.mDrawScale.IncInVal())
			{
				double num = this.mDrawScale;
			}
			this.MarkDirty();
			this.mMultFX.mDrawTransform.LoadIdentity();
			float num2 = GameApp.DownScaleNum(1f);
			this.mMultFX.mDrawTransform.Scale(num2, num2);
			this.mMultFX.mDrawTransform.Translate((float)Common._DS(Common._M(988)), (float)Common._DS(Common._M1(470)));
			this.mMultFX.Update();
			if (this.mClosing && this.mDrawScale == 0.0)
			{
				this.mBoard.ChallengeHelpClosed();
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0005D3F8 File Offset: 0x0005B5F8
		public override void Draw(SexyGraphics g)
		{
			if (g != null)
			{
				g.Get3D();
			}
			int mWidth = GameApp.gApp.mWidth;
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK_GLOW);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(TextManager.getInstance().getString(410));
			stringBuilder.Append("^d8d8d8^ ");
			stringBuilder.Append(TextManager.getInstance().getString(411));
			stringBuilder.Append("^oldclr^ ");
			stringBuilder.Append(TextManager.getInstance().getString(412));
			stringBuilder.Append("^d8d8d8^ ");
			stringBuilder.Append(TextManager.getInstance().getString(413));
			stringBuilder.Append("^oldclr^ ");
			int num = fontByID.StringWidth(TextManager.getInstance().getString(410)) + fontByID.StringWidth(TextManager.getInstance().getString(411)) + fontByID.StringWidth(TextManager.getInstance().getString(412)) + fontByID.StringWidth(TextManager.getInstance().getString(413)) + fontByID.CharWidth(' ') * 3;
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(TextManager.getInstance().getString(414));
			stringBuilder2.Append("^d8d8d8^ ");
			stringBuilder2.Append(TextManager.getInstance().getString(415));
			stringBuilder2.Append("^oldclr^ ");
			stringBuilder2.Append(TextManager.getInstance().getString(416));
			int num2 = fontByID.StringWidth(TextManager.getInstance().getString(414)) + fontByID.StringWidth(TextManager.getInstance().getString(415)) + fontByID.StringWidth(TextManager.getInstance().getString(416)) + fontByID.CharWidth(' ') * 2;
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder3.Append(TextManager.getInstance().getString(417));
			stringBuilder3.Append("^d8d8d8^ ");
			stringBuilder3.Append(TextManager.getInstance().getString(418));
			stringBuilder3.Append("^oldclr^");
			int num3 = fontByID.StringWidth(TextManager.getInstance().getString(417)) + fontByID.StringWidth(TextManager.getInstance().getString(418)) + fontByID.CharWidth(' ');
			int num4 = (num > num2) ? num : num2;
			num4 = ((num4 > num3) ? num4 : num3);
			num4 += 40;
			int num5 = (num4 + 100 < Common._DS(Common._M(1000))) ? Common._DS(Common._M(1000)) : (num4 + 100);
			int height = Common._DS(Common._M(996));
			int x = (mWidth - num5) / 2;
			int num6 = Common._DS(Common._M(170));
			int num7 = (num4 < Common._DS(Common._M(900))) ? Common._DS(Common._M(900)) : num4;
			int num8 = (mWidth - num7) / 2;
			Common.DrawCommonDialogBacking(g, x, num6, num5, height);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, 200));
			g.DrawImageBox(new Rect(num8, num6 + Common._DS(Common._M(168)), num7, Common._DS(Common._M1(200))), this.IMAGE_GUI_DIALOG_MARQUE_BOX);
			g.DrawImageBox(new Rect(num8, num6 + Common._DS(Common._M(390)), num7, Common._DS(Common._M1(290))), this.IMAGE_GUI_DIALOG_MARQUE_BOX);
			g.DrawImageBox(new Rect(num8, num6 + Common._DS(Common._M(704)), num7, Common._DS(Common._M1(108))), this.IMAGE_GUI_DIALOG_MARQUE_BOX);
			g.SetColorizeImages(false);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE));
			g.SetColor(new Color(205, 151, 57));
			g.WriteString(TextManager.getInstance().getString(409), 0, num6 - g.GetFont().mHeight / 2 + Common._DS(Common._M(190)), GameApp.gApp.mWidth, 0);
			float mTransX = g.mTransX;
			g.mTransX = (float)(GameApp.gApp.mBoardOffsetX + 10);
			int num9 = (int)(-(int)g.mTransX) + 10;
			int num10 = 4;
			Common._DS(Common._M(382));
			Common._DS(Common._M1(420));
			g.SetFont(fontByID);
			g.SetColor(new Color(205, 151, 57));
			g.WriteWordWrapped(new Rect(num8 + num9, num6 + Common._DS(Common._M(168)) + num10, num7 - num9 * 2, Common._DS(Common._M1(200)) - num10 * 2), stringBuilder.ToString());
			g.DrawImage(this.IMAGE_GUI_BARIMAGE, Common._DS(Common._M(430)), Common._DS(Common._M1(436)));
			g.DrawImage(this.IMAGE_GUI_EQUALIMAGE, Common._DS(Common._M(810)), Common._DS(Common._M1(456)));
			g.DrawImage(this.IMAGE_GUI_BALLIMAGE, Common._DS(Common._M(950)), Common._DS(Common._M1(434)));
			if (this.mMultFX != null)
			{
				this.mMultFX.Draw(g);
			}
			Common._DS(Common._M(398));
			Common._DS(Common._M1(638));
			g.SetColor(new Color(205, 151, 57));
			g.WriteWordWrapped(new Rect(num8 + num9, num6 + Common._DS(Common._M(390)) + num10, num7 - num9 * 2, Common._DS(Common._M1(290)) - num10 * 2), stringBuilder2.ToString());
			int num11 = (mWidth - this.IMAGE_UI_CHALLENGE_GAUGE_EMPTY.mWidth) / 2;
			g.DrawImage(this.IMAGE_UI_CHALLENGE_GAUGE_EMPTY, num11, Common._DS(Common._M(675)));
			g.DrawImage(this.IMAGE_UI_CHALLENGE_GAUGE_FILL, num11, Common._DS(Common._M(675)));
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_STROKE));
			g.SetColor(Color.White);
			g.DrawString("2x", num11 + Common._DS(Common._M(105)), Common._DS(Common._M1(800)));
			Common._DS(Common._M(442));
			Common._DS(Common._M1(944));
			g.SetColor(new Color(205, 151, 57));
			g.SetFont(fontByID);
			g.WriteWordWrapped(new Rect(num8 + num9, num6 + Common._DS(Common._M(704)) + num10, num7 - num9 * 2, Common._DS(Common._M1(108)) - num10 * 2), stringBuilder3.ToString());
			g.mTransX = mTransX;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0005DB0E File Offset: 0x0005BD0E
		public virtual void ButtonPress(int id)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0005DB24 File Offset: 0x0005BD24
		public virtual void ButtonDepress(int id)
		{
			this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mClosing = true;
			GameApp.gApp.HideHelp();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0005DB60 File Offset: 0x0005BD60
		public void PreDraw(SexyGraphics g)
		{
			g.SetDrawMode(1);
			g.DrawImage(this.mCutoutImage, Common._DS(Common._M(400) - 160), Common._DS(Common._M1(0)));
			g.SetDrawMode(0);
			g.SetColor(new Color(0, 0, 0, 128));
			for (int i = 0; i < this.mMaskedRects.Count; i++)
			{
				g.FillRect(this.mMaskedRects[i].r);
			}
			float num = (float)this.mDrawScale;
			if (num > 1f)
			{
			}
			Graphics3D graphics3D = (g != null) ? g.Get3D() : null;
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				SexyTransform2D sexyTransform2D;
				sexyTransform2D = new SexyTransform2D(false);
				sexyTransform2D.Translate(-g.mTransX - (float)(this.mWidth / 2), -g.mTransY - (float)(this.mHeight / 2));
				sexyTransform2D.Scale((float)this.mDrawScale, (float)this.mDrawScale);
				sexyTransform2D.Translate(g.mTransX + (float)(this.mWidth / 2), g.mTransY + (float)(this.mHeight / 2));
				graphics3D.PushTransform(sexyTransform2D);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
		private void DrawBonusBar(SexyGraphics g)
		{
			float num = (float)this.mDrawScale;
			if (num > 1f)
			{
				num = 1f;
			}
			float num2 = num * 255f;
			g.SetFont(this.FONT_SHAGLOUNGE28_STROKE);
			g.SetColor(new Color(255, 0, 0, (int)num2));
			g.DrawString(TextManager.getInstance().getString(419), Common._DS(Common._M(80)) + GameApp.gApp.mBoardOffsetX, (int)((float)Common._DS(Common._M1(160)) + (float)this.FONT_SHAGLOUNGE28_STROKE.GetHeight() * 0.5f - 10f));
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)num2));
			g.DrawImageRotatedF(this.IMAGE_GUI_ARROW_RED, (float)(Common._DS(Common._M(390) - 160) + GameApp.gApp.mBoardOffsetX), (float)Common._DS(Common._M1(40)), (double)Common.DegreesToRadians((float)Common._M2(30)));
			g.SetColorizeImages(false);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0005DDD0 File Offset: 0x0005BFD0
		public override void DrawAll(ModalFlags theFlags, SexyGraphics g)
		{
			this.PreDraw(g);
			this.Draw(g);
			if (this.mOKBtn != null)
			{
				g.Translate(this.mOKBtn.mX, this.mOKBtn.mY);
				this.mOKBtn.Draw(g);
				g.Translate(-this.mOKBtn.mX, -this.mOKBtn.mY);
			}
			this.PostDraw(g);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0005DE40 File Offset: 0x0005C040
		public virtual void PostDraw(SexyGraphics g)
		{
			Graphics3D graphics3D = (g != null) ? g.Get3D() : null;
			if (this.mDrawScale != 1.0 && graphics3D != null)
			{
				graphics3D.PopTransform();
			}
			this.DrawBonusBar(g);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0005DE81 File Offset: 0x0005C081
		public virtual void ButtonDownTick(int x)
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0005DE83 File Offset: 0x0005C083
		public virtual void ButtonMouseEnter(int x)
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0005DE85 File Offset: 0x0005C085
		public virtual void ButtonMouseLeave(int x)
		{
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0005DE87 File Offset: 0x0005C087
		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0005DE89 File Offset: 0x0005C089
		public virtual void ButtonPress(int z, int y)
		{
		}

		// Token: 0x0400127D RID: 4733
		public Board mBoard;

		// Token: 0x0400127E RID: 4734
		public MemoryImage mCutoutImage;

		// Token: 0x0400127F RID: 4735
		public DialogButton mOKBtn;

		// Token: 0x04001280 RID: 4736
		public List<MaskedRect> mMaskedRects = new List<MaskedRect>();

		// Token: 0x04001281 RID: 4737
		public PIEffect mMultFX;

		// Token: 0x04001282 RID: 4738
		public bool mFromHelp;

		// Token: 0x04001283 RID: 4739
		public CurvedVal mDrawScale;

		// Token: 0x04001284 RID: 4740
		public bool mClosing;

		// Token: 0x04001285 RID: 4741
		private Font FONT_SHAGLOUNGE28_STROKE;

		// Token: 0x04001286 RID: 4742
		private Image IMAGE_GUI_ARROW_RED;

		// Token: 0x04001287 RID: 4743
		private Image IMAGE_GUI_BARIMAGE;

		// Token: 0x04001288 RID: 4744
		private Image IMAGE_GUI_EQUALIMAGE;

		// Token: 0x04001289 RID: 4745
		private Image IMAGE_GUI_BALLIMAGE;

		// Token: 0x0400128A RID: 4746
		private Image IMAGE_GUI_DIALOG_MARQUE_BOX;

		// Token: 0x0400128B RID: 4747
		private Image IMAGE_UI_CHALLENGE_GAUGE_EMPTY;

		// Token: 0x0400128C RID: 4748
		private Image IMAGE_UI_CHALLENGE_GAUGE_FILL;
	}
}
