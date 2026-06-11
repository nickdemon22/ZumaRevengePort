using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000039 RID: 57
	public class ChallengeMenu : Widget, ButtonListener, PopAnimListener
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		public ChallengeMenu(GameApp theApp, MainMenu theMainMenu, bool fromMainMenu)
		{
			this.mApp = theApp;
			this.mMainMenu = theMainMenu;
			this.mCurrentChallengeZone = 0;
			this.mCrownSize = 1f;
			this.mCrownAlpha = 255f;
			this.mCrownZoomType = -1;
			this.mCrownZoomDelay = 0;
			this.mTrophy = null;
			this.mTrophyY = 0f;
			this.mDoBounceTrophy = false;
			this.mTrophyVY = 0f;
			this.mTrophyBounceCount = 0;
			this.mAceFX = null;
			this.mRegularTrophy = null;
			this.mIsAceTrophy = false;
			this.mCrossFadeTrophies = false;
			this.mAceTrophyAlpha = 0f;
			this.mFadeInAceTrophy = false;
			this.mTrophyBounceDelay = 0;
			this.mTrophyFlare = null;
			this.mShowFullAceFX = false;
			this.mCSVisFrame = 0;
			this.mLoopTrophyFlare = false;
			this.mSlideDir = 0;
			this.mXFadeAlpha = 255f;
			this.mTimer = 0;
			this.mSelectedLevel = -1;
			this.mButtons = new List<ButtonWidget>();
			this.mDefaultStringContainer = new ChallengeMenu.DefaultStringContainer();
			this.mHomeButton = null;
			this.mChallengeLevelInfoWidget = null;
			this.mChallengeScrollWidget = null;
			if (GameApp.mGameRes == 768)
			{
				this.mTitleXOffset = 30f;
			}
			else
			{
				this.mTitleXOffset = 0f;
			}
			this.mFromMainMenu = fromMainMenu;
			this.mCueMainSong = false;
			this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE);
			this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR);
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);
			this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END);
			this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP);
			this.IMAGE_UI_CHALLENGESCREEN_WOOD = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD);
			this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE);
			this.IMAGE_GUI_TIKITEMPLE_PEDESTAL = Res.GetImageByID(ResID.IMAGE_GUI_TIKITEMPLE_PEDESTAL);
			this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1);
			this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2);
			this.IMAGE_UI_CHALLENGESCREEN_DRUMS = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS);
			this.IMAGE_UI_CHALLENGESCREEN_FRUIT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT);
			this.IMAGE_UI_LEADERBOARDS_LEAVES2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2);
			this.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING);
			this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);
			this.IMAGE_UI_CHALLENGESCREEN_HOME = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0004BE48 File Offset: 0x0004A048
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			this.mChallengePages = null;
			this.mChallengeScrollWidget = null;
			this.mHomeButton = null;
			this.mChallengeLevelInfoWidget = null;
			this.mChallengeScrollPageControl = null;
			for (int i = 0; i < GlobalChallenge.NUM_CHALLENGE_ZONES; i++)
			{
				GameApp.gApp.DeleteZoneThumbnails(i);
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0004BE9C File Offset: 0x0004A09C
		public override void Draw(SexyGraphics g)
		{
			g.Translate(-GameApp.gApp.GetScreenRect().mX / 2, 0);
			int gScreenShake = GlobalChallenge.gScreenShake;
			int num = 0;
			if (GameApp.gLastZone != -1)
			{
				num = ((GameApp.gLastZone == 7 && this.mApp.mUserProfile.mChallengeUnlockState[GameApp.gLastZone - 1, 0] == 0) ? Common._DS(Common._M(635)) : Common._DS(Common._M1(608))) + gScreenShake;
			}
			int num2 = Common._DS(Common._M(500));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset, 0);
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset + this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, 0, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR, 0, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR)), GameApp.gApp.GetScreenWidth(), this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)));
			int num3 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END));
			int num4 = GameApp.gApp.GetScreenRect().mWidth - GameApp.gApp.GetScreenRect().mX - num3;
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP, num3 + this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP)), num4 - (num3 + this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth()), this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num3, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num4, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_WOOD, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)));
			if (this.mTrophy != null)
			{
				int num5 = this.mApp.mHiRes ? 2 : 0;
				int[] array = new int[]
				{
					Common._M(194),
					Common._M1(184) + num5,
					Common._M2(188),
					Common._M3(194),
					Common._M4(188),
					Common._M5(194),
					Common._M6(194)
				};
				Point[] array2 = new Point[]
				{
					new Point(Common._M(-250), Common._M1(-830)),
					new Point(Common._M2(-246), Common._M3(-850)),
					new Point(Common._M4(-246), Common._M5(-850)),
					new Point(Common._M6(-252), Common._M7(-876)),
					new Point(Common._M(-246), Common._M1(-850)),
					new Point(Common._M2(-252), Common._M3(-860)),
					new Point(Common._M4(-248), Common._M5(-838))
				};
				g.DrawImage(this.mTrophy, num + Common._DS(array[GameApp.gLastZone - 1]) - this.mTrophy.mWidth / 2, (int)(this.mTrophyY + (float)GlobalChallenge.gScreenShake));
				if (g.Is3D() && this.mTrophyFlare != null && !this.mDoBounceTrophy)
				{
					Transform transform = new Transform();
					transform.Translate((float)(num + Common._DS(array[GameApp.gLastZone - 1] + array2[GameApp.gLastZone - 1].mX)), (float)(num2 + Common._DS(array2[GameApp.gLastZone - 1].mY)));
					this.mTrophyFlare.SetTransform(transform.GetMatrix());
					this.mTrophyFlare.Draw(g);
				}
				if (g.Is3D() && this.mAceFX != null)
				{
					g.PushState();
					g.ClipRect(Common._DS(Common._M(540)) + gScreenShake, Common._DS(Common._M1(40)), Common._DS(Common._M2(530)), Common._DS(Common._M3(1200)));
					if (this.mShowFullAceFX)
					{
						this.mAceFX.Draw(g);
					}
					else
					{
						this.mAceFX.DrawLayer(g, this.mAceFX.GetLayer("mask"));
					}
					g.PopState();
				}
			}
			base.DeferOverlay(9);
			g.Translate(GameApp.gApp.GetScreenRect().mX / 2, 0);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0004C39C File Offset: 0x0004A59C
		public override void DrawOverlay(SexyGraphics g)
		{
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, -GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, GameApp.gApp.GetScreenWidth() + GameApp.gApp.mWideScreenXOffset - this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE.GetWidth() + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset - Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1.GetHeight() + Common._DS(15));
			g.DrawImage(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset - Common._DS(20) + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2.GetHeight() - Common._DS(15));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + Common._DS(120));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + Common._DS(120));
			g.SetColor(new Color(255, 255, 255, 255));
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_GAUNTLET));
			string @string = TextManager.getInstance().getString(782);
			int num = g.GetFont().StringWidth(@string);
			g.DrawString(@string, (int)(this.mTitleXOffset + (float)Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - (float)GameApp.gApp.mWideScreenXOffset + (float)((this.IMAGE_UI_CHALLENGESCREEN_WOOD.GetWidth() - num) / 2)), Common._DS(135));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DRUMS, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_FRUIT, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)));
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_LEAVES2, 42 + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)));
			if (this.mHomeButton != null)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING, 0, 0);
				if (this.mHomeButton.IsButtonDown())
				{
					float num2 = (float)((this.IMAGE_UI_CHALLENGESCREEN_HOME.GetWidth() - this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT.GetWidth()) / 2);
					float num3 = (float)((this.IMAGE_UI_CHALLENGESCREEN_HOME.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT.GetHeight()) / 2);
					g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT, (int)((float)this.mHomeButton.mX + num2 + (float)GameApp.gApp.GetScreenRect().mX), (int)((float)this.mHomeButton.mY + num3));
					return;
				}
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_HOME, this.mHomeButton.mX + GameApp.gApp.GetScreenRect().mX, this.mHomeButton.mY);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0004C764 File Offset: 0x0004A964
		public override void Update()
		{
			Common._M(0);
			if (this.mTrophyFlare != null && GameApp.gApp.Is3DAccelerated() && !this.mDoBounceTrophy)
			{
				this.MarkDirty();
				this.mTrophyFlare.Update();
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mChallengeScrollWidget.SetVisible(false);
			}
			else
			{
				this.mChallengeScrollWidget.SetVisible(true);
			}
			if (this.mFromMainMenu && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				if (this.mChallengeScrollWidget == null)
				{
					this.Init();
				}
				if (this.mChallengeScrollWidget != null)
				{
					this.mChallengeScrollWidget.SetPageHorizontal(1, false);
					this.mChallengeScrollWidget.SetPageHorizontal(0, true);
					this.mChallengePages.PreloadButtonImage(0);
				}
				this.mFromMainMenu = false;
			}
			if (this.mCueMainSong && GameApp.gApp.mBambooTransition != null && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mApp.PlaySong(1);
				this.mCueMainSong = false;
			}
			if (GlobalChallenge.gScreenShakeTimer > 0)
			{
				this.MarkDirty();
				GlobalChallenge.gScreenShakeTimer--;
				GlobalChallenge.gScreenShake = Common.Rand(Common._M(10));
				if (GlobalChallenge.gScreenShakeTimer == 0)
				{
					GlobalChallenge.gScreenShake = 0;
				}
			}
			if (this.mCrownZoomType >= 0 && --this.mCrownZoomDelay <= 0)
			{
				this.MarkDirty();
				this.mTimer++;
				int num = Common._M(75) - this.mTimer;
				float num2 = 255f / (float)num;
				this.mCrownAlpha += (float)((int)num2);
				if (this.mCrownAlpha > 255f)
				{
					this.mCrownAlpha = 255f;
				}
				num2 = Common._M(15f) / (float)num;
				this.mCrownSize -= num2;
				if (this.mCrownSize <= 1f)
				{
					if (this.mCrownZoomType == 0)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_MINI_CROWN_IMPACT));
					}
					else
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_ACE_MINI_CROWN_IMPACT));
					}
					GlobalChallenge.gScreenShakeTimer = Common._M(15);
					this.mCrownSize = 1f;
					this.mCrownAlpha = 255f;
					if (this.mApp.mUserProfile.mDoChallengeAceTrophyZoom)
					{
						if (this.mApp.mUserProfile.mDoChallengeTrophyZoom)
						{
							this.mCrownZoomType = 1;
							this.mCrownSize = Common._M(16f);
							this.mCrownAlpha = 0f;
							this.mCrownZoomDelay = Common._M(20);
							this.mTimer = 0;
						}
						else
						{
							this.mCrownZoomType = -1;
						}
						this.mApp.mUserProfile.mDoChallengeTrophyZoom = (this.mApp.mUserProfile.mDoChallengeAceTrophyZoom = false);
						return;
					}
					this.mApp.mUserProfile.mDoChallengeTrophyZoom = false;
					this.mCrownZoomType = -1;
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_LEVELS_UNLOCKED));
					return;
				}
			}
			else if (this.mCrownZoomType == -1 && --this.mCrownZoomDelay <= 0 && this.mDoBounceTrophy)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0004CA84 File Offset: 0x0004AC84
		public void ShowChallengeLevelInfo(int theZoneNum, int theLevelNum, string theLevelName)
		{
			if (this.mChallengeLevelInfoWidget != null)
			{
				this.mChallengeState = ChallengeMenu.EChallengeState.State_LevelInfo;
				this.mChallengeLevelInfoWidget.SetLevel(theZoneNum, theLevelNum, theLevelName);
				this.mChallengeLevelInfoWidget.SetVisible(true);
				this.mChallengeLevelInfoWidget.SetDisabled(false);
				this.SetFocus(this.mChallengeLevelInfoWidget);
				int num;
				if (GameApp.gApp.IsWideScreen())
				{
					num = (int)((float)(GameApp.gApp.GetScreenRect().mWidth - GameApp.gApp.GetScreenRect().mX - this.mChallengeLevelInfoWidget.GetWidth()) * 0.5f);
				}
				else
				{
					num = (int)((float)(GameApp.gApp.GetScreenRect().mWidth - this.mChallengeLevelInfoWidget.mWidth) * 0.5f);
				}
				int mY = this.mChallengeLevelInfoWidget.mY;
				this.mChallengeLevelInfoWidget.Move(num, mY);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0004CB54 File Offset: 0x0004AD54
		public void HideChallengeLevelInfo()
		{
			this.mChallengeState = ChallengeMenu.EChallengeState.State_Challenge;
			if (this.mChallengeLevelInfoWidget != null)
			{
				this.mChallengeLevelInfoWidget.SetLevel(-1, -1, "");
				this.mChallengeLevelInfoWidget.SetVisible(false);
				this.mChallengeLevelInfoWidget.SetDisabled(true);
				this.SetFocus(this);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0004CBA1 File Offset: 0x0004ADA1
		public override void MouseUp(int x, int y)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0004CBA4 File Offset: 0x0004ADA4
		public void Init()
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.ShowResourceError(true);
				GameApp.gApp.Shutdown();
			}
			Common._M(0);
			this.mChallengeState = ChallengeMenu.EChallengeState.State_Challenge;
			this.mChallengePages = new ChallengeMenuScrollContainer(this);
			this.mChallengeScrollWidget = new ScrollWidget();
			this.mChallengeScrollWidget.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset - GameApp.gApp.GetScreenRect().mX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth(), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			this.mChallengeScrollWidget.SetScrollMode((ScrollWidget.ScrollMode)1);
			this.mChallengeScrollWidget.EnableBounce(true);
			this.mChallengeScrollWidget.EnablePaging(true);
			this.mChallengeScrollWidget.AddWidget(this.mChallengePages);
			this.mChallengeScrollPageControl = new PageControl(this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR.GetCelWidth();
			this.mChallengePages.NumPages();
			this.mChallengeScrollPageControl.SetNumberOfPages(this.mChallengePages.NumPages());
			this.mChallengeScrollPageControl.Move((int)(this.mTitleXOffset + (float)((this.mWidth - this.mChallengeScrollPageControl.mWidth) / 2) - (float)GameApp.gApp.GetScreenRect().mX), Common._DS(145));
			this.mChallengeScrollPageControl.SetCurrentPage(0);
			this.AddWidget(this.mChallengeScrollPageControl);
			this.mChallengeScrollWidget.SetPageControl(this.mChallengeScrollPageControl);
			this.AddWidget(this.mChallengeScrollWidget);
			if (this.mFromMainMenu)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(this.mChallengePages.NumPages(), false);
			}
			else
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, false);
			}
			this.mChallengeLevelInfoWidget = new ChallengeLevelInfo(this);
			int num = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + Common._DS(100);
			this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight();
			Common._DS(150);
			this.mChallengeLevelInfoWidget.Resize(0, 0, num, GameApp.gApp.GetScreenRect().mHeight);
			Common.SetupDialog(this.mChallengeLevelInfoWidget);
			this.mChallengeLevelInfoWidget.mPriority = 2147483645;
			this.mChallengeLevelInfoWidget.SetVisible(false);
			this.mChallengeLevelInfoWidget.SetDisabled(true);
			this.AddWidget(this.mChallengeLevelInfoWidget);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0004CE1E File Offset: 0x0004B01E
		public void RehupChallengeButtons()
		{
			if (this.mChallengePages != null)
			{
				this.mChallengePages.RehupChallengeButtons();
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0004CE34 File Offset: 0x0004B034
		public void InitCS()
		{
			if (this.mApp.mUserProfile.mAdvModeVars.mHighestLevelBeat < 10)
			{
				return;
			}
			this.mChallengePages.RehupChallengeButtons();
			this.mMainMenu.RehupButtons();
			this.mCrownZoomType = -1;
			if ((this.mApp.mUserProfile.mDoChallengeTrophyZoom || this.mApp.mUserProfile.mDoChallengeAceTrophyZoom) && GameApp.gApp.Is3DAccelerated())
			{
				this.mCrownZoomType = (this.mApp.mUserProfile.mDoChallengeTrophyZoom ? 0 : 1);
				this.mCrownSize = Common._M(16f);
				this.mCrownAlpha = 0f;
				this.mTimer = 0;
				this.mCrownZoomDelay = 0;
			}
			bool flag = GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete;
			bool flag2 = GameApp.gApp.mUserProfile.mUnlockSparklesIdx1 != -1 || GameApp.gApp.mUserProfile.mUnlockSparklesIdx2 != -1;
			if (flag && !flag2)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, false);
				this.mChallengeScrollPageControl.SetCurrentPage(0);
				this.mChallengePages.AwardMedal(GameApp.gLastZone, GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete);
			}
			else
			{
				this.SetupChallengeZone(GameApp.gLastZone);
			}
			if (this.mFromMainMenu && this.mChallengeScrollWidget != null)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(this.mChallengePages.NumPages(), false);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0004CFAE File Offset: 0x0004B1AE
		public void StartChallengeGame()
		{
			this.mApp.StartGauntletMode(this.mChallengeLevelInfoWidget.GetChallengeLevelName(), this.mCSOverRect);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0004CFCC File Offset: 0x0004B1CC
		public virtual void ButtonPress(int id)
		{
			this.ButtonPress(id, 1);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0004CFD6 File Offset: 0x0004B1D6
		public virtual void ButtonPress(int id, int cc)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0004D00C File Offset: 0x0004B20C
		public virtual void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mSlideDir != 0)
			{
				return;
			}
			if (id == 0)
			{
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.HideChallengeMenu);
				GameApp.gApp.ToggleBambooTransition();
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0004D06C File Offset: 0x0004B26C
		public void SetupChallengeZone(int zone)
		{
			if (this.mAceFX != null)
			{
				this.mAceFX.mEmitAfterTimeline = false;
			}
			if (this.mChallengeScrollWidget != null)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(zone + 1, false);
				this.mChallengeScrollPageControl.SetCurrentPage(zone + 1);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0004D0A8 File Offset: 0x0004B2A8
		public bool ProcessHardwareBackButton()
		{
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
			if (GameApp.gApp.GetDialog(0) != null)
			{
				GameApp.gApp.DialogButtonDepress(0, 0);
				return false;
			}
			if (this.mChallengeLevelInfoWidget != null && this.mChallengeLevelInfoWidget.mVisible && !this.mChallengeLevelInfoWidget.mDisabled)
			{
				this.HideChallengeLevelInfo();
				return false;
			}
			if (this.mChallengeScrollWidget.GetPageHorizontal() > 0)
			{
				this.mChallengeScrollWidget.SetPageHorizontal(0, true);
				return false;
			}
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideChallengeMenu);
			return true;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0004D150 File Offset: 0x0004B350
		public bool IsReceivingAward()
		{
			bool flag = GameApp.gApp.mUserProfile.mUnlockSparklesIdx1 != -1 || GameApp.gApp.mUserProfile.mUnlockSparklesIdx2 != -1;
			bool flag2 = GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete;
			return flag || flag2;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
		public bool HasAcedZone(int theZoneNum)
		{
			bool flag = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, 0] == 0;
			if (flag)
			{
				return false;
			}
			bool result = true;
			if (GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete && GameApp.gLastZone == theZoneNum)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < 10; i++)
				{
					int num = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, i];
					if (num != 5)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0004D22C File Offset: 0x0004B42C
		public bool HasBeatZone(int theZoneNum)
		{
			bool flag = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, 0] == 0;
			if (flag)
			{
				return false;
			}
			bool result = true;
			if ((GameApp.gApp.mUserProfile.mDoChallengeCupComplete || GameApp.gApp.mUserProfile.mDoChallengeAceCupComplete) && GameApp.gLastZone == theZoneNum)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < 10; i++)
				{
					int num = GameApp.gApp.mUserProfile.mChallengeUnlockState[theZoneNum, i];
					if (num != 4 && num != 5)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0004D2B9 File Offset: 0x0004B4B9
		public virtual void ButtonDownTick(int x)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0004D2BB File Offset: 0x0004B4BB
		public virtual void ButtonMouseEnter(int x)
		{
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0004D2BD File Offset: 0x0004B4BD
		public virtual void ButtonMouseLeave(int x)
		{
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0004D2BF File Offset: 0x0004B4BF
		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0004D2C1 File Offset: 0x0004B4C1
		public void PopAnimStopped(int id)
		{
			if (this.mTrophyFlare != null && id == this.mTrophyFlare.mId)
			{
				if (this.mLoopTrophyFlare)
				{
					this.mTrophyFlare.Play("Main");
					return;
				}
				this.mTrophyFlare = null;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0004D2FA File Offset: 0x0004B4FA
		public void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps)
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0004D2FC File Offset: 0x0004B4FC
		public PIEffect PopAnimLoadParticleEffect(string theEffectName)
		{
			return null;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0004D2FF File Offset: 0x0004B4FF
		public bool PopAnimObjectPredraw(int theId, SexyGraphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0004D302 File Offset: 0x0004B502
		public bool PopAnimObjectPostdraw(int theId, SexyGraphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0004D305 File Offset: 0x0004B505
		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, SexyGraphics g, int theDrawCount)
		{
			return (ImagePredrawResult)1;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0004D308 File Offset: 0x0004B508
		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0004D30A File Offset: 0x0004B50A
		public bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam)
		{
			this.PopAnimCommand(theId, theCommand, theParam);
			return true;
		}

		// Token: 0x04000D1B RID: 3355
		protected ChallengeMenu.EChallengeState mChallengeState;

		// Token: 0x04000D1C RID: 3356
		public GameApp mApp;

		// Token: 0x04000D1D RID: 3357
		public PIEffect mAceFX;

		// Token: 0x04000D1E RID: 3358
		public PopAnim mTrophyFlare;

		// Token: 0x04000D1F RID: 3359
		public Rect mCSOverRect;

		// Token: 0x04000D20 RID: 3360
		public Image mTrophy;

		// Token: 0x04000D21 RID: 3361
		public Image mRegularTrophy;

		// Token: 0x04000D22 RID: 3362
		public float mTrophyY;

		// Token: 0x04000D23 RID: 3363
		public bool mDoBounceTrophy;

		// Token: 0x04000D24 RID: 3364
		public float mTrophyVY;

		// Token: 0x04000D25 RID: 3365
		public int mTrophyBounceCount;

		// Token: 0x04000D26 RID: 3366
		public int mTrophyBounceDelay;

		// Token: 0x04000D27 RID: 3367
		public bool mCrossFadeTrophies;

		// Token: 0x04000D28 RID: 3368
		public bool mIsAceTrophy;

		// Token: 0x04000D29 RID: 3369
		public bool mShowFullAceFX;

		// Token: 0x04000D2A RID: 3370
		public int mCurrentChallengeZone;

		// Token: 0x04000D2B RID: 3371
		public float mCrownSize;

		// Token: 0x04000D2C RID: 3372
		public float mCrownAlpha;

		// Token: 0x04000D2D RID: 3373
		public int mCrownZoomType;

		// Token: 0x04000D2E RID: 3374
		public int mCrownZoomDelay;

		// Token: 0x04000D2F RID: 3375
		public int mCSVisFrame;

		// Token: 0x04000D30 RID: 3376
		public bool mLoopTrophyFlare;

		// Token: 0x04000D31 RID: 3377
		public float mXFadeAlpha;

		// Token: 0x04000D32 RID: 3378
		public int mTimer;

		// Token: 0x04000D33 RID: 3379
		public int mSelectedLevel;

		// Token: 0x04000D34 RID: 3380
		public List<ButtonWidget> mButtons;

		// Token: 0x04000D35 RID: 3381
		public MainMenu mMainMenu;

		// Token: 0x04000D36 RID: 3382
		public float mAceTrophyAlpha;

		// Token: 0x04000D37 RID: 3383
		public bool mFadeInAceTrophy;

		// Token: 0x04000D38 RID: 3384
		public int mSlideDir;

		// Token: 0x04000D39 RID: 3385
		public ChallengeMenuScrollContainer mChallengePages;

		// Token: 0x04000D3A RID: 3386
		public PageControl mChallengeScrollPageControl;

		// Token: 0x04000D3B RID: 3387
		public ScrollWidget mChallengeScrollWidget;

		// Token: 0x04000D3C RID: 3388
		public ButtonWidget mHomeButton;

		// Token: 0x04000D3D RID: 3389
		public ChallengeLevelInfo mChallengeLevelInfoWidget;

		// Token: 0x04000D3E RID: 3390
		public float mTitleXOffset;

		// Token: 0x04000D3F RID: 3391
		public bool mFromMainMenu;

		// Token: 0x04000D40 RID: 3392
		public bool mCueMainSong;

		// Token: 0x04000D41 RID: 3393
		public ChallengeMenu.DefaultStringContainer mDefaultStringContainer;

		// Token: 0x04000D42 RID: 3394
		private Image IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE;

		// Token: 0x04000D43 RID: 3395
		private Image IMAGE_UI_CHALLENGESCREEN_BG_FLOOR;

		// Token: 0x04000D44 RID: 3396
		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES;

		// Token: 0x04000D45 RID: 3397
		private Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END;

		// Token: 0x04000D46 RID: 3398
		private Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP;

		// Token: 0x04000D47 RID: 3399
		private Image IMAGE_UI_CHALLENGESCREEN_WOOD;

		// Token: 0x04000D48 RID: 3400
		private Image IMAGE_UI_CHALLENGESCREEN_BG_SIDE;

		// Token: 0x04000D49 RID: 3401
		private Image IMAGE_GUI_TIKITEMPLE_PEDESTAL;

		// Token: 0x04000D4A RID: 3402
		private Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1;

		// Token: 0x04000D4B RID: 3403
		private Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2;

		// Token: 0x04000D4C RID: 3404
		private Image IMAGE_UI_CHALLENGESCREEN_DRUMS;

		// Token: 0x04000D4D RID: 3405
		private Image IMAGE_UI_CHALLENGESCREEN_FRUIT;

		// Token: 0x04000D4E RID: 3406
		private Image IMAGE_UI_LEADERBOARDS_LEAVES2;

		// Token: 0x04000D4F RID: 3407
		private Image IMAGE_UI_CHALLENGESCREEN_HOME_BACKING;

		// Token: 0x04000D50 RID: 3408
		private Image IMAGE_UI_CHALLENGESCREEN_HOME_SELECT;

		// Token: 0x04000D51 RID: 3409
		private Image IMAGE_UI_CHALLENGESCREEN_HOME;

		// Token: 0x04000D52 RID: 3410
		private Image IMAGE_UI_CHALLENGE_PAGE_INDICATOR;

		// Token: 0x020000FA RID: 250
		public class DefaultStringContainer
		{
			// Token: 0x06000F1D RID: 3869 RVA: 0x0009CA32 File Offset: 0x0009AC32
			public DefaultStringContainer()
			{
				this.mDefaultStr = this.NonIfLocked();
			}

			// Token: 0x06000F1E RID: 3870 RVA: 0x0009CA46 File Offset: 0x0009AC46
			public string NonIfLocked()
			{
				return TextManager.getInstance().getString(427);
			}

			// Token: 0x06000F1F RID: 3871 RVA: 0x0009CA57 File Offset: 0x0009AC57
			public string IfLocked()
			{
				return TextManager.getInstance().getString(428);
			}

			// Token: 0x06000F20 RID: 3872 RVA: 0x0009CA68 File Offset: 0x0009AC68
			public string CanPlayZone()
			{
				return TextManager.getInstance().getString(429);
			}

			// Token: 0x06000F21 RID: 3873 RVA: 0x0009CA79 File Offset: 0x0009AC79
			public string ZoneUnlocked()
			{
				return TextManager.getInstance().getString(430);
			}

			// Token: 0x06000F22 RID: 3874 RVA: 0x0009CA8A File Offset: 0x0009AC8A
			public string NothingSelected()
			{
				return TextManager.getInstance().getString(431);
			}

			// Token: 0x0400189E RID: 6302
			public string mDefaultStr;
		}

		// Token: 0x020000FB RID: 251
		public enum Zoom
		{
			// Token: 0x040018A0 RID: 6304
			Zooming_Crown,
			// Token: 0x040018A1 RID: 6305
			Zooming_AceCrown
		}

		// Token: 0x020000FC RID: 252
		protected enum EChallengeState
		{
			// Token: 0x040018A3 RID: 6307
			State_Challenge,
			// Token: 0x040018A4 RID: 6308
			State_LevelInfo
		}
	}
}
