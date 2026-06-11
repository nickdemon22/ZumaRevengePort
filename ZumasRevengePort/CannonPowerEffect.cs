using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000090 RID: 144
	public class CannonPowerEffect : PowerEffect
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00088CF4 File Offset: 0x00086EF4
		public CannonPowerEffect(Ball b)
		{
			int radius = b.GetRadius();
			int num = (int)b.GetX() - radius;
			int num2 = (int)b.GetY() - radius;
			this.mRings[0].mX = (float)(num + 18);
			this.mRings[0].mY = (float)(num2 + 11);
			this.mRings[1].mX = (float)(num + 11);
			this.mRings[1].mY = (float)(num2 + 23);
			this.mRings[2].mX = (float)(num + 24);
			this.mRings[2].mY = (float)(num2 + 22);
			this.mBallRotation = b.GetRotation();
			for (int i = 0; i < 3; i++)
			{
				Common.RotatePoint(this.mBallRotation - 1.5707645f, ref this.mRings[i].mX, ref this.mRings[i].mY, (float)(num + radius), (float)(num2 + radius));
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00088E04 File Offset: 0x00087004
		public CannonPowerEffect()
		{
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00088E40 File Offset: 0x00087040
		public override void Update()
		{
			if (this.IsDone())
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				if (this.mRings[i].mSizePct < 1f)
				{
					this.mRings[i].mSizePct += 0.06666667f;
					if (this.mRings[i].mSizePct >= 1f)
					{
						this.mRings[i].mSizePct = 1f;
						float num2 = MathUtils.DegreesToRadians(this.mBallRotation + (float)(120 * i));
						this.mRings[i].mVX = (float)Math.Cos((double)num2) * 1.2f;
						this.mRings[i].mVY = -(float)Math.Sin((double)num2) * 1.2f;
						this.mRings[i].mTX = this.mRings[i].mX + this.mRings[i].mVX * 15f;
						this.mRings[i].mTY = this.mRings[i].mY + this.mRings[i].mVY * 15f;
					}
				}
				else if (this.mRings[i].mVX != 0f || this.mRings[i].mVY != 0f)
				{
					this.mRings[i].mX += this.mRings[i].mVX;
					this.mRings[i].mY += this.mRings[i].mVY;
					if (Common.DoneMoving(this.mRings[i].mX, this.mRings[i].mVX, this.mRings[i].mTX))
					{
						this.mRings[i].mX = this.mRings[i].mTX;
						this.mRings[i].mVX = 0f;
					}
					if (Common.DoneMoving(this.mRings[i].mY, this.mRings[i].mVY, this.mRings[i].mTY))
					{
						this.mRings[i].mY = this.mRings[i].mTY;
						this.mRings[i].mVY = 0f;
					}
				}
				else if (this.mRings[i].mAlpha != 0f)
				{
					this.mRings[i].mSizePct += 1f / (float)Common._M(25);
					this.mRings[i].mAlpha -= Common._M(6f);
					if (this.mRings[i].mAlpha < 0f)
					{
						num++;
						this.mRings[i].mAlpha = 0f;
					}
				}
				else
				{
					num++;
				}
			}
			if (num == 3)
			{
				this.mDone = true;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00089128 File Offset: 0x00087328
		public override void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_CANNON_RING_BLUE + this.mColorType);
			g.SetDrawMode(1);
			for (int i = 0; i < 3; i++)
			{
				int num = (int)(this.mRings[i].mSizePct * (float)imageByID.mWidth);
				int num2 = (int)(this.mRings[i].mSizePct * (float)imageByID.mHeight);
				if (this.mRings[i].mAlpha != 255f)
				{
					g.SetColor(255, 255, 255, (int)this.mRings[i].mAlpha);
					g.SetColorizeImages(true);
				}
				g.DrawImage(imageByID, (int)(Common._S(this.mRings[i].mX) - (float)(num / 2)), (int)(Common._S(this.mRings[i].mY) - (float)(num2 / 2)), num, num2);
				g.SetColorizeImages(false);
			}
			g.SetDrawMode(0);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00089214 File Offset: 0x00087414
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncFloat(ref this.mBallRotation);
			for (int i = 0; i < 3; i++)
			{
				sync.SyncFloat(ref this.mRings[i].mX);
				sync.SyncFloat(ref this.mRings[i].mY);
				sync.SyncFloat(ref this.mRings[i].mVX);
				sync.SyncFloat(ref this.mRings[i].mVY);
				sync.SyncFloat(ref this.mRings[i].mTX);
				sync.SyncFloat(ref this.mRings[i].mTY);
				sync.SyncFloat(ref this.mRings[i].mSizePct);
				sync.SyncFloat(ref this.mRings[i].mAlpha);
			}
		}

		// Token: 0x04001598 RID: 5528
		protected CannonPowerEffect.CannonRing[] mRings = new CannonPowerEffect.CannonRing[]
		{
			new CannonPowerEffect.CannonRing(),
			new CannonPowerEffect.CannonRing(),
			new CannonPowerEffect.CannonRing()
		};

		// Token: 0x04001599 RID: 5529
		protected float mBallRotation;

		// Token: 0x020000C3 RID: 195
		protected class CannonRing
		{
			// Token: 0x040016F1 RID: 5873
			public float mX;

			// Token: 0x040016F2 RID: 5874
			public float mY;

			// Token: 0x040016F3 RID: 5875
			public float mVX;

			// Token: 0x040016F4 RID: 5876
			public float mVY;

			// Token: 0x040016F5 RID: 5877
			public float mTX;

			// Token: 0x040016F6 RID: 5878
			public float mTY;

			// Token: 0x040016F7 RID: 5879
			public float mSizePct;

			// Token: 0x040016F8 RID: 5880
			public float mAlpha = 255f;
		}
	}
}
