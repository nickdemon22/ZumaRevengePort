using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x020000F8 RID: 248
	public class CSButton : ButtonWidget
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x0009C3B4 File Offset: 0x0009A5B4
		public CSButton(int id, ChallengeMenu theChallengeMenu, ButtonListener listener) : base(id, listener)
		{
			this.mChallengeMenu = theChallengeMenu;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0009C40A File Offset: 0x0009A60A
		public override void Dispose()
		{
			if (this.mUnlockSparkles != null)
			{
				this.mUnlockSparkles.Dispose();
				this.mUnlockSparkles = null;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0009C428 File Offset: 0x0009A628
		public override void Draw(SexyGraphics g)
		{
			if (g.mClipRect.mWidth <= 0 || g.mClipRect.mHeight <= 0)
			{
				return;
			}
			CSButton.last_uc = this.mUpdateCnt;
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			bool flag2 = this.mId - 3 + 1 == GameApp.gLastLevel && this.mChallengeMenu.mCrownZoomType >= 0;
			int num = flag ? Common._DS(Common._M(0)) : 0;
			int num2 = flag ? Common._DS(Common._M(0)) : 0;
			Image image = null;
			if (this.mLevel != -1)
			{
				image = GameApp.gApp.GetLevelThumbnail(this.mLevel);
			}
			if (image != null)
			{
				g.DrawImage(image, GlobalChallenge.gScreenShake + num, GlobalChallenge.gScreenShake + num2, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
				if (this.mMouseOver)
				{
					g.PushState();
					g.SetColor(new Color(255, 255, 255, Common._M(100)));
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.DrawImage(image, GlobalChallenge.gScreenShake + num, GlobalChallenge.gScreenShake + num2, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
					g.PopState();
				}
				if (flag)
				{
					g.DrawImage(Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CH_THUMBNAILOVERLAY), GlobalChallenge.gScreenShake, GlobalChallenge.gScreenShake, Common._DS(GlobalChallenge.CS_BTN_WIDTH + Common._M(0)), Common._DS(GlobalChallenge.CS_BTN_HEIGHT + Common._M1(0)));
				}
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CS_LOCK_ANIMATION);
			if (this.mOpaque)
			{
				g.SetColor(new Color(0, 0, 0, Common._M(191)));
				g.FillRect(0, 0, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
			}
			else if (this.mMedal == imageByID)
			{
				g.SetColor(new Color(0, 0, 0, 120));
				g.FillRect(0, 0, Common._DS(GlobalChallenge.CS_BTN_WIDTH), Common._DS(GlobalChallenge.CS_BTN_HEIGHT));
			}
			Common.DrawCommonDialogBorder(g, GlobalChallenge.gScreenShake - Common._DS(15), GlobalChallenge.gScreenShake - Common._DS(15), this.mWidth + Common._DS(30), this.mHeight + Common._DS(30));
			if (this.mUnlockAlpha > 0)
			{
				Image image2 = imageByID;
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, this.mUnlockAlpha));
				g.DrawImageCel(image2, (this.mWidth - image2.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake, (this.mHeight - image2.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, this.mLockCel);
				g.SetColorizeImages(false);
			}
			if (this.mMedal != null)
			{
				if (!flag2 || !g.Is3D())
				{
					if (this.mMedal == imageByID)
					{
						g.DrawImageCel(this.mMedal, (this.mWidth - this.mMedal.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake + Common._DS(10), (this.mHeight - this.mMedal.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, 0);
					}
					else
					{
						g.DrawImageCel(this.mMedal, (this.mWidth - this.mMedal.GetCelWidth()) / 2 + GlobalChallenge.gScreenShake, (this.mHeight - this.mMedal.GetCelHeight()) / 2 + GlobalChallenge.gScreenShake, 0);
					}
				}
				else if (this.mMedal != null)
				{
					g.PushState();
					g.ClearClipRect();
					g.Translate(-this.mX, -this.mY);
					Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_CROWN);
					if (this.mChallengeMenu.mCrownZoomType == 1)
					{
						g.DrawImage(imageByID2, this.mX + (this.mWidth - imageByID2.mWidth) / 2, this.mY + (this.mHeight - imageByID2.mHeight) / 2);
						imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_ACECROWN);
					}
					g.SetColor(new Color(255, 255, 255, (int)this.mChallengeMenu.mCrownAlpha));
					g.SetColorizeImages(true);
					SexyTransform2D sexyTransform2D;
					sexyTransform2D = new SexyTransform2D(false);
					sexyTransform2D.Scale(this.mChallengeMenu.mCrownSize, this.mChallengeMenu.mCrownSize);
					sexyTransform2D.Translate((float)this.mX + ((float)this.mWidth - (float)imageByID2.mWidth * this.mChallengeMenu.mCrownSize) / 2f, (float)this.mY + ((float)this.mHeight - (float)imageByID2.mHeight * this.mChallengeMenu.mCrownSize) / 2f);
					g.DrawImageMatrix(imageByID2, sexyTransform2D, (float)imageByID2.mWidth * this.mChallengeMenu.mCrownSize / 2f, (float)imageByID2.mHeight * this.mChallengeMenu.mCrownSize / 2f);
					g.PopState();
				}
			}
			if (!flag2 && this.mUnlockSparkles != null)
			{
				g.Is3D();
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0009C940 File Offset: 0x0009AB40
		public override void Update()
		{
			this.mUpdateCnt++;
			bool flag = this.mChallengeMenu.mCrownZoomType >= 0;
			if (this.mUnlockSparkles != null && !flag)
			{
				this.mUnlockSparkles.Update();
				this.MarkDirty();
				if (this.mUnlockSparkles.mCurNumParticles == 0 && this.mUnlockSparkles.mFrameNum > 10f)
				{
					this.mUnlockSparkles.Dispose();
					this.mUnlockSparkles = null;
				}
			}
			if (!flag)
			{
				if (this.mLockCel < Res.GetImageByID(ResID.IMAGE_UI_MAIN_MENU_CS_LOCK_ANIMATION).mNumCols - 1 && this.mUpdateCnt % Common._M(8) == 0)
				{
					this.mLockCel++;
					return;
				}
				if (this.mUnlockAlpha > 0)
				{
					this.mUnlockAlpha -= Common._M(2);
				}
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0009CA10 File Offset: 0x0009AC10
		public override void MouseEnter()
		{
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0009CA12 File Offset: 0x0009AC12
		public override void MouseLeave()
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0009CA14 File Offset: 0x0009AC14
		public void PreLoadImage()
		{
			if (this.mLevel != -1)
			{
				GameApp.gApp.GetLevelThumbnail(this.mLevel);
			}
		}

		// Token: 0x0400188C RID: 6284
		private static int last_uc;

		// Token: 0x0400188D RID: 6285
		public PIEffect mUnlockSparkles;

		// Token: 0x0400188E RID: 6286
		public int mUnlockAlpha;

		// Token: 0x0400188F RID: 6287
		public int mLockCel;

		// Token: 0x04001890 RID: 6288
		public Image mMedal;

		// Token: 0x04001891 RID: 6289
		public string mScoreStr = "";

		// Token: 0x04001892 RID: 6290
		public string mLevelStr = "";

		// Token: 0x04001893 RID: 6291
		public string mAceStr = "";

		// Token: 0x04001894 RID: 6292
		public string mLevelId = "";

		// Token: 0x04001895 RID: 6293
		public bool mMouseOver;

		// Token: 0x04001896 RID: 6294
		public bool mOpaque = true;

		// Token: 0x04001897 RID: 6295
		public int mLevel = -1;

		// Token: 0x04001898 RID: 6296
		public ChallengeMenu mChallengeMenu;

		// Token: 0x020000F9 RID: 249
		public enum BtnType
		{
			// Token: 0x0400189A RID: 6298
			Btn_CS_Back,
			// Token: 0x0400189B RID: 6299
			Btn_CS_PrevSet,
			// Token: 0x0400189C RID: 6300
			Btn_CS_NextSet,
			// Token: 0x0400189D RID: 6301
			Btn_First_Challenge
		}
	}
}
