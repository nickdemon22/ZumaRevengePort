using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000EF RID: 239
	public class EyeBullet
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x0009AA78 File Offset: 0x00098C78
		public bool Update(int x, int y, bool do_explosion)
		{
			if (this.mInitialAlpha < 255f)
			{
				this.mInitialAlpha += Common._M(15f);
			}
			this.mProjectile.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mProjectile.mDrawTransform.Scale(num, num);
			this.mProjectile.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
			this.mProjectile.mColor.mAlpha = (int)this.mInitialAlpha;
			this.mProjectile.Update();
			if ((this.mSparks.mFrameNum < (float)this.mSparks.mLastFrameNum || this.mSparks.mCurNumParticles > 0) && (this.mSparks.mFrameNum > 0f || this.mSparkFirstFrame))
			{
				this.mSparkFirstFrame = false;
				this.mSparks.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mSparks.mDrawTransform.Scale(num2, num2);
				this.mSparks.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
				this.mSparks.Update();
			}
			if (do_explosion)
			{
				this.mExplosion.mDrawTransform.LoadIdentity();
				float num3 = GameApp.DownScaleNum(1f);
				this.mExplosion.mDrawTransform.Scale(num3, num3);
				this.mExplosion.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
				this.mExplosion.Update();
				if (this.mExplosion.mFrameNum > (float)this.mExplosion.mLastFrameNum)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0009AC58 File Offset: 0x00098E58
		public void Draw(SexyGraphics g, int alpha)
		{
			g.PushState();
			this.mProjectile.mColor.mAlpha = alpha;
			this.mProjectile.Draw(g);
			g.PopState();
			if (this.mSparks.mCurNumParticles > 0)
			{
				g.PushState();
				this.mSparks.mColor.mAlpha = alpha;
				this.mSparks.Draw(g);
				g.PopState();
			}
			if (this.mExplosion.mFrameNum > 0f)
			{
				g.PushState();
				this.mExplosion.mColor.mAlpha = alpha;
				this.mExplosion.Draw(g);
				g.PopState();
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0009AD00 File Offset: 0x00098F00
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mXOff);
			sync.SyncLong(ref this.mYOff);
			sync.SyncFloat(ref this.mInitialAlpha);
			sync.SyncBoolean(ref this.mSparkFirstFrame);
			if (sync.isWrite())
			{
				Common.SerializePIEffect(this.mExplosion, sync);
				Common.SerializePIEffect(this.mProjectile, sync);
				Common.SerializePIEffect(this.mSparks, sync);
				return;
			}
			this.mExplosion = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJEXPLOSION").Duplicate();
			this.mProjectile = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJ").Duplicate();
			this.mSparks = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJSPARKS").Duplicate();
			Common.DeserializePIEffect(this.mExplosion, sync);
			Common.DeserializePIEffect(this.mProjectile, sync);
			Common.DeserializePIEffect(this.mSparks, sync);
		}

		// Token: 0x0400183C RID: 6204
		public PIEffect mProjectile;

		// Token: 0x0400183D RID: 6205
		public PIEffect mSparks;

		// Token: 0x0400183E RID: 6206
		public PIEffect mExplosion;

		// Token: 0x0400183F RID: 6207
		public bool mSparkFirstFrame;

		// Token: 0x04001840 RID: 6208
		public float mInitialAlpha;

		// Token: 0x04001841 RID: 6209
		public int mXOff;

		// Token: 0x04001842 RID: 6210
		public int mYOff;
	}
}
