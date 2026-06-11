using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Drivers;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000018 RID: 24
	public class LoadingScreen : Widget, ButtonListener
	{
		// Token: 0x060003BC RID: 956 RVA: 0x000311D4 File Offset: 0x0002F3D4
		protected void DrawLightning(SexyGraphics g, int x, int cloud_num)
		{
			LoadingCloud loadingCloud = this.mClouds[cloud_num];
			if (loadingCloud.mLightning == null)
			{
				return;
			}
			int num = 2;
			int num2 = 2;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LS_CLOUD1A + cloud_num * 3);
			float num3 = loadingCloud.mLightningScale * (float)(loadingCloud.mLightning.mWidth * num2);
			float num4 = loadingCloud.mLightningScale * (float)(loadingCloud.mLightning.mHeight * num2);
			g.PushState();
			int num5 = (int)(255f * ((float)loadingCloud.mLightningTimer / (float)loadingCloud.mTimerTarget));
			g.SetColor(255, 255, 255, num5);
			g.SetColorizeImages(true);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LS_CLOUD1B + cloud_num * 3);
			int num6 = 10;
			g.DrawImage(imageByID2, x, Common._DS((int)loadingCloud.mY + Common._M(0)), imageByID2.GetWidth() * num6, imageByID2.GetHeight() * num6);
			g.DrawImage(loadingCloud.mLightning, x + (imageByID.mWidth * num - (int)num3) / 2, (int)Common._DS(loadingCloud.mY) + imageByID.mHeight * num / 3, (int)num3, (int)num4);
			g.PopState();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000312F4 File Offset: 0x0002F4F4
		public override void DrawOverlay(SexyGraphics g)
		{
			if (this.mBlackFadeAlpha > 0f)
			{
				g.PushState();
				g.SetColor(0, 0, 0, (int)this.mBlackFadeAlpha);
				g.FillRect(GlobalMembers.gSexyApp.mScreenBounds);
				g.PopState();
			}
			g.PushState();
			if (this.mFadeToMainMenu && !this.mBlackFadeIn)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)this.mBlackFadeAlpha);
			}
			g.PopState();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00031379 File Offset: 0x0002F579
		public bool CanLoad()
		{
			return this.mState == 2 && !this.mWaitingForConfirmation;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0003138F File Offset: 0x0002F58F
		public bool Done()
		{
			return this.mFadeToMainMenu && this.mBlackFadeAlpha <= 0f;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000313AB File Offset: 0x0002F5AB
		public bool CanShowMenu()
		{
			if (this.mFadeToMainMenu && this.mCanShowMenu && (!this.mBlackFadeIn || GameApp.gApp.mMinimized) && this.mUserProfileLoaded)
			{
				this.mCanShowMenu = false;
				return true;
			}
			return false;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000313E4 File Offset: 0x0002F5E4
		public void LoadingComplete()
		{
			this.mPantalookRipplePct.SetCurve(Common._MP("b;0,1,0.003333,1,~###         ~####"));
			this.mPantaloonFlopPct.SetCurve(Common._MP("b;0,1,0.002857,1,####    b####     ?~d,o"));
			this.mLoadingComplete = true;
			for (int i = 0; i < LoadingScreen.MAX_VOLCANO_PROJECTILES; i++)
			{
				this.mVolcanoProjectiles[i].mProjectile = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_PARTICLES_VOLCANO_PROJECTILE").Duplicate();
				this.mVolcanoProjectiles[i].mProjectile.mEmitAfterTimeline = true;
				this.mVolcanoProjectiles[i].mProjectile.mOptimizeValue = 2;
				this.mVolcanoProjectiles[i].mProjectile.mInUse = false;
				this.mVolcanoProjectiles[i].mProjectile.mDrawTransform.LoadIdentity();
				this.mVolcanoProjectiles[i].mProjectile.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
				this.mVolcanoProjectiles[i].mProjectile.mDrawTransform.Translate((float)(Common._DS(Common._M(790)) + this.mOffsetParticle), (float)Common._DS(Common._M1(150)));
				Common.SetFXNumScale(this.mVolcanoProjectiles[i].mProjectile, 3f);
				this.mEffectBatch.AddEffect(this.mVolcanoProjectiles[i].mProjectile);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00031548 File Offset: 0x0002F748
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mCompleteLoadingBarAlpha >= 255f && !this.mFadeToMainMenu)
			{
				this.mUserProfileLoaded = true;
				if (GameApp.gApp.mUserProfile == null && this.mLoadingComplete && !this.mFadeToMainMenu)
				{
					GameApp.gApp.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(GameApp.gApp.m_DefaultProfileName);
					if (GameApp.gApp.mUserProfile != null)
					{
						this.mBlackFadeAlpha = 0.0001f;
					}
				}
				if (GameApp.USE_XBOX_SERVICE && !GameApp.USE_TRIAL_VERSION)
				{
					GameApp.gApp.mUserProfile.m_AchievementMgr.SyncAchievementsXLive();
				}
				this.mFadeToMainMenu = true;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000315FB File Offset: 0x0002F7FB
		public override void GotFocus()
		{
			base.GotFocus();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00031604 File Offset: 0x0002F804
		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			this.pts[0] = new Point((this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2);
			this.pts[1] = new Point((this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2 - 77);
			this.pts[2] = new Point((this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2 - 77);
			this.pts[3] = new Point((this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2 - 77);
			this.pts[4] = new Point((this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2 - 135, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2 - 77);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00031737 File Offset: 0x0002F937
		public override void GamepadButtonDown(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			uint num = theFlags & 1U;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0003173D File Offset: 0x0002F93D
		private float GetMainMenuAlpha()
		{
			return this.mBlackFadeAlpha;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00031748 File Offset: 0x0002F948
		public LoadingScreen()
		{
			this.mDarkIslandAlpha = 255;
			this.mLogoHoldTime = 200;
			this.Init();
			this.mWaitingForConfirmation = false;
			if (GameApp.gApp.mFromReInit)
			{
				this.mState = 2;
				this.mFlashAlpha = 0f;
				SoundAttribs soundAttribs = new SoundAttribs();
				soundAttribs.fadein = 0.01f;
				soundAttribs.fadeout = 0.005f;
				GameApp.gApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_LS_STORM_LOOP), soundAttribs);
			}
			this.mClip = false;
			this.mFirstRun = false;
			this.mLoadingCompleteTime = 0;
			if (this.mFirstRun)
			{
				this.mLoadingTextIdx = 0;
			}
			else
			{
				this.mLoadingTextIdx = Common.Rand() % Enumerable.Count<string>(this.mLoadingTextContainer.GetLoadingText());
			}
			this.mSeenLoadingTextIndices.Add(this.mLoadingTextIdx);
			this.mLoadingTextTime = LoadingScreen.LOADING_TEXT_TIME;
			this.mCloudUpdateCount = 0;
			this.mRippleCnt = 0f;
			this.mPantalookRipplePct.SetConstant(1.0);
			this.mUserProfileLoaded = true;
			this.mLoading = false;
			this.mLeftTorch = null;
			this.mRightTorch = null;
			this.mVolcanoSmoke = null;
			this.mLoadingYOffset = Common._DS(Common._M(998));
			this.mLoadingTextFrame.mWidth = this.IMAGE_LS_REDLOADINGBAR.GetWidth();
			this.mLoadingTextFrame.mHeight = this.IMAGE_LS_REDLOADINGBAR.GetHeight();
			this.mLoadingTextFrame.mX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_REDLOADINGBAR) - this.mLoadingXOffset);
			this.mLoadingTextFrame.mY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_REDLOADINGBAR)) + this.mLoadingYOffset;
			for (int i = 0; i < LoadingScreen.MAX_VOLCANO_PROJECTILES; i++)
			{
				this.mVolcanoProjectiles[i] = new VolcanoProjectile();
				this.mVolcanoProjectiles[i].mProjectile = null;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00031B3C File Offset: 0x0002FD3C
		private void Init()
		{
			int seed = (int)Common.SexyTime();
			LoadingScreen.RandomNumbers.Seed(seed);
			MathUtils.Seed(seed);
			SexyApp gSexyApp = GlobalMembers.gSexyApp;
			this.mLoadingXOffset = 0;
			this.mLavaAlpha = 0f;
			this.mIncLavaAlpha = true;
			this.mLoadingCompleteDelay = 0;
			this.mState = 0;
			this.mHasShown = false;
			this.mLightningOn = true;
			this.mLightningTimer = 0;
			this.mLightningFrame = 0;
			this.mCanShowMenu = true;
			this.mLoadingBarAlpha = 0f;
			this.mCompleteLoadingBarAlpha = 0f;
			this.mBlackFadeAlpha = 255f;
			this.mLoadingComplete = false;
			this.mBlackFadeIn = true;
			this.mStormTimer = (this.mClearTimer = Common._M(100));
			this.mFadeToMainMenu = false;
			this.mWaves[0] = new LoadingWave();
			this.mWaves[1] = new LoadingWave();
			this.mWaves[2] = new LoadingWave();
			this.mWaves[3] = new LoadingWave();
			this.mWaves[4] = new LoadingWave();
			this.mWaves[0].mRadius = Common._M(20f);
			this.mWaves[0].mAngle = Common._M1(1f);
			this.mWaves[0].mAngleRate = Common._M2(-0.035f);
			this.mWaves[0].mY = (float)Common._M3(860);
			this.mWaves[1].mRadius = Common._M(16f);
			this.mWaves[1].mAngle = Common._M1(-1f);
			this.mWaves[1].mAngleRate = Common._M2(-0.025f);
			this.mWaves[1].mY = (float)Common._M3(700);
			this.mWaves[2].mRadius = Common._M(12f);
			this.mWaves[2].mAngle = Common._M1(2f);
			this.mWaves[2].mAngleRate = Common._M2(-0.015f);
			this.mWaves[2].mY = (float)Common._M3(620);
			this.mWaves[3].mRadius = Common._M(10f);
			this.mWaves[3].mAngle = Common._M1(-2f);
			this.mWaves[3].mAngleRate = Common._M2(-0.01f);
			this.mWaves[3].mY = (float)Common._M3(520);
			this.mWaves[4].mRadius = Common._M(6f);
			this.mWaves[4].mAngle = Common._M1(3f);
			this.mWaves[4].mAngleRate = Common._M2(-0.005f);
			this.mWaves[4].mY = (float)Common._M3(460);
			this.mCalmWaves[0] = new LoadingWave();
			this.mCalmWaves[1] = new LoadingWave();
			this.mCalmWaves[2] = new LoadingWave();
			this.mCalmWaves[3] = new LoadingWave();
			this.mCalmWaves[0].mRadius = Common._M(2f);
			this.mCalmWaves[0].mAngle = Common._M1(1f);
			this.mCalmWaves[0].mAngleRate = Common._M2(-0.035f) / 2f;
			this.mCalmWaves[0].mY = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_CALM_WAVE_1));
			this.mCalmWaves[1].mRadius = Common._M(8f);
			this.mCalmWaves[1].mAngle = Common._M1(-1f);
			this.mCalmWaves[1].mAngleRate = Common._M2(-0.025f) / 2f;
			this.mCalmWaves[1].mY = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_CALM_WAVE_2));
			this.mCalmWaves[2].mRadius = Common._M(6f);
			this.mCalmWaves[2].mAngle = Common._M1(2f);
			this.mCalmWaves[2].mAngleRate = Common._M2(-0.015f) / 2f;
			this.mCalmWaves[2].mY = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_CALM_WAVE_3));
			this.mCalmWaves[3].mRadius = Common._M(5f);
			this.mCalmWaves[3].mAngle = Common._M1(-2f);
			this.mCalmWaves[3].mAngleRate = Common._M2(-0.01f) / 2f;
			this.mCalmWaves[3].mY = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_CALM_WAVE_4));
			for (int i = 0; i < 4; i++)
			{
				this.mCalmWaves[i].mVX = 0.5f / (float)(i + 1);
				this.mCalmWaves[i].mXOff = 0f;
				this.mCalmWaves[i].mMaxXOff = Common._DS(20f) / (float)(i + 1);
				this.mCalmWaves[i].mY = (float)(gSexyApp.mHeight - Common._DS(452)) + this.mCalmWaves[i].mY;
				if (i % 2 == 0)
				{
					this.mCalmWaves[i].mIncVX = true;
				}
			}
			this.mFrogAngleDivisor = 1f;
			this.mFlashAlpha = 0f;
			this.mExtraProgress = 0f;
			this.mFrogAngle = 0f;
			this.mFrogPitchForward = true;
			this.mFrogAngleDelta = Common._M(0.004f);
			this.mClouds[0] = new LoadingCloud();
			this.mClouds[1] = new LoadingCloud();
			this.mClouds[2] = new LoadingCloud();
			this.mClouds[0].mStartX = (float)Common._DS(Common._M(-400));
			this.mClouds[0].mY = (float)Common._M1(-400);
			this.mClouds[0].mShadowOffset = (float)(Common._DS(Common._M2(-30)) - Common._DS(160));
			this.mClouds[0].mShadowY = (float)Common._DS(Common._M3(450));
			this.mClouds[1].mStartX = (float)Common._DS(Common._M(0));
			this.mClouds[1].mY = (float)Common._M1(0);
			this.mClouds[1].mShadowOffset = (float)(Common._DS(Common._M2(-19)) - Common._DS(160));
			this.mClouds[1].mShadowY = (float)Common._DS(Common._M3(550));
			this.mClouds[2].mStartX = (float)Common._DS(Common._M(-320));
			this.mClouds[2].mY = (float)Common._M1(25);
			this.mClouds[2].mShadowOffset = (float)(Common._DS(Common._M2(-70)) - Common._DS(160));
			this.mClouds[2].mShadowY = (float)Common._DS(Common._M3(650));
			this.mFrogWave = 0;
			this.mFrogPct = 0f;
			this.mFrogScale = 1f;
			this.mEffectBatch = new PIEffectBatch();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00032260 File Offset: 0x00030460
		public override void Update()
		{
			if (GameApp.gApp.IsHardwareBackButtonPressed())
			{
				this.ProcessHardwareBackButton();
			}
			base.Update();
			if (!this.mLoadingComplete && GameApp.gApp.GetLoadingThreadProgress() >= 1.0)
			{
				GameApp.gApp.LoadingThreadCompleted();
			}
			if (GameApp.gApp.StartLoadingComplete)
			{
				GameApp.gApp.LoadLevelXML();
				GameApp.gApp.StartLoadingComplete = false;
			}
			if (this.mBlackFadeIn && this.mState == 0)
			{
				this.mBlackFadeAlpha -= 2f;
				if (this.mBlackFadeAlpha <= 0f)
				{
					this.mBlackFadeAlpha = 0f;
				}
			}
			if (this.mLockBGM)
			{
				return;
			}
			if (this.mFlashAlpha > 0f)
			{
				this.mFlashAlpha -= ((this.mState == 2) ? Common._M(1.5f) : Common._M1(10f));
				if (this.mFlashAlpha < 0f)
				{
					this.mFlashAlpha = 0f;
				}
			}
			if (!this.mLoading && GameApp.gApp.mLoadLevelSuccess)
			{
				GameApp.gApp.LoadingThreadProc();
				this.mLoading = true;
			}
			if (!this.mLoadingComplete)
			{
				if (--this.mLoadingTextTime == 0)
				{
					if (this.mSeenLoadingTextIndices.Capacity == Enumerable.Count<string>(this.mLoadingTextContainer.GetLoadingText()))
					{
						this.mSeenLoadingTextIndices.Clear();
					}
					List<int> list = new List<int>();
					for (int i = 0; i < Enumerable.Count<string>(this.mLoadingTextContainer.GetLoadingText()); i++)
					{
						bool flag = false;
						for (int j = 0; j < Enumerable.Count<int>(this.mSeenLoadingTextIndices); j++)
						{
							if (this.mSeenLoadingTextIndices[j] == i)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(i);
						}
					}
					if (list.Count == 0)
					{
						this.mLoadingTextIdx = 0;
						this.mSeenLoadingTextIndices.Clear();
					}
					else if (this.mFirstRun)
					{
						this.mLoadingTextIdx = (this.mLoadingTextIdx + 1) % Enumerable.Count<string>(this.mLoadingTextContainer.GetBackStoryText());
					}
					else
					{
						int num = LoadingScreen.RandomNumbers.NextNumber() % list.Count;
						this.mLoadingTextIdx = list[num];
					}
					this.mSeenLoadingTextIndices.Add(this.mLoadingTextIdx);
					this.mLoadingTextTime = LoadingScreen.LOADING_TEXT_TIME;
				}
			}
			else
			{
				this.mLoadingCompleteTime++;
				if (this.mLoadingCompleteTime == 200)
				{
					GameApp.gApp.PlaySong(0);
				}
			}
			if (this.mFrogPitchForward)
			{
				this.mFrogAngle -= this.mFrogAngleDelta / this.mFrogAngleDivisor;
				if (this.mFrogAngle <= -LoadingScreen.max_angle)
				{
					this.mFrogAngle = -LoadingScreen.max_angle;
					this.mFrogAngleDelta *= -1f;
					this.mFrogPitchForward = false;
				}
			}
			else
			{
				this.mFrogAngle -= this.mFrogAngleDelta / this.mFrogAngleDivisor;
				if (this.mFrogAngle >= LoadingScreen.max_angle)
				{
					this.mFrogAngle = LoadingScreen.max_angle;
					this.mFrogAngleDelta = MathUtils.FloatRange(Common._M(0.0015f), Common._M1(0.003f));
					if (this.mLoadingComplete && MathUtils._geq(this.mExtraProgress, 1f, 0.01f))
					{
						LoadingScreen.max_angle = 0.04313f;
					}
					this.mFrogPitchForward = true;
				}
			}
			if (this.mLoadingComplete && this.mState == 2)
			{
				if (this.mStormTimer > 0)
				{
					if (--this.mStormTimer == 0)
					{
						GameApp.gApp.mSoundPlayer.Fade(Res.GetSoundByID(ResID.SOUND_LS_STORM_LOOP));
					}
				}
				else if (++this.mLoadingCompleteDelay >= Common._M(5))
				{
					this.mExtraProgress += Common._M(0.003f);
					if (this.mExtraProgress > 1f)
					{
						this.mExtraProgress = 1f;
						if (--this.mClearTimer <= 0 && !GameApp.gApp.mFromReInit)
						{
							this.mState++;
							SoundAttribs soundAttribs = new SoundAttribs();
							soundAttribs.fadeout = 0.1f;
							GameApp.gApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_SEAGULLS), soundAttribs);
						}
					}
				}
			}
			if (MathUtils._geq(this.mExtraProgress, 0.5f) && this.mDarkIslandAlpha > 0)
			{
				this.mDarkIslandAlpha -= Common._M(2);
			}
			if (this.mState >= 2 && !this.mFadeToMainMenu)
			{
				if (this.mLoadingBarAlpha < 255f)
				{
					this.mLoadingBarAlpha += Common._M(2f);
				}
				if (this.mLoadingBarAlpha > 255f)
				{
					this.mLoadingBarAlpha = 255f;
				}
				if (this.mFrogPct < Common._M(0.8f))
				{
					this.mFrogPct += Common._M(0.0001f) / (float)(this.mFrogWave + 1);
				}
			}
			if (this.mState == 0)
			{
				if (this.mHasShown)
				{
					if (this.mPartnerLogos.Capacity > 0)
					{
						PartnerLogo partnerLogo = this.mPartnerLogos[0];
						if (partnerLogo.mAlpha < 255 && partnerLogo.mTime == partnerLogo.mOrgTime)
						{
							partnerLogo.mAlpha += Common._M(5);
							if (partnerLogo.mAlpha >= 255)
							{
								partnerLogo.mAlpha = 255;
							}
						}
						else if (--partnerLogo.mTime <= 0)
						{
							partnerLogo.mAlpha -= Common._M(5);
							if (partnerLogo.mAlpha <= 0)
							{
								this.mPartnerLogos.RemoveAt(0);
							}
						}
					}
					else if (--this.mLogoHoldTime == 0)
					{
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_LS_THUNDERSTRIKE));
						this.mState++;
						GameApp.gApp.InitMetricsManager();
					}
				}
			}
			else if (this.mState == 1)
			{
				int[] array = new int[]
				{
					Common._M(5),
					Common._M1(10),
					Common._M2(10),
					Common._M3(15),
					Common._M4(10)
				};
				int[] array2 = new int[]
				{
					Common._M(5),
					Common._M1(5),
					Common._M2(15),
					Common._M3(10),
					Common._M4(10)
				};
				int num2 = this.mLightningOn ? array[this.mLightningFrame] : array2[this.mLightningFrame];
				if (++this.mLightningTimer == num2)
				{
					this.mLightningTimer = 0;
					this.mLightningOn = !this.mLightningOn;
					if (this.mLightningOn && this.mFlashAlpha <= 0f)
					{
						this.mFlashAlpha = 255f;
					}
					if (this.mLightningOn && ++this.mLightningFrame == 5)
					{
						this.mFlashAlpha = 255f;
						this.mState++;
					}
				}
				if (Enumerable.Count<LogoLightning>(this.mLogoLightning) < Common._M(3) && MathUtils.SafeRand() % Common._M1(20) == 0)
				{
					this.mLogoLightning.Add(new LogoLightning());
					LogoLightning logoLightning = this.mLogoLightning[Enumerable.Count<LogoLightning>(this.mLogoLightning) - 1];
					logoLightning.mImage = Res.GetImageByID(ResID.IMAGE_LS_LIGHT1 + MathUtils.SafeRand() % 2);
					logoLightning.mTimer = (logoLightning.mTimerTarget = MathUtils.IntRange(Common._M(5), Common._M1(25)));
				}
				for (int k = 0; k < Enumerable.Count<LogoLightning>(this.mLogoLightning); k++)
				{
					LogoLightning logoLightning2 = this.mLogoLightning[k];
					if (--logoLightning2.mTimer == 0)
					{
						this.mLogoLightning.RemoveAt(k);
						k--;
					}
				}
			}
			if (this.mLoadingComplete && !GameApp.gApp.mFromReInit && this.mCompleteLoadingBarAlpha < 255f && this.mLoadingCompleteTime >= 400)
			{
				this.mCompleteLoadingBarAlpha += Common._M(3f);
				if (this.mCompleteLoadingBarAlpha > 255f)
				{
					this.mCompleteLoadingBarAlpha = 255f;
				}
			}
			if (this.mFadeToMainMenu)
			{
				if (this.mBlackFadeIn)
				{
					this.mBlackFadeAlpha += Common._M(5f);
					if (this.mBlackFadeAlpha >= 255f)
					{
						this.mBlackFadeAlpha = 255f;
						this.mBlackFadeIn = false;
					}
				}
				else
				{
					this.mBlackFadeAlpha -= Common._M(2f);
					if (this.mBlackFadeAlpha <= 0f)
					{
						this.mBlackFadeAlpha = 0f;
					}
				}
			}
			if (this.mLoadingComplete && this.mExtraProgress < 0.99f)
			{
				this.mFrogAngleDivisor += Common._M(0.018f);
			}
			for (int l = 0; l < 5; l++)
			{
				LoadingWave loadingWave = this.mWaves[l];
				loadingWave.mAngle += loadingWave.mAngleRate;
				if (l < 4)
				{
					float num3 = Common._M(0.0005f) / (float)(l + 1);
					this.mCalmWaves[l].mAngle += this.mCalmWaves[l].mAngleRate;
					if (!this.mCalmWaves[l].mIncVX)
					{
						this.mCalmWaves[l].mVX -= num3;
					}
					else
					{
						this.mCalmWaves[l].mVX += num3;
					}
					float num4 = Common._M(0.2f);
					if (this.mCalmWaves[l].mVX > num4)
					{
						this.mCalmWaves[l].mVX = num4;
					}
					else if (this.mCalmWaves[l].mVX < -num4)
					{
						this.mCalmWaves[l].mVX = -num4;
					}
					this.mCalmWaves[l].mXOff += this.mCalmWaves[l].mVX;
					if (this.mCalmWaves[l].mXOff >= this.mCalmWaves[l].mMaxXOff)
					{
						if (this.mCalmWaves[l].mVX > 0f)
						{
							this.mCalmWaves[l].mVX /= Common._M(4f);
						}
						this.mCalmWaves[l].mIncVX = false;
					}
					else if (this.mCalmWaves[l].mXOff <= -this.mCalmWaves[l].mMaxXOff)
					{
						if (this.mCalmWaves[l].mVX < 0f)
						{
							this.mCalmWaves[l].mVX /= Common._M(4f);
						}
						this.mCalmWaves[l].mIncVX = true;
					}
				}
			}
			for (int m = 0; m < 3; m++)
			{
				LoadingCloud loadingCloud = this.mClouds[m];
				if (loadingCloud.mLightning == null && this.mExtraProgress == 0f && MathUtils.SafeRand() % Common._M(200) == 0)
				{
					loadingCloud.mLightning = Res.GetImageByID(ResID.IMAGE_LS_LIGHT1 + MathUtils.SafeRand() % 1);
					loadingCloud.mLightningTimer = (loadingCloud.mTimerTarget = MathUtils.IntRange(Common._M(10), Common._M1(25)));
					loadingCloud.mLightningScale = Common._M(0.75f) - (float)m * Common._M1(0.15f);
				}
				if (loadingCloud.mLightningTimer > 0 && --loadingCloud.mLightningTimer <= 0)
				{
					loadingCloud.mLightning = null;
				}
			}
			if (this.mLoadingComplete && this.mExtraProgress > 0f)
			{
				if (this.mIncLavaAlpha)
				{
					this.mLavaAlpha += Common._M(0.5f);
					if (this.mLavaAlpha >= 255f)
					{
						this.mLavaAlpha = 255f;
						this.mIncLavaAlpha = false;
					}
				}
				else
				{
					this.mLavaAlpha -= Common._M(0.5f);
					if (this.mLavaAlpha <= 0f)
					{
						this.mLavaAlpha = 0f;
						this.mIncLavaAlpha = true;
					}
				}
			}
			if (this.mLoadingComplete)
			{
				if (this.mLeftTorch == null)
				{
					this.mLeftTorch = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_PARTICLES_LS_TIKITORCH_FLAME").Duplicate();
					this.mLeftTorch.mEmitAfterTimeline = true;
					this.mLeftTorch.mDrawTransform.LoadIdentity();
					this.mLeftTorch.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
					this.mLeftTorch.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(264)) + this.mOffsetParticle), (float)(Common._S(this.mY) + Common._DS(Common._M1(430))));
					Common.SetFXNumScale(this.mLeftTorch, 4f);
					this.mEffectBatch.AddEffect(this.mLeftTorch);
				}
				if (this.mRightTorch == null)
				{
					this.mRightTorch = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_PARTICLES_LS_TIKITORCH_FLAME").Duplicate();
					this.mRightTorch.mEmitAfterTimeline = true;
					this.mRightTorch.mDrawTransform.LoadIdentity();
					this.mRightTorch.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
					this.mRightTorch.mDrawTransform.RotateDeg((float)Common._M(-20));
					this.mRightTorch.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(1357)) + this.mOffsetParticle), (float)(Common._S(this.mY) + Common._DS(Common._M1(430))));
					Common.SetFXNumScale(this.mRightTorch, 4f);
					this.mEffectBatch.AddEffect(this.mRightTorch);
				}
				if (this.mVolcanoSmoke == null)
				{
					this.mVolcanoSmoke = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_PARTICLES_VOLCANO_SMOKE").Duplicate();
					this.mVolcanoSmoke.mEmitAfterTimeline = true;
					this.mVolcanoSmoke.mDrawTransform.LoadIdentity();
					this.mVolcanoSmoke.mDrawTransform.Scale(Common._DS(1.4f), Common._DS(1.4f));
					this.mVolcanoSmoke.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(790)) + this.mOffsetParticle), (float)(Common._S(this.mY) + Common._DS(Common._M1(90))));
					Common.SetFXNumScale(this.mVolcanoSmoke, 3f);
					this.mEffectBatch.AddEffect(this.mVolcanoSmoke);
				}
				if (this.mLeftTorch != null)
				{
					this.mLeftTorch.Update();
				}
				if (this.mRightTorch != null)
				{
					this.mRightTorch.Update();
				}
				if (this.mVolcanoSmoke != null)
				{
					this.mVolcanoSmoke.Update();
				}
			}
			if (Common.Rand(Common._M(100)) == 0)
			{
				for (int n = 0; n < LoadingScreen.MAX_VOLCANO_PROJECTILES; n++)
				{
					VolcanoProjectile volcanoProjectile = this.mVolcanoProjectiles[n];
					if (volcanoProjectile.mProjectile != null && !volcanoProjectile.mProjectile.mInUse)
					{
						volcanoProjectile.mProjectile.mInUse = true;
						volcanoProjectile.mProjectile.ResetAnim();
						volcanoProjectile.mProjectile.mRandSeeds.Clear();
						volcanoProjectile.mProjectile.mRandSeeds.Add(Common.Rand(1000));
						break;
					}
				}
			}
			for (int num5 = 0; num5 < LoadingScreen.MAX_VOLCANO_PROJECTILES; num5++)
			{
				VolcanoProjectile volcanoProjectile2 = this.mVolcanoProjectiles[num5];
				if (volcanoProjectile2.mProjectile != null && volcanoProjectile2.mProjectile.mInUse && volcanoProjectile2.mProjectile != null)
				{
					volcanoProjectile2.mProjectile.Update();
					if (volcanoProjectile2.mProjectile.mCurNumParticles == 0 && MathUtils._geq(volcanoProjectile2.mProjectile.mFrameNum, (float)volcanoProjectile2.mProjectile.mLastFrameNum))
					{
						volcanoProjectile2.mProjectile.mInUse = false;
					}
				}
			}
			if (this.mLoadingComplete)
			{
				for (int num6 = 0; num6 < 3; num6++)
				{
					LoadingCloud loadingCloud2 = this.mClouds[num6];
					loadingCloud2.mVX += Common._M(0.0004f);
				}
			}
			if (GameApp.gApp.mUserProfile != null && GameApp.gApp.mUserProfile.IsLoaded())
			{
				this.mUserProfileLoaded = true;
			}
			this.mRippleCnt += (float)this.mPantalookRipplePct;
			this.MarkDirty();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0003334C File Offset: 0x0003154C
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			this.mLeftTorch.Dispose();
			this.mRightTorch.Dispose();
			this.mVolcanoSmoke.Dispose();
			for (int i = 0; i < LoadingScreen.MAX_VOLCANO_PROJECTILES; i++)
			{
				this.mVolcanoProjectiles[i].mProjectile.Dispose();
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000333A4 File Offset: 0x000315A4
		public override void Draw(SexyGraphics g)
		{
			if (this.mState >= 2 && this.mState == 2)
			{
				GameApp.gApp.GetLoadingThreadProgress();
			}
			this.mHasShown = true;
			if (this.mWaitingForConfirmation)
			{
				g.SetColor(Color.Black);
				g.FillRect(GlobalMembers.gSexyApp.mScreenBounds);
				g.DrawImage(this.IMAGE_LS_LOGO1, (this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2);
				return;
			}
			if (this.mState < 2)
			{
				g.SetColor(Color.Black);
				g.FillRect(GlobalMembers.gSexyApp.mScreenBounds);
			}
			float num = 255f - 255f * this.mExtraProgress;
			int num2 = (num < (float)Common._M(128)) ? ((int)num) : Common._M1(128);
			if (this.mState >= 2 && !this.mFadeToMainMenu)
			{
				g.SetColorizeImages(true);
				int num3 = (int)((float)Common._M(51) + (255f - num) * Common._M1(0.8f));
				g.SetColor(num3, num3, num3, 255);
				g.DrawImage(this.IMAGE_LS_HAPPYSKY_BKGRND, Common._S(0), 0, GameApp.gApp.GetScreenRect().mWidth, this.IMAGE_LS_HAPPYSKY_BKGRND.GetHeight());
				g.SetColorizeImages(false);
				if (num > 0f)
				{
					g.PushState();
					g.PopState();
				}
				for (int i = 0; i < 3; i++)
				{
					if (i == 1)
					{
						g.DrawImage(this.IMAGE_LS_HAPPYSKY_LAVA, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_HAPPYSKY_LAVA) - this.mLoadingXOffset), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_HAPPYSKY_LAVA)));
						g.SetColor(255, 255, 255, (int)this.mLavaAlpha);
						g.SetDrawMode(1);
						g.SetColorizeImages(true);
						g.DrawImage(this.IMAGE_LS_HAPPYSKY_LAVA, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_HAPPYSKY_LAVA) - this.mLoadingXOffset), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_HAPPYSKY_LAVA)));
						g.SetColorizeImages(false);
						g.SetDrawMode(0);
					}
					ResID id = ResID.IMAGE_LS_HAPPYSKY_ISLAND3 - i;
					int num4 = Common._DS(Res.GetOffsetXByID(id) - this.mLoadingXOffset);
					int num5 = Common._DS(Res.GetOffsetYByID(id));
					g.PushState();
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, 255 - (int)num);
					if (num < 255f)
					{
						g.DrawImage(Res.GetImageByID(id), num4, num5);
					}
					g.PopState();
				}
				if (this.mLoadingComplete && num != 255f)
				{
					this.mEffectBatch.DrawBatch(g);
				}
				g.PushState();
				if (this.mDarkIslandAlpha < 255)
				{
					g.SetColorizeImages(true);
				}
				g.SetColor(255, 255, 255, this.mDarkIslandAlpha);
				g.PopState();
				if (num != 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)num);
				}
				g.DrawImage(this.IMAGE_LS_CENTER_CLOUD, (int)((float)GameApp.gApp.mWidth - (float)this.IMAGE_LS_CENTER_CLOUD.mWidth * 2f) / 2, Common._DS(Common._M(240)), (int)((float)this.IMAGE_LS_CENTER_CLOUD.GetWidth() * 2f), (int)((float)this.IMAGE_LS_CENTER_CLOUD.GetHeight() * 2f));
				g.SetColorizeImages(false);
				if (num < 255f && this.mLoadingComplete)
				{
					if (255f - num > 0f)
					{
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, (int)(255f - num));
					}
					for (int j = 3; j >= 0; j--)
					{
						ResID id2 = ResID.IMAGE_LS_CALM_WAVE_1 + j;
						Image imageByID = Res.GetImageByID(id2);
						LoadingWave loadingWave = this.mCalmWaves[j];
						float num6 = (float)((this.mWidth - imageByID.mWidth) / 2) + Common._DS(loadingWave.mRadius * (float)Math.Cos((double)loadingWave.mAngle)) + loadingWave.mXOff;
						float num7 = loadingWave.mY - Common._DS(loadingWave.mRadius * (float)Math.Sin((double)loadingWave.mAngle));
						g.DrawImage(imageByID, (int)num6, (int)num7);
					}
					g.SetColorizeImages(false);
				}
				for (int k = 2; k >= 0; k--)
				{
					if (k == this.mFrogWave)
					{
						int num8 = 255;
						if (this.mLoadingComplete && this.mLoadingCompleteTime >= 200)
						{
							num8 = 255 - (this.mLoadingCompleteTime - 200);
						}
						if (num8 < 0)
						{
							num8 = 0;
						}
						if (num8 > 0)
						{
							if (this.mLoadingComplete)
							{
								this.mCloudUpdateCount++;
							}
							for (int l = 2; l >= 0; l--)
							{
								int mWidth = this.mWidth;
								float mStartX = this.mClouds[l].mStartX;
								float value = this.mClouds[l].mStartX + (float)((l != 1) ? -1 : 1) * this.mClouds[l].mVX * (float)this.mCloudUpdateCount;
								this.DrawLightning(g, (int)Common._DS(value), l);
								g.PushState();
								g.SetColorizeImages(true);
								g.SetColor(255, 255, 255, num8);
								Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LS_CLOUD1A + l * 3);
								int num9 = 2;
								g.DrawImage(imageByID2, (int)Common._DS(value), (int)Common._DS(this.mClouds[l].mY), imageByID2.GetWidth() * num9, imageByID2.GetHeight() * num9);
								g.PopState();
							}
						}
						float num10;
						if (this.mFrogWave == 0)
						{
							num10 = (float)Common._DS(Common._M(200)) + this.mFrogPct * (float)Common._DS(Common._M1(1200));
						}
						else if (this.mFrogWave % 2 == 1)
						{
							num10 = (float)Common._DS(Common._M(2000)) - this.mFrogPct * (float)Common._DS(Common._M1(2500));
						}
						else
						{
							num10 = (float)Common._DS(Common._M(-700)) + this.mFrogPct * (float)Common._DS(Common._M1(2800));
						}
						this.mGlobalTransform.Reset();
						if (g.Is3D())
						{
							float num11 = 0f;
							float num12 = 0f;
							this.mGlobalTransform.Translate(-num11, -num12);
							if (g.Is3D())
							{
								this.mGlobalTransform.Scale(this.mFrogScale, this.mFrogScale);
								this.mGlobalTransform.RotateRad(this.mFrogAngle);
							}
							this.mGlobalTransform.Translate(num11, num12);
						}
						float num13 = Common._DS(this.mWaves[this.mFrogWave].mY - (float)this.mFrogYOffs[this.mFrogWave] + (float)Common._M(70) - this.mWaves[this.mFrogWave].mRadius / this.mFrogAngleDivisor * (float)Math.Sin((double)this.mWaves[this.mFrogWave].mAngle));
						num10 -= (float)this.mFrogXOffset;
						num13 -= (float)this.mFrogYOffset;
						double num14 = this.mPantaloonFlopPct;
						num14 *= 18.0;
						_ = (((int)num14 + 1 > 9) ? "" : "0") + ((int)num14 + 1);
						ResID[] array = new ResID[]
						{
							ResID.IMAGE_LS_RAFTANIM_RAFT,
							ResID.IMAGE_LS_RAFTANIM_PANTS01 + (int)num14,
							ResID.IMAGE_LS_RAFTANIM_FROG
						};
						for (int m = 0; m < 3; m++)
						{
							ResID resID = array[m];
							if (resID == ResID.IMAGE_LS_RAFTANIM_PANTS01 && GlobalMembers.gIs3D)
							{
								Graphics3D graphics3D = g.Get3D();
								SexyVertex2D[] array2 = new SexyVertex2D[20];
								for (int n = 0; n < 10; n++)
								{
									float num15 = (float)(Math.Sin((double)(this.mRippleCnt * Common._M(0.35f) + (float)n * Common._M1(0.75f))) * (double)Common._S(Common._M2(1.2f)) * (double)n / 9.0 * this.mPantalookRipplePct);
									float num16 = (float)((double)Common._S(Common._M(10)) + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M1(-50)) + (double)num15 + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M2(-80)) * (double)n / 9.0);
									float num17 = (float)((double)this.IMAGE_LS_RAFTANIM_PANTS01.mWidth + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M(10)) + Math.Sin((double)(this.mRippleCnt * Common._M1(0.2f))) * (double)Common._S(Common._M2(1.2f)) * this.mPantalookRipplePct);
									float num18 = (float)((double)this.IMAGE_LS_RAFTANIM_PANTS01.mWidth + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M(80)) + Math.Sin((double)(this.mRippleCnt * Common._M1(0.2f))) * (double)Common._S(Common._M2(1.2f)) * this.mPantalookRipplePct);
									float num19 = (float)((double)this.IMAGE_LS_RAFTANIM_PANTS01.mHeight + Math.Max(0.0, Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M(-70))) * (double)n / 9.0);
									float num20 = (float)((double)Common._S(Common._M(10)) + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M1(-50)) + (double)Math.Max(0f, num15 - Common._M2(0.1f)) + (double)num19 + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M3(-80)) * (double)n / 9.0);
									array2[n * 2] = new SexyVertex2D((float)((double)Common._S(Common._M(47)) + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M1(20)) + (double)(num17 * (float)n / 9f) + (double)g.mTransX), num16, (float)n / 9f, 0f);
									array2[n * 2 + 1] = new SexyVertex2D((float)((double)Common._S(Common._M(47)) + Math.Sin((double)this.mFrogAngle) * (double)Common._S(Common._M1(85)) + (double)(num18 * (float)n / 9f) + (double)g.mTransX), num20, (float)n / 9f, 1f);
								}
								graphics3D.SetTexture(0, this.IMAGE_LS_RAFTANIM_PANTS01);
								graphics3D.DrawPrimitive(0U, (Graphics3D.EPrimitiveType)5, array2, Common._M(18), Color.White, 0, num10, num13, true, 0U);
							}
							else
							{
								Image imageByID3 = Res.GetImageByID(resID);
								this.mGlobalTransform.Reset();
								this.mGlobalTransform.Translate((float)(Common._DS(Res.GetOffsetXByID(resID)) + imageByID3.mWidth / 2), (float)(Common._DS(Res.GetOffsetYByID(resID)) + imageByID3.mHeight / 2));
								this.mGlobalTransform.Translate((float)(-(float)this.mCenterOffX), (float)(-(float)this.mCenterOffY));
								if (GlobalMembers.gIs3D)
								{
									this.mGlobalTransform.Scale(this.mFrogScale, this.mFrogScale);
									this.mGlobalTransform.RotateRad(this.mFrogAngle);
								}
								this.mGlobalTransform.Translate((float)this.mCenterOffX, (float)this.mCenterOffY);
								if (GlobalMembers.gIs3D)
								{
									g.DrawImageTransformF(imageByID3, this.mGlobalTransform, num10, num13);
								}
								else
								{
									g.DrawImageTransform(imageByID3, this.mGlobalTransform, (float)((int)num10), (float)((int)num13));
								}
							}
						}
					}
					if ((int)num > 0)
					{
						if (num < 255f)
						{
							g.SetColorizeImages(true);
							g.SetColor(255, 255, 255, (int)num);
						}
						Image imageByID4 = Res.GetImageByID(ResID.IMAGE_LS_WAVE1 + k);
						LoadingWave loadingWave2 = this.mWaves[k];
						float num21 = (float)((this.mWidth - imageByID4.mWidth * this.mWaveImgResScale) / 2) + Common._DS(loadingWave2.mRadius * (float)Math.Cos((double)loadingWave2.mAngle));
						g.DrawImage(imageByID4, (int)num21, (int)Common._DS(loadingWave2.mY - loadingWave2.mRadius * (float)Math.Sin((double)loadingWave2.mAngle)), imageByID4.GetWidth() * this.mWaveImgResScale, imageByID4.GetHeight() * this.mWaveImgResScale);
						g.SetColorizeImages(false);
					}
				}
				if (num2 > 0)
				{
					for (int num22 = 2; num22 >= 0; num22--)
					{
						int num23 = (int)((float)this.mWidth - this.mClouds[num22].mStartX);
						float mStartX2 = this.mClouds[num22].mStartX;
						float num24 = (float)((num22 != 1) ? -1 : 1) * this.mExtraProgress / 3f;
						g.PushState();
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, num2);
						Image imageByID5 = Res.GetImageByID(ResID.IMAGE_LS_CLOUD1C + num22 * 3);
						g.DrawImage(imageByID5, (int)Common._DS(this.mClouds[num22].mStartX + this.mExtraProgress / 3f / (float)(num22 + 1) * (float)Common._M(2000) + this.mClouds[num22].mShadowOffset), (int)Common._DS(this.mClouds[num22].mShadowY), imageByID5.GetWidth() * 10, imageByID5.GetHeight() * 10);
						g.PopState();
					}
				}
				bool flag = this.mLoadingComplete;
			}
			if (!this.mFadeToMainMenu)
			{
				if (this.mState == 0)
				{
					if (Enumerable.Count<PartnerLogo>(this.mPartnerLogos) == 0)
					{
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, 255);
						g.DrawImage(this.IMAGE_LS_LOGO1, (this.mWidth - this.IMAGE_LS_LOGO1.mWidth) / 2, (this.mHeight - this.IMAGE_LS_LOGO1.mHeight) / 2);
						g.SetColorizeImages(false);
					}
					else
					{
						g.PushState();
						PartnerLogo partnerLogo = this.mPartnerLogos[0];
						if (partnerLogo.mAlpha != 255)
						{
							g.SetColorizeImages(true);
						}
						g.SetColor(255, 255, 255, partnerLogo.mAlpha);
						g.DrawImage(partnerLogo.mImage, (this.mWidth - partnerLogo.mImage.mWidth) / 2, (this.mHeight - partnerLogo.mImage.mHeight) / 2);
						g.PopState();
					}
				}
				else if (this.mState == 1)
				{
					Image imageByID6 = Res.GetImageByID(ResID.IMAGE_LS_LOGO1 + this.mLightningFrame);
					if (this.mLightningOn)
					{
						g.DrawImage(imageByID6, this.pts[this.mLightningFrame].mX, (this.mLightningFrame == 0) ? this.pts[this.mLightningFrame].mY : 0);
					}
					for (int num25 = 0; num25 < Enumerable.Count<LogoLightning>(this.mLogoLightning); num25++)
					{
						LogoLightning logoLightning = this.mLogoLightning[num25];
						g.SetColor(255, 255, 255, (int)(255f * ((float)logoLightning.mTimer / (float)logoLightning.mTimerTarget)));
						g.SetColorizeImages(true);
						g.DrawImage(logoLightning.mImage, (this.mWidth - logoLightning.mImage.mWidth * 2) / 2, 0, logoLightning.mImage.GetWidth() * 2, logoLightning.mImage.GetHeight() * 2);
						g.SetColorizeImages(false);
					}
				}
			}
			if (this.mLoadingBarAlpha > 0f && (!this.mFadeToMainMenu || this.mBlackFadeIn))
			{
				if (this.mLoadingBarAlpha < 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)this.mLoadingBarAlpha);
				}
				g.DrawImage(this.IMAGE_LS_BACKING, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_BACKING)), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_BACKING)), GameApp.gApp.GetScreenRect().mWidth, this.IMAGE_LS_BACKING.GetHeight());
				if (this.mCompleteLoadingBarAlpha < 255f)
				{
					float num26 = (float)GlobalMembers.gSexyApp.GetLoadingThreadProgress();
					int num27 = (int)((float)this.IMAGE_LS_REDLOADINGBAR.mWidth * num26);
					if (num27 > this.IMAGE_LS_REDLOADINGBAR.mWidth)
					{
						num27 = this.IMAGE_LS_REDLOADINGBAR.mWidth;
					}
					Rect rect;
					rect = new Rect(this.IMAGE_LS_REDLOADINGBAR.mWidth - num27, 0, num27, this.IMAGE_LS_REDLOADINGBAR.mHeight);
					int num28 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_REDLOADINGBAR) - this.mLoadingXOffset);
					g.DrawImage(this.IMAGE_LS_REDLOADINGBAR, num28, this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_REDLOADINGBAR)), rect);
					this.mLoadStarRotateAngle += -0.1f;
					g.DrawImageRotated(this.IMAGE_LS_STARFISH, num28 + num27 - Common._DS(Common._M(40)), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_STARFISH)), (double)this.mLoadStarRotateAngle);
				}
				g.SetColorizeImages(false);
				if (this.mCompleteLoadingBarAlpha > 0f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)this.mCompleteLoadingBarAlpha);
					g.DrawImage(this.IMAGE_LS_GREENLOADEDBAR, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_GREENLOADEDBAR) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_GREENLOADEDBAR)));
					if (this.mCompleteLoadingBarAlpha >= 255f)
					{
						int num29 = 127 + Common.GetAlphaFromUpdateCount(this.mUpdateCnt, Common._M(128));
						g.SetColor(255, 255, 255, num29);
					}
					g.SetColorizeImages(false);
				}
				if (this.mLoadingBarAlpha < 255f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)this.mLoadingBarAlpha);
				}
				g.DrawImage(this.IMAGE_LS_BAR, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_BAR) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_BAR)));
				if (GameApp.gApp.GetLoadingThreadProgress() < (double)Common._M(0.06f))
				{
					g.DrawImage(this.IMAGE_LS_L_TIKI01, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_L_TIKI01) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_L_TIKI01)));
				}
				else
				{
					g.DrawImage(this.IMAGE_LS_L_TIKI02, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_L_TIKI02) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_L_TIKI02)));
				}
				if (GameApp.gApp.GetLoadingThreadProgress() < (double)Common._M(0.95f))
				{
					g.DrawImage(this.IMAGE_LS_R_TIKI01, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_R_TIKI01) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_R_TIKI01)));
				}
				else
				{
					g.DrawImage(this.IMAGE_LS_R_TIKI02, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_R_TIKI02) - this.mLoadingXOffset), this.mLoadingYOffset + Common._DS(Res.GetOffsetYByID(ResID.IMAGE_LS_R_TIKI02)));
				}
				g.SetColorizeImages(false);
				if (this.mCompleteLoadingBarAlpha < 255f)
				{
					g.PushState();
					g.SetColorizeImages(true);
					int num30;
					if (this.mLoadingBarAlpha < 255f)
					{
						num30 = (int)this.mLoadingBarAlpha;
					}
					else if (this.mCompleteLoadingBarAlpha > 0f)
					{
						num30 = 255 - (int)this.mCompleteLoadingBarAlpha;
					}
					else
					{
						num30 = 127 + Common.GetAlphaFromUpdateCount(this.mUpdateCnt, Common._M(128));
					}
					g.SetColor(200, 200, 200, num30);
					Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_STROKE);
					g.SetFont(fontByID);
					string text;
					if (this.mFirstRun)
					{
						text = this.mLoadingTextContainer.GetBackStoryText()[this.mLoadingTextIdx];
					}
					else
					{
						text = this.mLoadingTextContainer.GetLoadingText()[this.mLoadingTextIdx];
					}
					Rect rect2 = this.mLoadingTextFrame;
					rect2.mY = 368;
					rect2.mX = 110;
					g.SetScale(1.5f, 1.5f, g.mScaleOrigX, g.mScaleOrigY);
					g.WriteWordWrapped(rect2, text, -1, 0);
					g.PopState();
				}
				if (this.mCompleteLoadingBarAlpha > 0f)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)this.mCompleteLoadingBarAlpha);
					int num31 = 369;
					if (Localization.GetCurrentLanguage() != null)
					{
						num31 = this.IMAGE_LS_GREENLOADEDBAR.GetWidth() - 51;
					}
					int num32 = 30;
					int num33 = (num31 - this.IMAGE_LS_CLICKTXT.GetWidth()) / 2;
					int num34 = (num32 - this.IMAGE_LS_CLICKTXT.GetHeight()) / 2;
					int num35 = Common._DS(Common._M(605));
					if (Localization.GetCurrentLanguage() != null)
					{
						num35 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_LS_GREENLOADEDBAR)) + 51;
					}
					int num36 = Common._DS(Common._M1(1042));
					g.DrawImage(this.IMAGE_LS_CLICKTXT, num35 + num33, num36 + num34);
					g.SetColorizeImages(false);
				}
			}
			if (this.mFlashAlpha > 0f)
			{
				g.SetColor(255, 255, 255, (int)this.mFlashAlpha);
				g.FillRect(GlobalMembers.gSexyApp.mScreenBounds);
			}
			base.DeferOverlay(20);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000349A7 File Offset: 0x00032BA7
		public void ButtonPress(int id)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000349A9 File Offset: 0x00032BA9
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000349AB File Offset: 0x00032BAB
		public void ButtonDepress(int theId)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000349AD File Offset: 0x00032BAD
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000349AF File Offset: 0x00032BAF
		public void ButtonMouseEnter(int theId)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000349B1 File Offset: 0x00032BB1
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000349B3 File Offset: 0x00032BB3
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000349B8 File Offset: 0x00032BB8
		public void ProcessHardwareBackButton()
		{
			if (this.mLockBGM)
			{
				Dialog dialog = GameApp.gApp.GetDialog(1);
				dialog.ButtonDepress(1001);
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (this.mState <= 1)
			{
				GameApp.gApp.Shutdown();
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (!this.mLoadingComplete)
			{
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (GameApp.gApp.GetDialog(1) != null)
			{
				GameApp.gApp.GetDialog(1).ButtonDepress(1001);
				GameApp.gApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			GameApp.gApp.DoQuitPromptDialog();
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00034A78 File Offset: 0x00032C78
		public void ProcessYesNo(int theId)
		{
			if (theId == 1000)
			{
				if (!GameApp.gApp.IsRegistered() && GameApp.gApp.mTrialType == 1 && GameApp.gApp.GetBoolean("UpsellExit", false))
				{
					GameApp.gApp.DoUpsell(true);
					return;
				}
				GameApp.gApp.Shutdown();
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00034AD0 File Offset: 0x00032CD0
		public void ProcessBGM()
		{
			string @string = TextManager.getInstance().getString(58);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(58), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessBGMlock);
			this.mLockBGM = true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00034B5A File Offset: 0x00032D5A
		public void ProcessBGMlock(int theId)
		{
			this.mLockBGM = false;
			if (theId == 1000)
			{
				GameApp.gApp.mMusicInterface.stopUserMusic();
			}
		}

		// Token: 0x04000AC3 RID: 2755
		protected const float mCenterCloudImageScale = 2f;

		// Token: 0x04000AC4 RID: 2756
		protected long updateTimes;

		// Token: 0x04000AC5 RID: 2757
		protected long drawTimes;

		// Token: 0x04000AC6 RID: 2758
		public static int MAX_VOLCANO_PROJECTILES = 4;

		// Token: 0x04000AC7 RID: 2759
		private static float max_angle = 0.08726f;

		// Token: 0x04000AC8 RID: 2760
		private static int LOADING_TEXT_TIME = 200;

		// Token: 0x04000AC9 RID: 2761
		protected List<PartnerLogo> mPartnerLogos = new List<PartnerLogo>();

		// Token: 0x04000ACA RID: 2762
		protected float mLoadStarRotateAngle;

		// Token: 0x04000ACB RID: 2763
		protected float mLavaAlpha;

		// Token: 0x04000ACC RID: 2764
		protected bool mIncLavaAlpha;

		// Token: 0x04000ACD RID: 2765
		protected List<LogoLightning> mLogoLightning = new List<LogoLightning>();

		// Token: 0x04000ACE RID: 2766
		protected LoadingWave[] mWaves = new LoadingWave[5];

		// Token: 0x04000ACF RID: 2767
		protected LoadingWave[] mCalmWaves = new LoadingWave[4];

		// Token: 0x04000AD0 RID: 2768
		protected LoadingCloud[] mClouds = new LoadingCloud[3];

		// Token: 0x04000AD1 RID: 2769
		protected float mLoadingBarAlpha;

		// Token: 0x04000AD2 RID: 2770
		protected float mCompleteLoadingBarAlpha;

		// Token: 0x04000AD3 RID: 2771
		protected float mFlashAlpha;

		// Token: 0x04000AD4 RID: 2772
		protected float mExtraProgress;

		// Token: 0x04000AD5 RID: 2773
		protected float mFrogAngle;

		// Token: 0x04000AD6 RID: 2774
		protected float mFrogAngleDivisor;

		// Token: 0x04000AD7 RID: 2775
		protected float mFrogAngleDelta;

		// Token: 0x04000AD8 RID: 2776
		protected float mZumaY;

		// Token: 0x04000AD9 RID: 2777
		protected float mRevengeY;

		// Token: 0x04000ADA RID: 2778
		protected float mRevengeStretch;

		// Token: 0x04000ADB RID: 2779
		protected float mBlackFadeAlpha;

		// Token: 0x04000ADC RID: 2780
		protected bool mBlackFadeIn;

		// Token: 0x04000ADD RID: 2781
		protected bool mCanShowMenu;

		// Token: 0x04000ADE RID: 2782
		protected bool mFrogPitchForward;

		// Token: 0x04000ADF RID: 2783
		protected bool mHasShown;

		// Token: 0x04000AE0 RID: 2784
		protected bool mLightningOn;

		// Token: 0x04000AE1 RID: 2785
		protected bool mLoadingComplete;

		// Token: 0x04000AE2 RID: 2786
		protected bool mFadeToMainMenu;

		// Token: 0x04000AE3 RID: 2787
		protected bool mFirstRun;

		// Token: 0x04000AE4 RID: 2788
		protected int mLightningTimer;

		// Token: 0x04000AE5 RID: 2789
		protected int mLightningFrame;

		// Token: 0x04000AE6 RID: 2790
		protected int mLogoHoldTime;

		// Token: 0x04000AE7 RID: 2791
		protected int mState;

		// Token: 0x04000AE8 RID: 2792
		protected int mFrogWave;

		// Token: 0x04000AE9 RID: 2793
		protected int mStormTimer;

		// Token: 0x04000AEA RID: 2794
		protected int mClearTimer;

		// Token: 0x04000AEB RID: 2795
		protected int mLoadingCompleteDelay;

		// Token: 0x04000AEC RID: 2796
		protected float mFrogScale;

		// Token: 0x04000AED RID: 2797
		protected float mFrogPct;

		// Token: 0x04000AEE RID: 2798
		protected int mDarkIslandAlpha;

		// Token: 0x04000AEF RID: 2799
		protected int mLoadingCompleteTime;

		// Token: 0x04000AF0 RID: 2800
		protected int mCloudUpdateCount;

		// Token: 0x04000AF1 RID: 2801
		protected CurvedVal mPantaloonFlopPct = new CurvedVal();

		// Token: 0x04000AF2 RID: 2802
		protected CurvedVal mPantalookRipplePct = new CurvedVal();

		// Token: 0x04000AF3 RID: 2803
		protected float mRippleCnt;

		// Token: 0x04000AF4 RID: 2804
		protected int mLoadingOffset;

		// Token: 0x04000AF5 RID: 2805
		protected int mLoadingTextIdx;

		// Token: 0x04000AF6 RID: 2806
		protected int mLoadingTextTime;

		// Token: 0x04000AF7 RID: 2807
		protected List<int> mSeenLoadingTextIndices = new List<int>();

		// Token: 0x04000AF8 RID: 2808
		protected int mOffsetParticle = 85;

		// Token: 0x04000AF9 RID: 2809
		protected bool mUserProfileLoaded;

		// Token: 0x04000AFA RID: 2810
		protected bool mLoading;

		// Token: 0x04000AFB RID: 2811
		protected PIEffect mLeftTorch;

		// Token: 0x04000AFC RID: 2812
		protected PIEffect mRightTorch;

		// Token: 0x04000AFD RID: 2813
		protected PIEffect mVolcanoSmoke;

		// Token: 0x04000AFE RID: 2814
		protected VolcanoProjectile[] mVolcanoProjectiles = new VolcanoProjectile[LoadingScreen.MAX_VOLCANO_PROJECTILES];

		// Token: 0x04000AFF RID: 2815
		public PIEffectBatch mEffectBatch;

		// Token: 0x04000B00 RID: 2816
		protected Transform mGlobalTransform = new Transform();

		// Token: 0x04000B01 RID: 2817
		protected int mLoadingXOffset;

		// Token: 0x04000B02 RID: 2818
		protected int mLoadingYOffset;

		// Token: 0x04000B03 RID: 2819
		protected Rect mLoadingTextFrame = default(Rect);

		// Token: 0x04000B04 RID: 2820
		protected Image IMAGE_LS_LOGO1 = Res.GetImageByID(ResID.IMAGE_LS_LOGO1);

		// Token: 0x04000B05 RID: 2821
		protected Image IMAGE_LS_LIGHT1_ID;

		// Token: 0x04000B06 RID: 2822
		protected Image IMAGE_LS_REDLOADINGBAR = Res.GetImageByID(ResID.IMAGE_LS_REDLOADINGBAR);

		// Token: 0x04000B07 RID: 2823
		protected Image IMAGE_LS_CLICKTXT = Res.GetImageByID(ResID.IMAGE_LS_CLICKTXT);

		// Token: 0x04000B08 RID: 2824
		protected Image IMAGE_LS_BACKING = Res.GetImageByID(ResID.IMAGE_LS_BACKING);

		// Token: 0x04000B09 RID: 2825
		protected Image IMAGE_LS_STARFISH = Res.GetImageByID(ResID.IMAGE_LS_STARFISH);

		// Token: 0x04000B0A RID: 2826
		protected Image IMAGE_LS_R_TIKI02 = Res.GetImageByID(ResID.IMAGE_LS_R_TIKI02);

		// Token: 0x04000B0B RID: 2827
		protected Image IMAGE_LS_R_TIKI01 = Res.GetImageByID(ResID.IMAGE_LS_R_TIKI01);

		// Token: 0x04000B0C RID: 2828
		protected Image IMAGE_LS_L_TIKI02 = Res.GetImageByID(ResID.IMAGE_LS_L_TIKI02);

		// Token: 0x04000B0D RID: 2829
		protected Image IMAGE_LS_L_TIKI01 = Res.GetImageByID(ResID.IMAGE_LS_L_TIKI01);

		// Token: 0x04000B0E RID: 2830
		protected Image IMAGE_LS_GREENLOADEDBAR = Res.GetImageByID(ResID.IMAGE_LS_GREENLOADEDBAR);

		// Token: 0x04000B0F RID: 2831
		protected Image IMAGE_LS_BAR = Res.GetImageByID(ResID.IMAGE_LS_BAR);

		// Token: 0x04000B10 RID: 2832
		protected Image IMAGE_LS_HAPPYSKY_BKGRND = Res.GetImageByID(ResID.IMAGE_LS_HAPPYSKY_BKGRND);

		// Token: 0x04000B11 RID: 2833
		protected Image IMAGE_LS_HAPPYSKY_LAVA = Res.GetImageByID(ResID.IMAGE_LS_HAPPYSKY_LAVA);

		// Token: 0x04000B12 RID: 2834
		protected Image IMAGE_LS_CENTER_CLOUD = Res.GetImageByID(ResID.IMAGE_LS_CENTER_CLOUD);

		// Token: 0x04000B13 RID: 2835
		protected Image IMAGE_LS_RAFTANIM_PANTS01 = Res.GetImageByID(ResID.IMAGE_LS_RAFTANIM_PANTS01);

		// Token: 0x04000B14 RID: 2836
		protected Point[] pts = new Point[5];

		// Token: 0x04000B15 RID: 2837
		protected int mCenterOffX;

		// Token: 0x04000B16 RID: 2838
		protected int mCenterOffY;

		// Token: 0x04000B17 RID: 2839
		protected int mFrogXOffset = Common._S(Common._M(100));

		// Token: 0x04000B18 RID: 2840
		protected int mFrogYOffset = Common._S(Common._M(120));

		// Token: 0x04000B19 RID: 2841
		protected int mWaveImgResScale = 2;

		// Token: 0x04000B1A RID: 2842
		public bool mLockBGM;

		// Token: 0x04000B1B RID: 2843
		protected int[] mFrogYOffs = new int[]
		{
			Common._DS(Common._M(100)),
			Common._DS(Common._M1(20)),
			Common._DS(Common._M2(-40)),
			Common._DS(Common._M3(0)),
			Common._DS(Common._M4(60))
		};

		// Token: 0x04000B1C RID: 2844
		private LoadingScreen.LoadingTextContainer mLoadingTextContainer = new LoadingScreen.LoadingTextContainer();

		// Token: 0x04000B1D RID: 2845
		public bool mWaitingForConfirmation;

		// Token: 0x04000B1E RID: 2846
		private ButtonWidget mHelpButton;

		// Token: 0x04000B1F RID: 2847
		private ButtonWidget mStartButton;

		// Token: 0x02000135 RID: 309
		internal static class RandomNumbers
		{
			// Token: 0x06000FED RID: 4077 RVA: 0x000A3687 File Offset: 0x000A1887
			internal static int NextNumber()
			{
				if (LoadingScreen.RandomNumbers.r == null)
				{
					LoadingScreen.RandomNumbers.Seed();
				}
				return LoadingScreen.RandomNumbers.r.Next();
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x000A369F File Offset: 0x000A189F
			internal static int NextNumber(int ceiling)
			{
				if (LoadingScreen.RandomNumbers.r == null)
				{
					LoadingScreen.RandomNumbers.Seed();
				}
				return LoadingScreen.RandomNumbers.r.Next(ceiling);
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x000A36B8 File Offset: 0x000A18B8
			internal static void Seed()
			{
				LoadingScreen.RandomNumbers.r = new Random();
			}

			// Token: 0x06000FF0 RID: 4080 RVA: 0x000A36C4 File Offset: 0x000A18C4
			internal static void Seed(int seed)
			{
				LoadingScreen.RandomNumbers.r = new Random(seed);
			}

			// Token: 0x04001A38 RID: 6712
			private static Random r;
		}

		// Token: 0x02000136 RID: 310
		private enum State
		{
			// Token: 0x04001A3A RID: 6714
			State_LogoIntro,
			// Token: 0x04001A3B RID: 6715
			State_Lightning,
			// Token: 0x04001A3C RID: 6716
			State_Loading,
			// Token: 0x04001A3D RID: 6717
			State_Final
		}

		// Token: 0x02000137 RID: 311
		internal class LoadingTextContainer
		{
			// Token: 0x06000FF1 RID: 4081 RVA: 0x000A36D1 File Offset: 0x000A18D1
			private static string _(string s)
			{
				return s;
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x000A36D4 File Offset: 0x000A18D4
			public LoadingTextContainer()
			{
				int num = 582;
				int num2 = 29;
				for (int i = num; i < num + num2; i++)
				{
					this.mLoadingText.Add(TextManager.getInstance().getString(i));
				}
				num += num2;
				num2 = 3;
				for (int j = num; j < num + num2; j++)
				{
					this.mBackStoryText.Add(TextManager.getInstance().getString(j));
				}
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x000A3754 File Offset: 0x000A1954
			public List<string> GetLoadingText()
			{
				return this.mLoadingText;
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x000A375C File Offset: 0x000A195C
			public List<string> GetBackStoryText()
			{
				return this.mBackStoryText;
			}

			// Token: 0x04001A3E RID: 6718
			private List<string> mLoadingText = new List<string>();

			// Token: 0x04001A3F RID: 6719
			private List<string> mBackStoryText = new List<string>();
		}
	}
}
