using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x020000EA RID: 234
	public class InkParticle
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0009A7A4 File Offset: 0x000989A4
		public void SyncState(DataSync s)
		{
			Buffer buffer = s.GetBuffer();
			if (s.isRead())
			{
				buffer.WriteBoolean(this.mImage == Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE1));
			}
			else if (buffer.ReadBoolean())
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE1);
			}
			else
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE2);
			}
			s.SyncFloat(ref this.mWidthPct);
			s.SyncFloat(ref this.mHeightPct);
			s.SyncFloat(ref this.mX);
			s.SyncFloat(ref this.mY);
			s.SyncFloat(ref this.mAngle);
			s.SyncFloat(ref this.mInitSpeed);
			s.SyncFloat(ref this.mVX);
			s.SyncFloat(ref this.mVY);
			s.SyncFloat(ref this.mGravity);
			s.SyncFloat(ref this.mAlpha);
			s.SyncFloat(ref this.mAlphaRate);
			s.SyncFloat(ref this.mJiggleRate);
			s.SyncLong(ref this.mJiggleDir);
			s.SyncLong(ref this.mPostHitCount);
		}

		// Token: 0x0400181D RID: 6173
		public float mWidthPct;

		// Token: 0x0400181E RID: 6174
		public float mHeightPct;

		// Token: 0x0400181F RID: 6175
		public float mAngle;

		// Token: 0x04001820 RID: 6176
		public float mX;

		// Token: 0x04001821 RID: 6177
		public float mY;

		// Token: 0x04001822 RID: 6178
		public Image mImage;

		// Token: 0x04001823 RID: 6179
		public float mInitSpeed;

		// Token: 0x04001824 RID: 6180
		public float mVX;

		// Token: 0x04001825 RID: 6181
		public float mVY;

		// Token: 0x04001826 RID: 6182
		public float mGravity;

		// Token: 0x04001827 RID: 6183
		public float mAlpha;

		// Token: 0x04001828 RID: 6184
		public float mAlphaRate;

		// Token: 0x04001829 RID: 6185
		public float mJiggleRate;

		// Token: 0x0400182A RID: 6186
		public int mJiggleDir;

		// Token: 0x0400182B RID: 6187
		public int mPostHitCount;
	}
}
