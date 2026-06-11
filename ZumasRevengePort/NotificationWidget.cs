using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200007B RID: 123
	public class NotificationWidget : Widget
	{
		// Token: 0x06000BEF RID: 3055 RVA: 0x000766E0 File Offset: 0x000748E0
		public NotificationWidget(Board theBoard, string theStringInfo)
		{
			this.mBoard = theBoard;
			this.mDisplayTime = 2000UL;
			this.mDisplayStart = ulong.MaxValue;
			this.mSoundID = -1;
			this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_ON;
			this.mIsFinished = false;
			this.mSlideVal.mAppUpdateCountSrc = this.mBoard.mUpdateCnt;
			this.mSlideVal.SetCurve(Common._MP("b70,1,0.02,1,#     $P    }~"));
			this.mFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW);
			this.mNotification = theStringInfo;
			this.mNotificationStringWidth = this.mFont.StringWidth(theStringInfo);
			int num = Common._DS(600);
			int num2 = Common._DS(100);
			int num3 = (this.mNotificationStringWidth + num2 > num) ? (this.mNotificationStringWidth + num2) : num;
			int num4 = Common._DS(150);
			this.mYStart = (float)(GameApp.gApp.GetScreenRect().mHeight + num4);
			this.mYEnd = (float)(GameApp.gApp.GetScreenRect().mHeight - num4 / 2);
			this.Resize(GameApp.gApp.GetScreenRect().mX + (GameApp.gApp.GetScreenRect().mWidth - num3) / 2, (int)this.mYStart, num3, num4);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00076818 File Offset: 0x00074A18
		public override void Draw(SexyGraphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight * 2);
			g.SetFont(this.mFont);
			g.SetColor(Color.White);
			if ((int)Localization.GetCurrentLanguage() == 5 || (int)Localization.GetCurrentLanguage() == 10)
			{
				g.DrawString(this.mNotification, (this.mWidth - this.mNotificationStringWidth) / 2, Common._DS(60) + 3);
				return;
			}
			g.DrawString(this.mNotification, (this.mWidth - this.mNotificationStringWidth) / 2, Common._DS(60));
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000768AC File Offset: 0x00074AAC
		public override void Update()
		{
			switch (this.mSlideState)
			{
			case NotificationWidget.SLIDE_STATE.SLIDE_ON:
				if (this.mSlideVal.IsDoingCurve())
				{
					float num = (float)this.mSlideVal.GetOutVal() * (this.mYEnd - this.mYStart);
					this.Move(this.mX, (int)(this.mYStart + num));
					return;
				}
				this.PlaySound();
				this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_ONSCREEN;
				this.mDisplayStart = (ulong)Common.SexyTime();
				return;
			case NotificationWidget.SLIDE_STATE.SLIDE_ONSCREEN:
			{
				ulong num2 = (ulong)Common.SexyTime();
				if (num2 - this.mDisplayStart >= this.mDisplayTime)
				{
					this.mSlideVal.SetCurve(Common._MP("b70,1,0.02,1,#     $P    }~"));
					this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_OFF;
					this.mYStart = (float)this.mY;
					this.mYEnd = (float)(GameApp.gApp.GetScreenRect().mHeight + this.mHeight);
					return;
				}
				break;
			}
			case NotificationWidget.SLIDE_STATE.SLIDE_OFF:
				if (this.mSlideVal.IsDoingCurve())
				{
					float num3 = (float)this.mSlideVal.GetOutVal() * (this.mYEnd - this.mYStart);
					this.Move(this.mX, (int)(this.mYStart + num3));
					return;
				}
				this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_OFFSCREEN;
				return;
			case NotificationWidget.SLIDE_STATE.SLIDE_OFFSCREEN:
				this.mIsFinished = true;
				break;
			default:
				return;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000769DC File Offset: 0x00074BDC
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseDown(x, y, theClickCount);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000769F5 File Offset: 0x00074BF5
		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseDown(x, y, theClickCount);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00076A0E File Offset: 0x00074C0E
		public override void MouseMove(int x, int y)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseMove(x, y);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00076A26 File Offset: 0x00074C26
		public override void MouseDrag(int x, int y)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseMove(x, y);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00076A3E File Offset: 0x00074C3E
		public bool IsFinished()
		{
			return this.mIsFinished;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00076A46 File Offset: 0x00074C46
		private void PlaySound()
		{
			if (this.mSoundID == -1)
			{
				return;
			}
			this.mBoard.mApp.mSoundPlayer.Play(this.mSoundID);
			this.mSoundID = -1;
		}

		// Token: 0x0400141B RID: 5147
		private Board mBoard;

		// Token: 0x0400141C RID: 5148
		private float mYStart;

		// Token: 0x0400141D RID: 5149
		private float mYEnd;

		// Token: 0x0400141E RID: 5150
		private ulong mDisplayTime;

		// Token: 0x0400141F RID: 5151
		private ulong mDisplayStart;

		// Token: 0x04001420 RID: 5152
		private NotificationWidget.SLIDE_STATE mSlideState;

		// Token: 0x04001421 RID: 5153
		private bool mIsFinished;

		// Token: 0x04001422 RID: 5154
		private CurvedVal mSlideVal = new CurvedVal();

		// Token: 0x04001423 RID: 5155
		private string mNotification;

		// Token: 0x04001424 RID: 5156
		private int mNotificationStringWidth;

		// Token: 0x04001425 RID: 5157
		private Font mFont;

		// Token: 0x04001426 RID: 5158
		public int mSoundID;

		// Token: 0x0200010C RID: 268
		private enum SLIDE_STATE
		{
			// Token: 0x04001922 RID: 6434
			SLIDE_ON,
			// Token: 0x04001923 RID: 6435
			SLIDE_ONSCREEN,
			// Token: 0x04001924 RID: 6436
			SLIDE_OFF,
			// Token: 0x04001925 RID: 6437
			SLIDE_OFFSCREEN,
			// Token: 0x04001926 RID: 6438
			NUM_SLIDE_STATES
		}
	}
}
