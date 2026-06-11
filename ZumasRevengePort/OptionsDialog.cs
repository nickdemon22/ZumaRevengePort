using System;
using System.Text;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000038 RID: 56
	internal class OptionsDialog : ZumaDialog, SliderListener
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x0004A71C File Offset: 0x0004891C
		public OptionsDialog(bool inGame) : base(2, true, "", "", "", 0)
		{
			this.mLanguageButton = null;
			this.mInGame = inGame;
			this.mMusicEnabled = false;
			this.mMusicSliderOn = false;
			this.mHeightPad = Common._S(Common._M(360));
			this.mState = OptionsDialog.OptionState.OptionState_None;
			this.mAllowDrag = false;
			this.mClip = false;
			this.LoadResources();
			this.InitMusicSlider();
			this.InitSfxSlider();
			this.InitColorblindSlider();
			this.InitDisplaySettings();
			this.InitButtons();
			this.InitSize();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0004A7AC File Offset: 0x000489AC
		~OptionsDialog()
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0004A7D4 File Offset: 0x000489D4
		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (this.mInGame)
			{
				this.LayoutAdventureDialog();
				return;
			}
			this.LayoutMainMenuDialog();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0004A7F8 File Offset: 0x000489F8
		public override void Update()
		{
			base.Update();
			if (this.mMusicVolumeSlider.mDisabled && !GameApp.gApp.mMusicInterface.m_isUserMusicOn)
			{
				this.mMusicVolumeSlider.mDisabled = false;
				this.mMusicEnabled = true;
				double musicVolume = GameApp.gApp.GetMusicVolume();
				this.mOriginMusicVolume = musicVolume;
				this.mMusicVolumeSlider.SetValue(musicVolume);
			}
			else if (!this.mMusicVolumeSlider.mDisabled && GameApp.gApp.mMusicInterface.m_isUserMusicOn)
			{
				this.mMusicVolumeSlider.mDisabled = true;
				this.mMusicEnabled = false;
				this.mMusicVolumeSlider.SetValue(0.0);
			}
			if (this.mState == OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt)
			{
				this.SetVisible(false);
				return;
			}
			this.SetVisible(true);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public override void Draw(SexyGraphics g)
		{
			if (GameApp.gApp.mCredits != null)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_STROKE);
			Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			Font fontByID3 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_GAUNTLET);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_CROWN);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_LARGE_ACECROWN);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CROWN_HOLE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_ADVENTURE);
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.Draw(g);
			if (this.mInGame)
			{
				g.SetFont(fontByID);
				g.SetColor(255, 255, 255);
				Board board = GameApp.gApp.GetBoard();
				if (GameApp.gApp.GetBoard().GauntletMode())
				{
					g.SetFont(fontByID2);
					g.SetColorizeImages(true);
					g.SetColor(Color.White);
					int num = Common._S(100);
					int num2 = Common._S(120);
					if ((int)Localization.GetCurrentLanguage() == 4 || (int)Localization.GetCurrentLanguage() == 9)
					{
						num -= 35;
					}
					string @string = TextManager.getInstance().getString(669);
					float num3 = (float)fontByID2.StringWidth(@string);
					g.DrawString(@string, num, num2);
					string text = Common.CommaSeperate(board.mScore);
					fontByID2.StringWidth(text);
					float num4 = (float)num + num3 + (float)Common._DS(20);
					g.DrawString(text, (int)num4, num2);
					float num5 = (float)Common._S(190);
					float num6 = (float)Common._S(150);
					float num7 = (float)Common._DS(15);
					float num8 = (float)Common._DS(10);
					float num9 = 0.5f;
					float num10 = (float)imageByID.GetWidth() * num9;
					float num11 = (float)imageByID.GetHeight() * num9;
					float num12 = (float)imageByID.GetWidth() * num9;
					imageByID.GetHeight();
					g.DrawImage(imageByID2, Common._S(40), Common._S(129));
					g.DrawImage(imageByID, (int)num5, (int)num6, (int)num10, (int)num11);
					string text2 = Common.UCommaSeparate((uint)board.mLevel.mChallengePoints);
					g.DrawString(text2, (int)(num5 + num10 + num8), (int)(num6 + (float)fontByID2.mAscent));
					g.DrawImage(imageByID3, (int)num5, (int)(num6 + num11 + num7), (int)num12, (int)num11);
					string text3 = Common.UCommaSeparate((uint)board.mLevel.mChallengeAcePoints);
					g.DrawString(text3, (int)(num5 + num12 + num8), (int)(num6 + num11 + num7 + (float)fontByID2.mAscent));
					string text4 = Common.UpdateToTimeStr(board.mLevel.mGauntletCurTime);
					string text5 = Common.UpdateToTimeStr(((GameApp)GlobalMembers.gSexyApp).GetLevelMgr().mGauntletSessionLength);
					string text6 = string.Format(" {0} / {1}", text4, text5);
					g.DrawString(TextManager.getInstance().getString(679) + text6, Common._S(45), Common._S(310));
					if (GameApp.gApp.mUserProfile != null && GameApp.gApp.mBoard != null && GameApp.gApp.mBoard.mLevel != null)
					{
						float num13 = (float)Common._S(60);
						float num14 = (float)Common._S(132);
						string text7 = "";
						Image image;
						if (board.mScore < board.mLevel.mChallengePoints)
						{
							image = imageByID4;
							text7 = TextManager.getInstance().getString(681);
						}
						else if (board.mScore < board.mLevel.mChallengeAcePoints)
						{
							image = imageByID;
						}
						else
						{
							image = imageByID3;
						}
						if (image != null)
						{
							if ((int)Localization.GetCurrentLanguage() == 6)
							{
								g.DrawImage(image, (int)num13 - 24, (int)num14 - 28, (int)((double)imageByID4.GetWidth() * 1.35), (int)((double)imageByID4.GetHeight() * 1.35));
							}
							else if ((int)Localization.GetCurrentLanguage() == 4 || (int)Localization.GetCurrentLanguage() == 9 || (int)Localization.GetCurrentLanguage() == 7)
							{
								g.DrawImage(image, (int)num13 - 13, (int)num14 - 15, (int)((double)imageByID4.GetWidth() * 1.2), (int)((double)imageByID4.GetHeight() * 1.2));
							}
							else
							{
								g.DrawImage(image, (int)num13, (int)num14, imageByID4.GetWidth(), imageByID4.GetHeight());
							}
							g.SetColor(136, 156, 43, 255);
							g.SetFont(fontByID3);
							g.GetFont().StringWidth(text7);
							float num15 = num13 + (float)Common._S(7);
							float num16 = num14 + (float)Common._S(38);
							g.PushState();
							g.SetScale(0.7f, 0.7f, num15, num16);
							g.WriteWordWrapped(new Rect((int)num15 + Common._S(15), (int)num16, imageByID4.GetWidth(), imageByID4.GetHeight()), text7, -1, 0);
							g.PopState();
						}
					}
					g.SetColorizeImages(false);
					return;
				}
				g.SetFont(fontByID2);
				g.SetColorizeImages(true);
				g.SetColor(Color.White);
				g.DrawString(TextManager.getInstance().getString(670), Common._S(120), Common._S(120));
				int num17 = Common._S(80);
				int num18 = Common._S(130);
				g.DrawImage(imageByID5, num17, num18);
				int num19 = board.GetNumLives() - 1;
				if (num19 < 0)
				{
					num19 = 0;
				}
				else if (num19 > 99)
				{
					num19 = 99;
				}
				string text8 = string.Format("x {0}", num19);
				fontByID2.StringWidth(text8);
				float num20 = (float)(num17 + imageByID5.GetWidth() + Common._S(10));
				float num21 = (float)(num18 + imageByID5.GetHeight() / 2);
				g.DrawString(text8, (int)num20, (int)num21);
				if (GameApp.gApp.mBoard.mGameState != GameState.GameState_Losing)
				{
					string string2 = TextManager.getInstance().getString(679);
					float num22 = (float)fontByID2.StringWidth(string2);
					Level mLevel = board.mLevel;
					int num23 = 65;
					if (mLevel != null && mLevel.mBoss == null && mLevel.mIndex != num23)
					{
						StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(671));
						stringBuilder.Replace("$1", Common.UpdateToTimeStr(mLevel.mParTime));
						string text9 = stringBuilder.ToString();
						float num24 = (float)fontByID2.StringWidth(text9);
						g.DrawString(text9, Common._S(120) + (int)(num22 - num24) / 2, (int)num21 + Common._S(160));
					}
					string text10;
					if (board.mGameState != GameState.GameState_Playing)
					{
						text10 = Common.UpdateToTimeStr(board.mEndLevelStats.mTimePlayed);
					}
					else
					{
						text10 = Common.UpdateToTimeStr(board.mStateCount - board.mIgnoreCount);
					}
					float num25 = (float)fontByID2.StringWidth(text10);
					g.DrawString(string2, Common._S(120), (int)num21 + Common._S(80));
					g.DrawString(text10, Common._S(120) + (int)(num22 - num25) / 2, (int)num21 + Common._S(120));
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0004AFE0 File Offset: 0x000491E0
		public virtual void DrawAll(ref ModalFlags theFlags, SexyGraphics g)
		{
			g.PushState();
			g.Translate(-this.mX, -this.mY);
			g.SetColor(0, 0, 0, 130);
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			g.PopState();
			base.DrawAll(theFlags, g);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0004B054 File Offset: 0x00049254
		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.AddWidget(this.mMusicVolumeSlider);
			this.AddWidget(this.mSfxVolumeSlider);
			this.AddWidget(this.mHelpButton);
			this.AddWidget(this.mMainMenuButton);
			this.AddWidget(this.mBackToGame);
			this.AddWidget(this.mCreditsButton);
			this.AddWidget(this.mDebugButton);
			this.AddWidget(this.mColorBlindSlider);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0004B0BC File Offset: 0x000492BC
		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			this.RemoveWidget(this.mMusicVolumeSlider);
			this.RemoveWidget(this.mSfxVolumeSlider);
			this.RemoveWidget(this.mHelpButton);
			this.RemoveWidget(this.mMainMenuButton);
			this.RemoveWidget(this.mBackToGame);
			this.RemoveWidget(this.mCreditsButton);
			this.RemoveWidget(this.mDebugButton);
			this.RemoveWidget(this.mColorBlindSlider);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0004B124 File Offset: 0x00049324
		public void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 0:
				if (GameApp.gApp.mMusicInterface.isPlayingUserMusic() && theVal > 0.0)
				{
					GameApp.gApp.mMusicInterface.stopUserMusic();
				}
				this.SetMusicSlider(theVal);
				return;
			case 1:
				this.SetSfxSlider(theVal);
				return;
			default:
				return;
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0004B180 File Offset: 0x00049380
		public void ProcessYesNo(int theId)
		{
			GameApp gameApp = (GameApp)GlobalMembers.gSexyApp;
			if (theId == 1000)
			{
				gameApp.KillDialog(this);
				gameApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.DoDeferredEndGame);
				gameApp.ToggleBambooTransition();
				gameApp.mMusic.StopAll();
			}
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0004B1DC File Offset: 0x000493DC
		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			GameApp gameApp = (GameApp)GlobalMembers.gSexyApp;
			if (theId == 3)
			{
				this.mState = OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt;
				int width_pad = Common._DS(Common._M(20));
				string @string;
				if (((GameApp)GlobalMembers.gSexyApp).GetBoard().GauntletMode())
				{
					@string = TextManager.getInstance().getString(449);
				}
				else if (GameApp.gApp.GetBoard().IronFrogMode())
				{
					@string = TextManager.getInstance().getString(450);
				}
				else
				{
					@string = TextManager.getInstance().getString(451);
				}
				this.SetVisible(false);
				gameApp.DoYesNoDialog(TextManager.getInstance().getString(448), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
				gameApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
				this.SetVisible(true);
				return;
			}
			if (theId == 8)
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				this.ApplyDisplaySettings();
				GameApp.gApp.FinishOptionsDialog(true);
				return;
			}
			if (theId == 13)
			{
				GameApp.gApp.CycleDesktopResolutionPreset();
				this.UpdateResolutionButtonLabel();
				this.ApplyDisplaySettings();
				return;
			}
			if (theId == 2)
			{
				this.mState = OptionsDialog.OptionState.OptionState_Help;
				Board board = GameApp.gApp.GetBoard();
				GameApp.gApp.mColorblind = this.mColorBlindSlider.IsOn();
				if (board != null && board.GauntletMode())
				{
					board.ShowChallengeHelpScreen();
					return;
				}
				GameApp.gApp.mGenericHelp = new GenericHelp();
				GameApp.gApp.AddDialog(GameApp.gApp.mGenericHelp);
				return;
			}
			else
			{
				if (theId == 5)
				{
					this.mState = OptionsDialog.OptionState.OptionState_Credits;
					GameApp.gApp.DoCredits(true);
					return;
				}
				if (theId == 7)
				{
					this.mState = OptionsDialog.OptionState.OptionState_Legal;
					GameApp.gApp.ShowLegal();
					return;
				}
				if (theId == 10)
				{
					GameApp.gApp.ShowDebugDialog();
					return;
				}
				return;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0004B37C File Offset: 0x0004957C
		public void DetectMusicSettings()
		{
			this.mMusicEnabled = GameApp.gApp.MusicEnabled();
			double num = this.mMusicEnabled ? GameApp.gApp.GetMusicVolume() : 0.0;
			this.mOriginMusicVolume = num;
			this.mMusicVolumeSlider.SetValue(num);
			this.SetMusicSlider(num);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0004B3D1 File Offset: 0x000495D1
		private void LoadResources()
		{
			if (GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame"))
			{
				return;
			}
			if (!GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.Shutdown();
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0004B40A File Offset: 0x0004960A
		private void InitMusicSlider()
		{
			this.mMusicVolumeSlider = new ZumaSlider(0, this, TextManager.getInstance().getString(672));
			this.mMusicVolumeSlider.mFeedbackSoundID = Res.GetSoundByID(ResID.SOUND_BALLCLICK1);
			this.DetectMusicSettings();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0004B444 File Offset: 0x00049644
		private void InitSfxSlider()
		{
			this.mSfxVolumeSlider = new ZumaSlider(1, this, TextManager.getInstance().getString(673));
			this.mSfxVolumeSlider.mFeedbackSoundID = Res.GetSoundByID(ResID.SOUND_BALLCLICK1);
			this.mOriginSfxVolume = GlobalMembers.gSexyApp.GetSfxVolume();
			this.mSfxVolumeSlider.SetValue(this.mOriginSfxVolume);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0004B4A3 File Offset: 0x000496A3
		private void InitColorblindSlider()
		{
			this.mColorBlindSlider = new ZumaSlideBox(this, 4, TextManager.getInstance().getString(680));
			this.mOriginColorBlind = GameApp.gApp.mColorblind;
			this.mColorBlindSlider.SetOnOff(this.mOriginColorBlind);
		}

		private void InitDisplaySettings()
		{
			GameApp app = GameApp.gApp;
			this.mOriginShowFps = app.mShowFPS;
			this.mOriginDesktopFullscreen = app.mDesktopFullscreen;
			this.mOriginDesktopResolutionPreset = app.mDesktopResolutionPreset;
			this.mFpsToggle = new ZumaSlideBox(this, 11, "FPS");
			this.mFpsToggle.SetOnOff(app.mShowFPS);
			this.mFullscreenToggle = new ZumaSlideBox(this, 12, "Полный экран");
			this.mFullscreenToggle.SetOnOff(app.mDesktopFullscreen);
			this.mResolutionButton = this.InitButton(13, GameApp.gApp.GetDesktopResolutionLabel());
			this.AddWidget(this.mFpsToggle);
			this.AddWidget(this.mFullscreenToggle);
			this.AddWidget(this.mResolutionButton);
		}

		private void UpdateResolutionButtonLabel()
		{
			if (this.mResolutionButton != null)
			{
				this.mResolutionButton.mLabel = GameApp.gApp.GetDesktopResolutionLabel();
			}
		}

		private void ApplyDisplaySettings()
		{
			GameApp app = GameApp.gApp;
			app.mShowFPS = this.mFpsToggle.IsOn();
			app.mDesktopFullscreen = this.mFullscreenToggle.IsOn();
			app.ApplySavedDesktopDisplay();
			app.UpdateDebugOverlayText();
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0004B4E4 File Offset: 0x000496E4
		private void InitButtons()
		{
			this.mMainMenuButton = this.InitButton(3, TextManager.getInstance().getString(676));
			this.mHelpButton = this.InitButton(2, TextManager.getInstance().getString(674));
			this.mBackToGame = this.InitButton(8, TextManager.getInstance().getString(675));
			this.mCreditsButton = this.InitButton(5, TextManager.getInstance().getString(677));
			this.mDebugButton = this.InitButton(10, "DEBUG");
			this.HideButton(this.mMainMenuButton, !this.mInGame);
			this.HideButton(this.mCreditsButton, this.mInGame);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0004B588 File Offset: 0x00049788
		private void InitSize()
		{
			if (this.mInGame)
			{
				this.Resize(0, 0, Common._S(Common._M(690)), Common._S(Common._M1(230)) + this.mHeightPad);
				return;
			}
			this.Resize(0, 0, Common._S(Common._M(600)), Common._S(Common._M1(230)) + this.mHeightPad - 80);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0004B5FC File Offset: 0x000497FC
		private ButtonWidget InitButton(int inButtonID, string inButtonName)
		{
			ButtonWidget buttonWidget = Common.MakeButton(inButtonID, this, inButtonName);
			buttonWidget.mDoFinger = true;
			return buttonWidget;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0004B61A File Offset: 0x0004981A
		private void HideButton(ButtonWidget inButton, bool inHide)
		{
			inButton.SetVisible(!inHide);
			inButton.mDisabled = inHide;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0004B630 File Offset: 0x00049830
		private void LayoutMainMenuDialog()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			int num = base.GetLeft() - this.mX;
			int num2 = base.GetTop() - this.mY;
			int width = base.GetWidth();
			int num3 = width / 2;
			int num4 = Common._DS(Common._M(8));
			int num5 = num2 - Common._DS(Common._M(70));
			this.mMusicVolumeSlider.Resize(num + num4 / 2 + Common._DS(Common._M(10)), num5, num3 - Common._DS(Common._M1(24)), Common._DS(Common._M2(94)));
			this.mSfxVolumeSlider.Layout(17411, this.mMusicVolumeSlider, Common._DS(Common._M(25)), 0, 0, 0);
			this.mColorBlindSlider.Resize((this.mMusicVolumeSlider.mX + this.mSfxVolumeSlider.mX + this.mSfxVolumeSlider.mWidth) / 2 - imageByID.GetWidth() / 2, this.mMusicVolumeSlider.mY + Common._S(45), imageByID.GetWidth(), imageByID.GetHeight());
			int num6 = Common._DS(10);
			int num7 = (this.mWidth - (OptionsDialog.OPTIONS_BUTTON_WIDTH * 3 + num6)) / 2;
			this.mCreditsButton.Resize(num7, this.mColorBlindSlider.mY + Common._S(90), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mHelpButton.Resize(this.mCreditsButton.mX + this.mCreditsButton.mWidth + num6, this.mCreditsButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.HideButton(this.mHelpButton, true);
			int num8 = 200;
			this.mBackToGame.Resize(this.mHelpButton.mX + num8, this.mHelpButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mMainMenuButton.Layout(16387, this.mBackToGame, 0, 0, 0, 0);
			this.mFpsToggle.Resize(this.mCreditsButton.mX, this.mColorBlindSlider.mY + Common._S(95), imageByID.GetWidth(), imageByID.GetHeight());
			this.mFullscreenToggle.Resize(this.mSfxVolumeSlider.mX, this.mFpsToggle.mY, imageByID.GetWidth(), imageByID.GetHeight());
			this.mResolutionButton.Resize(this.mCreditsButton.mX, this.mFpsToggle.mY + Common._S(55), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mDebugButton.Resize(this.mResolutionButton.mX + this.mResolutionButton.mWidth + Common._DS(10), this.mResolutionButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0004B824 File Offset: 0x00049A24
		private void LayoutAdventureDialog()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			int num = base.GetLeft() - this.mX;
			int num2 = base.GetTop() - this.mY;
			int width = base.GetWidth();
			int num3 = width / 2;
			Common._S(Common._M(8));
			int num4 = num2 - Common._S(Common._M(40));
			this.mMusicVolumeSlider.Resize(num + Common._S(Common._M(340)), num4 - 7, num3 - Common._S(Common._M1(24)), Common._S(Common._M2(44)));
			this.mSfxVolumeSlider.Layout(4611, this.mMusicVolumeSlider, Common._S(Common._M(0)), Common._S(37), 0, 0);
			this.mColorBlindSlider.Resize(this.mSfxVolumeSlider.mX - Common._S(80), this.mSfxVolumeSlider.mY + Common._S(45), imageByID.GetWidth(), imageByID.GetHeight());
			this.mHelpButton.Resize(10 + this.mSfxVolumeSlider.mX + Common._S(115), this.mColorBlindSlider.mY + Common._S(100), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mBackToGame.Resize(10 + this.mHelpButton.mX - this.mHelpButton.mWidth - Common._S(50), this.mHelpButton.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mMainMenuButton.Resize(10 + this.mBackToGame.mX - this.mBackToGame.mWidth + Common._S(-50), this.mBackToGame.mY, OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mFpsToggle.Resize(this.mColorBlindSlider.mX, this.mColorBlindSlider.mY + Common._S(95), imageByID.GetWidth(), imageByID.GetHeight());
			this.mFullscreenToggle.Resize(this.mSfxVolumeSlider.mX, this.mFpsToggle.mY, imageByID.GetWidth(), imageByID.GetHeight());
			this.mResolutionButton.Resize(this.mHelpButton.mX, this.mFpsToggle.mY + Common._S(55), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
			this.mDebugButton.Resize(this.mResolutionButton.mX, this.mResolutionButton.mY + OptionsDialog.OPTIONS_BUTTON_HEIGHT + Common._DS(10), OptionsDialog.OPTIONS_BUTTON_WIDTH, OptionsDialog.OPTIONS_BUTTON_HEIGHT);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		private void SetMusicSlider(double inVolume)
		{
			if (this.mMusicEnabled)
			{
				GameApp.gApp.SetMusicVolume(inVolume);
			}
			if (this.mMusicVolumeSlider.mDragging)
			{
				return;
			}
			this.mMusicSliderOn = (this.mMusicEnabled && inVolume > 0.0);
			this.mMusicVolumeSlider.Label = (this.mMusicSliderOn ? TextManager.getInstance().getString(672) : TextManager.getInstance().getString(682));
			this.mMusicVolumeSlider.mDisabled = !this.mMusicEnabled;
			GameApp.gApp.mMusic.Enable(this.mMusicSliderOn);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0004BA8B File Offset: 0x00049C8B
		private void SetSfxSlider(double inVolume)
		{
			GameApp.gApp.SetSfxVolume(inVolume);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0004BA98 File Offset: 0x00049C98
		public void SliderReleased(int theId, double theVal)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0004BA9A File Offset: 0x00049C9A
		public void OnLegalInfoHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0004BAA3 File Offset: 0x00049CA3
		public void OnCreditsHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0004BAAC File Offset: 0x00049CAC
		public void OnHelpHided()
		{
			this.mState = OptionsDialog.OptionState.OptionState_None;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		public void ProcessHardwareBackButton()
		{
			switch (this.mState)
			{
			case OptionsDialog.OptionState.OptionState_OptionToMainMenuPrompt:
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				Dialog dialog = GameApp.gApp.GetDialog(1);
				if (dialog != null)
				{
					dialog.ButtonDepress(1001);
				}
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			case OptionsDialog.OptionState.OptionState_Credits:
				this.mState = OptionsDialog.OptionState.OptionState_None;
				GameApp.gApp.ReturnFromCredits();
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			case OptionsDialog.OptionState.OptionState_Help:
			{
				this.mState = OptionsDialog.OptionState.OptionState_None;
				Board board = GameApp.gApp.GetBoard();
				if (board != null && board.GauntletMode())
				{
					board.ChallengeHelpClosed();
				}
				else
				{
					GameApp.gApp.mGenericHelp.ButtonDepress(0);
				}
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			case OptionsDialog.OptionState.OptionState_Legal:
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				if (GameApp.gApp.mLegalInfo == null)
				{
					this.mState = OptionsDialog.OptionState.OptionState_None;
					return;
				}
				break;
			default:
				this.mState = OptionsDialog.OptionState.OptionState_None;
				this.SetMusicSlider(this.mOriginMusicVolume);
				this.SetSfxSlider(this.mOriginSfxVolume);
				GameApp.gApp.mShowFPS = this.mOriginShowFps;
				GameApp.gApp.mDesktopFullscreen = this.mOriginDesktopFullscreen;
				GameApp.gApp.mDesktopResolutionPreset = this.mOriginDesktopResolutionPreset;
				GameApp.gApp.ApplySavedDesktopDisplay();
				GameApp.gApp.FinishOptionsDialog(false);
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				break;
			}
		}

		// Token: 0x04000D07 RID: 3335
		private const double MUSIC_SLIDER_THRESHOLD = 0.01;

		// Token: 0x04000D08 RID: 3336
		private static int OPTIONS_BUTTON_WIDTH = Common._DS(372);

		// Token: 0x04000D09 RID: 3337
		private static int OPTIONS_BUTTON_HEIGHT = Common._DS(157);

		// Token: 0x04000D0A RID: 3338
		private static int INCLUDE_LANGUAGE_BUTTON = 0;

		// Token: 0x04000D0B RID: 3339
		public ZumaSlider mMusicVolumeSlider;

		// Token: 0x04000D0C RID: 3340
		public ZumaSlider mSfxVolumeSlider;

		// Token: 0x04000D0D RID: 3341
		public ZumaSlideBox mColorBlindSlider;

		// Token: 0x04000D0E RID: 3342
		public double mOriginMusicVolume;

		// Token: 0x04000D0F RID: 3343
		public double mOriginSfxVolume;

		// Token: 0x04000D10 RID: 3344
		public bool mOriginColorBlind;

		public bool mOriginShowFps;

		public bool mOriginDesktopFullscreen;

		public int mOriginDesktopResolutionPreset;

		// Token: 0x04000D11 RID: 3345
		public ButtonWidget mHelpButton;

		// Token: 0x04000D12 RID: 3346
		public ButtonWidget mMainMenuButton;

		// Token: 0x04000D13 RID: 3347
		public ButtonWidget mBackToGame;

		// Token: 0x04000D14 RID: 3348
		public ButtonWidget mCreditsButton;

		public ButtonWidget mDebugButton;

		public ZumaSlideBox mFpsToggle;

		public ZumaSlideBox mFullscreenToggle;

		public ButtonWidget mResolutionButton;

		// Token: 0x04000D15 RID: 3349
		public ButtonWidget mLanguageButton;

		// Token: 0x04000D16 RID: 3350
		public bool mInGame;

		// Token: 0x04000D17 RID: 3351
		public bool mMusicEnabled;

		// Token: 0x04000D18 RID: 3352
		public bool mMusicSliderOn;

		// Token: 0x04000D19 RID: 3353
		public int mHeightPad;

		// Token: 0x04000D1A RID: 3354
		protected OptionsDialog.OptionState mState;

		// Token: 0x0200011F RID: 287
		public enum ControlId
		{
			// Token: 0x04001996 RID: 6550
			OptionsDialog_MusicVolume,
			// Token: 0x04001997 RID: 6551
			OptionsDialog_SfxVolume,
			// Token: 0x04001998 RID: 6552
			OptionsDialog_Help,
			// Token: 0x04001999 RID: 6553
			OptionsDialog_ToMainMenu,
			// Token: 0x0400199A RID: 6554
			OptionsDialog_Colorblind,
			// Token: 0x0400199B RID: 6555
			OptionsDialog_Credits,
			// Token: 0x0400199C RID: 6556
			OptionsDialog_Language,
			// Token: 0x0400199D RID: 6557
			OptionsDialog_Legal,
			// Token: 0x0400199E RID: 6558
			OptionsDialog_BackToGame
		}

		// Token: 0x02000120 RID: 288
		protected enum OptionState
		{
			// Token: 0x040019A0 RID: 6560
			OptionState_BackToMainMenuPrompt,
			// Token: 0x040019A1 RID: 6561
			OptionState_OptionToMainMenuPrompt,
			// Token: 0x040019A2 RID: 6562
			OptionState_Credits,
			// Token: 0x040019A3 RID: 6563
			OptionState_Help,
			// Token: 0x040019A4 RID: 6564
			OptionState_Legal,
			// Token: 0x040019A5 RID: 6565
			OptionState_None
		}
	}
}
