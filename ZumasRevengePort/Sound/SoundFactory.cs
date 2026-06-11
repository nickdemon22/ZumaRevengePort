using System;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	// Token: 0x020000DB RID: 219
	public class SoundFactory
	{
		// Token: 0x06000ED9 RID: 3801 RVA: 0x0009A296 File Offset: 0x00098496
		public static void SetSoundManager(SoundManager inSoundManager)
		{
			SoundFactory.m_SoundManager = inSoundManager;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0009A29E File Offset: 0x0009849E
		public static Sound GetSound(int inSoundID, int inDelay)
		{
			return SoundFactory.GetSound(inSoundID, inDelay, true);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0009A2A8 File Offset: 0x000984A8
		public static Sound GetSound(int inSoundID, int inDelay, bool inAutoRelease)
		{
			BurstSound burstSound = new BurstSound(inSoundID, SoundFactory.m_SoundManager, inAutoRelease);
			if (inDelay > 0)
			{
				return new DelayedSound(burstSound, inDelay);
			}
			return burstSound;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0009A2CF File Offset: 0x000984CF
		public static Sound GetStaggeredSound(int inSoundID, int inStaggerTime)
		{
			return new StaggeredSound(new BurstSound(inSoundID, SoundFactory.m_SoundManager, true), inStaggerTime);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0009A2E4 File Offset: 0x000984E4
		public static Sound GetLoopingSound(int inSoundID, int inDelay, float inFadeInSpeed, float inFadeOutSpeed)
		{
			LoopingSound loopingSound = new LoopingSound(inSoundID, SoundFactory.m_SoundManager);
			if (inDelay > 0)
			{
				if (inFadeInSpeed < 1f || inFadeOutSpeed < 1f)
				{
					return new DelayedSound(new FadedSound(loopingSound, inFadeInSpeed, inFadeOutSpeed), inDelay);
				}
				return new DelayedSound(loopingSound, inDelay);
			}
			else
			{
				if (inFadeInSpeed < 1f || inFadeOutSpeed < 1f)
				{
					return new FadedSound(loopingSound, inFadeInSpeed, inFadeOutSpeed);
				}
				return loopingSound;
			}
		}

		// Token: 0x040017D9 RID: 6105
		private static SoundManager m_SoundManager;
	}
}
