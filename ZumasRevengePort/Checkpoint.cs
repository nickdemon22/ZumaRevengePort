using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000069 RID: 105
	public class Checkpoint : Widget, ButtonListener, IDisposable
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00068F00 File Offset: 0x00067100
		public Checkpoint(Level l, int score, bool game_over)
		{
			this.mScore = score;
			this.mFromGameOver = game_over;
			this.mDone = false;
			this.mContinuePressed = false;
			this.mPostcardGroupName = "";
			this.mState = 1;
			this.mAlpha = 0f;
			this.mSize = Common._DS(Common._M(8f));
			this.mClip = false;
			this.mShowMap = false;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARD_BACK);
			this.mPostCardX = (GameApp.gApp.GetScreenRect().mWidth - imageByID.mWidth) / 2 + GameApp.gApp.mWideScreenXOffset;
			this.mPostCardY = (GameApp.gApp.GetScreenRect().mHeight - imageByID.mHeight) / 2;
			if (this.mFromGameOver)
			{
				int num = GameApp.gApp.GetLevelMgr().GetLevelIndex(l.mId);
				if (l.mNum <= 5)
				{
					num -= l.mNum - 1;
				}
				else
				{
					num -= l.mNum - 6;
				}
				Level levelByIndex = GameApp.gApp.GetLevelMgr().GetLevelByIndex(num);
				this.mZone = LevelMgr.GetZoneName(levelByIndex.mZone - 1);
				this.mLevelNum = (levelByIndex.mZone - 1) * 10 + levelByIndex.mNum;
				int num2 = (GameApp.gApp.GetScreenRect().mWidth + Common._S(Common._M(160)) - Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_BASETEXT).mWidth) / 2;
				int num3 = (GameApp.gApp.GetScreenRect().mHeight - Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_BASETEXT).mHeight) / 2;
				this.mButtons[0] = new ButtonWidget(0, this);
				this.mButtons[0].mDoFinger = true;
				this.mButtons[0].mButtonImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_CONTINUE_BUTTON);
				this.mButtons[0].mDownImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_CONTINUE_BUTTON_CLICK);
				this.mButtons[0].mOverImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_CONTINUE_BUTTON_CLICK);
				this.mButtons[0].mDisabled = true;
				this.mButtons[0].Resize(this.mPostCardX + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_CONTINUE_BUTTON)), this.mPostCardY + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_CONTINUE_BUTTON)), this.mButtons[0].mOverImage.mWidth, this.mButtons[0].mOverImage.mHeight);
				this.AddWidget(this.mButtons[0]);
				this.mButtons[1] = new ButtonWidget(1, this);
				this.mButtons[1].mDoFinger = true;
				this.mButtons[1].mButtonImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_MM_BUTTON);
				this.mButtons[1].mDownImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_MM_BUTTON_CLICK);
				this.mButtons[1].mOverImage = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_MM_BUTTON_CLICK);
				this.mButtons[1].mDisabled = true;
				this.mButtons[1].Resize(this.mPostCardX + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_MM_BUTTON_CLICK)), this.mPostCardY + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_MM_BUTTON_CLICK)), this.mButtons[1].mOverImage.mWidth, this.mButtons[1].mOverImage.mHeight);
				this.AddWidget(this.mButtons[1]);
				return;
			}
			this.mZone = LevelMgr.GetZoneName(l.mZone);
			this.mLevelNum = (l.mZone - 1) * 10 + l.mNum;
			if (l.mBoss != null)
			{
				this.mBossName = "\"" + l.mBoss.mName + "\"";
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000692BE File Offset: 0x000674BE
		public override void Dispose()
		{
			base.RemoveAllWidgets(true, false);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000692C8 File Offset: 0x000674C8
		public override void Update()
		{
			int num = Common._M(75);
			this.mUpdateCnt++;
			if (this.mState == 1)
			{
				float num2 = 128f / (float)num;
				this.mAlpha += num2;
				if (this.mAlpha > 128f)
				{
					this.mAlpha = 128f;
				}
				num2 = Common._M(7f) / (float)num;
				this.mSize -= num2;
				if (this.mSize < 1f)
				{
					this.mSize = 1f;
				}
				if (this.mUpdateCnt >= num)
				{
					if (this.mFromGameOver)
					{
						for (int i = 0; i < 2; i++)
						{
							if (this.mButtons[i] != null)
							{
								this.mButtons[i].mDisabled = false;
							}
						}
					}
					this.mState = 0;
					return;
				}
			}
			else if (this.mState == -1)
			{
				float num3 = 128f / (float)num;
				this.mAlpha -= num3;
				if (this.mAlpha <= 0f)
				{
					this.mAlpha = 0f;
					this.mDone = true;
					this.mState = 0;
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000693E0 File Offset: 0x000675E0
		public override void Draw(SexyGraphics g)
		{
			int num = (int)(this.mAlpha * 2f);
			if (num > 255)
			{
				num = 255;
			}
			else if (num < 0)
			{
				num = 0;
			}
			if (num != 255)
			{
				g.SetColorizeImages(true);
			}
			g.SetColor(255, 255, 255, num);
			GameApp gApp = GameApp.gApp;
			if (!this.mFromGameOver)
			{
				Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
				Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
				int num2 = gApp.GetScreenRect().mWidth / 2;
				int num3 = this.mHeight / 2;
				g.SetFont(fontByID);
				g.SetColor(255, 255, 255, num);
				string @string = TextManager.getInstance().getString(432);
				g.WriteString(@string, 0, num3 - g.GetFont().GetHeight() - Common._S(Common._M(-30)), this.mWidth, 0);
				int num4 = Common._S(Common._M(20));
				g.SetFont(fontByID2);
				g.SetColor(Common._M(240), Common._M1(200), Common._M2(0), num);
				g.WriteString(this.mZone, num2, num3 + Common._S(Common._M(35)), this.mWidth - num4 * 2, -1);
				g.WriteString((this.mLevelNum < int.MaxValue) ? (TextManager.getInstance().getString(683) + " " + this.mLevelNum) : this.mBossName, 0, num3 + Common._S(Common._M(15)), this.mWidth - num4 * 2, 0);
				g.WriteString(Common.CommaSeperate(this.mScore), num2, num3 + Common._S(Common._M(30)), this.mWidth - num4 * 2, 1);
				g.DrawString(TextManager.getInstance().getString(433), num2, num3 + Common._S(75));
				g.SetFont(fontByID2);
				g.SetColor(Common._M(255), Common._M1(0), Common._M2(0), num);
				g.WriteString(TextManager.getInstance().getString(434), 0, num3 + Common._S(Common._M(60)), this.mWidth, 0);
			}
			else
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARD_BACK);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_BASETEXT);
				g.DrawImage(imageByID, this.mPostCardX, this.mPostCardY);
				g.DrawImage(imageByID2, this.mPostCardX + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_BASETEXT)), this.mPostCardY + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_CHECKPOINT_POSTCARDTEXT_BASETEXT)));
				int num5 = gApp.GetLevelMgr().GetLevelIndex(gApp.mBoard.mLevel.mId);
				if (gApp.mBoard.mLevel.mNum <= 5)
				{
					num5 -= gApp.mBoard.mLevel.mNum - 1;
				}
				else
				{
					num5 -= gApp.mBoard.mLevel.mNum - 6;
				}
				int num6 = gApp.mBoard.mLevel.mZone;
				int theLevelNum = this.mLevelNum - 1;
				g.PushState();
				Image levelThumbnail = gApp.GetLevelThumbnail(theLevelNum);
				float num7 = 2f;
				int value = 1225;
				int value2 = 332;
				int num8 = -27;
				int value3 = (int)(num7 * (float)levelThumbnail.mWidth);
				int value4 = (int)(num7 * (float)levelThumbnail.mHeight);
				if (GameApp.mGameRes != 768)
				{
					g.DrawImage(levelThumbnail, Common._DS(value) + gApp.GetScreenRect().mX, Common._DS(value2), Common._DS(value3), Common._DS(value4));
				}
				else
				{
					g.DrawImage(levelThumbnail, Common._DS(value) + gApp.GetScreenRect().mX + num8, Common._DS(value2), Common._DS(value3), Common._DS(value4));
				}
				Font fontByID3 = Res.GetFontByID(ResID.FONT_CHECKPOINT_CURSIVE);
				g.SetFont(fontByID3);
				g.SetColor(Common._M(0), Common._M1(0), Common._M2(0), num);
				int num9 = Common._S(Common._M(480));
				int num10 = Common._S(Common._M(320));
				g.DrawString(this.mZone, num9, num10);
				g.DrawString(TextManager.getInstance().getString(683) + " " + this.mLevelNum, num9, num10 + Common._S(Common._M(25)));
				g.DrawString(Common.CommaSeperate(this.mScore) + " pts", num9, num10 + Common._S(Common._M(50)));
			}
			g.SetColorizeImages(false);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00069888 File Offset: 0x00067A88
		public void ButtonDepress(int id)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (id == 1)
			{
				GameApp.gApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(GameApp.gApp.DoDeferredEndGame);
				GameApp.gApp.ToggleBambooTransition();
				return;
			}
			if (id == 0)
			{
				this.mContinuePressed = true;
				this.mDone = true;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000698F2 File Offset: 0x00067AF2
		public override void MouseDown(int x, int y, int cc)
		{
			if (this.mState != 0)
			{
				return;
			}
			if (!this.mFromGameOver)
			{
				this.mUpdateCnt = 0;
				this.mState = -1;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00069914 File Offset: 0x00067B14
		public void Disable(bool d)
		{
			this.SetDisabled(d);
			this.SetVisible(!d);
			for (int i = 0; i < 2; i++)
			{
				if (this.mButtons[i] != null)
				{
					this.mButtons[i].SetDisabled(d);
					this.mButtons[i].SetVisible(!d);
				}
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00069968 File Offset: 0x00067B68
		public virtual void PreDraw(SexyGraphics g)
		{
			g.SetColor(0, 0, 0, (int)this.mAlpha);
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			Graphics3D graphics3D = g.Get3D();
			if (!MathUtils._eq(this.mSize, 1f) && graphics3D != null)
			{
				this.mTransform.Translate((float)(-(float)GameApp.gApp.mWidth / 2), (float)(-(float)GameApp.gApp.mHeight / 2));
				this.mTransform.Scale(this.mSize, this.mSize);
				this.mTransform.Translate((float)(GameApp.gApp.mWidth / 2), (float)(GameApp.gApp.mHeight / 2));
				graphics3D.PushTransform(this.mTransform);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00069A2C File Offset: 0x00067C2C
		public override void DrawAll(ModalFlags theFlags, SexyGraphics g)
		{
			this.PreDraw(g);
			this.Draw(g);
			for (int i = 0; i < 2; i++)
			{
				if (this.mButtons[i] != null)
				{
					g.Translate(this.mButtons[i].mX, this.mButtons[i].mY);
					this.mButtons[i].Draw(g);
					g.Translate(-this.mButtons[i].mX, -this.mButtons[i].mY);
				}
			}
			this.PostDraw(g);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00069AB4 File Offset: 0x00067CB4
		public virtual void PostDraw(SexyGraphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if (!MathUtils._eq(this.mSize, 1f) && graphics3D != null)
			{
				graphics3D.PopTransform();
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00069AE3 File Offset: 0x00067CE3
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00069AE5 File Offset: 0x00067CE5
		public void ButtonMouseEnter(int theId)
		{
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00069AE7 File Offset: 0x00067CE7
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00069AE9 File Offset: 0x00067CE9
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00069AEB File Offset: 0x00067CEB
		public void ButtonPress(int theId)
		{
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00069AED File Offset: 0x00067CED
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x04001327 RID: 4903
		protected ButtonWidget[] mButtons = new ButtonWidget[2];

		// Token: 0x04001328 RID: 4904
		protected string mZone;

		// Token: 0x04001329 RID: 4905
		protected string mBossName;

		// Token: 0x0400132A RID: 4906
		protected string mPostcardGroupName = "";

		// Token: 0x0400132B RID: 4907
		protected int mScore;

		// Token: 0x0400132C RID: 4908
		protected float mAlpha;

		// Token: 0x0400132D RID: 4909
		protected float mSize;

		// Token: 0x0400132E RID: 4910
		protected int mState;

		// Token: 0x0400132F RID: 4911
		protected int mPostCardX;

		// Token: 0x04001330 RID: 4912
		protected int mPostCardY;

		// Token: 0x04001331 RID: 4913
		public int mLevelNum;

		// Token: 0x04001332 RID: 4914
		public bool mFromGameOver;

		// Token: 0x04001333 RID: 4915
		public bool mDone;

		// Token: 0x04001334 RID: 4916
		public bool mContinuePressed;

		// Token: 0x04001335 RID: 4917
		public bool mShowMap;

		// Token: 0x04001336 RID: 4918
		private SexyTransform2D mTransform = new SexyTransform2D(false);

		// Token: 0x020000BB RID: 187
		public enum ButtonId
		{
			// Token: 0x040016C0 RID: 5824
			Button_Continue,
			// Token: 0x040016C1 RID: 5825
			Button_MainMenu,
			// Token: 0x040016C2 RID: 5826
			Max_Buttons
		}
	}
}
