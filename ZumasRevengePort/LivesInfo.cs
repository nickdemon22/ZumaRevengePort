using System;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200007A RID: 122
	public class LivesInfo : IDisposable
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x00075FC4 File Offset: 0x000741C4
		public LivesInfo(Board board, int theLivesDelta)
		{
			this.mFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.mLivesDelta = theLivesDelta;
			this.mDisplayTime = 1200UL;
			this.mDisplayStart = ulong.MaxValue;
			this.mWaitTime = 150UL;
			this.mLivesCount = board.GetNumLives() - 1;
			this.mSlideVal.SetConstant(0.0);
			this.mSlideVal.mAppUpdateCountSrc = board.mUpdateCnt;
			this.InitLayout();
			this.StartSliding(LivesInfo.SLIDE_STATE.SLIDE_ON, 0);
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.delay = 50;
			GameApp.gApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_NEW_EXTRA_LIFE), soundAttribs);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000760A2 File Offset: 0x000742A2
		public virtual void Dispose()
		{
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.mImage.Dispose();
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000760C4 File Offset: 0x000742C4
		public void Draw(SexyGraphics g)
		{
			this.mFrame.mX = (this.mInset.mX = (int)((float)this.mFrame.mX - g.mTransX));
			this.DrawPlank(g);
			this.DrawLivesCount(g);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0007610C File Offset: 0x0007430C
		public void Update()
		{
			switch (this.mSlideState)
			{
			case LivesInfo.SLIDE_STATE.SLIDE_ON:
				this.DisplayOldCount();
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN:
				this.SlideOff();
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_OFF:
				if (!this.IsSliding())
				{
					this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_OFFSCREEN;
				}
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_WAIT:
				this.DisplayCount();
				break;
			}
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.Update();
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0007617B File Offset: 0x0007437B
		public bool IsDone()
		{
			return this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_OFFSCREEN;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00076188 File Offset: 0x00074388
		private void InitLayout()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LIVESFRAME);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_FROG_LIVES);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_POLE);
			this.mXOffset = imageByID3.GetWidth();
			int height = imageByID.GetHeight();
			int num = GameApp.gApp.GetScreenRect().mX - GameApp.gApp.mWideScreenXOffset;
			int num2 = GameApp.gApp.GetScreenRect().mHeight - height;
			int num3 = (int)((float)height * 0.13f);
			this.mTextXOffset = imageByID2.GetWidth() + Common._S(50);
			this.mFrame = new Rect(num, num2, 0, height);
			this.mFrame.mWidth = this.mTextXOffset + this.mFont.StringWidth("x 00");
			this.mFrame.mX = this.mFrame.mX - this.mFrame.mWidth;
			this.mInset = this.mFrame;
			this.mInset.mWidth = this.mInset.mWidth - num3;
			this.mInset.mHeight = this.mInset.mHeight - num3;
			this.mInset.mY = this.mInset.mY + num3;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000762B0 File Offset: 0x000744B0
		private void StartSliding(LivesInfo.SLIDE_STATE inSlideState, int inXPos)
		{
			this.mSlideState = inSlideState;
			this.mXStart = (float)(this.mFrame.mX + this.mXOffset);
			this.mXEnd = (float)(GameApp.gApp.GetScreenRect().mX + inXPos + this.mXOffset);
			this.mSlideVal.SetCurve(Common._MP("b70,1,0.04,1,#     $P    }~"));
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00076314 File Offset: 0x00074514
		private void DrawPlank(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_FROG_LIVES);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LIVESFRAME);
			int mX = this.mInset.mX;
			int num = (int)((float)this.mInset.mY + (float)(this.mInset.mHeight - imageByID.GetHeight()) * 0.5f);
			g.DrawImageBox(this.mFrame, imageByID2);
			g.DrawImage(imageByID, mX, num);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00076384 File Offset: 0x00074584
		private void DrawLivesCount(SexyGraphics g)
		{
			if (this.mSlideState != LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN)
			{
				int num = this.CapAt99((this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_ON || this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_WAIT) ? (this.mLivesCount - this.mLivesDelta) : this.mLivesCount);
				string text = "x  " + num;
				g.SetFont(this.mFont);
				g.SetColor(Color.White);
				g.WriteString(text, this.mInset.mX + this.mTextXOffset, this.mInset.mY + this.mFont.GetHeight());
				return;
			}
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.Draw(g);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00076444 File Offset: 0x00074644
		private void DisplayOldCount()
		{
			if (this.IsSliding())
			{
				return;
			}
			this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_WAIT;
			this.mDisplayStart = (ulong)Common.SexyTime();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00076464 File Offset: 0x00074664
		private void DisplayCount()
		{
			if ((ulong)Common.SexyTime() - this.mDisplayStart < this.mWaitTime)
			{
				return;
			}
			this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN;
			this.mDisplayStart = (ulong)Common.SexyTime();
			this.InitLivesText();
			this.PreDrawLivesText(this.CapAt99(this.mLivesCount));
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000764B2 File Offset: 0x000746B2
		private void SlideOff()
		{
			if ((ulong)Common.SexyTime() - this.mDisplayStart < this.mDisplayTime)
			{
				return;
			}
			this.mLivesText.mImage = null;
			this.StartSliding(LivesInfo.SLIDE_STATE.SLIDE_OFF, -this.mFrame.mWidth);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000764EC File Offset: 0x000746EC
		private bool IsSliding()
		{
			if (this.mSlideVal.IsDoingCurve())
			{
				float num = (float)this.mSlideVal.GetOutVal() * (this.mXEnd - this.mXStart);
				this.mFrame.mX = (this.mInset.mX = (int)(this.mXStart + num));
				return true;
			}
			return false;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00076547 File Offset: 0x00074747
		private int CapAt99(int inLivesCount)
		{
			if (inLivesCount < 0)
			{
				return 0;
			}
			if (inLivesCount > 99)
			{
				return 99;
			}
			return inLivesCount;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00076558 File Offset: 0x00074758
		private void InitLivesText()
		{
			int num = this.mFont.StringWidth("x 00") + Common._S(20);
			int num2 = this.mFont.GetHeight() + Common._S(10);
			this.mLivesText = new FwooshImage();
			this.mLivesText.mAlphaDec = 0f;
			this.mLivesText.mImage = new DeviceImage();
			this.mLivesText.mImage.mApp = GameApp.gApp;
			this.mLivesText.mImage.SetImageMode(true, true);
			this.mLivesText.mImage.AddImageFlags(16U);
			this.mLivesText.mImage.Create(num, num2);
			this.mLivesText.mX = this.mInset.mX + this.mTextXOffset;
			this.mLivesText.mY = this.mInset.mY + (int)((float)this.mInset.mHeight * 0.5f) + Common._S(5);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00076658 File Offset: 0x00074858
		private void PreDrawLivesText(int inLivesCount)
		{
			SexyGraphics graphics = new SexyGraphics(this.mLivesText.mImage);
			graphics.Get3D().ClearColorBuffer(new Color(0, 0, 0, 0));
			graphics.SetFont(this.mFont);
			graphics.SetColor(Color.White);
			graphics.WriteString("x " + inLivesCount + " ", 0, this.mFont.GetAscent(), this.mLivesText.mImage.GetWidth());
			graphics.ClearRenderContext();
		}

		// Token: 0x0400140C RID: 5132
		private Font mFont;

		// Token: 0x0400140D RID: 5133
		private Rect mFrame = default(Rect);

		// Token: 0x0400140E RID: 5134
		private Rect mInset = default(Rect);

		// Token: 0x0400140F RID: 5135
		private int mTextXOffset;

		// Token: 0x04001410 RID: 5136
		private int mXOffset;

		// Token: 0x04001411 RID: 5137
		private FwooshImage mLivesText = new FwooshImage();

		// Token: 0x04001412 RID: 5138
		private int mLivesCount;

		// Token: 0x04001413 RID: 5139
		private int mLivesDelta;

		// Token: 0x04001414 RID: 5140
		private float mXStart;

		// Token: 0x04001415 RID: 5141
		private float mXEnd;

		// Token: 0x04001416 RID: 5142
		private ulong mDisplayTime;

		// Token: 0x04001417 RID: 5143
		private ulong mDisplayStart;

		// Token: 0x04001418 RID: 5144
		private ulong mWaitTime;

		// Token: 0x04001419 RID: 5145
		private LivesInfo.SLIDE_STATE mSlideState;

		// Token: 0x0400141A RID: 5146
		private CurvedVal mSlideVal = new CurvedVal();

		// Token: 0x0200011D RID: 285
		private enum SLIDE_STATE
		{
			// Token: 0x0400198B RID: 6539
			SLIDE_ON,
			// Token: 0x0400198C RID: 6540
			SLIDE_ONSCREEN,
			// Token: 0x0400198D RID: 6541
			SLIDE_OFF,
			// Token: 0x0400198E RID: 6542
			SLIDE_OFFSCREEN,
			// Token: 0x0400198F RID: 6543
			SLIDE_WAIT,
			// Token: 0x04001990 RID: 6544
			NUM_SLIDE_STATES
		}
	}
}
