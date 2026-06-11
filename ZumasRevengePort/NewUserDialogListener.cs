using System;

namespace ZumasRevenge
{
	// Token: 0x02000011 RID: 17
	public interface NewUserDialogListener
	{
		// Token: 0x06000376 RID: 886
		void BlankNameEntered();

		// Token: 0x06000377 RID: 887
		void NameIsAllSpaces();

		// Token: 0x06000378 RID: 888
		void FinishedNewUser(bool canceled);
	}
}
