using System;

namespace ZumasRevenge
{
	// Token: 0x020000F5 RID: 245
	internal struct Song
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x0009C319 File Offset: 0x0009A519
		public Song(int inID, bool inLoop, float inFadeSpeed)
		{
			this.mID = inID;
			this.mLoop = inLoop;
			this.mFadeSpeed = inFadeSpeed;
		}

		// Token: 0x0400187D RID: 6269
		public int mID;

		// Token: 0x0400187E RID: 6270
		public bool mLoop;

		// Token: 0x0400187F RID: 6271
		public float mFadeSpeed;

		// Token: 0x04001880 RID: 6272
		public static Song DefaultSong = new Song(-1, false, 1f);
	}
}
