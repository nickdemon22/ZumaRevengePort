using System;

namespace ZumasRevenge
{
	// Token: 0x020000B6 RID: 182
	public class HulaEntry
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x0008E591 File Offset: 0x0008C791
		public HulaEntry()
		{
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0008E59C File Offset: 0x0008C79C
		public HulaEntry(HulaEntry rhs)
		{
			this.mBerserkAmt = rhs.mBerserkAmt;
			this.mAmnesty = rhs.mAmnesty;
			this.mVX = rhs.mVX;
			this.mProjVY = rhs.mProjVY;
			this.mSpawnY = rhs.mSpawnY;
			this.mSpawnRate = rhs.mSpawnRate;
			this.mProjChance = rhs.mProjChance;
			this.mAttackType = rhs.mAttackType;
			this.mAttackTime = rhs.mAttackTime;
			this.mProjRange = rhs.mProjRange;
		}

		// Token: 0x0400169A RID: 5786
		public int mBerserkAmt;

		// Token: 0x0400169B RID: 5787
		public int mAmnesty;

		// Token: 0x0400169C RID: 5788
		public float mVX;

		// Token: 0x0400169D RID: 5789
		public float mProjVY;

		// Token: 0x0400169E RID: 5790
		public int mSpawnY;

		// Token: 0x0400169F RID: 5791
		public int mSpawnRate;

		// Token: 0x040016A0 RID: 5792
		public int mProjChance;

		// Token: 0x040016A1 RID: 5793
		public int mAttackType;

		// Token: 0x040016A2 RID: 5794
		public int mAttackTime;

		// Token: 0x040016A3 RID: 5795
		public int mProjRange;

		// Token: 0x020000BA RID: 186
		public enum AttackType
		{
			// Token: 0x040016BA RID: 5818
			Attack_None,
			// Token: 0x040016BB RID: 5819
			Attack_Stun,
			// Token: 0x040016BC RID: 5820
			Attack_Poison,
			// Token: 0x040016BD RID: 5821
			Attack_Hallucinate,
			// Token: 0x040016BE RID: 5822
			Attack_Slow
		}
	}
}
