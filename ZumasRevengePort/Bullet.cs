using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x0200006A RID: 106
	public class Bullet : Ball
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x00069AF0 File Offset: 0x00067CF0
		public Bullet()
		{
			this.mVelX = 0f;
			this.mVelY = 0f;
			this.mHitBall = null;
			this.mHitPercent = 0f;
			this.mMergeSpeed = Common._M(0.025f);
			this.mJustFired = false;
			this.mDoNewMerge = false;
			this.mUpdateCount = 0;
			this.mHitDX = 0f;
			this.mHitDY = 0f;
			this.mAngleFired = 0f;
			this.mSkip = false;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00069B90 File Offset: 0x00067D90
		public Bullet(Bullet other)
		{
			base.CopyFrom(other);
			this.mHitBall = other.mHitBall;
			this.mVelX = other.mVelX;
			this.mVelY = other.mVelY;
			this.mHitX = other.mHitX;
			this.mHitY = other.mHitY;
			this.mHitDX = other.mHitDX;
			this.mHitDY = other.mHitDY;
			this.mDestX = other.mDestX;
			this.mDestY = other.mDestY;
			this.mHitPercent = other.mHitPercent;
			this.mMergeSpeed = other.mMergeSpeed;
			this.mAngleFired = other.mAngleFired;
			this.mUpdateCount = other.mUpdateCount;
			this.mHitInFront = other.mHitInFront;
			this.mHaveSetPrevBall = other.mHaveSetPrevBall;
			this.mJustFired = other.mJustFired;
			this.mDoNewMerge = other.mDoNewMerge;
			this.mSkip = other.mSkip;
			this.mGapInfo.AddRange(other.mGapInfo.ToArray());
			Array.Copy(other.mCurCurvePoint, this.mCurCurvePoint, this.mCurCurvePoint.Length);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00069CC8 File Offset: 0x00067EC8
		public override void Dispose()
		{
			this.SetBallInfo(null);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00069CD1 File Offset: 0x00067ED1
		public void SetBallInfo(Bullet theBullet)
		{
			if (this.mHitBall != null)
			{
				this.mHitBall.SetBullet(theBullet);
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00069CE7 File Offset: 0x00067EE7
		public void SetVelocity(float vx, float vy)
		{
			this.mVelX = vx;
			this.mVelY = vy;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00069CF8 File Offset: 0x00067EF8
		public void SetHitBall(Ball theBall, bool hitInFront)
		{
			this.SetBallInfo(null);
			this.mHaveSetPrevBall = false;
			this.mHitBall = theBall;
			this.mHitX = this.mX;
			this.mHitY = this.mY;
			this.mHitDX = this.mX - theBall.GetX();
			this.mHitDY = this.mY - theBall.GetY();
			this.mHitPercent = 0f;
			this.mHitInFront = hitInFront;
			this.SetBallInfo(this);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00069D74 File Offset: 0x00067F74
		public void CheckSetHitBallToPrevBall()
		{
			if (this.mHaveSetPrevBall || this.mHitBall == null)
			{
				return;
			}
			Ball prevBall = this.mHitBall.GetPrevBall();
			if (prevBall == null)
			{
				return;
			}
			if (prevBall.CollidesWithPhysically(this) && !prevBall.GetIsExploding())
			{
				this.mHaveSetPrevBall = true;
				this.SetBallInfo(null);
				this.mHitBall = prevBall;
				this.mHitInFront = true;
				this.mHitX = this.mX;
				this.mHitY = this.mY;
				this.mHitDX = this.mX - prevBall.GetX();
				this.mHitDY = this.mY - prevBall.GetY();
				this.mHitPercent = 0f;
				this.SetBallInfo(this);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00069E1F File Offset: 0x0006801F
		public void SetDestPos(float x, float y)
		{
			this.mDestX = x;
			this.mDestY = y;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00069E30 File Offset: 0x00068030
		public void SetDXPos()
		{
			float num = 1f - this.mHitPercent;
			this.mX += this.mHitDX * num;
			this.mY += this.mHitDY * num;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00069E74 File Offset: 0x00068074
		public void Update(float theAmount)
		{
			this.mUpdateCount++;
			this.mDisplayType = this.mColorType;
			if (this.mHitBall == null)
			{
				float num = this.mVelX * theAmount;
				float num2 = this.mVelY * theAmount;
				this.mX += num;
				this.mY += num2;
			}
			else if (!this.mExploding)
			{
				this.mHitPercent += this.mMergeSpeed;
				if (this.mHitPercent > 1f)
				{
					this.mHitPercent = 1f;
				}
				if (!this.mDoNewMerge)
				{
					this.mX = this.mHitX + this.mHitPercent * (this.mDestX - this.mHitX);
					this.mY = this.mHitY + this.mHitPercent * (this.mDestY - this.mHitY);
				}
			}
			base.UpdateRotation();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00069F56 File Offset: 0x00068156
		public new void Update()
		{
			this.Update(1f);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00069F63 File Offset: 0x00068163
		public void MergeFully()
		{
			this.mHitPercent = 1f;
			this.Update();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00069F78 File Offset: 0x00068178
		public Ball GetPushBall()
		{
			if (this.mHitBall == null)
			{
				return null;
			}
			Ball ball = this.mHitInFront ? this.mHitBall.GetNextBall() : this.mHitBall;
			if (ball != null && (this.mDoNewMerge || ball.CollidesWithPhysically(this)))
			{
				return ball;
			}
			return null;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00069FC4 File Offset: 0x000681C4
		public void UpdateHitPos()
		{
			this.mHitX = this.mX;
			this.mHitY = this.mY;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00069FDE File Offset: 0x000681DE
		public void SetCurCurvePoint(int theCurveNum, int thePoint)
		{
			this.mCurCurvePoint[theCurveNum] = thePoint;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00069FE9 File Offset: 0x000681E9
		public int GetCurCurvePoint(int theCurveNum)
		{
			return this.mCurCurvePoint[theCurveNum];
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00069FF4 File Offset: 0x000681F4
		public bool AddGapInfo(int theCurve, int theDist, int theBallId)
		{
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (gapInfo.mBallId == theBallId)
				{
					return false;
				}
			}
			GapInfo gapInfo2 = new GapInfo();
			gapInfo2.mBallId = theBallId;
			gapInfo2.mDist = theDist;
			gapInfo2.mCurve = theCurve;
			this.mGapInfo.Add(gapInfo2);
			return true;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0006A078 File Offset: 0x00068278
		public int GetCurGapBall(int theCurveNum)
		{
			int result = 0;
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (gapInfo.mCurve == theCurveNum)
				{
					result = gapInfo.mBallId;
				}
			}
			return result;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0006A0D8 File Offset: 0x000682D8
		public int GetMinGapDist()
		{
			int num = 0;
			foreach (GapInfo gapInfo in this.mGapInfo)
			{
				if (num == 0 || gapInfo.mDist < num)
				{
					num = gapInfo.mDist;
				}
			}
			return num;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0006A13C File Offset: 0x0006833C
		public void RemoveGapInfoForBall(int theBallId)
		{
			int num = 0;
			while (num != Enumerable.Count<GapInfo>(this.mGapInfo))
			{
				if (this.mGapInfo[num].mBallId == theBallId)
				{
					this.mGapInfo.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0006A184 File Offset: 0x00068384
		public override void SyncState(DataSync theSync)
		{
			base.SyncState(theSync);
			theSync.SyncFloat(ref this.mVelX);
			theSync.SyncFloat(ref this.mVelY);
			theSync.SyncBoolean(ref this.mHitInFront);
			theSync.SyncBoolean(ref this.mHaveSetPrevBall);
			theSync.SyncFloat(ref this.mHitX);
			theSync.SyncFloat(ref this.mHitY);
			theSync.SyncFloat(ref this.mDestX);
			theSync.SyncFloat(ref this.mDestY);
			theSync.SyncFloat(ref this.mHitDX);
			theSync.SyncFloat(ref this.mHitDY);
			theSync.SyncLong(ref this.mUpdateCount);
			theSync.SyncBoolean(ref this.mHitInFront);
			theSync.SyncBoolean(ref this.mHaveSetPrevBall);
			theSync.SyncBoolean(ref this.mJustFired);
			theSync.SyncBoolean(ref this.mDoNewMerge);
			theSync.SyncFloat(ref this.mHitPercent);
			theSync.SyncFloat(ref this.mMergeSpeed);
			theSync.SyncFloat(ref this.mAngleFired);
			theSync.SyncBoolean(ref this.mSkip);
			for (int i = 0; i < 4; i++)
			{
				theSync.SyncLong(ref this.mCurCurvePoint[i]);
			}
			theSync.SyncPointer(this);
			this.SyncListGapInfos(theSync, true);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0006A2AC File Offset: 0x000684AC
		private void SyncListGapInfos(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mGapInfo.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					GapInfo gapInfo = new GapInfo();
					gapInfo.SyncState(sync);
					this.mGapInfo.Add(gapInfo);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mGapInfo.Count);
			foreach (GapInfo gapInfo2 in this.mGapInfo)
			{
				gapInfo2.SyncState(sync);
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0006A360 File Offset: 0x00068560
		public override void Draw(SexyGraphics g, int xoff, int yoff)
		{
			if (!this.mIsCannon)
			{
				float mWayPoint = this.mWayPoint;
				this.mWayPoint = 0f;
				base.Draw(g, xoff, yoff);
				this.mWayPoint = mWayPoint;
				return;
			}
			if (this.mFrog.mBoard.LevelIsSkeletonBoss())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_SKELETON_GLOWBALL);
				float num = Common._S(this.mX) - (float)(imageByID.mWidth / 2);
				float num2 = Common._S(this.mY) - (float)(imageByID.mHeight / 2);
				g.DrawImage(imageByID, (int)num, (int)num2);
				g.PushState();
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				int alphaFromUpdateCount = Common.GetAlphaFromUpdateCount(this.mUpdateCount, Common._M(64));
				g.SetColor(255, 255, 255, alphaFromUpdateCount);
				g.DrawImage(imageByID, (int)num, (int)num2);
				g.PopState();
				return;
			}
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_CANNON_BALL);
			float num3 = Common._S(this.mX) - (float)(imageByID2.mWidth / 2);
			float num4 = Common._S(this.mY) - (float)(imageByID2.mHeight / 2);
			if (g.Is3D())
			{
				g.DrawImageRotatedF(imageByID2, num3, num4, (double)(this.mRotation + 3.14159f));
				return;
			}
			g.DrawImageRotated(imageByID2, (int)num3, (int)num4, (double)(this.mRotation + 3.14159f));
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0006A4BC File Offset: 0x000686BC
		public new void Draw(SexyGraphics g)
		{
			this.Draw(g, 0, 0);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0006A4C7 File Offset: 0x000686C7
		public Ball GetHitBall()
		{
			return this.mHitBall;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0006A4CF File Offset: 0x000686CF
		public float GetHitPercent()
		{
			return this.mHitPercent;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0006A4D7 File Offset: 0x000686D7
		public float GetVelX()
		{
			return this.mVelX;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0006A4DF File Offset: 0x000686DF
		public float GetVelY()
		{
			return this.mVelY;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0006A4E7 File Offset: 0x000686E7
		public bool GetHitInFront()
		{
			return this.mHitInFront;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0006A4EF File Offset: 0x000686EF
		public bool GetJustFired()
		{
			return this.mJustFired;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0006A4F7 File Offset: 0x000686F7
		public new int GetNumGaps()
		{
			return Enumerable.Count<GapInfo>(this.mGapInfo);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0006A504 File Offset: 0x00068704
		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0006A50C File Offset: 0x0006870C
		public void SetJustFired(bool fired)
		{
			this.mJustFired = fired;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0006A515 File Offset: 0x00068715
		public void SetMergeSpeed(float theSpeed)
		{
			this.mMergeSpeed = theSpeed;
		}

		// Token: 0x04001337 RID: 4919
		public Ball mHitBall;

		// Token: 0x04001338 RID: 4920
		public float mVelX;

		// Token: 0x04001339 RID: 4921
		public float mVelY;

		// Token: 0x0400133A RID: 4922
		public float mHitX;

		// Token: 0x0400133B RID: 4923
		public float mHitY;

		// Token: 0x0400133C RID: 4924
		public float mHitDX;

		// Token: 0x0400133D RID: 4925
		public float mHitDY;

		// Token: 0x0400133E RID: 4926
		public float mDestX;

		// Token: 0x0400133F RID: 4927
		public float mDestY;

		// Token: 0x04001340 RID: 4928
		public float mHitPercent;

		// Token: 0x04001341 RID: 4929
		public float mMergeSpeed;

		// Token: 0x04001342 RID: 4930
		public float mAngleFired;

		// Token: 0x04001343 RID: 4931
		public new int mUpdateCount;

		// Token: 0x04001344 RID: 4932
		public bool mHitInFront;

		// Token: 0x04001345 RID: 4933
		public bool mHaveSetPrevBall;

		// Token: 0x04001346 RID: 4934
		public bool mJustFired;

		// Token: 0x04001347 RID: 4935
		public bool mDoNewMerge;

		// Token: 0x04001348 RID: 4936
		public bool mSkip;

		// Token: 0x04001349 RID: 4937
		public List<GapInfo> mGapInfo = new List<GapInfo>();

		// Token: 0x0400134A RID: 4938
		public int[] mCurCurvePoint = new int[4];
	}
}
