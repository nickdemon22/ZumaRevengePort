using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000059 RID: 89
	public class MainMenuOverlayWidget : Widget, ButtonListener
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0005B556 File Offset: 0x00059756
		public void ButtonPress(int theId)
		{
			if (theId == 11 || theId == 6 || theId == 14)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0005B57A File Offset: 0x0005977A
		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0005B584 File Offset: 0x00059784
		public void ButtonDepress(int theId)
		{
			GameApp mApp = this.mMenu.mApp;
			if (this.mMenu.mFirstTimeAlpha > 0 || this.mMenu.mIFUnlockAnim != null || mApp.mGenericHelp != null || this.mMenu.mDelayedIFStartState > 0 || this.mMenu.ShowingTikiTemple() || mApp.mMapScreen != null)
			{
				return;
			}
			if (mApp.mBambooTransition != null && mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (mApp.GetDialog(2) != null)
			{
				return;
			}
			if (theId == 11)
			{
				GameApp.gApp.ShowLegal();
				return;
			}
			if (theId == 6)
			{
				mApp.DoOptionsDialog(false);
				return;
			}
			if (theId == 14 && GameApp.USE_TRIAL_VERSION)
			{
				this.ProcessLocked();
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0005B634 File Offset: 0x00059834
		public void ProcessLocked()
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string = TextManager.getInstance().getString(834);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(835), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessUnlock);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0005B6CA File Offset: 0x000598CA
		public void ProcessUnlock(int theId)
		{
			if (theId == 1000 && GameApp.USE_TRIAL_VERSION)
			{
				GameApp.gApp.ToMarketPlace();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0005B6F1 File Offset: 0x000598F1
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0005B6F3 File Offset: 0x000598F3
		public void ButtonMouseEnter(int theId)
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0005B6F5 File Offset: 0x000598F5
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0005B6F7 File Offset: 0x000598F7
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0005B6FC File Offset: 0x000598FC
		public MainMenuOverlayWidget(MainMenu theMainMenu)
		{
			this.mMenu = theMainMenu;
			this.mMenuMoreGamesStartY = -1;
			this.mMenuMoreGamesOriginY = -1;
			this.mMenuMoreGamesDestY = -1;
			this.mMenuMoreGamesSignStartY = -1;
			this.mMenuMoreGamesSignOriginY = -1;
			this.mMenuMoreGamesSignDestY = -1;
			this.mMenuMoreGamesSignY = -1;
			this.mMenuOptionsStartX = -1;
			this.mMenuOptionsDestX = -1;
			this.mMenuOptionsOriginX = -1;
			this.mHasTransparencies = true;
			this.mWidgetFlagsMod.mRemoveFlags |= 49;
			GameApp mApp = this.mMenu.mApp;
			this.Resize(mApp.GetScreenRect().mX, mApp.GetScreenRect().mY, mApp.GetScreenRect().mWidth - mApp.GetScreenRect().mX, mApp.GetScreenRect().mHeight - mApp.GetScreenRect().mY);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0005B7D0 File Offset: 0x000599D0
		public void Init()
		{
			GameApp mApp = this.mMenu.mApp;
			this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN);
			this.IMAGE_UI_MAINMENU_OPTIONS_DOWN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN);
			this.IMAGE_UI_MAINMENU_MORE_GAMES = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES);
			this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN);
			this.IMAGE_UI_MAINMENU_OPTIONS = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_OPTIONS);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER);
			this.IMAGE_UI_MAINMENU_RIBBIT = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_RIBBIT);
			this.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE);
			this.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE);
			this.IMAGE_UI_MAINMENU_TIKIHEAD = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD);
			this.IMAGE_UI_MAINMENU_UNLOCK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_UNLOCK);
			this.IMAGE_UI_MAINMENU_UNLOCK_ON = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_UNLOCK_ON);
			this.AddOptionsButton();
			this.AddMoreGamesButton();
			this.mMenuMoreGamesSignY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN));
			this.mMenuMoreGamesSignStartY = this.mMenuMoreGamesSignY;
			this.mMenuMoreGamesSignOriginY = this.mMenuMoreGamesSignY;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0005B8F4 File Offset: 0x00059AF4
		public void DoMoreGamesSlide(bool isSlidingIn)
		{
			this.mMenuMoreGamesSignStartY = this.mMenuMoreGamesSignY;
			this.mMenuMoreGamesStartY = this.mMenu.mMoreGamesButton.mY;
			this.mMenuOptionsStartX = this.mMenu.mOptionsButton.mX;
			if (isSlidingIn)
			{
				this.mMenuMoreGamesDestY = this.mMenuMoreGamesOriginY;
				this.mMenuOptionsDestX = this.mMenuOptionsOriginX;
				this.mMenuMoreGamesSignDestY = this.mMenuMoreGamesSignOriginY;
				return;
			}
			this.mMenuMoreGamesDestY = (this.mMenuMoreGamesSignDestY = this.mMenu.mApp.mScreenBounds.mHeight + Common._S(150));
			this.mMenuOptionsDestX = -Common._S(300);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0005B9A4 File Offset: 0x00059BA4
		public void AddMoreGamesButton()
		{
			GameApp mApp = this.mMenu.mApp;
			int width = this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN.GetWidth();
			int height = this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN.GetHeight();
			float num = (float)width * 0.64f;
			float num2 = (float)height * 0.68f;
			float num3 = (float)Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) + (float)width * 0.18f;
			float num4 = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) + (float)height * 0.16f;
			this.mMenuMoreGamesStartY = (int)num4;
			this.mMenuMoreGamesOriginY = (int)num4;
			this.mMenu.mMoreGamesButton = new ButtonWidget(11, this);
			this.mMenu.mMoreGamesButton.mButtonImage = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			this.mMenu.mMoreGamesButton.mOverImage = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			this.mMenu.mMoreGamesButton.mDownImage = this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;
			this.mMenu.mMoreGamesButton.mBtnNoDraw = true;
			this.mMenu.mMoreGamesButton.mDoFinger = true;
			this.mMenu.mMoreGamesButton.Resize(mApp.GetWideScreenAdjusted((int)num3), (int)num4, (int)num, (int)num2);
			this.mMenu.AddWidget(this.mMenu.mMoreGamesButton);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0005BAE0 File Offset: 0x00059CE0
		public void AddOptionsButton()
		{
			GameApp mApp = this.mMenu.mApp;
			this.mMenu.mOptionsButton = new ButtonWidget(6, this);
			this.mMenu.mOptionsButton.mButtonImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mOptionsButton.mOverImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mOptionsButton.mDownImage = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
			this.mMenu.AddWidget(this.mMenu.mOptionsButton);
			this.mMenu.mOptionsButton.mBtnNoDraw = true;
			this.mMenu.mOptionsButton.mDoFinger = true;
			this.mMenuOptionsOriginX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN));
			this.mMenu.mOptionsButton.Resize(this.mMenuOptionsOriginX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN)), this.IMAGE_UI_MAINMENU_OPTIONS_DOWN.GetWidth(), this.IMAGE_UI_MAINMENU_OPTIONS_DOWN.GetHeight());
			this.mMenu.mUnlockButton = new ButtonWidget(14, this);
			this.mMenu.mUnlockButton.mButtonImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mUnlockButton.mOverImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mUnlockButton.mDownImage = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
			this.mMenu.AddWidget(this.mMenu.mUnlockButton);
			this.mMenu.mUnlockButton.mBtnNoDraw = true;
			this.mMenu.mUnlockButton.mDoFinger = true;
			this.mMenu.mUnlockButton.Resize(325, 230, this.IMAGE_UI_MAINMENU_UNLOCK.GetWidth(), this.IMAGE_UI_MAINMENU_UNLOCK.GetHeight());
			if (GameApp.USE_TRIAL_VERSION)
			{
				this.mMenu.mUnlockButton.SetVisible(true);
				return;
			}
			this.mMenu.mUnlockButton.SetVisible(false);
			this.mMenu.mUnlockButton.SetDisabled(true);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0005BCD0 File Offset: 0x00059ED0
		public void UpdateOverlaySlide(float theSlidePct)
		{
			float num = theSlidePct * (float)(this.mMenuMoreGamesDestY - this.mMenuMoreGamesStartY);
			this.mMenu.mMoreGamesButton.Move(this.mMenu.mMoreGamesButton.mX, (int)((float)this.mMenuMoreGamesStartY + num));
			float num2 = theSlidePct * (float)(this.mMenuMoreGamesSignDestY - this.mMenuMoreGamesSignStartY);
			this.mMenuMoreGamesSignY = (int)((float)this.mMenuMoreGamesSignStartY + num2);
			float num3 = theSlidePct * (float)(this.mMenuOptionsDestX - this.mMenuOptionsStartX);
			this.mMenu.mOptionsButton.Move((int)((float)this.mMenuOptionsStartX + num3), this.mMenu.mOptionsButton.mY);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0005BD74 File Offset: 0x00059F74
		public void DrawOptionsButton(SexyGraphics g)
		{
			Image image = this.IMAGE_UI_MAINMENU_OPTIONS;
			int width = image.GetWidth();
			int height = image.GetHeight();
			if (this.mMenu.mOptionsButton.mIsDown)
			{
				image = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
				width = image.GetWidth();
				height = image.GetHeight();
			}
			int num = (int)((float)this.mMenu.mOptionsButton.mX + (float)(this.mMenu.mOptionsButton.mWidth - width) / 2f);
			int num2 = (int)((float)this.mMenu.mOptionsButton.mY + (float)(this.mMenu.mOptionsButton.mHeight - height) / 2f);
			if ((int)Localization.GetCurrentLanguage() == 3 || (int)Localization.GetCurrentLanguage() == 6 || (int)Localization.GetCurrentLanguage() == 4 || (int)Localization.GetCurrentLanguage() == 9 || (int)Localization.GetCurrentLanguage() == 2)
			{
				g.DrawImage(image, num, num2, (int)((float)image.mWidth * 0.9f), (int)((float)image.mHeight * 0.9f));
			}
			else
			{
				g.DrawImage(image, num, num2);
			}
			if (GameApp.USE_TRIAL_VERSION)
			{
				image = this.IMAGE_UI_MAINMENU_UNLOCK;
				width = image.GetWidth();
				height = image.GetHeight();
				if (this.mMenu.mUnlockButton.mIsDown)
				{
					image = this.IMAGE_UI_MAINMENU_UNLOCK_ON;
					width = image.GetWidth();
					height = image.GetHeight();
				}
				num = this.mMenu.mUnlockButton.mX + 35;
				num2 = this.mMenu.mUnlockButton.mY;
				g.DrawImage(image, num, num2);
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0005BEE8 File Offset: 0x0005A0E8
		public void DrawMoreGamesButton(SexyGraphics g)
		{
			Image image = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			int num = image.GetWidth();
			int height = image.GetHeight();
			if (this.mMenu.mMoreGamesButton.mIsDown)
			{
				image = this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;
				num = image.GetWidth() + Common._DS(20);
				height = image.GetHeight();
			}
			else
			{
				image = this.IMAGE_UI_MAINMENU_MORE_GAMES;
				num = image.GetWidth();
				height = image.GetHeight();
			}
			float num2 = (float)this.mMenu.mMoreGamesButton.mX + (float)(this.mMenu.mMoreGamesButton.mWidth - num) * 0.5f;
			float num3 = (float)this.mMenu.mMoreGamesButton.mY + (float)(this.mMenu.mMoreGamesButton.mHeight - height) * 0.5f;
			g.DrawImage(image, (int)num2, (int)num3);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
		public override void Draw(SexyGraphics g)
		{
			GameApp mApp = this.mMenu.mApp;
			if (mApp.mCredits != null && MathUtils._geq(mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mMenu.mChallengeMenu != null)
			{
				return;
			}
			if (mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mMenu.mTikiTemple != null)
			{
				return;
			}
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW, 0, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER)) + (this.mMenu.mMainMenuButtonsScrollWidget.mY - this.mMenu.mMenuScrollOriginY));
			g.DrawImageMirror(this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW, mApp.GetScreenRect().mWidth - mApp.GetScreenRect().mX - this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER)) + (this.mMenu.mMainMenuButtonsScrollWidget.mY - this.mMenu.mMenuScrollOriginY));
			g.DrawImage(this.IMAGE_UI_MAINMENU_RIBBIT, this.mMenu.mMenuFrogX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_RIBBIT)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE)) - mApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE)) - mApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_TIKIHEAD, this.mMenu.mMenuTikiX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) - mApp.mWideScreenXOffset + mApp.GetScreenRect().mX, this.mMenuMoreGamesSignY);
			this.DrawOptionsButton(g);
			this.DrawMoreGamesButton(g);
			if (this.mMenu.mFirstTimeAlpha > 0)
			{
				g.SetColor(new Color(0, 0, 0, this.mMenu.mFirstTimeAlpha));
				g.FillRect(Common._S(-80), 0, this.mWidth + Common._S(160), this.mHeight);
			}
		}

		// Token: 0x04001233 RID: 4659
		private MainMenu mMenu;

		// Token: 0x04001234 RID: 4660
		private int mMenuMoreGamesStartY;

		// Token: 0x04001235 RID: 4661
		private int mMenuMoreGamesOriginY;

		// Token: 0x04001236 RID: 4662
		private int mMenuMoreGamesDestY;

		// Token: 0x04001237 RID: 4663
		private int mMenuMoreGamesSignStartY;

		// Token: 0x04001238 RID: 4664
		private int mMenuMoreGamesSignOriginY;

		// Token: 0x04001239 RID: 4665
		private int mMenuMoreGamesSignDestY;

		// Token: 0x0400123A RID: 4666
		private int mMenuMoreGamesSignY;

		// Token: 0x0400123B RID: 4667
		private int mMenuOptionsStartX;

		// Token: 0x0400123C RID: 4668
		private int mMenuOptionsDestX;

		// Token: 0x0400123D RID: 4669
		private int mMenuOptionsOriginX;

		// Token: 0x0400123E RID: 4670
		private Image IMAGE_UI_MAINMENU_MORE_GAMES_SIGN;

		// Token: 0x0400123F RID: 4671
		private Image IMAGE_UI_MAINMENU_OPTIONS_DOWN;

		// Token: 0x04001240 RID: 4672
		private Image IMAGE_UI_MAINMENU_MORE_GAMES;

		// Token: 0x04001241 RID: 4673
		private Image IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;

		// Token: 0x04001242 RID: 4674
		private Image IMAGE_UI_MAINMENU_OPTIONS;

		// Token: 0x04001243 RID: 4675
		private Image IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW;

		// Token: 0x04001244 RID: 4676
		private Image IMAGE_UI_MAINMENU_SCROLLMENU_BORDER;

		// Token: 0x04001245 RID: 4677
		private Image IMAGE_UI_MAINMENU_RIBBIT;

		// Token: 0x04001246 RID: 4678
		private Image IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE;

		// Token: 0x04001247 RID: 4679
		private Image IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE;

		// Token: 0x04001248 RID: 4680
		private Image IMAGE_UI_MAINMENU_TIKIHEAD;

		// Token: 0x04001249 RID: 4681
		private Image IMAGE_UI_MAINMENU_UNLOCK;

		// Token: 0x0400124A RID: 4682
		private Image IMAGE_UI_MAINMENU_UNLOCK_ON;
	}
}
