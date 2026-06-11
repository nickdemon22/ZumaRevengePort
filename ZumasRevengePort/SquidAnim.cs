using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000E7 RID: 231
	public class SquidAnim
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0009A5F5 File Offset: 0x000987F5
		public SquidAnim()
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0009A608 File Offset: 0x00098808
		public SquidAnim(SquidAnim rhs)
		{
			this.mImage = rhs.mImage;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mCurCel = rhs.mCurCel;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			for (int i = 0; i < rhs.mCels.Count; i++)
			{
				SquidAnimCel squidAnimCel = new SquidAnimCel();
				squidAnimCel.mCelNum = rhs.mCels[i].mCelNum;
				squidAnimCel.mDelay = rhs.mCels[i].mDelay;
				this.mCels.Add(squidAnimCel);
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0009A6B8 File Offset: 0x000988B8
		public void AddAnimInfo(int cel_num, int delay)
		{
			SquidAnimCel squidAnimCel = new SquidAnimCel();
			this.mCels.Add(squidAnimCel);
			squidAnimCel.mCelNum = cel_num;
			squidAnimCel.mDelay = delay;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0009A6E8 File Offset: 0x000988E8
		public void Update()
		{
			if (++this.mUpdateCount >= this.mCels[this.mCurCel].mDelay)
			{
				this.mUpdateCount = 0;
				this.mCurCel = (this.mCurCel + 1) % this.mCels.Count;
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0009A73F File Offset: 0x0009893F
		public void Draw(SexyGraphics g, float x, float y)
		{
			g.DrawImageCel(this.mImage, (int)(x + Common._S(this.mX)), (int)(y + Common._S(this.mY)), this.mCels[this.mCurCel].mCelNum);
		}

		// Token: 0x0400180F RID: 6159
		public Image mImage;

		// Token: 0x04001810 RID: 6160
		public List<SquidAnimCel> mCels = new List<SquidAnimCel>();

		// Token: 0x04001811 RID: 6161
		public int mUpdateCount;

		// Token: 0x04001812 RID: 6162
		public int mCurCel;

		// Token: 0x04001813 RID: 6163
		public float mX;

		// Token: 0x04001814 RID: 6164
		public float mY;
	}
}
