using System;

namespace ZumasRevenge
{
	// Token: 0x02000065 RID: 101
	public class GameStats
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0005DE8C File Offset: 0x0005C08C
		public GameStats()
		{
			this.mTimePlayed = 0;
			this.mNumBallsCleared = 0;
			this.mNumGemsCleared = 0;
			this.mNumGaps = 0;
			this.mNumCombos = 0;
			this.mMaxCombo = -1;
			this.mMaxComboScore = 0;
			this.mMaxInARow = 0;
			this.mMaxInARowScore = 0;
			this.mDangerTimePlayed = 0;
			this.mTotalShots = (this.mNumMisses = 0);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
		public void Reset()
		{
			this.mTimePlayed = 0;
			this.mNumBallsCleared = 0;
			this.mNumGemsCleared = 0;
			this.mNumGaps = 0;
			this.mNumCombos = 0;
			this.mMaxCombo = -1;
			this.mMaxComboScore = 0;
			this.mMaxInARow = 0;
			this.mMaxInARowScore = 0;
			this.mDangerTimePlayed = 0;
			this.mTotalShots = (this.mNumMisses = 0);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0005DF5C File Offset: 0x0005C15C
		public void Add(GameStats theStats)
		{
			this.mTimePlayed += theStats.mTimePlayed;
			this.mNumBallsCleared += theStats.mNumBallsCleared;
			this.mNumGemsCleared += theStats.mNumGemsCleared;
			this.mNumCombos += theStats.mNumCombos;
			this.mNumGaps += theStats.mNumGaps;
			if (theStats.mMaxCombo > this.mMaxCombo || (theStats.mMaxCombo == this.mMaxCombo && theStats.mMaxComboScore > this.mMaxComboScore))
			{
				this.mMaxCombo = theStats.mMaxCombo;
				this.mMaxComboScore = theStats.mMaxComboScore;
			}
			if (theStats.mMaxInARow > this.mMaxInARow)
			{
				this.mMaxInARow = theStats.mMaxInARow;
				this.mMaxInARowScore = theStats.mMaxInARowScore;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0005E030 File Offset: 0x0005C230
		public void SyncState(DataSync theSync)
		{
			theSync.SyncLong(ref this.mTimePlayed);
			theSync.SyncLong(ref this.mNumBallsCleared);
			theSync.SyncLong(ref this.mNumGemsCleared);
			theSync.SyncLong(ref this.mNumGaps);
			theSync.SyncLong(ref this.mNumCombos);
			theSync.SyncLong(ref this.mMaxCombo);
			theSync.SyncLong(ref this.mMaxComboScore);
			theSync.SyncLong(ref this.mMaxInARow);
			theSync.SyncLong(ref this.mMaxInARowScore);
			theSync.SyncLong(ref this.mDangerTimePlayed);
			theSync.SyncLong(ref this.mTotalShots);
			theSync.SyncLong(ref this.mNumMisses);
		}

		// Token: 0x0400128D RID: 4749
		public int mTimePlayed;

		// Token: 0x0400128E RID: 4750
		public int mDangerTimePlayed;

		// Token: 0x0400128F RID: 4751
		public int mNumBallsCleared;

		// Token: 0x04001290 RID: 4752
		public int mNumGemsCleared;

		// Token: 0x04001291 RID: 4753
		public int mNumGaps;

		// Token: 0x04001292 RID: 4754
		public int mNumCombos;

		// Token: 0x04001293 RID: 4755
		public int mMaxCombo;

		// Token: 0x04001294 RID: 4756
		public int mMaxComboScore;

		// Token: 0x04001295 RID: 4757
		public int mMaxInARow;

		// Token: 0x04001296 RID: 4758
		public int mMaxInARowScore;

		// Token: 0x04001297 RID: 4759
		public int mTotalShots;

		// Token: 0x04001298 RID: 4760
		public int mNumMisses;
	}
}
