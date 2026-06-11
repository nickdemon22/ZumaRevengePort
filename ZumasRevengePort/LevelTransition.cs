using System;
using System.Collections.Generic;
using System.Linq;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000073 RID: 115
	public class LevelTransition : IDisposable
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x0006C3B4 File Offset: 0x0006A5B4
		protected void SetupBambooSmoke()
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

		// Token: 0x06000B87 RID: 2951 RVA: 0x0006C450 File Offset: 0x0006A650
		public LevelTransition(int next_level_override, bool dont_record_stats)
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("AdventureStats"))
			{
				GameApp.gApp.mResourceManager.LoadResources("AdventureStats");
			}
			this.mFrog = GameApp.gApp.GetBoard().GetGun();
			this.mFrogEffect = new FrogFlyOff();
			this.Reset(true);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0006C4CB File Offset: 0x0006A6CB
		public LevelTransition(int next_level_override) : this(next_level_override, false)
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0006C4D5 File Offset: 0x0006A6D5
		public LevelTransition() : this(-1, false)
		{
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0006C4DF File Offset: 0x0006A6DF
		public void Dispose()
		{
			GameApp.gApp.mResourceManager.DeleteResources("AdventureStats");
			this.mFrogEffect = null;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0006C4FC File Offset: 0x0006A6FC
		public bool Update()
		{
			this.mTimer++;
			if (this.mDone)
			{
				return false;
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					bool sound = false;
					if (i == Enumerable.Count<BambooColumn>(this.mBambooColumns) - 1)
					{
						sound = true;
					}
					this.mBambooColumns[i].Update(sound);
				}
			}
			if (this.mState == 0)
			{
				if (this.mIntro)
				{
					this.mFrogEffect.Update();
					for (int j = 0; j < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); j++)
					{
						LTSmokeParticle s = this.mFrogSmoke[j];
						if (BambooTransition.UpdateSmokeParticle(s))
						{
							this.mFrogSmoke.RemoveAt(j);
							j--;
						}
					}
				}
				if ((this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime / 2 && this.mIntro) || !this.mIntro)
				{
					if (this.mFrogEffect.HasCompletedFlyOff() && Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int k = 0; k < Enumerable.Count<BambooColumn>(this.mBambooColumns); k++)
						{
							this.mBambooColumns[k].Close();
						}
					}
					this.mBGAlpha += 255f / (float)this.mBambooTime;
					if (this.mBGAlpha > 255f)
					{
						this.mBGAlpha = 255f;
					}
					bool flag = true;
					if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
					{
						for (int l = 0; l < Enumerable.Count<BambooColumn>(this.mBambooColumns); l++)
						{
							flag &= this.mBambooColumns[l].IsClosed();
						}
					}
					if (flag)
					{
						this.mTimer = 0;
						this.mState++;
						if (GameApp.gApp.mBoard.mLevel.mNum != 10)
						{
							int mNum = GameApp.gApp.mBoard.mLevel.mNum;
							int num = GameApp.gApp.mBoard.mLevel.mZone - 1;
							int num2 = num;
							int index = mNum + num * 10 + num2;
							string text = GameApp.gApp.GetLevelMgr().GetLevelId(index);
							text = char.ToUpper(text[0]) + text.Substring(1);
							string text2 = "Levels_" + text;
							if (!GameApp.gApp.mResourceManager.IsGroupLoaded(text2))
							{
								GameApp.gApp.mResourceManager.PrepareLoadResources(text2);
							}
						}
					}
				}
			}
			else
			{
				if (this.mState == 1)
				{
					if (++this.mDelay == Common._M(20))
					{
						this.mTimer = 0;
					}
					this.mFrogEffect.Update();
					if (GameApp.gApp.mBoard.mGameState == GameState.GameState_BossIntro && GameApp.gApp.mBoard.mBossIntroBGAlpha.GetOutVal() == 1.0)
					{
						this.mDone = true;
					}
					return this.mDelay == Common._M(19);
				}
				if (this.mState == 2)
				{
					this.mFrogEffect.Update();
					if ((!this.mIntro && this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime / 2) || this.mIntro)
					{
						this.mBGAlpha -= 255f / (float)this.mBambooTime;
						if (this.mBGAlpha < 0f)
						{
							this.mBGAlpha = 0f;
						}
						bool flag2 = true;
						if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
						{
							for (int m = 0; m < Enumerable.Count<BambooColumn>(this.mBambooColumns); m++)
							{
								flag2 &= this.mBambooColumns[m].IsOpened();
							}
						}
						if (flag2 && (this.mIntro || this.mFrogEffect.mTimer >= this.mFrogEffect.mFrogJumpTime))
						{
							GameApp.gApp.mBoard.CueLevelTransition();
							this.mDone = true;
							if (!this.mIntro && this.mDrawFrogEffect)
							{
								for (int n = 0; n < Common._M(20); n++)
								{
									this.mFrog.mSmokeParticles.Add(BambooTransition.SpawnSmokeParticle((float)this.mFrog.GetCenterX(), (float)this.mFrog.GetCenterY(), false, true));
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0006C95C File Offset: 0x0006AB5C
		public void DrawOverlay(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mDone)
			{
				return;
			}
			if (this.mBGAlpha > 0f)
			{
				g.SetColor(0, 0, 0, (int)this.mBGAlpha);
				g.FillRect(GameApp.gApp.GetScreenRect());
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
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
			if (((this.mState == 0 && this.mIntro) || (this.mState == 2 && !this.mIntro)) && this.mDrawFrogEffect && this.mFrogEffect.mFrogY + (float)(imageByID.mHeight / 2) + (float)Common._M(0) >= 0f)
			{
				if (this.mIntro)
				{
					for (int k = 0; k < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); k++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mFrogSmoke[k]);
					}
				}
				this.mFrogEffect.Draw(g);
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0006CA94 File Offset: 0x0006AC94
		public void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mDone)
			{
				return;
			}
			if (this.mBGAlpha > 0f)
			{
				g.SetColor(0, 0, 0, (int)this.mBGAlpha);
				g.FillRect(GameApp.gApp.GetScreenRect());
			}
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
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
			if (((this.mState == 0 && this.mIntro) || (this.mState == 2 && !this.mIntro)) && this.mDrawFrogEffect && this.mFrogEffect.mFrogY + (float)(imageByID.mHeight / 2) + (float)Common._M(0) >= 0f)
			{
				if (this.mIntro)
				{
					for (int k = 0; k < Enumerable.Count<LTSmokeParticle>(this.mFrogSmoke); k++)
					{
						BambooTransition.DrawSmokeParticle(g, this.mFrogSmoke[k]);
					}
				}
				this.mFrogEffect.Draw(g);
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0006CBCC File Offset: 0x0006ADCC
		public void Reset(bool intro)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BAMBOO_PIECE_A);
			this.mDrawFrogEffect = true;
			this.mIntro = intro;
			this.mState = 0;
			this.mDone = false;
			this.mDelay = 0;
			this.mIntroDelay = 0;
			this.mFrogSmoke.Clear();
			this.mSilent = false;
			if (this.mIntro)
			{
				this.mFrogEffect.JumpOut(this.mFrog);
				for (int i = 0; i < Common._M(20); i++)
				{
					this.mFrogSmoke.Add(BambooTransition.SpawnSmokeParticle(this.mFrogEffect.mFrogX, this.mFrogEffect.mFrogY, true, false));
				}
			}
			this.mBGAlpha = 0f;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) == 0)
			{
				float num = (float)GameApp.gApp.GetScreenRect().mX - 10f;
				for (float num2 = num; num2 <= (float)GameApp.gApp.GetScreenRect().mWidth; num2 += (float)(imageByID.GetWidth() - Common._DS(19)))
				{
					this.mBambooColumns.Add(new BambooColumn());
					Enumerable.Last<BambooColumn>(this.mBambooColumns).SetColumnX(num2);
				}
			}
			else
			{
				for (int j = 0; j < Enumerable.Count<BambooColumn>(this.mBambooColumns); j++)
				{
					this.mBambooColumns[j].Reset();
				}
			}
			this.SetupBambooSmoke();
			this.mTimer = 0;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0006CD27 File Offset: 0x0006AF27
		public void RehupFrogPosition()
		{
			this.mFrogEffect.RehupFrogPosition(this.mFrog.GetCenterX(), this.mFrog.GetCenterY());
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0006CD4C File Offset: 0x0006AF4C
		public void Open()
		{
			this.mState = 2;
			if (Enumerable.Count<BambooColumn>(this.mBambooColumns) > 0)
			{
				for (int i = 0; i < Enumerable.Count<BambooColumn>(this.mBambooColumns); i++)
				{
					this.mBambooColumns[i].Open();
				}
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0006CD95 File Offset: 0x0006AF95
		public bool IsDone()
		{
			return this.mDone;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0006CD9D File Offset: 0x0006AF9D
		public int GetState()
		{
			return this.mState;
		}

		// Token: 0x04001386 RID: 4998
		public List<LTSmokeParticle> mFrogSmoke = new List<LTSmokeParticle>();

		// Token: 0x04001387 RID: 4999
		public FrogFlyOff mFrogEffect;

		// Token: 0x04001388 RID: 5000
		public Gun mFrog;

		// Token: 0x04001389 RID: 5001
		public int mBambooTime;

		// Token: 0x0400138A RID: 5002
		public int mDelay;

		// Token: 0x0400138B RID: 5003
		public int mState;

		// Token: 0x0400138C RID: 5004
		public int mTimer;

		// Token: 0x0400138D RID: 5005
		public bool mDone;

		// Token: 0x0400138E RID: 5006
		public bool mIntro;

		// Token: 0x0400138F RID: 5007
		public float mBGAlpha;

		// Token: 0x04001390 RID: 5008
		public List<BambooColumn> mBambooColumns = new List<BambooColumn>();

		// Token: 0x04001391 RID: 5009
		public int mIntroDelay;

		// Token: 0x04001392 RID: 5010
		public int mNextLevelOverride;

		// Token: 0x04001393 RID: 5011
		public bool mDontRecordStats;

		// Token: 0x04001394 RID: 5012
		public bool mTransitionToStats;

		// Token: 0x04001395 RID: 5013
		public bool mDrawFrogEffect;

		// Token: 0x04001396 RID: 5014
		public bool mSilent;

		// Token: 0x04001397 RID: 5015
		public bool mDidFirstBounce;

		// Token: 0x02000099 RID: 153
		public enum State
		{
			// Token: 0x040015D8 RID: 5592
			BambooClose,
			// Token: 0x040015D9 RID: 5593
			Delay,
			// Token: 0x040015DA RID: 5594
			BambooOpen
		}
	}
}
