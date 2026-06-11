using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000146 RID: 326
	public class LeaderBoardText
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x000A4520 File Offset: 0x000A2720
		public LeaderBoardText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
			this.mX = 0;
			this.mY = 0;
		}

		// Token: 0x04001A90 RID: 6800
		public string mHeaderStr = "";

		// Token: 0x04001A91 RID: 6801
		public string mValueStr = "";

		// Token: 0x04001A92 RID: 6802
		public float mAlpha;

		// Token: 0x04001A93 RID: 6803
		public bool mFadeIn;

		// Token: 0x04001A94 RID: 6804
		public int mX;

		// Token: 0x04001A95 RID: 6805
		public int mY;

		// Token: 0x04001A96 RID: 6806
		public Image mIcon;

		// Token: 0x04001A97 RID: 6807
		public bool mShowIcon;
	}
}
