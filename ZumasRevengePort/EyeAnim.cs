using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000EE RID: 238
	public class EyeAnim
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0009A93B File Offset: 0x00098B3B
		public EyeAnim()
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0009A943 File Offset: 0x00098B43
		public EyeAnim(EyeAnim rhs)
		{
			if (rhs == null || rhs == this)
			{
				return;
			}
			this.mEyeFlame = rhs.mEyeFlame;
			this.mFiring = rhs.mFiring;
			this.mUpdateCount = rhs.mUpdateCount;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0009A978 File Offset: 0x00098B78
		public void Update(int x, int y, int alpha)
		{
			if (!this.mFiring && this.mEyeFlame.mCurNumParticles == 0)
			{
				return;
			}
			this.mUpdateCount++;
			this.mEyeFlame.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mEyeFlame.mDrawTransform.Scale(num, num);
			this.mEyeFlame.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
			this.mEyeFlame.mColor.mAlpha = alpha;
			this.mEyeFlame.Update();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0009AA10 File Offset: 0x00098C10
		public void Draw(SexyGraphics g)
		{
			if (!this.mFiring && this.mEyeFlame.mCurNumParticles == 0)
			{
				return;
			}
			this.mEyeFlame.Draw(g);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0009AA34 File Offset: 0x00098C34
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncBoolean(ref this.mFiring);
			if (sync.isWrite())
			{
				Common.SerializePIEffect(this.mEyeFlame, sync);
				return;
			}
			Common.DeserializePIEffect(this.mEyeFlame, sync);
		}

		// Token: 0x04001839 RID: 6201
		public PIEffect mEyeFlame;

		// Token: 0x0400183A RID: 6202
		public bool mFiring;

		// Token: 0x0400183B RID: 6203
		public int mUpdateCount;
	}
}
