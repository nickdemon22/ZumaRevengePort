using System;
using System.Threading;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000040 RID: 64
	public class Achievements : Widget, ButtonListener
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x000544DC File Offset: 0x000526DC
		public Achievements()
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.Shutdown();
			}
			this.mDisplayMode = -1;
			this.mClip = false;
			this.mSelectedScreenState = 0;
			this.mHomeButton = null;
			this.mUpButton = null;
			this.mDownButton = null;
			if (GameApp.mGameRes == 768)
			{
				this.mTitleXOffset = 30f;
			}
			else
			{
				this.mTitleXOffset = 20f;
			}
			this.mNeedsInitScroll = true;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000546A3 File Offset: 0x000528A3
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000546AD File Offset: 0x000528AD
		public void StartLoading()
		{
			this.mLoadingProc = new ThreadStart(this.LoadingRank);
			this.mLoadDataThread = new Thread(this.mLoadingProc);
			this.mLoadingData = true;
			this.mLoadingDataComplete = false;
			this.mLoadDataThread.Start();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000546EC File Offset: 0x000528EC
		private void LoadingRank()
		{
			this.mAchievementsPages.AddPage();
			this.mAchievementsPages.AddPage();
			this.mAchievementsPages.AddPage();
			this.mAchievementsPages.AddPage();
			this.mAchievementsPages.AddPage();
			this.mAchievementsPages.Resize(0, 0, this.mAchievementsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.mAchievementsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mAchievementsPages.mNumPages - 100);
			this.mAchievementsScrollWidget.AddWidget(this.mAchievementsPages);
			this.mLoadingDataComplete = true;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00054788 File Offset: 0x00052988
		public void Init()
		{
			this.mAchievementsPages = new AchievementsPages(this);
			this.mAchievementsScrollWidget = new ScrollWidget();
			this.mAchievementsScrollWidget.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)) - GameApp.gApp.mWideScreenXOffset + Common._DS(10), 20 + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)), this.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth() + 30, this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() - 40);
			this.mAchievementsScrollWidget.SetScrollMode((ScrollWidget.ScrollMode)2);
			this.mAchievementsScrollWidget.EnableBounce(true);
			this.mAchievementsScrollWidget.EnablePaging(true);
			this.mAchievementsPageControl = new PageControl(this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR.GetCelWidth();
			this.mAchievementsPages.NumPages();
			this.mAchievementsPageControl.SetNumberOfPages(this.mAchievementsPages.NumPages());
			this.mAchievementsPageControl.Move((int)this.mTitleXOffset + (this.mWidth - this.mAchievementsPageControl.mWidth) / 2, Common._DS(145));
			this.mAchievementsPageControl.SetCurrentPage(0);
			this.AddWidget(this.mAchievementsPageControl);
			this.mAchievementsScrollWidget.SetPageControl(this.mAchievementsPageControl);
			this.AddWidget(this.mAchievementsScrollWidget);
			this.mUpButton = new ButtonWidget(7, this);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHT);
			this.mUpButton.mButtonImage = imageByID;
			this.mUpButton.mDownImage = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHT_ON);
			float num = 0f;
			float num2 = 0f;
			this.mUpButton.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHT)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHT)), imageByID.GetWidth(), imageByID.GetHeight());
			this.mUpButton.mNormalRect = new Rect(0, 0, imageByID.GetWidth(), imageByID.GetHeight());
			this.mUpButton.mDownRect = new Rect((int)num, (int)num2, imageByID.GetWidth() - (int)num, imageByID.GetHeight() - (int)num2);
			this.mUpButton.mDoFinger = true;
			this.mUpButton.mVisible = true;
			this.AddWidget(this.mUpButton);
			this.mUpButton.SetVisible(false);
			this.mUpButton.SetDisabled(true);
			this.mDownButton = new ButtonWidget(6, this);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHTF);
			this.mDownButton.mButtonImage = imageByID2;
			this.mDownButton.mDownImage = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHTF_ON);
			float num3 = 0f;
			float num4 = 0f;
			this.mDownButton.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHTF)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_ARROW_LIGHTF)), imageByID2.GetWidth(), imageByID2.GetHeight());
			this.mDownButton.mNormalRect = new Rect(0, 0, imageByID2.GetWidth(), imageByID2.GetHeight());
			this.mDownButton.mDownRect = new Rect((int)num3, (int)num4, imageByID2.GetWidth() - (int)num3, imageByID2.GetHeight() - (int)num4);
			this.mDownButton.mDoFinger = true;
			this.mDownButton.mVisible = true;
			this.mDownButton.SetDisabled(true);
			this.AddWidget(this.mDownButton);
			this.mAchievementsScrollWidget.SetPageVertical(1, false);
			this.mDownButton.SetVisible(true);
			this.mUpButton.SetVisible(true);
			this.mCurrentPage = 0;
			this.mEnterScreneLoad = false;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00054B18 File Offset: 0x00052D18
		public override void Update()
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (!this.mEnterScreneLoad && GameApp.gApp.mBambooTransition != null && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mEnterScreneLoad = true;
				this.StartLoading();
			}
			if (this.mAchievementsScrollWidget != null)
			{
				if (this.mAchievementsScrollWidget.GetPageVertical() == 0)
				{
					this.mUpButton.SetVisible(false);
				}
				else
				{
					this.mUpButton.SetVisible(true);
				}
				if (this.mAchievementsScrollWidget.GetPageVertical() == 4)
				{
					this.mDownButton.SetVisible(false);
				}
				else
				{
					this.mDownButton.SetVisible(true);
				}
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mAchievementsScrollWidget.SetVisible(false);
			}
			else
			{
				this.mAchievementsScrollWidget.SetVisible(true);
			}
			if (!this.mLoadingDataComplete)
			{
				ulong num = (ulong)Common.SexyTime();
				if (num - this.mTicker > 500UL)
				{
					if (this.loadingDot.Length < 6)
					{
						this.loadingDot += ".";
					}
					else
					{
						this.loadingDot = "";
					}
					this.mTicker = num;
				}
			}
			if (!GameApp.gApp.mBambooTransition.IsInProgress() && this.mNeedsInitScroll)
			{
				this.mAchievementsScrollWidget.SetPageVertical(0, true);
				this.mNeedsInitScroll = false;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00054C87 File Offset: 0x00052E87
		public float GetTitleXOffset()
		{
			return this.mTitleXOffset;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00054C90 File Offset: 0x00052E90
		public override void Draw(SexyGraphics g)
		{
			Graphics3D graphics3D = (g != null) ? g.Get3D() : null;
			g.Translate(this.mX / 2, 0);
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset, 0, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetHeight());
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE, -GameApp.gApp.mWideScreenXOffset + this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, 0, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetWidth() + 21, this.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR, 0, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR)), GameApp.gApp.GetScreenWidth(), this.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR.GetHeight());
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE));
			int num = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END));
			int num2 = GameApp.gApp.GetScreenWidth() - num - this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth() + GameApp.gApp.mWideScreenXOffset;
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP, num + this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP)), num2 - num - this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END.GetWidth(), this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END, num2, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_WOOD, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)));
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_SHADOW, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)));
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 195;
			GameApp.gApp.mUserProfile.m_AchievementMgr.getAchievementsInfo(ref num4, ref num3, ref num6, ref num5);
			int num8 = 270;
			Common._DS(Common._M(200));
			int num9 = Common._DS(Common._M(0));
			string text = string.Concat(new object[]
			{
				num4,
				" / ",
				num3,
				" ",
				TextManager.getInstance().getString(93)
			});
			int num10 = num8;
			int num11 = num9 + num7;
			g.SetColor(255, 255, 255, 255);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK));
			g.DrawString(text, num10, num11);
			text = string.Concat(new object[]
			{
				" G : ",
				num6,
				" / ",
				num5
			});
			int num12 = 810;
			num10 = num12 - this.mTitleFont.StringWidth(text);
			num11 = num9 + num7;
			num9 += 32;
			g.DrawString(text, num10, num11);
			graphics3D.SetMasking((Graphics3D.EMaskMode)1);
			g.FillRect(260, 160, 588, 40);
			graphics3D.SetMasking((Graphics3D.EMaskMode)0);
			g.Translate(-this.mX / 2, 0);
			base.DeferOverlay(9);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0005505C File Offset: 0x0005325C
		public override void DrawOverlay(SexyGraphics g)
		{
			g.Translate(this.mX / 2, 0);
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, -GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, GameApp.gApp.GetScreenWidth() + GameApp.gApp.mWideScreenXOffset - this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_GUI_Achievements_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset - Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1.GetHeight() + Common._DS(15));
			g.DrawImage(this.IMAGE_GUI_Achievements_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset - Common._DS(20) + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2.GetHeight() - Common._DS(15));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + Common._DS(120));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + Common._DS(120));
			g.SetColor(255, 255, 255, 255);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_GAUNTLET));
			string @string = TextManager.getInstance().getString(860);
			float num = (float)g.GetFont().StringWidth(@string);
			int num2 = 0;
			if ((int)Localization.GetCurrentLanguage() == 6 || (int)Localization.GetCurrentLanguage() == 7)
			{
				num2 = 15;
			}
			else if ((int)Localization.GetCurrentLanguage() == 5 || (int)Localization.GetCurrentLanguage() == 10)
			{
				num2 = 20;
			}
			g.DrawString(@string, (int)this.mTitleXOffset + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset + (int)(((float)this.IMAGE_UI_CHALLENGESCREEN_WOOD.GetWidth() - num) / 2f), Common._DS(135) + num2);
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_BOSSES, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_BOSSES)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_BOSSES)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_FRUIT, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)));
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_LEAVES2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX / 2 + this.mAspectOffset + 10, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)));
			g.Translate(-this.mX / 2, 0);
			if (!this.mLoadingDataComplete)
			{
				g.PushState();
				g.Translate(-this.mX, -this.mY);
				g.SetColor(0, 0, 0, 130);
				g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
				g.PopState();
				g.SetFont(this.mLoadingFont);
				g.DrawString(TextManager.getInstance().getString(581) + this.loadingDot, GameApp.gApp.GetScreenWidth() / 2 - 100, GameApp.gApp.mHeight / 2);
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00055440 File Offset: 0x00053640
		public bool ProcessHardwareBackButton()
		{
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return true;
			}
			Dialog dialog = GameApp.gApp.GetDialog(0);
			if (dialog != null)
			{
				GameApp.gApp.DialogButtonDepress(0, 0);
				return false;
			}
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideAchievements);
			GameApp.gApp.ToggleBambooTransition();
			return true;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000554C4 File Offset: 0x000536C4
		public void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (this.mHomeButton != null && this.mHomeButton.mId == id)
			{
				GameApp.gApp.ToggleBambooTransition();
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideAchievements);
				return;
			}
			if (this.mUpButton != null && this.mUpButton.mId == id)
			{
				this.mAchievementsScrollWidget.PreviousVertPage();
				return;
			}
			if (this.mDownButton != null && this.mDownButton.mId == id)
			{
				this.mAchievementsScrollWidget.NextVertPage();
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00055577 File Offset: 0x00053777
		public void ButtonPress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000555AB File Offset: 0x000537AB
		public void ButtonPress(int theId, int theClickCount)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000555DF File Offset: 0x000537DF
		public void ButtonMouseEnter(int id)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000555E1 File Offset: 0x000537E1
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000555E3 File Offset: 0x000537E3
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000555E5 File Offset: 0x000537E5
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000555E8 File Offset: 0x000537E8
		public virtual void TouchEnded(SexyAppBase.Touch touch)
		{
			int mX = touch.location.mX;
			int mY = touch.location.mY;
			this.MouseUp(mX, mY, 1);
		}

		// Token: 0x040010D4 RID: 4308
		private int mSelectedScreenState;

		// Token: 0x040010D5 RID: 4309
		protected ButtonWidget mHomeButton;

		// Token: 0x040010D6 RID: 4310
		protected ButtonWidget mUpButton;

		// Token: 0x040010D7 RID: 4311
		protected ButtonWidget mDownButton;

		// Token: 0x040010D8 RID: 4312
		protected int mDisplayMode;

		// Token: 0x040010D9 RID: 4313
		protected int mBounceCount;

		// Token: 0x040010DA RID: 4314
		protected AchievementsPages mAchievementsPages;

		// Token: 0x040010DB RID: 4315
		protected PageControl mAchievementsPageControl;

		// Token: 0x040010DC RID: 4316
		protected ScrollWidget mAchievementsScrollWidget;

		// Token: 0x040010DD RID: 4317
		protected bool mNeedsInitScroll;

		// Token: 0x040010DE RID: 4318
		protected float mTitleXOffset;

		// Token: 0x040010DF RID: 4319
		protected int mAspectOffset = 30;

		// Token: 0x040010E0 RID: 4320
		protected Image IMAGE_UI_CHALLENGE_PAGE_INDICATOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);

		// Token: 0x040010E1 RID: 4321
		protected Image IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE);

		// Token: 0x040010E2 RID: 4322
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_FLOOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR);

		// Token: 0x040010E3 RID: 4323
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END);

		// Token: 0x040010E4 RID: 4324
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP);

		// Token: 0x040010E5 RID: 4325
		protected Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		// Token: 0x040010E6 RID: 4326
		protected Image IMAGE_UI_CHALLENGESCREEN_WOOD = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD);

		// Token: 0x040010E7 RID: 4327
		protected Image IMAGE_UI_LEADERBOARDS_LEAVES2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2);

		// Token: 0x040010E8 RID: 4328
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_SIDE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE);

		// Token: 0x040010E9 RID: 4329
		protected Image IMAGE_GUI_Achievements_PEDESTAL = Res.GetImageByID(ResID.IMAGE_GUI_TIKITEMPLE_PEDESTAL);

		// Token: 0x040010EA RID: 4330
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1);

		// Token: 0x040010EB RID: 4331
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2);

		// Token: 0x040010EC RID: 4332
		protected Image IMAGE_UI_LEADERBOARDS_BOSSES = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_BOSSES);

		// Token: 0x040010ED RID: 4333
		protected Image IMAGE_UI_CHALLENGESCREEN_FRUIT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT);

		// Token: 0x040010EE RID: 4334
		protected Image IMAGE_UI_LEADERBOARDS_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW2);

		// Token: 0x040010EF RID: 4335
		public float mXOff;

		// Token: 0x040010F0 RID: 4336
		protected int mCurrentPage;

		// Token: 0x040010F1 RID: 4337
		private Thread mLoadDataThread;

		// Token: 0x040010F2 RID: 4338
		private ThreadStart mLoadingProc;

		// Token: 0x040010F3 RID: 4339
		private bool mLoadingData;

		// Token: 0x040010F4 RID: 4340
		private bool mLoadingDataComplete;

		// Token: 0x040010F5 RID: 4341
		private Font mLoadingFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);

		// Token: 0x040010F6 RID: 4342
		private Font mTitleFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK);

		// Token: 0x040010F7 RID: 4343
		private string loadingDot = "";

		// Token: 0x040010F8 RID: 4344
		private ulong mTicker = (ulong)Common.SexyTime();

		// Token: 0x040010F9 RID: 4345
		private bool mEnterScreneLoad;

		// Token: 0x02000041 RID: 65
		private enum ButtonState
		{
			// Token: 0x040010FB RID: 4347
			AdvStats_Btn,
			// Token: 0x040010FC RID: 4348
			HardAdvStats_Btn,
			// Token: 0x040010FD RID: 4349
			Challenge_Btn,
			// Token: 0x040010FE RID: 4350
			IronFrog_Btn,
			// Token: 0x040010FF RID: 4351
			MoreStats_Btn,
			// Token: 0x04001100 RID: 4352
			Back_Btn,
			// Token: 0x04001101 RID: 4353
			Next_Btn,
			// Token: 0x04001102 RID: 4354
			Prev_Btn
		}
	}
}
