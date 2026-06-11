using System;
using System.Linq;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200008F RID: 143
	public class ReversePowerEffect : PowerEffect
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x000889A7 File Offset: 0x00086BA7
		public ReversePowerEffect()
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000889B0 File Offset: 0x00086BB0
		public ReversePowerEffect(float x, float y, Ball b) : base(x, y)
		{
			this.mScale = 1f;
			this.mCurve = GameApp.gApp.GetBoard().GetCurve(b);
			this.mStartWaypoint = (this.mWaypoint = b.GetWayPoint());
			SexyVector2 pointPos = this.mCurve.mWayPointMgr.GetPointPos(this.mWaypoint);
			this.mX = pointPos.x;
			this.mY = pointPos.y;
			this.mRotation = this.mCurve.mWayPointMgr.GetRotationForPoint((int)this.mWaypoint);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00088A4C File Offset: 0x00086C4C
		public override void Update()
		{
			if (this.IsDone())
			{
				return;
			}
			base.Update();
			if (!this.mDone)
			{
				return;
			}
			this.mWaypoint -= (float)Common._M(20);
			SexyVector2 pointPos = this.mCurve.mWayPointMgr.GetPointPos(this.mWaypoint);
			this.mX = pointPos.x;
			this.mY = pointPos.y;
			this.mRotation = this.mCurve.mWayPointMgr.GetRotationForPoint((int)this.mWaypoint);
			this.mScale = this.mWaypoint / this.mStartWaypoint;
			if (this.mScale < 0f)
			{
				this.mDone = true;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00088B00 File Offset: 0x00086D00
		public override void Draw(SexyGraphics g)
		{
			if (this.IsDone())
			{
				return;
			}
			g.PushState();
			g.SetColorizeImages(true);
			g.SetDrawMode(1);
			int num = this.mDrawReverse ? (Enumerable.Count<EffectItem>(this.mItems) - 1) : 0;
			int num2 = this.mDrawReverse ? 0 : Enumerable.Count<EffectItem>(this.mItems);
			int num3 = num;
			while (this.mDrawReverse ? (num3 >= num2) : (num3 < num2))
			{
				EffectItem effectItem = this.mItems[num3];
				Color mColor = effectItem.mColor;
				mColor.mAlpha = (int)Component.GetComponentValue(effectItem.mOpacity, 255f, this.mUpdateCount);
				if (mColor.mAlpha != 0)
				{
					float num4 = this.mDone ? this.mScale : Component.GetComponentValue(effectItem.mScale, 1f, this.mUpdateCount);
					g.SetColor(mColor);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.RotateRad(this.mRotation);
					this.mGlobalTranform.Scale(num4, num4);
					Rect celRect = effectItem.mImage.GetCelRect(effectItem.mCel);
					if (g.Is3D())
					{
						g.DrawImageTransformF(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
					else
					{
						g.DrawImageTransform(effectItem.mImage, this.mGlobalTranform, celRect, Common._S(this.mX), Common._S(this.mY));
					}
				}
				num3 += (this.mDrawReverse ? -1 : 1);
			}
			g.PopState();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00088C9B File Offset: 0x00086E9B
		public override bool IsDone()
		{
			return this.mDone && this.mWaypoint < 0f;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00088CB4 File Offset: 0x00086EB4
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncFloat(ref this.mWaypoint);
			sync.SyncFloat(ref this.mStartWaypoint);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mScale);
			sync.SyncPointer(this);
		}

		// Token: 0x04001593 RID: 5523
		protected float mWaypoint;

		// Token: 0x04001594 RID: 5524
		protected float mStartWaypoint;

		// Token: 0x04001595 RID: 5525
		protected float mRotation;

		// Token: 0x04001596 RID: 5526
		protected float mScale;

		// Token: 0x04001597 RID: 5527
		public CurveMgr mCurve;
	}
}
