using System;

namespace ZumasRevenge
{
	// Token: 0x020000A8 RID: 168
	public class Stopwatch
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0008C2E4 File Offset: 0x0008A4E4
		public Stopwatch(string msg)
		{
			this.text = msg;
			this.start = DateTime.Now.Millisecond;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0008C314 File Offset: 0x0008A514
		~Stopwatch()
		{
		}

		// Token: 0x04001640 RID: 5696
		private string text;

		// Token: 0x04001641 RID: 5697
		private int start;
	}
}
