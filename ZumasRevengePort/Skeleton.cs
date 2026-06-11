using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000124 RID: 292
	public class Skeleton : IDisposable
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x000A10A0 File Offset: 0x0009F2A0
		public Skeleton()
		{
			this.mAlpha = 0f;
			this.mIncAlpha = true;
			this.mActivated = false;
			this.mEffectDone = false;
			this.mFadeOut = false;
			this.mFadeAlpha = 255f;
			this.mOrbSize = 1f;
			this.mOrbSizeDec = 0f;
			this.mRibCel = 0;
			this.mHeadYOff = 0f;
			this.mHeadVY = 0f;
			this.mHeadBounceCount = 0;
			this.mUpdateCount = 0;
			this.mExplosionCel = 0;
			this.mRings[0] = (this.mRings[1] = null);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000A114D File Offset: 0x0009F34D
		public virtual void Dispose()
		{
			if (this.mRings[0] != null)
			{
				this.mRings[0].Dispose();
			}
			if (this.mRings[1] != null)
			{
				this.mRings[1].Dispose();
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000A1180 File Offset: 0x0009F380
		public void Update()
		{
			if (this.mDelay > 0)
			{
				this.mDelay--;
				return;
			}
			this.mUpdateCount++;
			this.mX += this.mVX;
			this.mY += this.mVY;
			if (this.mHasPowerup)
			{
				int num = (int)Common._M(14f);
				if (this.mIncAlpha)
				{
					this.mAlpha += (float)num;
					if (this.mAlpha >= 255f)
					{
						this.mAlpha = 255f;
						this.mIncAlpha = false;
					}
				}
				else
				{
					this.mAlpha -= (float)num;
					if (this.mAlpha <= 0f)
					{
						this.mAlpha = 0f;
						this.mIncAlpha = true;
					}
				}
				if (this.mActivated)
				{
					if (this.mUpdateCount % Common._M(4) == 0)
					{
						this.mRibCel++;
					}
					if (this.mHeadBounceCount < Common._M(5))
					{
						float num2 = (float)Common._M(25);
						float num3 = num2 - (float)Common._M(10);
						if (this.mHeadBounceCount % 2 == 0)
						{
							this.mHeadYOff += this.mHeadVY;
							if (this.mHeadYOff >= num2)
							{
								this.mHeadYOff = num2;
								this.mHeadBounceCount++;
							}
						}
						else
						{
							this.mHeadYOff -= this.mHeadVY;
							if (this.mHeadYOff <= num3)
							{
								this.mHeadYOff = num3;
								this.mHeadBounceCount++;
							}
						}
					}
				}
			}
			else if (this.mActivated)
			{
				if (this.mUpdateCount % Common._M(2) == 0)
				{
					this.mExplosionCel++;
				}
				if (this.mExplosionCel == Common._M(3))
				{
					this.mFadeOut = true;
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_EXPLODE);
				if (this.mExplosionCel >= imageByID.mNumCols * imageByID.mNumRows)
				{
					this.mEffectDone = true;
				}
			}
			if (this.mRings[0] != null)
			{
				if (this.mOrbSize > 0f)
				{
					this.mOrbSize -= this.mOrbSizeDec;
					if (this.mOrbSize < 0f)
					{
						this.mOrbSize = 0f;
					}
				}
				this.mRings[0].Update();
				this.mRings[1].Update();
				if (this.mRings[0].IsDone() && this.mRings[1].IsDone())
				{
					this.mEffectDone = true;
				}
				else if (!this.mRings[0].IsExpanding())
				{
					this.mFadeOut = true;
				}
			}
			if (this.mFadeOut)
			{
				this.mFadeAlpha -= 255f / Common._M(15f);
				if (this.mFadeAlpha < 0f)
				{
					this.mFadeAlpha = 0f;
				}
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000A1454 File Offset: 0x0009F654
		public void DoHit()
		{
			float max_radius = Common._M(30f);
			float alpha_fade = 255f / Common._M(20f);
			float size_fade = 1f / Common._M(50f);
			float angle_inc = Common._M(0.2f);
			this.mRings[0] = new OrbPowerRing(0f, max_radius, alpha_fade, size_fade, angle_inc);
			this.mRings[1] = new OrbPowerRing(3.14159f, max_radius, alpha_fade, size_fade, angle_inc);
			this.mOrbSizeDec = 1f / Common._M(50f);
			this.mHeadVY = Common._M(2f);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000A14EB File Offset: 0x0009F6EB
		public void SetupFade(SexyGraphics g)
		{
			if (this.mFadeOut)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mFadeAlpha);
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000A1518 File Offset: 0x0009F718
		public void Draw(SexyGraphics g)
		{
			if (this.mDelay > 0 || (this.mFadeOut && this.mFadeAlpha <= 0f))
			{
				return;
			}
			this.SetupFade(g);
			if (!this.mHasPowerup)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON), (int)Common._S(this.mX), (int)Common._S(this.mY));
			}
			g.SetColorizeImages(false);
			if (this.mHasPowerup)
			{
				this.SetupFade(g);
				if (this.mRibCel < Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS).mNumCols)
				{
					g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS), (int)Common._S(this.mX + (float)Common._M(1)), (int)Common._S(this.mY + (float)Common._M1(34)), this.mRibCel);
				}
				Image image = this.mActivated ? Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_CLOSED) : Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD);
				g.DrawImage(image, (int)Common._S(this.mX + (float)Common._M(0)), (int)Common._S(this.mY + (float)Common._M1(0) + this.mHeadYOff));
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_JAW), (int)Common._S(this.mX + (float)Common._M(34)), (int)Common._S(this.mY + (float)Common._M1(82)));
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_GLOWBALL);
				float num = (float)imageByID.GetCelWidth() * this.mOrbSize;
				float num2 = (float)imageByID.GetCelHeight() * this.mOrbSize;
				g.DrawImage(imageByID, (int)(Common._S(this.mX + (float)Common._M(28)) + (float)(imageByID.GetCelWidth() / 2) - num / 2f), (int)(Common._S(this.mY + (float)Common._M1(40)) + (float)(imageByID.GetCelHeight() / 2) - num2 / 2f), (int)num, (int)num2);
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				g.SetColor(255, 255, 255, (int)((this.mFadeAlpha < this.mAlpha) ? this.mFadeAlpha : this.mAlpha));
				g.DrawImage(imageByID, (int)(Common._S(this.mX + (float)Common._M(28)) + (float)(imageByID.GetCelWidth() / 2) - num / 2f), (int)(Common._S(this.mY + (float)Common._M1(40)) + (float)(imageByID.GetCelHeight() / 2) - num2 / 2f), (int)num, (int)num2);
				g.SetColorizeImages(false);
				g.SetDrawMode(0);
				this.SetupFade(g);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_RIBS_SHADOW);
				if (this.mRibCel < imageByID2.mNumCols)
				{
					g.DrawImageCel(imageByID2, (int)Common._S(this.mX + (float)Common._M(1)), (int)Common._S(this.mY + (float)Common._M1(34)), this.mRibCel);
				}
				image = (this.mActivated ? Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_CLOSED) : Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_HEAD_SHADOW));
				g.DrawImage(image, (int)Common._S(this.mX + (float)Common._M(0)), (int)Common._S(this.mY + (float)Common._M1(0) + this.mHeadYOff));
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_JAW_SHADOW), (int)Common._S(this.mX + (float)Common._M(34)), (int)Common._S(this.mY + (float)Common._M1(82)));
				g.SetColorizeImages(false);
			}
			this.SetupFade(g);
			if (!this.mHasPowerup)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON_NOSHADOW), (int)Common._S(this.mX), (int)Common._S(this.mY));
			}
			g.SetColorizeImages(false);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELE_EXPLODE);
			if (this.mActivated && !this.mHasPowerup && this.mExplosionCel < imageByID3.mNumCols * imageByID3.mNumRows)
			{
				Rect celRect = imageByID3.GetCelRect(this.mExplosionCel);
				int num3 = celRect.mWidth * 4;
				int num4 = celRect.mHeight * 4;
				g.DrawImage(imageByID3, new Rect((int)Common._S(this.mX + (float)Common._M(-50)), (int)Common._S(this.mY + (float)Common._M1(-50)), num3, num4), celRect);
			}
			if (this.mRings[0] != null)
			{
				Image imageByID4 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON);
				for (int i = 0; i < 2; i++)
				{
					this.mRings[i].Draw(g, Common._S(this.mX) + (float)(imageByID4.GetCelWidth() / 2), Common._S(this.mY) + (float)(imageByID4.GetCelHeight() / 2));
				}
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000A19C8 File Offset: 0x0009FBC8
		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mHasPowerup);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncLong(ref this.mDelay);
			sync.SyncFloat(ref this.mOrbSize);
			sync.SyncFloat(ref this.mOrbSizeDec);
			sync.SyncFloat(ref this.mFadeAlpha);
			sync.SyncBoolean(ref this.mFadeOut);
			sync.SyncLong(ref this.mRibCel);
			sync.SyncFloat(ref this.mHeadYOff);
			sync.SyncFloat(ref this.mHeadVY);
			sync.SyncLong(ref this.mHeadBounceCount);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mExplosionCel);
			sync.SyncBoolean(ref this.mActivated);
			Buffer buffer = sync.GetBuffer();
			if (sync.isRead())
			{
				if (buffer.ReadBoolean())
				{
					this.mRings[0] = new OrbPowerRing(0f, 0f, 0f, 0f, 0f);
					this.mRings[1] = new OrbPowerRing(0f, 0f, 0f, 0f, 0f);
					for (int i = 0; i < 2; i++)
					{
						this.mRings[i].SyncState(sync);
					}
					return;
				}
			}
			else
			{
				if (this.mRings[0] == null)
				{
					buffer.WriteBoolean(false);
					return;
				}
				buffer.WriteBoolean(true);
				for (int j = 0; j < 2; j++)
				{
					this.mRings[j].SyncState(sync);
				}
			}
		}

		// Token: 0x040019AF RID: 6575
		public bool mHasPowerup;

		// Token: 0x040019B0 RID: 6576
		public float mVX;

		// Token: 0x040019B1 RID: 6577
		public float mVY;

		// Token: 0x040019B2 RID: 6578
		public float mX;

		// Token: 0x040019B3 RID: 6579
		public float mY;

		// Token: 0x040019B4 RID: 6580
		public int mDelay;

		// Token: 0x040019B5 RID: 6581
		public float mOrbSize;

		// Token: 0x040019B6 RID: 6582
		public float mOrbSizeDec;

		// Token: 0x040019B7 RID: 6583
		public float mAlpha;

		// Token: 0x040019B8 RID: 6584
		public float mFadeAlpha;

		// Token: 0x040019B9 RID: 6585
		public bool mIncAlpha;

		// Token: 0x040019BA RID: 6586
		public bool mActivated;

		// Token: 0x040019BB RID: 6587
		public bool mEffectDone;

		// Token: 0x040019BC RID: 6588
		public bool mFadeOut;

		// Token: 0x040019BD RID: 6589
		public int mRibCel;

		// Token: 0x040019BE RID: 6590
		public float mHeadYOff;

		// Token: 0x040019BF RID: 6591
		public float mHeadVY;

		// Token: 0x040019C0 RID: 6592
		public int mHeadBounceCount;

		// Token: 0x040019C1 RID: 6593
		public int mUpdateCount;

		// Token: 0x040019C2 RID: 6594
		public int mExplosionCel;

		// Token: 0x040019C3 RID: 6595
		public OrbPowerRing[] mRings = new OrbPowerRing[2];
	}
}
