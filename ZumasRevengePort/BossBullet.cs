using System;

namespace ZumasRevenge
{
	// Token: 0x020000D7 RID: 215
	public class BossBullet : IDisposable
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00099F00 File Offset: 0x00098100
		public BossBullet()
		{
			this.mDelay = (this.mBouncesLeft = (this.mUpdateCount = (this.mOffscreenPause = 0)));
			this.mGravity = (this.mTargetVX = (this.mTargetVY = 0f));
			this.mDeleteInstantly = false;
			this.mSize = 1f;
			this.mShotType = 0;
			this.mId = -1;
			this.mInitialSpeed = 0f;
			this.mVolcanoShot = (this.mHoming = false);
			this.mAmp = (this.mFreq = 0f);
			this.mSineMotion = false;
			this.mCanHitPlayer = true;
			this.mState = 0;
			this.mImageNum = 0;
			this.mAngle = 0f;
			this.mAlpha = 255f;
			this.mCel = 0;
			this.mData = null;
			this.mBossShoot = null;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00099FF0 File Offset: 0x000981F0
		public BossBullet(BossBullet rhs) : this()
		{
			if (rhs == null)
			{
				return;
			}
			this.mDelay = rhs.mDelay;
			this.mBouncesLeft = rhs.mBouncesLeft;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mOffscreenPause = rhs.mOffscreenPause;
			this.mGravity = rhs.mGravity;
			this.mTargetVX = rhs.mTargetVX;
			this.mTargetVY = rhs.mTargetVY;
			this.mDeleteInstantly = rhs.mDeleteInstantly;
			this.mSize = rhs.mSize;
			this.mShotType = rhs.mShotType;
			this.mId = rhs.mId;
			this.mInitialSpeed = rhs.mInitialSpeed;
			this.mVolcanoShot = rhs.mVolcanoShot;
			this.mHoming = rhs.mHoming;
			this.mAmp = rhs.mAmp;
			this.mFreq = rhs.mFreq;
			this.mSineMotion = rhs.mSineMotion;
			this.mCanHitPlayer = rhs.mCanHitPlayer;
			this.mState = rhs.mState;
			this.mImageNum = rhs.mImageNum;
			this.mAngle = rhs.mAngle;
			this.mAlpha = rhs.mAlpha;
			this.mCel = rhs.mCel;
			this.mData = rhs.mData;
			this.mBossShoot = rhs.mBossShoot;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0009A133 File Offset: 0x00098333
		public virtual void Dispose()
		{
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0009A138 File Offset: 0x00098338
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mAmp);
			sync.SyncFloat(ref this.mFreq);
			sync.SyncBoolean(ref this.mSineMotion);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mImageNum);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncBoolean(ref this.mHoming);
			sync.SyncFloat(ref this.mTargetVX);
			sync.SyncBoolean(ref this.mCanHitPlayer);
			sync.SyncFloat(ref this.mTargetVY);
			sync.SyncFloat(ref this.mInitialSpeed);
			sync.SyncLong(ref this.mOffscreenPause);
			sync.SyncBoolean(ref this.mVolcanoShot);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mShotType);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mBouncesLeft);
			sync.SyncLong(ref this.mId);
		}

		// Token: 0x040017B4 RID: 6068
		public float mVX;

		// Token: 0x040017B5 RID: 6069
		public float mVY;

		// Token: 0x040017B6 RID: 6070
		public float mInitialSpeed;

		// Token: 0x040017B7 RID: 6071
		public float mTargetVX;

		// Token: 0x040017B8 RID: 6072
		public float mTargetVY;

		// Token: 0x040017B9 RID: 6073
		public float mX;

		// Token: 0x040017BA RID: 6074
		public float mY;

		// Token: 0x040017BB RID: 6075
		public float mAmp;

		// Token: 0x040017BC RID: 6076
		public float mFreq;

		// Token: 0x040017BD RID: 6077
		public float mGravity;

		// Token: 0x040017BE RID: 6078
		public float mAngle;

		// Token: 0x040017BF RID: 6079
		public float mSize;

		// Token: 0x040017C0 RID: 6080
		public float mAlpha;

		// Token: 0x040017C1 RID: 6081
		public bool mSineMotion;

		// Token: 0x040017C2 RID: 6082
		public bool mHoming;

		// Token: 0x040017C3 RID: 6083
		public bool mCanHitPlayer;

		// Token: 0x040017C4 RID: 6084
		public bool mDeleteInstantly;

		// Token: 0x040017C5 RID: 6085
		public int mBouncesLeft;

		// Token: 0x040017C6 RID: 6086
		public int mId;

		// Token: 0x040017C7 RID: 6087
		public int mUpdateCount;

		// Token: 0x040017C8 RID: 6088
		public int mDelay;

		// Token: 0x040017C9 RID: 6089
		public int mState;

		// Token: 0x040017CA RID: 6090
		public int mImageNum;

		// Token: 0x040017CB RID: 6091
		public int mOffscreenPause;

		// Token: 0x040017CC RID: 6092
		public int mShotType;

		// Token: 0x040017CD RID: 6093
		public int mCel;

		// Token: 0x040017CE RID: 6094
		public bool mVolcanoShot;

		// Token: 0x040017CF RID: 6095
		public object mData;

		// Token: 0x040017D0 RID: 6096
		public BossShoot mBossShoot;
	}
}
