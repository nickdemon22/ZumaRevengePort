using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200001B RID: 27
	public class MapScreenHackWidget : Widget
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0003A546 File Offset: 0x00038746
		public MapScreenHackWidget()
		{
			this.mClip = false;
			this.mApp = GameApp.gApp;
			this.mDelay = 0;
			this.mToggledAdventureMode = false;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0003A570 File Offset: 0x00038770
		public override void Update()
		{
			if (this.mApp.mMapScreen != null && this.mApp.mMapScreen.mDirty)
			{
				this.MarkDirty();
			}
			if (this.mDelay == 0)
			{
				this.mApp.mMapScreen.Update();
				if (this.mApp.mMapScreen != null && this.mApp.mMapScreen.mRemove)
				{
					if (this.mApp.mMapScreen.mSelectedZone == -1)
					{
						this.mDelay = Common._M(10);
						return;
					}
					this.mDelay = Common._M(40);
					return;
				}
			}
			else if (this.mApp.mMapScreen != null)
			{
				this.mDelay--;
				if (this.mDelay == 0 && !this.mToggledAdventureMode)
				{
					this.mToggledAdventureMode = true;
					this.mApp.mMapScreen.CleanButtons();
					this.mApp.mForceZoneRestart = this.mApp.mMapScreen.mSelectedZone;
					this.mApp.mBambooTransition.mTransitionDelegate = new BambooTransition.BambooTransitionDelegate(this.mApp.StartAdventureMode);
					this.mApp.ToggleBambooTransition();
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0003A697 File Offset: 0x00038897
		public override void Draw(SexyGraphics g)
		{
			if (this.mApp.mMapScreen == null)
			{
				return;
			}
			this.mApp.mMapScreen.Draw(g);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0003A6B8 File Offset: 0x000388B8
		public override void DrawAll(ModalFlags theFlags, SexyGraphics g)
		{
			if (g != null)
			{
				g.Get3D();
			}
			base.DrawAll(theFlags, g);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0003A6CC File Offset: 0x000388CC
		public override void MouseMove(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseMove(x, y);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0003A70A File Offset: 0x0003890A
		public override void MouseDrag(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseMove(x, y);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0003A748 File Offset: 0x00038948
		public override void MouseDown(int x, int y, int cc)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseDown(x, y);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0003A786 File Offset: 0x00038986
		public override void MouseUp(int x, int y)
		{
			if (this.mApp.mMapScreen == null || this.mApp.mDialogMap.Count > 0 || this.mDelay > 0)
			{
				return;
			}
			this.mApp.mMapScreen.MouseUp(x, y);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0003A7C4 File Offset: 0x000389C4
		public override void MouseLeave()
		{
			this.mApp.mMapScreen.MouseLeave();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0003A7D6 File Offset: 0x000389D6
		public override void KeyChar(char theChar)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0003A7D8 File Offset: 0x000389D8
		public override void GotFocus()
		{
			base.GotFocus();
			if (this.mWidgetManager != null && this.mApp.mMapScreen != null && this.mApp.mMapScreen.mContinueBtn != null)
			{
				this.mWidgetManager.SetGamepadSelection(this.mApp.mMapScreen.mContinueBtn, 0);
			}
		}

		// Token: 0x04000B65 RID: 2917
		public GameApp mApp;

		// Token: 0x04000B66 RID: 2918
		public int mDelay;

		// Token: 0x04000B67 RID: 2919
		public bool mToggledAdventureMode;
	}
}
