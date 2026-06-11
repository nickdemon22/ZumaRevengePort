using System;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000156 RID: 342
	internal class StaggeredSound : UpdatedSound
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x000A84DD File Offset: 0x000A66DD
		public StaggeredSound(Sound inSound, int inStaggerTime)
		{
			this.mSound = inSound;
			this.mStaggerTime = inStaggerTime;
			this.mStaggerCount = inStaggerTime;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000A84FA File Offset: 0x000A66FA
		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000A8507 File Offset: 0x000A6707
		public override void Play()
		{
			if (this.mStaggerCount < this.mStaggerTime)
			{
				return;
			}
			this.mStaggerCount = 0;
			this.mSound.Play();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000A852A File Offset: 0x000A672A
		public override void Fade()
		{
			this.mSound.Fade();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000A8537 File Offset: 0x000A6737
		public override void Update()
		{
			this.mStaggerCount++;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000A8547 File Offset: 0x000A6747
		public override float GetOptionVolume()
		{
			return 0f;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000A854E File Offset: 0x000A674E
		public override void Pause(bool inPauseOn)
		{
			this.mSound.Pause(inPauseOn);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000A855C File Offset: 0x000A675C
		public override bool IsFree()
		{
			return this.mStaggerCount > this.mStaggerTime * 1000 && this.mSound.IsFree();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000A857F File Offset: 0x000A677F
		public override bool IsFading()
		{
			return this.mSound.IsFading();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000A858C File Offset: 0x000A678C
		public override bool IsLooping()
		{
			return this.mSound.IsLooping();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000A8599 File Offset: 0x000A6799
		public override float GetVolume()
		{
			return this.mSound.GetVolume();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000A85A6 File Offset: 0x000A67A6
		public override void SetPan(int inPan)
		{
			this.mSound.SetPan(inPan);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000A85B4 File Offset: 0x000A67B4
		public override void SetPitch(float inPitch)
		{
			this.mSound.SetPitch(inPitch);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000A85C2 File Offset: 0x000A67C2
		public override void SetVolume(float inVolume)
		{
			this.mSound.SetVolume(inVolume);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000A85D0 File Offset: 0x000A67D0
		public override void EnableAutoUnload()
		{
			this.mSound.EnableAutoUnload();
		}

		// Token: 0x04001AEA RID: 6890
		private int mStaggerTime;

		// Token: 0x04001AEB RID: 6891
		private int mStaggerCount;
	}
}
