using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000126 RID: 294
	public class OrbParticle
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x000A1B55 File Offset: 0x0009FD55
		public OrbParticle()
		{
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000A1B68 File Offset: 0x0009FD68
		public OrbParticle(float angle, float radius, float alpha_fade, float size_fade)
		{
			this.mAngle = angle;
			this.mAlphaFade = alpha_fade;
			this.mSizeFade = size_fade;
			this.mAlpha = 255f;
			this.mRadius = radius;
			this.mSize = 1f;
			this.mRotation = 0f;
			this.mRed = 255f;
			this.mGreen = 255f;
			float num = 255f / this.mAlphaFade;
			this.mRedFade = 255f / num;
			this.mGreenFade = Common._M(54f) / num;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000A1C08 File Offset: 0x0009FE08
		public void Update()
		{
			this.mAlpha -= this.mAlphaFade;
			this.mSize -= this.mSizeFade;
			this.mRed -= this.mRedFade;
			this.mGreen -= this.mGreenFade;
			if (this.mRed < 0f)
			{
				this.mRed = 0f;
			}
			if (this.mGreen < 0f)
			{
				this.mGreen = 0f;
			}
			if (this.mAlpha < 0f)
			{
				this.mAlpha = 0f;
			}
			if (this.mSize < 0f)
			{
				this.mSize = 0f;
			}
			this.mRotation += Common._M(0.1f);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000A1CD8 File Offset: 0x0009FED8
		public void Draw(SexyGraphics g, float x, float y)
		{
			g.SetColorizeImages(true);
			g.SetColor((int)this.mRed, (int)this.mGreen, 255, (int)this.mAlpha);
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.RotateRad(this.mRotation);
			this.mGlobalTranform.Scale(this.mSize, this.mSize);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_PART_FAT);
			g.DrawImageTransform(imageByID, this.mGlobalTranform, x + this.mRadius * (float)Math.Cos((double)this.mAngle), y - this.mRadius * (float)Math.Sin((double)this.mAngle));
			g.SetColorizeImages(false);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000A1D8A File Offset: 0x0009FF8A
		public bool IsDone()
		{
			return this.mAlpha <= 0f;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000A1D9C File Offset: 0x0009FF9C
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlphaFade);
			sync.SyncFloat(ref this.mSizeFade);
			sync.SyncFloat(ref this.mRedFade);
			sync.SyncFloat(ref this.mGreenFade);
			sync.SyncFloat(ref this.mRed);
			sync.SyncFloat(ref this.mGreen);
		}

		// Token: 0x040019CE RID: 6606
		protected float mAlpha;

		// Token: 0x040019CF RID: 6607
		protected float mAngle;

		// Token: 0x040019D0 RID: 6608
		protected float mRadius;

		// Token: 0x040019D1 RID: 6609
		protected float mRotation;

		// Token: 0x040019D2 RID: 6610
		protected float mSize;

		// Token: 0x040019D3 RID: 6611
		protected float mAlphaFade;

		// Token: 0x040019D4 RID: 6612
		protected float mSizeFade;

		// Token: 0x040019D5 RID: 6613
		protected float mRedFade;

		// Token: 0x040019D6 RID: 6614
		protected float mGreenFade;

		// Token: 0x040019D7 RID: 6615
		protected float mRed;

		// Token: 0x040019D8 RID: 6616
		protected float mGreen;

		// Token: 0x040019D9 RID: 6617
		protected Transform mGlobalTranform = new Transform();
	}
}
