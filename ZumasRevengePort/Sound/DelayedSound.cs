using System;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000154 RID: 340
	internal class DelayedSound : UpdatedSound
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x000A814F File Offset: 0x000A634F
		public DelayedSound(Sound inSound, int inDelay)
		{
			this.mSound = inSound;
			this.mDelay = inDelay;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000A816C File Offset: 0x000A636C
		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000A8179 File Offset: 0x000A6379
		public override void Play()
		{
			if (this.mUpdateCount > 0)
			{
				return;
			}
			this.mIsFree = false;
			this.mDoCountdown = true;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000A8193 File Offset: 0x000A6393
		public override void Fade()
		{
			this.mSound.Fade();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000A81A0 File Offset: 0x000A63A0
		public override void Update()
		{
			if (this.mDoCountdown)
			{
				this.mUpdateCount++;
			}
			if (this.mUpdateCount == this.mDelay)
			{
				this.mSound.Play();
				return;
			}
			if (this.mUpdateCount > this.mDelay)
			{
				this.mIsFree = true;
				this.mDoCountdown = false;
				this.mSound.Update();
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000A8204 File Offset: 0x000A6404
		public override void Pause(bool inPauseOn)
		{
			if (this.mUpdateCount <= this.mDelay)
			{
				this.mDoCountdown = !inPauseOn;
			}
			this.mSound.Pause(inPauseOn);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000A822A File Offset: 0x000A642A
		public override float GetOptionVolume()
		{
			return 0f;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000A8231 File Offset: 0x000A6431
		public override bool IsFree()
		{
			return this.mIsFree && this.mSound.IsFree();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000A8248 File Offset: 0x000A6448
		public override bool IsFading()
		{
			return this.mSound.IsFading();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000A8255 File Offset: 0x000A6455
		public override bool IsLooping()
		{
			return this.mSound.IsLooping();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000A8262 File Offset: 0x000A6462
		public override float GetVolume()
		{
			return this.mSound.GetVolume();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x000A826F File Offset: 0x000A646F
		public override void SetPan(int inPan)
		{
			this.mSound.SetPan(inPan);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000A827D File Offset: 0x000A647D
		public override void SetPitch(float inPitch)
		{
			this.mSound.SetPitch(inPitch);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000A828B File Offset: 0x000A648B
		public override void SetVolume(float inVolume)
		{
			this.mSound.SetVolume(inVolume);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000A8299 File Offset: 0x000A6499
		public override void EnableAutoUnload()
		{
			this.mSound.EnableAutoUnload();
		}

		// Token: 0x04001ADF RID: 6879
		private bool mIsFree = true;

		// Token: 0x04001AE0 RID: 6880
		private bool mDoCountdown;

		// Token: 0x04001AE1 RID: 6881
		private int mDelay;

		// Token: 0x04001AE2 RID: 6882
		private int mUpdateCount;
	}
}
