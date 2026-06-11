using System;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000155 RID: 341
	internal class FadedSound : UpdatedSound
	{
		// Token: 0x06001066 RID: 4198 RVA: 0x000A82A6 File Offset: 0x000A64A6
		public FadedSound(Sound inSound, float inFadeInSpeed, float inFadeOutSpeed)
		{
			this.mSound = inSound;
			this.mFadeInSpeed = inFadeInSpeed;
			this.mFadeOutSpeed = inFadeOutSpeed;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000A82D5 File Offset: 0x000A64D5
		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000A82E4 File Offset: 0x000A64E4
		public override void Play()
		{
			if (!this.mIsFree)
			{
				return;
			}
			this.mIsFree = false;
			this.mTargetVolume = this.mSound.GetVolume();
			this.mSound.SetVolume(0f);
			this.mSound.Play();
			this.mIsFadeOut = false;
			this.mIsPaused = false;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000A833B File Offset: 0x000A653B
		public override void Fade()
		{
			this.mTargetVolume = 0f;
			this.mIsFadeOut = true;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000A8350 File Offset: 0x000A6550
		public override void Update()
		{
			if (this.mIsPaused)
			{
				return;
			}
			float num = this.mSound.GetVolume();
			if (num == this.mTargetVolume && !this.mIsFadeOut)
			{
				return;
			}
			if (this.mTargetVolume == 0f || this.mIsFadeOut)
			{
				num -= this.mFadeOutSpeed;
				if (num < 0f)
				{
					num = 0f;
					this.mIsFadeOut = false;
					this.mSound.Fade();
				}
			}
			else
			{
				num += this.mFadeInSpeed;
				if (num > this.mTargetVolume)
				{
					num = this.mTargetVolume;
				}
			}
			if (num == 0f)
			{
				this.mIsFree = true;
				return;
			}
			this.mSound.SetVolume(num);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000A83F9 File Offset: 0x000A65F9
		public override void Pause(bool inPauseOn)
		{
			if (!inPauseOn)
			{
				this.RestoreTargetVolume();
			}
			this.mIsPaused = inPauseOn;
			this.mSound.Pause(inPauseOn);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000A8417 File Offset: 0x000A6617
		public override bool IsFree()
		{
			return this.mIsFree;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000A841F File Offset: 0x000A661F
		public override bool IsFading()
		{
			return this.mTargetVolume == 0f && this.mSound.GetVolume() > 0f;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000A8442 File Offset: 0x000A6642
		public override bool IsLooping()
		{
			return this.mSound.IsLooping();
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000A844F File Offset: 0x000A664F
		public override float GetVolume()
		{
			return this.mSound.GetVolume();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000A845C File Offset: 0x000A665C
		public override void SetPan(int inPan)
		{
			this.mSound.SetPan(inPan);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000A846A File Offset: 0x000A666A
		public override void SetPitch(float inPitch)
		{
			this.mSound.SetPitch(inPitch);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000A8478 File Offset: 0x000A6678
		public override void SetVolume(float inVolume)
		{
			this.mSound.SetVolume(inVolume);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000A8486 File Offset: 0x000A6686
		public override void EnableAutoUnload()
		{
			this.mSound.EnableAutoUnload();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000A8493 File Offset: 0x000A6693
		public override float GetOptionVolume()
		{
			return 0f;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000A849A File Offset: 0x000A669A
		protected void CacheTargetVolume()
		{
			if (this.mTargetVolume == 0f)
			{
				return;
			}
			this.mLastTarget = this.mTargetVolume;
			this.mTargetVolume = 0f;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000A84C1 File Offset: 0x000A66C1
		protected void RestoreTargetVolume()
		{
			if (this.mIsFadeOut)
			{
				return;
			}
			this.mTargetVolume = this.mSound.GetOptionVolume();
		}

		// Token: 0x04001AE3 RID: 6883
		private bool mIsFree = true;

		// Token: 0x04001AE4 RID: 6884
		private float mFadeInSpeed;

		// Token: 0x04001AE5 RID: 6885
		private float mFadeOutSpeed;

		// Token: 0x04001AE6 RID: 6886
		private float mTargetVolume;

		// Token: 0x04001AE7 RID: 6887
		private float mLastTarget = -1f;

		// Token: 0x04001AE8 RID: 6888
		private bool mIsPaused;

		// Token: 0x04001AE9 RID: 6889
		private bool mIsFadeOut;
	}
}
