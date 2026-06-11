using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000056 RID: 86
	public class TikiTemple : Widget, ButtonListener
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x00057FD8 File Offset: 0x000561D8
		public TikiTemple()
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame") && !GameApp.gApp.mResourceManager.LoadResources("CommonGame"))
			{
				GameApp.gApp.Shutdown();
			}
			this.mDisplayMode = -1;
			this.mClip = false;
			this.mSelectedScreenState = 0;
			this.mHomeButton = null;
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

		// Token: 0x06000A19 RID: 2585 RVA: 0x00058180 File Offset: 0x00056380
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0005818C File Offset: 0x0005638C
		public void Init()
		{
			this.mTikiTemplePages = new TikiTemplePages(this);
			this.mTikiTempleScrollWidget = new ScrollWidget();
			this.mTikiTempleScrollWidget.Resize(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)) - GameApp.gApp.mWideScreenXOffset + Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES)), this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30, this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight());
			this.mTikiTempleScrollWidget.SetScrollMode((ScrollWidget.ScrollMode)1);
			this.mTikiTempleScrollWidget.EnableBounce(true);
			this.mTikiTempleScrollWidget.EnablePaging(true);
			this.mTikiTempleScrollWidget.AddWidget(this.mTikiTemplePages);
			this.mTikiTemplePageControl = new PageControl(this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);
			this.IMAGE_UI_CHALLENGE_PAGE_INDICATOR.GetCelWidth();
			this.mTikiTemplePages.NumPages();
			this.mTikiTemplePageControl.SetNumberOfPages(this.mTikiTemplePages.NumPages());
			this.mTikiTemplePageControl.Move((int)this.mTitleXOffset + (this.mWidth - this.mTikiTemplePageControl.mWidth) / 2, Common._DS(145));
			this.mTikiTemplePageControl.SetCurrentPage(0);
			this.AddWidget(this.mTikiTemplePageControl);
			this.mTikiTempleScrollWidget.SetPageControl(this.mTikiTemplePageControl);
			this.AddWidget(this.mTikiTempleScrollWidget);
			this.mTikiTempleScrollWidget.SetPageHorizontal(0, false);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000582EC File Offset: 0x000564EC
		public override void Update()
		{
			if (!GameApp.gApp.mBambooTransition.IsInProgress() && this.mNeedsInitScroll)
			{
				this.mTikiTempleScrollWidget.SetPageHorizontal(0, true);
				this.mNeedsInitScroll = false;
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mTikiTempleScrollWidget.SetVisible(false);
				return;
			}
			this.mTikiTempleScrollWidget.SetVisible(true);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0005835C File Offset: 0x0005655C
		public float GetTitleXOffset()
		{
			return this.mTitleXOffset;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00058364 File Offset: 0x00056564
		public override void Draw(SexyGraphics g)
		{
			if (g != null)
			{
				g.Get3D();
			}
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
			g.Translate(-this.mX / 2, 0);
			base.DeferOverlay(9);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00058588 File Offset: 0x00056788
		public override void DrawOverlay(SexyGraphics g)
		{
			g.Translate(this.mX / 2, 0);
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, -GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE, GameApp.gApp.GetScreenWidth() + GameApp.gApp.mWideScreenXOffset - this.IMAGE_UI_CHALLENGESCREEN_BG_SIDE.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE)));
			g.DrawImageMirror(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset - Common._DS(30), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1.GetHeight() + Common._DS(15));
			g.DrawImage(this.IMAGE_GUI_TIKITEMPLE_PEDESTAL, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset - Common._DS(20) + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2.GetHeight() - Common._DS(15));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) - GameApp.gApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1)) + Common._DS(120));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2)) + Common._DS(120));
			g.SetColor(255, 255, 255, 255);
			g.SetFont(Res.GetFontByID(ResID.FONT_SHAGEXOTICA100_GAUNTLET));
			string @string = TextManager.getInstance().getString(781);
			float num = (float)g.GetFont().StringWidth(@string);
			g.DrawString(@string, (int)this.mTitleXOffset + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD)) - GameApp.gApp.mWideScreenXOffset + (int)(((float)this.IMAGE_UI_CHALLENGESCREEN_WOOD.GetWidth() - num) / 2f), Common._DS(135));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DRUMS, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)) - GameApp.gApp.mWideScreenXOffset + this.mAspectOffset + 85, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS)));
			g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_FRUIT, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)) - GameApp.gApp.mWideScreenXOffset - 66, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT)));
			g.DrawImage(this.IMAGE_UI_LEADERBOARDS_LEAVES2, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)) - GameApp.gApp.mWideScreenXOffset + GameApp.gApp.GetScreenRect().mX / 2 + this.mAspectOffset + 10, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2)));
			g.Translate(-this.mX / 2, 0);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00058896 File Offset: 0x00056A96
		public void ProcessHardwareBackButton()
		{
			GameApp.gApp.ToggleBambooTransition();
			GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.mMainMenu.HideTikiTemple);
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000588D0 File Offset: 0x00056AD0
		public void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null)
			{
				GameApp.gApp.mBambooTransition.IsInProgress();
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000588EE File Offset: 0x00056AEE
		public void ButtonPress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			GameApp.gApp.PlaySample(1768);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0005891D File Offset: 0x00056B1D
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0005891F File Offset: 0x00056B1F
		public void ButtonMouseEnter(int id)
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00058921 File Offset: 0x00056B21
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00058923 File Offset: 0x00056B23
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00058925 File Offset: 0x00056B25
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x040011C7 RID: 4551
		private int mSelectedScreenState;

		// Token: 0x040011C8 RID: 4552
		protected ButtonWidget mHomeButton;

		// Token: 0x040011C9 RID: 4553
		protected int mDisplayMode;

		// Token: 0x040011CA RID: 4554
		protected int mBounceCount;

		// Token: 0x040011CB RID: 4555
		protected TikiTemplePages mTikiTemplePages;

		// Token: 0x040011CC RID: 4556
		protected PageControl mTikiTemplePageControl;

		// Token: 0x040011CD RID: 4557
		protected ScrollWidget mTikiTempleScrollWidget;

		// Token: 0x040011CE RID: 4558
		protected bool mNeedsInitScroll;

		// Token: 0x040011CF RID: 4559
		protected float mTitleXOffset;

		// Token: 0x040011D0 RID: 4560
		protected int mAspectOffset = 30;

		// Token: 0x040011D1 RID: 4561
		protected Image IMAGE_UI_CHALLENGESCREEN_HOME_SELECT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);

		// Token: 0x040011D2 RID: 4562
		protected Image IMAGE_UI_CHALLENGESCREEN_HOME = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);

		// Token: 0x040011D3 RID: 4563
		protected Image IMAGE_UI_CHALLENGE_PAGE_INDICATOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGE_PAGE_INDICATOR);

		// Token: 0x040011D4 RID: 4564
		protected Image IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_CEILING_PIECE);

		// Token: 0x040011D5 RID: 4565
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_FLOOR = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_FLOOR);

		// Token: 0x040011D6 RID: 4566
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_END);

		// Token: 0x040011D7 RID: 4567
		protected Image IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_POLE_BROKEN_UP);

		// Token: 0x040011D8 RID: 4568
		protected Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		// Token: 0x040011D9 RID: 4569
		protected Image IMAGE_UI_CHALLENGESCREEN_WOOD = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_WOOD);

		// Token: 0x040011DA RID: 4570
		protected Image IMAGE_UI_LEADERBOARDS_LEAVES2 = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_LEAVES2);

		// Token: 0x040011DB RID: 4571
		protected Image IMAGE_UI_CHALLENGESCREEN_BG_SIDE = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_BG_SIDE);

		// Token: 0x040011DC RID: 4572
		protected Image IMAGE_GUI_TIKITEMPLE_PEDESTAL = Res.GetImageByID(ResID.IMAGE_GUI_TIKITEMPLE_PEDESTAL);

		// Token: 0x040011DD RID: 4573
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_1);

		// Token: 0x040011DE RID: 4574
		protected Image IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2 = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TIKI_POLE_2);

		// Token: 0x040011DF RID: 4575
		protected Image IMAGE_UI_CHALLENGESCREEN_DRUMS = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DRUMS);

		// Token: 0x040011E0 RID: 4576
		protected Image IMAGE_UI_CHALLENGESCREEN_FRUIT = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_FRUIT);

		// Token: 0x040011E1 RID: 4577
		protected Image IMAGE_UI_CHALLENGESCREEN_HOME_BACKING = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_BACKING);

		// Token: 0x040011E2 RID: 4578
		public float mXOff;

		// Token: 0x0200014C RID: 332
		private enum ButtonState
		{
			// Token: 0x04001AB7 RID: 6839
			AdvStats_Btn,
			// Token: 0x04001AB8 RID: 6840
			HardAdvStats_Btn,
			// Token: 0x04001AB9 RID: 6841
			Challenge_Btn,
			// Token: 0x04001ABA RID: 6842
			IronFrog_Btn,
			// Token: 0x04001ABB RID: 6843
			MoreStats_Btn,
			// Token: 0x04001ABC RID: 6844
			Back_Btn,
			// Token: 0x04001ABD RID: 6845
			Next_Btn,
			// Token: 0x04001ABE RID: 6846
			Prev_Btn
		}
	}
}
