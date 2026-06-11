using System;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200002D RID: 45
	public class MoreGames : Widget, ButtonListener
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00045051 File Offset: 0x00043251
		public MoreGames(GameApp gameApp)
		{
			this.gameApp = gameApp;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00045060 File Offset: 0x00043260
		public void ButtonPress(int theId)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00045062 File Offset: 0x00043262
		public void ButtonPress(int theId, int theClickCount)
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00045064 File Offset: 0x00043264
		public void ButtonDepress(int theId)
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00045066 File Offset: 0x00043266
		public void ButtonDownTick(int theId)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00045068 File Offset: 0x00043268
		public void ButtonMouseEnter(int theId)
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0004506A File Offset: 0x0004326A
		public void ButtonMouseLeave(int theId)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0004506C File Offset: 0x0004326C
		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0004506E File Offset: 0x0004326E
		internal bool IsReadyForDelete()
		{
			return true;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00045071 File Offset: 0x00043271
		internal void DoSlide(bool p)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00045073 File Offset: 0x00043273
		internal void Init()
		{
		}

		// Token: 0x04000C8B RID: 3211
		private GameApp gameApp;
	}
}
