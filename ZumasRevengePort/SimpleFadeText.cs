using System;

namespace ZumasRevenge
{
	// Token: 0x02000066 RID: 102
	public class SimpleFadeText
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0005E0CD File Offset: 0x0005C2CD
		public SimpleFadeText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0005E0E7 File Offset: 0x0005C2E7
		public SimpleFadeText(string str) : this()
		{
			this.mString = str;
		}

		// Token: 0x04001299 RID: 4761
		public string mString;

		// Token: 0x0400129A RID: 4762
		public float mAlpha;

		// Token: 0x0400129B RID: 4763
		public bool mFadeIn;
	}
}
