using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200009F RID: 159
	public class OrbPowerRing : IDisposable
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0008B921 File Offset: 0x00089B21
		public OrbPowerRing()
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0008B934 File Offset: 0x00089B34
		public OrbPowerRing(float angle, float max_radius, float alpha_fade, float size_fade, float angle_inc)
		{
			this.mAlphaFade = alpha_fade;
			this.mSizeFade = size_fade;
			this.mAngle = angle;
			this.mRadius = 0f;
			this.mMaxRadius = max_radius;
			this.mExpanding = true;
			this.mUpdateCount = 0;
			this.mDone = false;
			this.mAngleInc = angle_inc;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0008B998 File Offset: 0x00089B98
		public virtual void Dispose()
		{
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				this.mParticles[i] = null;
			}
			this.mParticles.Clear();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0008B9D4 File Offset: 0x00089BD4
		public void Update()
		{
			if (this.mDone)
			{
				return;
			}
			this.mAngle += this.mAngleInc;
			this.mUpdateCount++;
			if (this.mUpdateCount > Common._M(50))
			{
				this.mExpanding = false;
			}
			if (this.mExpanding && this.mRadius < this.mMaxRadius)
			{
				this.mRadius += this.mMaxRadius / Common._M(30f);
			}
			else if (!this.mExpanding && this.mRadius > 0f)
			{
				this.mRadius -= this.mMaxRadius / Common._M(15f);
				if (this.mRadius < 0f)
				{
					this.mRadius = 0f;
				}
			}
			if ((this.mExpanding || this.mRadius > 0f) && this.mUpdateCount % Common._M(1) == 0)
			{
				this.mParticles.Add(new OrbParticle(this.mAngle, this.mRadius, this.mAlphaFade, this.mSizeFade));
			}
			bool flag = true;
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				OrbParticle orbParticle = this.mParticles[i];
				orbParticle.Update();
				if (!orbParticle.IsDone())
				{
					flag = false;
				}
				else
				{
					this.mParticles.RemoveAt(i);
					i--;
				}
			}
			if (!this.mExpanding && flag)
			{
				this.mDone = true;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0008BB4C File Offset: 0x00089D4C
		public void Draw(SexyGraphics g, float x, float y)
		{
			if (this.mDone)
			{
				return;
			}
			for (int i = 0; i < this.mParticles.Count; i++)
			{
				this.mParticles[i].Draw(g, x, y);
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0008BB8C File Offset: 0x00089D8C
		public bool IsDone()
		{
			return this.mDone;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0008BB94 File Offset: 0x00089D94
		public bool IsExpanding()
		{
			return this.mExpanding;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0008BB9C File Offset: 0x00089D9C
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncFloat(ref this.mMaxRadius);
			sync.SyncFloat(ref this.mAlphaFade);
			sync.SyncFloat(ref this.mSizeFade);
			sync.SyncFloat(ref this.mAngleInc);
			sync.SyncBoolean(ref this.mExpanding);
			sync.SyncBoolean(ref this.mDone);
			sync.SyncLong(ref this.mUpdateCount);
			this.SyncListOrbParticles(sync, true);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0008BC20 File Offset: 0x00089E20
		private void SyncListOrbParticles(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mParticles.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					OrbParticle orbParticle = new OrbParticle();
					orbParticle.SyncState(sync);
					this.mParticles.Add(orbParticle);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mParticles.Count);
			foreach (OrbParticle orbParticle2 in this.mParticles)
			{
				orbParticle2.SyncState(sync);
			}
		}

		// Token: 0x040015F8 RID: 5624
		protected List<OrbParticle> mParticles = new List<OrbParticle>();

		// Token: 0x040015F9 RID: 5625
		protected float mAngle;

		// Token: 0x040015FA RID: 5626
		protected float mRadius;

		// Token: 0x040015FB RID: 5627
		protected float mMaxRadius;

		// Token: 0x040015FC RID: 5628
		protected float mAlphaFade;

		// Token: 0x040015FD RID: 5629
		protected float mSizeFade;

		// Token: 0x040015FE RID: 5630
		protected float mAngleInc;

		// Token: 0x040015FF RID: 5631
		protected bool mExpanding;

		// Token: 0x04001600 RID: 5632
		protected bool mDone;

		// Token: 0x04001601 RID: 5633
		protected int mUpdateCount;
	}
}
