using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using JeffLib;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Drivers.App;
using SexyFramework.Drivers.Graphics;
using SexyFramework.Drivers.File;
using SexyFramework.Drivers.Profile;
using SexyFramework.File;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;
using ZumasRevenge.Profile;
using ZumasRevenge.Sound;

namespace ZumasRevenge
{
	// Token: 0x02000006 RID: 6
	public class GameApp : SexyApp, NewUserDialogListener, ProfileEventListener
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003850 File Offset: 0x00001A50
		protected void PreShowLoadingScreen()
		{
			if (!this.mResourceManager.IsGroupLoaded("LoadScreen"))
			{
				this.mResourceManager.LoadResources("LoadScreen");
			}
			this.mResourceManager.LoadImage("ATLASIMAGE_ATLAS_GAMEPLAY_640_00");
			this.mResourceManager.LoadImage("ATLASIMAGE_ATLAS_MENURELATED_640_00");
			this.mMusic.LoadMusic(1, "music/MUSIC_LOADING");
			this.mMusic.LoadMusic(0, "music/MUSIC_HAWAIIAN");
			this.mMusic.Enable(true);
			this.StartLoading();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000038D8 File Offset: 0x00001AD8
		public void ShowLoadingScreen()
		{
			this.mLoadingScreen = new LoadingScreen();
			this.mLoadingScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mLoadingScreen);
			if (this.mMusic.IsUserMusicPlaying())
			{
				this.mLoadingScreen.ProcessBGM();
			}
			this.mUnderDialogWidget.CreateImages();
			this.mUnderDialogWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mUnderDialogWidget);
			this.mUnderDialogWidget.SetVisible(false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003973 File Offset: 0x00001B73
		protected void LoadingScreenCallback()
		{
			this.mWidgetManager.BringToFront(this.mLoadingScreen);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003988 File Offset: 0x00001B88
		protected void SetupMainMenuDefaults(bool do_load_thread)
		{
			if (do_load_thread)
			{
				this.mLoadType = 4;
				this.mStartInGameModeThreadProcRunning = true;
				int num = Common._DS(Common._M(700));
				int num2 = Common._DS(Common._M(650));
				Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
				if (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3)
				{
					Common._DS(Common._M(160));
				}
				this.StartMMThreadProc();
				this.DoCommonInGameLoadThread(new Rect((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2));
				this.mReturnToMMDlg = null;
			}
			else
			{
				this.StartMMThreadProc();
			}
			this.mWidgetManager.AddWidget(this.mMainMenu);
			this.ClearUpdateBacklog(true);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003A45 File Offset: 0x00001C45
		protected void SetupMainMenuDefaults()
		{
			this.SetupMainMenuDefaults(true);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A50 File Offset: 0x00001C50
		protected void DoCommonInGameLoadThread(Rect aRect)
		{
			this.mLoadRect = aRect;
			bool flag = false;
			while (this.mStartInGameModeThreadProcRunning)
			{
				this.UpdateAppStep(ref flag);
				Common.SexySleep(0);
			}
			this.mWidgetManager.MarkAllDirty();
			if (this.mInGameLoadThreadProcFailed)
			{
				this.Popup("There was an error initializing the game.");
				this.mBoard.Dispose();
				this.mBoard = null;
				for (int i = 0; i < Common.size<Level>(this.mNormalLevelMgr.mLevels); i++)
				{
					this.mNormalLevelMgr.mLevels[i].mBoard = null;
				}
				if (this.mWidescreenBoardWidget != null)
				{
					this.mWidgetManager.RemoveWidget(this.mWidescreenBoardWidget);
					base.SafeDeleteWidget(this.mWidescreenBoardWidget);
					this.mWidescreenBoardWidget = null;
				}
				this.RestoreMainMenuAfterAdventureFailure();
				return;
			}
			if (this.mLoadType != 4)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
				this.mWidgetManager.SetFocus(this.mBoard);
				if (this.mWidescreenBoardWidget == null)
				{
					this.mWidescreenBoardWidget = new WidescreenBoardWidget();
					this.mWidescreenBoardWidget.Resize(Common._S(-80), 0, this.mWidth + Common._S(160), this.mHeight);
					this.mWidgetManager.AddWidget(this.mWidescreenBoardWidget);
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B90 File Offset: 0x00001D90
		public GameApp(Game xnaGame, bool from_reinit)
		{
			GameApp.gApp = this;
			this.mGameMain = xnaGame;
			((WP7AppDriver)this.mAppDriver).InitXNADriver(xnaGame);
			this.SetBoolean("drivers.ios.use_gles20", true);
			this.SetBoolean("drivers.ios.use_multitouch", false);
			this.SetInteger("compat_AppOrigScreenWidth", this.mOrigScreenWidth);
			this.SetInteger("compat_AppOrigScreenHeight", this.mOrigScreenHeight);
			this.mSavingOrLoadingProfile = false;
			this.mWideScreenXOffset = 0;
			this.mUpsell = null;
			this.mDoingDRM = false;
			this.mTrialType = 0;
			this.mShotCorrectionAngleToWidthDist = 1500f;
			this.mShotCorrectionAngleMax = 13f;
			this.mShotCorrectionWidthMax = 65f;
			this.mGuideStyle = 1;
			this.mShotCorrectionDebugStyle = 3;
			this.mIronFrogModeIncluded = false;
			this.mGenericHelp = null;
			this.mLegalInfo = null;
			this.mAboutInfo = null;
			this.mProdName = "ZumasRevenge";
			this.mRegKey = "PopCap\\ZumasRevenge";
			this.mLevelXML = "levels/levels";
			this.mHardLevelXML = "levels/levels_hard";
			this.mBoard = null;
			this.mDebugKeysEnabled = false;
			this.mAllowSwapScreenImage = false;
			this.mLoadType = -1;
			this.mCredits = null;
			this.mIFLoadingAnimStartCel = 0;
			this.mDelayIntro = false;
			this.mReturnToMMDlg = null;
			this.mDoingAdvModeLoad = false;
			this.mConfTime = 1500;
			GameApp.mGameRes = 640;
			this.mHiRes = false;
			this.mWidescreenAware = true;
			this.mWidescreenTranslate = true;
			this.mAllowWindowResize = true;
			this.mReInit = false;
			this.mFromReInit = from_reinit;
			this.mMapScreen = null;
			this.mMapScreenHackWidget = null;
			this.mInGameLoadThreadProcFailed = false;
			this.mForceZoneRestart = -1;
			this.mStartInGameModeThreadProcRunning = false;
			this.mClickedHardMode = false;
			this.mContinuedGame = false;
			GameApp.gNeedsPreCache = true;
			this.mAutoMonkey = null;
			GameApp.initResolution(640);
			this.mAutoStartLoadingThread = false;
			this.mLoadingScreen = null;
			this.mFramesPlayed = 0;
			this.mAutoEnable3D = true;
			this.mNoVSync = true;
			this.mCachedLoadState = 0;
			this.mCachedLoad = false;
			this.mNormalLevelMgr = null;
			this.mCustomCursorsEnabled = true;
			this.mCursorTarget = true;
			this.mColorblind = false;
			this.mUserProfile = null;
			this.mProfileMgr = null;
			this.mMainMenu = null;
			this.mMoreGames = null;
			this.mNewUserDlg = null;
			this.mUnderDialogWidget = new UnderDialogWidget();
			this.mDialogObscurePct = 0f;
			this.mFullscreenBits = 32;
			this.mInitialLoad = true;
			GameApp.gDDS = new DDS();
			GameApp.gDDS.mMinLevel = int.MaxValue;
			this.mBambooTransition = null;
			this.mProductVersion = this.GetProductVersion("");
			if (!this.mFileDriver.InitFileDriver(this))
			{
				this.Shutdown();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003ED4 File Offset: 0x000020D4
		public string GetProductVersion(string thePath)
		{
			string fullName = Assembly.GetCallingAssembly().FullName;
			string text = "v" + fullName.Split(new char[]
			{
				'='
			})[1].Split(new char[]
			{
				','
			})[0];
			return text.Substring(0, text.Length - 2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003F30 File Offset: 0x00002130
		public override void Dispose()
		{
			if (this.mSoundManager != null)
			{
				this.mSoundManager.ReleaseChannels();
				this.mSoundManager.ReleaseSounds();
			}
			if (this.mGenericHelp != null)
			{
				this.KillDialog(this.mGenericHelp);
				this.mGenericHelp = null;
			}
			if (this.mLegalInfo != null)
			{
				this.KillDialog(this.mLegalInfo);
				this.mLegalInfo = null;
			}
			if (this.mAboutInfo != null)
			{
				this.KillDialog(this.mAboutInfo);
				this.mAboutInfo = null;
			}
			if (this.mBambooTransition != null)
			{
				this.mWidgetManager.RemoveWidget(this.mBambooTransition);
				this.mBambooTransition = null;
			}
			if (this.mUpsell != null)
			{
				this.mWidgetManager.RemoveWidget(this.mUpsell);
				this.mUpsell = null;
			}
			if (this.gCreditsHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.gCreditsHackWidget);
			}
			this.gCreditsHackWidget = null;
			this.mWidgetManager.RemoveWidget(this.mUnderDialogWidget);
			this.mUnderDialogWidget = null;
			this.mCredits = null;
			Ball.DeleteBallGlobals();
			if (this.mBoard != null)
			if (this.mBoard != null)
			{
				if (this.mBoard.NeedSaveGame() && this.mUserProfile != null)
				{
					this.mBoard.SaveGame(this.mUserProfile.GetSaveGameName(this.IsHardMode()), null);
				}
				this.mWidgetManager.RemoveWidget(this.mBoard);
			}
			this.mReturnToMMDlg = null;
			this.mProxBombManager = null;
			this.mLevelThumbnails.Clear();
			this.mMusic = null;
			this.mSoundPlayer = null;
			this.mBoard = null;
			if (this.mNormalLevelMgr != null)
			{
				for (int i = 0; i < Common.size<Level>(this.mNormalLevelMgr.mLevels); i++)
				{
					this.mNormalLevelMgr.mLevels[i].mBoard = null;
				}
			}
			if (this.mMapScreen != null)
			{
				this.mMapScreen.CleanButtons();
			}
			if (this.mMapScreenHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMapScreenHackWidget);
			}
			this.mMapScreenHackWidget = null;
			this.mMapScreen = null;
			if (this.mMainMenu != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMainMenu);
			}
			this.mMainMenu = null;
			if (this.mMoreGames != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMoreGames);
			}
			this.mMoreGames = null;
			if (this.mLoadingScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mLoadingScreen);
			}
			this.mLoadingScreen = null;
			this.mNormalLevelMgr = null;
			if (this.mNewUserDlg != null)
			{
				this.KillDialog(this.mNewUserDlg.mId, true, false);
			}
			this.mNewUserDlg = null;
			GameApp.gDDS = null;
			for (int j = 0; j < Common.size<CachedTorchEffect>(this.mCachedTorchEffects); j++)
			{
				this.mCachedTorchEffects[j].mTorchFlame = null;
				this.mCachedTorchEffects[j].mTorchFlameOut = null;
			}
			for (int k = 0; k < Common.size<CachedVolcanoEffect>(this.mCachedVolcanoEffects); k++)
			{
				this.mCachedVolcanoEffects[k].mExplosion = null;
				this.mCachedVolcanoEffects[k].mProjectile = null;
			}
			this.mResourceManager.DeleteResources("");
			this.mProfileMgr = null;
			this.RegistryWriteBoolean("LastShutdownOK", true);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004248 File Offset: 0x00002448
		public bool IsWideScreen()
		{
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000424B File Offset: 0x0000244B
		public int GetWideScreenAdjusted(int x)
		{
			return x;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000424E File Offset: 0x0000244E
		public int GetWidthAdjusted(int x)
		{
			return x - Common._DS(125);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000425C File Offset: 0x0000245C
		public bool LoadMoreGamesInfo()
		{
			Buffer buffer = new Buffer();
			if (base.ReadBufferFromFile(Common.GetAppDataFolder() + "users/mg.dat", ref buffer))
			{
				this.mLastMoreGamesUpdate = buffer.ReadLong();
				return true;
			}
			return false;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004297 File Offset: 0x00002497
		public void SaveMoreGamesInfo()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004299 File Offset: 0x00002499
		public void ConsoleCallback(string cmd, List<string> _params)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000429B File Offset: 0x0000249B
		public void SaveProfile()
		{
			if (!this.mSavingOrLoadingProfile && this.mUserProfile != null)
			{
				this.mSavingOrLoadingProfile = true;
				this.mUserProfile.SaveDetails();
				this.mSavingOrLoadingProfile = false;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000042C8 File Offset: 0x000024C8
		public bool HasSaveGame()
		{
			if (this.mUserProfile == null)
			{
				return false;
			}
			string saveGameName = this.mUserProfile.GetSaveGameName(this.IsHardMode());
			return StorageFile.FileExists(saveGameName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000042FC File Offset: 0x000024FC
		public void HandleCrash(bool from_assert)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004300 File Offset: 0x00002500
		public void SaveGlobalConfig()
		{
			Buffer buffer = new Buffer();
			buffer.WriteDouble(this.mMusicVolume);
			buffer.WriteDouble(this.mSfxVolume);
			buffer.WriteBoolean(this.mColorblind);
			buffer.WriteBoolean(this.mShowFPS);
			buffer.WriteBoolean(this.mDesktopFullscreen);
			buffer.WriteLong(this.mDesktopResolutionPreset);
			StorageFile.WriteBufferToFile("users/OptionConfig.sav", buffer);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004344 File Offset: 0x00002544
		public void LoadGlobalConfig()
		{
			Buffer buffer = new Buffer();
			if (StorageFile.ReadBufferFromFile("users/OptionConfig.sav", buffer))
			{
				this.mMusicVolume = buffer.ReadDouble();
				this.mSfxVolume = buffer.ReadDouble();
				this.mColorblind = buffer.ReadBoolean();
				try
				{
					this.mShowFPS = buffer.ReadBoolean();
					this.mDesktopFullscreen = buffer.ReadBoolean();
					this.mDesktopResolutionPreset = (int)buffer.ReadLong();
				}
				catch (Exception)
				{
				}
				this.SetMusicVolume(this.mMusicVolume);
				this.SetSfxVolume(this.mSfxVolume);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000043A0 File Offset: 0x000025A0
		public void RevertOptionsChanges()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			this.RegistryReadBoolean("PreHiRes", ref flag);
			this.RegistryReadBoolean("Pre3D", ref flag2);
			this.RegistryReadBoolean("PreWindowed", ref flag3);
			this.RegistryWriteBoolean("NeedsConfirmation", false);
			this.SwitchScreenMode(flag3, flag2, true);
			this.mPreferredWidth = (this.mPreferredHeight = -1);
			this.RegistryWriteBoolean("HiRes", flag);
			this.mReInit = true;
			this.Shutdown();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004420 File Offset: 0x00002620
		public void InGameLoadThread_DrawFunc()
		{
			GameApp.InGameLoadThread_DrawFunc_CallCounter++;
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_STROKE");
			Image imageByName = this.GetImageByName("IMAGE_BLUE_BALL");
			SexyGraphics graphics = new SexyGraphics(this.mWidgetManager.mImage);
			string text = (this.mLoadType == 4) ? TextManager.getInstance().getString(726) : TextManager.getInstance().getString(581);
			string text2 = "";
			int num = GameApp.InGameLoadThread_DrawFunc_CallCounter % 40;
			if (num >= 30)
			{
				text2 = "...";
			}
			else if (num >= 20)
			{
				text2 = "..";
			}
			else if (num >= 10)
			{
				text2 = ".";
			}
			text += text2;
			if (this.mLoadType == 1 || this.mLoadType == 0)
			{
				Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
				int num2 = (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? Common._DS(Common._M(160)) : 0;
				if (this.mLoadType == 1)
				{
					int num3 = (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? Common._DS(Common._M(80)) : 0;
					int num4 = this.mLoadRect.mX + (this.mLoadRect.mWidth - fontByName.StringWidth("Loading...")) / 2;
					num4 += num3;
					int num5 = this.mLoadRect.mY + (this.mLoadRect.mHeight - fontByName.mHeight) / 2 + Common._DS(Common._M(50));
					graphics.SetColor(250, 124, 0);
					graphics.SetFont(fontByName);
					graphics.DrawString(text, num4, num5);
					return;
				}
				if (this.mLoadType == 0)
				{
					int num4 = Common._DS(Common._M(656)) + (Common._DS(Common._M1(330)) - fontByName.StringWidth("Loading...")) / 2 - 2;
					num4 += num2;
					int num5 = Common._DS(Common._M(697)) + (Common._DS(Common._M1(500)) - fontByName.mHeight) / 2 - 2;
					graphics.SetColor(Color.White);
					graphics.SetFont(fontByName);
					graphics.DrawString(text, num4 + 2, num5 + graphics.GetFont().GetAscent() + 2);
					return;
				}
			}
			else
			{
				if (this.mLoadType == 2)
				{
					graphics.SetFont(fontByName);
					graphics.SetColor(250, 124, 0);
					graphics.DrawString(text, this.mLoadRect.mX + (this.mLoadRect.mWidth - graphics.GetFont().StringWidth("Loading...")) / 2, this.mLoadRect.mY + (this.mLoadRect.mHeight - graphics.GetFont().mHeight) / 2 + graphics.GetFont().GetAscent());
					return;
				}
				if (this.mLoadType == 3)
				{
					graphics.SetFont(fontByName);
					graphics.SetColor(Color.White);
					return;
				}
				if (this.mLoadType == 4)
				{
					graphics.Translate(this.mReturnToMMDlg.mX, this.mReturnToMMDlg.mY);
					this.mReturnToMMDlg.Draw(graphics);
					graphics.SetFont(fontByName);
					graphics.SetColor(250, 124, 0);
					graphics.DrawString(text, (this.mLoadRect.mWidth - graphics.GetFont().StringWidth("Returning to Menu...")) / 2 + Common._DS(Common._M(20)), (this.mLoadRect.mHeight - graphics.GetFont().mHeight) / 2 - Common._DS(Common._M1(30)) + graphics.GetFont().GetAscent());
					int num6 = Common._DS(Common._M(400));
					int num7 = this.mLoadRect.mWidth - imageByName.GetCelWidth() * 4 - Common._DS(Common._M(-100));
					int num8 = num7 / 4;
					Image[] array = new Image[]
					{
						this.GetImageByName("IMAGE_BLUE_BALL"),
						this.GetImageByName("IMAGE_RED_BALL"),
						this.GetImageByName("IMAGE_YELLOW_BALL"),
						this.GetImageByName("IMAGE_GREEN_BALL")
					};
					int[] array2 = new int[]
					{
						Common.Rand(50),
						Common.Rand(50),
						Common.Rand(50),
						Common.Rand(50)
					};
					for (int i = 0; i < 4; i++)
					{
						int num9 = array[i].mNumCols * array[i].mNumRows;
						int num10 = (array2[i] + GameApp.InGameLoadThread_DrawFunc_CallCounter) % num9;
						if (num10 < 0)
						{
							num10 = -num10;
						}
						else if (num10 >= num9)
						{
							num10 = num9 - 1;
						}
						Rect rect;
						rect = new Rect(array[i].GetCelRect(num10));
						graphics.DrawImageRotated(array[i], num8 + num7 / 4 * i, num6, -1.5707963705062866, rect);
					}
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000048F0 File Offset: 0x00002AF0
		public void StartAdvModeThreadProc()
		{
			this.mBoard = new Board(this, -1);
			this.mBoard.mAdventureMode = true;
			this.mBoard.mIsHardMode = this.mClickedHardMode;
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mContinuedGame = false;
			if (this.HasSaveGame() && this.mForceZoneRestart == -1)
			{
				if (!this.mBoard.LoadGame(this.mUserProfile.GetSaveGameName(this.IsHardMode())))
				{
					StorageFile.DeleteFile(this.mUserProfile.GetSaveGameName(this.IsHardMode()));
					this.mUserProfile.ClearAdventureModeDetails();
				}
				else
				{
					this.mContinuedGame = true;
				}
			}
			else
			{
				this.PlaySong(12);
				if (this.mForceZoneRestart != -1)
				{
					this.mBoard.RestartFromZone(this.mForceZoneRestart);
				}
				else if (!this.mBoard.StartLevel(1))
				{
					this.mInGameLoadThreadProcFailed = true;
					this.mStartInGameModeThreadProcRunning = false;
					return;
				}
			}
			this.mBoard.MakeCachedBackground();
			this.mInGameLoadThreadProcFailed = false;
			this.mForceZoneRestart = -1;
			this.mStartInGameModeThreadProcRunning = false;
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004A2C File Offset: 0x00002C2C
		public void StartChallengeModeThreadProc()
		{
			this.mBoard = new Board(this, this.mNormalLevelMgr.GetStartingGauntletLevel(this.mChallengeLevelId));
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			if (!this.mBoard.StartLevel(this.mChallengeLevelId))
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public void StartIronFrogModeThreadProc()
		{
			this.mBoard = new Board(this, -1);
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			if (!this.mBoard.StartLevel(this.mNormalLevelMgr.GetFirstIronFrogLevel() + 1))
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004B50 File Offset: 0x00002D50
		public void StartMMThreadProc()
		{
			if (!this.mResourceManager.IsGroupLoaded("MenuRelated") && !this.mResourceManager.LoadResources("MenuRelated"))
			{
				this.mStartInGameModeThreadProcRunning = false;
				this.mInGameLoadThreadProcFailed = true;
				return;
			}
			if (this.mResourceManager.IsGroupLoaded("GrottoSounds"))
			{
				this.mResourceManager.DeleteResources("GrottoSounds");
			}
			if (this.mResourceManager.IsGroupLoaded("Boss6Common"))
			{
				this.mResourceManager.DeleteResources("Boss6Common");
			}
			this.mMainMenu = new MainMenu(this);
			this.mMainMenu.Init();
			this.mMainMenu.Resize(this.GetScreenRect());
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004C0A File Offset: 0x00002E0A
		public void DoUpsell(bool from_exit)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004C0C File Offset: 0x00002E0C
		public bool IsRegistered()
		{
			return true;
		}

		public bool EnsureAdventureResourcesLoaded()
		{
			string[] groups = new string[]
			{
				"Map",
				"MapZoom",
				"CloakedBoss",
				"IntroScreen",
				"Text"
			};
			for (int i = 0; i < groups.Length; i++)
			{
				string group = groups[i];
				if (this.mResourceManager.IsGroupLoaded(group))
				{
					continue;
				}
				if (!this.mResourceManager.LoadResources(group))
				{
					Debug.WriteLine("Failed to load resource group '" + group + "': " + this.mResourceManager.GetErrorText());
					this.ShowResourceError(false);
					return false;
				}
			}
			return true;
		}

		public void CancelBambooTransition()
		{
			if (this.mBambooTransition == null)
			{
				return;
			}
			this.mBambooTransition.mTransitionDelegate = null;
			if (this.mBambooTransition.IsInProgress())
			{
				this.mBambooTransition.Reset();
			}
			this.mBambooTransition.SetVisible(false);
			this.mBambooTransition.SetDisabled(true);
			this.mWidgetManager.RemoveWidget(this.mBambooTransition);
		}

		public void RestoreMainMenuAfterAdventureFailure()
		{
			this.CancelBambooTransition();
			if (this.mMapScreenHackWidget != null || this.mMapScreen != null)
			{
				this.HideAdventureModeMapScreen();
			}
			if (this.mBoard != null)
			{
				this.mBoard.mSkipShutdownSave = true;
				this.mWidgetManager.RemoveWidget(this.mBoard);
				this.mBoard.Dispose();
				this.mBoard = null;
			}
			if (this.mWidescreenBoardWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.mWidescreenBoardWidget);
				base.SafeDeleteWidget(this.mWidescreenBoardWidget);
				this.mWidescreenBoardWidget = null;
			}
			if (this.mMainMenu == null)
			{
				this.StartMMThreadProc();
				if (this.mMainMenu != null)
				{
					this.mWidgetManager.AddWidget(this.mMainMenu);
					this.mWidgetManager.SetFocus(this.mMainMenu);
					this.mMainMenu.RehupButtons();
				}
			}
			if (this.mMainMenu != null)
			{
				this.mMainMenu.mFirstTimeAlpha = 0;
			}
			this.mStartInGameModeThreadProcRunning = false;
			this.mInGameLoadThreadProcFailed = false;
			this.PlaySong(1);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004C0F File Offset: 0x00002E0F
		public bool IsSafeForLockout()
		{
			return !this.mLoadingThreadStarted || this.mLoadingThreadCompleted;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004C21 File Offset: 0x00002E21
		public void DoLockout()
		{
			this.mDoingDRM = true;
			if (this.mBoard != null)
			{
				this.mBoard.DoShutdownSaveGame();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C40 File Offset: 0x00002E40
		public void DoCredits(bool isFromMainMenu)
		{
			if (!this.mResourceManager.IsGroupLoaded("Credits") && !this.mResourceManager.LoadResources("Credits"))
			{
				this.ShowResourceError(true);
				this.Shutdown();
				return;
			}
			this.mCredits = new Credits(isFromMainMenu);
			this.mCredits.Init(this.mBoard != null && !this.mBoard.IsHardAdventureMode());
			this.gCreditsHackWidget = new CreditsHackWidget();
			this.gCreditsHackWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.gCreditsHackWidget.mClip = false;
			this.mWidgetManager.AddWidget(this.gCreditsHackWidget);
			if (!isFromMainMenu)
			{
				this.EndCurrentGame();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004CFC File Offset: 0x00002EFC
		public void ReturnFromCredits()
		{
			if (!this.mCredits.mFromMainMenu)
			{
				this.ShowMainMenu();
			}
			this.mWidgetManager.RemoveWidget(this.gCreditsHackWidget);
			base.SafeDeleteWidget(this.gCreditsHackWidget);
			this.mCredits.Dispose();
			this.mCredits = null;
			this.gCreditsHackWidget = null;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D52 File Offset: 0x00002F52
		public void GenericHelpClosed()
		{
			this.mGenericHelp = null;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004D5B File Offset: 0x00002F5B
		public void SetStat(string stat_name, int val)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004D5D File Offset: 0x00002F5D
		public int GetStat(string stat_name)
		{
			return 0;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004D60 File Offset: 0x00002F60
		public void SetAchievement(string achievement_name)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004D62 File Offset: 0x00002F62
		public void ResetAchievements()
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004D64 File Offset: 0x00002F64
		public void RehupAchievements()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004D66 File Offset: 0x00002F66
		public virtual void ConvertResources()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004D68 File Offset: 0x00002F68
		public virtual void ConvertLevels()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004D6A File Offset: 0x00002F6A
		public void OnHardwareBackButtonPressed()
		{
			GlobalMembers.IsBackButtonPressed = true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004D72 File Offset: 0x00002F72
		public void OnHardwareBackButtonPressProcessed()
		{
			GlobalMembers.IsBackButtonPressed = false;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004D7A File Offset: 0x00002F7A
		public void OnExiting()
		{
			if (this.mBoard != null)
			{
				this.mBoard.ProcessExitingEvent();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D8F File Offset: 0x00002F8F
		public void OnDeactivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.PauseAllMusic();
				this.mMusicInterface.OnDeactived();
			}
			if (this.mBoard != null)
			{
				this.mBoard.ProcessOnDeactiveEvent();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public void OnActivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnActived();
				this.mMusicInterface.ResumeAllMusic();
			}
			// GameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004DEC File Offset: 0x00002FEC
		public void OnServiceActivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnServiceActived();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004E01 File Offset: 0x00003001
		public void OnServiceDeactivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnServiceDeactived();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004E16 File Offset: 0x00003016
		public bool IsHardwareBackButtonPressed()
		{
			return GlobalMembers.IsBackButtonPressed;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004E1D File Offset: 0x0000301D
		public void InitText()
		{
			TextManager.getInstance().init();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004E2C File Offset: 0x0000302C
		public override void Init()
		{
			try
			{
				ContentReaderRegistration.RegisterAll();
            int num = GameApp.mGameRes;
				if (num != 320 && num != 640)
				{
					if (num == 768)
					{
						this.mWideScreenXOffset = Common._DS(160);
					}
				}
				else
				{
					this.mWideScreenXOffset = 0;
				}
				this.mProfileMgr = new ZumaProfileMgr();
				this.mProfileManager = this.mProfileMgr;
				this.mAutoMonkey = new AutoMonkey(this);
				base.Init();
				Res.InitResources(this);
				this.mResourceManager.mBaseArtRes = GameApp.mGameRes;
				this.mResourceManager.mLeadArtRes = 1200;
				this.mResourceManager.mCurArtRes = GameApp.mGameRes;
				this.SetString("DIALOG_BUTTON_YES", TextManager.getInstance().getString(446));
				this.SetString("DIALOG_BUTTON_NO", TextManager.getInstance().getString(447));
				this.SetString("DIALOG_BUTTON_OK", TextManager.getInstance().getString(675));
				this.SetString("DIALOG_BUTTON_CANCEL", TextManager.getInstance().getString(454));
				this.mCachedLoad = false;
				this.InitAudio();
				this.PreShowLoadingScreen();
				this.LoadGlobalConfig();
				this.mPendingDesktopDisplayApply = true;
			}
			catch (Exception ex)
			{
				this.mInitFailureReason = ex.ToString();
				Debug.WriteLine("GameApp.Init failed: " + this.mInitFailureReason);
				StartupError.Log("GameApp.Init failed: " + this.mInitFailureReason);
				this.mInitFailed = true;
			}
			finally
			{
				this.mInitFinished = true;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004F5C File Offset: 0x0000315C
		public void StartThreadInit()
		{
			ThreadStart threadStart = new ThreadStart(this.Init);
			this.mInitThread = new Thread(threadStart);
			this.mInitThread.Start();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004F8E File Offset: 0x0000318E
		public override void InitHook()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004F90 File Offset: 0x00003190
		public override string NotifyCrashHook()
		{
			return base.NotifyCrashHook();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004F98 File Offset: 0x00003198
		public string GetCrashZipName(int num_override)
		{
			return "";
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F9F File Offset: 0x0000319F
		public string GetCrashZipName()
		{
			return this.GetCrashZipName(-1);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004FA8 File Offset: 0x000031A8
		protected void GamerSignedInCallback(object sender, EventArgs args)
		{
			// Xbox LIVE sign-in removed for MonoGame port
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005074 File Offset: 0x00003274
		protected void GetAchievementsCallback(IAsyncResult result)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000050E8 File Offset: 0x000032E8
		public override void LoadingThreadProc()
		{
			if (this.mCachedLoadState > 1)
			{
				return;
			}
			GameApp.gInitialProfLoadSuccessful = this.mProfileMgr.Init();
			// SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
			int num = 70;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				Level levelByIndex;
				do
				{
					levelByIndex = this.mNormalLevelMgr.GetLevelByIndex(num2++);
				}
				while (levelByIndex != null && (levelByIndex.mBoss != null || Common.StrFindNoCase(levelByIndex.mId, "boss") != -1));
				if (levelByIndex == null)
				{
					break;
				}
				_ = "levelthumbs\\" + levelByIndex.mId.ToLower() + "_thumb";
				IdxThumbPair idxThumbPair = new IdxThumbPair();
				idxThumbPair.first = num2 - 1;
				idxThumbPair.second = null;
				this.mLevelThumbnails.Add(idxThumbPair);
			}
			this.mResourceManager.PrepareLoadResourcesList(GameApp.gInitialLoadGroups);
			this.mMusic.LoadMusic(12, "music/MUSIC_TUNE1");
			this.mMusic.LoadMusic(24, "music/MUSIC_TUNE2");
			this.mMusic.LoadMusic(35, "music/MUSIC_TUNE3");
			this.mMusic.LoadMusic(45, "music/MUSIC_TUNE4");
			this.mMusic.LoadMusic(58, "music/MUSIC_TUNE5");
			this.mMusic.LoadMusic(71, "music/MUSIC_TUNE6");
			this.mMusic.LoadMusic(120, "music/MUSIC_WON1");
			this.mMusic.LoadMusic(121, "music/MUSIC_WON2");
			this.mMusic.LoadMusic(122, "music/MUSIC_WON3");
			this.mMusic.LoadMusic(123, "music/MUSIC_WON4");
			this.mMusic.LoadMusic(124, "music/MUSIC_WON5");
			this.mMusic.LoadMusic(125, "music/MUSIC_WON6");
			this.mMusic.LoadMusic(127, "music/MUSIC_BOSS");
			this.mMusic.LoadMusic(144, "music/MUSIC_WON_GAME");
			this.mMusic.LoadMusic(126, "music/MUSIC_GAME_OVER");
			GameApp.gInitialProfLoadSuccessful = true;
			this.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
			GameApp.gDDS.ChangeProfile(this.mUserProfile);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000052FC File Offset: 0x000034FC
		public override void LoadingThreadCompleted()
		{
			base.LoadingThreadCompleted();
			Enumerable.Count<string>(GameApp.gInitialLoadGroups);
			this.mBambooTransition = new BambooTransition();
			this.mProxBombManager = new ProxBombManager();
			if (this.mCachedLoad)
			{
				this.mLoadingThreadCompleted = true;
				this.mLoaded = true;
				this.ShowMainMenu();
				return;
			}
			if (this.mLoadingFailed || this.mCachedLoadState > 1)
			{
				return;
			}
			this.mLoadingScreen.LoadingComplete();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000536A File Offset: 0x0000356A
		public bool IsFinishedLoading()
		{
			return true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000536D File Offset: 0x0000356D
		public void GameFinishedLoading()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005370 File Offset: 0x00003570
		public void StartLoading()
		{
			if (!this.mResourceManager.IsGroupLoaded("MainSounds"))
			{
				this.mResourceManager.LoadResources("MainSounds");
			}
			if (!this.mResourceManager.IsGroupLoaded("Text"))
			{
				this.mResourceManager.LoadResources("Text");
			}
			Font fontByName = this.GetFontByName("FONT_SHAGEXOTICA68_BASE");
			((ImageFont)fontByName).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName2 = this.GetFontByName("FONT_SHAGEXOTICA68_BLACK");
			((ImageFont)fontByName2).PushLayerColor("Main", new Color(0, 0, 0, 255));
			Font fontByName3 = this.GetFontByName("FONT_SHAGEXOTICA68_STROKE");
			((ImageFont)fontByName3).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName4 = this.GetFontByName("FONT_SHAGLOUNGE28_STROKE");
			((ImageFont)fontByName4).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName5 = this.GetFontByName("FONT_SHAGEXOTICA38_BASE");
			((ImageFont)fontByName5).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName5).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName6 = this.GetFontByName("FONT_SHAGEXOTICA38_BLACK");
			((ImageFont)fontByName6).PushLayerColor("Main", new Color(0, 0, 0, 255));
			Font fontByName7 = this.GetFontByName("FONT_SHAGEXOTICA38_BLACK_GLOW");
			((ImageFont)fontByName7).PushLayerColor("Glow", new Color(0, 0, 0, 255));
			Font fontByName8 = this.GetFontByName("FONT_SHAGEXOTICA38_GREEN_STROKE");
			((ImageFont)fontByName8).PushLayerColor("Stroke", new Color(79, 91, 66, 255));
			Font fontByName9 = this.GetFontByName("FONT_SHAGEXOTICA100_BASE");
			((ImageFont)fontByName9).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName9).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName10 = this.GetFontByName("FONT_SHAGEXOTICA100_STROKE");
			((ImageFont)fontByName10).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName10).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName11 = this.GetFontByName("FONT_SHAGEXOTICA100_GAUNTLET");
			((ImageFont)fontByName11).PushLayerColor("Main", new Color(85, 50, 160, 255));
			((ImageFont)fontByName11).PushLayerColor("Stroke", new Color(248, 238, 195, 255));
			((ImageFont)fontByName11).PushLayerColor("Shadow", new Color(235, 131, 130, 255));
			Font fontByName12 = this.GetFontByName("FONT_SHAGLOUNGE28_BASE");
			((ImageFont)fontByName12).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			if ((int)Localization.GetCurrentLanguage() != 6 && (int)Localization.GetCurrentLanguage() != 7)
			{
				((ImageFont)fontByName12).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			}
			Font fontByName13 = this.GetFontByName("FONT_SHAGLOUNGE28_SHADOW");
			((ImageFont)fontByName13).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName14 = this.GetFontByName("FONT_SHAGLOUNGE28_STROKE_GREEN");
			((ImageFont)fontByName14).PushLayerColor("Stroke", new Color(80, 92, 67, 255));
			Font fontByName15 = this.GetFontByName("FONT_SHAGLOUNGE28_BROWN");
			((ImageFont)fontByName15).PushLayerColor("Main", new Color(193, 145, 54, 255));
			((ImageFont)fontByName15).PushLayerColor("Stroke", new Color(66, 45, 14, 255));
			((ImageFont)fontByName15).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName16 = this.GetFontByName("FONT_SHAGLOUNGE28_GREEN");
			((ImageFont)fontByName16).PushLayerColor("Main", new Color(165, 232, 25, 255));
			((ImageFont)fontByName16).PushLayerColor("Glow", new Color(0, 0, 0, 255));
			Font fontByName17 = this.GetFontByName("FONT_SHAGLOUNGE38_BASE");
			((ImageFont)fontByName17).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName17).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName18 = this.GetFontByName("FONT_SHAGLOUNGE38_STROKE");
			((ImageFont)fontByName18).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName19 = this.GetFontByName("FONT_SHAGLOUNGE38_RED_STROKE_YELLOW");
			((ImageFont)fontByName19).PushLayerColor("Main", new Color(218, 10, 9, 255));
			((ImageFont)fontByName19).PushLayerColor("Stroke", new Color(248, 241, 135, 255));
			Font fontByName20 = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			((ImageFont)fontByName20).PushLayerColor("Stroke", new Color(247, 207, 0, 255));
			Font fontByName21 = this.GetFontByName("FONT_SHAGLOUNGE38_GAUNTLET");
			((ImageFont)fontByName21).PushLayerColor("Main", new Color(249, 245, 188, 255));
			((ImageFont)fontByName21).PushLayerColor("Stroke", new Color(88, 51, 159, 255));
			Font fontByName22 = this.GetFontByName("FONT_SHAGLOUNGE38_GAUNTLET2");
			((ImageFont)fontByName22).PushLayerColor("Main", new Color(251, 245, 189, 255));
			((ImageFont)fontByName22).PushLayerColor("Stroke", new Color(228, 39, 226, 255));
			Font fontByName23 = this.GetFontByName("FONT_SHAGLOUNGE45_BASE");
			((ImageFont)fontByName23).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName23).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName24 = this.GetFontByName("FONT_SHAGLOUNGE45_GAUNTLET");
			((ImageFont)fontByName24).PushLayerColor("Main", new Color(249, 245, 188, 255));
			((ImageFont)fontByName24).PushLayerColor("Stroke", new Color(88, 51, 159, 255));
			Font fontByName25 = this.GetFontByName("FONT_SHAGLOUNGE45_RED");
			((ImageFont)fontByName25).PushLayerColor("Main", new Color(183, 61, 47, 255));
			Font fontByName26 = this.GetFontByName("FONT_SHAGLOUNGE45_YELLOW");
			((ImageFont)fontByName26).PushLayerColor("Main", new Color(222, 180, 8, 255));
			this.StartLoadingComplete = true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005AA9 File Offset: 0x00003CA9
		public override void LostFocus()
		{
			if (this.mBoard != null && Board.gPauseOnLostFocus)
			{
				this.mBoard.Pause(true);
			}
			this.mMusic.Enable(false);
			this.SaveProfile();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public override void GotFocus()
		{
			this.DetectMusicSettings();
			if (this.mBoard != null && Board.gPauseOnLostFocus)
			{
				this.mBoard.Pause(false);
				this.mBoard.mNumPauseUpdatesToDo = Common._M(50);
				this.mBoard.MarkDirty();
			}
			this.ReportAppLaunchInfo(4);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005B2A File Offset: 0x00003D2A
		public override bool DebugKeyDown(int key)
		{
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005B30 File Offset: 0x00003D30
		public override void UpdateFrames()
		{
			this.mMusic.Update();
			this.mSoundPlayer.Update();
			base.UpdateFrames();
			this.TransitionFromLoadingScreen();
			if (this.mDialogMap.Count > 0)
			{
				if (this.mMainMenu != null && this.mMainMenu.mUserSelDlg != null)
				{
					this.mWidgetManager.PutBehind(this.mUnderDialogWidget, this.mMainMenu.mUserSelDlg);
				}
				else
				{
					this.mWidgetManager.PutBehind(this.mUnderDialogWidget, this.mDialogList.Last.Value);
				}
				if (this.mDialogObscurePct < 1f)
				{
					if (this.mBoard != null && this.mBoard.mDoingFirstTimeIntro)
					{
						this.mDialogObscurePct = Math.Min(Common._M(0.9f), this.mDialogObscurePct + Common._M1(0.06f));
					}
					else
					{
						this.mDialogObscurePct = Math.Min(1f, this.mDialogObscurePct + Common._M(0.06f));
					}
				}
			}
			else
			{
				if (this.mBoard != null && this.mBoard.mDoingFirstTimeIntro)
				{
					this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - Common._M(0.015f));
				}
				else
				{
					this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - Common._M(0.06f));
				}
				if (this.mDialogObscurePct == 0f && this.mUnderDialogWidget.mVisible)
				{
					this.mUnderDialogWidget.SetVisible(false);
				}
			}
			if (this.m_XLiveState == GameApp.EXLiveWaiting.E_Ready)
			{
				this.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005D88 File Offset: 0x00003F88
		public virtual void PlaySamplePan(int theSoundNum, int thePan, int min_time)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.pan = thePan;
			this.mSoundPlayer.Play(theSoundNum, soundAttribs);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005DAF File Offset: 0x00003FAF
		public virtual void PlaySamplePan(int theSoundNum, int thePan)
		{
			this.PlaySamplePan(theSoundNum, thePan, 5);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005DBA File Offset: 0x00003FBA
		public override void PlaySample(int theSoundNum, int min_time)
		{
			this.mSoundPlayer.Play(theSoundNum);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005DC8 File Offset: 0x00003FC8
		public override void PlaySample(int theSoundNum)
		{
			this.PlaySample(theSoundNum, 5);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public override void DialogButtonDepress(int dialog_id, int button_id)
		{
			if (dialog_id == 1)
			{
				if (this.mYesNoDialogDelegate != null)
				{
					this.mYesNoDialogDelegate(button_id);
					this.mDialog.Kill();
					if (this.mBoard != null)
					{
						this.mBoard.Pause(false, true);
					}
					if (Enumerable.Count<KeyValuePair<int, Dialog>>(this.mDialogMap) == 1)
					{
						this.mDialog.SetFocusWidgetToBoard();
					}
					this.mDialog.Kill();
					return;
				}
			}
			else if (dialog_id == 0)
			{
				((ZumaDialog)base.GetDialog(dialog_id)).Kill();
				if (this.mBoard != null)
				{
					this.mBoard.Pause(false, true);
				}
				if (this.mDialogCallBack != null)
				{
					this.mDialogCallBack();
					this.mDialogCallBack = null;
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005E84 File Offset: 0x00004084
		public override void SwitchScreenMode(bool wantWindowed, bool is3d, bool force)
		{
			base.SwitchScreenMode(wantWindowed, is3d, force);
			this.RegistryWriteBoolean("Is3D", is3d);
			if (this.mBoard != null)
			{
				this.mBoard.mNumPauseUpdatesToDo = Common._M(10);
				this.mBoard.MarkDirty();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005EC1 File Offset: 0x000040C1
		public override MusicInterface CreateMusicInterface()
		{
			if (this.mNoSoundNeeded)
			{
				return new MusicInterface();
			}
			return base.CreateMusicInterface();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005ED7 File Offset: 0x000040D7
		public override void HandleCmdLineParam(string theParamName, string theParamValue)
		{
			base.HandleCmdLineParam(theParamName, theParamValue);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005EE4 File Offset: 0x000040E4
		public override void AddDialog(int id, Dialog d)
		{
			GameApp.gAddingDlgID = id;
			base.AddDialog(id, d);
			GameApp.gAddingDlgID = -12345;
			if (id != 6)
			{
				foreach (Dialog dialog in this.mDialogList)
				{
					if (dialog != d)
					{
						DialogHideInfo dialogHideInfo = new DialogHideInfo();
						dialogHideInfo.mDialog = dialog;
						dialogHideInfo.mHideCount = 1;
						new KeyValuePair<int, DialogHideInfo>(dialog.mId, dialogHideInfo);
						DialogHideInfo dialogHideInfo2 = null;
						if (this.mDialogHideInfoMap != null)
						{
							if (this.mDialogHideInfoMap.TryGetValue(dialog.mId, out dialogHideInfo2))
							{
								dialogHideInfo2.mHideCount++;
							}
							else
							{
								this.mDialogHideInfoMap.Add(dialog.mId, dialogHideInfo);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005FB8 File Offset: 0x000041B8
		public override void AddDialog(Dialog theDialog)
		{
			base.AddDialog(theDialog);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005FC4 File Offset: 0x000041C4
		public override bool KillDialog(int id, bool removeWidget, bool deleteWidget)
		{
			if (id != GameApp.gAddingDlgID)
			{
				List<int> list = new List<int>();
				if (this.mDialogHideInfoMap != null)
				{
					foreach (KeyValuePair<int, DialogHideInfo> keyValuePair in this.mDialogHideInfoMap)
					{
						if (--keyValuePair.Value.mHideCount == 0)
						{
							list.Add(keyValuePair.Key);
						}
					}
					for (int i = 0; i < Enumerable.Count<int>(list); i++)
					{
						this.mDialogHideInfoMap.Remove(list[i]);
					}
				}
			}
			return base.KillDialog(id, removeWidget, deleteWidget);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006080 File Offset: 0x00004280
		public override bool KillDialog(int theDialogId)
		{
			return base.KillDialog(theDialogId);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006089 File Offset: 0x00004289
		public override bool KillDialog(Dialog theDialog)
		{
			return base.KillDialog(theDialog);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006092 File Offset: 0x00004292
		public void InitAudio()
		{
			this.mMusic = new Music(this.mMusicInterface);
			this.mMusic.RegisterCallBack();
			this.mSoundPlayer = new SoundEffects(this.mSoundManager);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000060C1 File Offset: 0x000042C1
		public bool MusicEnabled()
		{
			return !this.mMusicInterface.isPlayingUserMusic();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000060D4 File Offset: 0x000042D4
		public void DetectMusicSettings()
		{
			Dialog dialog = base.GetDialog(2);
			if (dialog != null)
			{
				((OptionsDialog)dialog).DetectMusicSettings();
				return;
			}
			this.mMusic.Enable(this.MusicEnabled() && this.GetMusicVolume() > 0.0);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006120 File Offset: 0x00004320
		public void TransitionFromLoadingScreen()
		{
			if (this.mLoadingScreen == null)
			{
				return;
			}
			if (this.mDelayIntro)
			{
				this.LoadBoard();
				return;
			}
			if (this.mLoadingScreen.CanShowMenu() && !this.TriggerFirstProfileDialog())
			{
				this.ShowMainMenu();
				this.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_SEAGULLS));
				this.mWidgetManager.BringToFront(this.mLoadingScreen);
				return;
			}
			if (this.mLoadingScreen.Done() && this.mNewUserDlg == null)
			{
				this.KillLoadingScreen();
				this.mSoundPlayer.Stop(this.GetSoundIDByName("SOUND_SEAGULLS"), true);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000061B9 File Offset: 0x000043B9
		public void LoadBoard()
		{
			this.mDelayIntro = false;
			if (this.mBoard != null)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
			}
			this.KillLoadingScreen();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000061E4 File Offset: 0x000043E4
		public void KillLoadingScreen()
		{
			if (this.mLoadingScreen == null)
			{
				return;
			}
			this.mWidgetManager.RemoveWidget(this.mLoadingScreen);
			this.mLoadingScreen.Dispose();
			this.mLoadingScreen = null;
			if (this.mResourceManager.IsGroupLoaded("LoadScreen"))
			{
				this.mResourceManager.DeleteResources("LoadScreen");
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000623F File Offset: 0x0000443F
		public bool TriggerFirstProfileDialog()
		{
			return false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006242 File Offset: 0x00004442
		public bool IsFirstGameLoad()
		{
			return this.mProfileMgr.GetNumProfiles() == 0U;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006252 File Offset: 0x00004452
		public bool IsFirstGameLoad(string name)
		{
			return !this.mProfileMgr.HasProfile(name);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006263 File Offset: 0x00004463
		public LevelMgr GetLevelMgr()
		{
			if (this.mUserProfile == null || this.mBoard == null)
			{
				return this.mNormalLevelMgr;
			}
			return this.mNormalLevelMgr;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006282 File Offset: 0x00004482
		public void ResetAllLevelMgrs()
		{
			this.mNormalLevelMgr.Reset();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006290 File Offset: 0x00004490
		public bool ReloadAllLevelMgrs()
		{
			LevelMgr[] array = new LevelMgr[]
			{
				this.mNormalLevelMgr
			};
			for (int i = 0; i < 1; i++)
			{
				if (!array[i].LoadLevels(array[i].mLevelXML))
				{
					this.Popup(array[i].GetErrorText());
					this.Popup("Your boss DDS parameters were all reset. You should quit and restart.");
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000062EC File Offset: 0x000044EC
		public void ShowMainMenu(bool do_load_thread)
		{
			this.mClickedHardMode = false;
			this.PlaySong(1);
			if (this.mInitialLoad)
			{
				if (!GameApp.gApp.mResourceManager.IsGroupLoaded("MenuRelated") && !this.mResourceManager.LoadResources("MenuRelated"))
				{
					this.mStartInGameModeThreadProcRunning = false;
					this.mInGameLoadThreadProcFailed = true;
					return;
				}
				this.mMainMenu = new MainMenu(this);
				this.mMainMenu.Init();
				this.mMainMenu.Resize(this.GetScreenRect());
				this.mWidgetManager.AddWidget(this.mMainMenu);
				this.mWidgetManager.SetFocus(this.mMainMenu);
				this.mLoadingScreen.mMouseVisible = false;
				this.mInitialLoad = false;
				this.CheckForAppUpdate();
			}
			else
			{
				if (this.mUserProfile != null)
				{
					this.mUserProfile.mDoChallengeTrophyZoom = (this.mUserProfile.mDoChallengeAceTrophyZoom = false);
					this.mUserProfile.mDoChallengeAceCupComplete = (this.mUserProfile.mDoChallengeCupComplete = false);
					this.mUserProfile.mUnlockSparklesIdx1 = (this.mUserProfile.mUnlockSparklesIdx2 = -1);
				}
				this.SetupMainMenuDefaults(do_load_thread);
			}
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("Map"))
			{
				this.mResourceManager.PrepareLoadResources("Map");
			}
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame"))
			{
				this.mResourceManager.PrepareLoadResources("CommonGame");
			}
			this.EnsureAdventureResourcesLoaded();
			if (this.mUserProfile == null)
			{
				ZumaProfile zumaProfile = (ZumaProfile)this.mProfileMgr.GetAnyProfile();
				string text = "";
				if (zumaProfile != null)
				{
					text = zumaProfile.GetName();
				}
				this.RegistryReadString("LastUser", ref text);
				if (text.Length > 0)
				{
					if (!GameApp.gInitialProfLoadSuccessful || !this.ChangeUser(text))
					{
						if (this.mProfileMgr.GetNumProfiles() > 0U)
						{
							zumaProfile = (ZumaProfile)this.mProfileMgr.GetAnyProfile();
							if (zumaProfile != null)
							{
								this.mUserProfile = zumaProfile;
								this.ChangeUser(zumaProfile.GetName());
							}
							this.mMainMenu.DoChangeUserDialog();
							this.ClearUpdateBacklog(false);
						}
						else
						{
							if (!GameApp.gInitialProfLoadSuccessful && !this.mCachedLoad)
							{
								this.DoGenericDialog("ERROR", "One or more of your saved game files is\nincompatible with this version of the game.\nThey have been deleted.", true, null, 0);
							}
							this.DoNewUserDialog();
						}
					}
					this.mMainMenu.MarkDirty();
				}
				else
				{
					this.DoNewUserDialog();
					this.mMainMenu.MarkDirty();
				}
				this.mMainMenu.RehupButtons();
			}
			if (this.mUserProfile != null && this.mMainMenu.mChallengeMenu != null)
			{
				this.mMainMenu.mChallengeMenu.InitCS();
			}
			this.mMainMenu.RehupButtons();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000657A File Offset: 0x0000477A
		public void ShowMainMenu()
		{
			this.ShowMainMenu(true);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006583 File Offset: 0x00004783
		public void HideChallengeMenu()
		{
			if (this.mMainMenu != null && this.mMainMenu.mChallengeMenu != null)
			{
				this.mMainMenu.HideChallengeMenu();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000065A8 File Offset: 0x000047A8
		public void HideMainMenu(bool delete_resources)
		{
			if (this.mMainMenu != null)
			{
				if (this.mMainMenu.mChallengeMenu != null)
				{
					this.mMainMenu.HideChallengeMenu();
				}
				this.mWidgetManager.RemoveWidget(this.mMainMenu);
				base.SafeDeleteWidget(this.mMainMenu);
				this.mMainMenu = null;
			}
			this.HideAdventureModeMapScreen();
			if (this.mResourceManager.IsGroupLoaded("MenuRelated"))
			{
				this.mResourceManager.DeleteResources("MenuRelated");
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006624 File Offset: 0x00004824
		public void ShowMoreGames()
		{
			this.mMoreGames = new MoreGames(this);
			this.mMoreGames.Init();
			this.mMoreGames.Resize(GameApp.gApp.GetScreenRect());
			this.mWidgetManager.AddWidget(this.mMoreGames);
			this.mMainMenu.DoMoreGamesSlide(false);
			this.mMoreGames.DoSlide(true);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006686 File Offset: 0x00004886
		public void HideMoreGames()
		{
			if (this.mMoreGames != null)
			{
				this.mMoreGames.DoSlide(false);
			}
			this.mMainMenu.DoMoreGamesSlide(true);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000066A8 File Offset: 0x000048A8
		public void DeleteMoreGames(bool delete_resources)
		{
			if (this.mMoreGames != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMoreGames);
				base.SafeDeleteWidget(this.mMoreGames);
				this.mMoreGames = null;
			}
			if (delete_resources)
			{
				this.mResourceManager.DeleteResources("MoreGames");
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000066F4 File Offset: 0x000048F4
		public void ShowIronFrog()
		{
			this.SetupMainMenuDefaults();
			this.mMainMenu.mChallengeMenu.InitCS();
			this.mMainMenu.RehupButtons();
			this.mMainMenu.DoIronFrog(false);
			this.PlaySong(1);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000672A File Offset: 0x0000492A
		public void ShowChallengeSelector()
		{
			this.SetupMainMenuDefaults();
			this.mMainMenu.ShowChallengeMenu();
			this.mMainMenu.mChallengeMenu.mCueMainSong = true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006750 File Offset: 0x00004950
		public void ShowAdventureModeMapScreen()
		{
			if (!this.EnsureAdventureResourcesLoaded())
			{
				this.CancelBambooTransition();
				return;
			}
			if (!this.mResourceManager.IsGroupLoaded("GamePlay"))
			{
				this.mResourceManager.PrepareLoadResources("GamePlay");
			}
			try
			{
				this.mMapScreen = new MapScreen();
				this.mMapScreenHackWidget = new MapScreenHackWidget();
				this.mMapScreen.mParent = this.mMapScreenHackWidget;
				this.mWidgetManager.AddWidget(this.mMapScreenHackWidget);
				this.mMapScreenHackWidget.Resize(0, 0, this.mWidth, this.mHeight);
				this.mMapScreen.Init(false, this.mUserProfile.GetAdvModeVars().mCurrentAdvZone, this.mUserProfile.GetAdvModeVars().mCurrentAdvLevel, false, true);
				this.mWidgetManager.SetFocus(this.mMapScreenHackWidget);
				this.mMapScreen.DoSlide(true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("ShowAdventureModeMapScreen failed: " + ex);
				this.ShowResourceError(false);
				this.RestoreMainMenuAfterAdventureFailure();
				return;
			}
			if (this.mMainMenu != null)
			{
				this.mMainMenu.HideScrollButtons();
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006860 File Offset: 0x00004A60
		public void HideAdventureModeMapScreen()
		{
			if (this.mMapScreenHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMapScreenHackWidget);
				base.SafeDeleteWidget(this.mMapScreenHackWidget);
				this.mMapScreenHackWidget = null;
			}
			if (this.mMapScreen != null)
			{
				this.mMapScreen.Dispose();
				this.mMapScreen = null;
			}
			if ((this.mUserProfile == null || !this.mUserProfile.mNeedsFirstTimeIntro) && this.mResourceManager.IsGroupLoaded("Map"))
			{
				this.mResourceManager.DeleteResources("Map");
			}
			if (this.mMainMenu != null)
			{
				this.mMainMenu.ShowScrollButtons();
				this.PlaySong(1);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000690C File Offset: 0x00004B0C
		public void StartAdventureMode()
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.PlaySong(12);
			this.mLoadType = ((this.mForceZoneRestart == -1) ? 0 : 1);
			if (this.IsHardMode())
			{
				this.mUserProfile.mFirstTimeReplayingHardMode = false;
			}
			else
			{
				this.mUserProfile.mFirstTimeReplayingNormalMode = false;
			}
			this.mStartInGameModeThreadProcRunning = true;
			this.StartAdvModeThreadProc();
			Rect aRect;
			if (this.mLoadType == 1)
			{
				int mX = this.mMapScreen.mCards[this.mMapScreen.mSelectedZone - 1].mX;
				int mY = this.mMapScreen.mCards[this.mMapScreen.mSelectedZone - 1].mY;
				Image imageByName = this.GetImageByName("IMAGE_UI_CHALLENGESCREEN_HOME_SELECT");
				aRect = new Rect(mX, mY, (int)(0.4f * (float)imageByName.mWidth), (int)(0.4f * (float)imageByName.mHeight));
			}
			else
			{
				aRect = new Rect(Common._DS(Common._M(624)), Common._DS(Common._M1(697)), Common._DS(Common._M2(700)), Common._DS(Common._M3(500)));
			}
			Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
			int num = (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? Common._DS(Common._M(160)) : 0;
			aRect.mWidth += num;
			this.DoCommonInGameLoadThread(aRect);
			this.mBoard.AdventureModeSetupComplete(this.mContinuedGame);
			this.HideMainMenu(true);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006A98 File Offset: 0x00004C98
		public void StartAdvModeFirstTime()
		{
			if (!this.EnsureAdventureResourcesLoaded())
			{
				if (this.mMainMenu != null)
				{
					this.mMainMenu.mFirstTimeAlpha = 0;
				}
				return;
			}
			this.mMusic.FadeOut();
			this.HideMainMenu(true);
			this.mBoard = new Board(this, -1);
			if (this.mLoadingScreen == null)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
			}
			this.mBoard.mAdventureMode = true;
			if (!this.mBoard.Init(true))
			{
				this.RestoreMainMenuAfterAdventureFailure();
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mContinuedGame = false;
			if (!this.mBoard.StartLevel(1))
			{
				this.RestoreMainMenuAfterAdventureFailure();
				return;
			}
			this.mBoard.MakeCachedBackground();
			this.mWidgetManager.SetFocus(this.mBoard);
			if (this.mWidescreenBoardWidget == null)
			{
				this.mWidescreenBoardWidget = new WidescreenBoardWidget();
				this.mWidescreenBoardWidget.Resize(Common._S(-80), 0, this.mWidth + Common._S(160), this.mHeight);
				this.mWidgetManager.AddWidget(this.mWidescreenBoardWidget);
			}
			this.mBoard.SetMenuBtnEnabled(false);
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006C19 File Offset: 0x00004E19
		public void DoDeferredEndGame()
		{
			if (this.mBoard != null)
			{
				this.mBoard.mNumDrawFramesLeft = Common._M(2);
				this.mBoard.mReturnToMainMenu = true;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006C40 File Offset: 0x00004E40
		public void EndCurrentGame()
		{
			this.mBoard.DoShutdownSaveGame();
			this.mBoard.mSkipShutdownSave = true;
			this.mWidgetManager.RemoveWidget(this.mBoard);
			base.SafeDeleteWidget(this.mBoard);
			this.mBoard = null;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006C80 File Offset: 0x00004E80
		public void StartGauntletMode(string normal_level_id, Rect thumb_rect)
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.mLoadType = 2;
			this.mChallengeLevelId = normal_level_id;
			this.mStartInGameModeThreadProcRunning = true;
			this.StartChallengeModeThreadProc();
			Rect aRect;
			aRect = new Rect(thumb_rect);
			Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
			int num = (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? Common._DS(Common._M(320)) : 0;
			aRect.mWidth += num;
			this.DoCommonInGameLoadThread(aRect);
			this.HideMainMenu(true);
			this.PlaySong(12);
			this.mBoard.GauntletModeSetupComplete();
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006D28 File Offset: 0x00004F28
		public void StartIronFrogMode()
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.mUserProfile.mIronFrogStats.mCurTime = 0;
			this.mLoadType = 3;
			this.mStartInGameModeThreadProcRunning = true;
			this.StartIronFrogModeThreadProc();
			int num = Common._DS(Common._M(700));
			int num2 = Common._DS(Common._M(650));
			this.DoCommonInGameLoadThread(new Rect((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2));
			this.HideMainMenu(true);
			this.PlaySong(12);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public void PlaySong(int song, float fade_speed)
		{
			bool inLoop = true;
			if (song != 0)
			{
				switch (song)
				{
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
					goto IL_37;
				case 126:
					break;
				default:
					if (song != 137)
					{
						goto IL_3D;
					}
					break;
				}
				inLoop = false;
				goto IL_3D;
			}
			IL_37:
			inLoop = false;
			IL_3D:
			this.mMusic.PlaySongNoDelay(song, inLoop);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006E0B File Offset: 0x0000500B
		public void PlaySong(int song)
		{
			this.PlaySong(song, 0.005f);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006E1C File Offset: 0x0000501C
		public void DoOptionsDialog(bool ingame)
		{
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			OptionsDialog optionsDialog = new OptionsDialog(ingame);
			Common.SetupDialog(optionsDialog);
			this.AddDialog(optionsDialog);
			if (ingame)
			{
				optionsDialog.Move(optionsDialog.mX, optionsDialog.mY + Common._S(30));
			}
		}

		public void ShowDebugDialog()
		{
			try
			{
				if (this.mDebugDialog == null)
				{
					this.mDebugDialog = new DebugDialog();
					this.mDebugDialog.Resize(0, 0, this.mWidth, this.mHeight);
				}
				this.mDebugDialog.RefreshInfo();
				if (!this.mWidgetManager.HasWidget(this.mDebugDialog))
				{
					this.mWidgetManager.AddWidget(this.mDebugDialog);
				}
				this.mWidgetManager.BringToFront(this.mDebugDialog);
				this.mWidgetManager.SetFocus(this.mDebugDialog);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("ShowDebugDialog failed: " + ex);
				WP7AppDriver driver = this.mAppDriver as WP7AppDriver;
				if (driver != null)
				{
					driver.Popup("DEBUG error: " + ex.Message);
				}
			}
		}

		public void HideDebugDialog()
		{
			if (this.mDebugDialog == null)
			{
				return;
			}
			this.mWidgetManager.RemoveWidget(this.mDebugDialog);
			this.mDebugOverlayText = "";
		}

		public void ApplyDesktopDisplay(int width, int height, bool windowed)
		{
			WP7AppDriver wp7AppDriver = this.mAppDriver as WP7AppDriver;
			if (wp7AppDriver == null || wp7AppDriver.mXNAGraphicsDriver == null)
			{
				return;
			}
			BaseXNARenderDevice renderDevice = wp7AppDriver.mXNAGraphicsDriver.mXNARenderDevice;
			renderDevice.SetBackBufferSize(width, height, !windowed);
			renderDevice.EnsureDeviceChanges();
			renderDevice.ApplyLetterboxViewport(this.mWidth, this.mHeight);
			this.mPreferredWidth = width;
			this.mPreferredHeight = height;
			this.mIsWindowed = windowed;
			this.mIsPhysWindowed = windowed;
			this.SyncMouseRectsFromViewport();
			this.mWidgetManager.MarkAllDirty();
			this.UpdateDebugOverlayText();
		}

		public void SyncMouseRectsFromViewport()
		{
			WP7AppDriver wp7AppDriver = this.mAppDriver as WP7AppDriver;
			if (wp7AppDriver == null || wp7AppDriver.mXNAGraphicsDriver == null)
			{
				return;
			}
			Viewport viewport = wp7AppDriver.mXNAGraphicsDriver.mXNARenderDevice.mDevice.GraphicsDevice.Viewport;
			Rect mouseDest = new Rect(0, 0, this.mWidth, this.mHeight);
			Rect mouseSource = new Rect(viewport.X, viewport.Y, viewport.Width, viewport.Height);
			this.mWidgetManager.Resize(mouseDest, mouseSource);
		}

		public void ApplySavedDesktopDisplay()
		{
			int width;
			int height;
			DesktopDisplay.ResolvePreset(this.mDesktopResolutionPreset, this.mDesktopFullscreen, out width, out height);
			this.ApplyDesktopDisplay(width, height, !this.mDesktopFullscreen);
		}

		public string GetDesktopResolutionLabel()
		{
			int idx = this.mDesktopResolutionPreset;
			if (idx < 0 || idx >= DesktopDisplay.ResolutionLabels.Length)
			{
				idx = 0;
			}
			return DesktopDisplay.ResolutionLabels[idx];
		}

		public void CycleDesktopResolutionPreset()
		{
			this.mDesktopResolutionPreset++;
			if (this.mDesktopResolutionPreset >= DesktopDisplay.ResolutionPresets.Length)
			{
				this.mDesktopResolutionPreset = 0;
			}
		}

		public void UpdateDebugOverlayText()
		{
			if (!this.mShowFPS && !this.mShowDebugResourceList)
			{
				this.mDebugOverlayText = "";
				return;
			}
			WP7AppDriver wp7AppDriver = this.mAppDriver as WP7AppDriver;
			int backW = this.mWidth;
			int backH = this.mHeight;
			bool fullscreen = false;
			if (wp7AppDriver != null && wp7AppDriver.mXNAGraphicsDriver != null && wp7AppDriver.mXNAGraphicsDriver.mXNARenderDevice != null)
			{
				GraphicsDeviceManager device = wp7AppDriver.mXNAGraphicsDriver.mXNARenderDevice.mDevice;
				backW = device.PreferredBackBufferWidth;
				backH = device.PreferredBackBufferHeight;
				fullscreen = device.IsFullScreen;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Back buffer: ");
			stringBuilder.Append(backW);
			stringBuilder.Append("x");
			stringBuilder.Append(backH);
			stringBuilder.Append(fullscreen ? " fullscreen" : " windowed");
			stringBuilder.Append(" | Game: ");
			stringBuilder.Append(this.mWidth);
			stringBuilder.Append("x");
			stringBuilder.Append(this.mHeight);
			stringBuilder.Append(" | Mem: ");
			stringBuilder.Append(GC.GetTotalMemory(false) / 1048576L);
			stringBuilder.Append(" MB");
			if (this.mShowDebugResourceList && this.mResourceManager != null)
			{
				stringBuilder.Append(" | Groups: ");
				stringBuilder.Append(this.mResourceManager.mLoadedGroups.Count);
				string[] resNames = new string[]
				{
					"Images",
					"Sounds",
					"Fonts",
					"PopAnims",
					"PIEffects",
					"RenderFX",
					"Generic"
				};
				for (int i = 0; i < this.mResourceManager.mResMaps.Length && i < resNames.Length; i++)
				{
					if (this.mResourceManager.mResMaps[i] != null)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(resNames[i]);
						stringBuilder.Append("=");
						stringBuilder.Append(this.mResourceManager.mResMaps[i].Count);
					}
				}
			}
			this.mDebugOverlayText = stringBuilder.ToString();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006E70 File Offset: 0x00005070
		public void FinishOptionsDialog(bool doSave)
		{
			OptionsDialog optionsDialog = base.GetDialog(2) as OptionsDialog;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			bool mIsWindowed = this.mIsWindowed;
			this.Is3DAccelerated();
			if (flag3)
			{
				flag2 = true;
			}
			bool flag4 = false;
			this.EnableCustomCursors(false);
			this.mCursorTarget = false;
			this.RegistryWriteBoolean("Z2Cursor", this.mCursorTarget);
			if (doSave)
			{
				this.mColorblind = optionsDialog.mColorBlindSlider.IsOn();
				if (optionsDialog.mFpsToggle != null)
				{
					this.mShowFPS = optionsDialog.mFpsToggle.IsOn();
				}
				if (optionsDialog.mFullscreenToggle != null)
				{
					this.mDesktopFullscreen = optionsDialog.mFullscreenToggle.IsOn();
				}
				this.SaveGlobalConfig();
			}
			if (flag4)
			{
				this.RegistryWriteBoolean("PreHiRes", this.mHiRes);
				this.RegistryWriteBoolean("Pre3D", this.Is3DAccelerated());
				this.RegistryWriteBoolean("PreWindowed", this.mIsWindowed);
				this.RegistryWriteBoolean("NeedsConfirmation", true);
				this.mPreferredWidth = (this.mPreferredHeight = -1);
				this.RegistryWriteBoolean("HiRes", true);
				this.mReInit = true;
				this.Shutdown();
				if (!flag2)
				{
					this.RegistryWriteBoolean("Is3D", false);
				}
				else
				{
					this.RegistryEraseValue("Is3D");
				}
			}
			else
			{
				this.SwitchScreenMode(flag, flag2, true);
				this.ClearUpdateBacklog(false);
			}
			optionsDialog.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			optionsDialog.mWidgetFlagsMod.mRemoveFlags |= 16;
			optionsDialog.Kill();
			if (this.mBoard != null)
			{
				this.mBoard.Pause(false, true);
				if (this.mBoard.mMenuButton != null)
				{
					this.mBoard.mMenuButton.mDisabled = false;
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006FE7 File Offset: 0x000051E7
		public int DoQuitPromptDialog()
		{
			return this.DoYesNoDialog(TextManager.getInstance().getString(448), TextManager.getInstance().getString(453), true);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000700E File Offset: 0x0000520E
		public void TakeScreenshot(string prefix)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007010 File Offset: 0x00005210
		public static SharedImageRef CompositionLoadFunc(string file_dir, string file_name)
		{
			int num = file_name.IndexOf('\\');
			string text = "";
			string text2 = "";
			if (num != -1)
			{
				text = file_name.Substring(0, num);
				text2 = file_name.Substring(num + 1);
			}
			string text3;
			string text4;
			if (text.Length == 0)
			{
				text3 = Common.PathToResName(file_dir, "images", "IMAGE") + GameApp.mCompositionResPrefix + "_" + Common.StripFileExtension(file_name).ToUpper();
				text4 = Common.PathToResName(file_dir, "images", "IMAGE") + "_" + Common.StripFileExtension(file_name).ToUpper();
			}
			else
			{
				text3 = Common.PathToResName(file_dir, "images", "IMAGE") + GameApp.mCompositionResPrefix + "_" + (text + "_" + text2).ToUpper();
				text4 = Common.PathToResName(file_dir, "images", "IMAGE") + "_" + (text + "_" + text2).ToUpper();
			}
			text3 = text3.Replace(' ', '_');
			text3 = text3.Replace('-', '_');
			text4 = text4.Replace(' ', '_');
			text4 = text4.Replace('-', '_');
			SharedImageRef sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(text3);
			if (sharedImageRef == null || (sharedImageRef != null && sharedImageRef.GetImage() == null))
			{
				sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(text4);
				sharedImageRef.mSharedImage.mImage.mFilePath = text4;
			}
			else
			{
				sharedImageRef.mSharedImage.mImage.mFilePath = text3;
			}
			return sharedImageRef;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007194 File Offset: 0x00005394
		public static void CompositionPostLoadFunc(SharedImageRef img, Layer l)
		{
			l.mXOff = Common._DS(GameApp.gApp.mResourceManager.GetImageOffset(l.GetImage().mFilePath).mX);
			l.mYOff = Common._DS(GameApp.gApp.mResourceManager.GetImageOffset(l.GetImage().mFilePath).mY);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000071F5 File Offset: 0x000053F5
		public bool ChangeUser(string user_name)
		{
			return true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000071F8 File Offset: 0x000053F8
		public bool DeleteUser(string user_name)
		{
			return true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000071FB File Offset: 0x000053FB
		public bool ShadersSupported()
		{
			return true;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000071FE File Offset: 0x000053FE
		public void DoNewUserDialog(int button_mode, bool isIntro)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007200 File Offset: 0x00005400
		public void DoNewUserDialog()
		{
			this.DoNewUserDialog(3, false);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000720A File Offset: 0x0000540A
		public void DoNewUserDialog(int button_mode)
		{
			this.DoNewUserDialog(button_mode, false);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007214 File Offset: 0x00005414
		public Rect GetNewUserDialogFrame()
		{
			return Rect.ZERO_RECT;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000721B File Offset: 0x0000541B
		public void BlankNameEntered()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000721D File Offset: 0x0000541D
		public void NameIsAllSpaces()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000721F File Offset: 0x0000541F
		public void FinishedNewUser(bool canceled)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007224 File Offset: 0x00005424
		public void DoGenericDialog(string header, string message, bool block, GameApp.PreBlockCallback pre_block_callback, int width_pad)
		{
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			ZumaDialog zumaDialog = new ZumaDialog(0, true, "", message, TextManager.getInstance().getString(483), 3);
			zumaDialog.mSpaceAfterHeader = 0;
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			zumaDialog.mContentInsets.mTop += Common._S(Common._M(30));
			int num;
			int num2;
			Common.StringDimensions(message, fontByName, out num, out num2);
			zumaDialog.mAllowDrag = false;
			zumaDialog.GetSize(ref num, ref num2);
			num += width_pad;
			zumaDialog.Resize((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2);
			Common.SetupDialog(zumaDialog);
			this.AddDialog(zumaDialog);
			this.mDialogCallBack = pre_block_callback;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000072EC File Offset: 0x000054EC
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space, int id)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, header_space, id, 0);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007310 File Offset: 0x00005510
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, header_space, 1, 0);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007330 File Offset: 0x00005530
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, -1, 1, 0);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007350 File Offset: 0x00005550
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, true, -1, 1, 0);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007370 File Offset: 0x00005570
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000739C File Offset: 0x0000559C
		public int DoYesNoDialog(string header, string message, bool block)
		{
			return this.DoYesNoDialog(header, message, block, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000073D4 File Offset: 0x000055D4
		public int DoYesNoDialog(string header, string message)
		{
			return this.DoYesNoDialog(header, message, false, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000740C File Offset: 0x0000560C
		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space, int id, int width_pad)
		{
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			this.mDialog = new ZumaDialog(id, true, "", message, "", 1);
			this.mDialog.mSpaceAfterHeader = 0;
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			this.mDialog.mContentInsets.mTop += Common._S(Common._M(30));
			int num;
			int num2;
			Common.StringDimensions(message, fontByName, out num, out num2);
			this.mDialog.mAllowDrag = false;
			this.mDialog.GetSize(ref num, ref num2);
			num += width_pad;
			this.mDialog.Resize((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2);
			this.mDialog.mYesButton.mLabel = btn_yes;
			this.mDialog.mNoButton.mLabel = btn_no;
			this.mDialog.mAllowDrag = false;
			Common.SetupDialog(this.mDialog);
			this.AddDialog(this.mDialog);
			this.mWidgetManager.SetFocus(this.mDialog);
			if (block)
			{
				return this.mDialog.WaitForResult(false);
			}
			return -1;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000753A File Offset: 0x0000573A
		public void EndYesNoDialog(int ButtonId)
		{
			if (this.mYesNoDialogDelegate != null)
			{
				this.mYesNoDialogDelegate(ButtonId);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007550 File Offset: 0x00005750
		public int GetPan(int thePos)
		{
			return 3000 * (thePos - 400) / 400;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007568 File Offset: 0x00005768
		public CompositionMgr LoadComposition(string file_name, string res_prefix)
		{
			string text = SexyLocale.StringToUpper(file_name);
			if (this.mPreloadedComps.ContainsKey(text) && this.mPreloadedComps[text].isValid())
			{
				return new CompositionMgr(this.mPreloadedComps[text]);
			}
			CompositionMgr compositionMgr = new CompositionMgr();
			compositionMgr.mLoadImageFunc = new AECommon.LoadCompImageFunc(GameApp.CompositionLoadFunc);
			compositionMgr.mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(GameApp.CompositionPostLoadFunc);
			GameApp.mCompositionResPrefix = res_prefix;
			bool flag = compositionMgr.LoadFromFile(file_name);
			GameApp.mCompositionResPrefix = "";
			if (!flag)
			{
				compositionMgr = null;
			}
			this.mPreloadedComps[text] = compositionMgr;
			return new CompositionMgr(compositionMgr);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000760C File Offset: 0x0000580C
		public PIEffect GetPIEffect(string file_name, bool create_copy)
		{
			if (this.mLoadingThreadCompleted)
			{
				if (file_name == "TorchFlame")
				{
					for (int i = 0; i < Common.size<CachedTorchEffect>(this.mCachedTorchEffects); i++)
					{
						CachedTorchEffect cachedTorchEffect = this.mCachedTorchEffects[i];
						if (!cachedTorchEffect.mFlameInUse)
						{
							cachedTorchEffect.mFlameInUse = true;
							cachedTorchEffect.mTorchFlame.ResetAnim();
							return cachedTorchEffect.mTorchFlame;
						}
					}
				}
				else if (file_name == "TorchFlameOut")
				{
					for (int j = 0; j < Common.size<CachedTorchEffect>(this.mCachedTorchEffects); j++)
					{
						CachedTorchEffect cachedTorchEffect2 = this.mCachedTorchEffects[j];
						if (!cachedTorchEffect2.mFlameOutInUse)
						{
							cachedTorchEffect2.mFlameOutInUse = true;
							cachedTorchEffect2.mTorchFlameOut.ResetAnim();
							return cachedTorchEffect2.mTorchFlameOut;
						}
					}
				}
				else if (file_name == "Devil Projectile")
				{
					for (int k = 0; k < Common.size<CachedVolcanoEffect>(this.mCachedVolcanoEffects); k++)
					{
						CachedVolcanoEffect cachedVolcanoEffect = this.mCachedVolcanoEffects[k];
						if (!cachedVolcanoEffect.mProjectileInUse)
						{
							cachedVolcanoEffect.mProjectileInUse = true;
							cachedVolcanoEffect.mProjectile.ResetAnim();
							cachedVolcanoEffect.mProjectile.mEmitAfterTimeline = true;
							return cachedVolcanoEffect.mProjectile;
						}
					}
				}
				else if (file_name == "Devil Explosion")
				{
					for (int l = 0; l < Common.size<CachedVolcanoEffect>(this.mCachedVolcanoEffects); l++)
					{
						CachedVolcanoEffect cachedVolcanoEffect2 = this.mCachedVolcanoEffects[l];
						if (!cachedVolcanoEffect2.mExplosionInUse)
						{
							cachedVolcanoEffect2.mExplosionInUse = true;
							cachedVolcanoEffect2.mExplosion.ResetAnim();
							return cachedVolcanoEffect2.mExplosion;
						}
					}
				}
			}
			if (this.mCachedPIEffects.ContainsKey(file_name))
			{
				for (int m = 0; m < this.mCachedPIEffects[file_name].Count; m++)
				{
					GenericCachedEffect genericCachedEffect = this.mCachedPIEffects[file_name][m];
					if (!genericCachedEffect.mInUse)
					{
						genericCachedEffect.mInUse = true;
						genericCachedEffect.mEffect.ResetAnim();
						return genericCachedEffect.mEffect;
					}
				}
			}
			string text = string.Concat(new string[]
			{
				this.GetBaseResImagesDir(),
				"particles\\",
				file_name,
				"\\",
				file_name,
				".ppf"
			});
			PIEffect pieffect = new PIEffect();
			PIEffect pieffect2 = new PIEffect();
			if (!pieffect2.LoadEffect(text))
			{
				return null;
			}
			pieffect = pieffect2;
			if (!create_copy)
			{
				return pieffect;
			}
			return pieffect.Duplicate();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000786D File Offset: 0x00005A6D
		public PIEffect GetPIEffect(string file_name)
		{
			return this.GetPIEffect(file_name, true);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007877 File Offset: 0x00005A77
		public bool IsHardMode()
		{
			return false;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000787A File Offset: 0x00005A7A
		public MemoryImage GenerateLevelThumbnail(string thumb_path, Level l)
		{
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000787D File Offset: 0x00005A7D
		public bool IronFrogUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[5] > 0;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000789E File Offset: 0x00005A9E
		public bool ChallengeModeUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mChallengeUnlockState[0, 0] > 0;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000078BF File Offset: 0x00005ABF
		public bool HSScreenUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[0] >= 1;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000078E4 File Offset: 0x00005AE4
		public void ReleaseTorchEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			for (int i = 0; i < Common.size<CachedTorchEffect>(this.mCachedTorchEffects); i++)
			{
				CachedTorchEffect cachedTorchEffect = this.mCachedTorchEffects[i];
				if (cachedTorchEffect.mTorchFlame == fx)
				{
					cachedTorchEffect.mFlameInUse = false;
					return;
				}
				if (cachedTorchEffect.mTorchFlameOut == fx)
				{
					cachedTorchEffect.mFlameOutInUse = false;
					return;
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000793C File Offset: 0x00005B3C
		public void ReleaseVolcanoEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			for (int i = 0; i < Common.size<CachedVolcanoEffect>(this.mCachedVolcanoEffects); i++)
			{
				CachedVolcanoEffect cachedVolcanoEffect = this.mCachedVolcanoEffects[i];
				if (cachedVolcanoEffect.mProjectile == fx)
				{
					cachedVolcanoEffect.mProjectileInUse = false;
					return;
				}
				if (cachedVolcanoEffect.mExplosion == fx)
				{
					cachedVolcanoEffect.mExplosionInUse = false;
					return;
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007994 File Offset: 0x00005B94
		public void ReleaseGenericCachedEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			foreach (KeyValuePair<string, List<GenericCachedEffect>> keyValuePair in this.mCachedPIEffects)
			{
				foreach (GenericCachedEffect genericCachedEffect in keyValuePair.Value)
				{
					if (genericCachedEffect.mEffect == fx)
					{
						genericCachedEffect.mInUse = false;
						break;
					}
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007A18 File Offset: 0x00005C18
		public Board GetBoard()
		{
			return this.mBoard;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007A20 File Offset: 0x00005C20
		public bool ShowingLoadingScreen()
		{
			return this.mLoadingScreen != null;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007A2E File Offset: 0x00005C2E
		public void IncFramesPlayed()
		{
			this.mFramesPlayed++;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007A3E File Offset: 0x00005C3E
		public string GetResImagesDir()
		{
			return string.Format("images\\{0}\\", GameApp.mGameRes);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007A54 File Offset: 0x00005C54
		public string GetBaseResImagesDir()
		{
			return string.Format("images\\{0}\\", this.mResourceManager.mBaseArtRes);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007A70 File Offset: 0x00005C70
		public static int ScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameUpScale) + theAdd;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007A7D File Offset: 0x00005C7D
		public static int ScaleNum(int theNum)
		{
			return GameApp.ScaleNum(theNum, 0);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007A86 File Offset: 0x00005C86
		public static float ScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameUpScale + theAdd;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007A91 File Offset: 0x00005C91
		public static float ScaleNum(float theNum)
		{
			return GameApp.ScaleNum(theNum, 0f);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007A9E File Offset: 0x00005C9E
		public static double ScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameUpScale + theAdd;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007AAA File Offset: 0x00005CAA
		public static double ScaleNum(double theNum)
		{
			return GameApp.ScaleNum(theNum, 0.0);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007ABB File Offset: 0x00005CBB
		public static int DownScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameDownScale) + theAdd;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public static int DownScaleNum(int theNum)
		{
			return GameApp.DownScaleNum(theNum, 0);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007AD1 File Offset: 0x00005CD1
		public static float DownScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameDownScale + theAdd;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007ADC File Offset: 0x00005CDC
		public static float DownScaleNum(float theNum)
		{
			return GameApp.DownScaleNum(theNum, 0f);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007AE9 File Offset: 0x00005CE9
		public static double DownScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameDownScale + theAdd;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007AF5 File Offset: 0x00005CF5
		public static double DownScaleNum(double theNum)
		{
			return GameApp.DownScaleNum(theNum, 0.0);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007B06 File Offset: 0x00005D06
		public static int ScreenScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameScreenScale) + theAdd;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007B13 File Offset: 0x00005D13
		public static int ScreenScaleNum(int theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007B1C File Offset: 0x00005D1C
		public static float ScreenScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameScreenScale + theAdd;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007B27 File Offset: 0x00005D27
		public static float ScreenScaleNum(float theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0f);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007B34 File Offset: 0x00005D34
		public static double ScreenScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameScreenScale + theAdd;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007B40 File Offset: 0x00005D40
		public static double ScreenScaleNum(double theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0.0);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007B51 File Offset: 0x00005D51
		public virtual uint GetProfileVersion()
		{
			return 0U;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007B54 File Offset: 0x00005D54
		public virtual void NotifyProfileChanged(UserProfile player)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007B56 File Offset: 0x00005D56
		public virtual UserProfile CreateUserProfile()
		{
			return new ZumaProfile();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007B5D File Offset: 0x00005D5D
		public virtual void OnProfileLoad(UserProfile player, Buffer buffer)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007B5F File Offset: 0x00005D5F
		public virtual void OnProfileSave(UserProfile player, Buffer buffer)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007B61 File Offset: 0x00005D61
		public Rect GetScreenRect()
		{
			return this.mWidgetManager.mMouseDestRect;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007B6E File Offset: 0x00005D6E
		public int GetScreenWidth()
		{
			return this.mWidgetManager.mMouseDestRect.mWidth - this.mWidgetManager.mMouseDestRect.mX;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007B91 File Offset: 0x00005D91
		public static bool IsTablet()
		{
			return true;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007B94 File Offset: 0x00005D94
		public Image GetLevelThumbnail(int theLevelNum)
		{
			Image second = this.mLevelThumbnails[theLevelNum].second;
			string[] array = new string[]
			{
				"jungle",
				"village",
				"city",
				"coast",
				"grotto",
				"volcano"
			};
			if (second == null)
			{
				int num = theLevelNum / 10;
				int num2 = theLevelNum % 10 + 1;
				string text = array[num] + string.Format("{0}", num2);
				string text2 = "levelthumbs\\" + text + "_thumb";
				IdxThumbPair idxThumbPair = this.mLevelThumbnails[theLevelNum];
				idxThumbPair.second = this.GetImage(text2, true, true, false);
				if (idxThumbPair.second != null)
				{
					second = idxThumbPair.second;
				}
			}
			return second;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007C64 File Offset: 0x00005E64
		public void DeleteLevelThumbnail(int theLevel)
		{
			if (theLevel >= 0 && theLevel <= Enumerable.Count<IdxThumbPair>(this.mLevelThumbnails))
			{
				IdxThumbPair idxThumbPair = this.mLevelThumbnails[theLevel];
				if (idxThumbPair.second != null)
				{
					idxThumbPair.second.Dispose();
					idxThumbPair.second = null;
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007CAC File Offset: 0x00005EAC
		public void DeleteZoneThumbnails(int theZone)
		{
			if (theZone >= 0 && theZone <= 6)
			{
				int num = theZone * 10;
				for (int i = 0; i < 10; i++)
				{
					IdxThumbPair idxThumbPair = this.mLevelThumbnails[num + i];
					if (idxThumbPair.second != null)
					{
						idxThumbPair.second.Dispose();
						idxThumbPair.second = null;
					}
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007CFC File Offset: 0x00005EFC
		public void LoadAllThumbnails()
		{
			for (int i = 0; i < 6; i++)
			{
				int num = i * 10;
				for (int j = 0; j < 10; j++)
				{
					this.GetLevelThumbnail(num + j);
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007D31 File Offset: 0x00005F31
		public void AppEnteredBackground()
		{
			if (this.mBoard != null && this.mBoard.NeedSaveGame() && this.mUserProfile != null)
			{
				this.mBoard.SaveGame(this.mUserProfile.GetSaveGameName(this.IsHardMode()), null);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007D6D File Offset: 0x00005F6D
		public override double GetLoadingThreadProgress()
		{
			return (double)this.mResourceManager.GetLoadResourcesListProgress(GameApp.gInitialLoadGroups);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007D80 File Offset: 0x00005F80
		public void ToggleBambooTransition()
		{
			if (this.mBambooTransition == null)
			{
				return;
			}
			if (!this.mBambooTransition.IsInProgress())
			{
				this.mBambooTransition.Reset();
				this.mBambooTransition.SetVisible(true);
				this.mBambooTransition.SetDisabled(false);
				this.mWidgetManager.AddWidget(this.mBambooTransition);
				this.mWidgetManager.BringToFront(this.mBambooTransition);
				this.mBambooTransition.StartTransition();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007DF3 File Offset: 0x00005FF3
		public void BambooTransitionOpened()
		{
			this.mBambooTransition.Reset();
			this.mBambooTransition.SetVisible(false);
			this.mBambooTransition.SetDisabled(true);
			this.mWidgetManager.RemoveWidget(this.mBambooTransition);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007E29 File Offset: 0x00006029
		public void EndChallengeModeGame()
		{
			this.EndCurrentGame();
			this.ShowChallengeSelector();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007E37 File Offset: 0x00006037
		public void InitMetricsManager()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007E3C File Offset: 0x0000603C
		public void HideHelp()
		{
			OptionsDialog optionsDialog = base.GetDialog(2) as OptionsDialog;
			if (optionsDialog != null)
			{
				optionsDialog.OnHelpHided();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007E5F File Offset: 0x0000605F
		public void ShowAbout()
		{
			this.mAboutInfo = new AboutInfo();
			this.AddDialog(this.mAboutInfo);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007E78 File Offset: 0x00006078
		public void HideAbout()
		{
			this.mAboutInfo.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mAboutInfo.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mAboutInfo = null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007EB4 File Offset: 0x000060B4
		public void ShowLegal()
		{
			this.mLegalInfo = new LegalInfo();
			this.AddDialog(this.mLegalInfo);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007ECD File Offset: 0x000060CD
		public void HideLegal()
		{
			this.mLegalInfo.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mLegalInfo.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mLegalInfo = null;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007F09 File Offset: 0x00006109
		public void ShowMetricsDebug()
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007F0B File Offset: 0x0000610B
		public void HideMetricsDebug()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007F0D File Offset: 0x0000610D
		public void ReportAppLaunchInfo(int theAppEvent)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007F0F File Offset: 0x0000610F
		public void ReportEndOfLevelMetrics(Board theBoard, bool theLevelSuccess, bool theAcedLevel)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007F11 File Offset: 0x00006111
		public void ReportEndOfLevelMetrics(Board theBoard, bool theLevelSuccess)
		{
			this.ReportEndOfLevelMetrics(theBoard, theLevelSuccess, false);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007F1C File Offset: 0x0000611C
		public void ReportEndOfLevelMetrics(Board theBoard)
		{
			this.ReportEndOfLevelMetrics(theBoard, false, false);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007F27 File Offset: 0x00006127
		public void CheckForAppUpdate()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007F29 File Offset: 0x00006129
		public void GetTouchInputOffset(ref int x, ref int y)
		{
			x = this.mTouchOffsetX;
			y = this.mTouchOffsetY;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007F3B File Offset: 0x0000613B
		public void SetTouchInputOffset(int x, int y)
		{
			this.mTouchOffsetX = x;
			this.mTouchOffsetY = y;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007F4B File Offset: 0x0000614B
		public void LoadLevelXML()
		{
			this.mLoadingProc = new ThreadStart(this.LoadingLevel);
			this.mLoadingThread = new Thread(this.mLoadingProc);
			this.mLoadLevelSuccess = false;
			this.mLoadingThread.Start();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007F84 File Offset: 0x00006184
		private void LoadingLevel()
		{
			try
			{
				ContentReaderRegistration.RegisterAll();
				this.mNormalLevelMgr = ((XNAFileDriver)this.mFileDriver).GetContentManager().Load<LevelMgr>(this.mLevelXML);
				this.mNormalLevelMgr.Init();
				this.mNormalLevelMgr.mLevelXML = this.mLevelXML;
				this.mLoadLevelSuccess = true;
			}
			catch (Exception ex)
			{
				this.mLoadLevelSuccess = false;
				StartupError.Log("LoadingLevel failed (" + this.mLevelXML + "): " + ex);
				Debug.WriteLine("LoadingLevel failed: " + ex);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007FD8 File Offset: 0x000061D8
		public void OpenURL(string url)
		{
			try
			{
				new WebBrowserTask
				{
					Uri = new Uri(url)
				}.Show();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008014 File Offset: 0x00006214
		public void HandleGameUpdateRequired(GameUpdateRequiredException ex)
		{
			GameApp.UN_UPDATE_VERSION = true;
			GameApp.USE_XBOX_SERVICE = false;
			GameApp.mDisplayTitleUpdateMessage = true;
			GameApp.DisplayTitleUpdateMessage();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008030 File Offset: 0x00006230
		public static void DisplayTitleUpdateMessage()
		{
			List<string> list = new List<string>();
			string @string = TextManager.getInstance().getString(446);
			string string2 = TextManager.getInstance().getString(447);
			list.Add(string2);
			list.Add(@string);
			if (GameApp.mDisplayTitleUpdateMessage && !Guide.IsVisible)
			{
				GameApp.mDisplayTitleUpdateMessage = false;
				string string3 = TextManager.getInstance().getString(62);
				Guide.BeginShowMessageBox("   ", string3, list, 1, 3, new AsyncCallback(GameApp.UpdateDialogGetMBResult), null);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000080B0 File Offset: 0x000062B0
		public static void UpdateDialogGetMBResult(IAsyncResult userResult)
		{
			int? num = Guide.EndShowMessageBox(userResult);
			if (num != null && num.Value > 0)
			{
				if (Guide.IsTrialMode)
				{
					Guide.ShowMarketplace(0);
					return;
				}
				new MarketplaceDetailTask
				{
					ContentType = 1,
					ContentIdentifier = "43f34364-9df4-4d95-b9cf-e48b3c85cda9"
				}.Show();
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008104 File Offset: 0x00006304
		public void ToMarketPlace()
		{
			if (Guide.IsTrialMode)
			{
				Guide.ShowMarketplace(0);
				return;
			}
			new MarketplaceDetailTask
			{
				ContentType = 1
			}.Show();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008134 File Offset: 0x00006334
		public static void initResolution(int param1)
		{
			GameApp.mGameRes = param1;
			int num = GameApp.mGameRes;
			if (num <= 640)
			{
				if (num == 320)
				{
					GameApp.mGameUpScale = 0.5333334f;
					GameApp.mGameDownScale = 0.2666667f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
				if (num == 600)
				{
					GameApp.mGameUpScale = 1f;
					GameApp.mGameDownScale = 0.5f;
					GameApp.mGameScreenScale = 1f;
					return;
				}
				if (num == 640)
				{
					GameApp.mGameUpScale = 1.0666668f;
					GameApp.mGameDownScale = 0.5333334f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
			}
			else
			{
				if (num == 720)
				{
					GameApp.mGameUpScale = 1.2f;
					GameApp.mGameDownScale = 0.6f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
				if (num == 768)
				{
					GameApp.mGameUpScale = 1.28f;
					GameApp.mGameDownScale = 0.64f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
			}
			GameApp.mGameUpScale = 2f;
			GameApp.mGameDownScale = 1f;
			GameApp.mGameScreenScale = 0.5f;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008261 File Offset: 0x00006461
		public void SetOrientation(int Orientation)
		{
			((WP7AppDriver)this.mAppDriver).SetOrientation(Orientation);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008274 File Offset: 0x00006474
		// Note: this type is marked as 'beforefieldinit'.
		static GameApp()
		{
			string[] array = new string[5];
			array[0] = "Init";
			array[1] = "CommonGame";
			array[2] = "GamePlay";
			array[3] = "MenuRelated";
			GameApp.gInitialLoadGroups = array;
		}

		// Token: 0x04000791 RID: 1937
		public static bool USE_TRIAL_VERSION = false;

		// Token: 0x04000792 RID: 1938
		public static bool NONE_XBOX_LIVE = false;

		// Token: 0x04000793 RID: 1939
		public static bool UN_UPDATE_VERSION = false;

		// Token: 0x04000794 RID: 1940
		public static bool USE_XBOX_SERVICE = false;

		// Token: 0x04000795 RID: 1941
		public WebBrowserTask mWbt;

		// Token: 0x04000796 RID: 1942
		public bool mWaitForActive;

		// Token: 0x04000797 RID: 1943
		public bool mInitFinished;

		// Token: 0x04000797 RID: 1943
		public bool mInitFailed;

		public string mInitFailureReason = "";

		// Token: 0x04000798 RID: 1944
		public static bool mDisplayTitleUpdateMessage = false;

		// Token: 0x04000799 RID: 1945
		public static bool mExit = false;

		// Token: 0x0400079A RID: 1946
		public Image mBackgroundLayer;

		// Token: 0x0400079B RID: 1947
		public Thread mInitThread;

		// Token: 0x0400079C RID: 1948
		public static GameApp gApp = null;

		// Token: 0x0400079D RID: 1949
		public static DDS gDDS = null;

		// Token: 0x0400079E RID: 1950
		public static int gSaveGameVersion = 197;

		// Token: 0x0400079F RID: 1951
		public static int gNumOptionalGroups = 8;

		// Token: 0x040007A0 RID: 1952
		public static string[] gOptionalGroups = new string[]
		{
			"CommonBoss",
			"Boss1",
			"Boss2",
			"Boss3",
			"Boss4",
			"Boss5",
			"Boss6Common",
			"GrottoSounds"
		};

		// Token: 0x040007A1 RID: 1953
		public static string gOrgTitle = "";

		// Token: 0x040007A2 RID: 1954
		public static bool gDidCrashHandler = false;

		// Token: 0x040007A3 RID: 1955
		public static string gMetricsVersion = "1.0";

		// Token: 0x040007A4 RID: 1956
		public static int gScreenShakeX = 0;

		// Token: 0x040007A5 RID: 1957
		public static int gScreenShakeY = 0;

		// Token: 0x040007A6 RID: 1958
		public static int gLastLevel = 0;

		// Token: 0x040007A7 RID: 1959
		public static int gLastZone = -1;

		// Token: 0x040007A8 RID: 1960
		public static bool gNeedsPreCache = true;

		// Token: 0x040007A9 RID: 1961
		private static int InGameLoadThread_DrawFunc_CallCounter = 0;

		// Token: 0x040007AA RID: 1962
		private static int gAddingDlgID = -12345;

		// Token: 0x040007AB RID: 1963
		public static bool gInitialProfLoadSuccessful;

		// Token: 0x040007AC RID: 1964
		private static string[] gInitialLoadGroups;

		// Token: 0x040007AD RID: 1965
		private GameApp.PreBlockCallback mDialogCallBack;

		// Token: 0x040007AE RID: 1966
		public Game mGameMain;

		// Token: 0x040007AF RID: 1967
		public AutoMonkey mAutoMonkey;

		// Token: 0x040007B0 RID: 1968
		public bool mSavingOrLoadingProfile;

		// Token: 0x040007B1 RID: 1969
		public float mShotCorrectionAngleToWidthDist;

		// Token: 0x040007B2 RID: 1970
		public float mShotCorrectionAngleMax;

		// Token: 0x040007B3 RID: 1971
		public float mShotCorrectionWidthMax;

		// Token: 0x040007B4 RID: 1972
		public int mGuideStyle;

		// Token: 0x040007B5 RID: 1973
		public int mShotCorrectionDebugStyle;

		// Token: 0x040007B6 RID: 1974
		public bool mIronFrogModeIncluded;

		// Token: 0x040007B7 RID: 1975
		public Board mBoard;

		// Token: 0x040007B8 RID: 1976
		public LoadingScreen mLoadingScreen;

		// Token: 0x040007B9 RID: 1977
		public LevelMgr mNormalLevelMgr;

		// Token: 0x040007BA RID: 1978
		public Dictionary<string, List<GenericCachedEffect>> mCachedPIEffects = new Dictionary<string, List<GenericCachedEffect>>();

		// Token: 0x040007BB RID: 1979
		public MapScreenHackWidget mMapScreenHackWidget;

		// Token: 0x040007BC RID: 1980
		public Rect mLoadRect = default(Rect);

		// Token: 0x040007BD RID: 1981
		public ZumaDialog mReturnToMMDlg;

		// Token: 0x040007BE RID: 1982
		public Dictionary<int, DialogHideInfo> mDialogHideInfoMap;

		// Token: 0x040007BF RID: 1983
		public bool mDoingDRM;

		// Token: 0x040007C0 RID: 1984
		public int mTrialType;

		// Token: 0x040007C1 RID: 1985
		public int mFramesPlayed;

		// Token: 0x040007C2 RID: 1986
		public int mCachedLoadState;

		// Token: 0x040007C3 RID: 1987
		public bool mCachedLoad;

		// Token: 0x040007C4 RID: 1988
		public bool mInitialLoad;

		// Token: 0x040007C5 RID: 1989
		public bool mDelayIntro;

		// Token: 0x040007C6 RID: 1990
		public int mWideScreenXOffset;

		// Token: 0x040007C7 RID: 1991
		public long mLastMoreGamesUpdate;

		// Token: 0x040007C8 RID: 1992
		public int mIFLoadingAnimStartCel;

		// Token: 0x040007C9 RID: 1993
		public Upsell mUpsell;

		// Token: 0x040007CA RID: 1994
		public List<CachedTorchEffect> mCachedTorchEffects = new List<CachedTorchEffect>();

		// Token: 0x040007CB RID: 1995
		public List<CachedVolcanoEffect> mCachedVolcanoEffects = new List<CachedVolcanoEffect>();

		// Token: 0x040007CC RID: 1996
		public Dictionary<string, PIEffect> mPIEffectMap = new Dictionary<string, PIEffect>();

		// Token: 0x040007CD RID: 1997
		public bool mClickedHardMode;

		// Token: 0x040007CE RID: 1998
		public bool mInGameLoadThreadProcFailed;

		// Token: 0x040007CF RID: 1999
		public bool mStartInGameModeThreadProcRunning;

		// Token: 0x040007D0 RID: 2000
		public bool mContinuedGame;

		// Token: 0x040007D1 RID: 2001
		public int mForceZoneRestart;

		// Token: 0x040007D2 RID: 2002
		public string mChallengeLevelId = "";

		// Token: 0x040007D3 RID: 2003
		public UnderDialogWidget mUnderDialogWidget;

		// Token: 0x040007D4 RID: 2004
		public float mDialogObscurePct;

		// Token: 0x040007D5 RID: 2005
		public Dictionary<string, CompositionMgr> mPreloadedComps = new Dictionary<string, CompositionMgr>();

		// Token: 0x040007D6 RID: 2006
		public Credits mCredits;

		// Token: 0x040007D7 RID: 2007
		public CreditsHackWidget gCreditsHackWidget;

		// Token: 0x040007D8 RID: 2008
		public GenericHelp mGenericHelp;

		// Token: 0x040007D9 RID: 2009
		public MapScreen mMapScreen;

		// Token: 0x040007DA RID: 2010
		public List<IdxThumbPair> mLevelThumbnails = new List<IdxThumbPair>();

		// Token: 0x040007DB RID: 2011
		public ProxBombManager mProxBombManager;

		// Token: 0x040007DC RID: 2012
		public Music mMusic;

		// Token: 0x040007DD RID: 2013
		public SoundEffects mSoundPlayer;

		// Token: 0x040007DE RID: 2014
		public ZumaProfile mUserProfile;

		// Token: 0x040007DF RID: 2015
		public ZumaProfileMgr mProfileMgr;

		// Token: 0x040007E0 RID: 2016
		public MainMenu mMainMenu;

		// Token: 0x040007E1 RID: 2017
		public MoreGames mMoreGames;

		// Token: 0x040007E2 RID: 2018
		public NewUserDialog mNewUserDlg;

		// Token: 0x040007E3 RID: 2019
		public string mLevelXML;

		// Token: 0x040007E4 RID: 2020
		public string mHardLevelXML;

		// Token: 0x040007E5 RID: 2021
		public static string mCompositionResPrefix;

		// Token: 0x040007E6 RID: 2022
		public bool mHiRes;

		// Token: 0x040007E7 RID: 2023
		public static int mGameRes;

		// Token: 0x040007E8 RID: 2024
		public static float mGameDownScale;

		// Token: 0x040007E9 RID: 2025
		public static float mGameUpScale;

		// Token: 0x040007EA RID: 2026
		public static float mGameScreenScale;

		// Token: 0x040007EB RID: 2027
		public bool mReInit;

		// Token: 0x040007EC RID: 2028
		public bool mFromReInit;

		// Token: 0x040007ED RID: 2029
		public bool mDoingAdvModeLoad;

		// Token: 0x040007EE RID: 2030
		public int mConfTime;

		// Token: 0x040007EF RID: 2031
		public int mLoadType;

		// Token: 0x040007F0 RID: 2032
		public bool mColorblind;

		// Token: 0x040007F1 RID: 2033
		public bool mCursorTarget;

		// Token: 0x040007F2 RID: 2034
		public string mTimeStamp;

		// Token: 0x040007F3 RID: 2035
		public BambooTransition mBambooTransition;

		public DebugDialog mDebugDialog;

		public string mDebugOverlayText = "";

		public bool mDesktopFullscreen = true;

		public int mDesktopResolutionPreset;

		public bool mPendingDesktopDisplayApply;

		public bool mShowDebugResourceList;

		// Token: 0x040007F4 RID: 2036
		public string m_DefaultProfileName = "DewinterWang";

		// Token: 0x040007F5 RID: 2037
		public string m_DefaultName = "DewinterWang";

		// Token: 0x040007F6 RID: 2038
		public LegalInfo mLegalInfo;

		// Token: 0x040007F7 RID: 2039
		public AboutInfo mAboutInfo;

		// Token: 0x040007F8 RID: 2040
		public WidescreenBoardWidget mWidescreenBoardWidget;

		// Token: 0x040007F9 RID: 2041
		public ZumasRevenge.Profile.Profile m_Profile = new ZumasRevenge.Profile.Profile();

		// Token: 0x040007FA RID: 2042
		public GameApp.YesNoDialogDelegate mYesNoDialogDelegate;

		// Token: 0x040007FB RID: 2043
		public ZumaDialog mDialog;

		// Token: 0x040007FC RID: 2044
		public int mTouchOffsetX;

		// Token: 0x040007FD RID: 2045
		public int mTouchOffsetY;

		// Token: 0x040007FE RID: 2046
		private Thread mLoadingThread;

		// Token: 0x040007FF RID: 2047
		private ThreadStart mLoadingProc;

		// Token: 0x04000800 RID: 2048
		public bool mLoadLevelSuccess;

		// Token: 0x04000801 RID: 2049
		public bool StartLoadingComplete;

		// Token: 0x04000802 RID: 2050
		public int mBoardOffsetX = 85;

		// Token: 0x04000803 RID: 2051
		public int mBoardUIOffsetX = 53;

		// Token: 0x04000804 RID: 2052
		public int mOffset160X = 160;

		// Token: 0x04000805 RID: 2053
		protected GameApp.EXLiveWaiting m_XLiveState = GameApp.EXLiveWaiting.E_NONE;

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x0600037A RID: 890
		public delegate void YesNoDialogDelegate(int buttonId);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x0600037E RID: 894
		public delegate void PreBlockCallback();

		// Token: 0x02000014 RID: 20
		public enum Metrics_AppEventType
		{
			// Token: 0x04000A98 RID: 2712
			Metrics_AppEvent_StartNormal = 1,
			// Token: 0x04000A99 RID: 2713
			Metrics_AppEvent_StartUpgrade,
			// Token: 0x04000A9A RID: 2714
			Metrics_AppEvent_StartInstall,
			// Token: 0x04000A9B RID: 2715
			Metrics_AppEvent_MovedToForeground,
			// Token: 0x04000A9C RID: 2716
			Metrics_AppEvent_StartFromPushNotification
		}

		// Token: 0x02000015 RID: 21
		public enum EXLiveWaiting
		{
			// Token: 0x04000A9E RID: 2718
			E_NONE,
			// Token: 0x04000A9F RID: 2719
			E_WaitingForSignIn,
			// Token: 0x04000AA0 RID: 2720
			E_WaitingForAchivements,
			// Token: 0x04000AA1 RID: 2721
			E_Ready
		}
	}
}
