using System;

namespace ZumasRevenge
{
	// Token: 0x0200003B RID: 59
	public class IronFrogTempleStats
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0004D494 File Offset: 0x0004B694
		public void CopyFrom(IronFrogTempleStats rhs)
		{
			this.mNumAttempts = rhs.mNumAttempts;
			this.mNumVictories = rhs.mNumVictories;
			this.mBestTime = rhs.mBestTime;
			this.mCurTime = rhs.mCurTime;
			this.mBestScore = rhs.mBestScore;
			this.mHighestLevel = rhs.mHighestLevel;
			for (int i = 0; i < 10; i++)
			{
				this.mLevelDeaths[i] = rhs.mLevelDeaths[i];
			}
			this.mTotalTimePlayed = rhs.mTotalTimePlayed;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0004D514 File Offset: 0x0004B714
		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mNumAttempts);
			theSync.SyncLong(ref this.mNumVictories);
			theSync.SyncLong(ref this.mBestTime);
			theSync.SyncLong(ref this.mCurTime);
			theSync.SyncLong(ref this.mBestScore);
			theSync.SyncLong(ref this.mHighestLevel);
			for (int i = 0; i < 10; i++)
			{
				theSync.SyncLong(ref this.mLevelDeaths[i]);
			}
			theSync.SyncLong(ref this.mTotalTimePlayed);
		}

		// Token: 0x04000D62 RID: 3426
		public int mNumAttempts;

		// Token: 0x04000D63 RID: 3427
		public int mNumVictories;

		// Token: 0x04000D64 RID: 3428
		public int mBestTime;

		// Token: 0x04000D65 RID: 3429
		public int mCurTime;

		// Token: 0x04000D66 RID: 3430
		public int mBestScore;

		// Token: 0x04000D67 RID: 3431
		public int mHighestLevel;

		// Token: 0x04000D68 RID: 3432
		public int[] mLevelDeaths = new int[10];

		// Token: 0x04000D69 RID: 3433
		public int mTotalTimePlayed;
	}
}
