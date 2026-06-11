using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	// Token: 0x020000E4 RID: 228
	public class MosquitoBall : IDisposable
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x0009A524 File Offset: 0x00098724
		public virtual void Dispose()
		{
			this.mMosquitoes.Clear();
		}

		// Token: 0x04001806 RID: 6150
		public List<Mosquito> mMosquitoes = new List<Mosquito>();
	}
}
