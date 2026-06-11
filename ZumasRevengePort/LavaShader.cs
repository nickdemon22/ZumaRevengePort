using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200011C RID: 284
	public class LavaShader : Effect
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x000A0A80 File Offset: 0x0009EC80
		protected override void Init()
		{
			this.mDisabled = false;
			this.mFadeInFromDeath = (this.mFadeoutDistortion = false);
			if (this.mBuffer == null)
			{
				this.mBuffer = new DeviceImage();
				this.mBuffer.mApp = this.mApp;
				this.mBuffer.AddImageFlags(24U);
				this.mBuffer.SetImageMode(false, false);
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000A0AE2 File Offset: 0x0009ECE2
		protected void DoShader(SexyGraphics g, DeviceImage buffer)
		{
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000A0AE4 File Offset: 0x0009ECE4
		public LavaShader()
		{
			this.mResGroup = "GamePlay";
			this.mApp = GameApp.gApp;
			this.mDisabled = false;
			this.mOrgDistAmt = (this.mDistAmt = (this.mScale = (this.mScroll = 0f)));
			this.mNeedFadein = false;
			this.mFadeInFromDeath = false;
			this.mAffectSkull = true;
			this.mFadeoutDistortion = false;
			this.mApplyTunnels = true;
			this.mApplyFullScene = false;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000A0B64 File Offset: 0x0009ED64
		public override void Dispose()
		{
			base.Dispose();
			if (this.mBuffer != null)
			{
				this.mBuffer.Dispose();
				this.mBuffer = null;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000A0B86 File Offset: 0x0009ED86
		public override void LevelStarted(bool from_load)
		{
			if (this.mApplyFullScene && this.mOrgDistAmt > 0f)
			{
				this.mFadeoutDistortion = true;
				return;
			}
			this.mFadeoutDistortion = false;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000A0BAC File Offset: 0x0009EDAC
		public override void Update()
		{
			this.Update(false);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000A0BB8 File Offset: 0x0009EDB8
		public void Update(bool only_check_shaders_supported)
		{
			Board board = this.mApp.GetBoard();
			if (this.mActivateOnMuMu && !board.mDoMuMuMode)
			{
				return;
			}
			if (this.mNeedFadein)
			{
				if (this.mApp.mBoard == null || this.mApp.mBoard.mTransitionScreenImage == null)
				{
					this.mDistAmt = Math.Min(this.mOrgDistAmt, this.mDistAmt + Common._M(5E-06f));
				}
				this.mDisabled = ((double)this.mDistAmt <= 1E-09);
				this.mNeedFadein = (this.mDistAmt < this.mOrgDistAmt);
			}
			else if (this.mFadeoutDistortion && this.mDistAmt > 0f && !board.mDoMuMuMode)
			{
				this.mDistAmt -= Common._M(1E-06f);
				if (this.mDistAmt <= 0f)
				{
					this.mDistAmt = 0f;
					this.mDisabled = true;
				}
			}
			else if (board.mDoMuMuMode)
			{
				this.mDistAmt = (this.mOrgDistAmt = Common._M(0.0005f));
				this.mScroll = Common._M(0.08f);
				this.mScale = Common._M(0.2f);
				this.mDisabled = false;
			}
			if (only_check_shaders_supported)
			{
				if (!this.mApp.ShadersSupported())
				{
					return;
				}
			}
			else
			{
				bool flag = !this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence();
				if (this.mApp.mLoadingThreadStarted && !this.mApp.mLoadingThreadCompleted)
				{
					flag = false;
				}
				if (!flag)
				{
					return;
				}
			}
			this.mUpdateCount++;
			this.mTimer += Common._M(0.02f);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000A0D78 File Offset: 0x0009EF78
		public override void DrawUnderBackground(SexyGraphics g)
		{
			Board board = this.mApp.GetBoard();
			bool flag = !this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence() && (!this.mActivateOnMuMu || board.mDoMuMuMode);
			if (!this.mApp.mLoadingThreadStarted || !this.mApp.mLoadingThreadCompleted)
			{
			}
			int num = 1024;
			int num2 = Common._DS(1200);
			g.DrawImage(this.mApp.mBoard.mBackgroundImage, (Common._S(800) - num) / 2 + GameApp.gScreenShakeX, GameApp.gScreenShakeY, num, num2);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000A0E24 File Offset: 0x0009F024
		public override bool DrawTunnel(SexyGraphics g, Image img, int x, int y)
		{
			Board board = this.mApp.GetBoard();
			if (!this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence() && this.mActivateOnMuMu)
			{
				bool mDoMuMuMode = board.mDoMuMuMode;
			}
			return true;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000A0E6D File Offset: 0x0009F06D
		public bool DrawTunnel(SexyGraphics g, Image img, int x, int y, float dist_amt, float scale, float scroll, float timer, float alpha_mult)
		{
			return false;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000A0E70 File Offset: 0x0009F070
		public override void DrawFullScene(SexyGraphics g)
		{
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000A0E72 File Offset: 0x0009F072
		public override void SetParams(string key, string value)
		{
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000A0E74 File Offset: 0x0009F074
		public override void NukeParams()
		{
			this.mActivateOnMuMu = false;
			this.mApplyTunnels = true;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000A0E84 File Offset: 0x0009F084
		public override bool DrawSkullPit(SexyGraphics g, HoleMgr hole)
		{
			Board board = this.mApp.GetBoard();
			if (!this.mDisabled && this.mApp.ShadersSupported() && board != null)
			{
				board.DoingMainDarkFrogSequence();
			}
			return false;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000A0EBD File Offset: 0x0009F0BD
		public override void UserDied()
		{
			if (this.mFadeoutDistortion && this.mDistAmt < this.mOrgDistAmt)
			{
				this.mDisabled = false;
				this.mFadeInFromDeath = true;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000A0EE3 File Offset: 0x0009F0E3
		public override string GetName()
		{
			return "LavaShader";
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000A0EEA File Offset: 0x0009F0EA
		public override void CopyFrom(Effect e)
		{
		}

		// Token: 0x0400197B RID: 6523
		public bool mActivateOnMuMu;

		// Token: 0x0400197C RID: 6524
		public float mDistAmt;

		// Token: 0x0400197D RID: 6525
		public float mOrgDistAmt;

		// Token: 0x0400197E RID: 6526
		public float mScroll;

		// Token: 0x0400197F RID: 6527
		public float mScale;

		// Token: 0x04001980 RID: 6528
		public bool mAffectSkull;

		// Token: 0x04001981 RID: 6529
		public bool mApplyFullScene;

		// Token: 0x04001982 RID: 6530
		public bool mFadeoutDistortion;

		// Token: 0x04001983 RID: 6531
		public bool mDisabled;

		// Token: 0x04001984 RID: 6532
		public bool mFadeInFromDeath;

		// Token: 0x04001985 RID: 6533
		public bool mNeedFadein;

		// Token: 0x04001986 RID: 6534
		public bool mApplyTunnels;

		// Token: 0x04001987 RID: 6535
		protected DeviceImage mBuffer;

		// Token: 0x04001988 RID: 6536
		protected GameApp mApp;

		// Token: 0x04001989 RID: 6537
		protected float mTimer;
	}
}
