using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000083 RID: 131
	public class HoleInfo
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x0007FB84 File Offset: 0x0007DD84
		public void DrawRings(SexyGraphics g)
		{
			if (this.mDeathAlpha >= 255f || (!this.mDoDeathFade && this.mDeathAlpha != 0f))
			{
				return;
			}
			float num = (float)Common._S(96);
			float num2 = num / 2f;
			float num3 = num / 2f;
			g.SetDrawMode(1);
			g.SetColorizeImages(true);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_INFERNO_RING);
			int num4 = (int)((num - (float)imageByID.GetCelWidth()) / 2f - (float)Common._S(Common._M(0)));
			int num5 = (int)((num - (float)imageByID.GetCelHeight()) / 2f - (float)Common._S(Common._M(1)));
			for (int i = 0; i < 3; i++)
			{
				FireRing fireRing = this.mRing[i];
				if (fireRing.mCel != -1 && fireRing.mAlpha != 0f)
				{
					int num6 = (int)fireRing.mAlpha;
					if (this.mDoDeathFade)
					{
						num6 = (int)Math.Min((float)num6, 255f - this.mDeathAlpha);
					}
					g.SetColor(255, 255, 255, num6);
					Rect celRect = imageByID.GetCelRect(fireRing.mCel);
					if (g.Is3D())
					{
						g.DrawImageRotatedF(imageByID, (float)(Common._S(this.mX) + num4), (float)(Common._S(this.mY) + num5), (double)(this.mRotation + 3.14159f), num2 - (float)num4, num3 - (float)num5, celRect);
					}
					else
					{
						g.DrawImageRotated(imageByID, Common._S(this.mX) + num4, Common._S(this.mY) + num5, (double)(this.mRotation + 3.14159f), (int)(num2 - (float)num4), (int)(num3 - (float)num5), celRect);
					}
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0007FD50 File Offset: 0x0007DF50
		public void DrawMain(SexyGraphics g, bool is_gray)
		{
			Image[] array = new Image[]
			{
				is_gray ? Res.GetImageByID(ResID.IMAGE_HOLE_BASE_GRAY) : Res.GetImageByID(ResID.IMAGE_HOLE_BASE),
				is_gray ? Res.GetImageByID(ResID.IMAGE_HOLE_GRAY) : Res.GetImageByID(ResID.IMAGE_HOLE),
				is_gray ? Res.GetImageByID(ResID.IMAGE_HOLE_HEAD_GRAY) : Res.GetImageByID(ResID.IMAGE_HOLE_HEAD),
				is_gray ? Res.GetImageByID(ResID.IMAGE_HOLE_JAW_GRAY) : Res.GetImageByID(ResID.IMAGE_HOLE_JAW)
			};
			g.PushState();
			if (this.mDeathAlpha > 0f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, is_gray ? ((int)this.mDeathAlpha) : ((int)Math.Max(0f, 255f - this.mDeathAlpha)));
			}
			float num = (float)array[0].mWidth / 2f;
			float num2 = (float)array[0].mHeight / 2f;
			g.PushState();
			g.SetClipRect(Common._S(this.mX) + Common._S(Common._M(19)), Common._S(this.mY) + Common._S(Common._M1(15)), array[0].mWidth - Common._S(Common._M2(38)), array[0].mHeight - Common._S(Common._M3(33)));
			float num3 = this.mRotation + 3.14159f;
			float num4 = (float)(Common._S(Common._M(7)) + GameApp.gScreenShakeX);
			float num5 = (float)(Common._S(Common._M(7)) + GameApp.gScreenShakeY);
			if (g.Is3D())
			{
				g.DrawImageRotatedF(array[1], (float)Common._S(this.mX) + num4, (float)Common._S(this.mY) + num5, (double)num3, num - num4, num2 - num5);
			}
			else
			{
				g.DrawImageRotated(array[1], (int)((float)Common._S(this.mX) + num4), (int)((float)Common._S(this.mY) + num5), (double)num3, (int)(num - num4), (int)(num2 - num5));
			}
			float num6 = (float)Common._S(Common._M(17)) - this.mPercentOpen * Common._S(Common._M1(30f)) + (float)GameApp.gScreenShakeX;
			float num7 = (float)(Common._S(Common._M(17)) + GameApp.gScreenShakeY);
			if (g.Is3D())
			{
				g.DrawImageRotatedF(array[2], (float)Common._S(this.mX) + num7, (float)Common._S(this.mY) + num6, (double)num3, num - num7, num2 - num6);
			}
			else
			{
				g.DrawImageRotated(array[2], (int)((float)Common._S(this.mX) + num7), (int)((float)Common._S(this.mY) + num6), (double)num3, (int)(num - num7), (int)(num2 - num6));
			}
			num6 = (float)Common._S(Common._M(48)) + this.mPercentOpen * Common._S(Common._M1(6f)) + (float)GameApp.gScreenShakeX;
			num7 = (float)(Common._S(Common._M(17)) + GameApp.gScreenShakeY);
			if (g.Is3D())
			{
				g.DrawImageRotatedF(array[3], (float)Common._S(this.mX) + num7, (float)Common._S(this.mY) + num6, (double)num3, num - num7, num2 - num6);
			}
			else
			{
				g.DrawImageRotated(array[3], (int)((float)Common._S(this.mX) + num7), (int)((float)Common._S(this.mY) + num6), (double)num3, (int)(num - num7), (int)(num2 - num6));
			}
			g.PopState();
			g.DrawImageRotated(array[0], Common._S(this.mX) + GameApp.gScreenShakeX, Common._S(this.mY) + GameApp.gScreenShakeY, (double)num3);
			g.PopState();
			if (this.mVisible)
			{
				this.DrawRings(g);
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00080108 File Offset: 0x0007E308
		public HoleInfo()
		{
			this.mFrame = 0;
			this.mVisible = true;
			this.mPercentOpen = 0f;
			this.mPercentTarget = -1f;
			this.mUpdateCount = 0;
			this.mCurveNum = -1;
			this.mDoDeathFade = false;
			this.mDeathAlpha = 0f;
			this.mRing[2].mCel = -1;
			this.mRing[1].mCel = -1;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000801B4 File Offset: 0x0007E3B4
		public HoleInfo(HoleInfo rhs)
		{
			this.mFrame = 0;
			this.mVisible = true;
			this.mPercentOpen = 0f;
			this.mPercentTarget = -1f;
			this.mUpdateCount = 0;
			this.mCurveNum = -1;
			this.mDoDeathFade = false;
			this.mDeathAlpha = 0f;
			this.mRing[2].mCel = -1;
			this.mRing[1].mCel = -1;
			if (rhs == null)
			{
				return;
			}
			this.mPercentOpen = rhs.mPercentOpen;
			this.mPercentTarget = rhs.mPercentTarget;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mFrame = rhs.mFrame;
			this.mRotation = rhs.mRotation;
			this.mVisible = rhs.mVisible;
			this.mCurveNum = rhs.mCurveNum;
			this.mCurve = rhs.mCurve;
			this.mRing[0] = rhs.mRing[0];
			this.mRing[1] = rhs.mRing[1];
			this.mRing[2] = rhs.mRing[2];
			this.mDeathAlpha = rhs.mDeathAlpha;
			this.mDoDeathFade = rhs.mDoDeathFade;
			this.mShared.AddRange(rhs.mShared.ToArray());
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00080370 File Offset: 0x0007E570
		public void Update()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_INFERNO_RING);
			if (this.mDoDeathFade)
			{
				this.mDeathAlpha += Common._M(2f);
				if (this.mDeathAlpha > 255f)
				{
					this.mDeathAlpha = 255f;
				}
			}
			else if (this.mDeathAlpha > 0f)
			{
				this.mDeathAlpha -= Common._M(1f);
				if (this.mDeathAlpha < 0f)
				{
					this.mDeathAlpha = 0f;
				}
			}
			this.mUpdateCount++;
			if (this.mPercentOpen > this.mPercentTarget && this.mPercentTarget >= 0f)
			{
				this.mPercentOpen -= Common._M(0.01f);
				if (this.mPercentOpen < this.mPercentTarget)
				{
					this.mPercentOpen = this.mPercentTarget;
					this.mPercentTarget = -1f;
				}
			}
			if (this.mPercentOpen > 0f && this.mVisible)
			{
				int num = imageByID.mNumRows * imageByID.mNumCols;
				int num2 = 7 - (int)(this.mPercentOpen / 20f);
				if (num2 < 3)
				{
					num2 = 3;
				}
				else if (num2 > 7)
				{
					num2 = 7;
				}
				float num3 = 255f / (24f * (float)num * 2f);
				if (this.mRing[0].mCel != -1)
				{
					FireRing[] array = this.mRing;
					int num4 = 0;
					array[num4].mAlpha = array[num4].mAlpha + num3;
					if (this.mRing[0].mAlpha >= 255f)
					{
						this.mRing[0].mAlpha = 255f;
					}
					if (this.mUpdateCount % num2 == 0)
					{
						FireRing[] array2 = this.mRing;
						int num5 = 0;
						if ((array2[num5].mCel = array2[num5].mCel + 1) >= num)
						{
							this.mRing[0].mCel = -1;
						}
					}
				}
				float num6 = 128f / ((float)num2 * Common._M(80f));
				if (this.mRing[1].mAlpha == 0f && this.mRing[0].mCel == 4)
				{
					this.mRing[1].mCel = 0;
					this.mRing[1].mAlpha = 128f;
				}
				else if (this.mRing[1].mCel != -1)
				{
					FireRing[] array3 = this.mRing;
					int num7 = 1;
					array3[num7].mAlpha = array3[num7].mAlpha + num6;
					if (this.mRing[1].mAlpha >= 255f)
					{
						this.mRing[1].mAlpha = 255f;
					}
					if (this.mUpdateCount % num2 == 0)
					{
						FireRing[] array4 = this.mRing;
						int num8 = 1;
						if ((array4[num8].mCel = array4[num8].mCel + 1) >= num)
						{
							this.mRing[1].mCel = 0;
							this.mRing[1].mAlpha = 128f;
						}
					}
				}
				if (this.mRing[2].mAlpha == 0f && this.mRing[1].mCel == num / 2)
				{
					this.mRing[2].mCel = 0;
					this.mRing[2].mAlpha = 128f;
					return;
				}
				if (this.mRing[2].mCel != -1)
				{
					FireRing[] array5 = this.mRing;
					int num9 = 2;
					array5[num9].mAlpha = array5[num9].mAlpha + num6;
					if (this.mRing[2].mAlpha >= 255f)
					{
						this.mRing[2].mAlpha = 255f;
					}
					if (this.mUpdateCount % num2 == 0)
					{
						FireRing[] array6 = this.mRing;
						int num10 = 2;
						if ((array6[num10].mCel = array6[num10].mCel + 1) >= num)
						{
							this.mRing[2].mCel = 0;
							this.mRing[2].mAlpha = 128f;
							return;
						}
					}
				}
			}
			else if (this.mVisible)
			{
				bool flag = true;
				int num11 = 7 - (int)(this.mPercentOpen / 20f);
				if (num11 < 3)
				{
					num11 = 3;
				}
				else if (num11 > 7)
				{
					num11 = 7;
				}
				for (int i = 0; i < 3; i++)
				{
					if (this.mRing[i].mAlpha > 0f)
					{
						if (this.mUpdateCount % num11 == 0)
						{
							FireRing[] array7 = this.mRing;
							int num12 = i;
							if ((array7[num12].mCel = array7[num12].mCel + 1) >= imageByID.mNumRows * imageByID.mNumCols)
							{
								this.mRing[i].mCel = 0;
							}
						}
						FireRing[] array8 = this.mRing;
						int num13 = i;
						array8[num13].mAlpha = array8[num13].mAlpha - (float)Common._M(2);
						if (this.mRing[i].mAlpha <= 0f)
						{
							this.mRing[i].mAlpha = 0f;
						}
						else
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					for (int j = 0; j < 3; j++)
					{
						this.mRing[j].mCel = ((j == 0) ? 0 : -1);
						this.mRing[j].mAlpha = 0f;
					}
				}
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000808D4 File Offset: 0x0007EAD4
		public void Draw(SexyGraphics g, float hilite_override)
		{
			this.DrawMain(g, false);
			if (this.mDeathAlpha > 0f)
			{
				this.DrawMain(g, true);
			}
			float num = (hilite_override != 0f || this.mCurve == null) ? hilite_override : this.mCurve.mSkullHilite;
			if (this.mCurve != null && this.mDeathAlpha <= 0f && this.mCurve.mInitialPathHilite && num != 0f)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)num);
				g.SetDrawMode(1);
				this.DrawMain(g, false);
				g.SetColorizeImages(false);
				g.SetDrawMode(0);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00080982 File Offset: 0x0007EB82
		public void Draw(SexyGraphics g)
		{
			this.Draw(g, 0f);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00080990 File Offset: 0x0007EB90
		public void SetPctOpen(float pct)
		{
			if (pct < this.mPercentOpen && this.mVisible)
			{
				this.mPercentTarget = pct;
				return;
			}
			this.mPercentOpen = pct;
			this.mPercentTarget = -1f;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000809BD File Offset: 0x0007EBBD
		public float GetPctOpen()
		{
			return this.mPercentOpen;
		}

		// Token: 0x0400148D RID: 5261
		public float mPercentOpen;

		// Token: 0x0400148E RID: 5262
		public float mPercentTarget = -1f;

		// Token: 0x0400148F RID: 5263
		public int mUpdateCount;

		// Token: 0x04001490 RID: 5264
		public int mX;

		// Token: 0x04001491 RID: 5265
		public int mY;

		// Token: 0x04001492 RID: 5266
		public int mFrame;

		// Token: 0x04001493 RID: 5267
		public float mRotation;

		// Token: 0x04001494 RID: 5268
		public bool mVisible = true;

		// Token: 0x04001495 RID: 5269
		public int mCurveNum = -1;

		// Token: 0x04001496 RID: 5270
		public CurveMgr mCurve;

		// Token: 0x04001497 RID: 5271
		public FireRing[] mRing = new FireRing[3];

		// Token: 0x04001498 RID: 5272
		public float mDeathAlpha;

		// Token: 0x04001499 RID: 5273
		public bool mDoDeathFade;

		// Token: 0x0400149A RID: 5274
		public List<int> mShared = new List<int>();
	}
}
