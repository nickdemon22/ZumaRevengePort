using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000128 RID: 296
	public class WaterEffect1 : Effect
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x000A1E2D File Offset: 0x000A002D
		public WaterEffect1()
		{
			this.mResGroup = "";
			this.Reset("");
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000A1E4B File Offset: 0x000A004B
		protected void SetupShoreWaves(int x, int y, bool mirror, float vx, float vy)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000A1E4D File Offset: 0x000A004D
		public override string GetName()
		{
			return "WaterEffect1";
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000A1E54 File Offset: 0x000A0054
		public override void Update()
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000A1E56 File Offset: 0x000A0056
		public override void Reset(string level_id)
		{
			this.mUpdateCount++;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000A1E66 File Offset: 0x000A0066
		public override void DrawPriority(SexyGraphics g, int priority)
		{
		}
	}
}
