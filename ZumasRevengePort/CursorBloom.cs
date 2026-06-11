using System;

namespace ZumasRevenge
{
	// Token: 0x02000072 RID: 114
	public class CursorBloom
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0006C37A File Offset: 0x0006A57A
		public void Reset()
		{
			this.mScale = 0f;
			this.mX = 0;
			this.mY = 0;
			this.mAlpha = 255;
		}

		// Token: 0x04001382 RID: 4994
		public float mScale;

		// Token: 0x04001383 RID: 4995
		public int mX;

		// Token: 0x04001384 RID: 4996
		public int mY;

		// Token: 0x04001385 RID: 4997
		public int mAlpha = 255;
	}
}
