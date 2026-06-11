using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200008E RID: 142
	public class Bubble
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x00088880 File Offset: 0x00086A80
		public void Init(float vx, float vy, float jiggle_speed, int jiggle_timer)
		{
			this.mVX = vx;
			this.mVY = vy;
			this.mJiggleSpeed = jiggle_speed;
			this.mJiggleTimer = jiggle_timer;
			this.mDefJiggleTimer = jiggle_timer;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000888B4 File Offset: 0x00086AB4
		public void Update()
		{
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mX += this.mVX;
			this.mY += this.mVY;
			if (this.mJiggleLeft)
			{
				this.mX -= this.mJiggleSpeed;
			}
			else
			{
				this.mX += this.mJiggleSpeed;
			}
			if (--this.mJiggleTimer <= 0)
			{
				this.mJiggleLeft = !this.mJiggleLeft;
				this.mJiggleTimer = this.mDefJiggleTimer;
			}
			this.mAlpha -= this.mAlphaDec;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00088971 File Offset: 0x00086B71
		public void Draw(SexyGraphics g)
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00088973 File Offset: 0x00086B73
		public void SetX(float x)
		{
			this.mX = x;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0008897C File Offset: 0x00086B7C
		public void SetY(float y)
		{
			this.mY = y;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00088985 File Offset: 0x00086B85
		public void SetAlphaFade(float f)
		{
			this.mAlphaDec = f;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0008898E File Offset: 0x00086B8E
		public void SetDelay(int d)
		{
			this.mDelay = d;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00088997 File Offset: 0x00086B97
		public float GetAlpha()
		{
			return this.mAlpha;
		}

		// Token: 0x04001588 RID: 5512
		protected float mX;

		// Token: 0x04001589 RID: 5513
		protected float mY;

		// Token: 0x0400158A RID: 5514
		protected float mVX;

		// Token: 0x0400158B RID: 5515
		protected float mVY;

		// Token: 0x0400158C RID: 5516
		protected float mJiggleSpeed;

		// Token: 0x0400158D RID: 5517
		protected bool mJiggleLeft;

		// Token: 0x0400158E RID: 5518
		protected int mJiggleTimer;

		// Token: 0x0400158F RID: 5519
		protected int mDefJiggleTimer;

		// Token: 0x04001590 RID: 5520
		protected int mDelay;

		// Token: 0x04001591 RID: 5521
		protected float mAlpha;

		// Token: 0x04001592 RID: 5522
		protected float mAlphaDec;
	}
}
