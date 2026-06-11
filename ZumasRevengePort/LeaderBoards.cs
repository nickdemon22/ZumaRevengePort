using System;
using System.Threading;
// using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000057 RID: 87
	public class LeaderBoards : Widget, ButtonListener
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x00058928 File Offset: 0x00056B28
		public LeaderBoards()
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

		// Token: 0x06000A28 RID: 2600 RVA: 0x00058AEF File Offset: 0x00056CEF
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00058AFC File Offset: 0x00056CFC
		public void Init()
		{
			this.mLeaderBoardsPages = new LeaderBoardsPages(this);
			this.mLeaderBoardsScrollWidget = new ScrollWidget();
			this.mLeaderBoardsScrollWidget.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)) - GameApp.gApp.mWideScreenXOffset + Common._DS(10), 20 + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW)), this.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth() + 30, this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() - 40);
			this.mLeaderBoardsScrollWidget.SetScrollMode((ScrollWidget.ScrollMode)2);
			this.mLeaderBoardsScrollWidget.EnableBounce(true);
			this.mLeaderBoardsScrollWidget.EnablePaging(true);
			this.mLeaderBoardsScrollWidget.mLoadPage = new ScrollWidget.ExternalLoadPage(this.PageLoading);
			this.mLeaderBoardsPageControl = new PageControl(this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR.GetCelWidth();
			this.mLeaderBoardsPages.NumPages();
			this.mLeaderBoardsPageControl.SetNumberOfPages(this.mLeaderBoardsPages.NumPages());
			this.mLeaderBoardsPageControl.Move((int)this.mTitleXOffset + (this.mWidth - this.mLeaderBoardsPageControl.mWidth) / 2, Common._DS(145));
			this.mLeaderBoardsPageControl.SetCurrentPage(0);
			this.AddWidget(this.mLeaderBoardsPageControl);
			this.mLeaderBoardsScrollWidget.SetPageControl(this.mLeaderBoardsPageControl);
			this.AddWidget(this.mLeaderBoardsScrollWidget);
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
			this.mLeaderBoardsScrollWidget.SetPageVertical(1, false);
			this.mDownButton.SetVisible(true);
			this.mUpButton.SetVisible(true);
			this.mCurrentPage = 0;
			this.mDataPage = 0;
			this.mEnterScreneLoad = false;
			this.mFrogStr = TextManager.getInstance().getString(57);
			this.mScoreStr = TextManager.getInstance().getString(669);
			this.mScoreStr = this.mScoreStr.Substring(0, this.mScoreStr.Length - 1);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00058EF0 File Offset: 0x000570F0
		public void PageLoading(int ranking)
		{
			bool flag;
			if (ranking != 0)
			{
				if (!this.mCanPageDown)
				{
					this.mPageDown = false;
					this.mLeaderBoardsScrollWidget.SetPage(0, 1, true);
					return;
				}
				this.mCurrentPage++;
				this.mDataPage++;
				this.mPageDown = true;
				flag = true;
			}
			else
			{
				if (!this.mCanPageUp)
				{
					this.mPageUp = false;
					this.mLeaderBoardsScrollWidget.SetPage(0, 1, true);
					return;
				}
				this.mCurrentPage--;
				this.mDataPage--;
				this.mPageUp = true;
				flag = true;
			}
			if (flag)
			{
				this.mLoadPage = 1;
			}
			this.mLeaderBoardsScrollWidget.SetPageVertical(ranking, true);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00058FA4 File Offset: 0x000571A4
		public void StartLoading(int ranking)
		{
			this.mCurrentPage = ranking;
			this.mLoadingProc = new ThreadStart(this.LoadingRank);
			this.mLoadDataThread = new Thread(this.mLoadingProc);
			this.mLoadingData = true;
			this.mLoadingDataComplete = false;
			this.mLeaderBoardsScrollWidget.SetVisible(false);
			this.mLoadDataThread.Start();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00059000 File Offset: 0x00057200
		private void LoadingRank()
		{
			if (GameApp.USE_XBOX_SERVICE && !GameApp.USE_TRIAL_VERSION)
			{
				this.readLeaderboard();
				this.mLeaderBoardsScrollWidget.SetDisabled(true);
				return;
			}
			this.mLoadingDataComplete = true;
			this.mLeaderBoardsScrollWidget.SetVisible(true);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00059038 File Offset: 0x00057238
		public void LoadOfflineRanking()
		{
			ulong num = (ulong)Common.SexyTime();
			ulong num2;
			do
			{
				num2 = (ulong)Common.SexyTime();
			}
			while (num2 - num <= 1000UL);
			if (!this.mLeaderBoardsScrollWidget.HasWidget(this.mLeaderBoardsPages))
			{
				this.mLeaderBoardsPages.AddPage(this.mCurrentPage, false);
				this.mLeaderBoardsPages.Resize(0, 0, this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mLeaderBoardsPages.mNumPages * 3);
				this.mLeaderBoardsScrollWidget.AddWidget(this.mLeaderBoardsPages);
			}
			else
			{
				this.mLeaderBoardsPages.AddPage(this.mCurrentPage, true);
			}
			this.mLoadingDataComplete = true;
			this.mLeaderBoardsScrollWidget.SetVisible(true);
			this.mLeaderBoardsScrollWidget.SetPageVertical(1, false);
			this.mCurrentPage = 1;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00059110 File Offset: 0x00057310
		public void readLeaderboard()
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00059220 File Offset: 0x00057420
		protected void LeaderboardPageDownCallback(IAsyncResult result)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000593B4 File Offset: 0x000575B4
		protected void LeaderboardPageUpCallback(IAsyncResult result)
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00059548 File Offset: 0x00057748
		protected void LeaderboardReadCallback(IAsyncResult result)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00059694 File Offset: 0x00057894
		private void ShowXboxErrorMessage()
		{
			if (GameApp.gApp.mMainMenu != null && GameApp.gApp.mMainMenu.mState == MainMenu_State.State_LeaderBoards)
			{
				GameApp.gApp.DoGenericDialog("", TextManager.getInstance().getString(59), true, new GameApp.PreBlockCallback(this.ReturnMain), Common._DS(100));
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000596F0 File Offset: 0x000578F0
		public override void Update()
		{
			if (this.mLoadPage > 0 && this.mLoadPage < 60)
			{
				this.mLoadPage++;
				if (this.mLoadPage == 60)
				{
					this.StartLoading(this.mCurrentPage);
					this.mLoadPage = 0;
				}
			}
			if (!this.mEnterScreneLoad && GameApp.gApp.mBambooTransition != null && !GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mEnterScreneLoad = true;
				this.StartLoading(0);
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mLeaderBoardsScrollWidget.SetVisible(false);
			}
			else
			{
				this.mLeaderBoardsScrollWidget.SetVisible(true);
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
				this.mLeaderBoardsScrollWidget.SetPageVertical(0, true);
				this.mNeedsInitScroll = false;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00059829 File Offset: 0x00057A29
		public float GetTitleXOffset()
		{
			return this.mTitleXOffset;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00059834 File Offset: 0x00057A34
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
			int num3 = 410;
			int num4 = 165;
			g.SetFont(this.mStatsFont);
			g.SetColor(89, 187, 149);
			g.WriteString("#", num3 - 130 + (int)this.mXOff, num4 + this.mStatsFont.GetAscent(), 0, 1);
			int num5 = 25;
			g.WriteString(this.mFrogStr, num3 + (int)this.mXOff + num5, num4 + this.mStatsFont.GetAscent(), 0, 1);
			int num6 = this.mStatsFont.StringWidth(this.mScoreStr);
			g.WriteString(this.mScoreStr, 800 - num6, num4 + this.mStatsFont.GetAscent(), num6, -1);
			graphics3D.SetMasking((Graphics3D.EMaskMode)1);
			g.FillRect(250, 160, 588, 40);
			graphics3D.SetMasking((Graphics3D.EMaskMode)0);
			g.Translate(-this.mX / 2, 0);
			base.DeferOverlay(9);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00059B78 File Offset: 0x00057D78
		public override void DrawOverlay(SexyGraphics g)
		{
			g.Translate(this.mX / 2, 0);
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, -GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, GameApp.gApp.GetScreenWidth() + GameApp.gApp.mWideScreenXOffset - this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_GUI_LeaderBoards_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset - Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1.GetHeight() + Common._DS(15));
			g.DrawImage(this.IMAGE_GUI_LeaderBoards_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset - Common._DS(20) + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2.GetHeight() - Common._DS(15));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + Common._DS(120));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + Common._DS(120));
			g.SetColor(255, 255, 255, 255);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_GAUNTLET));
			string @string = TextManager.getInstance().getString(859);
			float num = (float)g.GetFont().StringWidth(@string);
			int num2 = 0;
			int num3 = 0;
			if ((int)Localization.GetCurrentLanguage() == 6 || (int)Localization.GetCurrentLanguage() == 7)
			{
				num2 = 15;
				num3 = 15;
			}
			else if ((int)Localization.GetCurrentLanguage() == 5 || (int)Localization.GetCurrentLanguage() == 10)
			{
				num2 = 20;
			}
			g.DrawString(@string, num3 + (int)this.mTitleXOffset + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset + (int)(((float)this.IMAGE_UI_CHALLENGESCREEN_WOOD.GetWidth() - num) / 2f), Common._DS(135) + num2);
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

		// Token: 0x06000A37 RID: 2615 RVA: 0x00059F64 File Offset: 0x00058164
		public bool ProcessHardwareBackButton()
		{
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
			Dialog dialog = GameApp.gApp.GetDialog(0);
			if (dialog != null)
			{
				GameApp.gApp.DialogButtonDepress(0, 0);
				return false;
			}
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideLeaderBoards);
			return true;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00059FC7 File Offset: 0x000581C7
		public void ReturnMain()
		{
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideLeaderBoards);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00059FF8 File Offset: 0x000581F8
		public void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (!this.mLoadingDataComplete)
			{
				return;
			}
			if (this.mHomeButton != null && this.mHomeButton.mId == id)
			{
				GameApp.gApp.ToggleBambooTransition();
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideLeaderBoards);
				return;
			}
			if (this.mUpButton != null && this.mUpButton.mId == id)
			{
				if (this.mCanPageUp)
				{
					this.mCurrentPage--;
					this.mPageUp = true;
					this.StartLoading(this.mCurrentPage);
					return;
				}
			}
			else if (this.mDownButton != null && this.mDownButton.mId == id && this.mCanPageDown)
			{
				this.mCurrentPage++;
				this.mPageDown = true;
				this.StartLoading(this.mCurrentPage);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0005A0F0 File Offset: 0x000582F0
		public void ButtonPress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (!this.mLoadingDataComplete)
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0005A12D File Offset: 0x0005832D
		public void ButtonPress(int theId, int theClickCount)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (!this.mLoadingDataComplete)
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0005A16A File Offset: 0x0005836A
		public void ButtonMouseEnter(int id)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0005A16C File Offset: 0x0005836C
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0005A16E File Offset: 0x0005836E
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0005A170 File Offset: 0x00058370
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0005A172 File Offset: 0x00058372
		public override void TouchBegan(SexyAppBase.Touch theTouch)
		{
			if (!this.mLoadingDataComplete)
			{
				return;
			}
			if (this.mDataPage == 0)
			{
				base.TouchMoved(theTouch);
			}
		}

		// Token: 0x040011E3 RID: 4579
		private int mSelectedScreenState;

		// Token: 0x040011E4 RID: 4580
		protected ButtonWidget mHomeButton;

		// Token: 0x040011E5 RID: 4581
		protected ButtonWidget mUpButton;

		// Token: 0x040011E6 RID: 4582
		protected ButtonWidget mDownButton;

		// Token: 0x040011E7 RID: 4583
		protected int mDisplayMode;

		// Token: 0x040011E8 RID: 4584
		protected int mBounceCount;

		// Token: 0x040011E9 RID: 4585
		protected LeaderBoardsPages mLeaderBoardsPages;

		// Token: 0x040011EA RID: 4586
		protected PageControl mLeaderBoardsPageControl;

		// Token: 0x040011EB RID: 4587
		protected ScrollWidget mLeaderBoardsScrollWidget;

		// Token: 0x040011EC RID: 4588
		protected bool mNeedsInitScroll;

		// Token: 0x040011ED RID: 4589
		protected float mTitleXOffset;

		// Token: 0x040011EE RID: 4590
		protected int mAspectOffset = 30;

		// Token: 0x040011EF RID: 4591
		protected Image IMAGE_UI_CHALLENGE_PAGE_INDICATOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);

		// Token: 0x040011F0 RID: 4592
		protected Image IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE);

		// Token: 0x040011F1 RID: 4593
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_FLOOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR);

		// Token: 0x040011F2 RID: 4594
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END);

		// Token: 0x040011F3 RID: 4595
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP);

		// Token: 0x040011F4 RID: 4596
		protected Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		// Token: 0x040011F5 RID: 4597
		protected Image IMAGE_UI_CHALLENGESCREEN_WOOD = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD);

		// Token: 0x040011F6 RID: 4598
		protected Image IMAGE_UI_LEADERBOARDS_LEAVES2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2);

		// Token: 0x040011F7 RID: 4599
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_SIDE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE);

		// Token: 0x040011F8 RID: 4600
		protected Image IMAGE_GUI_LeaderBoards_PEDESTAL = Res.GetImageByID(ResID.IMAGE_GUI_TIKITEMPLE_PEDESTAL);

		// Token: 0x040011F9 RID: 4601
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1);

		// Token: 0x040011FA RID: 4602
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2);

		// Token: 0x040011FB RID: 4603
		protected Image IMAGE_UI_LEADERBOARDS_BOSSES = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_BOSSES);

		// Token: 0x040011FC RID: 4604
		protected Image IMAGE_UI_CHALLENGESCREEN_FRUIT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT);

		// Token: 0x040011FD RID: 4605
		protected Image IMAGE_UI_LEADERBOARDS_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW);

		// Token: 0x040011FE RID: 4606
		public float mXOff;

		// Token: 0x040011FF RID: 4607
		protected int mCurrentPage;

		// Token: 0x04001200 RID: 4608
		protected int mDataPage;

		// Token: 0x04001201 RID: 4609
		private Thread mLoadDataThread;

		// Token: 0x04001202 RID: 4610
		private ThreadStart mLoadingProc;

		// Token: 0x04001203 RID: 4611
		private bool mLoadingData;

		// Token: 0x04001204 RID: 4612
		private bool mLoadingDataComplete;

		// Token: 0x04001205 RID: 4613
		private Font mLoadingFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);

		// Token: 0x04001206 RID: 4614
		private string loadingDot = "";

		// Token: 0x04001207 RID: 4615
		private ulong mTicker = (ulong)Common.SexyTime();

		// Token: 0x04001208 RID: 4616
		// private LeaderboardReader mLeaderboardReader;

		// Token: 0x04001209 RID: 4617
		private bool mCanPageUp;

		// Token: 0x0400120A RID: 4618
		private bool mCanPageDown;

		// Token: 0x0400120B RID: 4619
		private bool mEnterScreneLoad;

		// Token: 0x0400120C RID: 4620
		private bool mPageUp;

		// Token: 0x0400120D RID: 4621
		private bool mPageDown;

		// Token: 0x0400120E RID: 4622
		private int mLoadPage;

		// Token: 0x0400120F RID: 4623
		private Font mStatsFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_STROKE);

		// Token: 0x04001210 RID: 4624
		private string mFrogStr;

		// Token: 0x04001211 RID: 4625
		private string mScoreStr;

		// Token: 0x02000144 RID: 324
		private enum ButtonState
		{
			// Token: 0x04001A7E RID: 6782
			AdvStats_Btn,
			// Token: 0x04001A7F RID: 6783
			HardAdvStats_Btn,
			// Token: 0x04001A80 RID: 6784
			Challenge_Btn,
			// Token: 0x04001A81 RID: 6785
			IronFrog_Btn,
			// Token: 0x04001A82 RID: 6786
			MoreStats_Btn,
			// Token: 0x04001A83 RID: 6787
			Back_Btn,
			// Token: 0x04001A84 RID: 6788
			Next_Btn,
			// Token: 0x04001A85 RID: 6789
			Prev_Btn
		}
	}
}
