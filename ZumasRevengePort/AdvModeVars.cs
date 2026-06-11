using System;

namespace ZumasRevenge
{
	// Token: 0x0200003A RID: 58
	public class AdvModeVars
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0004D318 File Offset: 0x0004B518
		public AdvModeVars()
		{
			for (int i = 0; i < 6; i++)
			{
				this.mCheckpointScores[i] = new CheckpointScores();
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0004D378 File Offset: 0x0004B578
		public void CopyFrom(AdvModeVars rhs)
		{
			this.mHighestZoneBeat = rhs.mHighestZoneBeat;
			this.mHighestLevelBeat = rhs.mHighestLevelBeat;
			for (int i = 0; i < 6; i++)
			{
				this.mFirstTimeInZone[i] = rhs.mFirstTimeInZone[i];
			}
			this.mNumDeathsCurLevel = rhs.mNumDeathsCurLevel;
			this.mNumZumasCurLevel = rhs.mNumZumasCurLevel;
			this.mPerfectZone = rhs.mPerfectZone;
			for (int j = 0; j < 6; j++)
			{
				this.mNumTimesZoneBeat[j] = rhs.mNumTimesZoneBeat[j];
			}
			this.mDDSTier = rhs.mDDSTier;
			this.mRestartDDSTier = rhs.mRestartDDSTier;
			this.mCurrentAdvScore = rhs.mCurrentAdvScore;
			this.mCurrentAdvLevel = rhs.mCurrentAdvLevel;
			this.mCurrentAdvZone = rhs.mCurrentAdvZone;
			this.mCurrentAdvLives = rhs.mCurrentAdvLives;
			for (int k = 0; k < 60; k++)
			{
				this.mBestLevelTime[k] = rhs.mBestLevelTime[k];
			}
			for (int l = 0; l < 6; l++)
			{
				this.mCheckpointScores[l].CopyFrom(rhs.mCheckpointScores[l]);
			}
		}

		// Token: 0x04000D53 RID: 3411
		public int mHighestZoneBeat;

		// Token: 0x04000D54 RID: 3412
		public int mHighestLevelBeat;

		// Token: 0x04000D55 RID: 3413
		public bool[] mFirstTimeInZone = new bool[6];

		// Token: 0x04000D56 RID: 3414
		public int mNumDeathsCurLevel;

		// Token: 0x04000D57 RID: 3415
		public int mNumZumasCurLevel;

		// Token: 0x04000D58 RID: 3416
		public bool mPerfectZone;

		// Token: 0x04000D59 RID: 3417
		public int[] mNumTimesZoneBeat = new int[6];

		// Token: 0x04000D5A RID: 3418
		public int mDDSTier;

		// Token: 0x04000D5B RID: 3419
		public int mRestartDDSTier;

		// Token: 0x04000D5C RID: 3420
		public int mCurrentAdvScore;

		// Token: 0x04000D5D RID: 3421
		public int mCurrentAdvLevel;

		// Token: 0x04000D5E RID: 3422
		public int mCurrentAdvZone;

		// Token: 0x04000D5F RID: 3423
		public int mCurrentAdvLives;

		// Token: 0x04000D60 RID: 3424
		public int[] mBestLevelTime = new int[60];

		// Token: 0x04000D61 RID: 3425
		public CheckpointScores[] mCheckpointScores = new CheckpointScores[6];
	}
}
