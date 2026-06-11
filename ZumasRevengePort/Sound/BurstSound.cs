using System;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000152 RID: 338
	internal class BurstSound : BasicSound
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x000A7E59 File Offset: 0x000A6059
		public BurstSound(int inSoundID, SoundManager inSoundManager, bool inAutoRelease)
		{
			this.m_SoundID = inSoundID;
			this.m_SoundManager = inSoundManager;
			this.mAutoRelease = inAutoRelease;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000A7E81 File Offset: 0x000A6081
		public override void Dispose()
		{
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000A7E83 File Offset: 0x000A6083
		public override void Play()
		{
			if (this.mPaused || !this.ReleaseInstance() || !this.FindFreeSoundInstance(ref this.mSoundInstance))
			{
				return;
			}
			this.SetAttributes(this.mSoundInstance);
			this.mSoundInstance.Play(false, this.mAutoRelease);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000A7EC4 File Offset: 0x000A60C4
		protected override bool FindFreeSoundInstance(ref SoundInstance outInstance)
		{
			SoundInstance soundInstance = this.m_SoundManager.GetSoundInstance(this.m_SoundID);
			if (soundInstance != null)
			{
				outInstance = soundInstance;
			}
			return soundInstance != null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000A7EF0 File Offset: 0x000A60F0
		private void SetAttributes(SoundInstance inInstance)
		{
			if (this.mPan != 0)
			{
				inInstance.SetPan(this.mPan);
			}
			inInstance.AdjustPitch((double)this.mPitch);
			inInstance.SetVolume(this.m_SoundManager.GetMasterVolume());
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000A7F24 File Offset: 0x000A6124
		private bool ReleaseInstance()
		{
			if (this.mSoundInstance != null && !this.mAutoRelease)
			{
				if (this.mSoundInstance.IsPlaying())
				{
					return false;
				}
				this.mSoundInstance.Release();
				this.mSoundInstance = null;
			}
			return true;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000A7F58 File Offset: 0x000A6158
		public override void Fade()
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000A7F5A File Offset: 0x000A615A
		public override void Update()
		{
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000A7F5C File Offset: 0x000A615C
		public override float GetOptionVolume()
		{
			return 0f;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000A7F63 File Offset: 0x000A6163
		public override void Pause(bool inPauseOn)
		{
			this.mPaused = inPauseOn;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000A7F6C File Offset: 0x000A616C
		public override bool IsFree()
		{
			return this.mAutoRelease || this.mSoundInstance == null || !this.mSoundInstance.IsPlaying();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000A7F8E File Offset: 0x000A618E
		public override bool IsFading()
		{
			return false;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000A7F91 File Offset: 0x000A6191
		public override bool IsLooping()
		{
			return false;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000A7F94 File Offset: 0x000A6194
		public override float GetVolume()
		{
			if (!this.mPaused)
			{
				return this.mVolume;
			}
			return 0f;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000A7FAA File Offset: 0x000A61AA
		public override void SetPan(int inPan)
		{
			this.mPan = inPan;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000A7FB3 File Offset: 0x000A61B3
		public override void SetPitch(float inPitch)
		{
			this.mPitch = inPitch;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000A7FBC File Offset: 0x000A61BC
		public override void SetVolume(float inVolume)
		{
			this.mVolume = inVolume;
			this.m_SoundManager.SetVolume((double)this.mVolume);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000A7FD7 File Offset: 0x000A61D7
		public override void EnableAutoUnload()
		{
			this.mUnloadSource = true;
		}

		// Token: 0x04001AD4 RID: 6868
		private SoundInstance mSoundInstance;

		// Token: 0x04001AD5 RID: 6869
		private bool mAutoRelease;

		// Token: 0x04001AD6 RID: 6870
		private bool mPaused;

		// Token: 0x04001AD7 RID: 6871
		private bool mUnloadSource;

		// Token: 0x04001AD8 RID: 6872
		private int mPan;

		// Token: 0x04001AD9 RID: 6873
		private float mPitch;

		// Token: 0x04001ADA RID: 6874
		private float mVolume = 1f;
	}
}
