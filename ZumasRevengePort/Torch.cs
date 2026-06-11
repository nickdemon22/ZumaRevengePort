using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000091 RID: 145
	public class Torch : IDisposable
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x000892DE File Offset: 0x000874DE
		public Torch()
		{
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000892F0 File Offset: 0x000874F0
		public Torch(Torch rhs)
		{
			this.mOverlayAlpha = rhs.mOverlayAlpha;
			this.mWasHit = rhs.mWasHit;
			this.mDraw = rhs.mDraw;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mActive = rhs.mActive;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0008936A File Offset: 0x0008756A
		public virtual void Dispose()
		{
			GameApp.gApp.ReleaseTorchEffect(this.mFlame);
			GameApp.gApp.ReleaseTorchEffect(this.mFlameOut);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0008938C File Offset: 0x0008758C
		public void Update()
		{
			if (this.mFlame == null)
			{
				this.mFlame = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TORCHFLAME").Duplicate();
				this.mFlame.mEmitAfterTimeline = true;
				Common.SetFXNumScale(this.mFlame, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.5f));
			}
			if (this.mFlameOut == null)
			{
				this.mFlameOut = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TORCHFLAMEOUT").Duplicate();
				Common.SetFXNumScale(this.mFlameOut, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.5f));
			}
			if (this.mDraw)
			{
				if (this.mActive)
				{
					this.mFlame.mDrawTransform.LoadIdentity();
					float num = GameApp.DownScaleNum(1f);
					this.mFlame.mDrawTransform.Scale(num, num);
					if (this.mX > Common._DS(600))
					{
						this.mFlame.mDrawTransform.RotateDeg((float)Common._M(-75));
					}
					this.mFlame.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(50))), (float)(Common._S(this.mY) + Common._DS(Common._M1(130))));
					this.mFlame.Update();
					return;
				}
				if (this.mFlameOut.mFrameNum <= (float)this.mFlameOut.mLastFrameNum)
				{
					this.mFlameOut.mDrawTransform.LoadIdentity();
					float num2 = GameApp.DownScaleNum(1f);
					this.mFlameOut.mDrawTransform.Scale(num2, num2);
					this.mFlameOut.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(400))), (float)(Common._S(this.mY) + Common._DS(Common._M1(320))));
					this.mFlameOut.Update();
				}
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0008959D File Offset: 0x0008779D
		public void Draw(SexyGraphics g)
		{
			if (this.mDraw && this.mActive && this.mFlame != null)
			{
				g.PushState();
				this.mFlame.Draw(g);
				g.PopState();
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000895D0 File Offset: 0x000877D0
		public void DrawAbove(SexyGraphics g)
		{
			if (this.mDraw && this.mFlameOut != null && !this.mActive && this.mFlameOut.mFrameNum <= (float)this.mFlameOut.mLastFrameNum)
			{
				g.PushState();
				this.mFlameOut.Draw(g);
				g.PopState();
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00089628 File Offset: 0x00087828
		public bool CheckCollision(Rect r)
		{
			if (this.mActive && r.Intersects(new Rect(this.mX, this.mY, this.mWidth, this.mHeight)))
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_TORCH_EXTINGUISHED));
				this.mActive = false;
				this.mWasHit = true;
				this.mFlame.mEmitAfterTimeline = false;
				this.mFlameOut.ResetAnim();
				return true;
			}
			return false;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000896A0 File Offset: 0x000878A0
		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mActive);
			sync.SyncLong(ref this.mX);
			sync.SyncLong(ref this.mY);
			sync.SyncLong(ref this.mWidth);
			sync.SyncLong(ref this.mHeight);
			sync.SyncBoolean(ref this.mWasHit);
			sync.SyncBoolean(ref this.mDraw);
			sync.SyncLong(ref this.mOverlayAlpha);
			if (sync.isRead() && this.mWasHit)
			{
				this.mDraw = (this.mActive = false);
			}
		}

		// Token: 0x0400159A RID: 5530
		public PIEffect mFlame;

		// Token: 0x0400159B RID: 5531
		public PIEffect mFlameOut;

		// Token: 0x0400159C RID: 5532
		public int mX;

		// Token: 0x0400159D RID: 5533
		public int mY;

		// Token: 0x0400159E RID: 5534
		public int mWidth;

		// Token: 0x0400159F RID: 5535
		public int mHeight;

		// Token: 0x040015A0 RID: 5536
		public int mOverlayAlpha;

		// Token: 0x040015A1 RID: 5537
		public bool mActive;

		// Token: 0x040015A2 RID: 5538
		public bool mDraw = true;

		// Token: 0x040015A3 RID: 5539
		public bool mWasHit;
	}
}
