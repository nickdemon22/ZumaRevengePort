using System;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000031 RID: 49
	public class WidescreenBoardWidget : Widget
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0004611C File Offset: 0x0004431C
		public WidescreenBoardWidget()
		{
			this.mWidgetFlagsMod.mRemoveFlags |= 5;
			this.mApp = GameApp.gApp;
			this.mZOrder = 2147483646;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0004614D File Offset: 0x0004434D
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseDown(x - Common._S(80), y, theClickCount);
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00046177 File Offset: 0x00044377
		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseUp(x - Common._S(80), y, theClickCount);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000461A1 File Offset: 0x000443A1
		public override bool IsPointVisible(int x, int y)
		{
			return this.mApp.GetBoard() != null && (x < Common._S(80) || x > this.mApp.mWidth + Common._S(80));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000461D3 File Offset: 0x000443D3
		public override void MouseMove(int x, int y)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseMove(x - Common._S(80), y);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000461FC File Offset: 0x000443FC
		public override void MouseDrag(int x, int y)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseMove(x - Common._S(80), y);
			}
		}

		// Token: 0x04000CA5 RID: 3237
		public GameApp mApp;
	}
}
