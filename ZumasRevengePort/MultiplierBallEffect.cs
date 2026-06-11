using System;
using System.Collections.Generic;
using JeffLib;
using Microsoft.Xna.Framework;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	// Token: 0x02000075 RID: 117
	public class MultiplierBallEffect
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0006D038 File Offset: 0x0006B238
		protected void InitSpawnEffects()
		{
			if (this.mMultBall == null)
			{
				return;
			}
			this.mSpawnEffect = new SpawnEffect();
			PILSystem system = this.mSpawnEffect.mRings;
			system.SetLife(Common._M(100));
			Emitter emitter = new Emitter();
			emitter.mDeleteInvisParticles = true;
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			EmitterScale emitterScale = new EmitterScale();
			emitter.mTintColor = MultiplierBallEffect.gSpawnColors[this.mMultBall.GetColorType()].mRings;
			emitterScale.mLifeScale = Common._M(0.125f);
			emitterScale.mNumberScale = Common._M(0.96f);
			emitterScale.mSizeXScale = Common._M(0.5f);
			emitterScale.mVelocityScale = Common._M(6.52f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(1.87f);
			emitter.AddScaleKeyFrame(Common._M(52), emitterScale);
			EmitterSettings emitterSettings = new EmitterSettings();
			emitterSettings.mTintStrength = 1f;
			emitterSettings.mVisibility = Common._M(0.76f);
			emitter.AddSettingsKeyFrame(0, emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitterSettings.mVisibility = Common._M(0.69f);
			emitter.AddSettingsKeyFrame(Common._M(21), emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitterSettings.mVisibility = 0f;
			emitter.AddSettingsKeyFrame(Common._M(60), emitterSettings);
			ParticleType particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_RING);
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = 1f;
			particleType.mColorKeyManager.AddColorKey(0f, Color.White);
			particleType.mColorKeyManager.AddColorKey(1f, Color.Black);
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.5f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(96),
				mNumber = (int)((float)Common._M(6) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE),
				mXSize = Common._M(69)
			});
			LifetimeSettings lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = Common._M(2f);
			lifetimeSettings.mVelocityMult = Common._M(1.6f);
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mVelocityMult = Common._M(1f)
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
			system = this.mSpawnEffect.mSwirl;
			system.SetLife(Common._M(56));
			emitter = new Emitter();
			emitter.mDeleteInvisParticles = true;
			emitter.mEmissionCoordsAreOffsets = true;
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitterScale = new EmitterScale();
			emitter.mTintColor = MultiplierBallEffect.gSpawnColors[this.mMultBall.GetColorType()].mSwirl;
			emitter.SetEmitterType(2);
			emitter.mEmitDir = 1;
			emitter.mEmitAtXPoints = Common._M(20);
			emitter.mLinearEmitAtPoints = true;
			emitterScale.mLifeScale = Common._M(0.1f);
			emitterScale.mNumberScale = Common._M(20f);
			emitterScale.mSizeXScale = Common._M(5f);
			emitterScale.mVelocityScale = Common._M(2f);
			emitterScale.mZoom = Common._M(0.5f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterSettings = new EmitterSettings();
			emitterSettings.mTintStrength = Common._M(0.81f);
			emitterSettings.mEmissionAngle = 0f;
			emitterSettings.mEmissionRange = Common.DegreesToRadians(1f);
			emitterSettings.mXRadius = (emitterSettings.mYRadius = 5f);
			emitter.AddSettingsKeyFrame(0, emitterSettings);
			particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_STARBURST);
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = 1f;
			particleType.mColorKeyManager.AddColorKey(0f, Color.White);
			particleType.mColorKeyManager.AddColorKey(Common._M(0.9f), Color.White);
			particleType.mColorKeyManager.AddColorKey(1f, Color.Black);
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.9f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			ParticleSettings particleSettings = new ParticleSettings();
			particleSettings.mLife = Common._M(70);
			particleSettings.mNumber = (int)((float)Common._M(10) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mXSize = Common._M(11);
			particleSettings.mVelocity = Common._M(83);
			particleSettings.mWeight = (float)Common._M(2);
			particleType.AddSettingsKeyFrame(0, particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mLife = Common._M(70);
			particleType.AddSettingsKeyFrame(Common._M(10), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mLife = 0;
			particleType.AddSettingsKeyFrame(Common._M(22), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = 1f;
			particleType.AddSettingsKeyFrame(Common._M(48), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = 0f;
			particleType.AddSettingsKeyFrame(Common._M(56), particleSettings);
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mVelocityMult = Common._M(2f);
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			particleType.AddSettingAtLifePct(Common._M(0.78f), lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(2f);
			lifetimeSettings.mVelocityMult = Common._M(1.7f);
			particleType.AddSettingAtLifePct(Common._M(0.92f), lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mVelocityMult = Common._M(1.65f);
			particleType.AddSettingAtLifePct(Common._M(0.94f), lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mVelocityMult = 0f
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
		protected void UpdateStateSpawn()
		{
			this.mSpawnTimer++;
			if (this.mSpawnTimer % Common._M(30) == 0 && this.mMultBall != null && this.mSpawnTimer < Common._M1(50))
			{
				AlphaFadeInfo alphaFadeInfo = new AlphaFadeInfo();
				alphaFadeInfo.second = false;
				alphaFadeInfo.first = new AlphaFader();
				alphaFadeInfo.first.mColor = new FColor(MultiplierBallEffect.gSpawnColors[this.mMultBall.GetColorType()].mBeam);
				alphaFadeInfo.first.mColor.mAlpha = 0f;
				alphaFadeInfo.first.mMin = 0;
				alphaFadeInfo.first.mMax = 255;
				alphaFadeInfo.first.mFadeRate = Common._M(6f);
				this.mBeamAlphas.Add(alphaFadeInfo);
			}
			for (int i = 0; i < Common.size<AlphaFadeInfo>(this.mBeamAlphas); i++)
			{
				AlphaFadeInfo alphaFadeInfo2 = this.mBeamAlphas[i];
				alphaFadeInfo2.first.Update();
				if (!alphaFadeInfo2.second && Common._eq(alphaFadeInfo2.first.mColor.mAlpha, (float)alphaFadeInfo2.first.mMax))
				{
					alphaFadeInfo2.second = true;
					alphaFadeInfo2.first.mFadeRate = Common._M(-10f);
				}
				else if (alphaFadeInfo2.second && Common._leq(alphaFadeInfo2.first.mColor.mAlpha, (float)alphaFadeInfo2.first.mMin))
				{
					this.mBeamAlphas.RemoveAt(i);
					i--;
				}
			}
			if (this.mSpawnTimer >= Common._M(50))
			{
				this.mSpawnEffect.mRings.SetPos(this.mLastBallX, this.mLastBallY);
				this.mSpawnEffect.mRings.Update();
				this.mSpawnEffect.mSwirl.SetPos(this.mLastBallX, this.mLastBallY);
				this.mSpawnEffect.mSwirl.Update();
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0006D8D0 File Offset: 0x0006BAD0
		protected void DrawStateSpawn(SexyGraphics g)
		{
			if (this.mSpawnTimer >= Common._M(50))
			{
				this.mSpawnEffect.mRings.Draw(g);
				this.mSpawnEffect.mSwirl.Draw(g);
			}
			int num = Common._M(228);
			int num2 = Common._M(21);
			float num3 = Common.AngleBetweenPoints(this.mLastBallX, this.mLastBallY, (float)num, (float)num2) - Common.JL_PI / 2f;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_PARTICLE_BEAM);
			int mWidth = imageByID.mWidth;
			int mHeight = imageByID.mHeight;
			float num4 = Common._S(Common.Distance(this.mLastBallX, this.mLastBallY, (float)num, (float)num2) + (float)Common._M(17));
			float num5 = num4 / (float)mHeight;
			float num6 = (float)mHeight * num5;
			this.mGlobalTranform.Reset();
			this.mGlobalTranform.Scale(1f, num5);
			this.mGlobalTranform.Translate((float)Common._M(0), num6 / 2f);
			this.mGlobalTranform.RotateRad(num3);
			this.mGlobalTranform.Translate((float)Common._M(0), -num6 / 2f);
			for (int i = 0; i < Common.size<AlphaFadeInfo>(this.mBeamAlphas); i++)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mBeamAlphas[i].first.mColor);
				g.DrawImageTransform(imageByID, this.mGlobalTranform, (float)Common._S(num), (float)Common._S(num2) + num4 / 2f);
				g.SetDrawMode(1);
				g.DrawImageTransform(imageByID, this.mGlobalTranform, (float)Common._S(num), (float)Common._S(num2) + num4 / 2f);
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0006DA98 File Offset: 0x0006BC98
		protected void InitTriggeredEffects()
		{
			if (this.mTriggeredEffect != null)
			{
				return;
			}
			this.mTriggeredEffect = new TriggeredEffect();
			PILSystem system = this.mTriggeredEffect.mRings;
			system.SetLife(Common._M(44));
			Emitter emitter = new Emitter();
			emitter.mDeleteInvisParticles = true;
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitter.mDeleteInvisParticles = true;
			EmitterScale emitterScale = new EmitterScale();
			emitterScale.mLifeScale = Common._M(0.3f);
			emitterScale.mNumberScale = Common._M(1.06f);
			emitterScale.mSizeXScale = Common._M(1.55f);
			emitterScale.mVelocityScale = Common._M(0.79f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(2.79f);
			emitter.AddScaleKeyFrame(Common._M(29), emitterScale);
			EmitterSettings emitterSettings = new EmitterSettings();
			emitterSettings.mVisibility = Common._M(0.36f);
			emitter.AddSettingsKeyFrame(0, emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitterSettings.mVisibility = Common._M(1f);
			emitter.AddSettingsKeyFrame(Common._M(19), emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitterSettings.mVisibility = 0f;
			emitter.AddSettingsKeyFrame(Common._M(44), emitterSettings);
			ParticleType particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_RING);
			particleType.mName = "Rings";
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = 1f;
			particleType.mColorKeyManager.AddColorKey(0f, new Color(96, 255, 139));
			particleType.mColorKeyManager.AddColorKey(0.12f, new Color(213, 255, 87));
			particleType.mColorKeyManager.AddColorKey(0.28f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(0.54f, new Color(0, 72, 255));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(12, 0, 255));
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.6f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(6),
				mNumber = (int)((float)Common._M(10) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE),
				mXSize = Common._S(Common._M(69))
			});
			particleType.AddSettingAtLifePct(0f, new LifetimeSettings
			{
				mSizeXMult = Common._M(0.38f),
				mVelocityMult = Common._M(1.7f)
			});
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings
			{
				mSizeXMult = Common._M(1.4f),
				mVelocityMult = Common._M(1f)
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
			int num = Common._M(100);
			system = this.mTriggeredEffect.mRainbow;
			system.SetLife(num);
			emitter = new Emitter();
			emitter.mDeleteInvisParticles = true;
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitterScale = new EmitterScale();
			emitterScale.mLifeScale = Common._M(1f);
			emitterScale.mNumberScale = Common._M(0.5f);
			emitterScale.mSpinScale = Common._M(0.19f);
			emitterScale.mSizeXScale = Common._M(1.24f);
			emitterScale.mSizeYScale = Common._M(0.9f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitter.AddScaleKeyFrame(Common._M(9), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(1.24f);
			emitterScale.mSizeYScale = Common._M(0.71f);
			emitter.AddScaleKeyFrame(Common._M(15), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(1f);
			emitterScale.mSizeYScale = 0f;
			emitter.AddScaleKeyFrame(Common._M(33), emitterScale);
			emitter.AddScaleKeyFrame(num, new EmitterScale(emitterScale)
			{
				mSizeXScale = 0f
			});
			emitter.AddSettingsKeyFrame(0, new EmitterSettings
			{
				mVisibility = Common._M(0.58f)
			});
			emitterSettings = new EmitterSettings();
			emitterSettings.mVisibility = Common._M(1f);
			emitter.AddSettingsKeyFrame(Common._M(7), emitterSettings);
			emitterSettings = new EmitterSettings();
			emitter.AddSettingsKeyFrame(Common._M(19), emitterSettings);
			emitterSettings = new EmitterSettings();
			emitterSettings.mVisibility = Common._M(0.29f);
			emitter.AddSettingsKeyFrame(Common._M(50), emitterSettings);
			emitter.AddSettingsKeyFrame(num, new EmitterSettings
			{
				mVisibility = 0f
			});
			particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_BEAM);
			particleType.mName = "Rainbow beam crap";
			particleType.mAdditive = true;
			particleType.mAdditiveWithNormal = true;
			particleType.mRefYOff = (int)(4f * (float)Common._M(-300));
			particleType.mInitAngle = 3.1415927f;
			particleType.mAngleRange = 6.2831855f;
			particleType.mInitAngleStep = Common._M(0.5f);
			particleType.mLockSizeAspect = false;
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 0);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.25f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.75f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			particleType.mColorKeyManager.AddColorKey(0f, new Color(0, 220, 250));
			particleType.mColorKeyManager.AddColorKey(0.25f, new Color(51, 0, 255));
			particleType.mColorKeyManager.AddColorKey(0.375f, new Color(225, 0, 255));
			particleType.mColorKeyManager.AddColorKey(0.5f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(0.675f, new Color(225, 123, 0));
			particleType.mColorKeyManager.AddColorKey(0.75f, new Color(7, 255, 12));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(229, 255, 0));
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(9),
				mNumber = (int)((float)Common._M(80) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE),
				mXSize = Common._M(50),
				mYSize = Common._M(159),
				mSpin = Common.DegreesToRadians((float)Common._M(-70))
			});
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mSpinVar = Common.DegreesToRadians((float)Common._M(68))
			});
			LifetimeSettings lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = 1f;
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			particleType.AddSettingAtLifePct(0.7f, lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mSizeXMult = 0f
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
			system = this.mTriggeredEffect.mGas;
			system.SetLife(Common._M(200));
			emitter = new Emitter();
			emitter.mDeleteInvisParticles = true;
			emitter.mPreloadFrames = Common._M(0);
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitterScale = new EmitterScale();
			emitterScale.mLifeScale = Common._M(0.56f);
			emitterScale.mNumberScale = Common._M(0.45f);
			emitterScale.mSizeXScale = Common._M(2.87f);
			emitterScale.mVelocityScale = Common._M(0.18f);
			emitterScale.mZoom = Common._M(0.33f);
			emitterScale.mSpinScale = Common._M(1.75f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mZoom = Common._M(0.5f);
			emitter.AddScaleKeyFrame(Common._M(25), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitter.AddScaleKeyFrame(Common._M(75), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mVelocityScale = Common._M(0.48f);
			emitter.AddScaleKeyFrame(Common._M(87), emitterScale);
			emitter.AddSettingsKeyFrame(0, new EmitterSettings
			{
				mVisibility = Common._M(0.56f)
			});
			particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_SPIKEYCIRCLE);
			particleType.mAdditive = true;
			particleType.mColorKeyManager.AddColorKey(0f, new Color(232, 255, 32));
			particleType.mColorKeyManager.AddColorKey(Common._M(0.375f), new Color(98, 254, 255));
			particleType.mColorKeyManager.AddColorKey(Common._M(0.675f), new Color(255, 101, 206));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(21, 9, 34));
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.75f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 0);
			particleType.mColorKeyManager.SetColorMode(2);
			particleType.mName = "clouds";
			ParticleSettings particleSettings = new ParticleSettings();
			particleSettings.mLife = 0;
			particleSettings.mNumber = (int)((float)Common._M(10) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mXSize = Common._M(30);
			particleSettings.mVelocity = Common._M(243);
			particleSettings.mMotionRand = (float)Common._M(57);
			particleSettings.mGlobalVisibility = Common._M(0.56f);
			particleType.AddSettingsKeyFrame(0, particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mVelocity = Common._M(281);
			particleType.AddSettingsKeyFrame(Common._M(12), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mLife = Common._M(30);
			particleSettings.mVelocity = Common._M(333);
			particleType.AddSettingsKeyFrame(Common._M(30), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mLife = 0;
			particleSettings.mVelocity = Common._M(346);
			particleType.AddSettingsKeyFrame(Common._M(39), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mVelocity = Common._M(419);
			particleType.AddSettingsKeyFrame(Common._M(89), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = 0f;
			particleSettings.mVelocity = Common._M(435);
			particleType.AddSettingsKeyFrame(Common._M(100), particleSettings);
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mSizeXVar = Common._M(20),
				mVelocityVar = Common._M(26),
				mWeightVar = Common._M(9)
			});
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = 0f;
			lifetimeSettings.mVelocityMult = Common._M(0.85f);
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(1.8f);
			lifetimeSettings.mVelocityMult = Common._M(1.2f);
			particleType.AddSettingAtLifePct(Common._M(0.3f), lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(0.3f);
			lifetimeSettings.mVelocityMult = Common._M(1.6f);
			particleType.AddSettingAtLifePct(Common._M(0.68f), lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mSizeXMult = 0f,
				mVelocityMult = 2f
			});
			emitter.AddParticleType(particleType);
			particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_GAS);
			particleType.mRefXOff = (int)(4f * (float)Common._M(-3));
			particleType.mRefYOff = (int)(4f * (float)Common._M(-12));
			particleType.mColorKeyManager.AddColorKey(0f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(0.125f, new Color(148, 0, 255));
			particleType.mColorKeyManager.AddColorKey(0.375f, new Color(0, 33, 255));
			particleType.mColorKeyManager.AddColorKey(0.5f, new Color(7, 222, 255));
			particleType.mColorKeyManager.AddColorKey(0.675f, new Color(0, 255, 42));
			particleType.mColorKeyManager.AddColorKey(0.75f, new Color(9, 156, 26));
			particleType.mColorKeyManager.AddColorKey(0.9f, new Color(255, 144, 0));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(255, 255, 255));
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mAlphaKeyManager.AddAlphaKey(Common._M(0.8f), 255);
			particleType.mAlphaKeyManager.AddAlphaKey(1f, 150);
			particleType.mColorKeyManager.SetColorMode(2);
			particleSettings = new ParticleSettings();
			particleSettings.mLife = Common._M(40);
			particleSettings.mNumber = (int)((float)Common._M(29) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mXSize = Common._M(60);
			particleSettings.mVelocity = Common._M(3);
			particleSettings.mWeight = (float)Common._M(0);
			particleSettings.mSpin = Common.DegreesToRadians((float)Common._M(6));
			particleSettings.mGlobalVisibility = 0f;
			particleType.AddSettingsKeyFrame(0, particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mNumber = (int)((float)Common._M(8) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mGlobalVisibility = Common._M(0.22f);
			particleType.AddSettingsKeyFrame(Common._M(12), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mNumber = 0;
			particleSettings.mLife = Common._M(53);
			particleSettings.mGlobalVisibility = Common._M(0.3f);
			particleType.AddSettingsKeyFrame(Common._M(16), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = 1f;
			particleSettings.mLife = Common._M(40);
			particleType.AddSettingsKeyFrame(Common._M(29), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mLife = Common._M(16);
			particleType.AddSettingsKeyFrame(Common._M(51), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = Common._M(0.2f);
			particleSettings.mLife = 0;
			particleType.AddSettingsKeyFrame(Common._M(70), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mGlobalVisibility = 0f;
			particleType.AddSettingsKeyFrame(Common._M(90), particleSettings);
			ParticleVariance particleVariance = new ParticleVariance();
			particleVariance.mLifeVar = Common._M(63);
			particleVariance.mSizeXVar = Common._M(7);
			particleVariance.mWeightVar = Common._M(0);
			particleVariance.mSpinVar = Common.DegreesToRadians((float)Common._M(30));
			particleVariance.mMotionRandVar = (float)Common._M(0);
			particleType.AddVarianceKeyFrame(0, particleVariance);
			particleVariance = new ParticleVariance(particleVariance);
			particleType.AddVarianceKeyFrame(Common._M(19), particleVariance);
			particleVariance = new ParticleVariance(particleVariance);
			particleVariance.mLifeVar = 0;
			particleType.AddVarianceKeyFrame(Common._M(35), particleVariance);
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = 1f;
			lifetimeSettings.mVelocityMult = 0f;
			particleType.AddSettingAtLifePct(0f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(1.8f);
			lifetimeSettings.mVelocityMult = Common._M(0.6f);
			particleType.AddSettingAtLifePct(0.1f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(1.9f);
			lifetimeSettings.mVelocityMult = Common._M(1.2f);
			particleType.AddSettingAtLifePct(0.21f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(2f);
			lifetimeSettings.mVelocityMult = Common._M(1.3f);
			particleType.AddSettingAtLifePct(0.28f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mVelocityMult = Common._M(1.4f);
			particleType.AddSettingAtLifePct(0.42f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(1f);
			lifetimeSettings.mVelocityMult = Common._M(1.8f);
			particleType.AddSettingAtLifePct(0.75f, lifetimeSettings);
			lifetimeSettings = new LifetimeSettings(lifetimeSettings);
			lifetimeSettings.mSizeXMult = Common._M(0.4f);
			lifetimeSettings.mVelocityMult = Common._M(1.9f);
			particleType.AddSettingAtLifePct(0.88f, lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings(lifetimeSettings)
			{
				mSizeXMult = Common._M(0.2f),
				mVelocityMult = Common._M(2f)
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
			system = this.mTriggeredEffect.mFlare;
			emitter = new Emitter();
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitter.mDeleteInvisParticles = true;
			emitterScale = new EmitterScale();
			emitterScale.mLifeScale = Common._M(1.5f);
			emitterScale.mNumberScale = Common._M(3.15f);
			emitterScale.mZoom = Common._M(3.12f);
			emitterScale.mSizeXScale = Common._M(2.03f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(2.37f);
			emitter.AddScaleKeyFrame(Common._M(9), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = Common._M(2.06f);
			emitter.AddScaleKeyFrame(Common._M(16), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mSizeXScale = (float)Common._M(0);
			emitter.AddScaleKeyFrame(Common._M(60), emitterScale);
			emitter.AddSettingsKeyFrame(0, new EmitterSettings
			{
				mVisibility = Common._M(0.5f)
			});
			emitter.AddSettingsKeyFrame(20, new EmitterSettings
			{
				mVisibility = Common._M(1f)
			});
			emitter.AddSettingsKeyFrame(35, new EmitterSettings
			{
				mVisibility = Common._M(0f)
			});
			particleType = new ParticleType();
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = 1f;
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_STARBURST);
			particleType.mColorKeyManager.AddColorKey(0f, new Color(10, 255, 88));
			particleType.mColorKeyManager.AddColorKey(0.25f, new Color(25, 0, 255));
			particleType.mColorKeyManager.AddColorKey(0.5f, new Color(255, 0, 161));
			particleType.mColorKeyManager.AddColorKey(0.75f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(254, 255, 0));
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(8),
				mNumber = (int)((float)Common._M(5) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE),
				mXSize = Common._M(28),
				mVelocity = Common._M(4),
				mGlobalVisibility = Common._M(0.76f)
			});
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mLifeVar = Common._M(14),
				mVelocityVar = Common._M(6)
			});
			particleType.AddSettingAtLifePct(0f, new LifetimeSettings
			{
				mSizeXMult = 0f
			});
			particleType.AddSettingAtLifePct(0.25f, new LifetimeSettings
			{
				mSizeXMult = Common._M(1.5f)
			});
			particleType.AddSettingAtLifePct(0.37f, new LifetimeSettings
			{
				mSizeXMult = Common._M(0.6f)
			});
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings
			{
				mSizeXMult = Common._M(0.1f)
			});
			emitter.AddParticleType(particleType);
			particleType = new ParticleType();
			particleType.mAdditive = true;
			particleType.mEmitterAttachPct = 1f;
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_BIG_STAR);
			particleType.mAlphaKeyManager.AddAlphaKey(0f, 255);
			particleType.mColorKeyManager.AddColorKey(0f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(0.4f, new Color(255, 246, 1));
			particleType.mColorKeyManager.AddColorKey(0.675f, new Color(242, 255, 0));
			particleType.mColorKeyManager.AddColorKey(0.8f, new Color(Color.White));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(Color.White));
			particleType.AddSettingsKeyFrame(0, new ParticleSettings
			{
				mLife = Common._M(8),
				mNumber = (int)((float)Common._M(2) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE),
				mXSize = Common._M(16),
				mGlobalVisibility = Common._M(0.41f)
			});
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mLifeVar = Common._M(14)
			});
			particleType.AddSettingAtLifePct(0f, new LifetimeSettings
			{
				mSizeXMult = 0f
			});
			particleType.AddSettingAtLifePct(0.25f, new LifetimeSettings
			{
				mSizeXMult = Common._M(1.5f)
			});
			particleType.AddSettingAtLifePct(0.37f, new LifetimeSettings
			{
				mSizeXMult = Common._M(0.6f)
			});
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings
			{
				mSizeXMult = Common._M(0.1f)
			});
			emitter.AddParticleType(particleType);
			system.AddEmitter(emitter);
			system = this.mTriggeredEffect.mTrail;
			system.SetLife(Common._M(200));
			emitter = new Emitter();
			emitter.mCullingRect = new Rect(0, 0, Common._SS(GlobalMembers.gSexyAppBase.mWidth), Common._SS(GlobalMembers.gSexyAppBase.mHeight));
			emitter.mDeleteInvisParticles = true;
			emitterScale = new EmitterScale();
			emitterScale.mLifeScale = Common._M(1f);
			emitterScale.mNumberScale = Common._M(1f);
			emitterScale.mSizeXScale = Common._M(0.59f);
			emitterScale.mVelocityScale = Common._M(1f);
			emitterScale.mWeightScale = Common._M(3f);
			emitterScale.mSpinScale = Common._M(0.54f);
			emitterScale.mZoom = Common._M(3.63f);
			emitter.AddScaleKeyFrame(0, emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitter.AddScaleKeyFrame(Common._M(36), emitterScale);
			emitterScale = new EmitterScale(emitterScale);
			emitterScale.mLifeScale = 0f;
			emitter.AddScaleKeyFrame(Common._M(51), emitterScale);
			emitterSettings = new EmitterSettings();
			emitterSettings.mVisibility = 1f;
			emitter.AddSettingsKeyFrame(0, emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitter.AddSettingsKeyFrame(Common._M(37), emitterSettings);
			emitterSettings = new EmitterSettings(emitterSettings);
			emitterSettings.mVisibility = 0f;
			emitter.AddSettingsKeyFrame(Common._M(96), emitterSettings);
			particleType = new ParticleType();
			particleType.mAdditive = true;
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_BIG_STAR);
			particleType.mColorKeyManager.AddColorKey(0f, new Color(0, 255, 4));
			particleType.mColorKeyManager.AddColorKey(0.25f, new Color(242, 255, 0));
			particleType.mColorKeyManager.AddColorKey(0.45f, new Color(255, 0, 0));
			particleType.mColorKeyManager.AddColorKey(0.65f, new Color(38, 0, 255));
			particleType.mColorKeyManager.AddColorKey(1f, new Color(113, 38, 255));
			particleType.mAlphaKeyManager.SetFixedColor(new Color(Color.White));
			particleType.mColorKeyManager.SetColorMode(2);
			particleSettings = new ParticleSettings();
			particleSettings.mLife = Common._M(29);
			particleSettings.mNumber = (int)((float)Common._M(83) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mXSize = Common._M(10);
			particleSettings.mVelocity = Common._M(3);
			particleSettings.mWeight = (float)Common._M(-8);
			particleSettings.mSpin = Common.DegreesToRadians((float)Common._M(3));
			particleType.AddSettingsKeyFrame(0, particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleType.AddSettingsKeyFrame(Common._M(34), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mVelocity = Common._M(72);
			particleType.AddSettingsKeyFrame(Common._M(39), particleSettings);
			particleType.AddVarianceKeyFrame(0, new ParticleVariance
			{
				mNumberVar = Common._M(28),
				mSizeXVar = Common._M(7),
				mVelocityVar = Common._M(24),
				mSpinVar = Common.DegreesToRadians((float)Common._M(210)),
				mMotionRandVar = (float)Common._M(44)
			});
			particleType.AddSettingAtLifePct(0f, new LifetimeSettings
			{
				mSizeXMult = Common._M(1.6f)
			});
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = Common._M(1.4f);
			particleType.AddSettingAtLifePct(Common._M(0.43f), lifetimeSettings);
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = Common._M(1.2f);
			particleType.AddSettingAtLifePct(Common._M(0.63f), lifetimeSettings);
			lifetimeSettings = new LifetimeSettings();
			lifetimeSettings.mSizeXMult = Common._M(0.7f);
			particleType.AddSettingAtLifePct(Common._M(0.8f), lifetimeSettings);
			particleType.AddSettingAtLifePct(1f, new LifetimeSettings
			{
				mSizeXMult = 0f
			});
			emitter.AddParticleType(particleType);
			particleType = new ParticleType();
			particleType.mImage = Res.GetImageByID(ResID.IMAGE_PARTICLE_ROUND);
			particleType.mColorKeyManager.SetFixedColor(new Color(21, 0, 211));
			particleType.mAlphaKeyManager.SetFixedColor(new Color(255, 255, 255, 211));
			particleType.mAdditive = true;
			particleType.mSingle = true;
			particleSettings = new ParticleSettings();
			particleSettings.mLife = Common._M(100);
			particleSettings.mNumber = (int)((float)Common._M(33) * MultiplierBallEffect.MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE);
			particleSettings.mXSize = Common._M(100);
			particleType.AddSettingsKeyFrame(0, particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleType.AddSettingsKeyFrame(Common._M(24), particleSettings);
			particleSettings = new ParticleSettings(particleSettings);
			particleSettings.mXSize = Common._M(266);
			particleType.AddSettingsKeyFrame(Common._M(45), particleSettings);
			emitter.AddParticleType(particleType);
			float num2 = -1f;
			float num3 = -1f;
			if (this.mMultBall.GetX() < (float)(Common._SS(GlobalMembers.gSexyAppBase.mWidth) / 2))
			{
				num2 = 1f;
			}
			if (this.mMultBall.GetY() < (float)(Common._SS(GlobalMembers.gSexyAppBase.mHeight) / 2))
			{
				num3 = 1f;
			}
			Vector2 vector;
			vector = new Vector2(num2 * (float)Common._M(150) + this.mMultBall.GetX(), num3 * (float)Common._M1(250) + this.mMultBall.GetY());
			emitter.mWaypointManager.AddPoint(0, new Vector2(this.mMultBall.GetX(), this.mMultBall.GetY()), false, vector);
			vector.X = (float)Common._M(649);
			vector.Y = (float)Common._M(169);
			Gun gun = GameApp.gApp.GetBoard().GetGun();
			emitter.mWaypointManager.AddPoint(Common._M(45), new Vector2((float)(Common._M1(0) + gun.GetCenterX()), (float)(Common._M2(-50) + gun.GetCenterY())), false, vector);
			bool flag = false;
			emitter.mWaypointManager.Init(flag);
			MultiplierBallEffect.gTrailHandle = system.AddEmitter(emitter);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0006F8B4 File Offset: 0x0006DAB4
		protected void UpdateStateTriggered()
		{
			this.mTriggerTimer++;
			this.mTriggeredEffect.mRings.SetPos(this.mLastBallX, this.mLastBallY);
			this.mTriggeredEffect.mRings.Update();
			if (this.mTriggerTimer >= Common._M(17))
			{
				this.mTriggeredEffect.mRainbow.SetPos(this.mLastBallX, this.mLastBallY);
				this.mTriggeredEffect.mRainbow.Update();
			}
			this.mTriggeredEffect.mGas.SetPos(this.mLastBallX, this.mLastBallY);
			this.mTriggeredEffect.mGas.Update();
			this.mTriggeredEffect.mFlare.SetPos(this.mLastBallX, this.mLastBallY);
			this.mTriggeredEffect.mFlare.Update();
			this.mTriggeredEffect.mTrail.SetPos(this.mLastBallX, this.mLastBallY);
			this.mTriggeredEffect.mTrail.Update();
			if (this.mDoMultFlash && this.mTriggeredEffect.mTrail.GetEmitter(MultiplierBallEffect.gTrailHandle).mWaypointManager.AtEnd())
			{
				this.mDoMultFlash = false;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0006F9EC File Offset: 0x0006DBEC
		protected void DrawStateTriggered(SexyGraphics g)
		{
			this.mTriggeredEffect.mRings.Draw(g);
			this.mTriggeredEffect.mRainbow.Draw(g);
			this.mTriggeredEffect.mGas.Draw(g);
			this.mTriggeredEffect.mFlare.Draw(g);
			this.mTriggeredEffect.mTrail.Draw(g);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0006FA50 File Offset: 0x0006DC50
		public MultiplierBallEffect(Ball mult_ball, bool spawn)
		{
			this.mMultBall = mult_ball;
			this.mSpawnTimer = 0;
			this.mTriggerTimer = 0;
			this.mSpawnEffect = null;
			this.mDoMultFlash = false;
			this.mTriggeredEffect = null;
			if (spawn)
			{
				this.InitSpawnEffects();
				return;
			}
			this.InitTriggeredEffects();
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0006FAB3 File Offset: 0x0006DCB3
		public virtual void Dispose()
		{
			if (this.mSpawnEffect != null)
			{
				this.mSpawnEffect.Dispose();
				this.mSpawnEffect = null;
			}
			if (this.mTriggeredEffect != null)
			{
				this.mTriggeredEffect.Dispose();
				this.mTriggeredEffect = null;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0006FAEC File Offset: 0x0006DCEC
		public void Update()
		{
			if (this.mMultBall != null)
			{
				this.mLastBallX = this.mMultBall.GetX();
				this.mLastBallY = this.mMultBall.GetY();
			}
			if (this.mSpawnEffect != null)
			{
				this.UpdateStateSpawn();
				if (this.mSpawnEffect.mRings.Done() && this.mSpawnEffect.mSwirl.Done())
				{
					this.mSpawnEffect.Dispose();
					this.mSpawnEffect = null;
				}
			}
			if (this.mTriggeredEffect != null)
			{
				this.UpdateStateTriggered();
				if (this.mTriggeredEffect.mRings.Done() && this.mTriggeredEffect.mRainbow.Done() && this.mTriggeredEffect.mGas.Done() && this.mTriggeredEffect.mTrail.Done())
				{
					this.mTriggeredEffect.Dispose();
					this.mTriggeredEffect = null;
				}
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0006FBCF File Offset: 0x0006DDCF
		public void Draw(SexyGraphics g)
		{
			if (this.mSpawnEffect != null)
			{
				this.DrawStateSpawn(g);
			}
			if (this.mTriggeredEffect != null)
			{
				this.DrawStateTriggered(g);
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0006FBEF File Offset: 0x0006DDEF
		public void BallDestroyed(Ball mult_ball)
		{
			this.mMultBall = mult_ball;
			this.mLastBallX = this.mMultBall.GetX();
			this.mLastBallY = this.mMultBall.GetY();
			this.InitTriggeredEffects();
			this.mMultBall = null;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0006FC27 File Offset: 0x0006DE27
		public Ball GetBall()
		{
			return this.mMultBall;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0006FC30 File Offset: 0x0006DE30
		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mDoMultFlash);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mSpawnTimer);
			sync.SyncLong(ref this.mTriggerTimer);
			sync.SyncFloat(ref this.mLastBallX);
			sync.SyncFloat(ref this.mLastBallY);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteLong((long)this.mBeamAlphas.Count);
				for (int i = 0; i < this.mBeamAlphas.Count; i++)
				{
					buffer.WriteBoolean(this.mBeamAlphas[i].second);
					AlphaFader first = this.mBeamAlphas[i].first;
					buffer.WriteFloat(first.mFadeRate);
					buffer.WriteLong((long)first.mFadeCount);
					buffer.WriteLong((long)first.mMin);
					buffer.WriteLong((long)first.mMax);
					buffer.WriteFloat(first.mColor.mRed);
					buffer.WriteFloat(first.mColor.mGreen);
					buffer.WriteFloat(first.mColor.mBlue);
					buffer.WriteFloat(first.mColor.mAlpha);
				}
				buffer.WriteBoolean(this.mTriggeredEffect != null);
				if (this.mTriggeredEffect != null)
				{
					Common.SerializeParticleSystem(this.mTriggeredEffect.mRings, sync);
					Common.SerializeParticleSystem(this.mTriggeredEffect.mRainbow, sync);
					Common.SerializeParticleSystem(this.mTriggeredEffect.mGas, sync);
					Common.SerializeParticleSystem(this.mTriggeredEffect.mFlare, sync);
					Common.SerializeParticleSystem(this.mTriggeredEffect.mTrail, sync);
				}
				buffer.WriteBoolean(this.mSpawnEffect != null);
				if (this.mSpawnEffect != null)
				{
					Common.SerializeParticleSystem(this.mSpawnEffect.mRings, sync);
					Common.SerializeParticleSystem(this.mSpawnEffect.mSwirl, sync);
				}
				buffer.WriteBoolean(this.mMultBall != null);
				if (this.mMultBall != null)
				{
					buffer.WriteLong((long)this.mMultBall.GetId());
					return;
				}
			}
			else
			{
				int num = (int)buffer.ReadLong();
				this.mBeamAlphas.Clear();
				for (int j = 0; j < num; j++)
				{
					AlphaFadeInfo alphaFadeInfo = new AlphaFadeInfo(new AlphaFader(), false);
					alphaFadeInfo.second = buffer.ReadBoolean();
					alphaFadeInfo.first.mFadeRate = buffer.ReadFloat();
					alphaFadeInfo.first.mFadeCount = (int)buffer.ReadLong();
					alphaFadeInfo.first.mMin = (int)buffer.ReadLong();
					alphaFadeInfo.first.mMax = (int)buffer.ReadLong();
					alphaFadeInfo.first.mColor.mRed = buffer.ReadFloat();
					alphaFadeInfo.first.mColor.mGreen = buffer.ReadFloat();
					alphaFadeInfo.first.mColor.mBlue = buffer.ReadFloat();
					alphaFadeInfo.first.mColor.mAlpha = buffer.ReadFloat();
					this.mBeamAlphas.Add(alphaFadeInfo);
				}
				this.mTriggeredEffect = null;
				this.mSpawnEffect = null;
				if (buffer.ReadBoolean())
				{
					this.mTriggeredEffect = new TriggeredEffect(false);
					this.mTriggeredEffect.mRings = Common.DeserializeParticleSystem(sync);
					this.mTriggeredEffect.mRainbow = Common.DeserializeParticleSystem(sync);
					this.mTriggeredEffect.mGas = Common.DeserializeParticleSystem(sync);
					this.mTriggeredEffect.mFlare = Common.DeserializeParticleSystem(sync);
					this.mTriggeredEffect.mTrail = Common.DeserializeParticleSystem(sync);
				}
				if (buffer.ReadBoolean())
				{
					this.mSpawnEffect = new SpawnEffect(false);
					this.mSpawnEffect.mRings = Common.DeserializeParticleSystem(sync);
					this.mSpawnEffect.mSwirl = Common.DeserializeParticleSystem(sync);
				}
				if (buffer.ReadBoolean())
				{
					int id = (int)buffer.ReadLong();
					this.mMultBall = ((GameApp)GlobalMembers.gSexyApp).GetBoard().mLevel.GetBallById(id);
				}
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00070016 File Offset: 0x0006E216
		public bool Done()
		{
			return this.mSpawnEffect == null && this.mTriggeredEffect == null;
		}

		// Token: 0x0400139C RID: 5020
		public static SpawnColors[] gSpawnColors = new SpawnColors[]
		{
			new SpawnColors(new Color(1, 108, 222), new Color(12, 0, 255), new Color(0, 195, 255)),
			new SpawnColors(new Color(246, 236, 4), new Color(206, 151, 15), new Color(254, 255, 0)),
			new SpawnColors(new Color(222, 0, 0), new Color(250, 14, 124), new Color(255, 131, 0)),
			new SpawnColors(new Color(0, 236, 51), new Color(25, 125, 115), new Color(135, 235, 15)),
			new SpawnColors(new Color(155, 17, 236), new Color(11, 10, 255), new Color(216, 21, 255)),
			new SpawnColors(new Color(250, 240, 238), new Color(Color.White), new Color(Color.White))
		};

		// Token: 0x0400139D RID: 5021
		public static int[] gMaxParticles = new int[7];

		// Token: 0x0400139E RID: 5022
		public static int gTrailHandle = 0;

		// Token: 0x0400139F RID: 5023
		public static float MULTIPLIER_BALL_EFFECT_PARTICLE_COUNT_SCALE = 0.5f;

		// Token: 0x040013A0 RID: 5024
		protected Ball mMultBall;

		// Token: 0x040013A1 RID: 5025
		protected SpawnEffect mSpawnEffect;

		// Token: 0x040013A2 RID: 5026
		protected TriggeredEffect mTriggeredEffect;

		// Token: 0x040013A3 RID: 5027
		protected List<AlphaFadeInfo> mBeamAlphas = new List<AlphaFadeInfo>();

		// Token: 0x040013A4 RID: 5028
		protected float mLastBallX;

		// Token: 0x040013A5 RID: 5029
		protected float mLastBallY;

		// Token: 0x040013A6 RID: 5030
		protected int mSpawnTimer;

		// Token: 0x040013A7 RID: 5031
		protected int mTriggerTimer;

		// Token: 0x040013A8 RID: 5032
		protected int mState;

		// Token: 0x040013A9 RID: 5033
		protected bool mDoMultFlash;

		// Token: 0x040013AA RID: 5034
		protected Transform mGlobalTranform = new Transform();
	}
}
