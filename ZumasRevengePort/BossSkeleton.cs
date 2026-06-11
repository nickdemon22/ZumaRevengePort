using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	// Token: 0x02000092 RID: 146
	public class BossSkeleton : BossShoot
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x00089730 File Offset: 0x00087930
		protected void DrawHit(SexyGraphics g, Image img, int x, int y, int cel)
		{
			int num = (int)Math.Min(this.mHitAlpha, this.mAlphaOverride);
			if (num > 0 && this.mHP > 0f && g.Is3D())
			{
				g.PushState();
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				g.SetColor(255, 255, 255, num);
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_THROW);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_HIT);
				if (cel == -1)
				{
					g.DrawImage(img, x, y);
				}
				else if (img == imageByID2)
				{
					g.DrawImageCel(img, new Rect(x, y, imageByID.GetCelWidth(), imageByID.GetCelHeight()), cel);
				}
				else
				{
					g.DrawImageCel(img, new Rect(x, y, img.GetCelWidth(), img.GetCelHeight()), cel);
				}
				g.PopState();
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00089809 File Offset: 0x00087A09
		protected void DrawHit(SexyGraphics g, Image img, int x, int y)
		{
			this.DrawHit(g, img, x, y, -1);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00089818 File Offset: 0x00087A18
		protected override void DrawBossSpecificArt(SexyGraphics g)
		{
			int num = (int)(this.mX - (float)(this.mWidth / 2) + (float)this.mShakeXOff);
			int num2 = (int)(this.mY - (float)(this.mWidth / 2) + (float)this.mShakeYOff);
			if (this.mHP > 0f && !this.mDoDeathExplosions && !this.mLevel.mBoard.IsPaused())
			{
				for (int i = 0; i < Common.size<Skeleton>(this.mSkeletons); i++)
				{
					Skeleton skeleton = this.mSkeletons[i];
					skeleton.Draw(g);
				}
			}
			if (this.mAlphaOverride < 255f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
			}
			if (this.mState == 0 || this.mState == 1)
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_BODY);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_JAW);
				Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_THROW);
				g.DrawImage(imageByID, Common._S(num + Common._M(38)), Common._S(num2 + Common._M1(78)));
				this.DrawHit(g, imageByID, Common._S(num + Common._M(38)), Common._S(num2 + Common._M1(78)));
				g.DrawImageCel(imageByID2, Common._S(num + Common._M(73)), Common._S(num2 + Common._M1(96)), this.mJawCel);
				this.DrawHit(g, imageByID2, Common._S(num + Common._M(73)), Common._S(num2 + Common._M1(96)), this.mJawCel);
				this.DrawBullets(g);
				if (this.mState == 1 && this.mCel < 7)
				{
					g.PushState();
					g.ClipRect(Common._S(num), Common._S(num2 + Common._M(38)), Common._S(200), Common._S(200));
					Image imageByID4 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_SHRUNK_HEAD1);
					g.DrawImage(imageByID4, Common._S(num + Common._M(70)), (int)((float)(Common._S(num2 + Common._M1(94)) - imageByID4.GetHeight()) + this.mShrunkHeadYOff));
					g.PopState();
				}
				g.DrawImageCel(imageByID3, Common._S(num), Common._S(num2), this.mCel);
				this.DrawHit(g, imageByID3, Common._S(num), Common._S(num2), this.mCel);
			}
			else
			{
				Image imageByID5 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_HIT);
				Image imageByID6 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_THROW);
				g.DrawImageCel(imageByID5, new Rect(Common._S(num), Common._S(num2), imageByID6.GetCelWidth(), imageByID6.GetCelHeight()), this.mCel);
				this.DrawHit(g, imageByID5, Common._S(num + Common._M(0)), Common._S(num2 + Common._M1(0)), this.mCel);
				this.DrawBullets(g);
			}
			g.SetColorizeImages(false);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00089AFC File Offset: 0x00087CFC
		protected override bool PreBulletUpdate(BossBullet b, int index)
		{
			if (b.mDelay > 0)
			{
				b.mDelay--;
			}
			else if (b.mState == 0 && this.mState == 0 && this.mJawCel == 0)
			{
				this.mState = 1;
				this.mTimer = 0;
				b.mState = 1;
				base.PlaySound(2);
			}
			return b.mState <= 1;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00089B64 File Offset: 0x00087D64
		protected virtual void DrawBullets(SexyGraphics g)
		{
			g.PushState();
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				g.ClearClipRect();
			}
			if (this.mHP <= 0f || this.mDoDeathExplosions || this.mLevel.mBoard.IsPaused())
			{
				return;
			}
			for (int i = 0; i < Common.size<PSystemIntPair>(this.mParticles); i++)
			{
				this.mParticles[i].first.Draw(g);
			}
			for (int j = 0; j < Common.size<BossBullet>(this.mBullets); j++)
			{
				BossBullet bossBullet = this.mBullets[j];
				if (bossBullet.mDelay <= 0 && bossBullet.mState >= 2)
				{
					g.DrawImage(BossSkeleton.gHeadImages[bossBullet.mImageNum], (int)(Common._S(bossBullet.mX) - (float)(BossSkeleton.gHeadImages[bossBullet.mImageNum].mWidth / 2)), (int)(Common._S(bossBullet.mY) - (float)(BossSkeleton.gHeadImages[bossBullet.mImageNum].mHeight / 2)));
				}
			}
			if (Common._geq(this.mAlphaOverride, 255f))
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_HEAD_BURST);
				for (int k = 0; k < Common.size<int>(this.mHeadExplosions); k++)
				{
					Rect celRect = imageByID.GetCelRect(this.mHeadExplosions[k]);
					int num = celRect.mWidth * 10;
					int num2 = celRect.mHeight * 10;
					g.DrawImage(imageByID, new Rect(Common._S(this.mLevel.mFrog.GetCenterX()) - imageByID.GetCelWidth() / 2, Common._S(this.mLevel.mFrog.GetCenterY()) - imageByID.GetCelHeight() / 2, num, num2), celRect);
				}
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			g.PopState();
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00089D3C File Offset: 0x00087F3C
		protected override Rect GetBulletRect(BossBullet b)
		{
			int num = (int)((float)BossSkeleton.gHeadImages[b.mImageNum].mWidth * 0.75f);
			int num2 = (int)((float)BossSkeleton.gHeadImages[b.mImageNum].mHeight * 0.75f);
			return new Rect((int)b.mX - num / 2, (int)b.mY - num2 / 2, num, num2);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00089D9C File Offset: 0x00087F9C
		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			if (this.mState != 2)
			{
				this.mState = 2;
				this.mCel = 0;
				this.mJawCel = 0;
				this.mShrunkHeadYOff = 0f;
				for (int i = 0; i < Common.size<BossBullet>(this.mBullets); i++)
				{
					if (this.mBullets[i].mState == 1)
					{
						this.mBullets[i].mState = 0;
					}
				}
			}
			int num = (int)base.mHPDecPerHit;
			int mHeartPieceDecAmt = this.mHeartPieceDecAmt;
			if (b != null && b.GetIsCannon())
			{
				this.mHitAlpha = 255f;
				base.mHPDecPerHit = (float)this.mSpecialWpnHPDec;
				this.mHeartPieceDecAmt = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / base.mHPDecPerHit));
			}
			bool result = base.DoHit(b, from_prox_bomb);
			base.mHPDecPerHit = (float)num;
			this.mHeartPieceDecAmt = mHeartPieceDecAmt;
			return result;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00089E78 File Offset: 0x00088078
		protected override void BulletHitPlayer(BossBullet b)
		{
			base.BulletHitPlayer(b);
			this.mHeadExplosions.Add(0);
			for (int i = 0; i < Common.size<PSystemIntPair>(this.mParticles); i++)
			{
				if (this.mParticles[i].second == b.mId)
				{
					if (this.mParticles[i].first != null)
					{
						this.mParticles[i].first.Dispose();
						this.mParticles[i].first = null;
					}
					this.mParticles.RemoveAt(i);
					break;
				}
			}
			if (!this.mApp.GetLevelMgr().mBossesCanAttackFuckedFrog)
			{
				for (int j = 0; j < Common.size<BossBullet>(this.mBullets); j++)
				{
					BossBullet bossBullet = this.mBullets[j];
					if (bossBullet.mDelay > 0)
					{
						bossBullet.mDeleteInstantly = true;
					}
				}
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00089F55 File Offset: 0x00088155
		protected override bool CanFire()
		{
			return this.mState != 2;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00089F64 File Offset: 0x00088164
		protected virtual void AddParticleSystem(BossBullet b)
		{
			PILSystem system = new PILSystem(75, 50);
			system.mScale = Common._S(1f);
			system.WaitForEmitters(true);
			this.mParticles.Add(new PSystemIntPair(system, b.mId));
			Emitter emitter = new Emitter();
			emitter.mCullingRect = new Rect(0, 0, Common._SS(this.mApp.mWidth), Common._SS(this.mApp.mHeight));
			emitter.mEmissionCoordsAreOffsets = true;
			emitter.AddScaleKeyFrame(0, new EmitterScale
			{
				mLifeScale = Common._M(1f),
				mNumberScale = Common._M(1f),
				mSizeXScale = Common._M(0.91f),
				mVelocityScale = Common._M(1.5f),
				mWeightScale = Common._M(3f),
				mZoom = Common._M(3.63f)
			});
			EmitterSettings emitterSettings = new EmitterSettings();
			emitterSettings.mAngle = Common.DegreesToRadians((float)Common._M(-73));
			emitter.SetEmitterType(1);
			emitter.AddLineEmitterKeyFrame(0, 0, new Point(0, 0));
			emitter.AddLineEmitterKeyFrame(1, 0, new Point(Common._M(32), 0));
			ParticleType particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_FUZZY_CIRCLE);
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(5),
				mNumber = Common._M(100),
				mXSize = Common._M(8),
				mWeight = (float)Common._M(0)
			});
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mNumberVar = Common._M(0),
				mSizeXVar = Common._M(7),
				mMotionRandVar = (float)Common._M(44)
			});
			LifetimeSettings lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = 1.6f;
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = 1.4f;
			particleType.AddSettingAtLifePct(0.42f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = 1.2f;
			particleType.AddSettingAtLifePct(0.62f, lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mSizeXMult = 0f
			});
			particleType.mAdditive = true;
			particleType.mColorKeyManager.AddColorKey(0f, new Color(128, 124, 60));
			particleType.mColorKeyManager.AddColorKey(0.5f, new Color(57, 158, 44));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(253, 253, 0));
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 56);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.9f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0008A278 File Offset: 0x00088478
		protected override void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
			for (int i = 0; i < Common.size<PSystemIntPair>(this.mParticles); i++)
			{
				if (this.mParticles[i].second == b.mId)
				{
					this.mParticles[i].first.ForceStopEmitting(true);
					this.mParticles[i].second = -1;
					return;
				}
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0008A2E0 File Offset: 0x000884E0
		public BossSkeleton(Level l) : base(l)
		{
			this.mSkeletonEmitX = (float)Common._S(Common._M(-200));
			this.mWidth = 196;
			this.mHeight = 194;
			this.mBossRadius = Common._M(58);
			this.mBulletRadius = Common._M(25);
			this.mResGroup = "Boss3";
			this.mResPrefix = "IMAGE_BOSS_SKELETON_";
			this.mExplosionRate = Common._M(4);
			this.mHitEffectYOff = Common._M(10);
			this.mDrawHeartsBelowBoss = true;
			this.mBandagedXOff = Common._M(-8);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0008A3AC File Offset: 0x000885AC
		public BossSkeleton() : this(null)
		{
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0008A3B8 File Offset: 0x000885B8
		public override void Dispose()
		{
			base.Dispose();
			for (int i = 0; i < this.mSkeletons.Count; i++)
			{
				if (this.mSkeletons[i] != null)
				{
					this.mSkeletons[i].Dispose();
					this.mSkeletons[i] = null;
				}
			}
			for (int j = 0; j < this.mParticles.Count; j++)
			{
				if (this.mParticles[j].first != null)
				{
					this.mParticles[j].first.Dispose();
					this.mParticles[j].first = null;
				}
			}
			this.mSkeletons.Clear();
			this.mParticles.Clear();
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0008A474 File Offset: 0x00088674
		public void CopyFrom(BossSkeleton rhs)
		{
			base.CopyFrom(rhs);
			this.mHeadExplosions.AddRange(rhs.mHeadExplosions.ToArray());
			this.mState = rhs.mState;
			this.mCel = rhs.mCel;
			this.mJawCel = rhs.mJawCel;
			this.mTimer = rhs.mTimer;
			this.mShrunkHeadYOff = rhs.mShrunkHeadYOff;
			this.mHitAlpha = rhs.mHitAlpha;
			this.mSpawnPowerupWhilePoweredUp = rhs.mSpawnPowerupWhilePoweredUp;
			this.mSpecialWpnHPDec = rhs.mSpecialWpnHPDec;
			this.mNumSkeleToEmit = rhs.mNumSkeleToEmit;
			this.mSkeleDelay = rhs.mSkeleDelay;
			this.mDelayAfterSkeleEmit = rhs.mDelayAfterSkeleEmit;
			this.mCurSkeleDelay = rhs.mCurSkeleDelay;
			this.mChanceToSpawnPowerup = rhs.mChanceToSpawnPowerup;
			this.mSkeletonVX = rhs.mSkeletonVX;
			this.mSkeletonVY = rhs.mSkeletonVY;
			this.mSkeletonEmitX = rhs.mSkeletonEmitX;
			this.mSkeletonEmitY = rhs.mSkeletonEmitY;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0008A56A File Offset: 0x0008876A
		public override void MouseDownDuringNoFire(int x, int y)
		{
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0008A56C File Offset: 0x0008876C
		public override bool AllowFrogToFire()
		{
			return base.AllowFrogToFire();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0008A574 File Offset: 0x00088774
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mCurSkeleDelay);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mTimer);
			sync.SyncLong(ref this.mJawCel);
			sync.SyncFloat(ref this.mShrunkHeadYOff);
			if (sync.isRead())
			{
				this.mSkeletons.Clear();
			}
			this.SyncListSkeletons(sync, this.mSkeletons, true);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteLong((long)this.mParticles.Count);
				for (int i = 0; i < this.mParticles.Count; i++)
				{
					buffer.WriteLong((long)this.mParticles[i].second);
					Common.SerializeParticleSystem(this.mParticles[i].first, sync);
				}
				return;
			}
			this.mParticles.Clear();
			int num = (int)buffer.ReadLong();
			for (int j = 0; j < num; j++)
			{
				int s = (int)buffer.ReadLong();
				PILSystem f = Common.DeserializeParticleSystem(sync);
				this.mParticles.Add(new PSystemIntPair(f, s));
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0008A69C File Offset: 0x0008889C
		private void SyncListSkeletons(DataSync sync, List<Skeleton> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Skeleton skeleton = new Skeleton();
					skeleton.SyncState(sync);
					theList.Add(skeleton);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Skeleton skeleton2 in theList)
			{
				skeleton2.SyncState(sync);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0008A73C File Offset: 0x0008893C
		public override bool Collides(Bullet b)
		{
			float num = (float)b.GetRadius() * Common._M(0.75f);
			Rect rect;
			rect = new Rect((int)(b.GetX() - num), (int)(b.GetY() - num), (int)(num * 2f), (int)(num * 2f));
			for (int i = 0; i < Common.size<Skeleton>(this.mSkeletons); i++)
			{
				Skeleton skeleton = this.mSkeletons[i];
				if (!skeleton.mActivated)
				{
					Rect rect2;
					rect2 = new Rect((int)(skeleton.mX + (float)Common._M(32)), (int)(skeleton.mY + (float)Common._M1(11)), Common._M2(45), Common._M3(79));
					if (rect.Intersects(rect2))
					{
						if (skeleton.mHasPowerup)
						{
							this.mLevel.mFrog.SetCannonCount(1, false, Common.Rand() % 4, (float)Common._M(0));
							skeleton.DoHit();
							this.mLevel.mFrog.AddPowerOrb(new SkeletonPowerOrb());
							this.mLevel.mBoard.ForceFlipFrog();
							float max_radius = (float)Common._M(50);
							float alpha_fade = Common._M(8f);
							float size_fade = Common._M(0.01f);
							float angle_inc = Common._M(0.1f);
							this.mLevel.mFrog.AddPowerRing(new OrbPowerRing(0f, max_radius, alpha_fade, size_fade, angle_inc));
							this.mLevel.mFrog.AddPowerRing(new OrbPowerRing(3.14159f, max_radius, alpha_fade, size_fade, angle_inc));
						}
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_SKELETON_HIT));
						skeleton.mActivated = true;
						return !b.GetIsCannon();
					}
				}
			}
			return base.Collides(b);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		public override void Update(float f)
		{
			base.Update(f);
			for (int i = 0; i < Common.size<PSystemIntPair>(this.mParticles); i++)
			{
				PILSystem first = this.mParticles[i].first;
				first.Update();
				if (this.mParticles[i].second != -1)
				{
					for (int j = 0; j < Common.size<BossBullet>(this.mBullets); j++)
					{
						if (this.mBullets[j].mId == this.mParticles[i].second)
						{
							first.SetPos(this.mBullets[j].mX + (float)Common._M(-15), this.mBullets[j].mY + (float)Common._M1(0));
							break;
						}
					}
				}
				if (first.Done())
				{
					first.Dispose();
					this.mParticles.RemoveAt(i);
					i--;
				}
			}
			if (this.mHitAlpha > 0f)
			{
				this.mHitAlpha -= Common._M(2f);
			}
			if (this.mDoDeathExplosions || this.mHP <= 0f || this.mLevel.mBoard.DoingBossIntro())
			{
				return;
			}
			if (!this.mLevel.AllCurvesAtRolloutPoint())
			{
				return;
			}
			Bullet bullet = this.mLevel.mFrog.GetBullet();
			if (this.mNumSkeleToEmit != 0 && --this.mCurSkeleDelay <= 0 && Common._geq(this.mAlphaOverride, 255f))
			{
				this.mCurSkeleDelay = this.mDelayAfterSkeleEmit;
				int num = (this.mNumSkeleToEmit == -1) ? 1 : this.mNumSkeleToEmit;
				for (int k = 0; k < num; k++)
				{
					Skeleton skeleton = new Skeleton();
					skeleton.mDelay = k * this.mSkeleDelay;
					skeleton.mX = this.mSkeletonEmitX;
					skeleton.mY = this.mSkeletonEmitY;
					skeleton.mVX = this.mSkeletonVX;
					skeleton.mVY = this.mSkeletonVY;
					if (!this.mSpawnPowerupWhilePoweredUp && bullet != null && bullet.GetIsCannon())
					{
						skeleton.mHasPowerup = false;
					}
					else if (Common.Rand(100) < this.mChanceToSpawnPowerup)
					{
						skeleton.mHasPowerup = true;
					}
					else
					{
						skeleton.mHasPowerup = false;
					}
					this.mSkeletons.Add(skeleton);
				}
			}
			else if (this.mAlphaOverride < 255f && this.mLevel.mBoard.GetGameState() == GameState.GameState_Losing)
			{
				for (int l = 0; l < Common.size<Skeleton>(this.mSkeletons); l++)
				{
					this.mSkeletons[l].mFadeOut = true;
					if (this.mSkeletons[l].mDelay > 0)
					{
						this.mSkeletons.RemoveAt(l);
						l--;
					}
				}
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_MINI_SKELETON);
			for (int m = 0; m < Common.size<Skeleton>(this.mSkeletons); m++)
			{
				Skeleton skeleton2 = this.mSkeletons[m];
				if (!Common._geq(this.mAlphaOverride, 255f))
				{
					skeleton2.mFadeOut = true;
				}
				skeleton2.Update();
				if ((skeleton2.mVX > 0f && skeleton2.mX > (float)Common._SS(this.mApp.mWidth)) || (skeleton2.mVX < 0f && skeleton2.mX + (float)imageByID.GetCelWidth() < 0f) || (skeleton2.mVY > 0f && skeleton2.mY > (float)Common._SS(this.mApp.mHeight)) || (skeleton2.mVY < 0f && skeleton2.mY + (float)imageByID.GetCelHeight() < 0f) || skeleton2.mEffectDone)
				{
					skeleton2.Dispose();
					this.mSkeletons.RemoveAt(m);
					m--;
				}
			}
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_HEAD_BURST);
			for (int n = 0; n < Common.size<int>(this.mHeadExplosions); n++)
			{
				if (this.mUpdateCount % Common._M(3) == 0)
				{
					List<int> list;
					int num2;
					(list = this.mHeadExplosions)[num2 = n] = list[num2] + 1;
				}
				if (this.mHeadExplosions[n] >= imageByID2.GetCelCount())
				{
					this.mHeadExplosions.RemoveAt(n);
					n--;
				}
			}
			this.mTimer++;
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_JAW);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_THROW);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_HIT);
			if (this.mState == 1 && Common._geq(this.mAlphaOverride, 255f))
			{
				if (this.mJawCel < imageByID3.mNumCols - 1)
				{
					if (this.mTimer % Common._M(6) == 0)
					{
						this.mJawCel++;
						return;
					}
				}
				else
				{
					if (this.mCel == 0)
					{
						this.mCel = 1;
					}
					imageByID4.GetCelCount();
					int mNumCols = imageByID5.mNumCols;
					int mNumRows = imageByID5.mNumRows;
					float num3 = Common._DS(Common._M(140f));
					if (this.mShrunkHeadYOff < num3)
					{
						this.mShrunkHeadYOff += Common._DS(Common._M(5f));
						if (this.mShrunkHeadYOff >= num3)
						{
							this.mShrunkHeadYOff = num3;
							this.mCel = 2;
							return;
						}
					}
					else if (this.mCel != 6 && this.mTimer % Common._M(3) == 0)
					{
						this.mCel++;
						if (this.mCel == 3)
						{
							this.mShrunkHeadYOff += (float)Common._M(5);
							return;
						}
						if (this.mCel == 4)
						{
							return;
						}
						if (this.mCel == 5)
						{
							this.mShrunkHeadYOff -= (float)Common._M(3);
							return;
						}
						if (this.mCel == 6)
						{
							this.mShrunkHeadYOff -= (float)Common._M(2);
							return;
						}
						if (this.mCel == imageByID4.GetCelCount())
						{
							this.mCel = 0;
							this.mShrunkHeadYOff = 0f;
							this.mState = 0;
							return;
						}
					}
					else if (this.mCel == 6 && this.mTimer % Common._M(12) == 0)
					{
						this.mCel++;
						for (int num4 = 0; num4 < Common.size<BossBullet>(this.mBullets); num4++)
						{
							BossBullet bossBullet = this.mBullets[num4];
							if (bossBullet.mState == 1)
							{
								int num5 = (int)(this.mX - (float)(this.mWidth / 2));
								int num6 = (int)(this.mY - (float)(this.mHeight / 2));
								bossBullet.mState++;
								Image image = BossSkeleton.gHeadImages[bossBullet.mImageNum];
								bossBullet.mX = (float)(num5 + Common._M(70) + imageByID.GetWidth() / 2);
								bossBullet.mY = (float)(num6 + Common._M(94) + (imageByID.GetHeight() / 2 - image.GetHeight())) + this.mShrunkHeadYOff;
								this.AddParticleSystem(bossBullet);
								return;
							}
						}
						return;
					}
				}
			}
			else if (this.mState == 0)
			{
				if (this.mJawCel > 0 && this.mTimer % Common._M(12) == 0)
				{
					this.mJawCel--;
					return;
				}
			}
			else if (this.mState == 2 && this.mTimer % Common._M(2) == 0 && ++this.mCel >= imageByID5.mNumCols * imageByID5.mNumRows)
			{
				this.mCel = 0;
				this.mState = 0;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0008B094 File Offset: 0x00089294
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0008B09D File Offset: 0x0008929D
		public override void Init(Level l)
		{
			base.Init(l);
			this.mBandagedImg = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_BANDAGED);
			BossSkeleton.gHeadImages[0] = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_SHRUNK_HEAD1);
			BossSkeleton.gHeadImages[1] = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_SHRUNK_HEAD2);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0008B0D8 File Offset: 0x000892D8
		public override Boss Instantiate()
		{
			BossSkeleton bossSkeleton = new BossSkeleton(this.mLevel);
			bossSkeleton.CopyFrom(this);
			bossSkeleton.mParticles.Clear();
			bossSkeleton.mSkeletons.Clear();
			return bossSkeleton;
		}

		// Token: 0x040015A4 RID: 5540
		private static Image[] gHeadImages = new Image[2];

		// Token: 0x040015A5 RID: 5541
		protected List<PSystemIntPair> mParticles = new List<PSystemIntPair>();

		// Token: 0x040015A6 RID: 5542
		protected List<int> mHeadExplosions = new List<int>();

		// Token: 0x040015A7 RID: 5543
		protected List<Skeleton> mSkeletons = new List<Skeleton>();

		// Token: 0x040015A8 RID: 5544
		protected int mState;

		// Token: 0x040015A9 RID: 5545
		protected int mCel;

		// Token: 0x040015AA RID: 5546
		protected int mJawCel;

		// Token: 0x040015AB RID: 5547
		protected int mTimer;

		// Token: 0x040015AC RID: 5548
		protected float mShrunkHeadYOff;

		// Token: 0x040015AD RID: 5549
		protected float mHitAlpha;

		// Token: 0x040015AE RID: 5550
		public bool mSpawnPowerupWhilePoweredUp;

		// Token: 0x040015AF RID: 5551
		public int mSpecialWpnHPDec = 5;

		// Token: 0x040015B0 RID: 5552
		public int mNumSkeleToEmit;

		// Token: 0x040015B1 RID: 5553
		public int mSkeleDelay;

		// Token: 0x040015B2 RID: 5554
		public int mDelayAfterSkeleEmit;

		// Token: 0x040015B3 RID: 5555
		public int mCurSkeleDelay;

		// Token: 0x040015B4 RID: 5556
		public int mChanceToSpawnPowerup = 4;

		// Token: 0x040015B5 RID: 5557
		public float mSkeletonVX;

		// Token: 0x040015B6 RID: 5558
		public float mSkeletonVY;

		// Token: 0x040015B7 RID: 5559
		public float mSkeletonEmitX;

		// Token: 0x040015B8 RID: 5560
		public float mSkeletonEmitY;

		// Token: 0x02000122 RID: 290
		public enum State
		{
			// Token: 0x040019AA RID: 6570
			State_Idle,
			// Token: 0x040019AB RID: 6571
			State_Firing,
			// Token: 0x040019AC RID: 6572
			State_Hit
		}
	}
}
