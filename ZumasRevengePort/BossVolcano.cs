using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000D1 RID: 209
	public class BossVolcano : BossShoot
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x0009763C File Offset: 0x0009583C
		protected override void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
			if (b.mData != null)
			{
				PIEffect fx = (PIEffect)b.mData;
				this.mApp.ReleaseVolcanoEffect(fx);
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0009766C File Offset: 0x0009586C
		protected override void DrawBossSpecificArt(SexyGraphics g)
		{
			float num = this.mX - (float)this.mWidth / 2f + (float)this.mShakeXAmt;
			float num2 = this.mY - (float)this.mHeight / 2f + (float)this.mShakeYAmt;
			g.PushState();
			if (!Common._geq(this.mAlphaOverride, 255f))
			{
				g.SetColor(255, 255, 255, (int)this.mAlphaOverride);
				g.SetColorizeImages(true);
			}
			if (this.mHitCel == -1)
			{
				g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_WINGS), (int)Common._S(num + (float)Common._M(28)), (int)Common._S(num2 + (float)Common._M1(39)), BossVolcano.WING_CELS[this.mWingIndex]);
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HEAD_BOWL), (int)Common._S(num + (float)Common._M(77)), (int)Common._S(num2 + (float)Common._M1(36)));
				g.PushState();
				if (this.mBoilingLava == null)
				{
					this.mBoilingLava = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_BOILING_DEVIL_HEAD");
					this.mBoilingLava.mEmitAfterTimeline = true;
				}
				this.mBoilingLava.mColor.mAlpha = (int)this.mAlphaOverride;
				this.mBoilingLava.Draw(g);
				g.PopState();
				g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HAND), (int)Common._S(num + (float)Common._M(55)), (int)Common._S(num2 + (float)Common._M1(87)), BossVolcano.HAND_CELS[this.mLeftHandIndex]);
				g.DrawImageMirror(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HAND), (int)Common._S(num + (float)Common._M(135)), (int)Common._S(num2 + (float)Common._M1(87)), Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HAND).GetCelRect(BossVolcano.HAND_CELS[this.mRightHandIndex]));
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				g.ClearClipRect();
			}
			if (!this.mLevel.mBoard.IsPaused())
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					if (bossBullet.mDelay <= 0 && bossBullet.mOffscreenPause > 0)
					{
						PIEffect pieffect = (PIEffect)bossBullet.mData;
						g.PushState();
						g.ClipRect(0, 0, GameApp.gApp.mWidth, Common._DS(Common._M(200)));
						pieffect.mColor.mAlpha = (int)this.mAlphaOverride;
						pieffect.Draw(g);
						g.PopState();
					}
				}
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			if (this.mHitCel == -1)
			{
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HEAD), (int)Common._S(num + (float)Common._M(55)), (int)Common._S(num2 + (float)Common._M1(23)));
				g.DrawImage(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_JAW), (int)Common._S(num + (float)Common._M(79)), (int)(Common._S(num2 + (float)Common._M1(143)) + this.mJawYOff));
			}
			else
			{
				g.DrawImageCel(Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HIT), (int)Common._S(num + (float)Common._M(-12)), (int)Common._S(num2 + (float)Common._M1(19)), this.mHitCel);
			}
			if (!this.mLevel.mBoard.IsPaused())
			{
				for (int j = 0; j < this.mBullets.Count; j++)
				{
					BossBullet bossBullet2 = this.mBullets[j];
					if (bossBullet2.mDelay <= 0 && bossBullet2.mOffscreenPause <= 0)
					{
						PIEffect pieffect2 = (PIEffect)bossBullet2.mData;
						g.PushState();
						pieffect2.mColor.mAlpha = 255;
						pieffect2.Draw(g);
						g.PopState();
					}
				}
			}
			g.PopState();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00097A48 File Offset: 0x00095C48
		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			if (this.mHitCel == -1)
			{
				this.mHitCel = 0;
			}
			bool flag = base.DoHit(b, from_prox_bomb);
			if (flag)
			{
				this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_DEVIL_HIT));
			}
			if (flag && this.mHP <= 0f)
			{
				this.mApp.GetBoard().mContinueNextLevelOnLoadProfile = true;
				this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_DEVIL_DEATH));
			}
			return flag;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00097ABD File Offset: 0x00095CBD
		protected override Rect GetBulletRect(BossBullet b)
		{
			return new Rect((int)b.mX - Common._M(15), (int)b.mY + Common._M1(20), Common._M2(20), Common._M3(55));
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00097AF0 File Offset: 0x00095CF0
		protected override bool CheckBulletHitPlayer(BossBullet b)
		{
			if (!b.mCanHitPlayer)
			{
				return false;
			}
			float y = (float)(this.mLevel.mFrog.GetCenterY() - 5);
			float x = (float)(this.mLevel.mFrog.GetCenterX() + 2);
			return MathUtils.CirclesIntersect(x, y, b.mX, b.mY, (float)(this.mBossRadius + Common._M(10)));
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00097B54 File Offset: 0x00095D54
		protected override void BulletHitPlayer(BossBullet b)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.fadeout = 0.1f;
			this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_BURNINGFROGLOOP), soundAttribs);
			this.mApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_NEW_FIREHITFROG));
			if (!this.mApp.GetLevelMgr().mBossesCanAttackFuckedFrog)
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					if (bossBullet.mOffscreenPause > 0)
					{
						bossBullet.mDeleteInstantly = true;
					}
				}
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00097BEC File Offset: 0x00095DEC
		protected override BossBullet CreateBossBullet()
		{
			BossBullet bossBullet = base.CreateBossBullet();
			bossBullet.mData = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_DEVIL_PROJECTILE").Duplicate();
			return bossBullet;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00097C21 File Offset: 0x00095E21
		protected override void DidFire()
		{
			this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_DEVIL_FIRES));
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00097C38 File Offset: 0x00095E38
		public BossVolcano(Level l) : base(l)
		{
			this.mTauntTextYOff = Common._DS(Common._M(20));
			this.mBoilingLava = null;
			this.mBulletsUseSphereColl = true;
			this.mBossRadius = Common._M(70);
			this.mBulletRadius = Common._M(25);
			this.mDrawDeathBGTikis = false;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00097C9B File Offset: 0x00095E9B
		public BossVolcano() : this(null)
		{
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00097CA4 File Offset: 0x00095EA4
		public override void Dispose()
		{
			base.Dispose();
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				BossBullet bossBullet = this.mBullets[i];
				if (bossBullet.mData != null)
				{
					this.mApp.ReleaseVolcanoEffect((PIEffect)bossBullet.mData);
				}
			}
			this.mBoilingLava = null;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00097D00 File Offset: 0x00095F00
		protected void CopyFrom(BossVolcano rhs)
		{
			base.CopyFrom(rhs);
			this.mBoilingLava = rhs.mBoilingLava;
			this.mWingIndex = rhs.mWingIndex;
			this.mLeftHandIndex = rhs.mLeftHandIndex;
			this.mRightHandIndex = rhs.mRightHandIndex;
			this.mJawCount = rhs.mJawCount;
			this.mHitCel = rhs.mHitCel;
			this.mJawYOff = rhs.mJawYOff;
			this.mJawRate = rhs.mJawRate;
			this.mAnimateHands = rhs.mAnimateHands;
			this.mIntro = rhs.mIntro;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00097D8C File Offset: 0x00095F8C
		public override void Update(float f)
		{
			if (!this.mIntro)
			{
				base.Update(f);
			}
			else
			{
				this.mUpdateCount++;
			}
			Common._M(0.3f);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_VOLCANO_HIT);
			if (this.mHitCel >= 0 && !this.mIntro && this.mUpdateCount % BossVolcano.HIT_TIMES[this.mHitCel] == 0 && ++this.mHitCel >= imageByID.mNumCols)
			{
				this.mHitCel = -1;
			}
			if (this.mBoilingLava == null)
			{
				this.mBoilingLava = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_BOILING_DEVIL_HEAD");
				this.mBoilingLava.mEmitAfterTimeline = true;
			}
			this.mBoilingLava.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1.4f);
			this.mBoilingLava.mDrawTransform.Scale(num, num);
			this.mBoilingLava.mDrawTransform.Translate(Common._S(this.mX + (float)Common._M(10)), Common._S(this.mY + (float)Common._M1(-40)));
			this.mBoilingLava.Update();
			if (this.mHP > 0f)
			{
				if (this.mUpdateCount % Common._M(15) == 0)
				{
					this.mWingIndex = (this.mWingIndex + 1) % BossVolcano.NUM_WING_FRAMES;
				}
				if (this.mUpdateCount % Common._M(8) == 0 && this.mAnimateHands)
				{
					this.mLeftHandIndex = (this.mLeftHandIndex + 1) % BossVolcano.NUM_HAND_FRAMES;
					this.mRightHandIndex = (this.mRightHandIndex + 1) % BossVolcano.NUM_HAND_FRAMES;
					if (this.mLeftHandIndex == 0)
					{
						this.mAnimateHands = false;
					}
				}
				if (this.mJawRate == 0f && Common.Rand(400) == 0)
				{
					this.mJawRate = Common._M(-1f);
				}
				if (Common.Rand(100) == 0 && this.mLeftHandIndex == 0)
				{
					this.mAnimateHands = true;
				}
			}
			if (this.mJawRate != 0f)
			{
				this.mJawYOff += this.mJawRate;
				if (this.mJawRate < 0f && this.mJawYOff <= -8f)
				{
					this.mJawYOff = -8f;
					this.mJawRate *= -1f;
				}
				else if (this.mJawRate > 0f && this.mJawYOff >= 0f)
				{
					if (++this.mJawCount == 2)
					{
						this.mJawCount = 0;
						this.mJawYOff = (this.mJawRate = 0f);
					}
					else
					{
						this.mJawYOff = 0f;
						this.mJawRate *= -1f;
					}
				}
			}
			if (!this.mIntro && Common._geq(this.mAlphaOverride, 255f))
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					PIEffect pieffect = (PIEffect)bossBullet.mData;
					if (pieffect != null && !pieffect.mEmitAfterTimeline)
					{
						pieffect.mEmitAfterTimeline = true;
						Common.SetFXNumScale(pieffect, 3f);
					}
					if (bossBullet.mState == 0 && bossBullet.mDelay <= 0)
					{
						bossBullet.mState++;
						bossBullet.mCanHitPlayer = false;
						bool flag = this.mX > this.mDestX;
						bossBullet.mX = this.mX + (float)(flag ? Common._M(5) : Common._M1(30));
						bossBullet.mY = this.mY + (float)Common._M(50);
						pieffect.mDrawTransform.LoadIdentity();
						num = GameApp.DownScaleNum(1.4f);
						pieffect.mDrawTransform.Scale(num, num);
						pieffect.mDrawTransform.Scale(1f, -1f);
						pieffect.mDrawTransform.Translate(Common._S(bossBullet.mX + (float)Common._M(0)), Common._S(bossBullet.mY + (float)Common._M1(0)));
						pieffect.Update();
					}
					else if (bossBullet.mState == 1 && bossBullet.mY >= (float)(this.mLevel.mFrog.GetCenterY() - Common._M(0)))
					{
						bossBullet.mData = null;
						bossBullet.mState++;
						bossBullet.mCanHitPlayer = true;
						bossBullet.mVY = (bossBullet.mTargetVY = 0f);
						PIEffect pieffect2 = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_DEVIL_EXPLOSION").Duplicate();
						pieffect2.mEmitAfterTimeline = true;
						pieffect2.mDrawTransform.LoadIdentity();
						num = GameApp.DownScaleNum(1.4f);
						pieffect2.mDrawTransform.Scale(num, num);
						pieffect2.mDrawTransform.Translate(Common._S(bossBullet.mX + (float)Common._M(0)), Common._S(bossBullet.mY + (float)Common._M1(0)));
						pieffect2.Update();
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS_DEVIL_EXPLODES));
						bossBullet.mData = pieffect2;
					}
					else if (bossBullet.mState == 1)
					{
						pieffect.mDrawTransform.LoadIdentity();
						num = GameApp.DownScaleNum(1.4f);
						pieffect.mDrawTransform.Scale(num, num);
						if (bossBullet.mOffscreenPause > 0)
						{
							bool flag2 = this.mX > this.mDestX;
							bossBullet.mX = this.mX + (float)(flag2 ? Common._M(5) : Common._M1(5));
							pieffect.mDrawTransform.Scale(1f, -1f);
							pieffect.mDrawTransform.Translate(Common._S(bossBullet.mX + (float)Common._M(0)), Common._S(bossBullet.mY + (float)Common._M1(-30)));
						}
						else
						{
							pieffect.mDrawTransform.Scale(1f, 1f);
							pieffect.mDrawTransform.Translate(Common._S(bossBullet.mX + (float)Common._M(0)), Common._S(bossBullet.mY + (float)Common._M1(0)));
						}
						pieffect.Update();
					}
					else if (bossBullet.mState == 2)
					{
						PIEffect pieffect3 = (PIEffect)bossBullet.mData;
						pieffect3.Update();
						if (pieffect3.mFrameNum > (float)(pieffect3.mLastFrameNum - Common._M(20)))
						{
							bossBullet.mCanHitPlayer = false;
							this.mApp.ReleaseVolcanoEffect(pieffect3);
							bossBullet.mData = null;
							this.BossBulletDestroyed(bossBullet, false);
							this.mBullets.RemoveAt(i);
							i--;
						}
					}
				}
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00098418 File Offset: 0x00096618
		public override void Init(Level l)
		{
			this.mWidth = Common._M(225);
			this.mHeight = Common._M(225);
			base.Init(l);
			for (int i = 0; i < Boss.NUM_HEARTS; i++)
			{
				this.mHeartCels[i] = 0;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00098468 File Offset: 0x00096668
		public override Boss Instantiate()
		{
			BossVolcano bossVolcano = new BossVolcano();
			bossVolcano.CopyFrom(this);
			return bossVolcano;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00098484 File Offset: 0x00096684
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncLong(ref this.mWingIndex);
			sync.SyncLong(ref this.mLeftHandIndex);
			sync.SyncLong(ref this.mRightHandIndex);
			sync.SyncLong(ref this.mJawCount);
			sync.SyncLong(ref this.mHitCel);
			sync.SyncFloat(ref this.mJawYOff);
			sync.SyncFloat(ref this.mJawRate);
			sync.SyncBoolean(ref this.mAnimateHands);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					BossBullet bossBullet = this.mBullets[i];
					PIEffect s = (PIEffect)bossBullet.mData;
					buffer.WriteBoolean(bossBullet.mState == 2);
					Common.SerializePIEffect(s, sync);
				}
				return;
			}
			for (int j = 0; j < this.mBullets.Count; j++)
			{
				PIEffect pieffect;
				if (buffer.ReadBoolean())
				{
					pieffect = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_DEVIL_EXPLOSION").Duplicate();
				}
				else
				{
					pieffect = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_DEVIL_PROJECTILE").Duplicate();
				}
				Common.DeserializePIEffect(pieffect, sync);
				pieffect.mEmitAfterTimeline = true;
				this.mBullets[j].mData = pieffect;
			}
		}

		// Token: 0x0400177D RID: 6013
		private static int BV_WIDTH = 225;

		// Token: 0x0400177E RID: 6014
		private static int BV_HEIGHT = 225;

		// Token: 0x0400177F RID: 6015
		private static int NUM_WING_FRAMES = 4;

		// Token: 0x04001780 RID: 6016
		private static int[] WING_CELS = new int[]
		{
			1,
			2,
			1,
			0
		};

		// Token: 0x04001781 RID: 6017
		private static int NUM_HAND_FRAMES = 4;

		// Token: 0x04001782 RID: 6018
		private static int[] HAND_CELS = new int[]
		{
			1,
			2,
			3,
			0
		};

		// Token: 0x04001783 RID: 6019
		private static int NUM_HIT_FRAMES = 4;

		// Token: 0x04001784 RID: 6020
		private static int[] HIT_TIMES = new int[]
		{
			8,
			8,
			8,
			15
		};

		// Token: 0x04001785 RID: 6021
		public bool mIntro;

		// Token: 0x04001786 RID: 6022
		protected PIEffect mBoilingLava;

		// Token: 0x04001787 RID: 6023
		protected int mWingIndex;

		// Token: 0x04001788 RID: 6024
		protected int mLeftHandIndex;

		// Token: 0x04001789 RID: 6025
		protected int mRightHandIndex = 2;

		// Token: 0x0400178A RID: 6026
		protected int mJawCount;

		// Token: 0x0400178B RID: 6027
		protected int mHitCel = -1;

		// Token: 0x0400178C RID: 6028
		protected float mJawYOff;

		// Token: 0x0400178D RID: 6029
		protected float mJawRate;

		// Token: 0x0400178E RID: 6030
		protected bool mAnimateHands;
	}
}
