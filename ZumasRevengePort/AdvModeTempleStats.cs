using System;

namespace ZumasRevenge
{
	// Token: 0x0200004C RID: 76
	public class AdvModeTempleStats
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00056728 File Offset: 0x00054928
		public void CopyFrom(AdvModeTempleStats rhs)
		{
			this.mHighestLevel = rhs.mHighestLevel;
			this.mBestTime = rhs.mBestTime;
			this.mBestScore = rhs.mBestScore;
			this.mNumLevelsAced = rhs.mNumLevelsAced;
			this.mNumPerfectLevels = rhs.mNumPerfectLevels;
			this.mNumClearCurves = rhs.mNumClearCurves;
			for (int i = 0; i < 6; i++)
			{
				this.mBossDeaths[i] = rhs.mBossDeaths[i];
			}
			for (int j = 0; j < 60; j++)
			{
				this.mLevelDeaths[j] = rhs.mLevelDeaths[j];
			}
			this.mTotalTimePlayed = rhs.mTotalTimePlayed;
			this.mCurrentTime = rhs.mCurrentTime;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000567D0 File Offset: 0x000549D0
		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mHighestLevel);
			theSync.SyncLong(ref this.mBestTime);
			theSync.SyncLong(ref this.mBestScore);
			theSync.SyncLong(ref this.mNumLevelsAced);
			theSync.SyncLong(ref this.mNumPerfectLevels);
			theSync.SyncLong(ref this.mNumClearCurves);
			for (int i = 0; i < 6; i++)
			{
				theSync.SyncLong(ref this.mBossDeaths[i]);
			}
			for (int j = 0; j < 60; j++)
			{
				theSync.SyncLong(ref this.mLevelDeaths[j]);
			}
			theSync.SyncLong(ref this.mTotalTimePlayed);
			theSync.SyncLong(ref this.mCurrentTime);
		}

		// Token: 0x04001159 RID: 4441
		public int mHighestLevel;

		// Token: 0x0400115A RID: 4442
		public int mBestTime = int.MaxValue;

		// Token: 0x0400115B RID: 4443
		public int mBestScore;

		// Token: 0x0400115C RID: 4444
		public int mNumLevelsAced;

		// Token: 0x0400115D RID: 4445
		public int mNumPerfectLevels;

		// Token: 0x0400115E RID: 4446
		public int mNumClearCurves;

		// Token: 0x0400115F RID: 4447
		public int[] mBossDeaths = new int[6];

		// Token: 0x04001160 RID: 4448
		public int[] mLevelDeaths = new int[60];

		// Token: 0x04001161 RID: 4449
		public int mTotalTimePlayed;

		// Token: 0x04001162 RID: 4450
		public int mCurrentTime;
	}
}
