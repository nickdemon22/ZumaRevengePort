using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000076 RID: 118
	public class FrogFlyOff
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00070197 File Offset: 0x0006E397
		public FrogFlyOff()
		{
			this.mPlayThud = false;
			this.mFrogJumpTime = Common._M(80);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000701BE File Offset: 0x0006E3BE
		public virtual void Dispose()
		{
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000701C0 File Offset: 0x0006E3C0
		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x, int start_y, float angle)
		{
			FrogFlyOff.FROG_START_SCALE = Common._M(0.5f);
			this.mTimer = 0;
			this.mJumpOut = true;
			this.mFrog = frog;
			this.mFrogX = (float)((start_x == int.MaxValue) ? this.mFrog.GetCenterX() : start_x);
			this.mFrogY = (float)((start_y == int.MaxValue) ? this.mFrog.GetCenterY() : start_y);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (dest_x == 2147483647)
			{
				dest_x = GlobalMembers.gSexyApp.mWidth - imageByID.mWidth / 2;
			}
			if (dest_y == 2147483647)
			{
				dest_y = -(int)this.mFrogY - imageByID.mHeight / 2;
			}
			dest_x -= (int)this.mFrogX;
			this.mFrogVX = (float)dest_x / (float)this.mFrogJumpTime;
			this.mFrogVY = (float)dest_y / (float)this.mFrogJumpTime;
			this.mScaleDelta = (Common._M(2f) - FrogFlyOff.FROG_START_SCALE) / (float)this.mFrogJumpTime;
			this.mFrogScale = FrogFlyOff.FROG_START_SCALE;
			this.mFrogAngle = (this.mDestFrogAngle = (MathUtils._eq(angle, float.MaxValue) ? this.mFrog.GetAngle() : angle));
			this.mFrogAngleDelta = Common._M(0.15f);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00070302 File Offset: 0x0006E502
		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x, int start_y)
		{
			this.JumpOut(frog, dest_x, dest_y, start_x, start_y, float.MaxValue);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00070316 File Offset: 0x0006E516
		public void JumpOut(Gun frog, int dest_x, int dest_y, int start_x)
		{
			this.JumpOut(frog, dest_x, dest_y, start_x, int.MaxValue);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00070328 File Offset: 0x0006E528
		public void JumpOut(Gun frog, int dest_x, int dest_y)
		{
			this.JumpOut(frog, dest_x, dest_y, int.MaxValue);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00070338 File Offset: 0x0006E538
		public void JumpOut(Gun frog, int dest_x)
		{
			this.JumpOut(frog, dest_x, int.MaxValue, int.MaxValue);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0007034C File Offset: 0x0006E54C
		public void JumpOut(Gun frog)
		{
			this.JumpOut(frog, int.MaxValue);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0007035C File Offset: 0x0006E55C
		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out, int jump_to_x, int jump_to_y)
		{
			FrogFlyOff.FROG_START_SCALE = Common._M(0.5f);
			if (!continue_from_jump_out)
			{
				this.JumpOut(frog, jump_to_x, jump_to_y);
				this.mFrogX += this.mFrogVX * (float)this.mFrogJumpTime;
				this.mFrogY += this.mFrogVY * (float)this.mFrogJumpTime;
				this.mFrogAngle += this.mFrogAngleDelta * (float)this.mFrogJumpTime;
			}
			this.mTimer = 0;
			this.mFrog = frog;
			this.mJumpOut = false;
			this.mPlayThud = true;
			this.mFrogScale = Common._M(2f);
			this.mScaleDelta *= -1f;
			this.RehupFrogPosition(dest_x, dest_y);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0007041E File Offset: 0x0006E61E
		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out, int jump_to_x)
		{
			this.JumpIn(frog, dest_x, dest_y, continue_from_jump_out, jump_to_x, int.MaxValue);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00070432 File Offset: 0x0006E632
		public void JumpIn(Gun frog, int dest_x, int dest_y, bool continue_from_jump_out)
		{
			this.JumpIn(frog, dest_x, dest_y, continue_from_jump_out, int.MaxValue);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00070444 File Offset: 0x0006E644
		public void JumpIn(Gun frog, int dest_x, int dest_y)
		{
			this.JumpIn(frog, dest_x, dest_y, true);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00070450 File Offset: 0x0006E650
		public bool HasCompletedFlyOff()
		{
			return this.mTimer > this.mFrogJumpTime;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00070460 File Offset: 0x0006E660
		public void RehupFrogPosition(int dest_x, int dest_y)
		{
			this.RehupFrogPosition(dest_x, dest_y, this.mFrog.GetAngle());
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00070478 File Offset: 0x0006E678
		public void RehupFrogPosition(int dest_x, int dest_y, float forced_dest_angle)
		{
			this.mFrogAngleDelta = -(this.mFrogAngle - forced_dest_angle) / (float)this.mFrogJumpTime;
			this.mFrogVX = -(this.mFrogX - (float)dest_x) / (float)this.mFrogJumpTime;
			this.mFrogVY = -(this.mFrogY - (float)dest_y) / (float)this.mFrogJumpTime;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000704CC File Offset: 0x0006E6CC
		public void Update()
		{
			if (this.mTimer > this.mFrogJumpTime)
			{
				return;
			}
			this.mTimer++;
			if (this.mJumpOut)
			{
				if (this.mFrogScale < 1f)
				{
					if (this.mTimer == 1)
					{
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_FROG_LAUNCH));
					}
					this.mFrogScale += this.mScaleDelta;
					if (this.mFrogScale > 1f)
					{
						this.mFrogScale = 1f;
					}
				}
				this.mFrogAngle += this.mFrogAngleDelta;
				this.mFrogX += this.mFrogVX;
				this.mFrogY += this.mFrogVY;
				return;
			}
			this.mFrogAngle += this.mFrogAngleDelta;
			this.mFrogX += this.mFrogVX;
			this.mFrogY += this.mFrogVY;
			if (this.mTimer >= this.mFrogJumpTime)
			{
				this.mFrogAngle = this.mDestFrogAngle;
			}
			if (this.mFrogScale > FrogFlyOff.FROG_START_SCALE)
			{
				this.mFrogScale += this.mScaleDelta;
				this.PlayFrogLandingSound();
				if (this.mFrogScale < FrogFlyOff.FROG_START_SCALE)
				{
					this.mFrogScale = FrogFlyOff.FROG_START_SCALE;
				}
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00070624 File Offset: 0x0006E824
		public void Draw(SexyGraphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
			if (this.mFrogY + (float)(imageByID.mHeight / 2) >= 0f)
			{
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_FROG_SHADOW);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.RotateRad(this.mFrogAngle);
				float num = (float)this.mTimer / (float)this.mFrogJumpTime;
				if (num > 1f)
				{
					num = 1f;
				}
				float num2 = Common._M(1f);
				float num3 = Common._M(3f);
				float num4 = Common._M(1f);
				float num5 = Common._M(0f);
				float num6 = Common._M(0f);
				float num7 = Common._M(150f);
				float num8;
				float num9;
				float num10;
				if (this.mJumpOut)
				{
					num8 = num2 + (num3 - num2) * num;
					num9 = num4 + (num5 - num4) * num;
					num10 = num6 + (num7 - num6) * num;
				}
				else
				{
					num8 = num3 - (num3 - num2) * num;
					num9 = num5 - (num5 - num4) * num;
					num10 = num7 - (num7 - num6) * num;
				}
				this.mGlobalTranform.Scale(num8, num8);
				g.SetColorizeImages(true);
				g.SetColor(0, 0, 0, (int)(num9 * 255f));
				g.DrawImageTransform(imageByID2, this.mGlobalTranform, imageByID2.GetCelRect(0), Common._S(this.mFrogX - num10), Common._S(this.mFrogY + num10));
				g.SetColorizeImages(false);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.RotateRad(this.mFrogAngle);
				this.mGlobalTranform.Scale(this.mFrogScale, this.mFrogScale);
				g.DrawImageTransform(imageByID, this.mGlobalTranform, Common._S(this.mFrogX), Common._S(this.mFrogY));
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000707E8 File Offset: 0x0006E9E8
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mFrogScale);
			sync.SyncFloat(ref this.mFrogX);
			sync.SyncFloat(ref this.mFrogY);
			sync.SyncFloat(ref this.mFrogAngle);
			sync.SyncFloat(ref this.mFrogAngleDelta);
			sync.SyncFloat(ref this.mFrogVX);
			sync.SyncFloat(ref this.mFrogVY);
			sync.SyncFloat(ref this.mScaleDelta);
			sync.SyncFloat(ref this.mDestFrogAngle);
			sync.SyncLong(ref this.mFrogJumpTime);
			sync.SyncLong(ref this.mTimer);
			sync.SyncBoolean(ref this.mJumpOut);
			if (sync.isRead())
			{
				this.mFrog = GameApp.gApp.mBoard.mFrog;
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000708A2 File Offset: 0x0006EAA2
		private void PlayFrogLandingSound()
		{
			if (!this.mPlayThud || this.mFrogScale > FrogFlyOff.FROG_START_SCALE - this.mScaleDelta * 15f)
			{
				return;
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_FROG_FALL));
			this.mPlayThud = false;
		}

		// Token: 0x040013AB RID: 5035
		public float mFrogScale;

		// Token: 0x040013AC RID: 5036
		public float mFrogX;

		// Token: 0x040013AD RID: 5037
		public float mFrogY;

		// Token: 0x040013AE RID: 5038
		public float mFrogAngle;

		// Token: 0x040013AF RID: 5039
		public float mFrogAngleDelta;

		// Token: 0x040013B0 RID: 5040
		public float mFrogVX;

		// Token: 0x040013B1 RID: 5041
		public float mFrogVY;

		// Token: 0x040013B2 RID: 5042
		public float mScaleDelta;

		// Token: 0x040013B3 RID: 5043
		public float mDestFrogAngle;

		// Token: 0x040013B4 RID: 5044
		public int mFrogJumpTime;

		// Token: 0x040013B5 RID: 5045
		public int mTimer;

		// Token: 0x040013B6 RID: 5046
		public bool mJumpOut;

		// Token: 0x040013B7 RID: 5047
		public bool mPlayThud;

		// Token: 0x040013B8 RID: 5048
		public Gun mFrog;

		// Token: 0x040013B9 RID: 5049
		protected Transform mGlobalTranform = new Transform();

		// Token: 0x040013BA RID: 5050
		private static float FROG_START_SCALE = 0.26f;
	}
}
