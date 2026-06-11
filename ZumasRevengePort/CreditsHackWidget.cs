using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000023 RID: 35
	public class CreditsHackWidget : Widget, ButtonListener
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x0003CB04 File Offset: 0x0003AD04
		public CreditsHackWidget()
		{
			Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME_SELECT);
			Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_HOME);
			this.mPriority = 2147483646;
			this.mZOrder = 2147483646;
			this.mHasAlpha = (this.mHasTransparencies = true);
			this.mClip = false;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0003CB5A File Offset: 0x0003AD5A
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0003CB64 File Offset: 0x0003AD64
		public virtual void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0003CB6D File Offset: 0x0003AD6D
		public virtual void ButtonPress(int theId)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON2));
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0003CB8F File Offset: 0x0003AD8F
		public virtual void ButtonDepress(int theId)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.ReturnFromCredits();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0003CBA7 File Offset: 0x0003ADA7
		public override void Update()
		{
			if (GameApp.gApp.mCredits != null && GameApp.gApp.mHasFocus)
			{
				this.MarkDirty();
				GameApp.gApp.mCredits.Update();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0003CBD6 File Offset: 0x0003ADD6
		public override void Draw(SexyGraphics g)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.Draw(g);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
		public override void MouseUp(int x, int y)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.mSpeedUp = false;
				if (GameApp.gApp.mCredits.AtEnd() && GameApp.gApp.mCredits.mTapDown)
				{
					GameApp.gApp.mCredits.mTapDown = false;
					OptionsDialog optionsDialog = GameApp.gApp.GetDialog(2) as OptionsDialog;
					if (optionsDialog != null)
					{
						optionsDialog.OnCreditsHided();
					}
					GameApp.gApp.ReturnFromCredits();
				}
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0003CC73 File Offset: 0x0003AE73
		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (GameApp.gApp.mCredits != null)
			{
				GameApp.gApp.mCredits.mSpeedUp = true;
				if (GameApp.gApp.mCredits.AtEnd())
				{
					GameApp.gApp.mCredits.mTapDown = true;
				}
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0003CCB2 File Offset: 0x0003AEB2
		public virtual void ButtonDownTick(int x)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
		public virtual void ButtonMouseEnter(int x)
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0003CCB6 File Offset: 0x0003AEB6
		public virtual void ButtonMouseLeave(int x)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0003CCB8 File Offset: 0x0003AEB8
		public virtual void ButtonMouseMove(int x, int y, int z)
		{
		}

		// Token: 0x04000BAB RID: 2987
		public ButtonWidget mContinueBtn;
	}
}
