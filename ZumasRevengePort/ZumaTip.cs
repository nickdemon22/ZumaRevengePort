using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000054 RID: 84
	public class ZumaTip
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x00057378 File Offset: 0x00055578
		public ZumaTip(string text, int width, int height, Rect cutout_region, int id)
		{
			this.mCutoutX = cutout_region.mX;
			this.mCutoutY = cutout_region.mY;
			this.mCutoutW = cutout_region.mWidth;
			this.mCutoutH = cutout_region.mHeight;
			this.mText = text;
			this.mId = id;
			this.mWidth = width + Common._DS(100);
			this.mHeight = height + Common._DS(20);
			if (this.mCutoutX < 0 && id != ZumaProfile.FRUIT_HINT)
			{
				this.mCutoutX = 0;
			}
			if (id != ZumaProfile.CHALLENGE_HINT)
			{
				if (id == ZumaProfile.FIRST_SHOT_HINT)
				{
					this.mMaskImage = Res.GetImageByID(ResID.IMAGE_UI_CONE);
					this.mCutoutW = this.mMaskImage.mWidth * 4;
					this.mCutoutH = this.mMaskImage.mHeight * 4;
				}
				else if (id == ZumaProfile.ZUMA_BAR_HINT)
				{
					this.SetZumaBarBoundingBox();
					this.CreateCutoutImage();
				}
				else
				{
					this.mMaskImage = Res.GetImageByID(ResID.IMAGE_UI_CIRCLE);
				}
			}
			int num = 0;
			SexyGraphics graphics = new SexyGraphics();
			graphics.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			this.mTextHeight = graphics.GetWordWrappedHeight(this.mWidth - Common._DS(100), this.mText, -1, ref num, ref num);
			CommonGraphics.SetNonMaskedArea(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH, this.mMaskedRects, ZumaTip.MAX_ALPHA);
			if (this.mMaskedRects.Count == 4)
			{
				this.mMaskedRects[0].r.mX = -GameApp.gApp.mBoardOffsetX;
				MaskedRect maskedRect = this.mMaskedRects[0];
				maskedRect.r.mWidth = maskedRect.r.mWidth + GameApp.gApp.mBoardOffsetX;
				return;
			}
			if (this.mMaskedRects.Count == 3)
			{
				this.mMaskedRects.Add(new MaskedRect(new Rect(-GameApp.gApp.mBoardOffsetX, 0, GameApp.gApp.mBoardOffsetX, GlobalMembers.gSexyApp.mScreenBounds.mHeight), ZumaTip.MAX_ALPHA));
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000575C0 File Offset: 0x000557C0
		public virtual void Dispose()
		{
			this.mCutoutImage = null;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000575CC File Offset: 0x000557CC
		public void PointAt(int x, int y, int dir)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_ARROW);
			int num = Common._DS(Common._M(175));
			if (dir == 0)
			{
				this.mArrowAngle = 3.1415927f;
				this.mArrowX = x + Common._DS(Common._M(24));
				this.mArrowY = y - imageByID.mHeight / 2;
				this.mBoxRect = new Rect(x + num, y - this.mHeight / 2, this.mWidth, this.mHeight);
				if (this.mBoxRect.mY < 0)
				{
					this.mBoxRect.mY = 0;
					return;
				}
				if (this.mBoxRect.mY + this.mBoxRect.mHeight > GlobalMembers.gSexyApp.mHeight)
				{
					this.mBoxRect.mY = GlobalMembers.gSexyApp.mHeight - this.mBoxRect.mHeight;
					return;
				}
			}
			else if (dir == 1)
			{
				this.mArrowAngle = 0f;
				this.mArrowX = x - imageByID.mWidth - Common._DS(Common._M(24));
				this.mArrowY = y - imageByID.mHeight / 2;
				this.mBoxRect = new Rect(x - num - this.mWidth, y - this.mHeight / 2, this.mWidth, this.mHeight);
				if (this.mBoxRect.mY < 0)
				{
					this.mBoxRect.mY = 0;
					return;
				}
				if (this.mBoxRect.mY + this.mBoxRect.mHeight > GlobalMembers.gSexyApp.mHeight)
				{
					this.mBoxRect.mY = GlobalMembers.gSexyApp.mHeight - this.mBoxRect.mHeight;
					return;
				}
			}
			else if (dir == 2)
			{
				this.mArrowAngle = 1.5707964f;
				this.mArrowX = x - imageByID.mWidth / 2;
				this.mArrowY = y + Common._DS(Common._M(48));
				this.mBoxRect = new Rect(x - this.mWidth / 2, y + num, this.mWidth, this.mHeight);
				if (this.mBoxRect.mX < 0)
				{
					this.mBoxRect.mX = 0;
					return;
				}
				if (this.mBoxRect.mX + this.mBoxRect.mWidth > GlobalMembers.gSexyApp.mWidth)
				{
					this.mBoxRect.mX = GlobalMembers.gSexyApp.mWidth - this.mBoxRect.mWidth;
					return;
				}
			}
			else if (dir == 3)
			{
				this.mArrowAngle = -1.5707964f;
				this.mArrowX = x - imageByID.mWidth / 2;
				this.mArrowY = y - imageByID.mHeight - Common._DS(Common._M(46));
				this.mBoxRect = new Rect(x - this.mWidth / 2, y - num - this.mHeight, this.mWidth, this.mHeight);
				if (this.mBoxRect.mX < 0)
				{
					this.mBoxRect.mX = 0;
					return;
				}
				if (this.mBoxRect.mX + this.mBoxRect.mWidth > GlobalMembers.gSexyApp.mWidth)
				{
					this.mBoxRect.mX = GlobalMembers.gSexyApp.mWidth - this.mBoxRect.mWidth;
				}
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00057900 File Offset: 0x00055B00
		public void AutoPointAt(int x, int y, int region_w, int region_h)
		{
			int num = GlobalMembers.gSexyApp.mWidth - (x + region_w);
			int num2 = GlobalMembers.gSexyApp.mHeight - (y + region_h);
			int[] array = new int[]
			{
				num,
				x,
				num2,
				y
			};
			int num3 = 0;
			for (int i = 1; i < 4; i++)
			{
				if (array[i] > array[num3])
				{
					num3 = i;
				}
			}
			if (num3 == 0)
			{
				this.PointAt(x + region_w, y + region_h / 2, num3);
				return;
			}
			if (num3 == 1)
			{
				this.PointAt(x, y + region_h / 2, num3);
				return;
			}
			if (num3 == 2)
			{
				this.PointAt(x + region_w / 2, y + region_h, num3);
				return;
			}
			if (num3 == 3)
			{
				this.PointAt(x + region_w / 2, y, num3);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000579C8 File Offset: 0x00055BC8
		public void AutoPointAtCutoutRegion()
		{
			this.AutoPointAt(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000579E8 File Offset: 0x00055BE8
		public void Draw(SexyGraphics g)
		{
			if (this.mUpdateCount < this.mAppearDelay)
			{
				return;
			}
			if (this.mCutoutImage != null)
			{
				g.DrawImage(this.mCutoutImage, this.mCutoutX, this.mCutoutY);
			}
			else if (this.mMaskImage != null)
			{
				g.DrawImage(this.mMaskImage, this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
			}
			if (this.mMaskImage != null || this.mCutoutImage != null)
			{
				if (this.mCutoutX >= 0)
				{
					Common._S(80);
				}
				else
				{
					Common._S(80);
				}
				g.SetColor(0, 0, 0, ZumaTip.MAX_ALPHA);
				for (int i = 0; i < Common.size<MaskedRect>(this.mMaskedRects); i++)
				{
					g.FillRect(this.mMaskedRects[i].r);
				}
			}
			Common.DrawCommonDialogBacking(g, this.mBoxRect.mX, this.mBoxRect.mY, this.mBoxRect.mWidth, this.mBoxRect.mHeight);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_ARROW);
			if (this.mDrawArrow)
			{
				g.DrawImageRotated(imageByID, this.mArrowX, (int)((float)this.mArrowY + this.mArrowYOff), (double)this.mArrowAngle);
				if (this.mDoArrowAnim)
				{
					g.PushState();
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255, (int)this.mArrowAlpha);
					g.DrawImageRotated(imageByID, this.mArrowX, (int)((float)this.mArrowY + this.mArrowYOff), (double)this.mArrowAngle);
					g.PopState();
					if (this.mId == ZumaProfile.FIRST_SHOT_HINT)
					{
						g.SetFont(Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET));
						g.SetColor(255, 253, 99);
						g.DrawString(TextManager.getInstance().getString(824), Common._DS(Common._M(140)), Common._DS(Common._M1(540)));
					}
				}
			}
			g.SetColor(255, 220, 135);
			g.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			int num = Common._M(50);
			int num2 = Common._M(0);
			num = Common._DS(num);
			num2 = Common._DS(num2);
			Rect rect;
			rect = new Rect(this.mBoxRect.mX + num, this.mBoxRect.mY + num2, this.mBoxRect.mWidth - num * 2, this.mBoxRect.mHeight - num2 * 2);
			rect.mY += (rect.mHeight - this.mTextHeight) / 2;
			g.WriteWordWrapped(rect, this.mText, -1, 0);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00057C94 File Offset: 0x00055E94
		public void Update()
		{
			this.mUpdateCount++;
			if (this.mDoArrowAnim)
			{
				float num = Common._M(10.5f);
				float num2 = Common._M(0.5f);
				float num3 = (float)Common._M(10);
				this.mArrowAlpha += num * (float)this.mArrowAlphaDir;
				if (this.mArrowAlpha >= 255f && this.mArrowAlphaDir == 1)
				{
					this.mArrowAlpha = 255f;
					this.mArrowAlphaDir = -1;
				}
				else if (this.mArrowAlpha <= 0f && this.mArrowAlphaDir == -1)
				{
					this.mArrowAlphaDir = 1;
					this.mArrowAlpha = 0f;
				}
				this.mArrowYOff += num2 * (float)this.mArrowYOffDir;
				if (this.mArrowYOff >= num3 && this.mArrowYOffDir == 1)
				{
					this.mArrowYOff = num3;
					this.mArrowYOffDir = -1;
					return;
				}
				if (this.mArrowYOff <= 0f && this.mArrowYOffDir == -1)
				{
					this.mArrowYOff = 0f;
					this.mArrowYOffDir = 1;
				}
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00057DA0 File Offset: 0x00055FA0
		public bool CutoutContainsPoint(int x, int y)
		{
			Rect rect;
			rect = new Rect(this.mCutoutX, this.mCutoutY, this.mCutoutW, this.mCutoutH);
			return rect.Contains(x, y);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00057DD8 File Offset: 0x00055FD8
		private void SetZumaBarBoundingBox()
		{
			GameApp gApp = GameApp.gApp;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER);
			int num = gApp.IsWideScreen() ? 0 : ((int)((float)imageByID.mWidth * 0.05f));
			int wideScreenAdjusted = gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)) + num);
			int wideScreenAdjusted2 = gApp.GetWideScreenAdjusted(Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)) - num);
			this.mCutoutX = wideScreenAdjusted + Common._DS(25);
			this.mCutoutY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER));
			this.mCutoutW = wideScreenAdjusted2 - wideScreenAdjusted + imageByID2.mWidth - Common._DS(50);
			this.mCutoutH = imageByID3.mHeight;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00057EA8 File Offset: 0x000560A8
		private void CreateCutoutImage()
		{
			this.mCutoutImage = new DeviceImage();
			this.mCutoutImage.mApp = GameApp.gApp;
			this.mCutoutImage.SetImageMode(true, true);
			this.mCutoutImage.AddImageFlags(16U);
			this.mCutoutImage.Create(this.mCutoutW, this.mCutoutH);
			SexyGraphics graphics = new SexyGraphics(this.mCutoutImage);
			graphics.Get3D().ClearColorBuffer(new Color(0, 0));
			float num = (float)ZumaTip.MAX_ALPHA;
			float num2 = num / (float)ZumaTip.NUM_LINES;
			int num3 = 0;
			while (num > 0f)
			{
				graphics.SetColor(0, 0, 0, (int)num);
				graphics.FillRect(num3, num3, this.mCutoutW - num3 * 2, 1);
				graphics.FillRect(num3, num3 + 1, 1, this.mCutoutH - 1 - num3 * 2);
				graphics.FillRect(num3 + 1, this.mCutoutH - 1 - num3, this.mCutoutW - 1 - num3 * 2, 1);
				graphics.FillRect(this.mCutoutW - 1 - num3, num3 + 1, 1, this.mCutoutH - 2 - num3 * 2);
				num -= num2;
				num3++;
			}
			graphics.ClearRenderContext();
		}

		// Token: 0x0400119F RID: 4511
		public static readonly int MAX_ALPHA = 128;

		// Token: 0x040011A0 RID: 4512
		private static readonly int NUM_LINES = 10;

		// Token: 0x040011A1 RID: 4513
		protected List<MaskedRect> mMaskedRects = new List<MaskedRect>();

		// Token: 0x040011A2 RID: 4514
		protected MemoryImage mCutoutImage;

		// Token: 0x040011A3 RID: 4515
		protected Image mMaskImage;

		// Token: 0x040011A4 RID: 4516
		protected string mText = "";

		// Token: 0x040011A5 RID: 4517
		protected Rect mBoxRect = default(Rect);

		// Token: 0x040011A6 RID: 4518
		protected float mArrowAngle;

		// Token: 0x040011A7 RID: 4519
		protected int mArrowX;

		// Token: 0x040011A8 RID: 4520
		protected int mArrowY;

		// Token: 0x040011A9 RID: 4521
		protected int mTextHeight;

		// Token: 0x040011AA RID: 4522
		protected int mWidth;

		// Token: 0x040011AB RID: 4523
		protected int mHeight;

		// Token: 0x040011AC RID: 4524
		protected int mCutoutX;

		// Token: 0x040011AD RID: 4525
		protected int mCutoutY;

		// Token: 0x040011AE RID: 4526
		protected int mCutoutW;

		// Token: 0x040011AF RID: 4527
		protected int mCutoutH;

		// Token: 0x040011B0 RID: 4528
		protected float mArrowAlpha;

		// Token: 0x040011B1 RID: 4529
		protected int mArrowAlphaDir = 1;

		// Token: 0x040011B2 RID: 4530
		protected float mArrowYOff;

		// Token: 0x040011B3 RID: 4531
		protected int mArrowYOffDir = 1;

		// Token: 0x040011B4 RID: 4532
		public bool mDoArrowAnim;

		// Token: 0x040011B5 RID: 4533
		public bool mBlockUpdates = true;

		// Token: 0x040011B6 RID: 4534
		public bool mClickDismiss = true;

		// Token: 0x040011B7 RID: 4535
		public bool mDrawArrow = true;

		// Token: 0x040011B8 RID: 4536
		public int mId;

		// Token: 0x040011B9 RID: 4537
		public int mUpdateCount;

		// Token: 0x040011BA RID: 4538
		public int mAppearDelay;

		// Token: 0x02000098 RID: 152
		public enum Dir
		{
			// Token: 0x040015D3 RID: 5587
			Left,
			// Token: 0x040015D4 RID: 5588
			Right,
			// Token: 0x040015D5 RID: 5589
			Up,
			// Token: 0x040015D6 RID: 5590
			Down
		}
	}
}
