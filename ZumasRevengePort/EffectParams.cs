using System;

namespace ZumasRevenge
{
	// Token: 0x020000A6 RID: 166
	public class EffectParams
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0008C29A File Offset: 0x0008A49A
		public EffectParams()
		{
			this.mEffectIndex = -1;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0008C2A9 File Offset: 0x0008A4A9
		public EffectParams(string k, string v, int i)
		{
			this.mKey = k;
			this.mValue = v;
			this.mEffectIndex = i;
		}

		// Token: 0x0400163A RID: 5690
		public string mKey;

		// Token: 0x0400163B RID: 5691
		public string mValue;

		// Token: 0x0400163C RID: 5692
		public int mEffectIndex;
	}
}
