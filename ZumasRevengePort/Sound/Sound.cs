using System;

namespace ZumasRevenge.Sound
{
	// Token: 0x020000DA RID: 218
	public abstract class Sound : IDisposable
	{
		// Token: 0x06000ECA RID: 3786
		public abstract void Dispose();

		// Token: 0x06000ECB RID: 3787
		public abstract void Play();

		// Token: 0x06000ECC RID: 3788
		public abstract void Fade();

		// Token: 0x06000ECD RID: 3789
		public abstract void Update();

		// Token: 0x06000ECE RID: 3790
		public abstract void Pause(bool inPauseOn);

		// Token: 0x06000ECF RID: 3791
		public abstract bool IsFree();

		// Token: 0x06000ED0 RID: 3792
		public abstract bool IsFading();

		// Token: 0x06000ED1 RID: 3793
		public abstract bool IsLooping();

		// Token: 0x06000ED2 RID: 3794
		public abstract float GetVolume();

		// Token: 0x06000ED3 RID: 3795
		public abstract float GetOptionVolume();

		// Token: 0x06000ED4 RID: 3796
		public abstract void SetPan(int inPan);

		// Token: 0x06000ED5 RID: 3797
		public abstract void SetPitch(float inPitch);

		// Token: 0x06000ED6 RID: 3798
		public abstract void SetVolume(float inVolume);

		// Token: 0x06000ED7 RID: 3799
		public abstract void EnableAutoUnload();
	}
}
