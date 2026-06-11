using System;
using System.Collections.Generic;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000029 RID: 41
	public class SoundEffects : IDisposable
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x00040ACC File Offset: 0x0003ECCC
		public SoundEffects(SoundManager soundManager)
		{
			this.m_SoundManager = soundManager;
			SoundFactory.SetSoundManager(soundManager);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00040B0C File Offset: 0x0003ED0C
		public void Dispose()
		{
			this.StopAll();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00040B14 File Offset: 0x0003ED14
		public void Play(int inSoundID)
		{
			this.Play(inSoundID, new SoundAttribs());
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00040B24 File Offset: 0x0003ED24
		public void Play(int inSoundID, SoundAttribs inAttribs)
		{
			Sound sound = null;
			if (this.mSounds.ContainsKey(inSoundID))
			{
				sound = this.mSounds[inSoundID];
			}
			else
			{
				this.mSounds.Add(inSoundID, null);
			}
			if (sound == null)
			{
				if (inAttribs.stagger > 0)
				{
					sound = SoundFactory.GetStaggeredSound(inSoundID, inAttribs.stagger);
				}
				else
				{
					sound = SoundFactory.GetSound(inSoundID, inAttribs.delay);
				}
				this.mSounds[inSoundID] = sound;
			}
			sound.Play();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00040B98 File Offset: 0x0003ED98
		public void Loop(int inSoundID)
		{
			this.Loop(inSoundID, new SoundAttribs());
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		public void Loop(int inSoundID, SoundAttribs inAttribs)
		{
			Sound sound = null;
			if (this.mSounds.ContainsKey(inSoundID))
			{
				sound = this.mSounds[inSoundID];
			}
			else
			{
				this.mSounds.Add(inSoundID, null);
			}
			if (sound == null)
			{
				sound = SoundFactory.GetLoopingSound(inSoundID, inAttribs.delay, inAttribs.fadein, inAttribs.fadeout);
				this.mSounds[inSoundID] = sound;
			}
			sound.Play();
			this.mCurrentLoopSound = inSoundID;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00040C18 File Offset: 0x0003EE18
		public void Update()
		{
			bool flag = false;
			this.mSoundsToDelete.Clear();
			foreach (KeyValuePair<int, Sound> keyValuePair in this.mSounds)
			{
				Sound value = keyValuePair.Value;
				if (value.IsFree())
				{
					if (keyValuePair.Key == this.mChainedSound1)
					{
						flag = true;
					}
					this.mSoundsToDelete.Add(keyValuePair.Key);
				}
				else
				{
					value.Update();
				}
			}
			foreach (int num in this.mSoundsToDelete)
			{
				this.mSounds.Remove(num);
			}
			if (flag)
			{
				this.PlayNextInChain();
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00040D00 File Offset: 0x0003EF00
		internal bool IsLooping(int p)
		{
			return true;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00040D04 File Offset: 0x0003EF04
		internal void Stop(int inSoundID)
		{
			SoundInstance existSoundInstance = this.m_SoundManager.GetExistSoundInstance(inSoundID);
			if (existSoundInstance != null)
			{
				existSoundInstance.Release();
			}
			this.Stop(inSoundID, false);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00040D30 File Offset: 0x0003EF30
		internal void Stop(int inSoundID, bool inUnload)
		{
			Sound sound = null;
			if (!this.FindSound(inSoundID, ref sound))
			{
				return;
			}
			if (inUnload)
			{
				sound.EnableAutoUnload();
			}
			this.mSounds.Remove(inSoundID);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00040D61 File Offset: 0x0003EF61
		internal void StopAll()
		{
			if (this.m_SoundManager != null)
			{
				this.m_SoundManager.StopAllSounds();
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00040D78 File Offset: 0x0003EF78
		internal void Fade(int inSoundID, bool inUnload)
		{
			Sound sound = null;
			if (!this.FindSound(inSoundID, ref sound))
			{
				return;
			}
			if (inUnload)
			{
				sound.EnableAutoUnload();
			}
			sound.Fade();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00040DA2 File Offset: 0x0003EFA2
		internal void Fade(int inSoundID)
		{
			this.Fade(inSoundID, false);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00040DAC File Offset: 0x0003EFAC
		internal void PauseLoopingSounds(bool p)
		{
			Sound sound = null;
			if (!this.FindSound(this.mCurrentLoopSound, ref sound))
			{
				return;
			}
			sound.Pause(p);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00040DD3 File Offset: 0x0003EFD3
		internal void PlayChained(int p, int p_2, int aDelay)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00040DD5 File Offset: 0x0003EFD5
		private bool FindSound(int inSoundID, ref Sound outSound)
		{
			if (this.mSounds.ContainsKey(inSoundID))
			{
				outSound = this.mSounds[inSoundID];
				return true;
			}
			return false;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00040DF8 File Offset: 0x0003EFF8
		private void PlayNextInChain()
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.delay = this.mChainedWait;
			this.Play(this.mChainedSound2, soundAttribs);
			this.mChainedSound1 = -1;
			this.mChainedSound2 = -1;
			this.mChainedWait = 0;
		}

		// Token: 0x04000BEC RID: 3052
		private SoundManager m_SoundManager;

		// Token: 0x04000BED RID: 3053
		private Dictionary<int, Sound> mSounds = new Dictionary<int, Sound>();

		// Token: 0x04000BEE RID: 3054
		private List<int> mSoundsToDelete = new List<int>();

		// Token: 0x04000BEF RID: 3055
		private int mChainedSound1 = -1;

		// Token: 0x04000BF0 RID: 3056
		private int mChainedSound2 = -1;

		// Token: 0x04000BF1 RID: 3057
		private int mChainedWait;

		// Token: 0x04000BF2 RID: 3058
		private int mCurrentLoopSound = -1;
	}
}
