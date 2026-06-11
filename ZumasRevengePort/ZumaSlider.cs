using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000121 RID: 289
	public class ZumaSlider : Slider
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000A0EF4 File Offset: 0x0009F0F4
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x000A0EFC File Offset: 0x0009F0FC
		public string Label
		{
			get
			{
				return this.mLabel;
			}
			set
			{
				this.mLabel = value;
				Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
				this.mLabelWidth = fontByID.StringWidth(this.mLabel);
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000A0F2C File Offset: 0x0009F12C
		public ZumaSlider(int id, SliderListener listener, string label) : base(Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_THUMB), Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDER), id, listener)
		{
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
			this.mFeedbackSoundID = -1;
			this.mLabel = label;
			this.mLabelWidth = fontByID.StringWidth(this.mLabel);
			this.mHasAlpha = (this.mHasTransparencies = true);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000A0F90 File Offset: 0x0009F190
		public override void Draw(SexyGraphics g)
		{
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
			g.PushState();
			g.ClearClipRect();
			g.SetFont(fontByID);
			g.SetColor(255, 255, 64, 255);
			int num = Common._S(Common._M(20));
			int num2 = Common._S(Common._M(-35));
			g.DrawString(this.mLabel, (this.mWidth + num - this.mLabelWidth) / 2, g.mFont.mAscent + this.mHeight + num2 - Common._S(Common._M(12)) - g.mFont.mHeight);
			g.PopState();
			base.Draw(g);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000A1040 File Offset: 0x0009F240
		public override void MouseEnter()
		{
			base.MouseEnter();
			this.MarkDirty();
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000A104E File Offset: 0x0009F24E
		public override void MouseLeave()
		{
			base.MouseLeave();
			this.MarkDirty();
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000A105C File Offset: 0x0009F25C
		public override void MouseUp(int x, int y)
		{
			base.MouseUp(x, y);
			if (this.mFeedbackSoundID >= 0)
			{
				GameApp.gApp.PlaySample(this.mFeedbackSoundID);
			}
		}

		// Token: 0x040019A6 RID: 6566
		public string mLabel;

		// Token: 0x040019A7 RID: 6567
		public int mLabelWidth;

		// Token: 0x040019A8 RID: 6568
		public int mFeedbackSoundID;
	}
}
