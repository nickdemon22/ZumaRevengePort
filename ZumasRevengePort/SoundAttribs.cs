using System;

namespace ZumasRevenge
{
	// Token: 0x02000037 RID: 55
	public class SoundAttribs
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0004A650 File Offset: 0x00048850
		public SoundAttribs()
		{
			this.pan = 0;
			this.pitch = 0f;
			this.fadein = 1f;
			this.fadeout = 1f;
			this.delay = 0;
			this.stagger = 0;
			this.volume = 1f;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0004A6A4 File Offset: 0x000488A4
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0004A6AC File Offset: 0x000488AC
		public int pan { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0004A6B5 File Offset: 0x000488B5
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0004A6BD File Offset: 0x000488BD
		public int delay { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0004A6C6 File Offset: 0x000488C6
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0004A6CE File Offset: 0x000488CE
		public int stagger { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0004A6D7 File Offset: 0x000488D7
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0004A6DF File Offset: 0x000488DF
		public float fadein { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0004A6E8 File Offset: 0x000488E8
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0004A6F0 File Offset: 0x000488F0
		public float fadeout { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0004A6F9 File Offset: 0x000488F9
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0004A701 File Offset: 0x00048901
		public float pitch { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0004A70A File Offset: 0x0004890A
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0004A712 File Offset: 0x00048912
		public float volume { get; set; }
	}
}
