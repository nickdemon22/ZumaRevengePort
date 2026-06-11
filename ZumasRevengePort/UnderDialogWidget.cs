using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000021 RID: 33
	public class UnderDialogWidget : Widget
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0003BB43 File Offset: 0x00039D43
		public UnderDialogWidget()
		{
			this.mMouseVisible = false;
			this.mHasAlpha = true;
			this.mShrunkScreen1 = null;
			this.mShrunkScreen2 = null;
			this.mClip = false;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0003BB70 File Offset: 0x00039D70
		~UnderDialogWidget()
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0003BB98 File Offset: 0x00039D98
		public void CreateImages()
		{
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0003BB9A File Offset: 0x00039D9A
		public void DrawPaused(SexyGraphics g)
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0003BB9C File Offset: 0x00039D9C
		public override void Update()
		{
			base.Update();
			if (GameApp.gApp.mDialogObscurePct > 0f && GlobalMembers.gSexyAppBase.mHasFocus)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0003BBC7 File Offset: 0x00039DC7
		public override void Draw(SexyGraphics g)
		{
		}

		// Token: 0x04000B96 RID: 2966
		public DeviceImage mShrunkScreen1;

		// Token: 0x04000B97 RID: 2967
		public DeviceImage mShrunkScreen2;
	}
}
