using System;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000153 RID: 339
	internal class LoopingSound : BasicSound
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x000A7FE0 File Offset: 0x000A61E0
		public LoopingSound(int inSoundID, SoundManager inSoundManager)
		{
			this.m_SoundID = inSoundID;
			this.m_SoundManager = inSoundManager;
			this.mVolume = (float)this.m_SoundManager.GetMasterVolume();
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000A8013 File Offset: 0x000A6213
		public override void Dispose()
		{
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000A8018 File Offset: 0x000A6218
		public override void Play()
		{
			if (this.mSoundInstance != null || !this.FindFreeSoundInstance(ref this.mSoundInstance))
			{
				return;
			}
			this.mVolume = (float)this.m_SoundManager.GetMasterVolume();
			this.mSoundInstance.SetVolume((double)this.GetVolume());
			this.mSoundInstance.Play(true, false);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000A8070 File Offset: 0x000A6270
		protected override bool FindFreeSoundInstance(ref SoundInstance outInstance)
		{
			SoundInstance soundInstance = this.m_SoundManager.GetSoundInstance(this.m_SoundID);
			if (soundInstance != null)
			{
				outInstance = soundInstance;
			}
			return soundInstance != null;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000A809C File Offset: 0x000A629C
		public override void Fade()
		{
			if (this.mSoundInstance != null)
			{
				this.mSoundInstance.Stop();
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000A80B1 File Offset: 0x000A62B1
		public override void Update()
		{
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000A80B3 File Offset: 0x000A62B3
		public override float GetOptionVolume()
		{
			return (float)this.m_SoundManager.GetMasterVolume();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000A80C1 File Offset: 0x000A62C1
		public override void Pause(bool inPauseOn)
		{
			this.mPaused = inPauseOn;
			if (this.mSoundInstance == null)
			{
				return;
			}
			if (this.mPaused)
			{
				this.mSoundInstance.Pause();
				return;
			}
			this.mSoundInstance.Resume();
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000A80F2 File Offset: 0x000A62F2
		public override bool IsFree()
		{
			return this.mSoundInstance == null;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000A80FD File Offset: 0x000A62FD
		public override bool IsFading()
		{
			return false;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000A8100 File Offset: 0x000A6300
		public override bool IsLooping()
		{
			return this.mSoundInstance != null;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000A810E File Offset: 0x000A630E
		public override float GetVolume()
		{
			if (!this.mPaused)
			{
				return this.mVolume;
			}
			return 0f;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000A8124 File Offset: 0x000A6324
		public override void SetPan(int inPan)
		{
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000A8126 File Offset: 0x000A6326
		public override void SetPitch(float inPitch)
		{
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000A8128 File Offset: 0x000A6328
		public override void SetVolume(float inVolume)
		{
			this.mVolume = inVolume;
			if (this.mSoundInstance != null)
			{
				this.mSoundInstance.SetVolume((double)inVolume);
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000A8146 File Offset: 0x000A6346
		public override void EnableAutoUnload()
		{
			this.mUnloadSource = true;
		}

		// Token: 0x04001ADB RID: 6875
		private SoundInstance mSoundInstance;

		// Token: 0x04001ADC RID: 6876
		public bool mPaused;

		// Token: 0x04001ADD RID: 6877
		private bool mUnloadSource;

		// Token: 0x04001ADE RID: 6878
		private float mVolume = 1f;
	}
}
