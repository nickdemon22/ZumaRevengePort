using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000105 RID: 261
	public class CheatWidget : Widget
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x0009F200 File Offset: 0x0009D400
		public CheatWidget(Widget theTarget, string theCheats, Font theFont)
		{
			float num = (GameApp.mGameRes == 768) ? 1f : ((GameApp.mGameRes == 640) ? 2f : 1.5f);
			this.mButtonSize = (int)((float)CheatWidget.BUTTON_SIZE * num);
			this.mClient = theTarget;
			this.mCheatChars = theCheats;
			int length = theCheats.Length;
			this.mButtonsPerRow = (GameApp.gApp.GetScreenRect().mWidth + GameApp.gApp.GetScreenRect().mX - GameApp.gApp.mBoardOffsetX * 2) / this.mButtonSize;
			this.mCols = this.mButtonsPerRow;
			this.mRows = (length + this.mButtonsPerRow - 1) / this.mButtonsPerRow;
			this.mWidth = this.mCols * this.mButtonSize + 1;
			this.mHeight = this.mRows * this.mButtonSize + 1;
			this.mAlignment = true;
			this.mEnable = true;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0009F2F8 File Offset: 0x0009D4F8
		public override void Draw(SexyGraphics g)
		{
			if (!this.mEnable)
			{
				return;
			}
			g.SetColor(new Color(255, 200));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			int num = 0;
			for (int i = 0; i < this.mRows; i++)
			{
				for (int j = 0; j < this.mCols; j++)
				{
					Rect rect;
					rect = new Rect(j * this.mButtonSize + 1, i * this.mButtonSize + 1, this.mButtonSize - 2, this.mButtonSize - 2);
					g.SetColor(20, 20, 20);
					g.FillRect(rect);
				}
				int num2 = 0;
				while (num < this.mCheatChars.Length && num2 < this.mCols)
				{
					((GameMain)GameApp.gApp.mGameMain).DrawSysString(string.Concat(this.mCheatChars[num]), (float)(num2 * this.mButtonSize + this.mButtonSize / 2 - 10) * 800f / 1066f, (float)(i * this.mButtonSize + this.mY + this.mButtonSize / 2 - 10) * 800f / 1066f);
					num2++;
					num++;
				}
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0009F440 File Offset: 0x0009D640
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (!this.mEnable)
			{
				return;
			}
			int num = y / this.mButtonSize;
			int num2 = x / this.mButtonSize;
			int num3 = num * this.mButtonsPerRow + num2;
			if (num3 < this.mCheatChars.Length)
			{
				if (this.mCheatChars[num3] == 'X')
				{
					GameApp.gApp.mStepMode = 0;
					GameApp.gApp.ClearUpdateBacklog(false);
					this.mEnable = false;
					this.SetVisible(false);
					return;
				}
				if (this.mCheatChars[num3] == 'j')
				{
					this.SwapAlignment();
					return;
				}
				this.mClient.KeyChar(this.mCheatChars[num3]);
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0009F4E4 File Offset: 0x0009D6E4
		public override void MouseUp(int x, int y, int theClickCount)
		{
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0009F4E8 File Offset: 0x0009D6E8
		public void SwapAlignment()
		{
			if (this.mAlignment)
			{
				this.Move(this.mX, GameApp.gApp.GetScreenRect().mHeight - this.mHeight);
				this.mAlignment = false;
				return;
			}
			this.Move(this.mX, 0);
			this.mAlignment = true;
		}

		// Token: 0x040018DC RID: 6364
		public string mCheatChars;

		// Token: 0x040018DD RID: 6365
		public Widget mClient;

		// Token: 0x040018DE RID: 6366
		public int mRows;

		// Token: 0x040018DF RID: 6367
		public int mCols;

		// Token: 0x040018E0 RID: 6368
		public int mButtonsPerRow;

		// Token: 0x040018E1 RID: 6369
		public int mButtonSize;

		// Token: 0x040018E2 RID: 6370
		public bool mAlignment;

		// Token: 0x040018E3 RID: 6371
		public bool mEnable;

		// Token: 0x040018E4 RID: 6372
		private static int BUTTON_SIZE = Common._DS(80);
	}
}
