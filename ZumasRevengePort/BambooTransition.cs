using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x0200002F RID: 47
	public class BambooTransition : Widget
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0004507D File Offset: 0x0004327D
		public BambooTransition()
		{
			this.Reset();
			this.mZOrder = int.MaxValue;
			this.mUpdateNum = 0;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000450A8 File Offset: 0x000432A8
		public void Reset()
		{
			this.IMAGE_BAMBOO_PIECE_A = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.IMAGE_BAMBOO_PIECE_B = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_B);
			this.IMAGE_BAMBOO_PIECE_C = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_C);
			this.IMAGE_BAMBOO_PIECE_D = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_D);
			this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
			this.mBambooCloseWaitCount = 0;
			this.mLoadStartTime = ulong.MaxValue;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) == 0)
			{
				float num = (float)GameApp.gApp.GetScreenRect().mX - 10f;
				for (float num2 = num; num2 <= (float)GameApp.gApp.GetScreenRect().mWidth; num2 += (float)(this.IMAGE_BAMBOO_PIECE_A.GetWidth() - Common._DS(19)))
				{
					this.mBambooColumns.Add(new BambooColumn());
					Enumerable.Last<BambooColumn>(this.mBambooColumns).SetColumnX(num2);
				}
			}
			else
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Reset();
				}
			}
			this.SetupBambooSmoke();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000451AD File Offset: 0x000433AD
		public override void Draw(SexyGraphics g)
		{
			if (this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT)
			{
				base.DeferOverlay(10);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000451C0 File Offset: 0x000433C0
		public override void DrawOverlay(SexyGraphics g)
		{
			int num = (int)(255f * (this.mFadeCount / 40f));
			g.SetColor(0, 0, 0, num);
			g.FillRect(GameApp.gApp.GetScreenRect().mX, GameApp.gApp.GetScreenRect().mY, GameApp.gApp.GetScreenRect().mWidth, GameApp.gApp.GetScreenRect().mHeight);
			if (this.mBambooColumns.Count > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Draw(g);
				}
				for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
				{
					this.mBambooColumns[j].DrawSmoke(g);
				}
			}
			this.mUpdateNum = 0;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00045294 File Offset: 0x00043494
		public override void Update()
		{
			this.mUpdateNum++;
			for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
			{
				this.mBambooColumns[i].UpdateSmokeParticle();
			}
			if (this.mUpdateNum > 2)
			{
				return;
			}
			for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
			{
				bool sound = false;
				if (j == Enumerable.Count<BambooColumn>(this.mBambooColumns) - 1)
				{
					sound = true;
				}
				this.mBambooColumns[j].Update(sound);
			}
			switch (this.mState)
			{
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSING:
			{
				if (this.mFadeCount < 40f)
				{
					this.mFadeCount += 1UL;
				}
				bool flag = true;
				if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
				{
					for (int k = 0; k < Enumerable.Count<BambooColumn>(this.mBambooColumns); k++)
					{
						flag &= this.mBambooColumns[k].IsClosed();
					}
				}
				if (flag)
				{
					this.mBambooCloseWaitCount++;
					if (this.mBambooCloseWaitCount >= 10)
					{
						this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSED;
						return;
					}
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSED:
				if (this.mTransitionDelegate != null)
				{
					this.mTransitionDelegate();
				}
				this.mFadeCount = 40UL;
				this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_PAUSE;
				this.mLoadStartTime = (ulong)Common.SexyTime();
				return;
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_PAUSE:
			{
				ulong num = (ulong)Common.SexyTime() - this.mLoadStartTime;
				if (num >= 100UL)
				{
					this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPENING;
					if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int l = 0; l < Enumerable.Count<BambooColumn>(this.mBambooColumns); l++)
						{
							this.mBambooColumns[l].Open();
						}
						return;
					}
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPENING:
			{
				if (this.mFadeCount > 0UL)
				{
					this.mFadeCount -= 1UL;
				}
				bool flag2 = true;
				if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
				{
					for (int m = 0; m < Enumerable.Count<BambooColumn>(this.mBambooColumns); m++)
					{
						flag2 &= this.mBambooColumns[m].IsOpened();
					}
				}
				if (flag2)
				{
					this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPEN;
					return;
				}
				break;
			}
			case BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_OPEN:
				this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
				GameApp.gApp.BambooTransitionOpened();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000454D0 File Offset: 0x000436D0
		public void StartTransition()
		{
			if (this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT)
			{
				Console.WriteLine("\n >>>>> WARNING: Attempting to start bamboo transition while a transition is occurring\n ");
				return;
			}
			this.mFadeCount = 0UL;
			this.mState = BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_CLOSING;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Close();
				}
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00045534 File Offset: 0x00043734
		public bool IsInProgress()
		{
			return this.mState != BambooTransition.BambooTransitionState.BAMBOO_TRANSITION_INIT;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00045544 File Offset: 0x00043744
		private void SetupBambooSmoke()
		{
			int i = Common._M(4);
			List<int> list = new List<int>();
			for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
			{
				list.Add(j);
			}
			while (i > 0)
			{
				int num = Common.Rand() % Enumerable.Count<int>(list);
				for (int k = 0; k < Common._M(20); k++)
				{
					BambooColumn bambooColumn = this.mBambooColumns[list[num]];
					bambooColumn.AddSmokeParticle(BambooTransition.SpawnSmokeParticle(bambooColumn.GetColumnX(), bambooColumn.GetCollisionY(), false, false));
				}
				list.RemoveAt(num);
				i--;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000455DF File Offset: 0x000437DF
		public static LTSmokeParticle SpawnSmokeParticle(float x, float y)
		{
			return BambooTransition.SpawnSmokeParticle(x, y, false);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000455E9 File Offset: 0x000437E9
		public static LTSmokeParticle SpawnSmokeParticle(float x, float y, bool fast)
		{
			return BambooTransition.SpawnSmokeParticle(x, y, fast, false);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000455F4 File Offset: 0x000437F4
		public static LTSmokeParticle SpawnSmokeParticle(float x, float y, bool fast, bool slow_fade)
		{
			LTSmokeParticle ltsmokeParticle = new LTSmokeParticle();
			ltsmokeParticle.mX = x;
			ltsmokeParticle.mY = y;
			ltsmokeParticle.mFadingIn = true;
			ltsmokeParticle.mSize = MathUtils.FloatRange(Common._M(0.22f), Common._M1(0.45f));
			float num = fast ? MathUtils.FloatRange(Common._M(1.5f), Common._M1(2.5f)) : MathUtils.FloatRange(Common._M2(0.75f), Common._M3(1.5f));
			float num2 = MathUtils.FloatRange(0f, 6.2831855f);
			ltsmokeParticle.mVX = num * (float)Math.Cos((double)num2);
			ltsmokeParticle.mVY = -num * (float)Math.Sin((double)num2);
			ltsmokeParticle.mAlpha.mColor = new FColor(0f, 0f, 0f, 0f);
			ltsmokeParticle.mAlpha.mFadeRate = (float)MathUtils.IntRange(Common._M(10), Common._M1(20));
			ltsmokeParticle.mAlphaFadeOutTime = (slow_fade ? MathUtils.IntRange(Common._M(50), Common._M1(75)) : MathUtils.IntRange(Common._M2(10), Common._M3(20)));
			if (Common.Rand() % 100 == 0)
			{
				ltsmokeParticle.mColorFader.mColor = (ltsmokeParticle.mColorFader.mMinColor = new FColor(249f, 255f, 249f));
				ltsmokeParticle.mColorFader.mMaxColor = new FColor(205f, 208f, 148f);
			}
			else
			{
				ltsmokeParticle.mColorFader.mColor = (ltsmokeParticle.mColorFader.mMinColor = new FColor(212f, 217f, 212f));
				ltsmokeParticle.mColorFader.mMaxColor = new FColor(153f, 148f, 99f);
			}
			ltsmokeParticle.mColorFader.FadeOverTime((int)((float)ltsmokeParticle.mAlphaFadeOutTime + 255f / ltsmokeParticle.mAlpha.mFadeRate));
			return ltsmokeParticle;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000457E4 File Offset: 0x000439E4
		public static void DrawSmokeParticle(SexyGraphics g, LTSmokeParticle s)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_PARTICLE_FUZZ);
			g.SetColorizeImages(true);
			Color color = s.mColorFader.mColor.ToColor();
			color.mAlpha = (int)s.mAlpha.mColor.mAlpha;
			g.SetColor(color);
			g.DrawImage(imageByID, (int)Common._S(s.mX), (int)Common._S(s.mY), (int)((float)imageByID.mWidth * s.mSize), (int)((float)imageByID.mHeight * s.mSize));
			g.SetColorizeImages(false);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00045878 File Offset: 0x00043A78
		public static bool UpdateSmokeParticle(LTSmokeParticle s)
		{
			s.mAlpha.Update();
			s.mColorFader.Update();
			s.mX += s.mVX;
			s.mY += s.mVY;
			if (!s.mFadingIn && s.mAlpha.mColor.mAlpha <= 0f)
			{
				return true;
			}
			if (s.mAlpha.mColor.mAlpha == (float)s.mAlpha.mMax && s.mFadingIn)
			{
				s.mFadingIn = false;
				s.mAlpha.mMin = 0;
				s.mAlpha.mFadeRate = -255f / (float)s.mAlphaFadeOutTime;
			}
			return false;
		}

		// Token: 0x04000C8C RID: 3212
		private BambooTransition.BambooTransitionState mState;

		// Token: 0x04000C8D RID: 3213
		private List<BambooColumn> mBambooColumns = new List<BambooColumn>();

		// Token: 0x04000C8E RID: 3214
		private ulong mLoadStartTime;

		// Token: 0x04000C8F RID: 3215
		private ulong mFadeCount;

		// Token: 0x04000C90 RID: 3216
		private int mBambooCloseWaitCount;

		// Token: 0x04000C91 RID: 3217
		public BambooTransition.BambooTransitionDelegate mTransitionDelegate;

		// Token: 0x04000C92 RID: 3218
		private Image IMAGE_BAMBOO_PIECE_A;

		// Token: 0x04000C93 RID: 3219
		private Image IMAGE_BAMBOO_PIECE_B;

		// Token: 0x04000C94 RID: 3220
		private Image IMAGE_BAMBOO_PIECE_C;

		// Token: 0x04000C95 RID: 3221
		private Image IMAGE_BAMBOO_PIECE_D;

		// Token: 0x04000C96 RID: 3222
		private int mUpdateNum;

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x060009B3 RID: 2483
		public delegate void BambooTransitionDelegate();

		// Token: 0x02000047 RID: 71
		private enum BambooTransitionState
		{
			// Token: 0x0400112E RID: 4398
			BAMBOO_TRANSITION_INIT,
			// Token: 0x0400112F RID: 4399
			BAMBOO_TRANSITION_CLOSING,
			// Token: 0x04001130 RID: 4400
			BAMBOO_TRANSITION_CLOSED,
			// Token: 0x04001131 RID: 4401
			BAMBOO_TRANSITION_PAUSE,
			// Token: 0x04001132 RID: 4402
			BAMBOO_TRANSITION_OPENING,
			// Token: 0x04001133 RID: 4403
			BAMBOO_TRANSITION_OPEN,
			// Token: 0x04001134 RID: 4404
			NUM_BAMBOO_TRANSITION_STATES
		}
	}
}
