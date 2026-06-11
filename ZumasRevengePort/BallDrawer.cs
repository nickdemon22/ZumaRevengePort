using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000060 RID: 96
	public class BallDrawer
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0005CA68 File Offset: 0x0005AC68
		public void Reset()
		{
			this.mMaxBallPriority = 0;
			for (int i = 0; i < 5; i++)
			{
				this.mNumBalls[i] = 0;
				this.mNumShadows[i] = 0;
				this.mNumOverlays[i] = 0;
				this.mNumUnderlays[i] = 0;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0005CAAC File Offset: 0x0005ACAC
		public void AddBall(Ball theBall, int thePriority)
		{
			int num = this.mNumBalls[thePriority]++;
			this.mBalls[thePriority, num] = theBall;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0005CAE4 File Offset: 0x0005ACE4
		public void AddShadow(Ball theBall, int thePriority)
		{
			int num = this.mNumShadows[thePriority]++;
			this.mShadows[thePriority, num] = theBall;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0005CB1C File Offset: 0x0005AD1C
		public void AddOverlay(Ball theBall, int thePriority)
		{
			int num = this.mNumOverlays[thePriority]++;
			this.mOverlays[thePriority, num] = theBall;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0005CB54 File Offset: 0x0005AD54
		public void AddUnderlay(Ball theBall, int thePriority)
		{
			int num = this.mNumUnderlays[thePriority]++;
			this.mUnderlays[thePriority, num] = theBall;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0005CB8C File Offset: 0x0005AD8C
		public void Draw(SexyGraphics g, Board theBoard)
		{
			g.Get3D();
			for (int i = 0; i < 5; i++)
			{
				if (i != 0)
				{
					theBoard.DrawTunnels(g, i, true);
				}
				theBoard.mLevel.DrawPriority(g, i);
				for (int j = 0; j < theBoard.mLevel.mNumCurves; j++)
				{
					theBoard.mLevel.mCurveMgr[j].DrawMisc(g, i);
					theBoard.mLevel.mCurveMgr[j].DrawSkullPathShit(g, i);
				}
				if (!Board.gHideBalls)
				{
					int num = this.mNumShadows[i];
					for (int j = 0; j < num; j++)
					{
						this.mShadows[i, j].DrawShadow(g);
					}
					theBoard.DrawTunnels(g, i, false);
					num = this.mNumUnderlays[i];
					for (int j = 0; j < num; j++)
					{
						this.mUnderlays[i, j].DrawBottomLayer(g);
					}
					num = this.mNumBalls[i];
					for (int j = 0; j < num; j++)
					{
						this.mBalls[i, j].DrawBase(g, 0, 0);
					}
					for (int j = 0; j < num; j++)
					{
						this.mBalls[i, j].DrawAdditive(g, 0, 0);
					}
					if (g.Is3D())
					{
						num = this.mNumOverlays[i];
						for (int j = 0; j < num; j++)
						{
							this.mOverlays[i, j].DrawTopLayer(g);
						}
					}
				}
			}
			for (int j = 0; j < theBoard.mLevel.mNumCurves; j++)
			{
				theBoard.mLevel.mCurveMgr[j].DrawAboveBalls(g);
			}
		}

		// Token: 0x0400126C RID: 4716
		public int mMaxBallPriority;

		// Token: 0x0400126D RID: 4717
		public int[] mNumBalls = new int[5];

		// Token: 0x0400126E RID: 4718
		public int[] mNumShadows = new int[5];

		// Token: 0x0400126F RID: 4719
		public int[] mNumOverlays = new int[5];

		// Token: 0x04001270 RID: 4720
		public int[] mNumUnderlays = new int[5];

		// Token: 0x04001271 RID: 4721
		private Ball[,] mBalls = new Ball[5, 1024];

		// Token: 0x04001272 RID: 4722
		private Ball[,] mShadows = new Ball[5, 1024];

		// Token: 0x04001273 RID: 4723
		private Ball[,] mOverlays = new Ball[5, 1024];

		// Token: 0x04001274 RID: 4724
		private Ball[,] mUnderlays = new Ball[5, 1024];
	}
}
