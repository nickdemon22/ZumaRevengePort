using System;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	// Token: 0x02000150 RID: 336
	public abstract class BasicSound : Sound
	{
		// Token: 0x06001032 RID: 4146
		protected abstract bool FindFreeSoundInstance(ref SoundInstance outInstance);

		// Token: 0x04001AD1 RID: 6865
		protected SoundManager m_SoundManager;

		// Token: 0x04001AD2 RID: 6866
		protected int m_SoundID = -1;
	}
}
