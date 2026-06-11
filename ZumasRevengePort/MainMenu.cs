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
	// Token: 0x0200002C RID: 44
	public class MainMenu : Widget, ButtonListener, DialogListener, PopAnimListener
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x00042662 File Offset: 0x00040862
		public void ButtonPress(int theId)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00042664 File Offset: 0x00040864
		public void ButtonPress(int theId, int theClickCount)
		{
			if (this.ShowingTikiTemple() || this.mApp.mGenericHelp != null || this.mApp.mMapScreen != null || this.mApp.mCredits != null)
			{
				return;
			}
			if (theId != 8 && this.mMainMenuOverlayWidget != null)
			{
				this.mMainMenuOverlayWidget.ButtonPress(theId);
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000426D0 File Offset: 0x000408D0
		public void ButtonDepress(int theId)
		{
			if (this.mFirstTimeAlpha > 0 || this.mIFUnlockAnim != null || this.mApp.mGenericHelp != null || this.mDelayedIFStartState > 0 || this.ShowingTikiTemple() || this.mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mState == MainMenu_State.State_Scroll)
			{
				return;
			}
			if (this.mApp.mBambooTransition != null && this.mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			ChallengeMenu challengeMenu = this.mChallengeMenu;
			if (this.mApp.GetDialog(2) != null)
			{
				return;
			}
			this.mTip = null;
			this.mApp.mClickedHardMode = false;
			if (theId == 17)
			{
				return;
			}
			if (theId == 11)
			{
				return;
			}
			if (theId == 6)
			{
				return;
			}
			if (theId == 7)
			{
				this.mSkipEnterSound = true;
				if (this.mApp.DoYesNoDialog(TextManager.getInstance().getString(448), TextManager.getInstance().getString(453), true) == 1000)
				{
					if (!this.mApp.IsRegistered() && this.mApp.mTrialType == 1 && this.mApp.GetBoolean("UpsellExit", false))
					{
						this.mApp.DoUpsell(true);
						return;
					}
					this.mApp.Shutdown();
					return;
				}
			}
			else
			{
				if (theId == 8)
				{
					return;
				}
				if (theId == 1)
				{
					if (!this.mApp.ChallengeModeUnlocked())
					{
						this.mState = MainMenu_State.State_UnlockPrompt;
						this.mApp.DoGenericDialog(TextManager.getInstance().getString(837), TextManager.getInstance().getString(838), true, new GameApp.PreBlockCallback(this.ChangeMainMenuState), Common._DS(100));
						this.mSkipEnterSound = true;
						return;
					}
					this.mApp.mUserProfile.mDoChallengeAceCupComplete = (this.mApp.mUserProfile.mDoChallengeCupComplete = false);
					this.mApp.mUserProfile.mDoChallengeAceTrophyZoom = (this.mApp.mUserProfile.mDoChallengeTrophyZoom = false);
					this.mApp.mUserProfile.mNewChallengeCupUnlocked = false;
					this.ShowChallengeMenu();
					return;
				}
				else
				{
					if (theId == 16)
					{
						this.DoMainMenu(true);
						return;
					}
					if (theId == 15)
					{
						ButtonWidget buttonWidget = null;
						for (int i = 0; i < this.mButtons.Count; i++)
						{
							if (this.mButtons[i].mId == theId)
							{
								buttonWidget = this.mButtons[i];
								break;
							}
						}
						buttonWidget.SetVisible(false);
						this.MarkDirty();
						this.mDelayedIFStartState = 1;
						return;
					}
					if (theId == 12)
					{
						if (this.mApp.mAutoMonkey != null)
						{
							this.mApp.mAutoMonkey.mEnableAutoMonkey = !this.mApp.mAutoMonkey.mEnableAutoMonkey;
							return;
						}
					}
					else
					{
						if (theId == 13)
						{
							return;
						}
						if (this.mMainMenuButtonsWidget != null)
						{
							this.mMainMenuButtonsWidget.ButtonDepress(theId);
						}
						if (this.mMainMenuOverlayWidget != null)
						{
							this.mMainMenuOverlayWidget.ButtonDepress(theId);
						}
					}
				}
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00042993 File Offset: 0x00040B93
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00042995 File Offset: 0x00040B95
		public void ButtonMouseEnter(int theId)
		{
			if (this.mApp.mCredits != null || this.mApp.mGenericHelp != null)
			{
				return;
			}
			this.mSkipEnterSound = false;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000429B9 File Offset: 0x00040BB9
		public void ButtonMouseLeave(int theId)
		{
			if (this.mApp.mCredits != null)
			{
				return;
			}
			this.MarkDirty();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000429CF File Offset: 0x00040BCF
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000429D1 File Offset: 0x00040BD1
		public void ChangeMainMenuState()
		{
			this.mState = MainMenu_State.State_MainMenu;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000429DA File Offset: 0x00040BDA
		public void DialogButtonPress(int theDialogId, int theButtonId)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000429DC File Offset: 0x00040BDC
		public void DialogButtonDepress(int theDialogId, int theButtonId)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000429DE File Offset: 0x00040BDE
		public void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000429E0 File Offset: 0x00040BE0
		public PIEffect PopAnimLoadParticleEffect(string theEffectName)
		{
			return null;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000429E3 File Offset: 0x00040BE3
		public bool PopAnimObjectPredraw(int theId, SexyGraphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000429E6 File Offset: 0x00040BE6
		public bool PopAnimObjectPostdraw(int theId, SexyGraphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000429E9 File Offset: 0x00040BE9
		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, SexyGraphics g, int theDrawCount)
		{
			return (ImagePredrawResult)1;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000429EC File Offset: 0x00040BEC
		public void PopAnimStopped(int theId)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000429EE File Offset: 0x00040BEE
		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000429F0 File Offset: 0x00040BF0
		public bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam)
		{
			this.PopAnimCommand(theId, theCommand, theParam);
			return true;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00042A00 File Offset: 0x00040C00
		public MainMenu(GameApp app)
		{
			this.mState = MainMenu_State.State_MainMenu;
			this.mUserSelDlg = null;
			this.mApp = app;
			this.mDistance = 0f;
			this.mAddAcc = 0f;
			this.mIFSparkle = null;
			this.mIFUnlockAnim = null;
			this.mHeroicSparkle = null;
			this.mTip = null;
			this.mFirstTimeAlpha = 0;
			this.mDelayedIFStartState = 0;
			this.mIncLavaAlpha = true;
			this.mUpsellBtn = null;
			this.mMainMenuButtonsWidget = null;
			this.mMenuScrollOriginY = -1;
			this.mMenuScrollDestY = -1;
			this.mMenuScrollStartY = -1;
			this.mMenuTikiStartX = -1;
			this.mMenuTikiDestX = -1;
			this.mMenuTikiOriginX = -1;
			this.mMenuTikiX = -1;
			this.mMenuFrogX = -1;
			this.mMenuFrogOriginX = -1;
			this.mMenuFrogStartX = -1;
			this.mMenuFrogDestX = -1;
			this.mMenuTikiDudeX = -1;
			this.mMenuTikiDudeDestX = -1;
			this.mMenuTikiDudeStartX = -1;
			this.mMenuTikiDudeOriginX = -1;
			this.mChallengeSparkle = null;
			this.mChallengeMenu = null;
			this.mTikiTemple = null;
			this.mMonkeyButton = null;
			this.mLogButton = null;
			this.mChangeProfileBtn = null;
			this.mClip = false;
			this.mMainMenuOverlayWidget = null;
			this.mTikiTeethSparkle = null;
			this.mVolcanoSmoke = null;
			this.mVolcanoProjectiles = null;
			this.mEffectBatch = new PIEffectBatch();
			this.mMoreGamesButton = null;
			this.mOptionsButton = null;
			this.mUnlockButton = null;
			this.mMenuScrollPct.SetConstant(0.0);
			this.mMenuScrollPct.mAppUpdateCountSrc = this.mUpdateCnt;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00042C98 File Offset: 0x00040E98
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			if (this.mTikiTemple != null)
			{
				this.mWidgetManager.RemoveWidget(this.mTikiTemple);
			}
			if (this.mChallengeMenu != null)
			{
				this.mWidgetManager.RemoveWidget(this.mChallengeMenu);
			}
			this.mVolcanoSmoke.Dispose();
			this.mTikiTeethSparkle.Dispose();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00042CF8 File Offset: 0x00040EF8
		public void DoIronFrog(bool scroll)
		{
			if (!scroll)
			{
				this.mState = MainMenu_State.State_IF;
				List<ButtonWidget>.Enumerator enumerator = this.mButtons.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.Move(enumerator.Current.mX + Common._S(960), enumerator.Current.mY);
				}
			}
			else
			{
				this.mState = MainMenu_State.State_Scroll;
				this.mDistance = 960f;
			}
			this.mAddAcc = 0f;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00042D74 File Offset: 0x00040F74
		public void DoMainMenu(bool scroll)
		{
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			if (!scroll)
			{
				this.mState = MainMenu_State.State_MainMenu;
			}
			else
			{
				this.mDistance = 960f;
				this.mState = MainMenu_State.State_Scroll;
			}
			this.mAddAcc = 0f;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00042DB4 File Offset: 0x00040FB4
		public void InitSparkles()
		{
			if (this.mApp.mUserProfile != null && this.mHeroicSparkle == null && this.mApp.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[5] > 0 && !this.mApp.mUserProfile.mHasDoneHeroicUnlockEffect)
			{
				MainMenu.gNeedsOtherModeUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneHeroicUnlockEffect = true;
				this.mHeroicSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num = GameApp.DownScaleNum(1f);
				this.mHeroicSparkle.mDrawTransform.Scale(num, num);
				this.mHeroicSparkle.mDrawTransform.Translate((float)(this.mPts[5].mX + Common._DS(Common._M(180))), (float)(this.mPts[5].mY - Common._DS(Common._M1(60))));
			}
			if (this.mApp.mUserProfile != null && this.mChallengeSparkle == null && this.mApp.mUserProfile.mAdvModeVars.mHighestLevelBeat >= 10 && !this.mApp.mUserProfile.mHasDoneChallengeUnlockEffect)
			{
				MainMenu.gNeedsOtherModeUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneChallengeUnlockEffect = true;
				this.mChallengeSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num2 = GameApp.DownScaleNum(1f);
				this.mChallengeSparkle.mDrawTransform.Scale(num2, num2);
				this.mChallengeSparkle.mDrawTransform.Translate((float)(this.mPts[1].mX + Common._DS(Common._M(180))), (float)(this.mPts[1].mY - Common._DS(Common._M1(-60))));
			}
			if (this.mApp.mUserProfile != null && this.mIFSparkle == null && this.mApp.IronFrogUnlocked() && !this.mApp.mUserProfile.mHasDoneIFUnlockEffect)
			{
				MainMenu.gNeedsIFUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneIFUnlockEffect = true;
				this.mIFSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num3 = GameApp.DownScaleNum(1f);
				this.mIFSparkle.mDrawTransform.Scale(num3, num3);
				this.mIFSparkle.mDrawTransform.Scale(Common._M(3f), Common._M1(2f));
				this.mIFSparkle.mDrawTransform.Translate((float)Common._DS(Common._M(-30)), (float)Common._DS(Common._M1(900)));
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00043058 File Offset: 0x00041258
		public void DoMoreGamesSlide(bool isSlidingIn)
		{
			this.mMenuScrollStartY = this.mMainMenuButtonsScrollWidget.mY;
			this.mMenuScrollPct.SetCurve(Common._MP("b30,1,0.02,1,#  ,#  tO  o~  3~"));
			this.mMenuTikiStartX = this.mMenuTikiX;
			this.mMenuFrogStartX = this.mMenuFrogX;
			this.mMenuTikiDudeStartX = this.mMenuTikiDudeX;
			if (isSlidingIn)
			{
				this.mMenuScrollDestY = this.mMenuScrollOriginY;
				this.mMenuTikiDestX = this.mMenuTikiOriginX;
				this.mMenuFrogDestX = this.mMenuFrogOriginX;
				this.mMenuTikiDudeDestX = this.mMenuTikiDudeOriginX;
			}
			else
			{
				this.mMenuScrollDestY = this.mApp.mScreenBounds.mHeight + Common._S(150);
				this.mMenuTikiDudeDestX = (this.mMenuTikiDestX = -Common._S(300));
				this.mMenuFrogDestX = this.mApp.GetScreenWidth() + Common._S(300);
			}
			this.mMainMenuOverlayWidget.DoMoreGamesSlide(isSlidingIn);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00043148 File Offset: 0x00041348
		public void Init()
		{
			this.IMAGE_UI_MAINMENU_TIKI = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKI);
			this.mLavaXOff = 0f;
			this.mLavaXScale = 1.09f;
			this.mLavaProjectileXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 150 : 60)));
			this.mLavaSmokeXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 150 : 60)));
			this.mTeethSparkleXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 144 : 57)));
			this.mUpdateCnt = 0;
			this.LoadTalkingBubbleText();
			if (this.mTalkingBubbleTextOptions.Count > 0)
			{
				Random random = new Random();
				int num = random.Next(0, this.mTalkingBubbleTextOptions.Count - 1);
				this.AddText(this.mTalkingBubbleTextOptions[num]);
			}
			this.InitSparkles();
			this.mMainMenuButtonsWidget = new MainMenuButtonsWidget(this, this.mApp);
			this.mMainMenuButtonsWidget.Resize(0, 0, this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsWidget.mHeight);
			this.mMainMenuButtonsScrollWidget = new ScrollWidget();
			this.mMainMenuButtonsScrollWidget.EnableBounce(false);
			this.mMenuScrollOriginY = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI));
			this.mMainMenuButtonsScrollWidget.Resize(0, this.mMenuScrollOriginY, this.mApp.GetScreenWidth(), this.mApp.GetScreenRect().mHeight);
			this.mMainMenuButtonsScrollWidget.AddWidget(this.mMainMenuButtonsWidget);
			this.mMainMenuButtonsScrollWidget.SetScrollMode((ScrollWidget.ScrollMode)1);
			this.mMainMenuButtonsScrollWidget.EnablePaging(true);
			this.AddWidget(this.mMainMenuButtonsScrollWidget);
			this.mMenuTikiOriginX = (this.mMenuTikiX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD)) - this.mApp.mWideScreenXOffset);
			this.mMenuFrogOriginX = (this.mMenuFrogX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_RIBBIT)) - this.mApp.mWideScreenXOffset + 42);
			this.mMenuTikiDudeOriginX = (this.mMenuTikiDudeX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_GUY)) - this.mApp.mWideScreenXOffset);
			Insets insets = new Insets();
			insets.mLeft = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI)) - this.IMAGE_UI_MAINMENU_TIKI.GetWidth() - this.mApp.GetScreenRect().mX + 67;
			insets.mRight = this.mApp.GetScreenWidth() - (insets.mLeft + this.mMainMenuButtonsWidget.mWidth / this.mMainMenuButtonsWidget.GetNumButtons());
			insets.mTop = 0;
			insets.mBottom = 0;
			this.mMainMenuOverlayWidget = new MainMenuOverlayWidget(this);
			this.mMainMenuOverlayWidget.Init();
			this.AddWidget(this.mMainMenuOverlayWidget);
			this.mMainMenuButtonsScrollWidget.SetScrollInsets(insets);
			this.mMainMenuButtonsScrollWidget.SetPageHorizontal(2, false);
			this.mMainMenuButtonsScrollWidget.SetPageHorizontal(0, true);
			if (this.mVolcanoSmoke == null)
			{
				this.mVolcanoSmoke = this.mApp.GetPIEffect("ls_volcano_smoke");
				this.mVolcanoSmoke.mEmitAfterTimeline = true;
				Common.SetFXNumScale(this.mVolcanoSmoke, 4f);
				this.mEffectBatch.AddEffect(this.mVolcanoSmoke);
			}
			if (this.mTikiTeethSparkle == null)
			{
				this.mTikiTeethSparkle = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
				this.mTikiTeethSparkle.mEmitAfterTimeline = true;
				Common.SetFXNumScale(this.mTikiTeethSparkle, 3f);
				this.mEffectBatch.AddEffect(this.mTikiTeethSparkle);
			}
			this.CreateChangeProfileButton();
			this.RehupButtons();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0004350D File Offset: 0x0004170D
		public void CreateChangeProfileButton()
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0004350F File Offset: 0x0004170F
		public override void AddedToManager(WidgetManager mgr)
		{
			base.AddedToManager(mgr);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00043518 File Offset: 0x00041718
		public bool ShouldShowUpsellBtn()
		{
			return this.mApp.mUserProfile != null && this.mApp.mTrialType != 0 && this.mApp.mUserProfile.mAdvModeVars.mCurrentAdvZone > 2;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0004354E File Offset: 0x0004174E
		public void CloseUserSelDialog()
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00043550 File Offset: 0x00041750
		public void RehupButtons()
		{
			this.GetButton(10);
			this.mDrawHat = (this.mDrawFro = (this.mDrawTuxedo = (this.mDrawMoustache = false)));
			if (this.mApp.mUserProfile != null)
			{
				this.mDrawHat = (this.mApp.mUserProfile.mHeroicModeVars.mHighestZoneBeat >= 6);
				this.mDrawMoustache = (this.mApp.mUserProfile.mIronFrogStats.mBestTime > 0);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						if (this.mApp.mUserProfile.mChallengeUnlockState[i, j] == 4)
						{
							num++;
						}
						else if (this.mApp.mUserProfile.mChallengeUnlockState[i, j] == 5)
						{
							num++;
							num2++;
						}
					}
				}
				this.mDrawTuxedo = (num == 70);
				this.mDrawFro = (num2 == 70);
			}
			ButtonWidget button = this.GetButton(17);
			if (button != null)
			{
				if (this.ShouldShowUpsellBtn())
				{
					button.mVisible = true;
					button.mDisabled = false;
				}
				else
				{
					button.mVisible = false;
					button.mDisabled = true;
				}
			}
			if (this.mChallengeMenu != null)
			{
				this.mChallengeMenu.RehupChallengeButtons();
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000436A0 File Offset: 0x000418A0
		public ButtonWidget GetButton(int id)
		{
			for (int i = 0; i < this.mButtons.Count; i++)
			{
				if (this.mButtons[i].mId == id)
				{
					return this.mButtons[i];
				}
			}
			return null;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000436E5 File Offset: 0x000418E5
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mApp.mBambooTransition != null && this.mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			this.mTip = null;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00043710 File Offset: 0x00041910
		public void RemoveUpsellButton()
		{
			if (this.mUpsellBtn == null)
			{
				return;
			}
			for (int i = 0; i < this.mButtons.Count; i++)
			{
				if (this.mButtons[i].mId == 17)
				{
					this.mButtons.Remove(this.mButtons[i]);
					i--;
				}
			}
			this.RemoveWidget(this.mUpsellBtn);
			this.mApp.SafeDeleteWidget(this.mUpsellBtn);
			this.mUpsellBtn = null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00043791 File Offset: 0x00041991
		public void DoChangeUserDialog()
		{
			ZumaUserSelDlg zumaUserSelDlg = this.mUserSelDlg;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0004379A File Offset: 0x0004199A
		public void RehupUserList()
		{
			this.RehupButtons();
			if (this.mUserSelDlg != null)
			{
				ZumaProfileMgr mProfileMgr = this.mApp.mProfileMgr;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000437B8 File Offset: 0x000419B8
		public override void Update()
		{
			if (this.mApp.mMapScreen != null && !this.mApp.mMapScreen.mDirty)
			{
				return;
			}
			if (this.mApp.IsHardwareBackButtonPressed())
			{
				this.ProcessHardwareBackButton();
			}
			if (this.mApp.mCredits != null && MathUtils._geq(this.mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mDelayedIFStartState > 0)
			{
				if (this.mDelayedIFStartState == 2)
				{
					ButtonWidget buttonWidget = null;
					for (int i = 0; i < this.mButtons.Count; i++)
					{
						if (this.mButtons[i].mId == 15)
						{
							buttonWidget = this.mButtons[i];
							break;
						}
					}
					this.mApp.mIFLoadingAnimStartCel = ((ExtraSexyButton)buttonWidget).mDownAnimation.GetFrame();
					this.mApp.StartIronFrogMode();
				}
				return;
			}
			this.mUpdateCnt++;
			float num = Common._M(10f);
			if (this.mApp.ShowingLoadingScreen() || (this.mApp.mUserProfile != null && this.mApp.mUserProfile.mNewChallengeCupUnlocked) || this.mFirstTimeAlpha > 0)
			{
				this.MarkDirty();
			}
			if (this.mFirstTimeAlpha > 0 && this.mFirstTimeAlpha < 255)
			{
				this.mFirstTimeAlpha += Common._M(3);
				if (this.mFirstTimeAlpha >= 255)
				{
					this.mFirstTimeAlpha = 255;
					this.mApp.StartAdvModeFirstTime();
				}
			}
			if (this.mHeroicSparkle != null)
			{
				this.mHeroicSparkle.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mHeroicSparkle.mDrawTransform.Scale(num2, num2);
				this.mHeroicSparkle.mDrawTransform.Translate((float)(this.mPts[5].mX + Common._DS(Common._M(180))), (float)(this.mPts[5].mY - Common._DS(Common._M1(-60))));
				this.mHeroicSparkle.Update();
				if (this.mHeroicSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mHeroicSparkle.mFrameNum > 2f)
				{
					this.mHeroicSparkle = null;
				}
			}
			if (this.mIFSparkle != null)
			{
				this.mIFSparkle.mDrawTransform.LoadIdentity();
				float num3 = GameApp.DownScaleNum(1f);
				this.mIFSparkle.mDrawTransform.Scale(num3, num3);
				this.mIFSparkle.mDrawTransform.Scale(Common._M(3f), Common._M1(2f));
				this.mIFSparkle.mDrawTransform.Translate((float)Common._DS(Common._M(-30)), (float)Common._DS(Common._M1(900)));
				this.mIFSparkle.Update();
				if (this.mIFSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mIFSparkle.mFrameNum > 2f)
				{
					this.mIFSparkle = null;
				}
			}
			if (this.mChallengeSparkle != null)
			{
				this.mChallengeSparkle.mDrawTransform.LoadIdentity();
				float num4 = GameApp.DownScaleNum(1f);
				this.mChallengeSparkle.mDrawTransform.Scale(num4, num4);
				this.mChallengeSparkle.mDrawTransform.Translate((float)(this.mPts[1].mX + Common._DS(Common._M(180)) + Common._S(960)), (float)(this.mPts[1].mY - Common._DS(Common._M1(-60))));
				this.mChallengeSparkle.Update();
				if (this.mChallengeSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mChallengeSparkle.mFrameNum > 2f)
				{
					this.mChallengeSparkle = null;
				}
			}
			if (this.mIFUnlockAnim != null)
			{
				this.mIFUnlockAnim.Update();
				if (this.mIFUnlockAnim.mMainSpriteInst.mFrameNum >= (float)(this.mIFUnlockAnim.mMainSpriteInst.mDef.mFrames.Count - 1))
				{
					this.mIFUnlockAnim = null;
					this.RehupButtons();
				}
			}
			if (this.mUpdateCnt >= Common._M(50))
			{
				if (MainMenu.gNeedsIFUnlockSound)
				{
					MainMenu.gNeedsIFUnlockSound = false;
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_IRON_FROG_UNLOCKED));
				}
				if (MainMenu.gNeedsOtherModeUnlockSound)
				{
					MainMenu.gNeedsOtherModeUnlockSound = false;
				}
			}
			if (this.mApp.Is3DAccelerated())
			{
				if (this.mApp.mHasFocus)
				{
					this.MarkDirty();
				}
				if (this.mIncLavaAlpha)
				{
					this.mLavaAlpha += Common._M(1.4f);
					if (this.mLavaAlpha >= 255f)
					{
						this.mLavaAlpha = 255f;
						this.mIncLavaAlpha = false;
					}
				}
				else
				{
					this.mLavaAlpha -= Common._M(1.4f);
					if (this.mLavaAlpha <= 0f)
					{
						this.mLavaAlpha = 0f;
						this.mIncLavaAlpha = true;
					}
				}
			}
			if (this.mMenuScrollPct.IsDoingCurve())
			{
				float num5 = (float)this.mMenuScrollPct.GetOutVal();
				float num6 = num5 * (float)(this.mMenuScrollDestY - this.mMenuScrollStartY);
				this.mMainMenuButtonsScrollWidget.Resize(this.mMainMenuButtonsScrollWidget.mX, (int)((float)this.mMenuScrollStartY + num6), this.mMainMenuButtonsScrollWidget.mWidth, this.mMainMenuButtonsScrollWidget.mHeight);
				float num7 = num5 * (float)(this.mMenuTikiDestX - this.mMenuTikiStartX);
				this.mMenuTikiX = (int)((float)this.mMenuTikiStartX + num7);
				float num8 = num5 * (float)(this.mMenuFrogDestX - this.mMenuFrogStartX);
				this.mMenuFrogX = (int)((float)this.mMenuFrogStartX + num8);
				float num9 = num5 * (float)(this.mMenuTikiDudeDestX - this.mMenuTikiDudeStartX);
				this.mMenuTikiDudeX = (int)((float)this.mMenuTikiDudeStartX + num9);
				if (this.mMainMenuOverlayWidget != null)
				{
					this.mMainMenuOverlayWidget.UpdateOverlaySlide(num5);
				}
			}
			if (this.mApp.mMoreGames != null && this.mApp.mMoreGames.IsReadyForDelete())
			{
				this.mApp.DeleteMoreGames(false);
			}
			if (this.mState == MainMenu_State.State_MainMenu && this.mApp.Is3DAccelerated() && this.mApp.mHasFocus)
			{
				this.MarkDirty();
			}
			for (int j = 0; j < this.mText.Count; j++)
			{
				MainMenu.MMText mmtext = this.mText[j];
				if (mmtext.mFadingIn && mmtext.mAlpha < 255f)
				{
					this.MarkDirty();
					mmtext.mAlpha += num;
					if (mmtext.mAlpha >= 255f)
					{
						mmtext.mAlpha = 255f;
					}
				}
				else if (!mmtext.mFadingIn)
				{
					this.MarkDirty();
					mmtext.mAlpha -= num;
					if (mmtext.mAlpha <= 0f)
					{
						this.mText.Remove(mmtext);
						j--;
					}
				}
			}
			if (this.mVolcanoSmoke != null)
			{
				this.mVolcanoSmoke.mDrawTransform.LoadIdentity();
				this.mVolcanoSmoke.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
				this.mVolcanoSmoke.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(1440))) + this.mLavaSmokeXOff, (float)(Common._S(this.mY) + Common._DS(Common._M1(115))));
				this.mVolcanoSmoke.Update();
			}
			if (this.mTikiTeethSparkle != null)
			{
				this.mTikiTeethSparkle.mDrawTransform.LoadIdentity();
				this.mTikiTeethSparkle.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
				this.mTikiTeethSparkle.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(165))) + this.mTeethSparkleXOff, (float)(Common._S(this.mY) + Common._DS(Common._M1(269))));
				this.mTikiTeethSparkle.Update();
				if (Common.Rand(2000) == 0 && this.mTikiTeethSparkle.mCurNumParticles == 0 && this.mTikiTeethSparkle.mFrameNum >= (float)this.mTikiTeethSparkle.mLastFrameNum)
				{
					this.mTikiTeethSparkle.ResetAnim();
					this.mTikiTeethSparkle.mRandSeeds.Clear();
					this.mTikiTeethSparkle.mRandSeeds.Add(Common.Rand(1000));
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00044030 File Offset: 0x00042230
		public MainMenu.MMText AddText(string txt)
		{
			if (txt.Length <= 0)
			{
				return null;
			}
			MainMenu.MMText mmtext = new MainMenu.MMText();
			mmtext.mAlpha = 0f;
			mmtext.mFadingIn = true;
			mmtext.mText = txt;
			this.mText.Insert(0, mmtext);
			this.FadeOutText(1);
			return this.mText[0];
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00044088 File Offset: 0x00042288
		public void FadeOutText(int start)
		{
			for (int i = start; i < this.mText.Count; i++)
			{
				this.mText[i].mFadingIn = false;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000440C0 File Offset: 0x000422C0
		public void DrawTalkingBubble(SexyGraphics g, int x, int y, int width, int height)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, 179));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TL);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TOP);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TAIL);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_BOT);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_BL);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_SIDE);
			g.DrawImage(imageByID, x, y);
			int i = x + imageByID.GetWidth();
			int num = x + width - imageByID.GetWidth();
			g.ClearClipRect();
			g.SetClipRect(i, y, num - i, imageByID2.GetHeight());
			while (i < num)
			{
				g.DrawImage(imageByID2, i, y);
				i += imageByID2.GetWidth();
			}
			g.ClearClipRect();
			g.DrawImageMirror(imageByID, num, y);
			g.DrawImage(imageByID3, x - imageByID3.GetWidth() + Common._DS(MainMenu.tailXOff), y + imageByID.GetHeight());
			int j = y + imageByID.GetHeight() + imageByID3.GetHeight();
			int num2 = y + height - imageByID4.GetHeight();
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID.GetHeight(), width, y + height - imageByID5.GetHeight() - (y + imageByID.GetHeight()));
			while (j < num2)
			{
				g.DrawImage(imageByID6, x, j);
				j += imageByID6.GetHeight();
			}
			for (j = y + imageByID.GetHeight(); j < num2; j += imageByID6.GetHeight())
			{
				g.DrawImageMirror(imageByID6, x + width - imageByID6.GetWidth(), j);
			}
			g.ClearClipRect();
			g.DrawImage(imageByID5, x, num2);
			i = x + imageByID5.GetWidth();
			num = x + width - imageByID5.GetWidth();
			g.ClearClipRect();
			g.SetClipRect(i, num2, num - i, imageByID4.GetHeight());
			while (i < num)
			{
				g.DrawImage(imageByID4, i, num2);
				i += imageByID4.GetWidth();
			}
			g.ClearClipRect();
			g.DrawImageMirror(imageByID5, num, num2);
			g.SetColorizeImages(false);
			g.SetColor(new Color(255, 255, 255, 179));
			int num3 = x + imageByID6.GetWidth();
			int num4 = y + imageByID.GetHeight();
			int num5 = x + width - imageByID6.GetWidth() - num3;
			int num6 = y + height - imageByID5.GetHeight() - num4;
			g.FillRect(num3, num4, num5, num6);
			g.FillRect(x + Common._DS(MainMenu.tailXOff), num4, imageByID6.GetWidth() - Common._DS(MainMenu.tailXOff), imageByID3.GetHeight());
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0004436C File Offset: 0x0004256C
		public void LoadTalkingBubbleText()
		{
			this.mTalkingBubbleTextOptions.Capacity = 45;
			for (int i = 614; i <= 658; i++)
			{
				this.mTalkingBubbleTextOptions.Add(TextManager.getInstance().getString(i));
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000443B0 File Offset: 0x000425B0
		public override void Draw(SexyGraphics g)
		{
			if (this.mApp.mCredits != null && MathUtils._geq(this.mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mChallengeMenu != null)
			{
				BambooTransition mBambooTransition = this.mApp.mBambooTransition;
				return;
			}
			if (this.mTikiTemple != null)
			{
				return;
			}
			if (this.mApp != null && this.mApp.mMapScreen != null)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BG);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_LAVA);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_GUY);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_RIGHTENDPIECE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ENDCAP);
			if (this.mDelayedIFStartState == 1)
			{
				this.mDelayedIFStartState = 2;
			}
			float num = (float)imageByID.GetWidth() / (float)this.mApp.GetScreenRect().mWidth;
			float num2 = (float)imageByID.GetHeight() / (float)this.mApp.GetScreenRect().mHeight;
			g.DrawImage(imageByID, 0, 0, this.mApp.GetScreenRect().mWidth, this.mApp.GetScreenRect().mHeight);
			g.SetDrawMode(1);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)this.mLavaAlpha));
			g.DrawImage(imageByID2, (int)((float)(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_LAVA)) - this.mApp.mWideScreenXOffset + this.mApp.GetScreenRect().mX) + this.mLavaXOff), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_LAVA)), (int)((float)imageByID2.GetWidth() * this.mLavaXScale), imageByID2.GetHeight());
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
			g.DrawImage(imageByID3, this.mMenuTikiDudeX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_GUY)));
			if (this.mMainMenuButtonsWidget != null && this.mMainMenuButtonsWidget.mVisible)
			{
				if (this.mMainMenuButtonsWidget.mX >= 0)
				{
					g.DrawImageMirror(imageByID4, this.mMainMenuButtonsWidget.mX - imageByID4.GetWidth(), this.mMainMenuButtonsScrollWidget.mY);
					g.DrawImageMirror(imageByID5, this.mMainMenuButtonsWidget.mX - imageByID4.GetWidth(), this.mMainMenuButtonsScrollWidget.mY - Common._S(42));
					g.DrawImage(this.IMAGE_UI_MAINMENU_TIKI, this.mMainMenuButtonsWidget.mX - this.IMAGE_UI_MAINMENU_TIKI.GetWidth(), this.mMainMenuButtonsScrollWidget.mY);
				}
				else if (this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth <= this.mApp.GetScreenWidth())
				{
					g.DrawImage(imageByID4, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsScrollWidget.mY);
					g.DrawImage(imageByID5, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth + imageByID4.GetWidth() + this.mApp.GetScreenRect().mX - imageByID5.GetWidth(), this.mMainMenuButtonsScrollWidget.mY - Common._S(42));
					g.DrawImageMirror(this.IMAGE_UI_MAINMENU_TIKI, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsScrollWidget.mY);
				}
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK);
			g.SetFont(fontByID);
			g.SetColor(Color.White);
			if (GameApp.USE_XBOX_SERVICE)
			{
				string text;
				if (this.mApp.mUserProfile != null)
				{
					StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(660));
					stringBuilder.Replace("$1", " " + this.mApp.mUserProfile.GetName());
					text = stringBuilder.ToString();
				}
				else
				{
					text = TextManager.getInstance().getString(659);
				}
				g.WriteString(text, 0, Common._S(Common._M(30)), this.mWidth);
				g.SetFont(fontByID2);
				this.DrawChangeProfileString(g);
			}
			if (this.mState == MainMenu_State.State_Scroll || this.mState == MainMenu_State.State_MainMenu)
			{
				Common._S(Common._M(375));
				Common._S(Common._M(75));
				Common._S(Common._M(1));
				this.DrawTikiTalk(g);
				this.mEffectBatch.DrawBatch(g);
			}
			if (this.mTip != null)
			{
				this.mTip.Draw(g);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00044824 File Offset: 0x00042A24
		public void DrawTikiTalk(SexyGraphics g)
		{
			if (this.mApp.mMoreGames != null || this.mText.Count == 0)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_BASE);
			string text = this.mText[0].mText;
			int num = Common._DS(800);
			int num2 = Common._DS(100);
			int num3 = (int)((float)(this.mApp.GetScreenRect().mWidth - num) * 0.5f);
			int num4 = Common._DS(75);
			int num5 = Common._DS(15);
			int num6 = num - num5 * 2;
			int num7 = num2 - num5 * 2;
			int num8 = Common._GetWordWrappedHeight(text, fontByID, num6);
			if (num8 > num7)
			{
				int num9 = num8 - num7;
				num7 += num9;
				num2 += num9;
			}
			Rect rect;
			rect = new Rect(num3 + num5, num4 + num5, num6, num7);
			rect.mY += (int)((float)(num7 - num8) * 0.5f);
			this.DrawTalkingBubble(g, num3, num4, num, num2 + 10);
			g.SetFont(fontByID);
			g.SetColor(Color.Black);
			g.WriteWordWrapped(rect, text, -1, 0);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00044939 File Offset: 0x00042B39
		public void DrawChangeProfileString(SexyGraphics g)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0004493B File Offset: 0x00042B3B
		public void SelectUser(string user_name)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0004493D File Offset: 0x00042B3D
		public void HideChallengeMenu()
		{
			this.RemoveWidget(this.mChallengeMenu);
			this.mApp.SafeDeleteWidget(this.mChallengeMenu);
			this.mChallengeMenu = null;
			this.mState = MainMenu_State.State_MainMenu;
			this.ShowScrollButtons();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00044970 File Offset: 0x00042B70
		public void ShowTikiTemple()
		{
			this.mTikiTemple = new TikiTemple();
			this.mTikiTemple.Resize(this.mApp.GetScreenRect());
			this.mTikiTemple.Init();
			this.mWidgetManager.AddWidget(this.mTikiTemple);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_TikiTemple;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000449D8 File Offset: 0x00042BD8
		public void ShowAchievements()
		{
			this.mAchievements = new Achievements();
			this.mAchievements.Resize(this.mApp.GetScreenRect());
			this.mAchievements.Init();
			this.mWidgetManager.AddWidget(this.mAchievements);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_Achievement;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00044A40 File Offset: 0x00042C40
		public void ShowLeaderBoards()
		{
			this.mLeaderBoards = new LeaderBoards();
			this.mLeaderBoards.Resize(this.mApp.GetScreenRect());
			this.mLeaderBoards.Init();
			this.mWidgetManager.AddWidget(this.mLeaderBoards);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_LeaderBoards;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00044AA8 File Offset: 0x00042CA8
		public void HideTikiTemple()
		{
			this.mWidgetManager.RemoveWidget(this.mTikiTemple);
			this.mApp.SafeDeleteWidget(this.mTikiTemple);
			this.mTikiTemple = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00044AFC File Offset: 0x00042CFC
		public void HideLeaderBoards()
		{
			this.mWidgetManager.RemoveWidget(this.mLeaderBoards);
			this.mApp.SafeDeleteWidget(this.mLeaderBoards);
			this.mLeaderBoards = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00044B50 File Offset: 0x00042D50
		public void HideAchievements()
		{
			this.mWidgetManager.RemoveWidget(this.mAchievements);
			this.RemoveWidget(this.mAchievements);
			this.mApp.SafeDeleteWidget(this.mAchievements);
			this.mAchievements = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public void ShowChallengeMenuFromMainMenu()
		{
			this.mApp.LoadAllThumbnails();
			this.mChallengeMenu = new ChallengeMenu(this.mApp, this, true);
			this.mChallengeMenu.Resize(this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mY, this.mApp.GetScreenRect().mWidth - this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mHeight - this.mApp.GetScreenRect().mY);
			this.mChallengeMenu.Init();
			this.AddWidget(this.mChallengeMenu);
			this.mChallengeMenu.InitCS();
			this.RehupButtons();
			this.mChallengeMenu.mCSVisFrame = this.mUpdateCnt;
			this.mState = MainMenu_State.State_CS;
			this.HideScrollButtons();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00044C90 File Offset: 0x00042E90
		public void ShowChallengeMenu()
		{
			this.mChallengeMenu = new ChallengeMenu(this.mApp, this, false);
			this.mChallengeMenu.Resize(this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mY, this.mApp.GetScreenRect().mWidth - this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mHeight - this.mApp.GetScreenRect().mY);
			this.mChallengeMenu.Init();
			this.AddWidget(this.mChallengeMenu);
			this.mChallengeMenu.InitCS();
			this.RehupButtons();
			this.mChallengeMenu.mCSVisFrame = this.mUpdateCnt;
			this.mState = MainMenu_State.State_CS;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00044D5D File Offset: 0x00042F5D
		public void HideScrollButtons()
		{
			this.mMainMenuButtonsWidget.HideScrollButtons();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00044D6A File Offset: 0x00042F6A
		public void ShowScrollButtons()
		{
			this.mMainMenuButtonsWidget.ShowScrollButtons();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00044D77 File Offset: 0x00042F77
		public bool ShowingTikiTemple()
		{
			return this.mTikiTemple != null;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00044D88 File Offset: 0x00042F88
		public void ProcessHardwareBackButton()
		{
			if (this.mApp.mMapScreen != null)
			{
				return;
			}
			Dialog dialog = this.mApp.GetDialog(2);
			if (dialog != null)
			{
				(dialog as OptionsDialog).ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mAboutInfo != null)
			{
				GameApp.gApp.mAboutInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mLegalInfo != null)
			{
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mLegalInfo != null)
			{
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			switch (this.mState)
			{
			case MainMenu_State.State_CS:
				if (this.mChallengeMenu.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_TikiTemple:
				this.mState = MainMenu_State.State_MainMenu;
				this.mTikiTemple.ProcessHardwareBackButton();
				return;
			case MainMenu_State.State_LeaderBoards:
				if (this.mLeaderBoards.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_Achievement:
				if (this.mAchievements.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_MapScreen:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			case MainMenu_State.State_UnlockPrompt:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.GetDialog(0).ButtonDepress(1000);
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			case MainMenu_State.State_QuitPrompt:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.GetDialog(1).ButtonDepress(1001);
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (GameApp.gApp.mGenericHelp != null)
			{
				this.mState = MainMenu_State.State_MainMenu;
				GameApp.gApp.mGenericHelp.ForceCloseDialog();
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			this.mState = MainMenu_State.State_QuitPrompt;
			this.mApp.DoQuitPromptDialog();
			this.mApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
			this.mApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00044F98 File Offset: 0x00043198
		public void ProcessYesNo(int theId)
		{
			this.mState = MainMenu_State.State_MainMenu;
			if (theId == 1000)
			{
				if (!this.mApp.IsRegistered() && this.mApp.mTrialType == 1 && this.mApp.GetBoolean("UpsellExit", false))
				{
					this.mApp.DoUpsell(true);
				}
				else
				{
					this.mApp.SaveProfile();
				}
				this.mApp.Shutdown();
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00045006 File Offset: 0x00043206
		public void StartChallengeGame()
		{
			this.mCSOverRect = default(Rect);
			this.mApp.StartGauntletMode(this.mGauntletModLevel_id, this.mCSOverRect);
		}

		// Token: 0x04000C43 RID: 3139
		private static int MAX_VOLCANO_PROJECTILES = 6;

		// Token: 0x04000C44 RID: 3140
		private static bool gNeedsIFUnlockSound = false;

		// Token: 0x04000C45 RID: 3141
		private static bool gNeedsOtherModeUnlockSound = false;

		// Token: 0x04000C46 RID: 3142
		public static int gScreenShake = 0;

		// Token: 0x04000C47 RID: 3143
		public static int gScreenShakeTimer = 0;

		// Token: 0x04000C48 RID: 3144
		public float mAdjust;

		// Token: 0x04000C49 RID: 3145
		private Point[] mPts = new Point[]
		{
			new Point((int)Common._DSA(1100f, 0f), Common._S(Common._M(253))),
			new Point((int)Common._DSA(1102f, 0f), Common._S(Common._M1(315))),
			new Point(Common._DS(Common._M2(0)), Common._S(Common._M3(376))),
			new Point((int)Common._DSA(1100f, 0f), Common._S(Common._M4(490))),
			new Point((int)Common._DSA(1288f, 0f), Common._S(Common._M5(490))),
			new Point((int)Common._DSA(1100f, 0f), Common._S(Common._M6(430)))
		};

		// Token: 0x04000C4A RID: 3146
		public GameApp mApp;

		// Token: 0x04000C4B RID: 3147
		public ZumaTip mTip;

		// Token: 0x04000C4C RID: 3148
		public List<MainMenu.MMText> mText = new List<MainMenu.MMText>();

		// Token: 0x04000C4D RID: 3149
		public PIEffect mHeroicSparkle;

		// Token: 0x04000C4E RID: 3150
		public PIEffect mIFSparkle;

		// Token: 0x04000C4F RID: 3151
		public PIEffect mChallengeSparkle;

		// Token: 0x04000C50 RID: 3152
		public PIEffectBatch mEffectBatch;

		// Token: 0x04000C51 RID: 3153
		public PopAnim mIFUnlockAnim;

		// Token: 0x04000C52 RID: 3154
		public float mDistance;

		// Token: 0x04000C53 RID: 3155
		public float mAddAcc;

		// Token: 0x04000C54 RID: 3156
		public float mLavaAlpha;

		// Token: 0x04000C55 RID: 3157
		public MainMenu_State mState;

		// Token: 0x04000C56 RID: 3158
		public int mDelayedIFStartState;

		// Token: 0x04000C57 RID: 3159
		public int mFirstTimeAlpha;

		// Token: 0x04000C58 RID: 3160
		public bool mIncLavaAlpha;

		// Token: 0x04000C59 RID: 3161
		public bool mDrawHat;

		// Token: 0x04000C5A RID: 3162
		public bool mDrawMoustache;

		// Token: 0x04000C5B RID: 3163
		public bool mDrawTuxedo;

		// Token: 0x04000C5C RID: 3164
		public bool mDrawFro;

		// Token: 0x04000C5D RID: 3165
		public bool mSkipEnterSound;

		// Token: 0x04000C5E RID: 3166
		public Achievements mAchievements;

		// Token: 0x04000C5F RID: 3167
		public ChallengeMenu mChallengeMenu;

		// Token: 0x04000C60 RID: 3168
		public TikiTemple mTikiTemple;

		// Token: 0x04000C61 RID: 3169
		public LeaderBoards mLeaderBoards;

		// Token: 0x04000C62 RID: 3170
		public ZumaUserSelDlg mUserSelDlg;

		// Token: 0x04000C63 RID: 3171
		private List<ButtonWidget> mButtons = new List<ButtonWidget>();

		// Token: 0x04000C64 RID: 3172
		private ButtonWidget mUpsellBtn;

		// Token: 0x04000C65 RID: 3173
		private ButtonWidget mChangeProfileBtn;

		// Token: 0x04000C66 RID: 3174
		public ScrollWidget mMainMenuButtonsScrollWidget;

		// Token: 0x04000C67 RID: 3175
		public MainMenuButtonsWidget mMainMenuButtonsWidget;

		// Token: 0x04000C68 RID: 3176
		public ButtonWidget mMoreGamesButton;

		// Token: 0x04000C69 RID: 3177
		public ButtonWidget mOptionsButton;

		// Token: 0x04000C6A RID: 3178
		public ButtonWidget mUnlockButton;

		// Token: 0x04000C6B RID: 3179
		public int mMenuScrollOriginY;

		// Token: 0x04000C6C RID: 3180
		public int mMenuScrollDestY;

		// Token: 0x04000C6D RID: 3181
		public int mMenuScrollStartY;

		// Token: 0x04000C6E RID: 3182
		public int mMenuTikiStartX;

		// Token: 0x04000C6F RID: 3183
		public int mMenuTikiOriginX;

		// Token: 0x04000C70 RID: 3184
		public int mMenuTikiDestX;

		// Token: 0x04000C71 RID: 3185
		public int mMenuTikiX;

		// Token: 0x04000C72 RID: 3186
		public int mMenuFrogStartX;

		// Token: 0x04000C73 RID: 3187
		public int mMenuFrogOriginX;

		// Token: 0x04000C74 RID: 3188
		public int mMenuFrogDestX;

		// Token: 0x04000C75 RID: 3189
		public int mMenuFrogX;

		// Token: 0x04000C76 RID: 3190
		public int mMenuTikiDudeStartX;

		// Token: 0x04000C77 RID: 3191
		public int mMenuTikiDudeOriginX;

		// Token: 0x04000C78 RID: 3192
		public int mMenuTikiDudeDestX;

		// Token: 0x04000C79 RID: 3193
		public int mMenuTikiDudeX;

		// Token: 0x04000C7A RID: 3194
		public ButtonWidget mMonkeyButton;

		// Token: 0x04000C7B RID: 3195
		public ButtonWidget mLogButton;

		// Token: 0x04000C7C RID: 3196
		private MainMenuOverlayWidget mMainMenuOverlayWidget;

		// Token: 0x04000C7D RID: 3197
		private CurvedVal mMenuScrollPct = new CurvedVal();

		// Token: 0x04000C7E RID: 3198
		private List<string> mTalkingBubbleTextOptions = new List<string>();

		// Token: 0x04000C7F RID: 3199
		private MainMenu.VolcanoProjectile[] mVolcanoProjectiles;

		// Token: 0x04000C80 RID: 3200
		private PIEffect mVolcanoSmoke;

		// Token: 0x04000C81 RID: 3201
		private PIEffect mTikiTeethSparkle;

		// Token: 0x04000C82 RID: 3202
		private float mLavaXOff;

		// Token: 0x04000C83 RID: 3203
		private float mLavaXScale;

		// Token: 0x04000C84 RID: 3204
		private float mLavaProjectileXOff;

		// Token: 0x04000C85 RID: 3205
		private float mLavaSmokeXOff;

		// Token: 0x04000C86 RID: 3206
		private float mTeethSparkleXOff;

		// Token: 0x04000C87 RID: 3207
		private Image IMAGE_UI_MAINMENU_TIKI;

		// Token: 0x04000C88 RID: 3208
		public Rect mCSOverRect;

		// Token: 0x04000C89 RID: 3209
		public string mGauntletModLevel_id;

		// Token: 0x04000C8A RID: 3210
		private static int tailXOff = 5;

		// Token: 0x02000052 RID: 82
		public class VolcanoProjectile
		{
			// Token: 0x04001197 RID: 4503
			public PIEffect mProjectile;

			// Token: 0x04001198 RID: 4504
			public bool mInUse;
		}

		// Token: 0x02000053 RID: 83
		public class MMText
		{
			// Token: 0x04001199 RID: 4505
			public float mAlpha;

			// Token: 0x0400119A RID: 4506
			public string mText;

			// Token: 0x0400119B RID: 4507
			public string mExtraText;

			// Token: 0x0400119C RID: 4508
			public bool mFadingIn;

			// Token: 0x0400119D RID: 4509
			public bool mShowChallengeCrowns;

			// Token: 0x0400119E RID: 4510
			public int mYOff;
		}
	}
}
