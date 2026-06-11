using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000143 RID: 323
	public class LeaderBoardsButtonWidget : ExtraSexyButton
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x000A396F File Offset: 0x000A1B6F
		public LeaderBoardsButtonWidget(int theId, LeaderBoards theListener) : base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mLeaderBoards = theListener;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000A3987 File Offset: 0x000A1B87
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
		}

		// Token: 0x04001A7C RID: 6780
		public LeaderBoards mLeaderBoards;
	}
}
