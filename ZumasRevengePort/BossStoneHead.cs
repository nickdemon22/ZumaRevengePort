using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200007C RID: 124
	public class BossStoneHead : BossShoot
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00076A74 File Offset: 0x00074C74
		protected override void DrawBossSpecificArt(SexyGraphics g)
		{
			if (this.mStretchPct >= BossStoneHead.MAX_STONE_HEAD_STRETCH)
			{
				if (this.mExplodeComp.GetUpdateCount() < Common._M(150))
				{
					int num = -(this.mApp.mWidth / 2 - Common._S(this.GetX())) + Common._S(Common._M(-193)) + this.mApp.mBoardOffsetX;
					int num2 = -(this.mApp.mHeight / 2 - Common._S(this.GetY())) + Common._S(Common._M(-91));
					CumulativeTransform cumulativeTransform = new CumulativeTransform();
					cumulativeTransform.mTrans.Translate((float)num, (float)num2);
					int num3 = (this.mExplodeComp.mUpdateCount >= this.mExplodeComp.GetMaxDuration()) ? (this.mExplodeComp.GetMaxDuration() - 1) : -1;
					this.mExplodeComp.Draw(g, cumulativeTransform, num3, Common._DS(1f));
				}
				else
				{
					this.mVolcanoBoss.Draw(g);
				}
			}
			float value = this.mX - (float)this.mWidth * this.mStretchPct / 2f + (float)this.mShakeXOff;
			float value2 = this.mY - (float)this.mHeight * this.mStretchPct / 2f + (float)this.mShakeYOff;
			for (int i = 0; i < this.mSteam.Count; i++)
			{
				Steam steam = this.mSteam[i];
				int num4 = (int)Math.Min(steam.mAlpha, this.mAlphaOverride);
				if (num4 != 255)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, num4);
				}
				if (!g.Is3D())
				{
					g.DrawImage(steam.mImage, (int)Common._S(this.mX + steam.mXOff + (float)Common._M(0)), (int)Common._S(this.mY + steam.mYOff + (float)Common._M1(0)), (int)(steam.mSize * (float)steam.mImage.mWidth), (int)(steam.mSize * (float)steam.mImage.mHeight));
				}
				else
				{
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(steam.mSize, steam.mSize);
					this.mGlobalTranform.RotateRad(steam.mAngle);
					if (g.Is3D())
					{
						g.DrawImageTransformF(steam.mImage, this.mGlobalTranform, Common._S(this.mX + steam.mXOff + (float)Common._M(0)), Common._S(this.mY + (float)Common._M1(0) + steam.mYOff));
					}
					else
					{
						g.DrawImageTransform(steam.mImage, this.mGlobalTranform, Common._S(this.mX + steam.mXOff + (float)Common._M(0)), Common._S(this.mY + (float)Common._M1(0) + steam.mYOff));
					}
				}
				g.SetColorizeImages(false);
			}
			if (this.mStretchPct < BossStoneHead.MAX_STONE_HEAD_STRETCH)
			{
				int num5 = 0;
				if (this.mHP <= 0f)
				{
					num5 = 1;
				}
				else if (this.mHitTimer > Common._M(194))
				{
					num5 = 1;
				}
				else if (this.mHitTimer > 0)
				{
					num5 = 2;
				}
				if (this.IMAGE_BOSS_STONEHEAD_FACES != null)
				{
					if (this.mHP <= 0f)
					{
						Rect rect;
						rect = new Rect((int)Common._S(value), (int)Common._S(value2), (int)((float)this.IMAGE_BOSS_STONEHEAD_FACES.GetCelWidth() * this.mStretchPct), (int)((float)this.IMAGE_BOSS_STONEHEAD_FACES.GetCelHeight() * this.mStretchPct));
						g.DrawImage(this.IMAGE_BOSS_STONEHEAD_FACES, rect, this.IMAGE_BOSS_STONEHEAD_FACES.GetCelRect(num5));
					}
					else
					{
						g.PushState();
						if (!Common._geq(this.mAlphaOverride, 255f))
						{
							g.SetColorizeImages(true);
							g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
						}
						g.DrawImageCel(this.IMAGE_BOSS_STONEHEAD_FACES, (int)Common._S(value), (int)Common._S(value2), num5);
						g.PopState();
					}
				}
			}
			g.PushState();
			if (!Common._geq(this.mAlphaOverride, 255f))
			{
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
				g.SetColorizeImages(true);
			}
			if (this.mHitTimer == 0 && !this.mDoingExplodeAnim && Common._geq(this.mAlphaOverride, 255f))
			{
				int num6 = Common._M(-1);
				int num7 = Common._M(0);
				if (this.IMAGE_BOSS_STONEHEAD_EYES != null)
				{
					g.DrawImageCel(this.IMAGE_BOSS_STONEHEAD_EYES, (int)(Common._S(value) + Common._DSA(50f, (float)num6)), (int)(Common._S(value2) + Common._DSA(58f, (float)num7)), this.mEyeFrame);
				}
			}
			if (!this.mDoingExplodeAnim)
			{
				g.PushState();
				this.mLeftEye.Draw(g);
				g.PopState();
				g.PushState();
				this.mRightEye.Draw(g);
				g.PopState();
			}
			for (int j = 0; j < this.mRocks.Count; j++)
			{
				RockChunk rockChunk = this.mRocks[j];
				if (rockChunk.mAlpha != 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)rockChunk.mAlpha);
				}
				if (this.IMAGE_BOSS_STONEHEAD_ROCKS != null)
				{
					g.DrawImage(this.IMAGE_BOSS_STONEHEAD_ROCKS, new Rect((int)Common._S(rockChunk.mX), (int)Common._S(rockChunk.mY), (int)((float)this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelWidth() * Common._M(0.5f)), (int)((float)this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelHeight() * Common._M1(0.5f))), this.IMAGE_BOSS_STONEHEAD_ROCKS.GetCelRect(rockChunk.mCol));
				}
				g.SetColorizeImages(false);
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				g.ClearClipRect();
			}
			if (!this.mDoingExplodeAnim && !this.mLevel.mBoard.IsPaused())
			{
				for (int k = 0; k < this.mBullets.Count; k++)
				{
					BossBullet bossBullet = this.mBullets[k];
					if (bossBullet.mDelay <= 0 && bossBullet.mState != 0)
					{
						EyeBullet eyeBullet = bossBullet.mData as EyeBullet;
						eyeBullet.Draw(g, (int)this.mAlphaOverride);
					}
				}
			}
			else if (this.mTextAlpha > 0f && this.mShowText)
			{
				g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
				g.SetColor(0, 0, 0, (int)Math.Min(this.mTextAlpha, 255f));
				float mTransX = g.mTransX;
				g.mTransX = 0f;
				if (!this.mLevel.mBoard.IsHardAdventureMode())
				{
					g.WriteString(TextManager.getInstance().getString(393), 0, Common._DS(Common._M(530)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(394), 0, Common._DS(Common._M(630)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(395), 0, Common._DS(Common._M(730)), this.mApp.mWidth, 0);
				}
				else
				{
					g.WriteString(TextManager.getInstance().getString(396), 0, Common._DS(Common._M(530)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(397), 0, Common._DS(Common._M(630)), this.mApp.mWidth, 0);
					g.WriteString(TextManager.getInstance().getString(398), 0, Common._DS(Common._M(730)), this.mApp.mWidth, 0);
				}
				g.mTransX = mTransX;
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			g.PopState();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000772A0 File Offset: 0x000754A0
		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			if (this.mDoingExplodeAnim)
			{
				return false;
			}
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_HIT));
			this.mHitTimer = Common._M(200);
			int num = Common._M(6);
			int num2 = (int)(this.mX - (float)(this.mWidth / 2));
			int num3 = (int)(this.mY - (float)(this.mHeight / 2));
			int num4 = num3 - Common._M(0);
			int num5 = num3 + Common._M(150);
			int num6 = (int)((float)(num5 - num4) / ((float)num / 2f));
			int num7 = num2 - Common._M(10);
			int num8 = Common._M(100);
			for (int i = 0; i < num; i++)
			{
				RockChunk rockChunk = new RockChunk();
				this.mRocks.Add(rockChunk);
				rockChunk.mCol = Common.Rand() % this.IMAGE_BOSS_STONEHEAD_ROCKS.mNumCols;
				rockChunk.mAlpha = 255f;
				rockChunk.mVX = 0f;
				rockChunk.mVY = Common.FloatRange(Common._M(3f), Common._M1(4f));
				rockChunk.mY = (float)(num4 + i / 2 * num6);
				rockChunk.mX = (float)(num7 + ((i % 2 == 0) ? num8 : 0));
			}
			bool flag = base.DoHit(b, from_prox_bomb);
			if (flag && Common._leq(this.mHP, 50f))
			{
				this.mVolcanoBoss = (this.mLevel.mSecondaryBoss as BossVolcano);
				this.mVolcanoBoss.mIntro = true;
				this.mVolcanoBoss.SetXY((float)this.GetX(), (float)this.GetY());
				this.mApp.mBoard.mDrawBossUI = false;
				this.mApp.mBoard.mMenuButton.SetVisible(false);
				SoundAttribs soundAttribs = new SoundAttribs();
				soundAttribs.delay = 130;
				this.mApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_TRANSFORM), soundAttribs);
				this.mApp.GetBoard().mPreventBallAdvancement = true;
				this.mPauseMovement = true;
				this.mDoingExplodeAnim = true;
				for (int j = 0; j < this.mBullets.Count; j++)
				{
					this.mBullets[j].mDeleteInstantly = true;
				}
				this.mTextAlpha = 0f;
				for (int k = 0; k < this.mHulaDancers.Count; k++)
				{
					this.mHulaDancers[k].mFadeOut = true;
				}
			}
			return flag;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00077524 File Offset: 0x00075724
		protected override void DidFire()
		{
			base.DidFire();
			this.mFiring = true;
			if (this.mLeftEye.mEyeFlame.mCurNumParticles == 0)
			{
				this.mLeftEye.mEyeFlame.ResetAnim();
			}
			if (this.mRightEye.mEyeFlame.mCurNumParticles == 0)
			{
				this.mRightEye.mEyeFlame.ResetAnim();
			}
			this.mLeftEye.mFiring = (this.mRightEye.mFiring = true);
			this.mLeftEye.mEyeFlame.mEmitAfterTimeline = (this.mRightEye.mEyeFlame.mEmitAfterTimeline = true);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000775C0 File Offset: 0x000757C0
		protected override bool PreBulletUpdate(BossBullet b, int index)
		{
			if (b.mState == 0)
			{
				return true;
			}
			if (b.mDelay > 0)
			{
				b.mDelay--;
				return true;
			}
			if (b.mData != null && ((EyeBullet)b.mData).Update((int)b.mX, (int)b.mY, b.mBouncesLeft <= 0))
			{
				b.mDeleteInstantly = true;
			}
			return false;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0007762C File Offset: 0x0007582C
		protected override BossBullet CreateBossBullet()
		{
			BossBullet bossBullet = new BossBullet();
			EyeBullet eyeBullet = new EyeBullet();
			bossBullet.mData = eyeBullet;
			eyeBullet.mExplosion = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJEXPLOSION").Duplicate();
			eyeBullet.mProjectile = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJ").Duplicate();
			eyeBullet.mProjectile.mEmitAfterTimeline = true;
			eyeBullet.mSparks = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJSPARKS").Duplicate();
			return bossBullet;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000776BC File Offset: 0x000758BC
		protected override void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
			if (b.mData != null)
			{
				EyeBullet eyeBullet = (EyeBullet)b.mData;
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mSparks);
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mProjectile);
				this.mApp.ReleaseGenericCachedEffect(eyeBullet.mExplosion);
				b.mData = null;
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00077718 File Offset: 0x00075918
		protected override Rect GetBulletRect(BossBullet b)
		{
			if (b.mData == null)
			{
				return new Rect(0, 0, 0, 0);
			}
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			return new Rect((int)(b.mX + (float)eyeBullet.mXOff), (int)(b.mY + (float)eyeBullet.mYOff), Common._DS(Common._M(14)), Common._DS(Common._M1(20)));
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00077780 File Offset: 0x00075980
		protected override void BulletHitPlayer(BossBullet b)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.fadeout = 0.1f;
			this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_BURNINGFROGLOOP), soundAttribs);
			this.mApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_NEW_FIREHITFROG));
			if (!this.mApp.GetLevelMgr().mBossesCanAttackFuckedFrog)
			{
				this.mLevel.mFrog.SetSlowTimer(0);
				this.mLevel.mBoard.SetHallucinateTimer(0);
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					if (bossBullet.mDelay > 0 || bossBullet.mState == 0)
					{
						bossBullet.mDeleteInstantly = true;
					}
				}
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00077844 File Offset: 0x00075A44
		protected override void GetShotBounceOffs(BossBullet b, ref int x, ref int y)
		{
			x = (y = 0);
			if (b.mData == null)
			{
				return;
			}
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			x += eyeBullet.mXOff + Common._DS(Common._M(0));
			y += eyeBullet.mYOff + Common._DS(Common._M(0));
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0007789C File Offset: 0x00075A9C
		protected override bool CanFire()
		{
			return !this.mDoingExplodeAnim;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000778A7 File Offset: 0x00075AA7
		protected override bool CanSpawnHulaDancers()
		{
			return !this.mDoingExplodeAnim;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000778B4 File Offset: 0x00075AB4
		protected override void ShotBounced(BossBullet b)
		{
			EyeBullet eyeBullet = (EyeBullet)b.mData;
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_EYE_LASER_BOUNCE));
			if (b.mBouncesLeft == 0)
			{
				eyeBullet.mExplosion.ResetAnim();
				eyeBullet.mProjectile.mEmitAfterTimeline = false;
				return;
			}
			if (eyeBullet.mSparks.mCurNumParticles == 0)
			{
				eyeBullet.mSparkFirstFrame = true;
				eyeBullet.mSparks.ResetAnim();
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00077921 File Offset: 0x00075B21
		protected override void BerserkActivated(int health_limit)
		{
			base.BerserkActivated(health_limit);
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_BERSERK));
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0007793F File Offset: 0x00075B3F
		protected override bool CanTaunt()
		{
			return !Common._leq(this.mHP, 50f) && base.CanTaunt();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0007795C File Offset: 0x00075B5C
		public BossStoneHead(Level l) : base(l)
		{
			this.mShouldDoDeathExplosions = false;
			this.mBossRadius = Common._M(70);
			this.mBulletRadius = Common._M(5);
			this.mResGroup = "Boss6Common";
			this.mDrawDeathBGTikis = false;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000779EB File Offset: 0x00075BEB
		public BossStoneHead() : this(null)
		{
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000779F4 File Offset: 0x00075BF4
		public override void Dispose()
		{
			if (this.mExplodeComp != null)
			{
				this.mExplodeComp.Dispose();
				this.mExplodeComp = null;
			}
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				if (this.mBullets[i].mData != null)
				{
					EyeBullet eyeBullet = (EyeBullet)this.mBullets[i].mData;
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mSparks);
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mProjectile);
					this.mApp.ReleaseGenericCachedEffect(eyeBullet.mExplosion);
				}
			}
			this.mApp.ReleaseGenericCachedEffect(this.mLeftEye.mEyeFlame);
			this.mApp.ReleaseGenericCachedEffect(this.mRightEye.mEyeFlame);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00077ABC File Offset: 0x00075CBC
		public void CopyForm(BossStoneHead rhs)
		{
			base.CopyFrom(rhs);
			this.mShakeTime = rhs.mShakeTime;
			this.mEyeFlameAlpha = rhs.mEyeFlameAlpha;
			this.mBlink = rhs.mBlink;
			this.mBlinkClosed = rhs.mBlinkClosed;
			this.mFiring = rhs.mFiring;
			this.mDoingExplodeAnim = rhs.mDoingExplodeAnim;
			this.mTextAlpha = rhs.mTextAlpha;
			this.mShowText = rhs.mShowText;
			this.mEyeFrame = rhs.mEyeFrame;
			this.mHitTimer = rhs.mHitTimer;
			this.mLeftInUse = rhs.mLeftInUse;
			this.mRightInUse = rhs.mRightInUse;
			this.mExplodeComp = rhs.mExplodeComp;
			this.mVolcanoBoss = rhs.mVolcanoBoss;
			this.mLeftEye = new EyeAnim(rhs.mLeftEye);
			this.mRightEye = new EyeAnim(rhs.mRightEye);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00077B9C File Offset: 0x00075D9C
		public override void Update(float f)
		{
			base.Update(f);
			if (this.mHitTimer > 0 && Common._geq(this.mAlphaOverride, 255f))
			{
				this.mHitTimer--;
				if (this.mUpdateCount % Common._M(2) == 0)
				{
					Steam steam = new Steam();
					this.mSteam.Add(steam);
					steam.mAlphaDec = Common._M(4f);
					steam.mAngleInc = Common.FloatRange(Common._M(0.01f), Common._M1(0.1f));
					steam.mVX = Common._M(-2f);
					steam.mVY = Common._M(-0.02f);
					steam.mImgNum = Common.Rand() % 2;
					steam.mImage = ((steam.mImgNum == 0) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
					steam = new Steam();
					this.mSteam.Add(steam);
					steam.mAlphaDec = Common._M(4f);
					steam.mAngleInc = Common.FloatRange(Common._M(0.01f), Common._M1(0.1f));
					steam.mVX = Common._M(2f);
					steam.mVY = Common._M(-0.02f);
					steam.mImgNum = Common.Rand() % 2;
					steam.mImage = ((steam.mImgNum == 0) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
				}
			}
			for (int i = 0; i < this.mRocks.Count; i++)
			{
				RockChunk rockChunk = this.mRocks[i];
				rockChunk.mY += rockChunk.mVY;
				rockChunk.mX += rockChunk.mVX;
				rockChunk.mAlpha -= Common._M(4.5f);
				if (rockChunk.mAlpha <= 0f)
				{
					this.mRocks.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < this.mSteam.Count; j++)
			{
				Steam steam2 = this.mSteam[j];
				steam2.mXOff += steam2.mVX;
				steam2.mYOff += steam2.mVY;
				steam2.mAngle += steam2.mAngleInc;
				steam2.mSize += Common._M(0.01f);
				if (Math.Abs(steam2.mXOff) >= Common._M(-1f))
				{
					steam2.mAlpha -= steam2.mAlphaDec;
					if (steam2.mAlpha <= 0f)
					{
						this.mSteam.RemoveAt(j);
						j--;
					}
				}
			}
			if (!this.mBlink && !this.mFiring && Common.Rand() % Common._M(400) == 0)
			{
				this.mBlink = (this.mBlinkClosed = true);
			}
			else if (this.mBlink && this.mUpdateCount % Common._M(5) == 0)
			{
				if (this.mBlinkClosed && ++this.mEyeFrame >= 3)
				{
					this.mBlinkClosed = false;
					this.mEyeFrame = 2;
				}
				else if (!this.mBlinkClosed && --this.mEyeFrame < 0)
				{
					this.mBlink = false;
					this.mEyeFrame = 0;
				}
			}
			if (this.mEyeFlameAlpha >= 255f && Common._geq(this.mAlphaOverride, 255f))
			{
				float mX = this.mX - (float)this.mWidth / 2f + (float)this.mShakeXOff;
				float mY = this.mY - (float)this.mHeight / 2f + (float)this.mShakeYOff;
				for (int k = 0; k < this.mBullets.Count; k++)
				{
					BossBullet bossBullet = this.mBullets[k];
					EyeBullet eyeBullet = (EyeBullet)bossBullet.mData;
					if (bossBullet.mState == 0)
					{
						if (!this.mLeftInUse)
						{
							this.mLeftInUse = true;
							bossBullet.mState = -1;
							bossBullet.mX = mX;
							bossBullet.mY = mY;
							eyeBullet.mXOff = Common._M(49);
							eyeBullet.mYOff = Common._M(55);
						}
						else if (!this.mRightInUse)
						{
							this.mRightInUse = true;
							bossBullet.mState = 1;
							bossBullet.mX = mX;
							bossBullet.mY = mY;
							eyeBullet.mXOff = Common._M(89);
							eyeBullet.mYOff = Common._M(55);
						}
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_STONE_EYE_LASER));
						if (bossBullet.mState != 0 && (bossBullet.mShotType == 1 || bossBullet.mShotType == 3))
						{
							base.FireBulletAtPlayer(bossBullet, Common.FloatRange(base.mMinBulletSpeed, base.mMaxBulletSpeed), bossBullet.mX + (float)eyeBullet.mXOff, bossBullet.mY + (float)eyeBullet.mYOff);
							bossBullet.mTargetVX = bossBullet.mVX;
							bossBullet.mTargetVY = bossBullet.mVY;
						}
					}
					else if (bossBullet.mUpdateCount > Common._M(50))
					{
						if (bossBullet.mState < 0)
						{
							this.mLeftInUse = false;
						}
						else
						{
							this.mRightInUse = false;
						}
					}
				}
				if (!this.mLeftInUse && !this.mRightInUse)
				{
					this.mLeftEye.mEyeFlame.mEmitAfterTimeline = (this.mRightEye.mEyeFlame.mEmitAfterTimeline = false);
					this.mLeftEye.mFiring = (this.mRightEye.mFiring = false);
					this.mFiring = false;
				}
			}
			if (this.mFiring)
			{
				if (this.mEyeFlameAlpha < 255f)
				{
					this.mEyeFlameAlpha += Common._M(4f);
					if (this.mEyeFlameAlpha > 255f)
					{
						this.mEyeFlameAlpha = 255f;
					}
				}
			}
			else if (this.mEyeFlameAlpha > 0f)
			{
				this.mEyeFlameAlpha -= Common._M(5f);
				if (this.mEyeFlameAlpha < 0f)
				{
					this.mEyeFlameAlpha = 0f;
				}
			}
			this.mLeftEye.Update((int)this.mX + BossStoneHead.LEFT_EYE_XOFF, (int)this.mY + BossStoneHead.EYE_YOFF, (int)this.mAlphaOverride);
			this.mRightEye.Update((int)this.mX + BossStoneHead.RIGHT_EYE_XOFF, (int)this.mY + BossStoneHead.EYE_YOFF, (int)this.mAlphaOverride);
			if (this.mDoingExplodeAnim)
			{
				bool flag = this.UpdateDeathSequence();
				if (flag)
				{
					this.mTextAlpha -= Common._M(1f);
					this.mExplodeComp.Update();
					if (this.mExplodeComp.GetUpdateCount() == 35)
					{
						this.DoDeathRockExplosionThing();
					}
					if (this.mExplodeComp.GetUpdateCount() >= Common._M(150))
					{
						this.mVolcanoBoss.Update();
					}
					if (this.mExplodeComp.Done() && this.mTextAlpha <= 0f)
					{
						this.mLevel.SwitchToSecondaryBoss();
						this.mVolcanoBoss.mIntro = false;
						this.mApp.GetBoard().mPreventBallAdvancement = false;
						this.mApp.mBoard.mDrawBossUI = true;
						this.mApp.mBoard.mMenuButton.SetVisible(true);
					}
				}
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00078304 File Offset: 0x00076504
		public override void Init(Level l)
		{
			this.mWidth = Common._M(120);
			this.mHeight = Common._M(182);
			base.Init(l);
			if (this.mExplodeComp != null)
			{
				this.mExplodeComp.Dispose();
				this.mExplodeComp = null;
			}
			this.mExplodeComp = new Composition();
			this.mExplodeComp.mLoadImageFunc = new AECommon.LoadCompImageFunc(GameApp.CompositionLoadFunc);
			this.mExplodeComp.mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(GameApp.CompositionPostLoadFunc);
			this.mExplodeComp.LoadFromFile("pax\\BreakEasterIsland_FINAL");
			this.mLeftEye.mEyeFlame = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSEYES").Duplicate();
			this.mRightEye.mEyeFlame = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSEYES").Duplicate();
			this.IMAGE_BOSS_STONEHEAD_FACES = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FACES);
			this.IMAGE_BOSS_STONEHEAD_ROCKS = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_ROCKS);
			this.IMAGE_BOSS_STONEHEAD_EYES = Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_EYES);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00078414 File Offset: 0x00076614
		public override Boss Instantiate()
		{
			BossStoneHead bossStoneHead = new BossStoneHead(this.mLevel);
			bossStoneHead.CopyFrom(this);
			bossStoneHead.mSteam.Clear();
			bossStoneHead.mRocks.Clear();
			return bossStoneHead;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0007844C File Offset: 0x0007664C
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			Buffer buffer = sync.GetBuffer();
			sync.SyncBoolean(ref this.mDoingExplodeAnim);
			sync.SyncFloat(ref this.mTextAlpha);
			sync.SyncBoolean(ref this.mShowText);
			sync.SyncLong(ref this.mExplodeComp.mUpdateCount);
			sync.SyncFloat(ref this.mStretchPct);
			sync.SyncLong(ref this.mShakeTime);
			sync.SyncFloat(ref this.mEyeFlameAlpha);
			sync.SyncBoolean(ref this.mBlink);
			sync.SyncBoolean(ref this.mBlinkClosed);
			sync.SyncBoolean(ref this.mFiring);
			sync.SyncLong(ref this.mEyeFrame);
			sync.SyncLong(ref this.mHitTimer);
			sync.SyncBoolean(ref this.mLeftInUse);
			sync.SyncBoolean(ref this.mRightInUse);
			this.mLeftEye.SyncState(sync);
			this.mRightEye.SyncState(sync);
			this.SyncListRockChunks(sync, this.mRocks, true);
			this.SyncListSteams(sync, this.mSteam, true);
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				if (sync.isWrite())
				{
					EyeBullet eyeBullet = (EyeBullet)this.mBullets[i].mData;
					eyeBullet.SyncState(sync);
				}
				else
				{
					EyeBullet eyeBullet2 = new EyeBullet();
					eyeBullet2.SyncState(sync);
					this.mBullets[i].mData = eyeBullet2;
				}
			}
			if (sync.isWrite())
			{
				buffer.WriteBoolean(this.mVolcanoBoss != null);
				if (this.mVolcanoBoss != null)
				{
					buffer.WriteLong((long)this.mVolcanoBoss.GetX());
					buffer.WriteLong((long)this.mVolcanoBoss.GetY());
					return;
				}
			}
			else if (sync.isRead())
			{
				if (buffer.ReadBoolean())
				{
					this.mVolcanoBoss = (BossVolcano)this.mLevel.mSecondaryBoss;
					this.mVolcanoBoss.mIntro = true;
					int num = (int)buffer.ReadLong();
					int num2 = (int)buffer.ReadLong();
					this.mVolcanoBoss.SetXY((float)num, (float)num2);
					return;
				}
				this.mVolcanoBoss = null;
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0007864C File Offset: 0x0007684C
		private void SyncListRockChunks(DataSync sync, List<RockChunk> theList, bool clear)
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
					RockChunk rockChunk = new RockChunk();
					rockChunk.SyncState(sync);
					theList.Add(rockChunk);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (RockChunk rockChunk2 in theList)
			{
				rockChunk2.SyncState(sync);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000786EC File Offset: 0x000768EC
		private void SyncListSteams(DataSync sync, List<Steam> theList, bool clear)
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
					Steam steam = new Steam();
					steam.SyncState(sync);
					theList.Add(steam);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Steam steam2 in theList)
			{
				steam2.SyncState(sync);
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0007878C File Offset: 0x0007698C
		public override bool AllowFrogToFire()
		{
			return base.AllowFrogToFire() && !this.mDoingExplodeAnim;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000787A4 File Offset: 0x000769A4
		public bool UpdateDeathSequence()
		{
			if (this.mShakeTime > 0)
			{
				if (this.mShakeTime == Common._M(150))
				{
					this.mShowText = true;
				}
				else if (this.mShakeTime < Common._M(150))
				{
					this.mTextAlpha += Common._M(2.8f);
				}
				this.mShakeTime--;
				if (this.mShakeTime % Common._M(50) == 0)
				{
					this.mShakeXAmt++;
					this.mShakeYAmt++;
				}
				this.mShakeXOff = Common.IntRange(0, this.mShakeXAmt);
				this.mShakeYOff = Common.IntRange(0, this.mShakeYAmt);
			}
			else
			{
				this.mShakeXOff = (this.mShakeYOff = 0);
			}
			for (int i = 0; i < this.mRocks.Count; i++)
			{
				RockChunk rockChunk = this.mRocks[i];
				rockChunk.mY += rockChunk.mVY;
				rockChunk.mX += rockChunk.mVX;
				rockChunk.mVY += Common._M(0.2f);
				rockChunk.mAlpha -= Common._M(4.5f);
				if (rockChunk.mAlpha <= 0f)
				{
					this.mRocks.RemoveAt(i);
					i--;
				}
			}
			if (Boss.gBerserkTextAlpha > 0f)
			{
				Boss.gBerserkTextAlpha -= Common._M(1f);
				Boss.gBerserkTextY -= Common._M(1f);
			}
			for (int j = 0; j < this.mSteam.Count; j++)
			{
				Steam steam = this.mSteam[j];
				steam.mXOff += steam.mVX;
				steam.mYOff += steam.mVY;
				steam.mAngle += steam.mAngleInc;
				steam.mSize += Common._M(0.01f);
				if (Math.Abs(steam.mXOff) >= Common._M(-1f))
				{
					steam.mAlpha -= steam.mAlphaDec;
					if (steam.mAlpha <= 0f)
					{
						this.mSteam.RemoveAt(j);
						j--;
					}
				}
			}
			if (this.mShakeTime <= 0)
			{
				this.mStretchPct += (BossStoneHead.MAX_STONE_HEAD_STRETCH - 1f) / Common._M(25f);
				if (this.mStretchPct >= BossStoneHead.MAX_STONE_HEAD_STRETCH)
				{
					this.mStretchPct = BossStoneHead.MAX_STONE_HEAD_STRETCH;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00078A4C File Offset: 0x00076C4C
		public void DoDeathRockExplosionThing()
		{
			for (int i = 45; i < 135; i += Common._M(2))
			{
				RockChunk rockChunk = new RockChunk();
				this.mRocks.Add(rockChunk);
				rockChunk.mCol = Common.Rand() % this.IMAGE_BOSS_STONEHEAD_ROCKS.mNumCols;
				rockChunk.mAlpha = 255f;
				float num = Common.DegreesToRadians((float)i);
				float num2 = Common.FloatRange(Common._M(4f), Common._M1(6f));
				rockChunk.mVX = num2 * (float)Math.Cos((double)num);
				rockChunk.mVY = -num2 * (float)Math.Sin((double)num);
				rockChunk.mX = this.mX;
				rockChunk.mY = this.mY;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00078B04 File Offset: 0x00076D04
		public override int GetTopLeftX()
		{
			return (int)(this.mX - (float)this.mWidth * this.mStretchPct / 2f);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00078B22 File Offset: 0x00076D22
		public override int GetTopLeftY()
		{
			return (int)(this.mY - (float)this.mHeight * this.mStretchPct / 2f);
		}

		// Token: 0x04001427 RID: 5159
		protected static float MAX_STONE_HEAD_STRETCH = 1.001f;

		// Token: 0x04001428 RID: 5160
		protected static int LEFT_EYE_XOFF = -16;

		// Token: 0x04001429 RID: 5161
		protected static int RIGHT_EYE_XOFF = 30;

		// Token: 0x0400142A RID: 5162
		protected static int EYE_YOFF = -35;

		// Token: 0x0400142B RID: 5163
		protected int mShakeTime = 300;

		// Token: 0x0400142C RID: 5164
		protected float mEyeFlameAlpha;

		// Token: 0x0400142D RID: 5165
		protected bool mBlink;

		// Token: 0x0400142E RID: 5166
		protected bool mBlinkClosed = true;

		// Token: 0x0400142F RID: 5167
		protected bool mFiring;

		// Token: 0x04001430 RID: 5168
		protected bool mDoingExplodeAnim;

		// Token: 0x04001431 RID: 5169
		protected float mTextAlpha;

		// Token: 0x04001432 RID: 5170
		protected bool mShowText;

		// Token: 0x04001433 RID: 5171
		protected int mEyeFrame;

		// Token: 0x04001434 RID: 5172
		protected int mHitTimer;

		// Token: 0x04001435 RID: 5173
		protected bool mLeftInUse;

		// Token: 0x04001436 RID: 5174
		protected bool mRightInUse;

		// Token: 0x04001437 RID: 5175
		protected EyeAnim mLeftEye = new EyeAnim();

		// Token: 0x04001438 RID: 5176
		protected EyeAnim mRightEye = new EyeAnim();

		// Token: 0x04001439 RID: 5177
		protected List<Steam> mSteam = new List<Steam>();

		// Token: 0x0400143A RID: 5178
		protected List<RockChunk> mRocks = new List<RockChunk>();

		// Token: 0x0400143B RID: 5179
		protected Composition mExplodeComp;

		// Token: 0x0400143C RID: 5180
		protected BossVolcano mVolcanoBoss;

		// Token: 0x0400143D RID: 5181
		private Image IMAGE_BOSS_STONEHEAD_FACES;

		// Token: 0x0400143E RID: 5182
		private Image IMAGE_BOSS_STONEHEAD_EYES;

		// Token: 0x0400143F RID: 5183
		private Image IMAGE_BOSS_STONEHEAD_ROCKS;

		// Token: 0x04001440 RID: 5184
		public float mStretchPct = 1f;
	}
}
