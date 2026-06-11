using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200001E RID: 30
	public class Upsell : Widget, ButtonListener, IDisposable
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0003B5C8 File Offset: 0x000397C8
		~Upsell()
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0003B5F0 File Offset: 0x000397F0
		public override void Dispose()
		{
			base.RemoveAllWidgets(true, false);
			this.mBuyBtn.Dispose();
			this.mMenuBtn.Dispose();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0003B610 File Offset: 0x00039810
		public Upsell(bool from_exit)
		{
			this.mClip = false;
			this.mPriority = (this.mZOrder = int.MaxValue);
			Upsell.gZoomStart = Common._M(4f);
			this.mBlock2X = (float)(GameApp.gApp.mWidth + Common._DS(160));
			this.mState = 1;
			this.mZoom = Upsell.gZoomStart;
			this.mMenuBtn = new ButtonWidget(1, this);
			this.mMenuBtn.mDoFinger = true;
			this.mMenuBtn.mNormalRect = this.mMenuBtn.mButtonImage.GetCelRect(0);
			this.mMenuBtn.mOverRect = this.mMenuBtn.mButtonImage.GetCelRect(1);
			this.mMenuBtn.mDownRect = this.mMenuBtn.mButtonImage.GetCelRect(2);
			this.AddWidget(this.mMenuBtn);
			this.mBuyBtn = new ButtonWidget(2, this);
			this.mBuyBtn.mDoFinger = true;
			this.mBuyBtn.mNormalRect = this.mBuyBtn.mButtonImage.GetCelRect(0);
			this.mBuyBtn.mOverRect = this.mBuyBtn.mButtonImage.GetCelRect(1);
			this.mBuyBtn.mDownRect = this.mBuyBtn.mButtonImage.GetCelRect(2);
			this.AddWidget(this.mBuyBtn);
			this.mScreenshotTimer = Upsell.gScreenshotTimer;
			this.mScreenshotIdx = 0;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0003B780 File Offset: 0x00039980
		public override void Update()
		{
			float num = Common._M(50f);
			if (this.mState == 1)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					this.mBlock1X += num;
					int num2 = 0;
					if (this.mBlock1X >= (float)num2)
					{
						this.mBlock1X = (float)num2;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 2)
			{
				this.mUpdateCnt++;
				int num3 = 0;
				if (this.mUpdateCnt >= Common._M(25))
				{
					this.mBlock2X -= num;
					if (this.mBlock2X <= (float)num3)
					{
						this.mBlock2X = (float)num3;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 3)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					int num4 = Common._M(20);
					float num5 = (Upsell.gZoomStart - 1f) / (float)num4;
					this.mZoom -= num5;
					if (this.mZoom <= 1f)
					{
						this.mZoom = 1f;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 4)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					int num6 = Common._M(15);
					this.mMenuBtn.Move(this.mMenuBtn.mX, this.mMenuBtn.mY - num6);
					this.mBuyBtn.Move(this.mBuyBtn.mX, this.mBuyBtn.mY - num6);
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					if (this.mMenuBtn.mY <= num7)
					{
						this.mMenuBtn.mY = num7;
						num9++;
					}
					if (this.mBuyBtn.mY <= num8)
					{
						this.mBuyBtn.mY = num8;
						num9++;
					}
					if (num9 == 2)
					{
						this.mState++;
					}
				}
			}
			else if (this.mState == 5)
			{
				this.mScreenshotTimer--;
				if (this.mScreenshotTimer == 0)
				{
					this.mScreenshotTimer = Upsell.gScreenshotTimer;
					this.mScreenshotIdx = (this.mScreenshotIdx + 1) % Upsell.MAX_SCREENSHOTS;
				}
			}
			if (this.mState < 5 || this.mScreenshotTimer <= Upsell.gScreenshotFade)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0003BA26 File Offset: 0x00039C26
		public override void Draw(SexyGraphics g)
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0003BA28 File Offset: 0x00039C28
		public void ButtonDepress(int id)
		{
			if (id == this.mMenuBtn.mId && this.mFromExit)
			{
				GameApp.gApp.mDoingDRM = false;
				GameApp.gApp.Shutdown();
				return;
			}
			if (id == this.mMenuBtn.mId)
			{
				Board mBoard = GameApp.gApp.mBoard;
				GameApp.gApp.mWidgetManager.RemoveWidget(this);
				GameApp.gApp.mDoingDRM = false;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0003BA98 File Offset: 0x00039C98
		public virtual void ButtonPress(int id)
		{
			if (id == this.mMenuBtn.mId && this.mFromExit)
			{
				GameApp.gApp.mDoingDRM = false;
				GameApp.gApp.Shutdown();
				return;
			}
			if (id == this.mMenuBtn.mId)
			{
				Board mBoard = GameApp.gApp.mBoard;
				GameApp.gApp.mWidgetManager.RemoveWidget(this);
				GameApp.gApp.mDoingDRM = false;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0003BB05 File Offset: 0x00039D05
		public virtual void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0003BB07 File Offset: 0x00039D07
		public virtual void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0003BB09 File Offset: 0x00039D09
		public virtual void ButtonMouseEnter(int theId)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0003BB0B File Offset: 0x00039D0B
		public virtual void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0003BB0D File Offset: 0x00039D0D
		public virtual void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x04000B81 RID: 2945
		private static float gZoomStart = 4f;

		// Token: 0x04000B82 RID: 2946
		private static int gScreenshotTimer = 300;

		// Token: 0x04000B83 RID: 2947
		private static int gScreenshotFade = 50;

		// Token: 0x04000B84 RID: 2948
		private static readonly int MAX_SCREENSHOTS = 9;

		// Token: 0x04000B85 RID: 2949
		protected float mBlock1X;

		// Token: 0x04000B86 RID: 2950
		protected float mBlock2X;

		// Token: 0x04000B87 RID: 2951
		protected float mZoom;

		// Token: 0x04000B88 RID: 2952
		protected ButtonWidget mMenuBtn;

		// Token: 0x04000B89 RID: 2953
		protected ButtonWidget mBuyBtn;

		// Token: 0x04000B8A RID: 2954
		protected int mState;

		// Token: 0x04000B8B RID: 2955
		protected int mScreenshotIdx;

		// Token: 0x04000B8C RID: 2956
		protected int mScreenshotTimer;

		// Token: 0x04000B8D RID: 2957
		public bool mFromExit;
	}
}
