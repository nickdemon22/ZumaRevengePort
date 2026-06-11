using System;
using SexyFramework;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200004E RID: 78
	public class BetaStats
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00056948 File Offset: 0x00054B48
		protected void Reset()
		{
			this.mBossHP = 0;
			this.mLives = 0;
			this.mNumDeathsThisLevel = 0;
			this.mLevelTime = (this.mAceTime = 0);
			this.mHighestGapShotScore = 0;
			this.mHighestChainShotPoints = 0;
			this.mHighestComboPoints = 0;
			this.mFurthestRolloutPct = 0f;
			this.mMaxFruitMultiplier = 0;
			this.mPerfectLevelBonus = 0;
			this.mAceBonus = 0;
			this.mLevelScore = (this.mTotalScore = 0);
			this.mLargestGapShot = 0;
			this.mNumGapShots = 0;
			this.mPointsFromGapShots = 0;
			this.mLargestChainShot = 0;
			this.mPointsFromChainShots = 0;
			this.mLargestCombo = 0;
			this.mPointsFromCombos = 0;
			this.mNumClearCurveBonuses = 0;
			this.mPointsFromClearCurve = 0;
			this.mNumFruits = 0;
			this.mPointsFromFruit = 0;
			this.mNumTimesLaserCanceled = 0;
			this.mPointsFromLaser = 0;
			this.mPointsFromCannon = 0;
			this.mPointsFromColorNuke = 0;
			this.mPointsFromProxBomb = 0;
			this.mWasFromCheckpoint = (this.mWasFromZoneRestart = false);
			this.mNumTimesActivatedPowerup = new int[14];
			this.mNumTimesSpawnedPowerup = new int[14];
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00056A59 File Offset: 0x00054C59
		protected void SaveCSVFile(int challenge_level, int challenge_mult)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00056A5B File Offset: 0x00054C5B
		protected void SaveCSVFile(int challenge_level)
		{
			this.SaveCSVFile(challenge_level, -1);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00056A65 File Offset: 0x00054C65
		protected void SaveCSVFile()
		{
			this.SaveCSVFile(-1, -1);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00056A6F File Offset: 0x00054C6F
		protected void Serialize(SexyFramework.Misc.Buffer b)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00056A71 File Offset: 0x00054C71
		protected bool Deserialize(SexyFramework.Misc.Buffer b)
		{
			return true;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00056A74 File Offset: 0x00054C74
		public BetaStats()
		{
			this.mSessionID = 0;
			this.mMode = BetaStats.Mode.Mode_None;
			this.mLevelZone = -1;
			this.mLevelNum = -1;
			this.mProfile = null;
			this.Reset();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00056ACC File Offset: 0x00054CCC
		public void CopyFrom(BetaStats rhs)
		{
			this.mProfile = rhs.mProfile;
			this.mSessionID = rhs.mSessionID;
			this.mMode = rhs.mMode;
			this.mNumDeathsThisLevel = rhs.mNumDeathsThisLevel;
			this.mLevelName = rhs.mLevelName;
			this.mLevelZone = rhs.mLevelZone;
			this.mLevelNum = rhs.mLevelNum;
			this.mLevelTime = rhs.mLevelTime;
			this.mAceTime = rhs.mAceTime;
			this.mNumGapShots = rhs.mNumGapShots;
			this.mLargestGapShot = rhs.mLargestGapShot;
			this.mHighestGapShotScore = rhs.mHighestGapShotScore;
			this.mLargestChainShot = rhs.mLargestChainShot;
			this.mHighestChainShotPoints = rhs.mHighestChainShotPoints;
			this.mLargestCombo = rhs.mLargestCombo;
			this.mHighestComboPoints = rhs.mHighestComboPoints;
			this.mFurthestRolloutPct = rhs.mFurthestRolloutPct;
			this.mNumClearCurveBonuses = rhs.mNumClearCurveBonuses;
			this.mPerfectLevelBonus = rhs.mPerfectLevelBonus;
			this.mAceBonus = rhs.mAceBonus;
			this.mPointsFromClearCurve = rhs.mPointsFromClearCurve;
			this.mPointsFromGapShots = rhs.mPointsFromGapShots;
			this.mPointsFromCombos = rhs.mPointsFromCombos;
			this.mPointsFromChainShots = rhs.mPointsFromChainShots;
			this.mNumFruits = rhs.mNumFruits;
			this.mLives = rhs.mLives;
			this.mBossHP = rhs.mBossHP;
			this.mPointsFromFruit = rhs.mPointsFromFruit;
			this.mMaxFruitMultiplier = rhs.mMaxFruitMultiplier;
			this.mNumTimesLaserCanceled = rhs.mNumTimesLaserCanceled;
			this.mWasFromCheckpoint = rhs.mWasFromCheckpoint;
			this.mWasFromZoneRestart = rhs.mWasFromZoneRestart;
			this.mLevelScore = rhs.mLevelScore;
			this.mTotalScore = rhs.mTotalScore;
			this.mPointsFromLaser = rhs.mPointsFromLaser;
			this.mPointsFromCannon = rhs.mPointsFromCannon;
			this.mPointsFromColorNuke = rhs.mPointsFromColorNuke;
			this.mPointsFromProxBomb = rhs.mPointsFromProxBomb;
			for (int i = 0; i < 14; i++)
			{
				this.mNumTimesActivatedPowerup[i] = rhs.mNumTimesActivatedPowerup[i];
				this.mNumTimesSpawnedPowerup[i] = rhs.mNumTimesSpawnedPowerup[i];
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00056CD0 File Offset: 0x00054ED0
		public string GetCSVFileName()
		{
			switch (this.mMode)
			{
			case BetaStats.Mode.Mode_Challenge:
				return Common.GetAppDataFolder() + "CHALLENGE STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_IronFrog:
				return Common.GetAppDataFolder() + "IRON FROG STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_Adventure:
				return Common.GetAppDataFolder() + "ADVENTURE STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_HardAdventure:
				return Common.GetAppDataFolder() + "HEROIC STATS DO NOT DELETE.csv";
			default:
				return "ERROR.csv";
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00056D44 File Offset: 0x00054F44
		public string GetDATFileName()
		{
			string text = "";
			switch (this.mMode)
			{
			case BetaStats.Mode.Mode_Challenge:
				text = "challenge";
				break;
			case BetaStats.Mode.Mode_IronFrog:
				text = "if";
				break;
			case BetaStats.Mode.Mode_Adventure:
				text = "adv";
				break;
			case BetaStats.Mode.Mode_HardAdventure:
				text = "hard_adv";
				break;
			}
			return Common.GetAppDataFolder() + string.Format("users/user{0}_{1}_stats.dat", this.mProfile.GetId(), text);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00056DB9 File Offset: 0x00054FB9
		public void Init(ZumaProfile p, int session_id, int mode)
		{
			this.mProfile = p;
			this.mSessionID = session_id;
			this.Reset();
			this.mMode = (BetaStats.Mode)mode;
			this.mLevelZone = -1;
			this.mLevelNum = -1;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00056DE4 File Offset: 0x00054FE4
		public void LevelStarted(string level_name, int zone, int num, bool from_checkpoint, bool zone_restart)
		{
			this.Reset();
			this.mLevelName = level_name;
			this.mLevelZone = zone;
			this.mLevelNum = num;
			this.mWasFromCheckpoint = from_checkpoint;
			this.mWasFromZoneRestart = zone_restart;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00056E14 File Offset: 0x00055014
		public void BeatLevel(int level_time, int ace_time, int ace_bonus, int perfect_bonus, float rollout_pct, int level_score, int total_score, int lives)
		{
			this.mLives = lives;
			this.mLevelTime = level_time;
			this.mAceTime = ace_time;
			this.mAceBonus = ace_bonus;
			this.mPerfectLevelBonus = perfect_bonus;
			this.mFurthestRolloutPct = rollout_pct;
			this.mLevelScore = level_score;
			this.mTotalScore = total_score;
			this.SaveCSVFile();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00056E64 File Offset: 0x00055064
		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level, int challenge_multiplier, int boss_hp)
		{
			this.mBossHP = boss_hp;
			this.mLevelScore = level_score;
			this.mTotalScore = total_score;
			this.mLives = lives_left;
			this.mFurthestRolloutPct = 1f;
			this.mNumDeathsThisLevel++;
			this.mLevelTime = level_time;
			this.SaveCSVFile(challenge_level, challenge_multiplier);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00056EB9 File Offset: 0x000550B9
		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level, int challenge_multiplier)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, challenge_level, challenge_multiplier, 0);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00056ECB File Offset: 0x000550CB
		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, challenge_level, -1);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00056EDB File Offset: 0x000550DB
		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, -1, -1);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00056EEC File Offset: 0x000550EC
		public void LoadData()
		{
			string datfileName = this.GetDATFileName();
			SexyFramework.Misc.Buffer b = new SexyFramework.Misc.Buffer();
			if (GameApp.gApp.ReadBufferFromFile(datfileName, ref b) && !this.Deserialize(b))
			{
				GameApp.gApp.EraseFile(datfileName);
				GameApp.gApp.EraseFile(this.GetCSVFileName());
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00056F3C File Offset: 0x0005513C
		public void SaveData()
		{
			SexyFramework.Misc.Buffer buffer = new SexyFramework.Misc.Buffer();
			this.Serialize(buffer);
			GameApp.gApp.WriteBufferToFile(this.GetDATFileName(), buffer);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00056F68 File Offset: 0x00055168
		public void SetFruitMultiplier(int m)
		{
			if (m > this.mMaxFruitMultiplier)
			{
				this.mMaxFruitMultiplier = m;
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00056F7C File Offset: 0x0005517C
		public void GapShot(int points, int size)
		{
			this.mNumGapShots++;
			this.mProfile.mNumGapShots++;
			if (size > this.mLargestGapShot)
			{
				this.mLargestGapShot = size;
			}
			if (points > this.mHighestGapShotScore)
			{
				this.mHighestGapShotScore = points;
			}
			if (points > this.mProfile.mHighestGapShotScore)
			{
				this.mProfile.mHighestGapShotScore = points;
			}
			if (this.mLargestGapShot > this.mProfile.mLargestGapShot)
			{
				this.mProfile.mLargestGapShot = this.mLargestGapShot;
			}
			this.mPointsFromGapShots += points;
			this.mProfile.mPointsFromGapShots += points;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0005702C File Offset: 0x0005522C
		public void ChainShot(int points, int size)
		{
			if (size > this.mLargestChainShot)
			{
				this.mLargestChainShot = size;
			}
			if (points > this.mHighestChainShotPoints)
			{
				this.mHighestChainShotPoints = points;
			}
			this.mPointsFromChainShots += points;
			this.mProfile.mPointsFromChainShots += points;
			if (this.mLargestChainShot > this.mProfile.mLargestChainShot)
			{
				this.mProfile.mLargestChainShot = this.mLargestChainShot;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000570A0 File Offset: 0x000552A0
		public void Combo(int points, int size)
		{
			if (points > this.mHighestComboPoints)
			{
				this.mHighestComboPoints = points;
			}
			if (size > this.mLargestCombo)
			{
				this.mLargestCombo = size;
			}
			this.mPointsFromCombos += points;
			this.mProfile.mPointsFromCombos += points;
			if (this.mLargestCombo > this.mProfile.mLargestCombo)
			{
				this.mProfile.mLargestCombo = this.mLargestCombo;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00057114 File Offset: 0x00055314
		public void ClearedCurve(int points)
		{
			this.mNumClearCurveBonuses++;
			this.mPointsFromClearCurve += points;
			this.mProfile.mNumClearCurveBonuses++;
			this.mProfile.mPointsFromClearCurve += points;
			if (this.mNumClearCurveBonuses >= 2)
			{
				GameApp.gApp.SetAchievement("clear_2x");
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0005717C File Offset: 0x0005537C
		public void HitFruit(int points)
		{
			this.mNumFruits++;
			this.mPointsFromFruit += points;
			this.mProfile.mNumFruits++;
			this.mProfile.mPointsFromFruit += points;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000571CB File Offset: 0x000553CB
		public void CanceledLaser()
		{
			this.mNumTimesLaserCanceled++;
			this.mProfile.mNumTimesLaserCanceled++;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000571F0 File Offset: 0x000553F0
		public void BallExplodedFromPowerup(int power_type)
		{
			if (power_type == 0)
			{
				this.mPointsFromProxBomb += 10;
				this.mProfile.mPointsFromProxBomb += 10;
				return;
			}
			switch (power_type)
			{
			case 7:
				this.mPointsFromCannon += 10;
				this.mProfile.mPointsFromCannon += 10;
				return;
			case 8:
				this.mPointsFromColorNuke += 10;
				this.mProfile.mPointsFromColorNuke += 10;
				return;
			case 9:
				this.mPointsFromLaser += 10;
				this.mProfile.mPointsFromLaser += 10;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000572AA File Offset: 0x000554AA
		public void ActivatedPowerup(int power_type)
		{
			this.mNumTimesActivatedPowerup[power_type]++;
			this.mProfile.mNumTimesActivatedPowerup[power_type]++;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000572E3 File Offset: 0x000554E3
		public void SpawnedPowerup(int power_type)
		{
			this.mNumTimesSpawnedPowerup[power_type]++;
		}

		// Token: 0x04001168 RID: 4456
		protected int mSessionID;

		// Token: 0x04001169 RID: 4457
		protected BetaStats.Mode mMode;

		// Token: 0x0400116A RID: 4458
		protected int mNumDeathsThisLevel;

		// Token: 0x0400116B RID: 4459
		protected string mLevelName;

		// Token: 0x0400116C RID: 4460
		protected int mLevelZone;

		// Token: 0x0400116D RID: 4461
		protected int mLevelNum;

		// Token: 0x0400116E RID: 4462
		protected int mLevelTime;

		// Token: 0x0400116F RID: 4463
		protected int mAceTime;

		// Token: 0x04001170 RID: 4464
		protected int mNumGapShots;

		// Token: 0x04001171 RID: 4465
		protected int mLargestGapShot;

		// Token: 0x04001172 RID: 4466
		protected int mHighestGapShotScore;

		// Token: 0x04001173 RID: 4467
		protected int mLargestChainShot;

		// Token: 0x04001174 RID: 4468
		protected int mHighestChainShotPoints;

		// Token: 0x04001175 RID: 4469
		protected int mLargestCombo;

		// Token: 0x04001176 RID: 4470
		protected int mHighestComboPoints;

		// Token: 0x04001177 RID: 4471
		protected float mFurthestRolloutPct;

		// Token: 0x04001178 RID: 4472
		protected int mNumClearCurveBonuses;

		// Token: 0x04001179 RID: 4473
		protected int mPerfectLevelBonus;

		// Token: 0x0400117A RID: 4474
		protected int mAceBonus;

		// Token: 0x0400117B RID: 4475
		protected int mPointsFromClearCurve;

		// Token: 0x0400117C RID: 4476
		protected int mPointsFromGapShots;

		// Token: 0x0400117D RID: 4477
		protected int mPointsFromCombos;

		// Token: 0x0400117E RID: 4478
		protected int mPointsFromChainShots;

		// Token: 0x0400117F RID: 4479
		protected int mNumFruits;

		// Token: 0x04001180 RID: 4480
		protected int mLives;

		// Token: 0x04001181 RID: 4481
		protected int mBossHP;

		// Token: 0x04001182 RID: 4482
		protected int mPointsFromFruit;

		// Token: 0x04001183 RID: 4483
		protected int mMaxFruitMultiplier;

		// Token: 0x04001184 RID: 4484
		protected int mNumTimesLaserCanceled;

		// Token: 0x04001185 RID: 4485
		protected bool mWasFromCheckpoint;

		// Token: 0x04001186 RID: 4486
		protected bool mWasFromZoneRestart;

		// Token: 0x04001187 RID: 4487
		protected int mLevelScore;

		// Token: 0x04001188 RID: 4488
		protected int mTotalScore;

		// Token: 0x04001189 RID: 4489
		protected int mPointsFromLaser;

		// Token: 0x0400118A RID: 4490
		protected int mPointsFromCannon;

		// Token: 0x0400118B RID: 4491
		protected int mPointsFromColorNuke;

		// Token: 0x0400118C RID: 4492
		protected int mPointsFromProxBomb;

		// Token: 0x0400118D RID: 4493
		protected int[] mNumTimesActivatedPowerup = new int[14];

		// Token: 0x0400118E RID: 4494
		protected int[] mNumTimesSpawnedPowerup = new int[14];

		// Token: 0x0400118F RID: 4495
		public ZumaProfile mProfile;

		// Token: 0x0200010B RID: 267
		public enum Mode
		{
			// Token: 0x0400191C RID: 6428
			Mode_Challenge,
			// Token: 0x0400191D RID: 6429
			Mode_IronFrog,
			// Token: 0x0400191E RID: 6430
			Mode_Adventure,
			// Token: 0x0400191F RID: 6431
			Mode_HardAdventure,
			// Token: 0x04001920 RID: 6432
			Mode_None
		}
	}
}
