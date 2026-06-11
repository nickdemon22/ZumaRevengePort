using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200007D RID: 125
	public class BossText
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x00078B61 File Offset: 0x00076D61
		public BossText()
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00078B87 File Offset: 0x00076D87
		public BossText(string t)
		{
			this.mAlpha = 0f;
			this.mText = t;
		}

		// Token: 0x04001441 RID: 5185
		public string mText = "";

		// Token: 0x04001442 RID: 5186
		public int mTextId = -1;

		// Token: 0x04001443 RID: 5187
		public float mAlpha;

		// Token: 0x04001444 RID: 5188
		public Color mColor = default(Color);
	}
}
