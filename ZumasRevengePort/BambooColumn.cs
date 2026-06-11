using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000048 RID: 72
	public class BambooColumn
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00055DBF File Offset: 0x00053FBF
		public BambooColumn()
		{
			this.Reset();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00055DF0 File Offset: 0x00053FF0
		public void Reset()
		{
			this.IMAGE_BAMBOO_PIECE_A = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.IMAGE_BAMBOO_PIECE_B = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_B);
			this.IMAGE_BAMBOO_PIECE_C = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_C);
			this.IMAGE_BAMBOO_PIECE_D = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_D);
			this.mState = BambooColumn.BambooState.Init;
			float num = (float)GameApp.gApp.GetScreenRect().mHeight / 2f;
			float num2 = (float)(Common.Rand() % Common._DS(400) - Common._DS(200));
			this.mTopEnd.mFinalY = num + num2;
			this.mTopEnd.mY = (float)(-(float)this.IMAGE_BAMBOO_PIECE_C.GetHeight());
			this.mTopEnd.mVelocityY = (this.mTopEnd.mFinalY - this.mTopEnd.mY) / 20f;
			this.mBotEnd.mFinalY = this.mTopEnd.mFinalY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight();
			this.mBotEnd.mY = (float)(GameApp.gApp.GetScreenRect().mHeight + this.IMAGE_BAMBOO_PIECE_D.GetHeight());
			this.mBotEnd.mVelocityY = (this.mBotEnd.mFinalY - this.mBotEnd.mY) / 20f;
			this.mGravity = 0.1f;
			this.mSmoke.Clear();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00055F50 File Offset: 0x00054150
		public void Draw(SexyGraphics g)
		{
			this.mDrawed = true;
			g.DrawImage(this.IMAGE_BAMBOO_PIECE_C, (int)(this.mX + (float)Common._DS(4)), (int)this.mTopEnd.mY);
			float num = this.mTopEnd.mY;
			bool flag = false;
			while (num >= 0f)
			{
				Image image;
				if (flag)
				{
					image = this.IMAGE_BAMBOO_PIECE_B;
					num -= (float)image.GetHeight();
				}
				else
				{
					image = this.IMAGE_BAMBOO_PIECE_A;
					num -= (float)image.GetHeight();
				}
				g.DrawImage(image, (int)this.mX, (int)num);
				flag = !flag;
			}
			g.DrawImage(this.IMAGE_BAMBOO_PIECE_D, (int)this.mX, (int)this.mBotEnd.mY);
			float num2 = this.mBotEnd.mY;
			flag = false;
			while (num2 <= (float)GameApp.gApp.GetScreenRect().mHeight)
			{
				Image image2;
				if (flag)
				{
					image2 = this.IMAGE_BAMBOO_PIECE_B;
					num2 += (float)image2.GetHeight();
				}
				else
				{
					image2 = this.IMAGE_BAMBOO_PIECE_A;
					num2 += (float)image2.GetHeight();
				}
				g.DrawImage(image2, (int)this.mX, (int)num2);
				flag = !flag;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00056068 File Offset: 0x00054268
		public void DrawSmoke(SexyGraphics g)
		{
			if (Enumerable.Count<LTSmokeParticle>(this.mSmoke) > 0)
			{
				for (int i = 0; i < Enumerable.Count<LTSmokeParticle>(this.mSmoke); i++)
				{
					BambooTransition.DrawSmokeParticle(g, this.mSmoke[i]);
				}
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000560AC File Offset: 0x000542AC
		public void Update(bool sound)
		{
			switch (this.mState)
			{
			case BambooColumn.BambooState.Falling:
				this.mTopEnd.mY = this.mTopEnd.mY + this.mTopEnd.mVelocityY;
				this.mBotEnd.mY = this.mBotEnd.mY + this.mBotEnd.mVelocityY;
				if (this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() >= this.mBotEnd.mY)
				{
					this.mTopEnd.mY = this.mBotEnd.mY - (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() - 1f;
					this.mState = BambooColumn.BambooState.Bouncing;
					if (sound)
					{
						this.PlayBambooSound(0.2f);
					}
				}
				break;
			case BambooColumn.BambooState.Bouncing:
			{
				float num = -(this.mTopEnd.mVelocityY / Common._M(10f) - this.mGravity);
				float num2 = -(this.mBotEnd.mVelocityY / Common._M(10f) + this.mGravity);
				this.mTopEnd.mY = this.mTopEnd.mY + num;
				this.mBotEnd.mY = this.mBotEnd.mY + num2;
				this.mGravity += 0.1f;
				if (this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() >= this.mBotEnd.mY)
				{
					this.mTopEnd.mY = this.mBotEnd.mY - (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() + (float)Common._DS(7);
					this.mState = BambooColumn.BambooState.Closed;
					if (sound)
					{
						this.PlayBambooSound(0.1f);
					}
				}
				break;
			}
			case BambooColumn.BambooState.Opening:
			{
				this.mTopEnd.mY = this.mTopEnd.mY - this.mTopEnd.mVelocityY;
				this.mBotEnd.mY = this.mBotEnd.mY - this.mBotEnd.mVelocityY;
				bool flag = this.mTopEnd.mY + (float)this.IMAGE_BAMBOO_PIECE_C.GetHeight() < -20f;
				bool flag2 = this.mBotEnd.mY >= (float)(GameApp.gApp.GetScreenRect().mHeight + 20);
				if (flag && flag2 && this.mDrawed)
				{
					this.mState = BambooColumn.BambooState.Open;
				}
				break;
			}
			}
			this.mDrawed = false;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00056308 File Offset: 0x00054508
		public void UpdateSmokeParticle()
		{
			if (this.mState != BambooColumn.BambooState.Init && this.mState != BambooColumn.BambooState.Falling)
			{
				for (int i = 0; i < Enumerable.Count<LTSmokeParticle>(this.mSmoke); i++)
				{
					LTSmokeParticle s = this.mSmoke[i];
					if (BambooTransition.UpdateSmokeParticle(s))
					{
						this.mSmoke.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00056361 File Offset: 0x00054561
		public void SetColumnX(float theX)
		{
			this.mX = theX;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0005636A File Offset: 0x0005456A
		public void Close()
		{
			if (this.mState == BambooColumn.BambooState.Open)
			{
				this.Reset();
			}
			if (this.mState == BambooColumn.BambooState.Init)
			{
				this.mState = BambooColumn.BambooState.Falling;
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0005638A File Offset: 0x0005458A
		public void Open()
		{
			if (this.mState == BambooColumn.BambooState.Closed)
			{
				this.mState = BambooColumn.BambooState.Opening;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0005639C File Offset: 0x0005459C
		public bool IsClosed()
		{
			return this.mState == BambooColumn.BambooState.Closed;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000563A7 File Offset: 0x000545A7
		public bool IsOpened()
		{
			return this.mState == BambooColumn.BambooState.Open;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000563B2 File Offset: 0x000545B2
		public float GetColumnX()
		{
			return this.mX;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000563BA File Offset: 0x000545BA
		public float GetCollisionY()
		{
			return this.mTopEnd.mFinalY;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000563C7 File Offset: 0x000545C7
		public void AddSmokeParticle(LTSmokeParticle s)
		{
			this.mSmoke.Add(s);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000563D8 File Offset: 0x000545D8
		private void PlayBambooSound(float inVolume)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.volume = inVolume;
			GameApp.gApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_BAMBOO_CLOSE), soundAttribs);
		}

		// Token: 0x04001135 RID: 4405
		public const int BAMBOO_TRANSITION_FADE_TIME = 100;

		// Token: 0x04001136 RID: 4406
		public const int BAMBOO_TRANSITION_PAUSE_TIME = 100;

		// Token: 0x04001137 RID: 4407
		public const float BAMBOO_TRANSITION_FALL_TIME = 20f;

		// Token: 0x04001138 RID: 4408
		public const float BAMBOO_BOUNCE_GRAVITY = 0.1f;

		// Token: 0x04001139 RID: 4409
		public const int BAMBOO_CLOSE_UPDATE_WAIT_COUNT = 10;

		// Token: 0x0400113A RID: 4410
		public const float BAMBOO_V_DIV = 10f;

		// Token: 0x0400113B RID: 4411
		private BambooColumn.BambooEnd mTopEnd = default(BambooColumn.BambooEnd);

		// Token: 0x0400113C RID: 4412
		private BambooColumn.BambooEnd mBotEnd = default(BambooColumn.BambooEnd);

		// Token: 0x0400113D RID: 4413
		private BambooColumn.BambooState mState;

		// Token: 0x0400113E RID: 4414
		private float mX;

		// Token: 0x0400113F RID: 4415
		private float mGravity;

		// Token: 0x04001140 RID: 4416
		private List<LTSmokeParticle> mSmoke = new List<LTSmokeParticle>();

		// Token: 0x04001141 RID: 4417
		private Image IMAGE_BAMBOO_PIECE_A;

		// Token: 0x04001142 RID: 4418
		private Image IMAGE_BAMBOO_PIECE_B;

		// Token: 0x04001143 RID: 4419
		private Image IMAGE_BAMBOO_PIECE_C;

		// Token: 0x04001144 RID: 4420
		private Image IMAGE_BAMBOO_PIECE_D;

		// Token: 0x04001145 RID: 4421
		private bool mDrawed;

		// Token: 0x02000109 RID: 265
		private enum BambooState
		{
			// Token: 0x04001912 RID: 6418
			Init,
			// Token: 0x04001913 RID: 6419
			Falling,
			// Token: 0x04001914 RID: 6420
			Bouncing,
			// Token: 0x04001915 RID: 6421
			Closed,
			// Token: 0x04001916 RID: 6422
			Opening,
			// Token: 0x04001917 RID: 6423
			Open
		}

		// Token: 0x0200010A RID: 266
		private struct BambooEnd
		{
			// Token: 0x04001918 RID: 6424
			public float mY;

			// Token: 0x04001919 RID: 6425
			public float mFinalY;

			// Token: 0x0400191A RID: 6426
			public float mVelocityY;
		}
	}
}
