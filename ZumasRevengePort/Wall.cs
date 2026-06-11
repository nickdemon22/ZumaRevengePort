using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	// Token: 0x02000088 RID: 136
	public class Wall
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00085258 File Offset: 0x00083458
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mWidth);
			sync.SyncFloat(ref this.mHeight);
			sync.SyncLong(ref this.mStrength);
			sync.SyncLong(ref this.mOrgStrength);
			sync.SyncLong(ref this.mMinRespawnTimer);
			sync.SyncLong(ref this.mMaxRespawnTimer);
			sync.SyncLong(ref this.mCurRespawnTimer);
			sync.SyncLong(ref this.mMinLifeTimer);
			sync.SyncLong(ref this.mMaxLifeTimer);
			sync.SyncLong(ref this.mCurLifeTimer);
			sync.SyncLong(ref this.mId);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
			sync.SyncLong(ref this.mColor.mAlpha);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mSize);
			sync.SyncLong(ref this.mMaxSize);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mExpCel);
			sync.SyncLong(ref this.mType);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mSpacing);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000853C0 File Offset: 0x000835C0
		public void Update()
		{
			if (this.mCurLifeTimer > 0 && --this.mCurLifeTimer == 0)
			{
				this.mType = 0;
				this.mCurRespawnTimer = MathUtils.IntRange(this.mMinRespawnTimer, this.mMaxRespawnTimer);
				return;
			}
			if (this.mCurRespawnTimer > 0 && --this.mCurRespawnTimer == 0)
			{
				this.mType = 1;
				this.mCurLifeTimer = MathUtils.IntRange(this.mMinLifeTimer, this.mMaxLifeTimer);
			}
			if (this.mType == 0 && this.mCurRespawnTimer <= 0)
			{
				return;
			}
			this.mUpdateCount++;
			if (this.mVX != 0f)
			{
				this.mX += this.mVX;
			}
			if (this.mVY != 0f)
			{
				this.mY += this.mVY;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000854A2 File Offset: 0x000836A2
		public void Draw(SexyGraphics g)
		{
			if (this.mStrength != 0)
			{
				int num = this.mType;
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000854B3 File Offset: 0x000836B3
		public bool Hit()
		{
			return false;
		}

		// Token: 0x0400152D RID: 5421
		public float mX;

		// Token: 0x0400152E RID: 5422
		public float mY;

		// Token: 0x0400152F RID: 5423
		public float mWidth;

		// Token: 0x04001530 RID: 5424
		public float mHeight;

		// Token: 0x04001531 RID: 5425
		public float mVX;

		// Token: 0x04001532 RID: 5426
		public float mVY;

		// Token: 0x04001533 RID: 5427
		public int mSpacing;

		// Token: 0x04001534 RID: 5428
		public int mStrength;

		// Token: 0x04001535 RID: 5429
		public int mOrgStrength;

		// Token: 0x04001536 RID: 5430
		public int mMinRespawnTimer;

		// Token: 0x04001537 RID: 5431
		public int mMaxRespawnTimer;

		// Token: 0x04001538 RID: 5432
		public int mCurRespawnTimer;

		// Token: 0x04001539 RID: 5433
		public int mMinLifeTimer;

		// Token: 0x0400153A RID: 5434
		public int mMaxLifeTimer;

		// Token: 0x0400153B RID: 5435
		public int mCurLifeTimer;

		// Token: 0x0400153C RID: 5436
		public int mId;

		// Token: 0x0400153D RID: 5437
		public int mUpdateCount;

		// Token: 0x0400153E RID: 5438
		public int mState;

		// Token: 0x0400153F RID: 5439
		public int mSize = 1;

		// Token: 0x04001540 RID: 5440
		public int mMaxSize = 1;

		// Token: 0x04001541 RID: 5441
		public Color mColor = default(Color);

		// Token: 0x04001542 RID: 5442
		public Image mImage;

		// Token: 0x04001543 RID: 5443
		public int mCel;

		// Token: 0x04001544 RID: 5444
		public int mExpCel;

		// Token: 0x04001545 RID: 5445
		public int mType = 1;
	}
}
