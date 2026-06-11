using System;

namespace ZumasRevenge
{
	// Token: 0x020000B4 RID: 180
	public class TauntText
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0008D7A0 File Offset: 0x0008B9A0
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mTextId);
			sync.SyncLong(ref this.mMinDeaths);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mCondition);
			sync.SyncLong(ref this.mMinTime);
			sync.SyncLong(ref this.mUpdateCount);
			if (sync.isRead())
			{
				this.mText = TextManager.getInstance().getString(this.mTextId);
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0008D813 File Offset: 0x0008BA13
		public TauntText()
		{
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0008D838 File Offset: 0x0008BA38
		public TauntText(TauntText rhs)
		{
			this.mText = rhs.mText;
			this.mMinDeaths = rhs.mMinDeaths;
			this.mDelay = rhs.mDelay;
			this.mCondition = rhs.mCondition;
			this.mMinTime = rhs.mMinTime;
			this.mUpdateCount = rhs.mUpdateCount;
		}

		// Token: 0x04001682 RID: 5762
		public string mText;

		// Token: 0x04001683 RID: 5763
		public int mTextId = -1;

		// Token: 0x04001684 RID: 5764
		public int mMinDeaths = -1;

		// Token: 0x04001685 RID: 5765
		public int mDelay = 100;

		// Token: 0x04001686 RID: 5766
		public int mCondition = -1;

		// Token: 0x04001687 RID: 5767
		public int mMinTime;

		// Token: 0x04001688 RID: 5768
		public int mUpdateCount;
	}
}
