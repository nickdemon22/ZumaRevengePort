using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	// Token: 0x0200000C RID: 12
	public class Level : IDisposable
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00023D24 File Offset: 0x00021F24
		protected void InitFinalBossLevel()
		{
			if (!this.mApp.mResourceManager.IsGroupLoaded("CloakedBoss") && !this.mApp.mResourceManager.LoadResources("CloakedBoss"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
			}
			this.mDoTorchCrap = true;
			this.mBoard.mPreventBallAdvancement = true;
			this.mTorchTextAlpha = 700f;
			this.mTorchStageState = 0;
			this.mTorchStageTimer = Common._M(150);
			this.mTorchDaisScale = 1f;
			this.mTorchCompMgr = this.mApp.LoadComposition("pax\\cloakedboss", "_BOSSES");
			Composition composition = this.mTorchCompMgr.GetComposition("squish");
			this.mTorchBossX = (float)(-(float)Common._DS(composition.mWidth) - Common._DS(Common._M(500)));
			this.mTorchBossY = (float)Common._DS(Common._M(-920));
			this.mTorchBossDestX = (float)Common._DS(Common._M(-520));
			this.mTorchBossDestY = (float)Common._DS(Common._M(-462));
			int num = Common._M(50);
			this.mTorchBossVX = (this.mTorchBossDestX - this.mTorchBossX) / (float)num;
			this.mTorchBossVY = (this.mTorchBossDestY - this.mTorchBossY) / (float)num;
			for (int i = 0; i < this.mTorches.Count; i++)
			{
				this.mTorches[i].mActive = (this.mTorches[i].mDraw = true);
			}
			for (int j = 0; j < 3; j++)
			{
				this.mCloakedBossTextAlpha[j] = 0f;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00023ED4 File Offset: 0x000220D4
		public Level()
		{
			this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = 0);
			this.mTorchStageState = -1;
			this.mTorchBossX = (this.mTorchBossY = -1f);
			this.mTorchDaisScale = 1f;
			this.mTorchCompMgr = null;
			this.mTorchStageTimer = 0;
			this.mTorchBossVX = (this.mTorchBossVY = (this.mTorchBossDestX = (this.mTorchBossDestY = 0f)));
			this.mFrogFlyOff = null;
			this.mTorchStageShakeAmt = 0;
			this.mNumGauntletBallsBroke = 0;
			this.mBossBGID = "";
			this.mZumaPulseUCStart = 0;
			this.mCurGauntletMultPct = 0f;
			this.mChallengePoints = 100;
			this.mChallengeAcePoints = 1000;
			this.mTorchStageAlpha = 0f;
			this.mGauntletCurTime = 0;
			this.mCloakPoof = null;
			this.mCloakClapFrame = -1;
			this.mCanDrawBoss = true;
			this.mIndex = -1;
			this.mIronFrog = false;
			this.mStartingGauntletLevel = 1;
			this.mAllCurvesAtRolloutPoint = false;
			this.mHasReachedCruisingSpeed = false;
			this.mPotPct = 1f;
			this.mFrog = null;
			this.mUpdateCount = 0;
			this.mFireSpeed = 8f;
			this.mBGFromPSD = false;
			this.mCurBarSizeInc = 1;
			this.mEndSequence = -1;
			this.mDoTorchCrap = false;
			this.mHasDoneTorchCrap = false;
			this.mTorchTextAlpha = 0f;
			this.mReloadDelay = 0;
			this.mTreasureFreq = 300;
			this.mParTime = 0;
			this.mBoss = (this.mOrgBoss = null);
			this.mSecondaryBoss = null;
			for (int i = 0; i < 4; i++)
			{
				this.mCurveSkullAngleOverrides[i] = float.MaxValue;
			}
			this.mLoopAtEnd = false;
			this.mIsEndless = false;
			this.mInvertMouseTimer = (this.mMaxInvertMouseTimer = 0);
			this.mTimer = (this.mTimeToComplete = -1);
			this.mBossFreezePowerupTime = (this.mFrogShieldPowerupCount = 300);
			this.mSliderEdgeRotate = false;
			this.mTorchTimer = 0;
			this.mFinalLevel = false;
			this.mNoBackground = false;
			this.mFurthestBallDistance = 0;
			this.mOffscreenClearBonus = false;
			this.mIntroTorchDelay = 0;
			this.mIntroTorchIndex = -1;
			for (int j = 0; j < 5; j++)
			{
				this.mFrogImages[j] = new LillyPadImageInfo();
				this.mFrogImages[j].mImage = null;
			}
			this.mPostZumaTimeCounter = 0;
			this.mPostZumaTimeSlowInc = (this.mPostZumaTimeSpeedInc = 0f);
			this.mZone = (this.mNum = -1);
			this.mApp = GameApp.gApp;
			this.mBoard = ((this.mApp != null) ? this.mApp.GetBoard() : null);
			this.mHurryToRolloutAmt = 0f;
			this.mTempSpeedupTimer = 0;
			this.mSuckMode = false;
			this.mMoveType = 0;
			this.mMoveSpeed = 25;
			this.mNumFrogPoints = 0;
			this.mCurFrogPoint = 0;
			this.mFrogX[0] = 320;
			this.mFrogY[0] = 240;
			this.mDoingPadHints = false;
			this.mBarWidth = (this.mBarHeight = 0);
			this.mNoFlip = false;
			this.mHoleMgr = new HoleMgr();
			this.mDrawCurves = false;
			for (int k = 0; k < 4; k++)
			{
				this.mCurveMgr[k] = null;
			}
			this.mNumCurves = 0;
			this.mMirrorType = MirrorType.MirrorType_None;
			if (this.mApp != null)
			{
				this.mGingerMouthXStart = (float)this.mApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW)));
				this.mFredMouthXStart = (float)this.mApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW)));
			}
			this.mZumaBarX = 344;
			this.mZumaBarWidth = int.MaxValue;
			this.Reset();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000243C4 File Offset: 0x000225C4
		public virtual Level Clone()
		{
			Level level = (Level)base.MemberwiseClone();
			level.mCurveMgr = new CurveMgr[4];
			level.mCloakedBossTextAlpha = (float[])this.mCloakedBossTextAlpha.Clone();
			level.mDaisRocks = new List<DaisRock>();
			if (this.mDaisRocks != null)
			{
				level.mDaisRocks.AddRange(this.mDaisRocks.ToArray());
			}
			level.mEggs = new List<TorchLevelEgg>();
			if (this.mEggs != null)
			{
				level.mEggs.AddRange(this.mEggs.ToArray());
			}
			level.mMovingWallDefaults = new List<Wall>();
			if (this.mMovingWallDefaults != null)
			{
				level.mMovingWallDefaults.AddRange(this.mMovingWallDefaults.ToArray());
			}
			level.mEffects = new List<Effect>();
			if (this.mEffects != null)
			{
				level.mEffects.AddRange(this.mEffects.ToArray());
			}
			level.mCloakPoof = this.mCloakPoof;
			level.mFrogFlyOff = this.mFrogFlyOff;
			level.mPowerupRegions = new List<PowerupRegion>();
			if (this.mPowerupRegions != null)
			{
				level.mPowerupRegions.AddRange(this.mPowerupRegions.ToArray());
			}
			level.mTorches = new List<Torch>();
			if (this.mTorches != null)
			{
				level.mTorches.AddRange(this.mTorches.ToArray());
			}
			level.mEffectNames = new List<string>();
			if (this.mEffectNames != null)
			{
				level.mEffectNames.AddRange(this.mEffectNames.ToArray());
			}
			level.mEffectParams = new List<EffectParams>();
			if (this.mEffectParams != null)
			{
				level.mEffectParams.AddRange(this.mEffectParams.ToArray());
			}
			level.mTreasurePoints = new List<TreasurePoint>();
			if (this.mTreasurePoints != null)
			{
				level.mTreasurePoints.AddRange(this.mTreasurePoints.ToArray());
			}
			level.mCurveMgr = (CurveMgr[])this.mCurveMgr.Clone();
			level.mCurveSkullAngleOverrides = (float[])this.mCurveSkullAngleOverrides.Clone();
			level.mHoleMgr = this.mHoleMgr;
			level.mTunnelData = new List<TunnelData>();
			if (this.mTunnelData != null)
			{
				level.mTunnelData.AddRange(this.mTunnelData.ToArray());
			}
			level.mWalls = new List<Wall>();
			if (this.mWalls != null)
			{
				level.mWalls.AddRange(this.mWalls.ToArray());
			}
			level.mFrogImages = (LillyPadImageInfo[])this.mFrogImages.Clone();
			level.mFrogX = (int[])this.mFrogX.Clone();
			level.mFrogY = (int[])this.mFrogY.Clone();
			return level;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00024658 File Offset: 0x00022858
		public virtual void Dispose()
		{
			if (this.mCloakPoof != null)
			{
				this.mCloakPoof.Dispose();
				this.mCloakPoof = null;
			}
			if (this.mFrogFlyOff != null)
			{
				this.mFrogFlyOff.Dispose();
				this.mFrogFlyOff = null;
			}
			if (this.mTorchCompMgr != null)
			{
				this.mTorchCompMgr = null;
			}
			for (int i = 0; i < 4; i++)
			{
				if (this.mCurveMgr[i] != null)
				{
					this.mCurveMgr[i].Dispose();
					this.mCurveMgr[i] = null;
				}
			}
			for (int j = 0; j < 5; j++)
			{
				if (this.mFrogImages[j].mFilename.Length > 0)
				{
					if (this.mFrogImages[j].mImage != null)
					{
						this.mFrogImages[j].mImage.Dispose();
					}
				}
				else
				{
					this.mApp.mResourceManager.DeleteImage(this.mFrogImages[j].mResId);
				}
			}
			if (this.mHoleMgr != null)
			{
				this.mHoleMgr = null;
			}
			if (this.mOrgBoss != null && this.mOrgBoss.mResGroup.Length > 0 && !Common.StrEquals(this.mOrgBoss.mResGroup, "Boss6Common") && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mOrgBoss.mResGroup))
			{
				GameApp.gApp.mResourceManager.DeleteResources(this.mOrgBoss.mResGroup);
			}
			if (this.mSecondaryBoss != null && this.mSecondaryBoss.mResGroup.Length > 0 && !Common.StrEquals(this.mOrgBoss.mResGroup, "Boss6Common") && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mSecondaryBoss.mResGroup))
			{
				GameApp.gApp.mResourceManager.DeleteResources(this.mSecondaryBoss.mResGroup);
			}
			if (this.mBossBGID != "")
			{
				BaseRes baseRes = GameApp.gApp.mResourceManager.GetBaseRes(0, this.mBossBGID);
				string text = baseRes.mCompositeResGroup;
				if (text.Length == 0)
				{
					text = baseRes.mResGroup;
				}
				if (text.Length > 0 && GameApp.gApp.mResourceManager.IsGroupLoaded(text))
				{
					GameApp.gApp.mResourceManager.DeleteResources(text);
				}
			}
			if (this.mOrgBoss != null)
			{
				this.mOrgBoss.Dispose();
				this.mOrgBoss = null;
			}
			if (this.mSecondaryBoss != null)
			{
				this.mSecondaryBoss.Dispose();
				this.mSecondaryBoss = null;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000248B2 File Offset: 0x00022AB2
		public virtual int GetNumCurves()
		{
			return this.mNumCurves;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000248BC File Offset: 0x00022ABC
		public virtual int GetGunPointFromPos(int x, int y)
		{
			for (int i = 0; i < this.mNumFrogPoints; i++)
			{
				int num = x - this.mFrogX[i];
				int num2 = y - this.mFrogY[i];
				if (num * num + num2 * num2 < 3136)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00024904 File Offset: 0x00022B04
		public virtual void Preload()
		{
			if (this.mZone == 6 && (this.IsFinalBossLevel() || this.mEndSequence != -1) && this.IsFinalBossLevel())
			{
				this.mBossIntroBG = this.mApp.mResourceManager.GetResourceRef(0, "IMAGE_BOSS6_INTRO_BG").GetSharedImageRef();
				this.mBossBGID = "IMAGE_BOSS6_INTRO_BG";
			}
			if (this.mBoss != null && this.mZone != 6)
			{
				this.mBossIntroBG = this.mApp.mResourceManager.GetResourceRef(0, this.mBoss.mResPrefix + "INTRO_BG").GetSharedImageRef();
				this.mBossBGID = this.mBoss.mResPrefix + "INTRO_BG";
			}
			if (this.mBossBGID != "")
			{
				BaseRes baseRes = this.mApp.mResourceManager.GetBaseRes(0, this.mBossBGID);
				string text = baseRes.mCompositeResGroup;
				if (text != "")
				{
					text = baseRes.mResGroup;
				}
				if (text != "" && !this.mApp.mResourceManager.IsGroupLoaded(text))
				{
					this.mApp.mResourceManager.LoadResources(text);
					if (!this.mApp.mResourceManager.LoadResources(text))
					{
						this.mApp.ShowResourceError(true);
					}
				}
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00024A58 File Offset: 0x00022C58
		public virtual void StartLevel(bool from_load, bool needs_reinit)
		{
			new Stopwatch("Level::StartLevel");
			this.Preload();
			if (this.mZone == 5 && !this.mApp.mResourceManager.IsGroupLoaded("GrottoSounds") && !this.mApp.mResourceManager.LoadResources("GrottoSounds"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (this.mZone != 5 && this.mApp.mResourceManager.IsGroupLoaded("GrottoSounds"))
			{
				this.mApp.mResourceManager.DeleteResources("GrottoSounds");
			}
			else if (this.mZone != 6 && this.mApp.mResourceManager.IsGroupLoaded("Boss6Common"))
			{
				this.mApp.mResourceManager.DeleteResources("Boss6Common");
			}
			if (!needs_reinit)
			{
				new Stopwatch("Level::StartLevel::GetImage - FrogImages");
				for (int i = 0; i < 5; i++)
				{
					if (this.mFrogImages[i].mFilename.Length != 0)
					{
						string pathFrom = Common.GetPathFrom(this.mFrogImages[i].mFilename, "");
						string idByPath = GameApp.gApp.mResourceManager.GetIdByPath(pathFrom);
						this.mFrogImages[i].mImage = (DeviceImage)this.mApp.mResourceManager.LoadImage(idByPath).GetImage();
						this.mFrogImages[i].mImage.mNumCols = 2;
					}
					else if (this.mFrogImages[i].mResId.Length != 0)
					{
						this.mFrogImages[i].mImage = (DeviceImage)this.mApp.mResourceManager.LoadImage(this.mFrogImages[i].mResId).GetImage();
						if (this.mFrogImages[i].mImage != null)
						{
							this.mFrogImages[i].mImage.mNumCols = 2;
						}
					}
				}
			}
			new Stopwatch("Level::StartLevel::LoadCurve");
			for (int j = 0; j < this.mNumCurves; j++)
			{
				if (!this.mCurveMgr[j].mIsLoaded && !this.mCurveMgr[j].LoadCurve())
				{
					this.mApp.Popup("Unable to load curve for " + this.mCurveMgr[j].GetPath());
				}
				if (this.mBoard.GauntletMode())
				{
					this.mCurveMgr[j].mCurveDesc.mVals.mNumColors = GameApp.gDDS.GetNumGauntletBalls(this.mNumCurves);
				}
				this.mCurveMgr[j].StartLevel(from_load);
				if (j == 0)
				{
					this.mCurveMgr[j].mInitialPathHilite = true;
				}
			}
			for (int k = 0; k < this.mHoleMgr.GetNumHoles(); k++)
			{
				int l = 0;
				while (l < 4)
				{
					if (this.mHoleMgr.GetHole(k).mCurveNum == l)
					{
						if (this.mCurveSkullAngleOverrides[l] < 3.4028235E+38f)
						{
							this.mHoleMgr.GetHole(k).mRotation = this.mCurveSkullAngleOverrides[l];
							break;
						}
						break;
					}
					else
					{
						l++;
					}
				}
			}
			Common.gAddBalls = false;
			if (!needs_reinit)
			{
				this.mEffects.Clear();
				this.InitEffects();
				if (this.IsFinalBossLevel() && !this.mHasDoneTorchCrap && !this.mDoTorchCrap)
				{
					this.InitFinalBossLevel();
				}
				else if (this.mEndSequence == 3)
				{
					this.mBoard.mPreventBallAdvancement = false;
				}
				this.ResetEffects();
			}
			this.mPostZumaTimeCounter = this.mApp.GetLevelMgr().mPostZumaTime;
			this.mPostZumaTimeSlowInc = 0f;
			this.mPostZumaTimeSpeedInc = 0f;
			this.mTimer = this.mTimeToComplete;
			if (this.mBoss != null)
			{
				this.mBoss.Init(this);
				if (this.mSecondaryBoss != null)
				{
					this.mSecondaryBoss.Init(this);
				}
				if (!needs_reinit && this.mEndSequence == 2 && this.mBoard.GetGameState() == GameState.GameState_Playing)
				{
					this.mCloakClapFrame = -1;
					this.mCanDrawBoss = false;
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST);
					this.mTorchBossY = (float)(-(float)imageByID.mHeight - Common._DS(Common._M(100)));
					this.mCloakPoof = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_CLOAKTOLAMEEXPLOSION01").Duplicate();
					Common.SetFXNumScale(this.mCloakPoof, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.15f));
					for (int m = 0; m < 3; m++)
					{
						this.mCloakedBossTextAlpha[m] = 0f;
					}
				}
				if (GameApp.gDDS.HasBossParam("HurryAmt"))
				{
					this.mHurryToRolloutAmt = GameApp.gDDS.GetBossParam("HurryAmt");
				}
				this.mGingerMouthX = this.mGingerMouthXStart + 20f;
				this.mFredMouthX = this.mFredMouthXStart - 20f;
				this.mFredTongueX = 505f;
				this.mTargetBarSize = 330;
				this.mCurBarSize = 0;
			}
			else if (this.mTimeToComplete > 0)
			{
				this.mGingerMouthX = this.mGingerMouthXStart + 20f;
				this.mFredMouthX = this.mFredMouthXStart - 20f;
				this.mFredTongueX = 505f;
				this.mTargetBarSize = 330;
				this.mCurBarSize = 0;
			}
			if (this.IsFinalBossLevel())
			{
				if (!this.mApp.mResourceManager.IsGroupLoaded("Bosses") && !this.mApp.mResourceManager.LoadResources("Bosses"))
				{
					this.mApp.ShowResourceError(true);
					this.mApp.Shutdown();
					return;
				}
				this.mIntroTorchDelay = 0;
				this.mIntroTorchIndex = -1;
			}
			if (!needs_reinit)
			{
				for (int n = 0; n < Common.size<Effect>(this.mEffects); n++)
				{
					this.mEffects[n].LevelStarted(from_load);
				}
			}
			if (this.mBoard.GauntletMode())
			{
				this.mGauntletCurNumForMult = this.mApp.GetLevelMgr().mGauntletNumForMultBase;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00025033 File Offset: 0x00023233
		public virtual void StartLevel()
		{
			this.StartLevel(false, false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00025040 File Offset: 0x00023240
		public string GetCurvePath(int curve_num)
		{
			string text = "levels/";
			if (this.mCurveMgr[curve_num].mCurveDesc.mPath.IndexOf('/') != -1 || this.mCurveMgr[curve_num].mCurveDesc.mPath.IndexOf('\\') != -1)
			{
				text += this.mCurveMgr[curve_num].mCurveDesc.mPath;
			}
			else
			{
				text = text + this.mId + "/" + this.mCurveMgr[curve_num].mCurveDesc.mPath;
			}
			return text;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000250CB File Offset: 0x000232CB
		public bool CanDrawFrog()
		{
			return !this.IsFinalBossLevel() || this.mTorchStageState == 6 || this.mTorchStageState > 10;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000250EC File Offset: 0x000232EC
		public virtual void Reset(bool reset_effects)
		{
			this.mGauntletTimeRedAmt = 0f;
			this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = 0);
			this.mGauntletCurNumForMult = 0;
			this.mGauntletCurTime = 0;
			this.mAllCurvesAtRolloutPoint = false;
			this.mHasReachedCruisingSpeed = false;
			this.mZumaBallPct = 0f;
			this.mZumaBallFrame = 0;
			this.mTargetBarSize = 0;
			this.mCurBarSize = 0;
			this.mBarLightness = 0f;
			this.mHaveReachedTarget = false;
			this.mNumGauntletBallsBroke = 0;
			this.mCurGauntletMultPct = 0f;
			this.mGauntletMultipliersEarned = 0;
			this.mGingerMouthX = this.mGingerMouthXStart;
			this.mFredMouthX = this.mFredMouthXStart;
			this.mGingerMouthVX = 0.5f;
			this.mFredMouthVX = 0f;
			this.mFredTongueX = 541f;
			this.mFredTongueVX = 0f;
			this.mZumaBallPct = 0f;
			this.mZumaBarState = -1;
			this.mFurthestBallDistance = 0;
			this.mGoldBallXOff = 0f;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].Reset();
			}
			if (this.mApp != null && reset_effects && ((this.mBoard != null && !this.mBoard.GauntletMode()) || (this.mBoard == null && this.mApp.mLoadingThreadStarted && !this.mApp.mLoadingThreadCompleted)))
			{
				for (int j = 0; j < Enumerable.Count<Effect>(this.mEffects); j++)
				{
					this.mEffects[j].NukeParams();
				}
				for (int k = 0; k < Enumerable.Count<EffectParams>(this.mEffectParams); k++)
				{
					this.mEffects[this.mEffectParams[k].mEffectIndex].SetParams(this.mEffectParams[k].mKey, this.mEffectParams[k].mValue);
				}
				for (int l = 0; l < Enumerable.Count<Effect>(this.mEffects); l++)
				{
					this.mEffects[l].Reset(this.mId);
				}
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00025301 File Offset: 0x00023501
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0002530C File Offset: 0x0002350C
		public virtual void ReInit()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].SetFarthestBall(0);
			}
			this.mPotPct = 1f;
			this.mCurBarSizeInc = 1;
			this.mInvertMouseTimer = (this.mMaxInvertMouseTimer = 0);
			this.mTimer = (this.mTimeToComplete = -1);
			this.mFurthestBallDistance = 0;
			this.mOffscreenClearBonus = false;
			this.mPostZumaTimeCounter = 0;
			this.mPostZumaTimeSlowInc = (this.mPostZumaTimeSpeedInc = 0f);
			this.mTempSpeedupTimer = 0;
			if (this.mOrgBoss != null)
			{
				int x = this.mBoss.GetX();
				int y = this.mBoss.GetY();
				Boss boss = this.mApp.GetLevelMgr().GetLevelById(this.mId).mBoss;
				Boss boss2 = boss.Instantiate();
				boss2.mName = this.mDisplayName;
				boss2.PostInstantiationHook(boss);
				boss2.mLevel = this;
				this.mOrgBoss = null;
				this.mBoss = boss2;
				this.mBoss.SetXY((float)x, (float)y);
				this.mOrgBoss = this.mBoss;
			}
			if (this.mSecondaryBoss != null)
			{
				Boss boss3 = this.mApp.GetLevelMgr().GetLevelById(this.mId).mSecondaryBoss;
				Boss boss2 = boss3.Instantiate();
				boss2.mName = this.mDisplayName;
				boss2.PostInstantiationHook(boss3);
				boss2.mLevel = this;
				this.mSecondaryBoss = null;
				this.mSecondaryBoss = boss2;
			}
			for (int j = 0; j < Common.size<Torch>(this.mTorches); j++)
			{
				if (this.mTorches[j].mFlame != null)
				{
					this.mTorches[j].mFlame.ResetAnim();
					this.mTorches[j].mFlame.mEmitAfterTimeline = true;
				}
				this.mTorches[j].mActive = true;
				this.mTorches[j].mDraw = true;
				this.mTorches[j].mWasHit = false;
			}
			this.Reset(false);
			for (int k = 0; k < this.mNumCurves; k++)
			{
				this.mCurveMgr[k].Reset();
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0002554A File Offset: 0x0002374A
		public virtual void AfterBoardAdded()
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0002554C File Offset: 0x0002374C
		public virtual bool CollidedWithWall(Bullet b)
		{
			float num = (float)b.GetRadius() * Common._M(0.75f);
			FRect frect;
			frect = new FRect(b.GetX() - num, b.GetY() - num, num * 2f, num * 2f);
			for (int i = 0; i < Common.size<Wall>(this.mWalls); i++)
			{
				Wall wall = this.mWalls[i];
				if (wall.mStrength != 0 && wall.mType != 0)
				{
					int num2 = (wall.mImage == null) ? ((int)wall.mWidth) : wall.mImage.GetCelWidth();
					int num3 = (wall.mImage == null) ? ((int)wall.mHeight) : wall.mImage.GetCelHeight();
					int num4 = (wall.mImage == null) ? 0 : (num2 / 2);
					int num5 = (wall.mImage == null) ? 0 : (num3 / 2);
					FRect frect2;
					frect2 = new FRect(wall.mX - (float)num4, wall.mY - (float)num5, (float)num2, (float)num3);
					if (frect2.Intersects(frect))
					{
						if (wall.mStrength > 0)
						{
							wall.mStrength--;
						}
						if (wall.mStrength == 0)
						{
							wall.mCurRespawnTimer = 0;
						}
						frect2.Inflate(frect.mWidth / 2f, frect.mHeight / 2f);
						FPoint a = new FPoint(b.GetX() + b.mVelX, b.GetY() + b.mVelY);
						FPoint a2 = new FPoint(b.GetX() - b.mVelX, b.GetY() - b.mVelY);
						float angle;
						if (Common.LinesIntersect(a, a2, new FPoint(frect2.mX, frect2.mY), new FPoint(frect2.mX + frect2.mWidth, frect2.mY)))
						{
							angle = 90f;
						}
						else if (Common.LinesIntersect(a, a2, new FPoint(frect2.mX, frect2.mY + frect2.mHeight), new FPoint(frect2.mX + frect2.mWidth, frect2.mY + frect2.mHeight)))
						{
							angle = 270f;
						}
						else if (Common.LinesIntersect(a, a2, new FPoint(frect2.mX, frect2.mY), new FPoint(frect2.mX, frect2.mY + frect2.mHeight)))
						{
							angle = 180f;
						}
						else
						{
							if (!Common.LinesIntersect(a, a2, new FPoint(frect2.mX + frect2.mWidth, frect2.mY), new FPoint(frect2.mX + frect2.mWidth, frect2.mY + frect2.mHeight)))
							{
								return true;
							}
							angle = 0f;
						}
						this.mBoard.AddBallExplosionParticleEffect(b, angle, 180f);
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_WALLBALL));
						return true;
					}
				}
			}
			Rect r;
			r = new Rect((int)frect.mX, (int)frect.mY, (int)frect.mWidth, (int)frect.mHeight);
			for (int j = 0; j < Common.size<Torch>(this.mTorches); j++)
			{
				Torch torch = this.mTorches[j];
				torch.CheckCollision(r);
			}
			return false;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000258A4 File Offset: 0x00023AA4
		public virtual void CopyEffectsFrom(Level l)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				for (int j = 0; j < Common.size<Effect>(l.mEffects); j++)
				{
					if (Common.StrEquals(l.mEffects[j].GetName(), this.mEffects[i].GetName()))
					{
						this.mEffects[i].CopyFrom(l.mEffects[j]);
						break;
					}
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00025925 File Offset: 0x00023B25
		public virtual string GetStatsScreenText(GameStats stats, int score)
		{
			return "";
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0002592C File Offset: 0x00023B2C
		public void AddTorch(int x, int y, int w, int h)
		{
			Torch torch = new Torch();
			torch.mX = x;
			torch.mY = y;
			torch.mWidth = w;
			torch.mHeight = h;
			this.mTorches.Add(torch);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00025968 File Offset: 0x00023B68
		public bool PointIntersectsWall(float x, float y)
		{
			if (Enumerable.Count<Wall>(this.mWalls) == 0)
			{
				return false;
			}
			for (int i = 0; i < Enumerable.Count<Wall>(this.mWalls); i++)
			{
				Wall wall = this.mWalls[i];
				Rect rect;
				rect = new Rect((int)wall.mX, (int)wall.mY, (int)wall.mWidth, (int)wall.mHeight);
				if (wall.mStrength != 0 && rect.Contains((int)x, (int)y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000259E4 File Offset: 0x00023BE4
		public void DrawDaisRocks(SexyGraphics g)
		{
			for (int i = 0; i < Common.size<DaisRock>(this.mDaisRocks); i++)
			{
				DaisRock daisRock = this.mDaisRocks[i];
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)daisRock.mAlpha);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.Scale(daisRock.mSize, daisRock.mSize);
				float num = (255f - daisRock.mAlpha) / 255f * Common._M(2.5f) * 3.1415927f;
				this.mGlobalTranform.RotateRad(num);
				g.DrawImageTransform(daisRock.mImg, this.mGlobalTranform, daisRock.mX, daisRock.mY);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00025AB0 File Offset: 0x00023CB0
		public void FadeInkSpots()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].QuicklyFadeInkSpots();
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00025ADC File Offset: 0x00023CDC
		public void MultiplierActivated()
		{
			this.mGauntletCurNumForMult += this.mApp.GetLevelMgr().mGauntletNumForMultInc;
			if (this.mGauntletCurNumForMult > this.mApp.GetLevelMgr().mMaxGauntletNumForMult)
			{
				this.mGauntletCurNumForMult = this.mApp.GetLevelMgr().mMaxGauntletNumForMult;
			}
			int mMultiplierDuration = this.mApp.GetLevelMgr().mMultiplierDuration;
			if (this.mCurMultiplierTimeLeft == 0)
			{
				this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = mMultiplierDuration);
			}
			else
			{
				this.mCurMultiplierTimeLeft += mMultiplierDuration;
				this.mMaxMultiplierTime = this.mCurMultiplierTimeLeft;
			}
			if (GameApp.gDDS.AddMultiplierTime(this.mApp.GetLevelMgr().mMultiplierTimeAdd))
			{
				this.UpdateChallengeModeDifficulty();
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00025B9C File Offset: 0x00023D9C
		public void UpdateChallengeModeDifficulty()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].mCurveDesc.mVals.mNumColors = GameApp.gDDS.GetNumGauntletBalls(this.mNumCurves);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00025BE4 File Offset: 0x00023DE4
		public virtual void SkipInitialPathHilite()
		{
			bool flag = false;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (Enumerable.Count<PathSparkle>(this.mCurveMgr[i].mSparkles) > 0 || this.mCurveMgr[i].mInitialPathHilite)
				{
					flag = true;
					this.mCurveMgr[i].mSparkles.Clear();
					this.mCurveMgr[i].mInitialPathHilite = false;
				}
			}
			if (flag)
			{
				Common.gAddBalls = true;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00025C54 File Offset: 0x00023E54
		public virtual bool DoingInitialPathHilite()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].mInitialPathHilite)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00025C84 File Offset: 0x00023E84
		public virtual void SwitchToSecondaryBoss()
		{
			int x = this.mBoss.GetX();
			int y = this.mBoss.GetY();
			float hp = this.mBoss.GetHP();
			this.mBoss = this.mSecondaryBoss;
			this.mBoss.SetHP(hp);
			this.mBoss.SetXY((float)x, (float)y);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00025CDC File Offset: 0x00023EDC
		public virtual void Update(float f)
		{
			this.mUpdateCount++;
			if (this.mTimer > 0)
			{
				this.mTimer--;
			}
			if (this.mInvertMouseTimer > 0 && --this.mInvertMouseTimer == 0)
			{
				GameApp.gApp.GetBoard().UpdateGunPos();
			}
			if (this.mTorchStageState == 10 || this.mTorchStageState == 11 || (this.mTorchStageState == 9 && this.mBoard.mFullScreenAlphaRate < 0))
			{
				if (this.mTorchStageState != 11 && this.mUpdateCount % Common._M(2) == 0)
				{
					List<Image> list = new List<Image>();
					for (int i = 0; i < 3; i++)
					{
						list.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_PEBBLE1 + i));
						list.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_SPECK1 + i));
					}
					this.mDaisRocks.Add(new DaisRock());
					DaisRock daisRock = Common.back<DaisRock>(this.mDaisRocks);
					daisRock.mImg = list[Common.Rand(Common.size<Image>(list))];
					daisRock.mX = (float)Common._DS(MathUtils.IntRange(Common._M(400), Common._M1(1200)));
					daisRock.mY = (float)(-(float)daisRock.mImg.mHeight / 2);
				}
				for (int j = 0; j < Common.size<DaisRock>(this.mDaisRocks); j++)
				{
					DaisRock daisRock2 = this.mDaisRocks[j];
					daisRock2.mY += Common._M(15f);
					daisRock2.mSize -= Common._M(0.002f);
					daisRock2.mAlpha -= Common._M(0.1f);
					if (daisRock2.mSize <= 0f || daisRock2.mAlpha <= 0f)
					{
						this.mDaisRocks.RemoveAt(j);
						j--;
					}
				}
			}
			if ((this.IsFinalBossLevel() && this.mTorchStageState != 6) || this.mTorchStageState >= 10)
			{
				string[] array = new string[]
				{
					"start",
					"squish",
					"rattle"
				};
				Composition composition = null;
				int num;
				switch (this.mTorchStageState)
				{
				case 0:
					num = 0;
					break;
				case 1:
					num = 1;
					break;
				default:
					num = 2;
					break;
				}
				if (this.mTorchStageState < 6)
				{
					composition = this.mTorchCompMgr.GetComposition(array[num]);
				}
				float num2 = Common._M(0.97f);
				int num3 = Common._M(15);
				if (this.mTorchStageState == 0)
				{
					if (--this.mTorchStageTimer <= 0)
					{
						composition.Update();
						this.mTorchBossX += this.mTorchBossVX;
						this.mTorchBossY += this.mTorchBossVY;
						if (this.mTorchBossX >= this.mTorchBossDestX)
						{
							this.mTorchBossX = this.mTorchBossDestX;
							this.mTorchBossVX = 0f;
						}
						if (this.mTorchBossY >= this.mTorchBossDestY)
						{
							this.mTorchBossY = this.mTorchBossDestY;
							this.mTorchBossVY = 0f;
						}
						if (this.mTorchBossVX == 0f && this.mTorchBossVY == 0f)
						{
							this.mTorchStageState = 1;
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CLOAKED_DAIS_LANDING));
						}
					}
				}
				else if (this.mTorchStageState == 1)
				{
					composition.Update();
					if (composition.mUpdateCount == num3)
					{
						this.mTorchDaisScale = num2;
					}
					float num4 = (1f - num2) / (float)(composition.GetMaxDuration() - num3);
					this.mTorchDaisScale += num4;
					if (this.mTorchDaisScale > 1f)
					{
						this.mTorchDaisScale = 1f;
					}
					if (composition.mUpdateCount >= composition.GetMaxDuration() && Common._eq(this.mTorchDaisScale, 1f))
					{
						this.mTorchStageState = 2;
						this.mTorchStageTimer = Common._M(100);
					}
				}
				else if (this.mTorchStageState == 2)
				{
					int num5 = Common._M(100);
					if (this.mTorchStageTimer > 0)
					{
						this.mTorchStageTimer--;
					}
					if (this.mTorchStageTimer == 0 && Common.size<TorchLevelEgg>(this.mEggs) < 4)
					{
						composition.Update();
					}
					float[] array2 = new float[]
					{
						38f,
						38f,
						1421f,
						1423f
					};
					float[] array3 = new float[]
					{
						82f,
						952f,
						85f,
						949f
					};
					if (composition.mUpdateCount >= composition.GetMaxDuration() && this.mTorchStageTimer <= 0 && Common.size<TorchLevelEgg>(this.mEggs) < 4)
					{
						this.mTorchStageTimer = num5;
						composition.Reset();
						this.mEggs.Add(new TorchLevelEgg());
						TorchLevelEgg torchLevelEgg = Common.back<TorchLevelEgg>(this.mEggs);
						torchLevelEgg.mX = (float)Common._DS(Common._M(545));
						torchLevelEgg.mY = (float)Common._DS(Common._M(208));
						torchLevelEgg.mAlpha = 0f;
						torchLevelEgg.mDestX = Common._DS(array2[this.mEggs.Count - 1]);
						torchLevelEgg.mDestY = Common._DS(array3[this.mEggs.Count - 1] + (float)Common._M(60));
						float num6 = Common._M(60f);
						torchLevelEgg.mVX = (torchLevelEgg.mDestX - torchLevelEgg.mX) / num6;
						torchLevelEgg.mVY = (torchLevelEgg.mDestY - torchLevelEgg.mY) / num6;
						torchLevelEgg.mDestAngle = 3.1415927f * Common._M(3f);
						if (torchLevelEgg.mDestX > torchLevelEgg.mX)
						{
							torchLevelEgg.mDestAngle *= -1f;
						}
						torchLevelEgg.mAngleInc = torchLevelEgg.mDestAngle / num6;
					}
					for (int k = 0; k < Common.size<TorchLevelEgg>(this.mEggs); k++)
					{
						TorchLevelEgg torchLevelEgg2 = this.mEggs[k];
						if (torchLevelEgg2.mAlpha < 255f && (torchLevelEgg2.mVX != 0f || torchLevelEgg2.mVY != 0f))
						{
							torchLevelEgg2.mAlpha += (float)Common._M(8);
							if (torchLevelEgg2.mAlpha > 255f)
							{
								torchLevelEgg2.mAlpha = 255f;
							}
						}
						torchLevelEgg2.mX += torchLevelEgg2.mVX;
						torchLevelEgg2.mY += torchLevelEgg2.mVY;
						int num7 = 0;
						if ((torchLevelEgg2.mVX < 0f && torchLevelEgg2.mX <= torchLevelEgg2.mDestX) || (torchLevelEgg2.mVX > 0f && torchLevelEgg2.mX >= torchLevelEgg2.mDestX))
						{
							num7++;
						}
						if ((torchLevelEgg2.mVY < 0f && torchLevelEgg2.mY <= torchLevelEgg2.mDestY) || (torchLevelEgg2.mVY > 0f && torchLevelEgg2.mY >= torchLevelEgg2.mDestY))
						{
							num7++;
						}
						if (num7 == 2)
						{
							if (this.mTorches[k].mActive)
							{
								this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_TORCH_EXTINGUISHED));
							}
							this.mTorches[k].mActive = false;
						}
						torchLevelEgg2.mAngle += torchLevelEgg2.mAngleInc;
					}
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG);
					if (Common.size<TorchLevelEgg>(this.mEggs) == 4 && !new Rect(-80, 0, this.mApp.mWidth + Common._S(160), this.mApp.mHeight).Intersects(new Rect((int)Enumerable.Last<TorchLevelEgg>(this.mEggs).mX, (int)Enumerable.Last<TorchLevelEgg>(this.mEggs).mY, imageByID.mWidth, imageByID.mHeight)))
					{
						this.mTorchStageState = 3;
						this.mTorchStageTimer = Common._M(50);
					}
				}
				else if (this.mTorchStageState == 7 || (this.mTorchStageState == 9 && this.mBoard.mFullScreenAlphaRate < 0))
				{
					this.mBoard.UpdatePlayingFX();
					List<Image> list2 = new List<Image>();
					for (int l = 0; l < 3; l++)
					{
						list2.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_PEBBLE1 + l));
						list2.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_SPECK1 + l));
					}
					this.mTorchStageAlpha += Common._M(1.5f);
					this.mTorchStageShakeAmt = Common.Rand(Common._M(5));
					if (this.mUpdateCount % Common._M(10) == 0)
					{
						Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_DIAS);
						this.mDaisRocks.Add(new DaisRock());
						DaisRock daisRock3 = Common.back<DaisRock>(this.mDaisRocks);
						float num8 = (float)(Common._DS(660) + Common._DS(Common._M(30)));
						float num9 = num8 + (float)Common._DS(Common._M(40));
						float num10 = num8 + (float)imageByID2.mWidth - (float)Common._DS(Common._M(100));
						float num11 = num10 + (float)Common._DS(Common._M(35));
						float mY = (float)(Common._DS(417) + imageByID2.mHeight - Common._DS(Common._M(100)));
						daisRock3.mImg = list2[Common.Rand(Common.size<Image>(list2))];
						float num12 = (float)(Common.IntRange((int)num8, (int)num9) - daisRock3.mImg.mWidth / 2);
						float num13 = (float)(Common.IntRange((int)num10, (int)num11) + daisRock3.mImg.mWidth / 2);
						daisRock3.mX = ((Common.Rand(2) == 0) ? num12 : num13);
						daisRock3.mY = mY;
					}
					if (this.mUpdateCount % Common._M(50) == 0)
					{
						this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
						if (++Level.last_sound_idx >= 2)
						{
							Level.last_sound_idx = 0;
						}
					}
					for (int m = 0; m < Common.size<DaisRock>(this.mDaisRocks); m++)
					{
						DaisRock daisRock4 = this.mDaisRocks[m];
						daisRock4.mY += Common._M(1f);
						daisRock4.mSize -= Common._M(0.02f);
						daisRock4.mAlpha -= Common._M(1f);
						if (daisRock4.mSize <= 0f || daisRock4.mAlpha <= 0f)
						{
							this.mDaisRocks.RemoveAt(m);
							m--;
						}
					}
					if (this.mTorchStageAlpha >= 255f && --this.mTorchStageTimer <= 0)
					{
						this.mTorchStageAlpha = 255f;
						this.mTorchStageState = 8;
						this.mTorchStageShakeAmt = 0;
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_LOWERING));
						this.mApp.SetCursor((ECURSOR)0);
					}
				}
				else if (this.mTorchStageState == 3 || this.mTorchStageState == 8)
				{
					for (int n = 0; n < Common.size<DaisRock>(this.mDaisRocks); n++)
					{
						DaisRock daisRock5 = this.mDaisRocks[n];
						daisRock5.mY += Common._M(1f);
						daisRock5.mSize -= Common._M(0.02f);
						daisRock5.mAlpha -= Common._M(1f);
						if (daisRock5.mSize <= 0f || daisRock5.mAlpha <= 0f)
						{
							this.mDaisRocks.RemoveAt(n);
							n--;
						}
					}
					if (this.mTorchStageState == 8 && this.mUpdateCount % Common._M(250) == 0)
					{
						this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
					}
					if (this.mTorchStageTimer == 1 && this.mTorchStageState == 3)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_LOWERING));
					}
					if (--this.mTorchStageTimer <= 0)
					{
						this.mTorchDaisScale -= Common._M(0.01f);
						if (this.mTorchDaisScale <= 0f)
						{
							this.mTorchDaisScale = 0f;
							if (this.mTorchStageState == 3)
							{
								this.mApp.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
								this.mTorchStageState = 4;
								this.mTorchStageTimer = Common._M(150);
							}
							else
							{
								this.mTorchStageState = 9;
							}
						}
					}
				}
				else if (this.mTorchStageState == 4)
				{
					if (--this.mTorchStageTimer <= 0)
					{
						if (this.mTorchStageTimer == 0)
						{
							for (int num14 = 0; num14 < Common.size<Torch>(this.mTorches); num14++)
							{
								this.mTorches[num14].mFlame.ResetAnim();
								this.mTorches[num14].mActive = true;
								this.mTorches[num14].mDraw = true;
							}
						}
						this.mTorchDaisScale += Common._M(0.02f);
						if (this.mTorchDaisScale >= 1f)
						{
							this.mTorchDaisScale = 1f;
							if (this.mFrogFlyOff != null)
							{
								this.mFrogFlyOff.Dispose();
								this.mFrogFlyOff = null;
							}
							this.mFrogFlyOff = new FrogFlyOff();
							this.mFrogFlyOff.JumpIn(this.mFrog, this.mFrog.GetCenterX(), this.mFrog.GetCenterY(), false);
							this.mTorchStageState = 5;
						}
					}
				}
				else if (this.mTorchStageState == 10)
				{
					this.mFrogFlyOff.Update();
					if (this.mFrogFlyOff.mTimer >= this.mFrogFlyOff.mFrogJumpTime)
					{
						this.mApp.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
						this.mTorchStageState = 11;
						this.mTorchStageTimer = Common._M(100);
						this.mFrog.SetPos((int)this.mFrogFlyOff.mFrogX, this.mFrog.GetCurY());
						this.mFrogFlyOff.Dispose();
						this.mFrogFlyOff = null;
					}
				}
				else if (this.mTorchStageState == 5)
				{
					this.mFrogFlyOff.Update();
					if (this.mFrogFlyOff.mTimer > this.mFrogFlyOff.mFrogJumpTime)
					{
						this.mFrog.SetAngle((float)((int)this.mFrogFlyOff.mFrogAngle));
						this.mFrogFlyOff.Dispose();
						this.mFrogFlyOff = null;
						this.mTorchStageState = 6;
						this.mBoard.mPreventBallAdvancement = false;
						this.mDoTorchCrap = false;
						this.mHasDoneTorchCrap = true;
					}
				}
				else if (this.mTorchStageState == 11)
				{
					if (--this.mTorchStageTimer <= 0 && (this.mTorchBossY += (float)Common._M(10)) >= (float)Common._M1(0))
					{
						this.mTorchStageState = 12;
						this.mTorchStageTimer = 0;
					}
				}
				else if (this.mTorchStageState == 12)
				{
					int num15 = Common._M(500);
					this.mTorchStageTimer++;
					for (int num16 = 0; num16 < 3; num16++)
					{
						if (this.mTorchStageTimer >= num15)
						{
							this.mCloakedBossTextAlpha[num16] -= Common._M(2f);
							if (this.mCloakedBossTextAlpha[num16] < 0f)
							{
								this.mCloakedBossTextAlpha[num16] = 0f;
							}
						}
						else
						{
							this.mCloakedBossTextAlpha[num16] += Common._M(2f);
							if (this.mCloakedBossTextAlpha[num16] > 255f)
							{
								this.mCloakedBossTextAlpha[num16] = 255f;
							}
							else if (this.mCloakedBossTextAlpha[num16] < (float)Common._M(128))
							{
								break;
							}
						}
					}
					Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_CLAP);
					int num17 = Common._M(6);
					int num18 = imageByID3.mNumRows * imageByID3.mNumCols;
					if (this.mTorchStageTimer >= num15 && this.mTorchStageTimer % num17 == 0)
					{
						this.mCloakClapFrame++;
						if (this.mCloakClapFrame == Common._M(5))
						{
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CLOAKED_CLAP));
						}
					}
					if (this.mTorchStageTimer >= num15 + Common._M(15))
					{
						this.mCloakPoof.mDrawTransform.LoadIdentity();
						float num19 = GameApp.DownScaleNum(1f);
						this.mCloakPoof.mDrawTransform.Scale(num19, num19);
						this.mCloakPoof.mDrawTransform.Translate((float)Common._DS(Common._M(812)), (float)Common._DS(Common._M1(220)));
						this.mCloakPoof.Update();
						if (Common._eq(this.mCloakPoof.mFrameNum, (float)Common._M(135), 0.5f))
						{
							this.mCanDrawBoss = true;
						}
						else if (this.mCloakPoof.mFrameNum >= (float)this.mCloakPoof.mLastFrameNum)
						{
							this.mBoard.mContinueNextLevelOnLoadProfile = false;
							this.mTorchStageState = 13;
							this.mBoard.mHasDoneIntroSounds = false;
							if (this.mApp.mResourceManager.IsGroupLoaded("CloakedBoss"))
							{
								this.mApp.mResourceManager.DeleteResources("CloakedBoss");
							}
						}
					}
				}
			}
			if (this.mTorchStageState > 4 && this.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mTorchStageState != 12 && this.mTorchTextAlpha > 0f)
			{
				this.mTorchTextAlpha -= Common._M(1.3f);
				if (this.mTorchTextAlpha < 0f)
				{
					this.mTorchTextAlpha = 0f;
				}
			}
			this.UpdateEffects();
			for (int num20 = 0; num20 < Common.size<Wall>(this.mWalls); num20++)
			{
				Wall wall = this.mWalls[num20];
				wall.Update();
				if ((wall.mVX > 0f && wall.mX > (float)Common._SS(this.mApp.mWidth)) || (wall.mVX < 0f && wall.mX + wall.mWidth < 0f) || (wall.mVY > 0f && wall.mY > (float)Common._SS(this.mApp.mHeight)) || (wall.mVY < 0f && wall.mY + wall.mHeight < 0f))
				{
					this.mWalls.RemoveAt(num20);
					num20--;
				}
			}
			for (int num21 = 0; num21 < Common.size<Wall>(this.mMovingWallDefaults); num21++)
			{
				Wall wall2 = this.mMovingWallDefaults[num21];
				int num22 = int.MaxValue;
				bool flag = false;
				for (int num23 = 0; num23 < Common.size<Wall>(this.mWalls); num23++)
				{
					Wall wall3 = this.mWalls[num23];
					if (wall3.mId == wall2.mId)
					{
						flag = true;
						int num24;
						if (wall3.mVX > 0f)
						{
							num24 = (int)((wall3.mX < 0f) ? 0f : (wall3.mX - wall2.mX));
						}
						else
						{
							num24 = (int)((wall3.mX + wall3.mWidth > wall2.mX) ? 0f : (wall2.mX - (wall3.mX + wall3.mWidth)));
						}
						int num25;
						if (wall3.mVY > 0f)
						{
							num25 = (int)((wall3.mY < 0f) ? 0f : (wall3.mY - wall2.mY));
						}
						else
						{
							num25 = (int)((wall3.mY + wall3.mHeight > wall2.mY) ? 0f : (wall2.mY - (wall3.mY + wall3.mHeight)));
						}
						int num26 = num24 * num24 + num25 * num25;
						if (num26 < num22)
						{
							num22 = num26;
						}
					}
				}
				if (num22 > wall2.mSpacing || !flag)
				{
					this.mWalls.Add(wall2);
					Common.back<Wall>(this.mWalls).mCurLifeTimer = MathUtils.IntRange(wall2.mMinLifeTimer, wall2.mMaxLifeTimer);
				}
			}
			this.mHoleMgr.Update();
			if (this.mBoss != null)
			{
				this.mBoss.Update(f);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000271AC File Offset: 0x000253AC
		public virtual void Draw(SexyGraphics g)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].DrawUnderBalls(g);
			}
			for (int j = 0; j < Common.size<Torch>(this.mTorches); j++)
			{
				this.mTorches[j].Draw(g);
			}
			for (int k = 0; k < Common.size<Effect>(this.mEffects); k++)
			{
				this.mEffects[k].DrawUnderBalls(g);
			}
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawBelowBalls(g);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00027244 File Offset: 0x00025444
		public virtual void DrawBottomLevel(SexyGraphics g)
		{
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawBottomLevel(g);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00027264 File Offset: 0x00025464
		public virtual void DrawToplevel(SexyGraphics g)
		{
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawTopLevel(g);
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].DrawTopLevel(g);
			}
			if (this.mTorchTextAlpha > 0f && this.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mTorchStageState > 5)
			{
				int centerX = this.mBoard.GetGun().GetCenterX();
				int centerY = this.mBoard.GetGun().GetCenterY();
				string text = this.mBoard.IsHardAdventureMode() ? TextManager.getInstance().getString(495) : TextManager.getInstance().getString(496);
				string text2 = this.mBoard.IsHardAdventureMode() ? TextManager.getInstance().getString(497) : TextManager.getInstance().getString(498);
				g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
				int num = (int)this.mTorchTextAlpha - 350;
				if (num > 0)
				{
					g.SetColor(0, 0, 0, (num > 255) ? 255 : num);
					g.DrawString(text, Common._S(centerX) - g.GetFont().StringWidth(text) / 2, Common._S(centerY - Common._M(90)));
				}
				g.SetColor(0, 0, 0, ((int)this.mTorchTextAlpha > 255) ? 255 : ((int)this.mTorchTextAlpha));
				g.DrawString(text2, Common._S(centerX) - g.GetFont().StringWidth(text2) / 2, Common._S(centerY + Common._M(120)));
			}
			for (int j = 0; j < Common.size<PowerupRegion>(this.mPowerupRegions); j++)
			{
				PowerupRegion powerupRegion = this.mPowerupRegions[j];
				if (powerupRegion.mDebugDraw)
				{
					g.SetColor(255, 0, 0);
					int numPoints = this.mCurveMgr[powerupRegion.mCurveNum].mWayPointMgr.GetNumPoints();
					float num2;
					float num3;
					this.mCurveMgr[powerupRegion.mCurveNum].GetXYFromWaypoint((int)(powerupRegion.mCurvePctStart * (float)numPoints), out num2, out num3);
					float num4;
					float num5;
					this.mCurveMgr[powerupRegion.mCurveNum].GetXYFromWaypoint((int)(powerupRegion.mCurvePctEnd * (float)numPoints), out num4, out num5);
					g.FillRect(Common._S((int)num2) - 2, Common._S((int)num3) - 2, 4, 4);
					g.SetColor(0, 255, 0);
					g.FillRect(Common._S((int)num4) - 2, Common._S((int)num5) - 2, 4, 4);
				}
			}
			if (this.mTorchStageState >= 11)
			{
				if (this.mTorchStageTimer > 0 || (this.mCloakPoof != null && this.mCloakPoof.mFrameNum < (float)Common._M(135)))
				{
					int num6 = Common._DS(Common._M(32));
					if (this.mTorchStageTimer < Common._M(570))
					{
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, 128);
						g.SetColorizeImages(false);
					}
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST);
					Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_CLAP);
					if (this.mCloakClapFrame < 0)
					{
						g.DrawImage(imageByID, Common._S(this.mBoss.GetX()) - imageByID.mWidth / 2 + Common._DS(Res.GetOffsetXByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST)) - num6, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST)) + Common._S(this.mBoss.GetY()) + (int)this.mTorchBossY - imageByID.mHeight / 2);
					}
					else if (this.mTorchStageTimer < Common._M(570))
					{
						int num7 = Math.Min(this.mCloakClapFrame, imageByID2.mNumRows * imageByID2.mNumCols - 1);
						g.DrawImageCel(imageByID2, Common._S(this.mBoss.GetX()) - imageByID2.GetCelWidth() / 2 - num6, Common._S(this.mBoss.GetY()) - imageByID2.GetCelHeight() / 2 + (int)this.mTorchBossY, num7);
					}
					g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
					bool flag = this.mBoard.IsHardAdventureMode();
					if (this.mTorchStageState == 12)
					{
						string[] array = new string[]
						{
							TextManager.getInstance().getString(490),
							TextManager.getInstance().getString(491),
							TextManager.getInstance().getString(492)
						};
						if (flag)
						{
							array[0] = TextManager.getInstance().getString(493);
							array[1] = TextManager.getInstance().getString(494);
							array[2] = "";
						}
						for (int k = 0; k < array.Length; k++)
						{
							if (this.mCloakedBossTextAlpha[k] > 0f)
							{
								g.SetColor(0, 0, 0, (int)this.mCloakedBossTextAlpha[k]);
								g.WriteString(array[k].ToString(), -GameApp.gApp.mBoardOffsetX, Common._DS(Common._M(550)) + k * g.GetFont().GetHeight(), 1024);
							}
						}
					}
				}
				if (this.mTorchStageState == 12 && this.mCloakPoof.mFrameNum < (float)this.mCloakPoof.mLastFrameNum && this.mCloakPoof.mFrameNum > 0f)
				{
					this.mCloakPoof.Draw(g);
				}
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000277DC File Offset: 0x000259DC
		public virtual void DrawAboveBalls(SexyGraphics g)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DrawAboveBalls(g);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00027814 File Offset: 0x00025A14
		public virtual void DrawUnderBackground(SexyGraphics g)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DrawUnderBackground(g);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0002784C File Offset: 0x00025A4C
		public virtual void DrawFullScene(SexyGraphics g)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DrawFullScene(g);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00027884 File Offset: 0x00025A84
		public virtual void DrawFullSceneNoFrog(SexyGraphics g)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DrawFullSceneNoFrog(g);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000278BC File Offset: 0x00025ABC
		public virtual void DrawPriority(SexyGraphics g, int priority)
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DrawPriority(g, priority);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000278F4 File Offset: 0x00025AF4
		public virtual void DrawTorchLighting(SexyGraphics g)
		{
			if (Common.size<Torch>(this.mTorches) == 0)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_QUADRANT);
			float[] array = new float[]
			{
				1f,
				1f,
				-1f,
				-1f
			};
			float[] array2 = new float[]
			{
				1f,
				-1f,
				1f,
				-1f
			};
			int[] array3 = new int[]
			{
				Common._DS(-160),
				Common._DS(-160),
				this.mApp.mWidth + Common._DS(320) - imageByID.mWidth,
				this.mApp.mWidth + Common._DS(320) - imageByID.mWidth
			};
			int[] array4 = new int[]
			{
				default(int),
				this.mApp.mHeight - imageByID.mHeight,
				default(int),
				this.mApp.mHeight - imageByID.mHeight
			};
			for (int i = 0; i < Common.size<Torch>(this.mTorches); i++)
			{
				int mOverlayAlpha = this.mTorches[i].mOverlayAlpha;
				if (mOverlayAlpha != 0)
				{
					if (mOverlayAlpha != 255)
					{
						g.SetColorizeImages(true);
					}
					g.SetColor(255, 255, 255, mOverlayAlpha);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(array[i], array2[i]);
					g.DrawImageTransform(imageByID, this.mGlobalTranform, (float)(array3[i] + imageByID.mWidth / 2), (float)(array4[i] + imageByID.mHeight / 2));
					g.SetColorizeImages(false);
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00027A90 File Offset: 0x00025C90
		public virtual void DrawSkullPit(SexyGraphics g)
		{
			bool flag = false;
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				if (this.mEffects[i].DrawSkullPit(g, this.mHoleMgr))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.mHoleMgr.DrawRings(g);
				this.mHoleMgr.Draw(g);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00027AF0 File Offset: 0x00025CF0
		public virtual void DrawTunnel(SexyGraphics g, Image img, int x, int y, int w, int h)
		{
			if (this.mNum != 2147483647 || this.mZone != 4 || this.mBoss == null)
			{
				for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
				{
					if (!this.mEffects[i].DrawTunnel(g, img, x, y))
					{
						return;
					}
				}
			}
			g.DrawImage(img, x, y, w, h);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00027B58 File Offset: 0x00025D58
		public void DrawGauntletUI(SexyGraphics g)
		{
			Common._S(Common._M(465));
			Common._S(Common._M(22));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMELEFT);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF))) - Common._S(27), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF)) + Common._S(7));
			g.DrawImage(imageByID2, GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON))) - Common._S(27), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON)) + Common._S(7), new Rect(0, 0, (int)((float)imageByID2.mWidth * this.mCurGauntletMultPct), imageByID2.mHeight));
			g.DrawImage(imageByID3, GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME))), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME)));
			int num = (int)this.mGauntletTimeRedAmt;
			if (this.mGauntletTimeRedAmt > 0f)
			{
				g.SetColorizeImages(true);
				if (num > 128)
				{
					num = (255 - num) * 2;
				}
				else
				{
					num *= 2;
				}
				if (num > 255)
				{
					num = 255;
				}
				else if (num < 0)
				{
					num = 0;
				}
			}
			int num2 = this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime;
			if (num2 < 0)
			{
				num2 = 0;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_STROKE);
			g.SetFont(fontByID);
			g.SetColor(192, 230, 99);
			int num3 = Common._DS(Common._M(93)) + (Common._DS(Common._M1(35)) - g.GetFont().mHeight) / 2;
			g.WriteString(Common.UpdateToTimeStr(num2), GameApp.gApp.GetWideScreenAdjusted(Common._DS(Common._M(225))), num3, Common._DS(Common._M1(141)), 0);
			if (num > 0)
			{
				g.SetColor(255, 0, 0, num);
				g.WriteString(Common.UpdateToTimeStr(num2), GameApp.gApp.GetWideScreenAdjusted(Common._DS(Common._M(225))), num3, Common._DS(Common._M1(141)), 0);
			}
			g.SetColorizeImages(false);
			int wideScreenAdjusted = GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER)));
			int num4 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER));
			int wideScreenAdjusted2 = GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMELEFT)));
			g.DrawImage(imageByID4, wideScreenAdjusted, num4);
			g.DrawImage(imageByID5, wideScreenAdjusted2, Common._S(0));
			g.DrawImageMirror(imageByID4, wideScreenAdjusted + imageByID4.GetWidth() + Common._S(60), num4);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00027E4C File Offset: 0x0002604C
		public void InitEffects(Level copy_effects_from)
		{
			for (int i = 0; i < Common.size<string>(this.mEffectNames); i++)
			{
				Effect effect = this.mApp.GetLevelMgr().mEffectManager.GetEffect(this.mEffectNames[i], this.mId, copy_effects_from);
				if (effect != null)
				{
					this.mEffects.Add(effect);
				}
				this.mEffects[i].NukeParams();
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00027EB8 File Offset: 0x000260B8
		public void InitEffects()
		{
			this.InitEffects(null);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00027EC4 File Offset: 0x000260C4
		public void ResetEffects()
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].LoadResources();
				this.mEffects[i].Reset(this.mId);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00027F10 File Offset: 0x00026110
		public void ForceTreasure(int tnum)
		{
			this.mBoard.mCurTreasureNum = tnum;
			this.mBoard.mCurTreasure = this.mTreasurePoints[tnum];
			this.mBoard.mMinTreasureY = (this.mBoard.mMaxTreasureY = float.MaxValue);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00027F60 File Offset: 0x00026160
		public Ball GetBallById(int id)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				foreach (Ball ball in this.mCurveMgr[i].mBallList)
				{
					if (ball.GetId() == id)
					{
						return ball;
					}
				}
				foreach (Ball ball2 in this.mCurveMgr[i].mPendingBalls)
				{
					if (ball2.GetId() == id)
					{
						return ball2;
					}
				}
			}
			return null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0002802C File Offset: 0x0002622C
		public bool AllTorchesOut()
		{
			if (this.mTorchStageState != 6)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < Enumerable.Count<Torch>(this.mTorches); i++)
			{
				if (!this.mTorches[i].mActive)
				{
					num++;
				}
			}
			return num == Enumerable.Count<Torch>(this.mTorches) && Enumerable.Count<Torch>(this.mTorches) > 0;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00028091 File Offset: 0x00026291
		public bool IsFinalBossLevel()
		{
			return Enumerable.Count<Torch>(this.mTorches) > 0;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000280A4 File Offset: 0x000262A4
		public virtual void UpdateUI()
		{
			if (this.mZumaBarState >= Common._M(2) && !this.mBoard.GauntletMode() && this.mCurBarSize < this.mTargetBarSize)
			{
				this.mCurBarSize++;
			}
			if (this.mZumaBarState == 0)
			{
				this.mGingerMouthX += this.mGingerMouthVX;
				int num = (int)this.mGingerMouthXStart + Common._S(15);
				if (this.mGingerMouthX >= (float)num)
				{
					this.mGingerMouthX = (float)num;
					this.mZumaBarState++;
					this.mGingerMouthVX = 0f;
				}
			}
			else if (this.mZumaBarState == 1)
			{
				this.mGoldBallXOff += Common._S(0.75f);
				if ((this.mZumaBallPct += 0.05f) >= 1.2f)
				{
					this.mZumaBallPct = 1.2f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 2)
			{
				if ((this.mZumaBallPct -= 0.05f) <= 1f)
				{
					this.mZumaBallPct = 1f;
				}
				if (this.mGingerMouthVX == 0f && this.mZumaBallPct <= 1f)
				{
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 4)
			{
				this.mFredMouthX += this.mFredMouthVX;
				int num2 = Common._S(15);
				if (this.mFredMouthX <= this.mFredMouthXStart - (float)num2)
				{
					this.mFredMouthX = this.mFredMouthXStart - (float)num2;
					this.mFredMouthVX *= -1f;
					this.mZumaBarState++;
					this.mFredTongueVX = Common._S(-2.5f);
				}
			}
			else if (this.mZumaBarState == 5)
			{
				this.mFredTongueX += this.mFredTongueVX;
				int num3 = Common._S(36);
				if (this.mFredTongueX <= (float)(541 - num3))
				{
					this.mFredTongueX = (float)(541 - num3);
					this.mFredTongueVX *= -1f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 6)
			{
				this.mFredTongueX += this.mFredTongueVX;
				this.mGoldBallXOff += Common._S(2.5f);
				if ((this.mZumaBallPct += 0.05f) >= 1.2f)
				{
					this.mZumaBallPct = 1.2f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 7)
			{
				this.mFredTongueX += this.mFredTongueVX;
				if ((this.mZumaBallPct -= 0.05f) <= 1f)
				{
					this.mZumaBallPct = 1f;
				}
				this.mGoldBallXOff += Common._S(0.75f);
				if (this.mFredTongueX >= 541f)
				{
					this.mFredTongueX = 541f;
					this.mFredTongueVX = 0f;
				}
				if (this.mFredTongueX >= 541f)
				{
					this.mZumaBarState++;
					int num4 = (int)Common._S(2.5f);
					this.mFredMouthVX = (float)num4;
					this.mGingerMouthVX = (float)(-(float)num4);
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BAR_FULL));
				}
			}
			else if (this.mZumaBarState >= 8 && this.mZumaBarState < 12)
			{
				this.mFredMouthX += this.mFredMouthVX;
				this.mGingerMouthX += this.mGingerMouthVX;
				int num5 = 0;
				if (this.mFredMouthX >= this.mFredMouthXStart && this.mFredMouthVX > 0f)
				{
					num5++;
					this.mFredMouthX = this.mFredMouthXStart;
				}
				else if (this.mFredMouthX <= this.mFredMouthXStart - (float)Common._S(15) && this.mFredMouthVX < 0f)
				{
					num5++;
					this.mFredMouthX = this.mFredMouthXStart - (float)Common._S(15);
				}
				if (this.mGingerMouthX <= this.mGingerMouthXStart && this.mGingerMouthVX < 0f)
				{
					this.mGingerMouthX = this.mGingerMouthXStart;
					num5++;
				}
				else if (this.mGingerMouthX >= this.mGingerMouthXStart + (float)Common._S(15) && this.mGingerMouthVX > 0f)
				{
					this.mGingerMouthX = this.mGingerMouthXStart + (float)Common._S(15);
					num5++;
				}
				if (num5 == 2)
				{
					this.mZumaBarState++;
					this.mFredMouthVX *= -1f;
					this.mGingerMouthVX *= -1f;
				}
			}
			else if (this.mZumaBarState == 12)
			{
				if ((this.mBarLightness += 18f) >= 255f)
				{
					this.mBarLightness = 255f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 13)
			{
				if ((this.mBarLightness -= 18f) <= 0f)
				{
					this.mBarLightness = 0f;
					this.mZumaBarState++;
					this.mZumaPulseUCStart = this.mUpdateCount;
				}
			}
			else if (this.mZumaBarState == 14 && this.mBoard.GauntletMode())
			{
				this.mCurBarSize -= 2;
				if (this.mCurBarSize <= 0)
				{
					this.Reset();
					this.mCurBarSize = 0;
				}
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_GOLD_BALL);
			if (this.mCurBarSize != this.mTargetBarSize)
			{
				this.mZumaBallFrame = (this.mZumaBallFrame + 1) % imageByID.mNumCols;
			}
			if (!this.mHaveReachedTarget && !this.mBoard.GauntletMode() && this.ShouldUpdateZumaBar() && this.mNumCurves > 0 && this.mCurBarSize == 330 && this.mBoard.mScore >= this.mBoard.mScoreTarget && this.mBoss == null)
			{
				this.mZumaBarState = 4;
				this.mFredMouthVX = Common._S(-2.5f);
				if (!this.mBoard.IsEndless())
				{
					this.mHaveReachedTarget = true;
					for (int i = 0; i < this.mNumCurves; i++)
					{
						this.mCurveMgr[i].ZumaAchieved(true);
						if (!this.mBoard.DestroyAll())
						{
							this.mCurveMgr[i].DetonateBalls();
						}
					}
					this.mApp.mUserProfile.GetAdvModeVars().mNumZumasCurLevel++;
					this.mBoard.mNumZumaBalls = this.GetTotalBallsOnLevel();
				}
				if (!this.mBoard.DestroyAll())
				{
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_EXPLODE));
				}
			}
			int num6 = this.mBoard.mScoreTarget - this.mBoard.GetLevelBeginScore();
			if (num6 > 0)
			{
				int num7 = this.mBoard.mScoreTarget - this.mBoard.mScore;
				if (num7 < 0)
				{
					num7 = 0;
					if (this.mBoard.mLevelEndFrame == 0)
					{
						if (this.mBoard.GetNumBallColors() <= 2)
						{
							this.mBoard.mLevelEndFrame = this.mBoard.GetStateCount();
						}
					}
					else if (this.mBoard.GetStateCount() - this.mBoard.mLevelEndFrame == 3000)
					{
						for (int j = 0; j < this.mNumCurves; j++)
						{
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[0] = 500;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[1] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[2] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[3] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mAccelerationRate = 0.0003f;
						}
					}
				}
				if (this.mBoss == null && !this.mBoard.GauntletMode())
				{
					this.mTargetBarSize = 330 - 330 * num7 / num6;
				}
			}
			if (this.mBoss != null)
			{
				this.mTargetBarSize = (int)(330f - (1f - this.mBoss.GetHP() / 100f) * 330f);
			}
			if (this.mBoard.GauntletMode() && !this.DoingInitialPathHilite())
			{
				if (this.mGauntletCurTime < this.mApp.GetLevelMgr().mGauntletSessionLength)
				{
					this.mGauntletCurTime++;
					if (this.mGauntletCurTime % 100 == 0 && this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime <= 1100)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_CHALLENGE_COUNTDOWN));
					}
					if (GameApp.gDDS.SetGauntletTime(this.mGauntletCurTime))
					{
						this.UpdateChallengeModeDifficulty();
					}
					int num8 = this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime;
					if (num8 <= 1100 && num8 % 100 == 0)
					{
						this.mGauntletTimeRedAmt = 255f;
					}
				}
				if (this.mGauntletCurTime >= this.mApp.GetLevelMgr().mGauntletSessionLength && this.CurvesAtRest())
				{
					this.mBoard.EndGauntletMode(true);
					bool theAcedLevel = false;
					if (this.mBoard.mScore > this.mChallengeAcePoints)
					{
						theAcedLevel = true;
					}
					GameApp.gApp.ReportEndOfLevelMetrics(this.mBoard, true, theAcedLevel);
				}
			}
			if (this.mGauntletTimeRedAmt > 0f)
			{
				this.mGauntletTimeRedAmt -= Common._M(2.6f);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00028AAC File Offset: 0x00026CAC
		public virtual void UpdatePlaying()
		{
			if (this.mCurMultiplierTimeLeft > 0 && --this.mCurMultiplierTimeLeft == 0)
			{
				this.mBoard.GauntletMultiplierEnded();
			}
			if (this.mDoingPadHints)
			{
				if (Common.size<ZumaTip>(this.mBoard.mZumaTips) != 0)
				{
					return;
				}
				this.mDoingPadHints = false;
			}
			for (int i = 0; i < Common.size<Torch>(this.mTorches); i++)
			{
				this.mTorches[i].Update();
				if (!this.mTorches[i].mActive)
				{
					this.mTorches[i].mOverlayAlpha += Common._M(2);
					if (this.mTorches[i].mOverlayAlpha > 255)
					{
						this.mTorches[i].mOverlayAlpha = 255;
					}
				}
				else
				{
					this.mTorches[i].mOverlayAlpha -= Common._M(2);
					if (this.mTorches[i].mOverlayAlpha < 0)
					{
						this.mTorches[i].mOverlayAlpha = 0;
					}
				}
			}
			if (this.mBoard.GauntletMode())
			{
				float num = (float)this.mNumGauntletBallsBroke / (float)this.mGauntletCurNumForMult;
				float num2 = num - this.mCurGauntletMultPct;
				float num3 = this.mCurGauntletMultPct;
				if (this.mCurGauntletMultPct < num || num2 < -0.001f)
				{
					this.mCurGauntletMultPct += Common._M(0.01f);
					if (num3 < num && this.mCurGauntletMultPct > num)
					{
						this.mCurGauntletMultPct = num;
					}
					else if (this.mCurGauntletMultPct > 1f)
					{
						this.mCurGauntletMultPct = 0f;
					}
				}
			}
			bool flag = this.mHasReachedCruisingSpeed;
			this.mHasReachedCruisingSpeed = true;
			this.mAllCurvesAtRolloutPoint = true;
			Common._M(20f);
			bool flag2 = false;
			if (!this.IsFinalBossLevel() || this.mTorchStageState == 6)
			{
				for (int j = 0; j < this.mNumCurves; j++)
				{
					if (this.mCurveMgr[j].UpdatePlaying() && j + 1 < this.mNumCurves)
					{
						this.mCurveMgr[j + 1].mInitialPathHilite = true;
					}
					if (Common.size<PathSparkle>(this.mCurveMgr[j].mSparkles) > 0)
					{
						flag2 = true;
					}
					if (!this.mCurveMgr[j].HasReachedCruisingSpeed())
					{
						this.mHasReachedCruisingSpeed = false;
					}
					if (!this.mCurveMgr[j].HasReachedRolloutPoint())
					{
						this.mAllCurvesAtRolloutPoint = false;
					}
					if (this.mTempSpeedupTimer == 1)
					{
						this.mCurveMgr[j].mOverrideSpeed = -1f;
					}
					int farthestBallPercent = this.mCurveMgr[j].GetFarthestBallPercent();
					if (farthestBallPercent > this.mFurthestBallDistance)
					{
						this.mFurthestBallDistance = farthestBallPercent;
					}
				}
			}
			if (!flag && this.mHasReachedCruisingSpeed)
			{
				this.mApp.mSoundPlayer.Fade((this.mZone == 5) ? Res.GetSoundByID(ResID.SOUND_UNDERWATER_ROLLOUT) : Res.GetSoundByID(ResID.SOUND_ROLLING));
			}
			if (!Common.gAddBalls && !flag2 && !this.mBoard.mPreventBallAdvancement)
			{
				Common.gAddBalls = true;
				for (int k = 0; k < this.mNumCurves; k++)
				{
					this.mCurveMgr[k].mInitialPathHilite = false;
				}
			}
			if (this.mTempSpeedupTimer > 0)
			{
				this.mTempSpeedupTimer--;
			}
			if (this.mBoard.HasAchievedZuma() && this.mPostZumaTimeCounter > 0)
			{
				this.mPostZumaTimeCounter--;
				float num4 = (float)(this.mApp.GetLevelMgr().mPostZumaTime - this.mPostZumaTimeCounter) / (float)this.mApp.GetLevelMgr().mPostZumaTime;
				this.mPostZumaTimeSlowInc = num4 * this.mApp.GetLevelMgr().mPostZumaTimeSlowInc;
				this.mPostZumaTimeSpeedInc = num4 * this.mApp.GetLevelMgr().mPostZumaTimeSlowInc;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00028E76 File Offset: 0x00027076
		public virtual void UpdateBossIntro()
		{
			if (this.mBoss != null)
			{
				this.mBoss.Update();
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00028E8C File Offset: 0x0002708C
		public virtual void DrawUI(SexyGraphics g)
		{
			if (this.mBoss != null || this.IsFinalBossLevel())
			{
				g.mTransX = 0f;
				this.DrawBossUI(g);
				g.mTransX = (float)this.mApp.mBoardOffsetX;
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			this.mBarXOffset = (int)((float)imageByID.mWidth * 0.05f);
			this.DrawWoodPanel(g);
			this.mBoard.DrawRollerScore(g);
			if (this.mBoard.GauntletMode())
			{
				this.DrawGauntletUI(g);
			}
			else
			{
				this.DrawScoreFrame(g);
				this.DrawZumaBar(g);
				this.DrawFredAndGinger(g);
			}
			this.DrawTikiEnds(g);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00028F34 File Offset: 0x00027134
		public virtual void DrawGunPoints(SexyGraphics g)
		{
			if (this.mNumFrogPoints > 1)
			{
				for (int i = 0; i < this.mNumFrogPoints; i++)
				{
					if (this.mFrogImages[i].mImage != null)
					{
						int num = (this.mBoard.mMouseOverGunPos == i) ? 1 : 0;
						g.DrawImageCel(this.mFrogImages[i].mImage, Common._S(this.mFrogX[i]) - this.mFrogImages[i].mImage.GetCelWidth() / 2 + GameApp.gScreenShakeX, Common._S(this.mFrogY[i]) - this.mFrogImages[i].mImage.GetCelHeight() / 2 + GameApp.gScreenShakeY, num);
					}
				}
			}
			float num2 = (1f - this.mTorchDaisScale) * (float)Common._DS(Common._M(189));
			if (this.IsFinalBossLevel() || this.mTorchStageState >= 10)
			{
				for (int j = 0; j < Common.size<Torch>(this.mTorches); j++)
				{
					this.mTorches[j].DrawAbove(g);
				}
				if (this.mTorchStageAlpha > 0f)
				{
					g.SetColor(0, 0, 0, (int)Math.Min(255f, this.mTorchStageAlpha));
					g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_BASE);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_DIAS);
				Image imageByID3 = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
				Image imageByID4 = Res.GetImageByID(ResID.IMAGE_FROG_SHADOW);
				if (this.IsFinalBossLevel())
				{
					g.DrawImage(imageByID, Common._DS(690 - this.mApp.mOffset160X), Common._DS(330));
				}
				string[] array = new string[]
				{
					"start",
					"squish",
					"rattle"
				};
				int num3 = imageByID2.mWidth * (int)this.mTorchDaisScale;
				int num4 = imageByID2.mHeight * (int)this.mTorchDaisScale;
				int num5 = Common._DS(793 - this.mApp.mOffset160X);
				int num6 = Common._DS(395);
				num5 += (imageByID2.mWidth - num3) / 2;
				num6 += (imageByID2.mHeight - num4) / 2;
				if (this.mTorchStageState < 9)
				{
					g.DrawImage(imageByID2, num5 + Common._DS(this.mTorchStageShakeAmt), num6 - Common._DS(this.mTorchStageShakeAmt) + (int)num2, num3, num4);
				}
				for (int k = 0; k < Common.size<DaisRock>(this.mDaisRocks); k++)
				{
					DaisRock daisRock = this.mDaisRocks[k];
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)daisRock.mAlpha);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(daisRock.mSize, daisRock.mSize);
					float num7 = (255f - daisRock.mAlpha) / 255f * Common._M(2.5f) * 3.1415927f;
					this.mGlobalTranform.RotateRad(num7);
					g.DrawImageTransform(daisRock.mImg, this.mGlobalTranform, daisRock.mX, daisRock.mY);
					g.SetColorizeImages(false);
				}
				if (this.mTorchStageState < 4)
				{
					int num8;
					switch (this.mTorchStageState)
					{
					case 0:
						num8 = 2;
						break;
					case 1:
						num8 = 2;
						break;
					default:
						num8 = 2;
						break;
					}
					Composition composition = this.mTorchCompMgr.GetComposition(array[num8]);
					int num9 = composition.mUpdateCount;
					if (num9 >= composition.GetMaxDuration())
					{
						num9 = composition.GetMaxDuration() - 1;
					}
					if (this.mTorchStageState == 0)
					{
						num9 = 1;
					}
					if (num9 == 0)
					{
						num9 = 1;
					}
					CumulativeTransform cumulativeTransform = new CumulativeTransform();
					cumulativeTransform.mTrans.Translate(this.mTorchBossX, this.mTorchBossY);
					if (this.mTorchStageState == 3)
					{
						cumulativeTransform.mTrans.Scale(this.mTorchDaisScale, this.mTorchDaisScale);
						cumulativeTransform.mTrans.Translate(((float)Common._DS(composition.mWidth) - (float)Common._DS(composition.mWidth) * this.mTorchDaisScale) / 1.5f + (float)Common._DS(Common._M(80)) * (1f - this.mTorchDaisScale), ((float)Common._DS(composition.mHeight) - (float)Common._DS(composition.mHeight) * this.mTorchDaisScale) / Common._M1(1.5f) + num2);
					}
					composition.Draw(g, cumulativeTransform, num9, Common._DS(1f));
					Image imageByID5 = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG_ADD);
					Image imageByID6 = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG);
					for (int l = 0; l < Common.size<TorchLevelEgg>(this.mEggs); l++)
					{
						TorchLevelEgg torchLevelEgg = this.mEggs[l];
						int num10 = (int)(torchLevelEgg.mAlpha * this.mTorchDaisScale);
						if (num10 != 255)
						{
							g.SetColorizeImages(true);
						}
						g.SetColor(255, 255, 255, num10);
						g.SetDrawMode(1);
						g.DrawImageRotated(imageByID5, (int)(torchLevelEgg.mX + (float)Common._DS(Common._M(-30))), (int)(torchLevelEgg.mY + (float)Common._DS(Common._M1(-30))), (double)torchLevelEgg.mAngle);
						g.SetDrawMode(0);
						g.DrawImageRotated(imageByID6, (int)torchLevelEgg.mX, (int)torchLevelEgg.mY, (double)torchLevelEgg.mAngle);
						g.SetColorizeImages(false);
					}
					return;
				}
				if (this.mTorchStageState == 8 || this.mTorchStageState == 7)
				{
					float num11 = this.mTorchDaisScale * Common._M(0.5f);
					SexyTransform2D sexyTransform2D;
					sexyTransform2D = new SexyTransform2D(false);
					sexyTransform2D.Scale(this.mTorchDaisScale, this.mTorchDaisScale);
					sexyTransform2D.Translate((float)Common._S(Common._M(-2)), (float)Common._S(Common._M1(3)));
					sexyTransform2D.RotateRad(this.mFrog.GetAngle());
					sexyTransform2D.Translate((float)Common._S(Common._M(2)), (float)Common._S(Common._M1(-3)));
					float num12 = Common._DS(Common._M(20f)) * (1f - this.mTorchDaisScale);
					g.DrawImageMatrix(imageByID4, sexyTransform2D, imageByID4.GetCelRect(1), (float)Common._S(this.mFrog.GetCurX()) + (float)Common._DS(Common._M(-2)) * (1f - this.mTorchDaisScale) + (float)Common._DS(this.mTorchStageShakeAmt), (float)Common._S(this.mFrog.GetCurY()) + num12 + num2 - (float)Common._DS(this.mTorchStageShakeAmt));
					num12 = Common._DS(Common._M(30f)) * (1f - this.mTorchDaisScale);
					sexyTransform2D.LoadIdentity();
					sexyTransform2D.Scale(num11, num11);
					sexyTransform2D.RotateRad(this.mFrog.GetAngle());
					g.DrawImageMatrix(imageByID3, sexyTransform2D, (float)(Common._S(this.mFrog.GetCenterX()) - Common._DS(Common._M(2)) + Common._DS(this.mTorchStageShakeAmt)), (float)(Common._S(this.mFrog.GetCenterY()) - Common._DS(Common._M1(10))) + num12 + num2 - (float)Common._DS(this.mTorchStageShakeAmt));
					return;
				}
				if (this.mFrogFlyOff != null)
				{
					this.mFrogFlyOff.Draw(g);
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000296B8 File Offset: 0x000278B8
		public void DrawBossUI(SexyGraphics g)
		{
			GameApp gApp = GameApp.gApp;
			if (gApp.mBoard != null && !gApp.mBoard.mDrawBossUI)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_BOSSUI);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE);
			if (gApp.IsWideScreen())
			{
				g.DrawImage(imageByID, (int)((double)gApp.GetScreenRect().mWidth - (double)imageByID.GetWidth() * 1.5 - (double)imageByID2.GetWidth()), 0, (int)((float)imageByID.GetWidth() * 1.5f), imageByID.GetHeight());
				g.DrawImage(imageByID2, gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE))) - (gApp.GetScreenWidth() - gApp.mScreenBounds.mWidth), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE)));
				return;
			}
			g.DrawImage(imageByID, (int)((double)gApp.GetScreenRect().mWidth - (double)imageByID.GetWidth() * 1.5), 0, (int)((double)imageByID.GetWidth() * 1.5), imageByID.GetHeight());
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000297C4 File Offset: 0x000279C4
		public void DrawWoodPanel(SexyGraphics g)
		{
			int num = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_WOOD));
			int num2 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_WOOD));
			int x = num - this.mBarXOffset;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(x), num2);
			g.DrawImageMirror(imageByID, GameApp.gApp.GetWideScreenAdjusted(this.mBarXOffset), num2);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00029830 File Offset: 0x00027A30
		public void DrawScoreFrame(SexyGraphics g)
		{
			int num = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME)) + this.mBarXOffset;
			int num2 = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME);
			if (!GameApp.gApp.IsWideScreen())
			{
				num -= Common._S(10);
			}
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(num), num2);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00029898 File Offset: 0x00027A98
		public void DrawTikiEnds(SexyGraphics g)
		{
			if (!GameApp.gApp.IsWideScreen())
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFTFRAMESIDE);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(0), 0);
			g.DrawImage(imageByID2, GameApp.gApp.GetWidthAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE))) - (GameApp.gApp.GetScreenWidth() - GameApp.gApp.mScreenBounds.mWidth), 0);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00029918 File Offset: 0x00027B18
		public void DrawZumaBar(SexyGraphics g)
		{
			if (this.mTimer >= 0)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESSLITEWOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_LIGHT);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			g.DrawImage(imageByID, this.mZumaBarX, Common._S(9));
			this.SetZumaBarProgress();
			if (this.mZumaBarState < 2)
			{
				return;
			}
			this.DrawZumaBarProgress(g, imageByID2);
			this.DrawZumaBarProgressPulse(g);
			this.DrawZumaBarProgress(g, imageByID3);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0002998C File Offset: 0x00027B8C
		public void DrawFredAndGinger(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR)) - this.mBarXOffset), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR)), imageByID4.GetWidth(), imageByID.GetHeight());
			g.DrawImage(imageByID2, (int)this.mGingerMouthX + this.mBarXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW)) - Common._S(3));
			g.DrawImage(imageByID3, (int)this.mFredMouthX - this.mBarXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW)) - Common._S(2));
			this.DrawGoldBall(g);
			int x = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)) + this.mBarXOffset;
			int x2 = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)) - this.mBarXOffset;
			g.DrawImage(imageByID5, GameApp.gApp.GetWideScreenAdjusted(x), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)));
			g.DrawImage(imageByID6, GameApp.gApp.GetWideScreenAdjusted(x2), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)));
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00029AF4 File Offset: 0x00027CF4
		public void SetZumaBarProgress()
		{
			int num = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP).mWidth - this.mBarXOffset * 2;
			if (this.mZumaBarState >= 7 && this.mZumaBarState < 14)
			{
				this.mZumaBarWidth = num;
			}
			else
			{
				this.mZumaBarWidth = (int)((float)num * (float)this.mCurBarSize / 330f + (float)Common._S(8));
			}
			this.mZumaBarWidth = Math.Min(this.mZumaBarWidth, num);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00029B68 File Offset: 0x00027D68
		public void DrawZumaBarProgress(SexyGraphics g, Image inImage)
		{
			g.DrawImage(inImage, this.mZumaBarX, Common._S(9), new Rect(0, 0, this.mZumaBarWidth, inImage.mHeight));
			if (this.mZumaBarState < 12 || this.mZumaBarState > 13 || this.mBarLightness <= 0f)
			{
				return;
			}
			g.SetColorizeImages(true);
			g.SetDrawMode(1);
			g.SetColor(255, 255, 255, (int)this.mBarLightness);
			g.DrawImage(inImage, this.mZumaBarX, Common._S(9), new Rect(0, 0, this.mZumaBarWidth, inImage.mHeight));
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00029C20 File Offset: 0x00027E20
		public void DrawZumaBarProgressPulse(SexyGraphics g)
		{
			if (this.mZumaBarState < 14)
			{
				return;
			}
			g.PushState();
			g.SetDrawMode(1);
			int num = Common._M(0) + Common.GetAlphaFromUpdateCount(this.mUpdateCount - this.mZumaPulseUCStart, Common._M1(255));
			if (num > 255)
			{
				num = 255;
			}
			g.SetColorizeImages(true);
			g.SetColor(255, 255, 255, num);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_LIGHT);
			g.DrawImage(imageByID, this.mZumaBarX, Common._S(9), new Rect(0, 0, this.mZumaBarWidth, imageByID.mHeight));
			g.PopState();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00029CCC File Offset: 0x00027ECC
		public void DrawGoldBall(SexyGraphics g)
		{
			if (this.mTimer >= 0 || this.mZumaBarState >= 8 || this.mZumaBallPct <= 0f)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_GOLD_BALL);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			int num = (int)((float)imageByID.GetCelHeight() * this.mZumaBallPct);
			int num2 = (int)((float)imageByID.GetCelWidth() * this.mZumaBallPct);
			int num3 = this.mZumaBarX - Common._S(20) + this.mZumaBarWidth - imageByID.mHeight / 2 + Common._S((int)this.mGoldBallXOff);
			if (num3 < this.mZumaBarX)
			{
				num3 = this.mZumaBarX;
			}
			Rect rect;
			rect = new Rect(num3, Common._S(9) + (imageByID2.mHeight - num2) / 2, num, num2);
			g.DrawImageCel(imageByID, rect, this.mZumaBallFrame);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00029D9C File Offset: 0x00027F9C
		public virtual int GetFarthestBallPercent(ref int farthest_curve, bool ignore_gaps)
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				int farthestBallPercent = this.mCurveMgr[i].GetFarthestBallPercent(ignore_gaps);
				if (farthestBallPercent > num)
				{
					farthest_curve = i;
					num = farthestBallPercent;
				}
			}
			return num;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00029DD8 File Offset: 0x00027FD8
		public virtual int GetFarthestBallPercent()
		{
			int num = 0;
			return this.GetFarthestBallPercent(ref num, true);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00029DF0 File Offset: 0x00027FF0
		public virtual void NukeEffects()
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].DeleteResources();
			}
			this.mEffects.Clear();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00029E30 File Offset: 0x00028030
		public virtual void BulletFired(Bullet b)
		{
			for (int i = 0; i < Enumerable.Count<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].BulletFired(b);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00029E68 File Offset: 0x00028068
		public virtual void BulletHit(Bullet b)
		{
			for (int i = 0; i < Enumerable.Count<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].BulletHit(b);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00029EA0 File Offset: 0x000280A0
		public virtual void ReactivateWalls(int wall_id)
		{
			for (int i = 0; i < Enumerable.Count<Wall>(this.mWalls); i++)
			{
				if (this.mWalls[i].mId == wall_id || wall_id == -1)
				{
					this.mWalls[i].mStrength = this.mWalls[i].mOrgStrength;
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00029EFD File Offset: 0x000280FD
		public virtual void ReactivateWalls()
		{
			this.ReactivateWalls(-1);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00029F08 File Offset: 0x00028108
		public virtual bool CompactCurves()
		{
			if (!this.CanCompactCurves())
			{
				return false;
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].CompactCurve();
			}
			return true;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00029F40 File Offset: 0x00028140
		public virtual bool CanCompactCurves()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (!this.mCurveMgr[i].CanCompact())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00029F70 File Offset: 0x00028170
		public virtual void SetupHiddenHoles()
		{
			if (this.mHoleMgr.GetNumHoles() < 2)
			{
				return;
			}
			int num = 0;
			HoleInfo hole;
			while ((hole = this.mHoleMgr.GetHole(num)) != null)
			{
				if (!hole.mVisible)
				{
					int num2 = 0;
					Rect rect;
					rect = new Rect(hole.mX, hole.mY, 96, 96);
					rect.Inflate(-4, -4);
					HoleInfo hole2;
					while ((hole2 = this.mHoleMgr.GetHole(num2)) != null)
					{
						if (!hole2.mVisible)
						{
							num2++;
						}
						else
						{
							Rect rect2;
							rect2 = new Rect(hole2.mX, hole2.mY, 96, 96);
							rect2.Inflate(-4, -4);
							if (rect2.Intersects(rect))
							{
								hole.mShared.Add(num2);
							}
							num2++;
						}
					}
				}
				num++;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0002A03A File Offset: 0x0002823A
		public virtual void PlayerLostLevel()
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0002A03C File Offset: 0x0002823C
		public void DeactivateLightningEffects()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].ElectrifyBalls(-1, false);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0002A06C File Offset: 0x0002826C
		public bool HasPowerup(PowerType p)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].HasPowerup(p))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0002A09D File Offset: 0x0002829D
		public virtual int GetRandomPendingBallColor(int max_curve_colors)
		{
			return MathUtils.SafeRand() % max_curve_colors;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0002A0A6 File Offset: 0x000282A6
		public virtual float GetRandomFrogBulletColor(int max_curve_colors, int color_num)
		{
			return 1f / (float)max_curve_colors;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0002A0B0 File Offset: 0x000282B0
		public virtual Ball GetBallAtXY(int x, int y)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				foreach (Ball ball in this.mCurveMgr[i].mBallList)
				{
					if (ball.Contains(x, y))
					{
						return ball;
					}
				}
			}
			return null;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0002A128 File Offset: 0x00028328
		public Ball GetRandomBall()
		{
			int num = MathUtils.SafeRand() % this.mNumCurves;
			if (this.mCurveMgr[num].mBallList.Count > 3)
			{
				int num2 = MathUtils.SafeRand() % (this.mCurveMgr[num].mBallList.Count - 2);
				Ball ball = this.mCurveMgr[num].mBallList[num2];
				if (ball.GetPowerType() == PowerType.PowerType_Max)
				{
					return ball;
				}
			}
			return null;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0002A195 File Offset: 0x00028395
		public virtual void ParseUnknownAttribute(string key, string val)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0002A198 File Offset: 0x00028398
		public virtual void CopyFrom(Level src)
		{
			for (int i = 0; i < this.mHoleMgr.GetNumHoles(); i++)
			{
				HoleInfo hole = this.mHoleMgr.GetHole(i);
				hole.mCurve = this.mCurveMgr[hole.mCurveNum];
			}
			this.mApp = GameApp.gApp;
			this.mBoard = this.mApp.GetBoard();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0002A1F8 File Offset: 0x000283F8
		public int GetTotalBallsOnLevel()
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				num += Enumerable.Count<Ball>(this.mCurveMgr[i].mBallList);
				num += Enumerable.Count<Ball>(this.mCurveMgr[i].mPendingBalls);
			}
			return num;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0002A244 File Offset: 0x00028444
		public int GetMaxBallsForLevel()
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				num += this.mCurveMgr[i].mCurveDesc.mVals.mNumBalls;
			}
			return num;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0002A280 File Offset: 0x00028480
		public bool CheckFruitActivation(int curve_num)
		{
			if (this.mBoard.mPreventBallAdvancement)
			{
				return false;
			}
			int num = this.mBoard.GauntletMode() ? this.mApp.GetLevelMgr().mGauntletTFreq : this.mTreasureFreq;
			if (!Board.gForceTreasure && (this.mBoard.mCurTreasure != null || MathUtils.SafeRand() % num != 0))
			{
				return false;
			}
			List<int> list = new List<int>();
			int num2;
			int num3;
			if (curve_num == -1)
			{
				num2 = 0;
				num3 = this.mNumCurves;
			}
			else
			{
				num3 = curve_num;
				num2 = curve_num;
			}
			for (int i = num2; i < num3; i++)
			{
				int farthestBallPercent = this.mCurveMgr[i].GetFarthestBallPercent();
				for (int j = 0; j < Enumerable.Count<TreasurePoint>(this.mTreasurePoints); j++)
				{
					TreasurePoint treasurePoint = this.mTreasurePoints[j];
					if (treasurePoint.mCurveDist[i] > 0 && farthestBallPercent >= treasurePoint.mCurveDist[i])
					{
						list.Add(j);
					}
				}
			}
			if (Enumerable.Count<int>(list) == 0)
			{
				return false;
			}
			int num4 = MathUtils.SafeRand() % Enumerable.Count<int>(list);
			this.mBoard.mCurTreasureNum = list[num4];
			this.mBoard.mCurTreasure = this.mTreasurePoints[list[num4]];
			this.mBoard.mMinTreasureY = (this.mBoard.mMaxTreasureY = float.MaxValue);
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0002A3D0 File Offset: 0x000285D0
		public bool CurvesAtRest()
		{
			if (this.mBoard.HasFiredBullets() || this.mBoard.GetGun().IsFiring())
			{
				return false;
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (!this.mCurveMgr[i].AtRest())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0002A421 File Offset: 0x00028621
		public virtual void MadeCombo(int combo_size)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0002A423 File Offset: 0x00028623
		public virtual void MadeGapShot(int gap_size)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0002A425 File Offset: 0x00028625
		public virtual void MadeConsecutiveClear(int clear_size)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0002A427 File Offset: 0x00028627
		public virtual void ClearedInARowBonus()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0002A429 File Offset: 0x00028629
		public virtual void AllBallsDestroyed()
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0002A42B File Offset: 0x0002862B
		public virtual void BallExploded(int ball_type)
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0002A42D File Offset: 0x0002862D
		public virtual bool ShouldUpdateZumaBar()
		{
			return true;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0002A430 File Offset: 0x00028630
		public virtual bool AllowPointsFromBalls()
		{
			return true;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0002A433 File Offset: 0x00028633
		public virtual bool CanAdvanceBalls()
		{
			return this.mBoss == null || this.mBoss.CanAdvanceBalls();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0002A44A File Offset: 0x0002864A
		public virtual bool BeatLevelOverride()
		{
			return false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0002A450 File Offset: 0x00028650
		public virtual void TemporarilySpeedUpCurves(float max_speed, int time_count)
		{
			this.mTempSpeedupTimer = time_count;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].mOverrideSpeed = max_speed;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0002A483 File Offset: 0x00028683
		public virtual void BallCreatedCallback(Ball b, int num_created)
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0002A485 File Offset: 0x00028685
		public virtual void MouseDown(int x, int y, int cc)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0002A487 File Offset: 0x00028687
		public virtual void ChangedPad(int new_pad)
		{
			if (!this.mDoingPadHints)
			{
				return;
			}
			this.mBoard.mZumaTips[0] = null;
			this.mBoard.mZumaTips.RemoveAt(0);
			this.mBoard.MarkDirty();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0002A4C0 File Offset: 0x000286C0
		public virtual int GetFrogReloadType()
		{
			if (!this.mApp.mUserProfile.HasSeenHint(ZumaProfile.FIRST_SHOT_HINT) && !this.mBoard.GauntletMode() && this.mNum == 1 && this.mZone == 1)
			{
				return 2;
			}
			if (this.mBoss != null)
			{
				return this.mBoss.GetFrogReloadType();
			}
			return -1;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0002A51A File Offset: 0x0002871A
		public virtual void PlayerStartedFiring()
		{
			if (this.mBoss != null)
			{
				this.mBoss.PlayerStartedFiring();
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0002A530 File Offset: 0x00028730
		public float GetPowerIncPct()
		{
			if (this.mBoard.IronFrogMode() || this.mBoard.GauntletMode())
			{
				return 0f;
			}
			float num = (float)this.mCurBarSize / 330f;
			if (num >= this.mApp.GetLevelMgr().mPowerupIncAtZumaPct)
			{
				return this.mApp.GetLevelMgr().mPowerIncPct;
			}
			return 0f;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0002A594 File Offset: 0x00028794
		public void IncNumBallsExploded(int val)
		{
			this.mApp.mUserProfile.mBallsBroken++;
			if (!this.mBoard.GauntletMode())
			{
				return;
			}
			this.mNumGauntletBallsBroke += val;
			if (this.mNumGauntletBallsBroke >= this.mGauntletCurNumForMult)
			{
				this.mNumGauntletBallsBroke %= this.mGauntletCurNumForMult;
				int num = Common.Rand() % this.mNumCurves;
				this.mCurveMgr[num].mNumMultBallsToSpawn++;
				this.mGauntletMultipliersEarned++;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0002A626 File Offset: 0x00028826
		public virtual bool CanUpdate()
		{
			return true;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0002A62C File Offset: 0x0002882C
		public int GetOwningCurve(Ball b)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].HasBall(b))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0002A660 File Offset: 0x00028860
		public void UpdateEffects()
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].Update();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0002A694 File Offset: 0x00028894
		public virtual bool CanRotateFrog()
		{
			return this.mEndSequence != 2 || ((this.mTorchStageState == 13 || this.mTorchStageState == -1) && this.mBoss.GetHP() > 0f);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0002A6C8 File Offset: 0x000288C8
		public virtual bool CanFireBall()
		{
			int num = 0;
			int num2 = 0;
			while (num2 < this.mNumCurves && this.mCurveMgr[num2].IsWinning())
			{
				num2++;
				num++;
			}
			return (!this.mBoard.GauntletMode() || this.mGauntletCurTime < this.mApp.GetLevelMgr().mGauntletSessionLength) && num != this.mNumCurves && (!this.mDoTorchCrap || this.mHasDoneTorchCrap) && (this.mApp.mUserProfile.HasSeenHint(ZumaProfile.FIRST_SHOT_HINT) || ((this.HasReachedCruisingSpeed() || this.mBoard.GauntletMode() || (this.mBoss != null && this.mBoss.AllowFrogToFire())) && (Enumerable.Count<ZumaTip>(this.mBoard.mZumaTips) == 0 || (this.mBoard.mZumaTips[0].mId == ZumaProfile.FIRST_SHOT_HINT && this.mFrog.GetAngle() >= Common._M(4.504f) && this.mFrog.GetAngle() <= Common._M1(4.9049f)))));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0002A7E6 File Offset: 0x000289E6
		public virtual bool CanUseKeyboard()
		{
			return true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0002A7E9 File Offset: 0x000289E9
		public virtual bool CanSwapBalls()
		{
			return true;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0002A7EC File Offset: 0x000289EC
		public virtual Level Instantiate()
		{
			Level level = this.Clone();
			level.mHoleMgr = null;
			return level;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0002A808 File Offset: 0x00028A08
		public virtual void SetFrog(Gun g)
		{
			this.mFrog = g;
			if (this.mBoss != null)
			{
				this.mBoss.FrogInitialized(g);
			}
			if (this.mSecondaryBoss != null)
			{
				this.mSecondaryBoss.FrogInitialized(g);
			}
			if (this.mMoveType != 0 && this.mCurveMgr[0] != null)
			{
				int endPoint = this.mCurveMgr[0].mWayPointMgr.GetEndPoint();
				float num;
				float num2;
				this.mCurveMgr[0].GetXYFromWaypoint(endPoint, out num, out num2);
				if (this.mMoveType == 1)
				{
					if (num2 < (float)g.GetCenterY())
					{
						g.SetDestAngle(-3.14159f);
						return;
					}
					g.SetDestAngle(0f);
					return;
				}
				else
				{
					if (num < (float)g.GetCenterX())
					{
						g.SetDestAngle(-1.570795f);
						return;
					}
					g.SetDestAngle(1.570795f);
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		public virtual void SyncState(DataSync sync)
		{
			this.SyncWalls(sync, true);
			bool flag = this.mBoss != null;
			bool flag2 = this.mBoss == this.mSecondaryBoss;
			sync.SyncBoolean(ref flag);
			sync.SyncBoolean(ref flag2);
			sync.SyncLong(ref this.mInvertMouseTimer);
			sync.SyncBoolean(ref this.mCanDrawBoss);
			if (flag)
			{
				bool flag3 = false;
				if (this.mBoard.ShouldBypassFinalSequenceOnLoad())
				{
					flag3 = true;
				}
				sync.SyncBoolean(ref flag3);
				if (!flag3)
				{
					if (sync.isWrite())
					{
						this.mBoss.SyncState(sync);
					}
					else
					{
						if (flag2)
						{
							this.mBoss = this.mSecondaryBoss;
						}
						this.mBoss.SyncState(sync);
					}
				}
			}
			sync.SyncFloat(ref this.mTorchDaisScale);
			sync.SyncLong(ref this.mTorchStageState);
			sync.SyncLong(ref this.mTorchStageTimer);
			sync.SyncFloat(ref this.mTorchStageAlpha);
			if (sync.isRead())
			{
				if (this.mTorchStageState >= 9 && this.mTorchStageState < 13)
				{
					this.mTorchStageState = 13;
					this.mBoard.mPreventBallAdvancement = false;
					this.mTorchStageTimer = 0;
					this.mTorchDaisScale = 0f;
					this.mCanDrawBoss = true;
				}
				this.mTorches.Clear();
				Buffer buffer = sync.GetBuffer();
				int num = (int)buffer.ReadLong();
				for (int i = 0; i < num; i++)
				{
					Torch torch = new Torch();
					torch.SyncState(sync);
					this.mTorches.Add(torch);
				}
				if (this.mTorchStageState != -1 && this.mTorchStageState < 6)
				{
					this.InitFinalBossLevel();
				}
				else if (this.mTorchStageState == 6)
				{
					this.mBoard.mPreventBallAdvancement = false;
					this.mDoTorchCrap = false;
					this.mHasDoneTorchCrap = true;
					this.mTorchDaisScale = 1f;
					this.mTorchTextAlpha = 0f;
				}
			}
			else
			{
				Buffer buffer2 = sync.GetBuffer();
				buffer2.WriteLong((long)this.mTorches.Count);
				for (int j = 0; j < this.mTorches.Count; j++)
				{
					this.mTorches[j].SyncState(sync);
				}
			}
			sync.SyncBoolean(ref this.mDoTorchCrap);
			sync.SyncBoolean(ref this.mHasDoneTorchCrap);
			sync.SyncFloat(ref this.mTorchTextAlpha);
			sync.SyncLong(ref this.mFurthestBallDistance);
			sync.SyncLong(ref this.mCurFrogPoint);
			sync.SyncLong(ref this.mTempSpeedupTimer);
			sync.SyncBoolean(ref this.mHaveReachedTarget);
			sync.SyncFloat(ref this.mBarLightness);
			sync.SyncFloat(ref this.mZumaBallPct);
			sync.SyncLong(ref this.mZumaBarState);
			sync.SyncFloat(ref this.mGoldBallXOff);
			sync.SyncFloat(ref this.mGingerMouthX);
			sync.SyncFloat(ref this.mGingerMouthVX);
			sync.SyncFloat(ref this.mFredMouthX);
			sync.SyncFloat(ref this.mFredMouthVX);
			sync.SyncFloat(ref this.mFredTongueX);
			sync.SyncFloat(ref this.mFredTongueVX);
			sync.SyncLong(ref this.mCurBarSize);
			sync.SyncLong(ref this.mTargetBarSize);
			this.SyncWalls(sync, true);
			for (int k = 0; k < this.mNumCurves; k++)
			{
				this.mCurveMgr[k].SyncState(sync);
			}
			sync.SyncBoolean(ref this.m_canGetAchievementNoMove);
			sync.SyncBoolean(ref this.m_canGetAchievementNoJump);
			sync.SyncLong(ref this.m_OriginX);
			sync.SyncLong(ref this.m_OriginY);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0002AC18 File Offset: 0x00028E18
		private void SyncWalls(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mWalls.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Wall wall = new Wall();
					wall.SyncState(sync);
					this.mWalls.Add(wall);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mWalls.Count);
			foreach (Wall wall2 in this.mWalls)
			{
				wall2.SyncState(sync);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0002ACCC File Offset: 0x00028ECC
		public bool AllCurvesAtRolloutPoint()
		{
			return this.mAllCurvesAtRolloutPoint;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		public bool HasReachedCruisingSpeed()
		{
			return this.mHasReachedCruisingSpeed;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0002ACDC File Offset: 0x00028EDC
		public float GetBarPercent()
		{
			return (float)this.mCurBarSize / (float)this.mTargetBarSize;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		public int GetBossBombDelay()
		{
			if (this.mBoss == null)
			{
				return 0;
			}
			BossShoot bossShoot = this.mBoss as BossShoot;
			if (bossShoot == null)
			{
				return 0;
			}
			return bossShoot.mBombAppearDelay;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0002AD1E File Offset: 0x00028F1E
		public void ProximityBombActivated(float x, float y, int radius)
		{
			if (this.mBoss != null && this.mBoss.IsHitByExplosion(x, y, radius))
			{
				this.mBoss.ProximityBombActivated(x, y, radius);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0002AD48 File Offset: 0x00028F48
		public void UserDied()
		{
			for (int i = 0; i < Common.size<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].UserDied();
			}
		}

		// Token: 0x04000991 RID: 2449
		public const int TARGET_BAR_SIZE = 330;

		// Token: 0x04000992 RID: 2450
		public const int FRED_TONGUE_X = 541;

		// Token: 0x04000993 RID: 2451
		public const int STARTING_TORCH_TEXT_ALPHA = 700;

		// Token: 0x04000994 RID: 2452
		protected float[] mCloakedBossTextAlpha = new float[3];

		// Token: 0x04000995 RID: 2453
		public List<DaisRock> mDaisRocks = new List<DaisRock>();

		// Token: 0x04000996 RID: 2454
		public List<TorchLevelEgg> mEggs = new List<TorchLevelEgg>();

		// Token: 0x04000997 RID: 2455
		public List<Wall> mMovingWallDefaults = new List<Wall>();

		// Token: 0x04000998 RID: 2456
		public List<Effect> mEffects = new List<Effect>();

		// Token: 0x04000999 RID: 2457
		protected bool mAllCurvesAtRolloutPoint;

		// Token: 0x0400099A RID: 2458
		protected bool mHasReachedCruisingSpeed;

		// Token: 0x0400099B RID: 2459
		protected float mCurGauntletMultPct;

		// Token: 0x0400099C RID: 2460
		protected Transform mGlobalTranform = new Transform();

		// Token: 0x0400099D RID: 2461
		private CompositionMgr mTorchCompMgr;

		// Token: 0x0400099E RID: 2462
		public float mTorchBossX;

		// Token: 0x0400099F RID: 2463
		public float mTorchBossY;

		// Token: 0x040009A0 RID: 2464
		public float mTorchBossDestX;

		// Token: 0x040009A1 RID: 2465
		public float mTorchBossDestY;

		// Token: 0x040009A2 RID: 2466
		public float mTorchBossVX;

		// Token: 0x040009A3 RID: 2467
		public float mTorchBossVY;

		// Token: 0x040009A4 RID: 2468
		public float mTorchDaisScale;

		// Token: 0x040009A5 RID: 2469
		public int mChallengePoints;

		// Token: 0x040009A6 RID: 2470
		public int mChallengeAcePoints;

		// Token: 0x040009A7 RID: 2471
		public int mCloakClapFrame;

		// Token: 0x040009A8 RID: 2472
		public PIEffect mCloakPoof;

		// Token: 0x040009A9 RID: 2473
		public FrogFlyOff mFrogFlyOff;

		// Token: 0x040009AA RID: 2474
		public List<PowerupRegion> mPowerupRegions = new List<PowerupRegion>();

		// Token: 0x040009AB RID: 2475
		public List<Torch> mTorches = new List<Torch>();

		// Token: 0x040009AC RID: 2476
		public List<string> mEffectNames = new List<string>();

		// Token: 0x040009AD RID: 2477
		public List<EffectParams> mEffectParams = new List<EffectParams>();

		// Token: 0x040009AE RID: 2478
		public List<TreasurePoint> mTreasurePoints = new List<TreasurePoint>();

		// Token: 0x040009AF RID: 2479
		public string mId = "";

		// Token: 0x040009B0 RID: 2480
		public string mDisplayName = "";

		// Token: 0x040009B1 RID: 2481
		public int mDisplayNameId = -1;

		// Token: 0x040009B2 RID: 2482
		public string mPopupText = "";

		// Token: 0x040009B3 RID: 2483
		public string mImagePath = "";

		// Token: 0x040009B4 RID: 2484
		public string mSoundscapeId = "";

		// Token: 0x040009B5 RID: 2485
		public MirrorType mMirrorType;

		// Token: 0x040009B6 RID: 2486
		public CurveMgr[] mCurveMgr = new CurveMgr[4];

		// Token: 0x040009B7 RID: 2487
		public float[] mCurveSkullAngleOverrides = new float[4];

		// Token: 0x040009B8 RID: 2488
		public HoleMgr mHoleMgr;

		// Token: 0x040009B9 RID: 2489
		public List<TunnelData> mTunnelData = new List<TunnelData>();

		// Token: 0x040009BA RID: 2490
		public List<Wall> mWalls = new List<Wall>();

		// Token: 0x040009BB RID: 2491
		public Boss mBoss;

		// Token: 0x040009BC RID: 2492
		public Boss mSecondaryBoss;

		// Token: 0x040009BD RID: 2493
		public Boss mOrgBoss;

		// Token: 0x040009BE RID: 2494
		public Gun mFrog;

		// Token: 0x040009BF RID: 2495
		public Board mBoard;

		// Token: 0x040009C0 RID: 2496
		public GameApp mApp;

		// Token: 0x040009C1 RID: 2497
		public SharedImageRef mBossIntroBG;

		// Token: 0x040009C2 RID: 2498
		public string mPreviewText = "";

		// Token: 0x040009C3 RID: 2499
		public int mPreviewTextId = -1;

		// Token: 0x040009C4 RID: 2500
		public LillyPadImageInfo[] mFrogImages = new LillyPadImageInfo[5];

		// Token: 0x040009C5 RID: 2501
		public bool mCanDrawBoss;

		// Token: 0x040009C6 RID: 2502
		public int mTorchStageState;

		// Token: 0x040009C7 RID: 2503
		public int mTorchStageTimer;

		// Token: 0x040009C8 RID: 2504
		public float mTorchStageAlpha;

		// Token: 0x040009C9 RID: 2505
		public int mTorchStageShakeAmt;

		// Token: 0x040009CA RID: 2506
		public int mEndSequence;

		// Token: 0x040009CB RID: 2507
		public int mIndex;

		// Token: 0x040009CC RID: 2508
		public bool mOffscreenClearBonus;

		// Token: 0x040009CD RID: 2509
		public bool mNoBackground;

		// Token: 0x040009CE RID: 2510
		public bool mFinalLevel;

		// Token: 0x040009CF RID: 2511
		public bool mBGFromPSD;

		// Token: 0x040009D0 RID: 2512
		public float mPotPct;

		// Token: 0x040009D1 RID: 2513
		public float mFireSpeed;

		// Token: 0x040009D2 RID: 2514
		public float mHurryToRolloutAmt;

		// Token: 0x040009D3 RID: 2515
		public bool mDoTorchCrap;

		// Token: 0x040009D4 RID: 2516
		public bool mHasDoneTorchCrap;

		// Token: 0x040009D5 RID: 2517
		public float mTorchTextAlpha;

		// Token: 0x040009D6 RID: 2518
		public bool mDrawCurves;

		// Token: 0x040009D7 RID: 2519
		public bool mSuckMode;

		// Token: 0x040009D8 RID: 2520
		public bool mIsEndless;

		// Token: 0x040009D9 RID: 2521
		public bool mLoopAtEnd;

		// Token: 0x040009DA RID: 2522
		public bool mDoingPadHints;

		// Token: 0x040009DB RID: 2523
		public bool mNoFlip;

		// Token: 0x040009DC RID: 2524
		public bool mSliderEdgeRotate;

		// Token: 0x040009DD RID: 2525
		public bool mIronFrog;

		// Token: 0x040009DE RID: 2526
		public int mReloadDelay;

		// Token: 0x040009DF RID: 2527
		public int mNumCurves;

		// Token: 0x040009E0 RID: 2528
		public int mNumFrogPoints;

		// Token: 0x040009E1 RID: 2529
		public int mCurFrogPoint;

		// Token: 0x040009E2 RID: 2530
		public int[] mFrogX = new int[5];

		// Token: 0x040009E3 RID: 2531
		public int[] mFrogY = new int[5];

		// Token: 0x040009E4 RID: 2532
		public int mBarWidth;

		// Token: 0x040009E5 RID: 2533
		public int mBarHeight;

		// Token: 0x040009E6 RID: 2534
		public int mTreasureFreq;

		// Token: 0x040009E7 RID: 2535
		public int mParTime;

		// Token: 0x040009E8 RID: 2536
		public int mMoveType;

		// Token: 0x040009E9 RID: 2537
		public int mMoveSpeed;

		// Token: 0x040009EA RID: 2538
		public int mUpdateCount;

		// Token: 0x040009EB RID: 2539
		public int mTimer;

		// Token: 0x040009EC RID: 2540
		public int mTimeToComplete;

		// Token: 0x040009ED RID: 2541
		public int mInvertMouseTimer;

		// Token: 0x040009EE RID: 2542
		public int mMaxInvertMouseTimer;

		// Token: 0x040009EF RID: 2543
		public int mTempSpeedupTimer;

		// Token: 0x040009F0 RID: 2544
		public int mBossFreezePowerupTime;

		// Token: 0x040009F1 RID: 2545
		public int mFrogShieldPowerupCount;

		// Token: 0x040009F2 RID: 2546
		public int mStartingGauntletLevel;

		// Token: 0x040009F3 RID: 2547
		public int mTorchTimer;

		// Token: 0x040009F4 RID: 2548
		public int mFurthestBallDistance;

		// Token: 0x040009F5 RID: 2549
		public int mIntroTorchDelay;

		// Token: 0x040009F6 RID: 2550
		public int mIntroTorchIndex;

		// Token: 0x040009F7 RID: 2551
		public int mGauntletCurTime;

		// Token: 0x040009F8 RID: 2552
		public int mGauntletMultipliersEarned;

		// Token: 0x040009F9 RID: 2553
		public int mNumGauntletBallsBroke;

		// Token: 0x040009FA RID: 2554
		public int mGauntletCurNumForMult;

		// Token: 0x040009FB RID: 2555
		public int mCurMultiplierTimeLeft;

		// Token: 0x040009FC RID: 2556
		public int mMaxMultiplierTime;

		// Token: 0x040009FD RID: 2557
		public float mGauntletTimeRedAmt;

		// Token: 0x040009FE RID: 2558
		public int mZone;

		// Token: 0x040009FF RID: 2559
		public int mNum;

		// Token: 0x04000A00 RID: 2560
		public int mPostZumaTimeCounter;

		// Token: 0x04000A01 RID: 2561
		public float mPostZumaTimeSpeedInc;

		// Token: 0x04000A02 RID: 2562
		public float mPostZumaTimeSlowInc;

		// Token: 0x04000A03 RID: 2563
		public string mBossBGID = "";

		// Token: 0x04000A04 RID: 2564
		public int m_OriginX = -1;

		// Token: 0x04000A05 RID: 2565
		public int m_OriginY = -1;

		// Token: 0x04000A06 RID: 2566
		public bool m_canGetAchievementNoMove;

		// Token: 0x04000A07 RID: 2567
		public bool m_canGetAchievementNoJump;

		// Token: 0x04000A08 RID: 2568
		public bool mHaveReachedTarget;

		// Token: 0x04000A09 RID: 2569
		public int mCurBarSize;

		// Token: 0x04000A0A RID: 2570
		public int mCurBarSizeInc;

		// Token: 0x04000A0B RID: 2571
		public int mTargetBarSize;

		// Token: 0x04000A0C RID: 2572
		public int mZumaBallFrame;

		// Token: 0x04000A0D RID: 2573
		public float mBarLightness;

		// Token: 0x04000A0E RID: 2574
		public int mZumaPulseUCStart;

		// Token: 0x04000A0F RID: 2575
		public float mGingerMouthX;

		// Token: 0x04000A10 RID: 2576
		public float mGingerMouthVX;

		// Token: 0x04000A11 RID: 2577
		public float mGingerMouthXStart;

		// Token: 0x04000A12 RID: 2578
		public float mFredMouthX;

		// Token: 0x04000A13 RID: 2579
		public float mFredMouthVX;

		// Token: 0x04000A14 RID: 2580
		public float mFredMouthXStart;

		// Token: 0x04000A15 RID: 2581
		public float mFredTongueX;

		// Token: 0x04000A16 RID: 2582
		public float mFredTongueVX;

		// Token: 0x04000A17 RID: 2583
		public float mZumaBallPct;

		// Token: 0x04000A18 RID: 2584
		public int mZumaBarState;

		// Token: 0x04000A19 RID: 2585
		public float mGoldBallXOff;

		// Token: 0x04000A1A RID: 2586
		public int mBarXOffset;

		// Token: 0x04000A1B RID: 2587
		public int mZumaBarX;

		// Token: 0x04000A1C RID: 2588
		public int mZumaBarWidth;

		// Token: 0x04000A1D RID: 2589
		private static int last_sound_idx;

		// Token: 0x04000A1E RID: 2590
		private static bool torchChangeState;

		// Token: 0x020000A2 RID: 162
		public enum TorchState
		{
			// Token: 0x04001618 RID: 5656
			TorchState_FlyIn,
			// Token: 0x04001619 RID: 5657
			TorchState_Bounce,
			// Token: 0x0400161A RID: 5658
			TorchState_TossEgg,
			// Token: 0x0400161B RID: 5659
			TorchState_Disappear,
			// Token: 0x0400161C RID: 5660
			TorchState_RaiseDais,
			// Token: 0x0400161D RID: 5661
			TorchState_FrogFlyIn,
			// Token: 0x0400161E RID: 5662
			TorchState_IntroDone,
			// Token: 0x0400161F RID: 5663
			TorchState_ShakeDais,
			// Token: 0x04001620 RID: 5664
			TorchState_FrogDisappear,
			// Token: 0x04001621 RID: 5665
			TorchState_DoFade,
			// Token: 0x04001622 RID: 5666
			TorchState_DropInToNextLevel,
			// Token: 0x04001623 RID: 5667
			TorchState_CloakedBossAppear,
			// Token: 0x04001624 RID: 5668
			TorchState_CloakedBossTransform,
			// Token: 0x04001625 RID: 5669
			TorchState_Complete
		}
	}
}
