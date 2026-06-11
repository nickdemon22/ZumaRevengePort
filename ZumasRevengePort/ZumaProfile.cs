using System;
using System.Collections.Generic;
using SexyFramework.Drivers.Profile;
using SexyFramework.File;
using SexyFramework.Misc;
using ZumasRevenge.Achievement;

namespace ZumasRevenge
{
	// Token: 0x0200002A RID: 42
	public class ZumaProfile : UserProfile
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00040E39 File Offset: 0x0003F039
		protected void InitBossLevel()
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00040E3C File Offset: 0x0003F03C
		public ZumaProfile(ZumaProfile rhs)
		{
			this.CopyFrom(rhs);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00040F0C File Offset: 0x0003F10C
		public ZumaProfile()
		{
			this.mLastSeenMoreGames = 0L;
			this.mWantsChallengeHelp = true;
			this.mNeedsFirstTimeIntro = true;
			this.mUnlockTrial = true;
			this.mHasDoneHeroicUnlockEffect = (this.mHasDoneChallengeUnlockEffect = (this.mHasDoneIFUnlockEffect = false));
			this.mLargestGapShot = 0;
			this.mHighestGapShotScore = 0;
			this.mNumGapShots = 0;
			this.mLargestChainShot = 0;
			this.mLargestCombo = 0;
			this.mNumClearCurveBonuses = 0;
			this.mNeedsChallengeUnlockHint = true;
			this.mPointsFromClearCurve = 0;
			this.mPointsFromGapShots = 0;
			this.mPointsFromCombos = 0;
			this.mDoChallengeAceTrophyZoom = (this.mDoChallengeTrophyZoom = false);
			this.mDoChallengeCupComplete = (this.mDoChallengeAceCupComplete = false);
			this.mUnlockSparklesIdx1 = (this.mUnlockSparklesIdx2 = -1);
			this.mPointsFromChainShots = 0;
			this.mNumFruits = 0;
			this.mPointsFromFruit = 0;
			this.mNumTimesLaserCanceled = 0;
			this.mPointsFromLaser = 0;
			this.mPointsFromCannon = 0;
			this.mPointsFromColorNuke = 0;
			this.mPointsFromProxBomb = 0;
			this.mDoAceCupXFade = false;
			this.mNewChallengeCupUnlocked = false;
			for (int i = 0; i < 14; i++)
			{
				this.mNumTimesActivatedPowerup[i] = 0;
				this.mNumTimesSpawnedPowerup[i] = 0;
			}
			this.mAdvModeVars.mDDSTier = (this.mAdvModeVars.mRestartDDSTier = -1);
			this.mHeroicModeVars.mDDSTier = (this.mHeroicModeVars.mRestartDDSTier = -1);
			this.mFirstTimeReplayingNormalMode = false;
			this.mFirstTimeReplayingHardMode = false;
			this.mBoss6Part2DialogSeen = 0;
			this.mSessionID = 0;
			this.mAdvModeVars.mHighestZoneBeat = 0;
			this.mAdvModeVars.mHighestLevelBeat = 0;
			this.mAdvModeVars.mNumDeathsCurLevel = 0;
			this.mAdvModeVars.mPerfectZone = true;
			this.mAdvModeVars.mNumZumasCurLevel = 0;
			this.mHeroicModeVars.mHighestZoneBeat = 0;
			this.mHeroicModeVars.mHighestLevelBeat = 0;
			this.mHeroicModeVars.mNumDeathsCurLevel = 0;
			this.mHeroicModeVars.mPerfectZone = true;
			this.mHeroicModeVars.mNumZumasCurLevel = 0;
			this.mHighestIronFrogLevel = (this.mHighestIronFrogScore = 0);
			this.mHighestAdvModeScore = (this.mAdvModeHSLevel = (this.mAdvModeHSZone = 0));
			this.mHasBeatIronFrogMode = false;
			for (int j = 0; j < 6; j++)
			{
				this.mHeroicModeVars.mNumTimesZoneBeat[j] = 0;
				this.mAdvModeVars.mNumTimesZoneBeat[j] = 0;
			}
			for (int k = 0; k < 60; k++)
			{
				this.mHeroicModeVars.mBestLevelTime[k] = (this.mAdvModeVars.mBestLevelTime[k] = int.MaxValue);
			}
			this.ClearAdventureModeDetails();
			this.mSessionID++;
			this.mAdvBetaStats.Init(this, this.mSessionID, 2);
			this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
			this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
			this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
			for (int l = 0; l < 7; l++)
			{
				for (int m = 0; m < 10; m++)
				{
					this.mChallengeUnlockState[l, m] = 0;
				}
			}
			this.mNumDoubleGapShots = (this.mNumTripleGapShots = (this.mMatchesMade = (this.mBallsBroken = (this.mBallsSwapped = (this.mBallsFired = (this.mFruitBombed = (this.mBallsTossed = (this.mDeathsAfterZuma = 0))))))));
			Buffer buffer = new Buffer();
			this.mGauntletHSMap.Clear();
			if (StorageFile.ReadBufferFromFile("users/hs.dat", buffer))
			{
				long num = buffer.ReadLong();
				if (num != (long)GameApp.gSaveGameVersion)
				{
					StorageFile.DeleteFile("users/hs.dat");
				}
				else
				{
					long num2 = buffer.ReadLong();
					int num3 = 0;
					while ((long)num3 < num2)
					{
						long num4 = buffer.ReadLong();
						long num5 = buffer.ReadLong();
						List<GauntletHSInfo> list = new List<GauntletHSInfo>();
						int num6 = 0;
						while ((long)num6 < num5)
						{
							long num7 = buffer.ReadLong();
							string n = buffer.ReadString();
							list.Add(new GauntletHSInfo((int)num7, n));
							num6++;
						}
						this.mGauntletHSMap.Add((int)num4, list);
						num3++;
					}
				}
			}
			this.mUserSharingEnabled = true;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000413F8 File Offset: 0x0003F5F8
		public virtual void CopyFrom(ZumaProfile rhs)
		{
			this.mNeedsFirstTimeIntro = rhs.mNeedsFirstTimeIntro;
			this.mUnlockTrial = rhs.mUnlockTrial;
			this.mAdvModeVars.CopyFrom(rhs.mAdvModeVars);
			this.mHeroicModeVars.CopyFrom(rhs.mHeroicModeVars);
			this.mUnlockSparklesIdx1 = rhs.mUnlockSparklesIdx1;
			this.mUnlockSparklesIdx2 = rhs.mUnlockSparklesIdx2;
			this.mDoChallengeAceTrophyZoom = rhs.mDoChallengeAceTrophyZoom;
			this.mDoChallengeTrophyZoom = rhs.mDoChallengeTrophyZoom;
			this.mDoChallengeCupComplete = rhs.mDoChallengeCupComplete;
			this.mDoChallengeAceCupComplete = rhs.mDoChallengeAceCupComplete;
			this.mDoAceCupXFade = rhs.mDoAceCupXFade;
			this.mNeedsChallengeUnlockHint = rhs.mNeedsChallengeUnlockHint;
			this.mHasDoneHeroicUnlockEffect = rhs.mHasDoneHeroicUnlockEffect;
			this.mHasDoneIFUnlockEffect = rhs.mHasDoneIFUnlockEffect;
			this.mHasDoneChallengeUnlockEffect = rhs.mHasDoneChallengeUnlockEffect;
			this.mNewChallengeCupUnlocked = rhs.mNewChallengeCupUnlocked;
			this.mWantsChallengeHelp = rhs.mWantsChallengeHelp;
			this.mAdvBetaStats.CopyFrom(rhs.mAdvBetaStats);
			this.mHardAdvBetaStats.CopyFrom(rhs.mHardAdvBetaStats);
			this.mChallengeBetaStats.CopyFrom(rhs.mChallengeBetaStats);
			this.mIronFrogBetaStats.CopyFrom(rhs.mIronFrogBetaStats);
			this.mSessionID = rhs.mSessionID;
			this.mSessionID++;
			this.mAdvBetaStats.Init(this, this.mSessionID, 2);
			this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
			this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
			this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
			this.mHasBeatIronFrogMode = rhs.mHasBeatIronFrogMode;
			this.mHighestIronFrogLevel = rhs.mHighestIronFrogLevel;
			this.mHighestIronFrogScore = rhs.mHighestIronFrogScore;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					this.mChallengeUnlockState[i, j] = rhs.mChallengeUnlockState[i, j];
				}
			}
			this.mHighestAdvModeScore = rhs.mHighestAdvModeScore;
			this.mAdvModeHSLevel = rhs.mAdvModeHSLevel;
			this.mAdvModeHSZone = rhs.mAdvModeHSZone;
			this.mBoss6Part2DialogSeen = rhs.mBoss6Part2DialogSeen;
			this.mFirstTimeAtBoss = rhs.mFirstTimeAtBoss;
			this.mHintsSeen = rhs.mHintsSeen;
			this.mFirstTimeReplayingNormalMode = rhs.mFirstTimeReplayingNormalMode;
			this.mFirstTimeReplayingHardMode = rhs.mFirstTimeReplayingHardMode;
			this.mAdventureStats.CopyFrom(rhs.mAdventureStats);
			this.mHeroicStats.CopyFrom(rhs.mHeroicStats);
			this.mIronFrogStats.CopyFrom(rhs.mIronFrogStats);
			this.mChallengeStats.CopyFrom(rhs.mChallengeStats);
			this.mNumDoubleGapShots = rhs.mNumDoubleGapShots;
			this.mNumTripleGapShots = rhs.mNumTripleGapShots;
			this.mMatchesMade = rhs.mMatchesMade;
			this.mBallsBroken = rhs.mBallsBroken;
			this.mBallsSwapped = rhs.mBallsSwapped;
			this.mBallsFired = rhs.mBallsFired;
			this.mFruitBombed = rhs.mFruitBombed;
			this.mBallsTossed = rhs.mBallsTossed;
			this.mDeathsAfterZuma = rhs.mDeathsAfterZuma;
			this.mLargestGapShot = rhs.mLargestGapShot;
			this.mHighestGapShotScore = rhs.mHighestGapShotScore;
			this.mNumGapShots = rhs.mNumGapShots;
			this.mLargestChainShot = rhs.mLargestChainShot;
			this.mLargestCombo = rhs.mLargestCombo;
			this.mNumClearCurveBonuses = rhs.mNumClearCurveBonuses;
			this.mPointsFromClearCurve = rhs.mPointsFromClearCurve;
			this.mPointsFromGapShots = rhs.mPointsFromGapShots;
			this.mPointsFromCombos = rhs.mPointsFromCombos;
			this.mPointsFromChainShots = rhs.mPointsFromChainShots;
			this.mNumFruits = rhs.mNumFruits;
			this.mPointsFromFruit = rhs.mPointsFromFruit;
			this.mNumTimesLaserCanceled = rhs.mNumTimesLaserCanceled;
			this.mPointsFromLaser = rhs.mPointsFromLaser;
			this.mPointsFromCannon = rhs.mPointsFromCannon;
			this.mPointsFromColorNuke = rhs.mPointsFromColorNuke;
			this.mPointsFromProxBomb = rhs.mPointsFromProxBomb;
			for (int k = 0; k < 14; k++)
			{
				this.mNumTimesActivatedPowerup[k] = rhs.mNumTimesActivatedPowerup[k];
				this.mNumTimesSpawnedPowerup[k] = rhs.mNumTimesSpawnedPowerup[k];
			}
			this.mLastSeenMoreGames = rhs.mLastSeenMoreGames;
			this.mUserSharingEnabled = rhs.mUserSharingEnabled;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00041800 File Offset: 0x0003FA00
		public void ClearAdventureModeDetails()
		{
			this.mAdvModeVars.mCurrentAdvLevel = (this.mAdvModeVars.mCurrentAdvZone = 1);
			this.mAdvModeVars.mCurrentAdvScore = 0;
			this.mAdvModeVars.mCurrentAdvLives = 3;
			this.mHeroicModeVars.mCurrentAdvLevel = (this.mHeroicModeVars.mCurrentAdvZone = 1);
			this.mHeroicModeVars.mCurrentAdvScore = 0;
			this.mHeroicModeVars.mCurrentAdvLives = 3;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					this.mChallengeUnlockState[i, j] = 0;
				}
			}
			this.mDoChallengeAceTrophyZoom = (this.mDoChallengeTrophyZoom = false);
			this.mDoAceCupXFade = (this.mDoChallengeCupComplete = (this.mDoChallengeAceCupComplete = false));
			this.mHintsSeen = 0;
			this.mAdvModeVars.mPerfectZone = true;
			this.mHeroicModeVars.mPerfectZone = true;
			this.mFirstTimeAtBoss = true;
			this.mAdvModeVars.mNumDeathsCurLevel = 0;
			this.mAdvModeVars.mNumZumasCurLevel = 0;
			this.mHeroicModeVars.mNumDeathsCurLevel = 0;
			this.mHeroicModeVars.mNumZumasCurLevel = 0;
			this.mBoss6Part2DialogSeen = 0;
			this.mAdvModeVars.mDDSTier = (this.mAdvModeVars.mRestartDDSTier = -1);
			this.mHeroicModeVars.mDDSTier = (this.mHeroicModeVars.mRestartDDSTier = -1);
			for (int k = 0; k < 6; k++)
			{
				this.mHeroicModeVars.mFirstTimeInZone[k] = true;
				this.mAdvModeVars.mFirstTimeInZone[k] = true;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00041988 File Offset: 0x0003FB88
		public int ChallengeCupComplete(int zone)
		{
			zone--;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 10; i++)
			{
				if (this.mChallengeUnlockState[zone, i] == 4)
				{
					num++;
				}
				else if (this.mChallengeUnlockState[zone, i] == 5)
				{
					num++;
					num2++;
				}
			}
			if (num2 == 10)
			{
				return 2;
			}
			if (num == 10)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000419E8 File Offset: 0x0003FBE8
		public string GetSaveGameNameFolder()
		{
			return "users";
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000419EF File Offset: 0x0003FBEF
		public string GetSaveGameName(bool hard_mode)
		{
			return this.GetSaveGameNameFolder() + string.Format("/{0}_in_game{1}.sav", hard_mode ? "heroic" : "adv", this.GetId());
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00041A20 File Offset: 0x0003FC20
		public AdvModeVars GetAdvModeVars()
		{
			if (GameApp.gApp.IsHardMode())
			{
				return this.mHeroicModeVars;
			}
			return this.mAdvModeVars;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00041A3B File Offset: 0x0003FC3B
		public void BossLevelStarted()
		{
			if (!this.mFirstTimeAtBoss)
			{
				return;
			}
			this.mFirstTimeAtBoss = false;
			this.InitBossLevel();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00041A54 File Offset: 0x0003FC54
		public bool SyncDetails(DataSync theSync)
		{
			int gSaveGameVersion = GameApp.gSaveGameVersion;
			theSync.SyncLong(ref gSaveGameVersion);
			if (gSaveGameVersion != GameApp.gSaveGameVersion)
			{
				return false;
			}
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvLives);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvScore);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvLevel);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvZone);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvLives);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvScore);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvLevel);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvZone);
			theSync.SyncBoolean(ref this.mWantsChallengeHelp);
			theSync.SyncBoolean(ref this.mNeedsFirstTimeIntro);
			theSync.SyncBoolean(ref this.mUnlockTrial);
			theSync.SyncLong(ref this.mSessionID);
			theSync.SyncLong(ref this.mAdvModeVars.mDDSTier);
			theSync.SyncLong(ref this.mAdvModeVars.mRestartDDSTier);
			theSync.SyncLong(ref this.mHeroicModeVars.mDDSTier);
			theSync.SyncLong(ref this.mHeroicModeVars.mRestartDDSTier);
			theSync.SyncBoolean(ref this.mHasDoneChallengeUnlockEffect);
			theSync.SyncBoolean(ref this.mHasDoneIFUnlockEffect);
			theSync.SyncBoolean(ref this.mHasDoneHeroicUnlockEffect);
			theSync.SyncBoolean(ref this.mFirstTimeAtBoss);
			for (int i = 0; i < 6; i++)
			{
				theSync.SyncBoolean(ref this.mAdvModeVars.mFirstTimeInZone[i]);
				theSync.SyncBoolean(ref this.mHeroicModeVars.mFirstTimeInZone[i]);
			}
			for (int j = 0; j < 6; j++)
			{
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mBoss);
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mMidpoint);
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mZoneStart);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mBoss);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mMidpoint);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mZoneStart);
			}
			theSync.SyncBoolean(ref this.mNeedsChallengeUnlockHint);
			theSync.SyncLong(ref this.mBoss6Part2DialogSeen);
			theSync.SyncLong(ref this.mHighestAdvModeScore);
			theSync.SyncLong(ref this.mAdvModeHSZone);
			theSync.SyncLong(ref this.mAdvModeHSLevel);
			theSync.SyncLong(ref this.mHintsSeen);
			theSync.SyncLong(ref this.mLargestGapShot);
			theSync.SyncLong(ref this.mHighestGapShotScore);
			theSync.SyncLong(ref this.mNumGapShots);
			theSync.SyncLong(ref this.mLargestChainShot);
			theSync.SyncLong(ref this.mLargestCombo);
			theSync.SyncLong(ref this.mNumClearCurveBonuses);
			theSync.SyncLong(ref this.mPointsFromClearCurve);
			theSync.SyncLong(ref this.mPointsFromGapShots);
			theSync.SyncLong(ref this.mPointsFromCombos);
			theSync.SyncLong(ref this.mPointsFromChainShots);
			theSync.SyncLong(ref this.mNumFruits);
			theSync.SyncLong(ref this.mPointsFromFruit);
			theSync.SyncLong(ref this.mNumTimesLaserCanceled);
			theSync.SyncLong(ref this.mPointsFromLaser);
			theSync.SyncLong(ref this.mPointsFromCannon);
			theSync.SyncLong(ref this.mPointsFromColorNuke);
			theSync.SyncLong(ref this.mPointsFromProxBomb);
			theSync.SyncLong(ref this.mNumDoubleGapShots);
			theSync.SyncLong(ref this.mNumTripleGapShots);
			theSync.SyncLong(ref this.mMatchesMade);
			theSync.SyncLong(ref this.mBallsBroken);
			theSync.SyncLong(ref this.mBallsSwapped);
			theSync.SyncLong(ref this.mBallsFired);
			theSync.SyncLong(ref this.mFruitBombed);
			theSync.SyncLong(ref this.mBallsTossed);
			theSync.SyncLong(ref this.mDeathsAfterZuma);
			for (int k = 0; k < 14; k++)
			{
				theSync.SyncLong(ref this.mNumTimesActivatedPowerup[k]);
				theSync.SyncLong(ref this.mNumTimesSpawnedPowerup[k]);
			}
			theSync.SyncLong(ref this.mAdvModeVars.mNumDeathsCurLevel);
			theSync.SyncLong(ref this.mAdvModeVars.mNumZumasCurLevel);
			theSync.SyncBoolean(ref this.mAdvModeVars.mPerfectZone);
			theSync.SyncLong(ref this.mHeroicModeVars.mNumDeathsCurLevel);
			theSync.SyncLong(ref this.mHeroicModeVars.mNumZumasCurLevel);
			theSync.SyncBoolean(ref this.mHeroicModeVars.mPerfectZone);
			theSync.SyncBoolean(ref this.mFirstTimeReplayingNormalMode);
			theSync.SyncBoolean(ref this.mFirstTimeReplayingHardMode);
			theSync.SyncLong(ref this.mAdvModeVars.mHighestZoneBeat);
			theSync.SyncLong(ref this.mAdvModeVars.mHighestLevelBeat);
			theSync.SyncLong(ref this.mHeroicModeVars.mHighestZoneBeat);
			theSync.SyncLong(ref this.mHeroicModeVars.mHighestLevelBeat);
			for (int l = 0; l < 6; l++)
			{
				theSync.SyncLong(ref this.mHeroicModeVars.mNumTimesZoneBeat[l]);
				theSync.SyncLong(ref this.mAdvModeVars.mNumTimesZoneBeat[l]);
			}
			for (int m = 0; m < 60; m++)
			{
				theSync.SyncLong(ref this.mHeroicModeVars.mBestLevelTime[m]);
				theSync.SyncLong(ref this.mAdvModeVars.mBestLevelTime[m]);
			}
			theSync.SyncBoolean(ref this.mHasBeatIronFrogMode);
			this.mAdventureStats.Sync(theSync);
			this.mHeroicStats.Sync(theSync);
			this.mIronFrogStats.Sync(theSync);
			this.mChallengeStats.Sync(theSync);
			for (int n = 0; n < 7; n++)
			{
				for (int num = 0; num < 10; num++)
				{
					theSync.SyncLong(ref this.mChallengeUnlockState[n, num]);
				}
			}
			theSync.SyncBoolean(ref this.mNewChallengeCupUnlocked);
			theSync.SyncLong(ref this.mLastSeenMoreGames);
			theSync.SyncBoolean(ref this.mUserSharingEnabled);
			this.m_AchievementMgr.Sync(theSync);
			return true;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00042018 File Offset: 0x00040218
		public override bool ReadProfileSettings(Buffer theData)
		{
			try
			{
				DataSync theSync = new DataSync(theData, true);
				this.SyncDetails(theSync);
				this.mSessionID++;
				this.mAdvBetaStats.Init(this, this.mSessionID, 2);
				this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
				this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
				this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
				this.mAdvBetaStats.LoadData();
				this.mHardAdvBetaStats.LoadData();
				this.mChallengeBetaStats.LoadData();
				this.mIronFrogBetaStats.LoadData();
			}
			catch (Exception)
			{
				this.ClearAdventureModeDetails();
				return false;
			}
			return true;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000420DC File Offset: 0x000402DC
		public override bool WriteProfileSettings(Buffer theData)
		{
			DataSync theSync = new DataSync(theData, false);
			this.SyncDetails(theSync);
			return true;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000420FA File Offset: 0x000402FA
		public void BossLevelComplete()
		{
			this.mFirstTimeAtBoss = true;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00042103 File Offset: 0x00040303
		public bool HasSeenHint(int hint_num)
		{
			return (this.mHintsSeen & hint_num) == hint_num;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00042110 File Offset: 0x00040310
		public void MarkHintAsSeen(int hint_num)
		{
			this.mHintsSeen |= hint_num;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00042120 File Offset: 0x00040320
		public int AddGauntletHighScore(int level, int score, string profile_name)
		{
			if (!this.mGauntletHSMap.ContainsKey(level))
			{
				this.mGauntletHSMap.Add(level, new List<GauntletHSInfo>());
			}
			List<GauntletHSInfo> list = this.mGauntletHSMap[level];
			string[] array = new string[]
			{
				TextManager.getInstance().getString(712),
				TextManager.getInstance().getString(713),
				TextManager.getInstance().getString(714),
				TextManager.getInstance().getString(715),
				TextManager.getInstance().getString(716),
				TextManager.getInstance().getString(717),
				TextManager.getInstance().getString(718)
			};
			int num = 0;
			while (list.Count < 5)
			{
				list.Insert(list.Count, new GauntletHSInfo(15000 - num * 1000, array[num++]));
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (score >= list[i].mScore)
				{
					list.Insert(i, new GauntletHSInfo(score, profile_name));
					Buffer buffer = new Buffer();
					buffer.WriteLong((long)GameApp.gSaveGameVersion);
					buffer.WriteLong((long)this.mGauntletHSMap.Count);
					foreach (KeyValuePair<int, List<GauntletHSInfo>> keyValuePair in this.mGauntletHSMap)
					{
						buffer.WriteLong((long)keyValuePair.Key);
						buffer.WriteLong((long)keyValuePair.Value.Count);
						for (int j = 0; j < keyValuePair.Value.Count; j++)
						{
							buffer.WriteLong((long)keyValuePair.Value[j].mScore);
							buffer.WriteString(keyValuePair.Value[j].mProfileName);
						}
					}
					StorageFile.WriteBufferToFile("users/hs.dat", buffer);
					return i;
				}
			}
			list.Insert(list.Count, new GauntletHSInfo(score, profile_name));
			Buffer buffer2 = new Buffer();
			buffer2.WriteLong((long)GameApp.gSaveGameVersion);
			buffer2.WriteLong((long)this.mGauntletHSMap.Count);
			foreach (KeyValuePair<int, List<GauntletHSInfo>> keyValuePair2 in this.mGauntletHSMap)
			{
				buffer2.WriteLong((long)keyValuePair2.Key);
				buffer2.WriteLong((long)keyValuePair2.Value.Count);
				for (int k = 0; k < keyValuePair2.Value.Count; k++)
				{
					buffer2.WriteLong((long)keyValuePair2.Value[k].mScore);
					buffer2.WriteString(keyValuePair2.Value[k].mProfileName);
				}
			}
			StorageFile.WriteBufferToFile("users/hs.dat", buffer2);
			return list.Count - 1;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00042444 File Offset: 0x00040644
		private static int HSSort(GauntletHSInfo hs1, GauntletHSInfo hs2)
		{
			if (hs1.mScore > hs2.mScore)
			{
				return -1;
			}
			if (hs1.mScore < hs2.mScore)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00042468 File Offset: 0x00040668
		public void GetGauntletHighScores(int level, ref List<GauntletHSInfo> scores)
		{
			if (this.mGauntletHSMap.ContainsKey(level))
			{
				scores = this.mGauntletHSMap[level];
			}
			string[] array = new string[]
			{
				TextManager.getInstance().getString(719),
				TextManager.getInstance().getString(720),
				TextManager.getInstance().getString(721),
				TextManager.getInstance().getString(722),
				TextManager.getInstance().getString(723),
				TextManager.getInstance().getString(724),
				TextManager.getInstance().getString(725)
			};
			int num = 0;
			while (scores.Count < 5)
			{
				scores.Insert(scores.Count, new GauntletHSInfo(15000 - num * 1000, array[num++]));
			}
			scores.Sort(new Comparison<GauntletHSInfo>(ZumaProfile.HSSort));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00042560 File Offset: 0x00040760
		public void DisableUserSharing()
		{
			this.mUserSharingEnabled = false;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00042569 File Offset: 0x00040769
		public void EnableUserSharing()
		{
			this.mUserSharingEnabled = true;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00042572 File Offset: 0x00040772
		public bool IsUserSharingEnabled()
		{
			return this.mUserSharingEnabled;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0004257A File Offset: 0x0004077A
		internal int GetGamepadIndex()
		{
			return 0;
		}

		// Token: 0x04000BF3 RID: 3059
		public static int MAX_LEVEL_SAMPLES = 4;

		// Token: 0x04000BF4 RID: 3060
		public static int MAX_BOSS_SAMPLES = 4;

		// Token: 0x04000BF5 RID: 3061
		public static int FIRST_SHOT_HINT = 1;

		// Token: 0x04000BF6 RID: 3062
		public static int ZUMA_BAR_HINT = 2;

		// Token: 0x04000BF7 RID: 3063
		public static int SKULL_PIT_HINT = 4;

		// Token: 0x04000BF8 RID: 3064
		public static int LILLY_PAD_HINT = 8;

		// Token: 0x04000BF9 RID: 3065
		public static int FRUIT_HINT = 16;

		// Token: 0x04000BFA RID: 3066
		public static int CHALLENGE_HINT = 32;

		// Token: 0x04000BFB RID: 3067
		public static int SWAP_BALL_HINT = 64;

		// Token: 0x04000BFC RID: 3068
		public long mLastSeenMoreGames;

		// Token: 0x04000BFD RID: 3069
		public AdvModeTempleStats mAdventureStats = new AdvModeTempleStats();

		// Token: 0x04000BFE RID: 3070
		public AdvModeTempleStats mHeroicStats = new AdvModeTempleStats();

		// Token: 0x04000BFF RID: 3071
		public IronFrogTempleStats mIronFrogStats = new IronFrogTempleStats();

		// Token: 0x04000C00 RID: 3072
		public ChallengeTempleStats mChallengeStats = new ChallengeTempleStats();

		// Token: 0x04000C01 RID: 3073
		public BetaStats mAdvBetaStats = new BetaStats();

		// Token: 0x04000C02 RID: 3074
		public BetaStats mHardAdvBetaStats = new BetaStats();

		// Token: 0x04000C03 RID: 3075
		public BetaStats mChallengeBetaStats = new BetaStats();

		// Token: 0x04000C04 RID: 3076
		public BetaStats mIronFrogBetaStats = new BetaStats();

		// Token: 0x04000C05 RID: 3077
		public bool mNewChallengeCupUnlocked;

		// Token: 0x04000C06 RID: 3078
		public int[,] mChallengeUnlockState = new int[7, 10];

		// Token: 0x04000C07 RID: 3079
		public bool mFirstTimeReplayingNormalMode;

		// Token: 0x04000C08 RID: 3080
		public bool mFirstTimeReplayingHardMode;

		// Token: 0x04000C09 RID: 3081
		public bool mUnlockTrial;

		// Token: 0x04000C0A RID: 3082
		public bool mNeedsFirstTimeIntro = true;

		// Token: 0x04000C0B RID: 3083
		public bool mWantsChallengeHelp;

		// Token: 0x04000C0C RID: 3084
		public bool mDoChallengeTrophyZoom;

		// Token: 0x04000C0D RID: 3085
		public bool mDoChallengeAceTrophyZoom;

		// Token: 0x04000C0E RID: 3086
		public bool mDoChallengeCupComplete;

		// Token: 0x04000C0F RID: 3087
		public bool mDoChallengeAceCupComplete;

		// Token: 0x04000C10 RID: 3088
		public bool mDoAceCupXFade;

		// Token: 0x04000C11 RID: 3089
		public bool mNeedsChallengeUnlockHint;

		// Token: 0x04000C12 RID: 3090
		public int mUnlockSparklesIdx1;

		// Token: 0x04000C13 RID: 3091
		public int mUnlockSparklesIdx2;

		// Token: 0x04000C14 RID: 3092
		public bool mHasDoneIFUnlockEffect;

		// Token: 0x04000C15 RID: 3093
		public bool mHasDoneChallengeUnlockEffect;

		// Token: 0x04000C16 RID: 3094
		public bool mHasDoneHeroicUnlockEffect;

		// Token: 0x04000C17 RID: 3095
		public bool mHasBeatIronFrogMode;

		// Token: 0x04000C18 RID: 3096
		public int mHighestIronFrogLevel;

		// Token: 0x04000C19 RID: 3097
		public int mHighestIronFrogScore;

		// Token: 0x04000C1A RID: 3098
		public int mNumDoubleGapShots;

		// Token: 0x04000C1B RID: 3099
		public int mNumTripleGapShots;

		// Token: 0x04000C1C RID: 3100
		public int mMatchesMade;

		// Token: 0x04000C1D RID: 3101
		public int mBallsBroken;

		// Token: 0x04000C1E RID: 3102
		public int mBallsSwapped;

		// Token: 0x04000C1F RID: 3103
		public int mBallsFired;

		// Token: 0x04000C20 RID: 3104
		public int mFruitBombed;

		// Token: 0x04000C21 RID: 3105
		public int mBallsTossed;

		// Token: 0x04000C22 RID: 3106
		public int mDeathsAfterZuma;

		// Token: 0x04000C23 RID: 3107
		public AdvModeVars mAdvModeVars = new AdvModeVars();

		// Token: 0x04000C24 RID: 3108
		public AdvModeVars mHeroicModeVars = new AdvModeVars();

		// Token: 0x04000C25 RID: 3109
		public int mHighestAdvModeScore;

		// Token: 0x04000C26 RID: 3110
		public int mAdvModeHSLevel;

		// Token: 0x04000C27 RID: 3111
		public int mAdvModeHSZone;

		// Token: 0x04000C28 RID: 3112
		public int mBoss6Part2DialogSeen;

		// Token: 0x04000C29 RID: 3113
		public int mLargestGapShot;

		// Token: 0x04000C2A RID: 3114
		public int mHighestGapShotScore;

		// Token: 0x04000C2B RID: 3115
		public int mNumGapShots;

		// Token: 0x04000C2C RID: 3116
		public int mLargestChainShot;

		// Token: 0x04000C2D RID: 3117
		public int mLargestCombo;

		// Token: 0x04000C2E RID: 3118
		public int mNumClearCurveBonuses;

		// Token: 0x04000C2F RID: 3119
		public int mPointsFromClearCurve;

		// Token: 0x04000C30 RID: 3120
		public int mPointsFromGapShots;

		// Token: 0x04000C31 RID: 3121
		public int mPointsFromCombos;

		// Token: 0x04000C32 RID: 3122
		public int mPointsFromChainShots;

		// Token: 0x04000C33 RID: 3123
		public int mNumFruits;

		// Token: 0x04000C34 RID: 3124
		public int mPointsFromFruit;

		// Token: 0x04000C35 RID: 3125
		public int mNumTimesLaserCanceled;

		// Token: 0x04000C36 RID: 3126
		public int mPointsFromLaser;

		// Token: 0x04000C37 RID: 3127
		public int mPointsFromCannon;

		// Token: 0x04000C38 RID: 3128
		public int mPointsFromColorNuke;

		// Token: 0x04000C39 RID: 3129
		public int mPointsFromProxBomb;

		// Token: 0x04000C3A RID: 3130
		public int[] mNumTimesActivatedPowerup = new int[14];

		// Token: 0x04000C3B RID: 3131
		public int[] mNumTimesSpawnedPowerup = new int[14];

		// Token: 0x04000C3C RID: 3132
		public static int MAX_GAUNTLET_HIGH_SCORES = 5;

		// Token: 0x04000C3D RID: 3133
		private bool mUserSharingEnabled;

		// Token: 0x04000C3E RID: 3134
		protected Dictionary<int, List<GauntletHSInfo>> mGauntletHSMap = new Dictionary<int, List<GauntletHSInfo>>();

		// Token: 0x04000C3F RID: 3135
		protected int mSessionID;

		// Token: 0x04000C40 RID: 3136
		protected bool mFirstTimeAtBoss;

		// Token: 0x04000C41 RID: 3137
		protected int mHintsSeen;

		// Token: 0x04000C42 RID: 3138
		public AchievementManager m_AchievementMgr = new AchievementManager();

		// Token: 0x0200004B RID: 75
		public enum ChallengeState
		{
			// Token: 0x04001153 RID: 4435
			ChallengeState_Locked,
			// Token: 0x04001154 RID: 4436
			ChallengeState_ZoneUnlocked,
			// Token: 0x04001155 RID: 4437
			ChallengeState_CanPlay,
			// Token: 0x04001156 RID: 4438
			ChallengeState_LevelComplete,
			// Token: 0x04001157 RID: 4439
			ChallengeState_GoalComplete,
			// Token: 0x04001158 RID: 4440
			ChallengeState_AceComplete
		}
	}
}
