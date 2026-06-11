using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000044 RID: 68
	public class AchievementText
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00055D38 File Offset: 0x00053F38
		public AchievementText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
			this.mX = 0;
			this.mY = 0;
			this.mUnlocked = false;
		}

		// Token: 0x0400110D RID: 4365
		public string mHeaderStr = "";

		// Token: 0x0400110E RID: 4366
		public string mValueStr = "";

		// Token: 0x0400110F RID: 4367
		public string mDescStr = "";

		// Token: 0x04001110 RID: 4368
		public string mPointStr = "";

		// Token: 0x04001111 RID: 4369
		public float mAlpha;

		// Token: 0x04001112 RID: 4370
		public bool mFadeIn;

		// Token: 0x04001113 RID: 4371
		public int mX;

		// Token: 0x04001114 RID: 4372
		public int mY;

		// Token: 0x04001115 RID: 4373
		public Image mIcon;

		// Token: 0x04001116 RID: 4374
		public bool mUnlocked;
	}
}
