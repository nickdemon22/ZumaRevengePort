using System;

namespace ZumasRevenge
{
	// Token: 0x0200004D RID: 77
	public class ChallengeTempleStats
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00056890 File Offset: 0x00054A90
		public void CopyFrom(ChallengeTempleStats rhs)
		{
			this.mHighestScore = rhs.mHighestScore;
			this.mNumTimesHitScoreTarget = rhs.mNumTimesHitScoreTarget;
			this.mHighestMult = rhs.mHighestMult;
			for (int i = 0; i < 70; i++)
			{
				this.mNumTimesPlayedCurve[i] = rhs.mNumTimesPlayedCurve[i];
			}
			this.mTotalTime = rhs.mTotalTime;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000568EC File Offset: 0x00054AEC
		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mHighestScore);
			theSync.SyncLong(ref this.mNumTimesHitScoreTarget);
			theSync.SyncLong(ref this.mHighestMult);
			for (int i = 0; i < 70; i++)
			{
				theSync.SyncLong(ref this.mNumTimesPlayedCurve[i]);
			}
			theSync.SyncLong(ref this.mTotalTime);
		}

		// Token: 0x04001163 RID: 4451
		public int mHighestScore;

		// Token: 0x04001164 RID: 4452
		public int mNumTimesHitScoreTarget;

		// Token: 0x04001165 RID: 4453
		public int mHighestMult;

		// Token: 0x04001166 RID: 4454
		public int[] mNumTimesPlayedCurve = new int[70];

		// Token: 0x04001167 RID: 4455
		public int mTotalTime;
	}
}
