using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x020000C1 RID: 193
	public class EffectItem
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x0008EE7C File Offset: 0x0008D07C
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
			sync.SyncLong(ref this.mColor.mAlpha);
			this.SyncListComponents(sync, this.mScale, true);
			this.SyncListComponents(sync, this.mOpacity, true);
			this.SyncListComponents(sync, this.mAngle, true);
			this.SyncListComponents(sync, this.mXOffset, true);
			this.SyncListComponents(sync, this.mYOffset, true);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0008EF20 File Offset: 0x0008D120
		private void SyncListComponents(DataSync sync, List<Component> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Component component = new Component();
					component.SyncState(sync);
					theList.Add(component);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Component component2 in theList)
			{
				component2.SyncState(sync);
			}
		}

		// Token: 0x040016E0 RID: 5856
		public Image mImage;

		// Token: 0x040016E1 RID: 5857
		public List<Component> mScale = new List<Component>();

		// Token: 0x040016E2 RID: 5858
		public List<Component> mOpacity = new List<Component>();

		// Token: 0x040016E3 RID: 5859
		public List<Component> mAngle = new List<Component>();

		// Token: 0x040016E4 RID: 5860
		public List<Component> mXOffset = new List<Component>();

		// Token: 0x040016E5 RID: 5861
		public List<Component> mYOffset = new List<Component>();

		// Token: 0x040016E6 RID: 5862
		public int mCel;

		// Token: 0x040016E7 RID: 5863
		public Color mColor = default(Color);
	}
}
