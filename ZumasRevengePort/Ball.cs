using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000033 RID: 51
	public class Ball
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0004625C File Offset: 0x0004445C
		public void DrawStandardPower(SexyGraphics g, int img_id, int cel, int thePowerType)
		{
			GameApp gApp = GameApp.gApp;
			ResID id = (ResID)(img_id + this.mColorType);
			if (gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_POWERUPS_GREEN_CBM;
			}
			else if (gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_POWERUPS_PURPLE_CBM;
			}
			else if (gApp.mColorblind && this.mColorType == 5)
			{
				id = ResID.IMAGE_POWERUPS_WHITE_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			Image imageByID2;
			if (this.mPowerType == PowerType.PowerType_MoveBackwards)
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUP_REVERSE_ANYCOLOR);
			}
			else if (this.mPowerType == PowerType.PowerType_Laser)
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUP_LAZER_ANYCOLOR);
			}
			else
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUPS_PULSES);
			}
			float num = Common._S(this.mX) - (float)(imageByID.GetCelWidth() / 2);
			float num2 = Common._S(this.mY) - (float)(imageByID.GetCelHeight() / 2);
			float num3 = (this.mPowerType == PowerType.PowerType_MoveBackwards) ? 1.570795f : -1.570795f;
			bool flag = gApp.Is3DAccelerated();
			if (flag)
			{
				Rect celRect = imageByID.GetCelRect(cel);
				g.DrawImageRotatedF(imageByID, (float)((int)num), (float)((int)num2), (double)(this.mRotation + num3), celRect);
			}
			else
			{
				BlendedImage blendedImage = Ball.CreateBlendedPowerup(thePowerType, this.mColorType, imageByID, cel);
				blendedImage.Draw(g, num, num2);
			}
			if (this.mPowerType == PowerType.PowerType_MoveBackwards || this.mPowerType == PowerType.PowerType_Laser)
			{
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				g.SetColor(new Color(Common.gBrightBallColors[this.mColorType]));
				float num4 = (float)imageByID2.GetCelWidth() / 2f;
				float num5 = (float)imageByID2.GetCelHeight() / 2f;
				num = Common._S(this.mX) - num4;
				num2 = Common._S(this.mY) - num5;
				Rect celRect2 = imageByID2.GetCelRect(this.mCel);
				if (flag)
				{
					g.DrawImageRotatedF(imageByID2, num, num2 - (float)Common._M(0), (double)(this.mRotation + num3), num4, num5 + (float)Common._M1(0), celRect2);
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
				return;
			}
			if (this.mPulseState < 2)
			{
				g.SetColorizeImages(true);
				int mAlpha = 255 - this.mPulseTimer * ((this.mPulseState == 0) ? Common._M(4) : Common._M1(2));
				Color color;
				color = new Color(Common.gBrightBallColors[this.mColorType]);
				if (gApp.mColorblind)
				{
					color = new Color(Color.White);
				}
				color.mAlpha = mAlpha;
				g.SetColor(color);
				g.SetDrawMode(1);
				float num6 = (float)imageByID2.GetCelWidth() / 2f;
				float num7 = (float)imageByID2.GetCelHeight() / 2f;
				num = Common._S(this.mX) - num6;
				num2 = Common._S(this.mY) - num7;
				Rect celRect3 = imageByID2.GetCelRect(cel);
				if (flag)
				{
					g.DrawImageRotatedF(imageByID2, num, num2 - (float)Common._M(0), (double)(this.mRotation + num3), num6, num7 + (float)Common._M1(0), celRect3);
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00046558 File Offset: 0x00044758
		public void DrawNewPower(SexyGraphics g, char theLetter, int xoff, int yoff)
		{
			GameApp gApp = GameApp.gApp;
			bool flag = gApp.Is3DAccelerated();
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BALL);
			float num = Common._S(this.mX + (float)xoff) - (float)(imageByID.mWidth / 2);
			float num2 = Common._S(this.mY + (float)yoff) - (float)(imageByID.mHeight / 2);
			g.SetColorizeImages(true);
			g.SetColor(new Color(Common.gBallColors[this.mColorType]));
			if (MathUtils._eq(this.mRadius, (float)Common.GetDefaultBallRadius()))
			{
				if (flag)
				{
					g.DrawImageF(imageByID, num, num2);
				}
				else
				{
					g.DrawImage(imageByID, (int)num, (int)num2);
				}
			}
			else
			{
				this.mGlobalTransform.Reset();
				float num3 = this.mRadius / (float)Common.GetDefaultBallRadius();
				this.mGlobalTransform.Scale(num3, num3);
				num = this.mX + (float)xoff;
				num2 = this.mY + (float)yoff;
				if (flag)
				{
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, num, num2);
				}
				else
				{
					g.DrawImageTransform(imageByID, this.mGlobalTransform, num, num2);
				}
			}
			g.SetColorizeImages(false);
			g.SetColor(new Color(Common._M(16777215)));
			g.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			string text = theLetter.ToString();
			g.DrawString(text, (int)(Common._S(this.mX + (float)xoff) - (float)(g.GetFont().CharWidth(theLetter) / 2)), (int)(Common._S(this.mY + (float)yoff) - (float)(g.GetFont().GetHeight() / 2) + (float)g.GetFont().GetAscent()));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000466E7 File Offset: 0x000448E7
		public void DrawNewPower(SexyGraphics g, char theLetter)
		{
			this.DrawNewPower(g, theLetter, 0, 0);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000466F4 File Offset: 0x000448F4
		public void DrawPower(SexyGraphics g)
		{
			PowerType thePowerType = this.mPowerType;
			switch (thePowerType)
			{
			case PowerType.PowerType_ProximityBomb:
				this.DrawStandardPower(g, 870, 3, (int)thePowerType);
				return;
			case PowerType.PowerType_SlowDown:
				this.DrawStandardPower(g, 870, 2, (int)thePowerType);
				return;
			case PowerType.PowerType_Accuracy:
				this.DrawStandardPower(g, 870, 0, (int)thePowerType);
				return;
			case PowerType.PowerType_MoveBackwards:
				this.DrawStandardPower(g, 870, 4, (int)thePowerType);
				return;
			case PowerType.PowerType_Lob:
			case PowerType.PowerType_BombBullet:
			case PowerType.PowerType_BallEater:
			case PowerType.PowerType_Fireball:
			case PowerType.PowerType_ShieldFrog:
			case PowerType.PowerType_FreezeBoss:
				break;
			case PowerType.PowerType_Cannon:
				this.DrawStandardPower(g, 870, 5, (int)thePowerType);
				return;
			case PowerType.PowerType_ColorNuke:
				this.DrawStandardPower(g, 870, 1, (int)thePowerType);
				return;
			case PowerType.PowerType_Laser:
				this.DrawStandardPower(g, 870, 6, (int)thePowerType);
				return;
			case PowerType.PowerType_GauntletMultBall:
				this.DrawMultPowerup(g);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000467B9 File Offset: 0x000449B9
		public void DrawExplosion(SexyGraphics g)
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000467BC File Offset: 0x000449BC
		protected void DoDrawBase(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				this.DrawPower(g);
				return;
			}
			int num = (GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType;
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			float x = Common._S(this.mX + (float)xoff - this.mRadius);
			float y = Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, Common._S(this.mX + (float)xoff), Common._S(this.mY + (float)yoff));
				return;
			}
			BlendedImage blendedImage = Ball.CreateBlendedBall(num);
			blendedImage.Draw(g, x, y);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00046918 File Offset: 0x00044B18
		protected void DoDrawAdditive(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				return;
			}
			int num = (GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType;
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			Common._S(this.mX + (float)xoff - this.mRadius);
			Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				if (this.mHilightPulse)
				{
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255);
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, Common._S(this.mX), Common._S(this.mY));
					g.SetDrawMode(0);
					g.SetColorizeImages(false);
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00046A8C File Offset: 0x00044C8C
		public void DoDraw(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				this.DrawPower(g);
				return;
			}
			int num = (GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType;
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			float x = Common._S(this.mX + (float)xoff - this.mRadius);
			float y = Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, Common._S(this.mX + (float)xoff), Common._S(this.mY + (float)yoff));
				if (this.mHilightPulse)
				{
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255);
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, Common._S(this.mX), Common._S(this.mY));
					g.SetDrawMode(0);
					g.SetColorizeImages(false);
					return;
				}
			}
			else
			{
				BlendedImage blendedImage = Ball.CreateBlendedBall(num);
				blendedImage.Draw(g, x, y);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00046C47 File Offset: 0x00044E47
		public void DoDraw(SexyGraphics g)
		{
			this.DoDraw(g, 0, 0);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00046C54 File Offset: 0x00044E54
		public void DrawMultPowerup(SexyGraphics g)
		{
			GameApp gApp = GameApp.gApp;
			ResID id = ResID.IMAGE_MULTIPLIER_BALL_BLUE + this.GetColorType();
			bool flag = true;
			if (gApp.mColorblind && this.mColorType == 3)
			{
				flag = false;
				id = (g.Is3D() ? ResID.IMAGE_GREEN_BALL_CBM : ResID.IMAGE_MULTIPLIER_BALL_GREEN_CBM);
			}
			else if (gApp.mColorblind && this.mColorType == 4)
			{
				flag = false;
				id = (g.Is3D() ? ResID.IMAGE_PURPLE_BALL_CBM : ResID.IMAGE_MULTIPLIER_BALL_PURPLE_CBM);
			}
			Image imageByID = Res.GetImageByID(id);
			float num = Common._S(this.mX) - (float)(imageByID.GetCelWidth() / 2);
			float num2 = Common._S(this.mY) - (float)(imageByID.GetCelHeight() / 2);
			if (flag)
			{
				int multAlpha = Ball.GetMultAlpha(this.mMultBallCel);
				int num3 = Common._M(255);
				if (multAlpha != num3)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, multAlpha);
				}
				BlendedImage blendedImage = null;
				BlendedImage blendedImage2 = null;
				if (!g.Is3D())
				{
					blendedImage = Ball.CreateBlendedPowerup(13, this.mColorType, imageByID, this.mMultBallCel);
					blendedImage2 = Ball.CreateBlendedPowerup(14, this.mColorType, imageByID, this.mMultBallCel2);
				}
				Rect celRect = imageByID.GetCelRect(this.mMultBallCel);
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					blendedImage.Draw(g, num, num2);
				}
				g.SetColorizeImages(false);
				multAlpha = Ball.GetMultAlpha(this.mMultBallCel2);
				if (multAlpha != num3)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, multAlpha);
				}
				celRect = imageByID.GetCelRect(this.mMultBallCel2);
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					blendedImage2.Draw(g, num, num2);
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(1);
				g.SetColor(255, 255, 255, Common._M(204));
				g.SetColorizeImages(true);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_OUTER);
				celRect = imageByID2.GetCelRect(this.GetFrame(imageByID2, Common._M(2)));
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID2, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					g.DrawImageRotated(imageByID2, (int)num, (int)num2, (double)this.mRotation, celRect);
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(0);
				return;
			}
			if (g.Is3D())
			{
				Rect celRect2 = imageByID.GetCelRect(0);
				g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect2);
				return;
			}
			BlendedImage blendedImage3 = Ball.CreateBlendedPowerup(13, this.mColorType, imageByID, 0);
			blendedImage3.Draw(g, num, num2);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00046F01 File Offset: 0x00045101
		public void UpdateProxmityBombExplosion()
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00046F04 File Offset: 0x00045104
		public void UpdateRotation()
		{
			if (this.mRotationInc != 0f)
			{
				this.mRotation += this.mRotationInc;
				if ((this.mRotationInc > 0f && this.mRotation > this.mDestRotation) || (this.mRotationInc < 0f && this.mRotation < this.mDestRotation))
				{
					this.mRotation = this.mDestRotation;
					this.mRotationInc = 0f;
				}
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00046F80 File Offset: 0x00045180
		public void SetupDefaultOverlayPulse()
		{
			int num = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
			this.mOverlayPulse.Clear();
			if (num == 128)
			{
				this.mOverlayPulse.Add(new Component(128f, 178f, this.mUpdateCount, this.mUpdateCount + 50));
				this.mOverlayPulse.Add(new Component(178f, 255f, this.mUpdateCount + 51, this.mUpdateCount + 60));
				this.mOverlayPulse.Add(new Component(255f, 128f, this.mUpdateCount + 61, this.mUpdateCount + 80));
				return;
			}
			this.mOverlayPulse.Add(new Component((float)num, 128f, this.mUpdateCount, this.mUpdateCount + 10));
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00047064 File Offset: 0x00045264
		public void SetupElectricOverlayPulse(bool force_fade_out)
		{
			int num = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
			this.mOverlayPulse.Clear();
			if (force_fade_out)
			{
				if (num == 0)
				{
					return;
				}
				this.mOverlayPulse.Add(new Component((float)num, 0f, this.mUpdateCount, this.mUpdateCount + 20));
				return;
			}
			else
			{
				if (num == 128)
				{
					this.mOverlayPulse.Add(new Component(128f, 255f, this.mUpdateCount, this.mUpdateCount + 20));
					this.mOverlayPulse.Add(new Component(255f, 128f, this.mUpdateCount + 21, this.mUpdateCount + 41));
					return;
				}
				this.mOverlayPulse.Add(new Component((float)num, 128f, this.mUpdateCount, this.mUpdateCount + 20));
				return;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00047146 File Offset: 0x00045346
		public void SetupElectricOverlayPulse()
		{
			this.SetupElectricOverlayPulse(false);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0004714F File Offset: 0x0004534F
		public static void ResetIdGen()
		{
			Ball.mIdGen = 0;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00047158 File Offset: 0x00045358
		public Ball()
		{
			this.mFrog = null;
			this.mMultOverlayAlpha = 0;
			this.mMultFX = null;
			this.mInTunnel = false;
			this.mCannonFrame = -1;
			this.mId = ++Ball.mIdGen;
			this.mDoBossPulse = false;
			this.mBossBlinkTimer = 0;
			this.mDebugDrawID = false;
			this.mCurve = null;
			this.mUpdateCount = 0;
			this.mHilightPulse = false;
			this.mSuckFromCompacting = false;
			this.mX = 0f;
			this.mY = 0f;
			this.mColorType = 0;
			this.mDisplayType = 0;
			this.mRadius = (float)Common.GetDefaultBallRadius();
			this.mSuckBack = true;
			this.mBullet = null;
			this.mCel = 0;
			this.mShouldRemove = false;
			this.mLastFrame = 0;
			this.mMultBallCel = 0;
			this.mMultBallCel2 = Common._M(7);
			this.mIsCannon = false;
			this.mSpeedy = false;
			this.mElectricOverlayCel = 0;
			this.mList = null;
			this.mCollidesWithNext = false;
			this.mSuckCount = 0;
			this.mBackwardsCount = 0;
			this.mBackwardsSpeed = 0f;
			this.mComboCount = 0;
			this.mComboScore = 0;
			this.mRotation = 0f;
			this.mRotationInc = 0f;
			this.mNeedCheckCollision = false;
			this.mSuckPending = false;
			this.mShrinkClear = false;
			this.mIconCel = -1;
			this.mIconAppearScale = 1f;
			this.mIconScaleRate = 0f;
			this.mStartFrame = 0;
			this.mWayPoint = 0f;
			this.mPowerType = PowerType.PowerType_Max;
			this.mDestPowerType = PowerType.PowerType_Max;
			this.mPowerCount = 0;
			this.mPowerFade = 0;
			this.mGapBonus = 0;
			this.mNumGaps = 0;
			this.mParticles = null;
			this.mDrawScale = 1f;
			this.mExplodeFrame = 0;
			this.mPowerGracePeriod = 0;
			this.mLastPowerType = PowerType.PowerType_Max;
			this.mDoLaserAnim = false;
			this.mElectricExplodeOverlay.mLoopCount = (this.mElectricExplodeOverlay.mLayer1Cel = (this.mElectricExplodeOverlay.mLayer2Cel = 0));
			this.mExplodingFromLightning = false;
			this.mExploding = (this.mExplodingInTunnel = false);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000473A4 File Offset: 0x000455A4
		public virtual void CopyFrom(Ball other)
		{
			this.mInTunnel = other.mInTunnel;
			this.mMultOverlayAlpha = other.mMultOverlayAlpha;
			this.mMultFX = other.mMultFX;
			this.mColorType = other.mColorType;
			this.mDisplayType = other.mDisplayType;
			this.mWayPoint = other.mWayPoint;
			this.mLastWayPoint = other.mLastWayPoint;
			this.mRotation = other.mRotation;
			this.mDestRotation = other.mDestRotation;
			this.mRotationInc = other.mRotationInc;
			this.mX = other.mX;
			this.mY = other.mY;
			this.mLastX = other.mLastX;
			this.mLastY = other.mLastY;
			this.mDrawScale = other.mDrawScale;
			this.mRadius = other.mRadius;
			this.mPulseState = other.mPulseState;
			this.mPulseTimer = other.mPulseTimer;
			this.mOverlayPulse.Clear();
			this.mOverlayPulse.AddRange(other.mOverlayPulse.ToArray());
			this.mElectricOverlay.Clear();
			this.mElectricOverlay.AddRange(other.mElectricOverlay.ToArray());
			this.mElectricExplodeOverlay = other.mElectricExplodeOverlay;
			this.mElectricOverlayCel = other.mElectricOverlayCel;
			this.mList = other.mList;
			this.mCurve = other.mCurve;
			this.mCollidesWithNext = other.mCollidesWithNext;
			this.mSuckPending = other.mSuckPending;
			this.mShrinkClear = other.mShrinkClear;
			this.mSuckFromCompacting = other.mSuckFromCompacting;
			this.mExplodingInTunnel = other.mExplodingInTunnel;
			this.mExploding = other.mExploding;
			this.mExplodingFromLightning = other.mExplodingFromLightning;
			this.mExplodeFrame = other.mExplodeFrame;
			this.mShouldRemove = other.mShouldRemove;
			this.mSpeedy = other.mSpeedy;
			this.mSuckBack = other.mSuckBack;
			this.mPowerGracePeriod = other.mPowerGracePeriod;
			this.mLastPowerType = other.mLastPowerType;
			this.mCannonFrame = other.mCannonFrame;
			this.mIsCannon = other.mIsCannon;
			this.mDoLaserAnim = other.mDoLaserAnim;
			this.mUpdateCount = other.mUpdateCount;
			this.mCel = other.mCel;
			this.mBullet = other.mBullet;
			this.mSuckCount = other.mSuckCount;
			this.mBackwardsCount = other.mBackwardsCount;
			this.mComboCount = other.mComboCount;
			this.mBackwardsSpeed = other.mBackwardsSpeed;
			this.mPowerCount = other.mPowerCount;
			this.mComboScore = other.mComboScore;
			this.mStartFrame = other.mStartFrame;
			this.mPowerFade = other.mPowerFade;
			this.mGapBonus = other.mGapBonus;
			this.mNumGaps = other.mNumGaps;
			this.mIconAppearScale = other.mIconAppearScale;
			this.mIconScaleRate = other.mIconScaleRate;
			this.mIconCel = other.mIconCel;
			this.mMultBallCel = other.mMultBallCel;
			this.mMultBallCel2 = other.mMultBallCel2;
			this.mParticles = other.mParticles;
			this.mPowerType = other.mPowerType;
			this.mDestPowerType = other.mDestPowerType;
			this.mHilightPulse = other.mHilightPulse;
			this.mDebugDrawID = other.mDebugDrawID;
			this.mDoBossPulse = other.mDoBossPulse;
			this.mBossBlinkTimer = other.mBossBlinkTimer;
			this.mLastFrame = other.mLastFrame;
			this.mFrog = other.mFrog;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00047700 File Offset: 0x00045900
		public virtual void Dispose()
		{
			if (this.mCurve != null && this.mCurve.mBoard != null)
			{
				this.mCurve.mBoard.BallDeleted(this);
			}
			Board board = GameApp.gApp.GetBoard();
			if (board != null && this == board.GetGuideBall())
			{
				board.GuideBallInvalidated();
			}
			this.mParticles = null;
			this.CleanUpMultiplierOverlays();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0004775D File Offset: 0x0004595D
		public void SetPos(float x, float y)
		{
			this.mX = x;
			this.mY = y;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0004776D File Offset: 0x0004596D
		public void SetWayPoint(float thePoint, bool in_tunnel)
		{
			this.mWayPoint = thePoint;
			this.mInTunnel = in_tunnel;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00047780 File Offset: 0x00045980
		public int GetFrame(Image img, int div)
		{
			int num = (img.mNumCols == 1) ? img.mNumRows : (img.mNumRows * img.mNumCols);
			int num2 = (int)this.mWayPoint;
			int num3 = (num2 / div + this.mStartFrame) % num;
			if (num3 < 0)
			{
				num3 = -num3;
			}
			else if (num3 >= num)
			{
				num3 = num - 1;
			}
			return num3;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000477D5 File Offset: 0x000459D5
		public int GetFrame(Image img)
		{
			return this.GetFrame(img, 1);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000477DF File Offset: 0x000459DF
		public void CleanUpMultiplierOverlays()
		{
			GameApp.gApp.ReleaseGenericCachedEffect(this.mMultFX);
			this.mMultFX = null;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000477F8 File Offset: 0x000459F8
		public void SetRotation(float theRot, bool immediate)
		{
			if (immediate)
			{
				this.mRotation = theRot;
				return;
			}
			if (MathUtils._eq(theRot, this.mRotation, 0.001f))
			{
				return;
			}
			while (Math.Abs(theRot - this.mRotation) > 3.14159f)
			{
				if (theRot > this.mRotation)
				{
					theRot -= 6.28318f;
				}
				else
				{
					theRot += 6.28318f;
				}
			}
			this.mDestRotation = theRot;
			this.mRotationInc = 0.10471967f;
			if (theRot < this.mRotation)
			{
				this.mRotationInc = -this.mRotationInc;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0004787D File Offset: 0x00045A7D
		public void SetRotation(float theRot)
		{
			this.SetRotation(theRot, false);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00047888 File Offset: 0x00045A88
		public virtual void DrawBase(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDrawBase(g, xoff, yoff);
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000478FC File Offset: 0x00045AFC
		public virtual void DrawAdditive(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDrawAdditive(g, xoff, yoff);
				if (this.mPowerFade != 0 && (this.mCurve == null || this.mCurve.mPostZumaFlashTimer <= 0))
				{
					int num = (this.mPowerType == PowerType.PowerType_GauntletMultBall) ? ((int)Common._M(2f)) : 4;
					if ((this.mPowerFade >> num & 1) != 0)
					{
						g.SetDrawMode(1);
						this.DoDrawBase(g, xoff, yoff);
						this.DoDrawAdditive(g, xoff, yoff);
					}
				}
				else if ((this.mDoBossPulse && (float)this.mBossBlinkTimer < Common._M(10f)) || (this.mCurve != null && this.mCurve.mPostZumaFlashTimer > 0))
				{
					g.SetDrawMode(1);
					this.DoDrawBase(g, xoff, yoff);
					this.DoDrawAdditive(g, xoff, yoff);
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00047A24 File Offset: 0x00045C24
		public virtual void Draw(SexyGraphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDraw(g, xoff, yoff);
				if (this.mPowerFade != 0 && (this.mCurve == null || this.mCurve.mPostZumaFlashTimer <= 0))
				{
					int num = (this.mPowerType == PowerType.PowerType_GauntletMultBall) ? ((int)Common._M(2f)) : 4;
					if ((this.mPowerFade >> num & 1) != 0)
					{
						g.SetDrawMode(1);
						this.DoDraw(g, xoff, yoff);
					}
				}
				else if ((this.mDoBossPulse && (float)this.mBossBlinkTimer < Common._M(10f)) || (this.mCurve != null && this.mCurve.mPostZumaFlashTimer > 0))
				{
					g.SetDrawMode(1);
					this.DoDraw(g, xoff, yoff);
					g.SetDrawMode(0);
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
			if (this.mDebugDrawID)
			{
				Font fontByID = Res.GetFontByID(ResID.FONT_MAIN22);
				g.SetFont(fontByID);
				g.SetColor(Color.Black);
				g.FillRect((int)Common._S(this.mX - 12f), (int)Common._S(this.mY - 8f), Common._S(24), Common._S(16));
				g.SetColor(Color.White);
				g.DrawString(string.Format("{0}", this.mId), (int)Common._S(this.mX - 10f), (int)Common._S(this.mY - 12f) + fontByID.GetAscent());
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00047BEE File Offset: 0x00045DEE
		public void Draw(SexyGraphics g)
		{
			this.Draw(g, 0, 0);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00047BF9 File Offset: 0x00045DF9
		public void DrawProximityBombExplosion(SexyGraphics g)
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00047BFC File Offset: 0x00045DFC
		public void DrawShadow(SexyGraphics g)
		{
			if (!GlobalMembers.gSexyApp.Is3DAccelerated())
			{
				return;
			}
			if (this.mExploding)
			{
				return;
			}
			Transform transform = new Transform();
			float num = Common._S(this.mX - 3f);
			float num2 = Common._S(this.mY + 5f);
			if (this.mDrawScale > 1f)
			{
				num -= Common._S(Common._M(9f)) * (this.mDrawScale - 1f);
				num2 += Common._S(Common._M(15f)) * (this.mDrawScale - 1f);
				transform.Scale(this.mDrawScale, this.mDrawScale);
			}
			g.DrawImageTransformF(Res.GetImageByID(ResID.IMAGE_BALL_SHADOW), transform, num, num2);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00047CBC File Offset: 0x00045EBC
		public void DrawTopLayer(SexyGraphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if ((this.mPowerType != PowerType.PowerType_Max || Common.size<Component>(this.mElectricOverlay) > 0 || Common.size<Component>(this.mOverlayPulse) > 0) && !this.GetIsExploding())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BALL_GLOW);
				Color color = Ball.gOverlayColors[this.mColorType];
				color.mAlpha = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
				g.SetColor(color);
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				int num = (int)Common._S(this.mX - this.mRadius);
				int num2 = (int)Common._S(this.mY - this.mRadius);
				num -= (imageByID.mWidth - Common._S(Common.GetDefaultBallSize())) / 2 - 1;
				num2 -= (imageByID.mHeight - Common._S(Common.GetDefaultBallSize())) / 2 - 1;
				if (!GameApp.gApp.mColorblind)
				{
					if (graphics3D != null)
					{
						g.DrawImageF(imageByID, (float)num, (float)num2);
					}
					else
					{
						g.DrawImage(imageByID, num, num2);
					}
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mMultFX != null && (!GameApp.gApp.mColorblind || (this.mColorType != 3 && this.mColorType != 4)))
			{
				this.mMultFX.DrawLayer(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawLayerNormal(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawLayerAdditive(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawPhisycalLayer(g, this.mMultFX.GetLayer("Top"));
			}
			if (this.mExplodingInTunnel)
			{
				this.DrawLightningExplosion(g);
			}
			if (Common.size<Component>(this.mElectricOverlay) > 0)
			{
				int num3 = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
				g.SetDrawMode(1);
				if (num3 != 255)
				{
					g.SetColor(255, 255, 255, num3);
					g.SetColorizeImages(true);
				}
				g.SetDrawMode(0);
				if (num3 != 255)
				{
					g.SetColorizeImages(false);
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00047EFC File Offset: 0x000460FC
		public void DrawBottomLayer(SexyGraphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if (!this.mCurve.mWayPointMgr.InTunnel(this, true))
			{
				this.mCurve.mWayPointMgr.InTunnel(this, false);
			}
			if (this.mMultFX != null && graphics3D != null)
			{
				this.mMultFX.DrawLayer(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawLayerNormal(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawLayerAdditive(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawPhisycalLayer(g, this.mMultFX.GetLayer("Bottom"));
			}
			if (this.mDoLaserAnim)
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_LAZER_BURN);
				Rect celRect = imageByID.GetCelRect(Ball.mLaserAnimCel);
				float num = CommonMath.AngleBetweenPoints((float)this.mFrog.GetCenterX(), (float)this.mFrog.GetCenterY(), this.mX, this.mY) + 1.570795f;
				g.DrawImageRotated(imageByID, (int)Common._S(this.mX + (float)Common._M(-38)), (int)Common._S(this.mY + (float)Common._M1(-52)), (double)num, Common._S(Common._M2(38)), Common._S(Common._M3(52)), celRect);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00048050 File Offset: 0x00046250
		public void DrawAboveBalls(SexyGraphics g)
		{
			if (this.mIconCel != -1 && MathUtils._geq(this.mIconAppearScale, 1f) && g.Is3D())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_POWERUPS_PULSES);
				float num = (this.GetPowerOrDestType() == PowerType.PowerType_MoveBackwards) ? 0f : -1.570795f;
				int num2 = (int)Common._S(this.mX);
				int num3 = (int)Common._S(this.mY);
				g.SetDrawMode(1);
				g.SetColorizeImages(true);
				g.SetColor(new Color(Common.gBallColors[this.mColorType]));
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.Scale(this.mIconAppearScale, this.mIconAppearScale);
				this.mGlobalTransform.RotateRad(this.mRotation + num);
				g.DrawImageTransform(imageByID, this.mGlobalTransform, imageByID.GetCelRect(this.mIconCel), (float)num2, (float)num3);
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mExploding && !this.mShrinkClear && !this.mExplodingInTunnel)
			{
				this.DrawExplosion(g);
			}
			if (!this.mExplodingInTunnel)
			{
				this.DrawLightningExplosion(g);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00048178 File Offset: 0x00046378
		public void DrawLightningExplosion(SexyGraphics g)
		{
			if (Common.size<Component>(this.mElectricExplodeOverlay.mLayer1Alpha) > 0)
			{
				g.SetDrawMode(1);
				int num = (int)Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer2Alpha, 255f, this.mUpdateCount);
				if (num != 255)
				{
					g.SetColor(255, 255, 255, num);
					g.SetColorizeImages(true);
				}
				g.SetColorizeImages(false);
				num = (int)Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer1Alpha, 255f, this.mUpdateCount);
				Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer1Scale, 1f, this.mUpdateCount);
				if (num != 255)
				{
					g.SetColor(255, 255, 255, num);
					g.SetColorizeImages(true);
				}
				g.SetDrawMode(0);
				if (num != 255)
				{
					g.SetColorizeImages(false);
				}
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00048260 File Offset: 0x00046460
		public void DoElectricOverlay(bool val)
		{
			if (!val && Enumerable.Count<Component>(this.mElectricOverlay) > 0)
			{
				int num = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
				this.mElectricOverlay.Clear();
				int num2 = (int)(0.039215688f * (float)num);
				this.mElectricOverlay.Add(new Component((float)num, 0f, this.mUpdateCount, this.mUpdateCount + ((num2 < 1) ? 1 : num2)));
				this.SetupDefaultOverlayPulse();
				return;
			}
			if (val && Enumerable.Count<Component>(this.mElectricOverlay) == 0)
			{
				this.mElectricOverlay.Add(new Component(0f, 255f, this.mUpdateCount, this.mUpdateCount + 10));
				return;
			}
			if (!val)
			{
				if (!val)
				{
					this.SetupDefaultOverlayPulse();
				}
				return;
			}
			int num3 = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
			if (num3 == 255)
			{
				return;
			}
			this.mElectricOverlay.Clear();
			int num4 = (int)(0.039215688f * (float)num3);
			this.mElectricOverlay.Add(new Component((float)num3, 255f, this.mUpdateCount, this.mUpdateCount + ((num4 < 1) ? 1 : num4)));
			this.SetupElectricOverlayPulse();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00048390 File Offset: 0x00046590
		public bool CollidesWithPhysically(Ball theBall, int thePad)
		{
			float num = theBall.GetX() - this.GetX();
			float num2 = theBall.GetY() - this.GetY();
			float num3 = (float)theBall.GetRadius() + (float)(thePad * 2) + (float)this.GetRadius();
			return num * num + num2 * num2 < num3 * num3;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000483DA File Offset: 0x000465DA
		public bool CollidesWithPhysically(Ball theBall)
		{
			return this.CollidesWithPhysically(theBall, 0);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000483E4 File Offset: 0x000465E4
		public bool CollidesWith(Ball theBall, int thePad)
		{
			return Math.Abs((float)((int)this.mWayPoint) - (float)((int)theBall.mWayPoint)) < (float)((Common.GetDefaultBallRadius() + thePad) * 2);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00048408 File Offset: 0x00046608
		public bool CollidesWith(Ball theBall)
		{
			return this.CollidesWith(theBall, 0);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00048414 File Offset: 0x00046614
		public bool CollidesWithPhysically(int pointx, int pointy, int radius)
		{
			float num = (float)pointx - this.GetX();
			float num2 = (float)pointy - this.GetY();
			float num3 = (float)radius + (float)this.GetRadius();
			return num * num + num2 * num2 < num3 * num3;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0004844C File Offset: 0x0004664C
		public bool Intersects(SexyVector3 p1, SexyVector3 v1, ref float t)
		{
			SexyVector3 sexyVector;
			sexyVector = new SexyVector3(p1.x - this.mX, p1.y - this.mY, 0f);
			float num = this.mRadius - (float)Common._M(1);
			float num2 = v1.Dot(v1);
			float num3 = 2f * sexyVector.Dot(v1);
			float num4 = sexyVector.Dot(sexyVector) - num * 2f * (num * 2f);
			float num5 = num3 * num3 - 4f * num2 * num4;
			if (num5 < 0f)
			{
				return false;
			}
			num5 = (float)Math.Sqrt((double)num5);
			t = (-num3 - num5) / (2f * num2);
			return true;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000484F8 File Offset: 0x000466F8
		public void SetBullet(Bullet theBullet)
		{
			this.mBullet = theBullet;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00048504 File Offset: 0x00046704
		public void SetCollidesWithPrev(bool collidesWithPrev)
		{
			Ball prevBall = this.GetPrevBall();
			if (prevBall != null)
			{
				prevBall.SetCollidesWithNext(collidesWithPrev);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00048524 File Offset: 0x00046724
		public bool GetCollidesWithPrev()
		{
			Ball prevBall = this.GetPrevBall();
			return prevBall != null && prevBall.GetCollidesWithNext();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00048544 File Offset: 0x00046744
		public void UpdateCollisionInfo(int thePad)
		{
			Ball prevBall = this.GetPrevBall();
			Ball nextBall = this.GetNextBall();
			if (prevBall != null)
			{
				prevBall.SetCollidesWithNext(prevBall.CollidesWith(this, thePad));
			}
			if (nextBall != null)
			{
				this.SetCollidesWithNext(nextBall.CollidesWith(this, thePad));
				return;
			}
			this.SetCollidesWithNext(false);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00048589 File Offset: 0x00046789
		public void UpdateCollisionInfo()
		{
			this.UpdateCollisionInfo(0);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00048594 File Offset: 0x00046794
		public void SetPowerType(PowerType theType, bool delay)
		{
			this.mDoBossPulse = false;
			if (theType == this.mPowerType)
			{
				return;
			}
			this.mPulseState = 0;
			this.mPulseTimer = 0;
			this.mIconCel = -1;
			if (theType != PowerType.PowerType_Max)
			{
				this.mPowerGracePeriod = 0;
				this.mLastPowerType = PowerType.PowerType_Max;
			}
			if (delay)
			{
				this.mDestPowerType = theType;
				if (theType == PowerType.PowerType_Max && this.mPowerType == PowerType.PowerType_GauntletMultBall)
				{
					this.mPowerFade = 300;
				}
				else
				{
					this.mPowerFade = 100;
				}
				switch (theType)
				{
				case PowerType.PowerType_ProximityBomb:
					this.mIconCel = 3;
					break;
				case PowerType.PowerType_SlowDown:
					this.mIconCel = 2;
					break;
				case PowerType.PowerType_Accuracy:
					this.mIconCel = 0;
					break;
				case PowerType.PowerType_MoveBackwards:
					this.mIconCel = 4;
					break;
				case PowerType.PowerType_Cannon:
					this.mIconCel = 5;
					break;
				case PowerType.PowerType_ColorNuke:
					this.mIconCel = 1;
					break;
				case PowerType.PowerType_Laser:
					this.mIconCel = 6;
					break;
				}
				int soundByID = Res.GetSoundByID(ResID.SOUND_MULT_APPEAR);
				int soundByID2 = Res.GetSoundByID(ResID.SOUND_POWERUP_APPEARS);
				int soundByID3 = Res.GetSoundByID(ResID.SOUND_MULT_DISAPPEAR);
				int soundByID4 = Res.GetSoundByID(ResID.SOUND_POWERUP_DISAPPEARS);
				if (theType != PowerType.PowerType_Max)
				{
					if (theType == PowerType.PowerType_GauntletMultBall)
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID);
					}
					else
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID2);
					}
				}
				else if (this.GetPowerOrDestType() != PowerType.PowerType_Max)
				{
					if (this.GetPowerOrDestType() == PowerType.PowerType_GauntletMultBall)
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID3);
					}
					else
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID4);
					}
				}
				this.mIconAppearScale = 5f;
				this.mIconScaleRate = (this.mIconAppearScale - 1f) / (float)this.mPowerFade;
			}
			else
			{
				this.mDestPowerType = PowerType.PowerType_Max;
				this.mPowerType = theType;
			}
			if (theType != PowerType.PowerType_Max && this.mCurve != null)
			{
				this.mCurve.SetColorHasPowerup(this.mColorType, true);
			}
			this.SetupDefaultOverlayPulse();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00048768 File Offset: 0x00046968
		public void SetPowerType(PowerType theType)
		{
			this.SetPowerType(theType, true);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00048772 File Offset: 0x00046972
		public PowerType GetPowerOrDestType(bool include_grace_period)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				return this.mPowerType;
			}
			if (this.mPowerGracePeriod > 0 && this.mLastPowerType != PowerType.PowerType_Max)
			{
				return this.mLastPowerType;
			}
			return this.mDestPowerType;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000487A5 File Offset: 0x000469A5
		public PowerType GetPowerOrDestType()
		{
			return this.GetPowerOrDestType(true);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000487AE File Offset: 0x000469AE
		public void RemoveFromList()
		{
			if (this.mList != null)
			{
				this.mList.Remove(this);
				this.mList = null;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000487CC File Offset: 0x000469CC
		public int InsertInList(List<Ball> theList, int theInsertItr, CurveMgr cm)
		{
			this.mList = theList;
			theList.Insert(theInsertItr, this);
			this.mCurve = cm;
			return theInsertItr;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000487E5 File Offset: 0x000469E5
		public SexyVector3 GetSpeed()
		{
			return new SexyVector3(this.mX - this.mLastX, this.mY - this.mLastY, 0f);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0004880B File Offset: 0x00046A0B
		public float GetWayPointProgress()
		{
			return this.mWayPoint - this.mLastWayPoint;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0004881C File Offset: 0x00046A1C
		public Ball GetPrevBall(bool mustCollide)
		{
			if (this.mList == null)
			{
				return null;
			}
			int listItr = this.GetListItr();
			if (listItr == 0)
			{
				return null;
			}
			if (!mustCollide)
			{
				return this.mList[listItr - 1];
			}
			Ball ball = this.mList[listItr - 1];
			if (ball.GetCollidesWithNext())
			{
				return ball;
			}
			return null;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0004886F File Offset: 0x00046A6F
		public Ball GetPrevBall()
		{
			return this.GetPrevBall(false);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00048878 File Offset: 0x00046A78
		public Ball GetNextBall(bool mustCollide)
		{
			if (this.mList == null)
			{
				return null;
			}
			int num = this.GetListItr();
			num++;
			if (num >= Enumerable.Count<Ball>(this.mList))
			{
				return null;
			}
			if (!mustCollide || this.GetCollidesWithNext())
			{
				return this.mList[num];
			}
			return null;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000488C3 File Offset: 0x00046AC3
		public Ball GetNextBall()
		{
			return this.GetNextBall(false);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000488CC File Offset: 0x00046ACC
		public CurveMgr GetCurve()
		{
			return this.mCurve;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000488D4 File Offset: 0x00046AD4
		public void Explode(bool in_tunnel, bool from_lightning_frog)
		{
			if (this.mExploding)
			{
				return;
			}
			this.mExploding = true;
			this.mExplodingInTunnel = in_tunnel;
			Board board = GameApp.gApp.GetBoard();
			if (!this.mExplodingInTunnel)
			{
				board.AddBallExplosionParticleEffect(this);
			}
			if (this.GetPowerOrDestType() == PowerType.PowerType_ProximityBomb)
			{
				PowerEffect powerEffect = new PowerEffect(this.mX, this.mY);
				powerEffect.AddDefaultEffectType(0, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect);
				board.AddProxBombExplosion(this.GetX(), this.GetY());
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Accuracy)
			{
				PowerEffect powerEffect2 = new PowerEffect(this.mX, this.mY);
				powerEffect2.AddDefaultEffectType(1, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect2);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_MoveBackwards)
			{
				PowerEffect powerEffect3 = new ReversePowerEffect(this.mX, this.mY, this);
				powerEffect3.AddDefaultEffectType(2, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect3);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_SlowDown)
			{
				PowerEffect powerEffect4 = new PowerEffect(this.mX, this.mY);
				powerEffect4.AddDefaultEffectType(3, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect4);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Cannon)
			{
				PowerEffect powerEffect5 = new CannonPowerEffect(this);
				powerEffect5.AddDefaultEffectType(4, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect5);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Laser)
			{
				PowerEffect powerEffect6 = new PowerEffect(this.mX, this.mY);
				powerEffect6.AddDefaultEffectType(5, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect6);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_GauntletMultBall)
			{
				this.CleanUpMultiplierOverlays();
			}
			if (this.GetPowerOrDestType() != PowerType.PowerType_Max)
			{
				this.mCurve.SetColorHasPowerup(this.mColorType, false);
			}
			if (from_lightning_frog)
			{
				this.mExplodingFromLightning = true;
				this.mElectricOverlay.Clear();
				this.mElectricOverlay.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 10));
				this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(0f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
				this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(25f, 255f, this.mUpdateCount + 21, this.mUpdateCount + 41));
				this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(0.5f, 1f, this.mUpdateCount + 21, this.mUpdateCount + 41));
				this.mElectricExplodeOverlay.mLayer2Alpha.Add(new Component(25f, 255f, this.mUpdateCount, this.mUpdateCount + 20));
				this.mElectricExplodeOverlay.mLoopCount = 0;
				return;
			}
			if (Common.size<Component>(this.mElectricOverlay) > 0)
			{
				this.mElectricOverlay.Clear();
				this.mElectricOverlay.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 5));
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00048BEF File Offset: 0x00046DEF
		public void Explode(bool in_tunnel)
		{
			this.Explode(in_tunnel, false);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00048BF9 File Offset: 0x00046DF9
		public void Explode()
		{
			this.Explode(false, false);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00048C04 File Offset: 0x00046E04
		public void Update()
		{
			this.mUpdateCount++;
			this.mLastWayPoint = this.mWayPoint;
			this.mLastX = this.mX;
			this.mLastY = this.mY;
			GameApp gApp = GameApp.gApp;
			if (gApp.GetBoard().GetHallucinateTimer() > 0 && this.mUpdateCount % Common._M(25) == 0)
			{
				this.mDisplayType = MathUtils.SafeRand() % 6;
			}
			if (this.mDoBossPulse && this.mBossBlinkTimer == 0)
			{
				this.mBossBlinkTimer = Common._M(20);
			}
			else if (this.mBossBlinkTimer > 0)
			{
				this.mBossBlinkTimer--;
			}
			if (this.mUpdateCount % Common._M(6) == 0 && (!gApp.mColorblind || (this.mColorType != 3 && this.mColorType != 4)))
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_BLUE);
				this.mMultBallCel = (this.mMultBallCel + 1) % (imageByID.mNumRows * imageByID.mNumCols);
				this.mMultBallCel2 = (this.mMultBallCel2 + 1) % (imageByID.mNumRows * imageByID.mNumCols);
			}
			if (this.mPowerFade > 0)
			{
				this.mIconAppearScale -= this.mIconScaleRate;
				if (this.mIconAppearScale < 1f)
				{
					this.mIconAppearScale = 1f;
				}
				if (this.mPowerType == PowerType.PowerType_GauntletMultBall && this.mDestPowerType == PowerType.PowerType_Max && this.mPowerFade < 51)
				{
					this.mMultOverlayAlpha -= 5;
					if (this.mMultOverlayAlpha < 0)
					{
						this.mMultOverlayAlpha = 0;
					}
				}
				this.mPowerFade--;
				if (this.mPowerFade == 0)
				{
					this.mPowerType = this.mDestPowerType;
					if (this.mPowerType == PowerType.PowerType_GauntletMultBall)
					{
						int num = this.mColorType;
						if (gApp.mColorblind && (this.mColorType == 3 || this.mColorType == 4))
						{
							num = 5;
						}
						this.mMultFX = gApp.mResourceManager.GetPIEffect(Ball.fx_files[num]).Duplicate();
						this.mMultFX.mEmitAfterTimeline = true;
						this.mMultOverlayAlpha = 0;
					}
					else if (this.mPowerType == PowerType.PowerType_Max)
					{
						this.CleanUpMultiplierOverlays();
					}
					this.mIconCel = -1;
					this.mDestPowerType = PowerType.PowerType_Max;
					if (this.mPowerType != PowerType.PowerType_Max && this.mPowerCount <= 0)
					{
						this.mPowerCount = (int)((float)Common._M(2000) * GameApp.gDDS.mHandheldBalance.mFruitPowerupAdditionalDuration);
					}
				}
			}
			if (this.mMultFX != null)
			{
				this.mMultFX.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mMultFX.mDrawTransform.Scale(num2, num2);
				this.mMultFX.mDrawTransform.RotateRad(this.mRotation);
				this.mMultFX.mDrawTransform.Translate(Common._S(this.mX), Common._S(this.mY));
				this.mMultFX.mColor.mAlpha = this.mMultOverlayAlpha;
				this.mMultFX.Update();
			}
			if (this.mMultFX != null && (this.mDestPowerType != PowerType.PowerType_Max || this.mPowerFade >= 51 || this.mPowerFade == 0))
			{
				int num3 = Common._M(3);
				if (this.mInTunnel && this.mMultOverlayAlpha > 0)
				{
					this.mMultOverlayAlpha -= num3;
				}
				else if (!this.mInTunnel && this.mMultOverlayAlpha < 255)
				{
					this.mMultOverlayAlpha += num3;
				}
			}
			this.mMultOverlayAlpha = Math.Min(Math.Max(this.mMultOverlayAlpha, 0), 255);
			if (this.mDoLaserAnim && this.mUpdateCount % Common._M(4) == 0)
			{
				Ball.mLaserAnimCel = (Ball.mLaserAnimCel + 1) % Res.GetImageByID(ResID.IMAGE_LAZER_BURN).mNumCols;
			}
			if (this.mPowerCount > 0 && !this.mExploding && --this.mPowerCount <= 0)
			{
				this.mPowerGracePeriod = Common._M(150);
				this.mLastPowerType = this.GetPowerOrDestType();
				this.mCurve.PowerupExpired(this.GetPowerOrDestType());
				this.mCurve.SetColorHasPowerup(this.mColorType, false);
				this.SetPowerType(PowerType.PowerType_Max);
			}
			if (this.mPowerGracePeriod > 0 && --this.mPowerGracePeriod == 0)
			{
				this.mLastPowerType = PowerType.PowerType_Max;
			}
			if (Common.size<Component>(this.mElectricOverlay) > 0 && Component.UpdateComponentVec(this.mElectricOverlay, this.mUpdateCount) && MathUtils._eq(Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount), 0f, 0.0001f))
			{
				this.mElectricOverlay.Clear();
			}
			if (Common.size<Component>(this.mElectricExplodeOverlay.mLayer1Alpha) > 0)
			{
				int num4 = this.mUpdateCount % Common._M(7);
			}
			if (this.mExploding && Common.size<Component>(this.mElectricExplodeOverlay.mLayer1Alpha) > 0)
			{
				Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer2Alpha, this.mUpdateCount);
				Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer1Scale, this.mUpdateCount);
				if (Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer1Alpha, this.mUpdateCount))
				{
					if (++this.mElectricExplodeOverlay.mLoopCount == 1)
					{
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(255f, 255f, this.mUpdateCount, this.mUpdateCount + 30));
					}
					else if (this.mElectricExplodeOverlay.mLoopCount == 2)
					{
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
						this.mElectricExplodeOverlay.mLayer2Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer2Alpha.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
						this.mElectricExplodeOverlay.mLayer1Scale.Clear();
						this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(1f, 1f, this.mUpdateCount, this.mUpdateCount + 4));
						this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(1f, 0.2f, this.mUpdateCount + 5, this.mUpdateCount + 20));
					}
					else if (this.mElectricExplodeOverlay.mLoopCount == 3)
					{
						this.mElectricExplodeOverlay.mLayer1Scale.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer2Alpha.Clear();
					}
				}
			}
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				if (Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
				{
					if (Common.size<Component>(this.mElectricOverlay) == 0)
					{
						this.SetupDefaultOverlayPulse();
					}
					else
					{
						this.SetupElectricOverlayPulse();
					}
				}
				if (!this.mExploding)
				{
					this.mPulseTimer++;
					if (this.mPulseState == 0 && this.mPulseTimer >= Common._M(30))
					{
						this.mPulseState++;
						this.mPulseTimer = 0;
					}
					else if (this.mPulseState == 1 && this.mPulseTimer >= 128)
					{
						this.mPulseTimer = 0;
						this.mPulseState++;
					}
					else if (this.mPulseState == 2 && this.mPulseTimer >= Common._M(25))
					{
						this.mPulseState = 0;
						this.mPulseTimer = 0;
					}
				}
			}
			else if (Common.size<Component>(this.mElectricOverlay) > 0 && Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
			{
				this.SetupElectricOverlayPulse();
			}
			else if (Common.size<Component>(this.mElectricOverlay) == 0 && Common.size<Component>(this.mOverlayPulse) > 0 && Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
			{
				this.mOverlayPulse.Clear();
			}
			this.UpdateRotation();
			if (this.mPowerType == PowerType.PowerType_MoveBackwards && this.mUpdateCount % Common._M(4) == 0)
			{
				this.mCel = ((this.mCel == 0) ? (Res.GetImageByID(ResID.IMAGE_POWERUP_REVERSE_ANYCOLOR).mNumCols - 1) : (this.mCel - 1));
				return;
			}
			if (this.mPowerType == PowerType.PowerType_Laser && this.mUpdateCount % Common._M(4) == 0)
			{
				this.mCel = (this.mCel + 1) % Res.GetImageByID(ResID.IMAGE_POWERUP_LAZER_ANYCOLOR).mNumRows;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00049484 File Offset: 0x00047684
		public void UpdateExplosion()
		{
			if (!this.mExploding)
			{
				return;
			}
			if (!this.mExplodingFromLightning && this.mUpdateCount % Common._M(2) == 0)
			{
				this.mExplodeFrame++;
			}
			if (this.mExplodeFrame >= 20 || this.mElectricExplodeOverlay.mLoopCount >= 3)
			{
				this.mShouldRemove = true;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000494E0 File Offset: 0x000476E0
		public void SetFrame(int theFrame)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			int mNumRows = imageByID.mNumRows;
			int num = (int)this.mWayPoint + theFrame;
			num %= mNumRows;
			this.mStartFrame = mNumRows - num;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00049557 File Offset: 0x00047757
		public void ForceFrame(int theFrame)
		{
			this.mStartFrame = theFrame;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00049560 File Offset: 0x00047760
		public void IncFrame(int theInc)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			int mNumRows = imageByID.mNumRows;
			this.mStartFrame += theInc;
			this.mStartFrame %= mNumRows;
			if (this.mStartFrame < 0)
			{
				this.mStartFrame = mNumRows + this.mStartFrame;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000495F4 File Offset: 0x000477F4
		public void RandomizeFrame()
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			this.mStartFrame = MathUtils.SafeRand() % imageByID.mNumRows;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00049660 File Offset: 0x00047860
		public static void DeleteBallGlobals()
		{
			for (int i = 0; i < 8; i++)
			{
				Ball.gBlendedBalls[i] = null;
				if (i < 6)
				{
					Ball.gBlendedBombLights[i] = null;
				}
				for (int j = 0; j <= 14; j++)
				{
					Ball.gBlendedPowerups[j, i] = null;
				}
			}
			for (int j = 0; j < 14; j++)
			{
				Ball.gBlendedPowerupLights[j] = null;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000496C0 File Offset: 0x000478C0
		public virtual void SyncState(DataSync sync)
		{
			sync.RegisterPointer(this);
			sync.SyncLong(ref this.mId);
			sync.SyncLong(ref this.mPowerGracePeriod);
			int num = (int)this.mLastPowerType;
			sync.SyncLong(ref num);
			this.mLastPowerType = (PowerType)num;
			sync.SyncLong(ref this.mColorType);
			sync.SyncFloat(ref this.mWayPoint);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mDestRotation);
			sync.SyncFloat(ref this.mRotationInc);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncBoolean(ref this.mInTunnel);
			sync.SyncLong(ref this.mMultOverlayAlpha);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteBoolean(this.mMultFX != null);
				if (this.mMultFX != null)
				{
					Common.SerializePIEffect(this.mMultFX, sync);
				}
			}
			else
			{
				this.mMultFX = null;
				if (buffer.ReadBoolean())
				{
					this.mMultFX = new PIEffect();
					Common.DeserializePIEffect(this.mMultFX, sync);
				}
			}
			sync.SyncBoolean(ref this.mDoBossPulse);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncLong(ref this.mPulseState);
			sync.SyncLong(ref this.mPulseTimer);
			sync.SyncLong(ref this.mCannonFrame);
			sync.SyncBoolean(ref this.mCollidesWithNext);
			sync.SyncBoolean(ref this.mNeedCheckCollision);
			sync.SyncBoolean(ref this.mSuckPending);
			sync.SyncBoolean(ref this.mShrinkClear);
			sync.SyncBoolean(ref this.mSuckFromCompacting);
			sync.SyncBoolean(ref this.mExplodingInTunnel);
			sync.SyncBoolean(ref this.mExploding);
			sync.SyncLong(ref this.mExplodeFrame);
			sync.SyncBoolean(ref this.mShouldRemove);
			sync.SyncBoolean(ref this.mIsCannon);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mSuckCount);
			sync.SyncBoolean(ref this.mSuckBack);
			sync.SyncLong(ref this.mBackwardsCount);
			sync.SyncFloat(ref this.mBackwardsSpeed);
			sync.SyncLong(ref this.mComboCount);
			sync.SyncLong(ref this.mComboScore);
			sync.SyncLong(ref this.mStartFrame);
			sync.SyncLong(ref this.mPowerCount);
			sync.SyncLong(ref this.mPowerFade);
			sync.SyncBoolean(ref this.mSpeedy);
			sync.SyncLong(ref this.mGapBonus);
			sync.SyncLong(ref this.mNumGaps);
			sync.SyncLong(ref this.mElectricOverlayCel);
			sync.SyncBoolean(ref this.mExplodingFromLightning);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLoopCount);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLayer2Cel);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLayer1Cel);
			this.SyncListComponents(sync, this.mOverlayPulse, true);
			this.SyncListComponents(sync, this.mElectricOverlay, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer1Alpha, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer2Alpha, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer1Scale, true);
			num = (int)this.mPowerType;
			sync.SyncLong(ref num);
			this.mPowerType = (PowerType)num;
			num = (int)this.mDestPowerType;
			sync.SyncLong(ref num);
			this.mDestPowerType = (PowerType)num;
			sync.SyncPointer(this);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000499FC File Offset: 0x00047BFC
		private void SyncListComponents(DataSync sync, List<Component> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Component component = new Component();
					component.SyncState(sync);
					theList.Add(component);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Component component2 in theList)
			{
				component2.SyncState(sync);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00049A9C File Offset: 0x00047C9C
		public void SetColorType(int theType)
		{
			this.mColorType = theType;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00049AA5 File Offset: 0x00047CA5
		public void SetCollidesWithNext(bool collidesWithNext)
		{
			this.mCollidesWithNext = collidesWithNext;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00049AAE File Offset: 0x00047CAE
		public void DoLaserAnim(bool d, Gun g)
		{
			this.mDoLaserAnim = d;
			this.mFrog = g;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00049ABE File Offset: 0x00047CBE
		public void DoLaserAnim(bool d)
		{
			this.DoLaserAnim(d, null);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00049AC8 File Offset: 0x00047CC8
		public void SetShrinkClear(bool shrink)
		{
			this.mShrinkClear = shrink;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00049AD1 File Offset: 0x00047CD1
		public void SetSuckCount(int theCount, bool suck_back)
		{
			this.mSuckCount = theCount;
			this.mSuckBack = suck_back;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00049AE1 File Offset: 0x00047CE1
		public void SetSuckCount(int theCount)
		{
			this.SetSuckCount(theCount, true);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00049AEB File Offset: 0x00047CEB
		public void SetComboCount(int theCount, int theScore)
		{
			this.mComboCount = theCount;
			this.mComboScore = theScore;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00049AFB File Offset: 0x00047CFB
		public void SetBackwardsCount(int theCount)
		{
			this.mBackwardsCount = theCount;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00049B04 File Offset: 0x00047D04
		public void SetBackwardsSpeed(float theSpeed)
		{
			this.mBackwardsSpeed = theSpeed;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00049B0D File Offset: 0x00047D0D
		public void SetNeedCheckCollision(bool needCheck)
		{
			this.mNeedCheckCollision = needCheck;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00049B16 File Offset: 0x00047D16
		public void SetSuckPending(bool pending, bool compact)
		{
			this.mSuckPending = pending;
			this.mSuckFromCompacting = compact;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00049B26 File Offset: 0x00047D26
		public void SetSuckPending(bool pending)
		{
			this.SetSuckPending(pending, false);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00049B30 File Offset: 0x00047D30
		public void SetGapBonus(int theBonus, int theNumGaps)
		{
			this.mGapBonus = (ushort)theBonus;
			this.mNumGaps = (ushort)theNumGaps;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00049B42 File Offset: 0x00047D42
		public void SetRadius(float r)
		{
			this.mRadius = r;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00049B4B File Offset: 0x00047D4B
		public void SetIsCannon(bool isCannon)
		{
			this.mIsCannon = isCannon;
			this.mCannonFrame = 0;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00049B5B File Offset: 0x00047D5B
		public void SetSpeedy(bool speedy)
		{
			this.mSpeedy = speedy;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00049B64 File Offset: 0x00047D64
		public void SetPowerCount(int c)
		{
			this.mPowerCount = c;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00049B6D File Offset: 0x00047D6D
		public bool GetSuckBack()
		{
			return this.mSuckBack;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00049B75 File Offset: 0x00047D75
		public bool GetSpeedy()
		{
			return this.mSpeedy;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00049B80 File Offset: 0x00047D80
		public bool Contains(int x, int y)
		{
			x -= (int)this.mX;
			y -= (int)this.mY;
			int num = this.GetRadius() - 3;
			return x * x + y * y < num * num;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00049BBB File Offset: 0x00047DBB
		public bool GetShouldRemove()
		{
			return this.mShouldRemove;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00049BC3 File Offset: 0x00047DC3
		public bool GetIsExploding()
		{
			return this.mExploding;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00049BCB File Offset: 0x00047DCB
		public bool GetIsCannon()
		{
			return this.mIsCannon;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00049BD3 File Offset: 0x00047DD3
		public static int GetIdGen()
		{
			return Ball.mIdGen;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00049BDA File Offset: 0x00047DDA
		public float GetX()
		{
			return this.mX;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00049BE2 File Offset: 0x00047DE2
		public float GetY()
		{
			return this.mY;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00049BEA File Offset: 0x00047DEA
		public float GetWayPoint()
		{
			return this.mWayPoint;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00049BF2 File Offset: 0x00047DF2
		public int GetColorType()
		{
			return this.mColorType;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00049BFA File Offset: 0x00047DFA
		public float GetRotation()
		{
			return this.mRotation;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00049C02 File Offset: 0x00047E02
		public float GetDestRotation()
		{
			return this.mDestRotation;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00049C0A File Offset: 0x00047E0A
		public Bullet GetBullet()
		{
			return this.mBullet;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00049C12 File Offset: 0x00047E12
		public bool GetCollidesWithNext()
		{
			return this.mCollidesWithNext;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00049C1A File Offset: 0x00047E1A
		public bool GetShrinkClear()
		{
			return this.mShrinkClear;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00049C22 File Offset: 0x00047E22
		public bool HasOverlays()
		{
			return this.mPowerType != PowerType.PowerType_Max || Enumerable.Count<Component>(this.mElectricOverlay) > 0 || Enumerable.Count<Component>(this.mElectricExplodeOverlay.mLayer1Alpha) > 0 || this.mMultFX != null;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00049C5D File Offset: 0x00047E5D
		public bool HasUnderlays()
		{
			return (this.mDoLaserAnim && !this.mExploding) || this.mMultFX != null;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00049C7D File Offset: 0x00047E7D
		public int GetSuckCount()
		{
			return this.mSuckCount;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00049C85 File Offset: 0x00047E85
		public int GetComboCount()
		{
			return this.mComboCount;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00049C8D File Offset: 0x00047E8D
		public int GetComboScore()
		{
			return this.mComboScore;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00049C95 File Offset: 0x00047E95
		public int GetBackwardsCount()
		{
			return this.mBackwardsCount;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00049C9D File Offset: 0x00047E9D
		public float GetBackwardsSpeed()
		{
			return this.mBackwardsSpeed;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00049CA5 File Offset: 0x00047EA5
		public bool GetNeedCheckCollision()
		{
			return this.mNeedCheckCollision;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00049CAD File Offset: 0x00047EAD
		public bool GetSuckPending()
		{
			return this.mSuckPending;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00049CB5 File Offset: 0x00047EB5
		public bool GetSuckFromCompacting()
		{
			return this.mSuckFromCompacting;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00049CBD File Offset: 0x00047EBD
		public PowerType GetPowerType()
		{
			return this.mPowerType;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00049CC5 File Offset: 0x00047EC5
		public PowerType GetDestPowerType()
		{
			return this.mDestPowerType;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00049CCD File Offset: 0x00047ECD
		public int GetListItr()
		{
			if (this.mList == null)
			{
				return -1;
			}
			return this.mList.IndexOf(this);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00049CE5 File Offset: 0x00047EE5
		public int GetPowerCount()
		{
			return this.mPowerCount;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00049CED File Offset: 0x00047EED
		public int GetGapBonus()
		{
			return (int)this.mGapBonus;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00049CF5 File Offset: 0x00047EF5
		public int GetNumGaps()
		{
			return (int)this.mNumGaps;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00049CFD File Offset: 0x00047EFD
		public int GetStartFrame()
		{
			return this.mStartFrame;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00049D05 File Offset: 0x00047F05
		public int GetId()
		{
			return this.mId;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00049D0D File Offset: 0x00047F0D
		public SexyVector2 GetPos()
		{
			return new SexyVector2(this.mX, this.mY);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00049D20 File Offset: 0x00047F20
		public int GetRadius()
		{
			return (int)this.mRadius;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00049D29 File Offset: 0x00047F29
		public bool GetInTunnel()
		{
			return this.mInTunnel;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00049D34 File Offset: 0x00047F34
		private static BlendedImage CreateBlendedPowerup(int thePowerupType, int theType, Image theImage, int cel)
		{
			int num = theType;
			if (GameApp.gApp.mColorblind && theType == 3)
			{
				num = 6;
			}
			else if (GameApp.gApp.mColorblind && theType == 4)
			{
				num = 7;
			}
			if (Ball.gBlendedPowerups[thePowerupType, num] == null)
			{
				Rect celRect = theImage.GetCelRect(cel);
				Ball.gBlendedPowerups[thePowerupType, num] = new BlendedImage((MemoryImage)theImage, celRect, false);
			}
			return Ball.gBlendedPowerups[thePowerupType, num];
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00049DA4 File Offset: 0x00047FA4
		private static BlendedImage CreateBlendedBall(int theType)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + theType;
			int num = theType;
			if (GameApp.gApp.mColorblind && theType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
				num = 6;
			}
			else if (GameApp.gApp.mColorblind && theType == 4)
			{
				num = 7;
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			if (Ball.gBlendedBalls[num] == null)
			{
				MemoryImage memoryImage = (MemoryImage)Res.GetImageByID(id);
				int num2 = memoryImage.mWidth / memoryImage.mNumCols;
				int num3 = memoryImage.mHeight / memoryImage.mNumRows;
				int num4 = memoryImage.mNumRows / 2;
				Rect celRect = memoryImage.GetCelRect(num4);
				Ball.gBlendedBalls[num] = new BlendedImage(memoryImage, celRect, false);
			}
			return Ball.gBlendedBalls[num];
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00049E48 File Offset: 0x00048048
		private static int GetMultAlpha(int cel)
		{
			int num = Common._M(255);
			int num2 = Common._M(5);
			int num3 = num;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_BLUE);
			int num4 = imageByID.mNumRows * imageByID.mNumCols - num2;
			if (cel < num2)
			{
				num3 = num / num2 * cel;
			}
			else if (cel > num4)
			{
				num3 = num - num / num2 * (cel - num4);
			}
			if (num3 > num)
			{
				num3 = num;
			}
			else if (num3 < 0)
			{
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x04000CA8 RID: 3240
		protected static int mIdGen = 0;

		// Token: 0x04000CA9 RID: 3241
		protected static int mLaserAnimCel;

		// Token: 0x04000CAA RID: 3242
		protected Transform mGlobalTransform = new Transform();

		// Token: 0x04000CAB RID: 3243
		public static Color[] gOverlayColors = new Color[]
		{
			new Color(0, 0, 255),
			new Color(255, 255, 0),
			new Color(255, 0, 0),
			new Color(0, 255, 0),
			new Color(255, 0, 255),
			new Color(255, 255, 255)
		};

		// Token: 0x04000CAC RID: 3244
		public static string[] fx_files = new string[]
		{
			"PIEFFECT_NONRESIZE_BPI",
			"PIEFFECT_NONRESIZE_YPI",
			"PIEFFECT_NONRESIZE_RPI",
			"PIEFFECT_NONRESIZE_GPI",
			"PIEFFECT_NONRESIZE_PPI",
			"PIEFFECT_NONRESIZE_WPI"
		};

		// Token: 0x04000CAD RID: 3245
		private static BlendedImage[] gBlendedBalls = new BlendedImage[8];

		// Token: 0x04000CAE RID: 3246
		private static BlendedImage[,] gBlendedPowerups = new BlendedImage[15, 8];

		// Token: 0x04000CAF RID: 3247
		private static BlendedImage[] gBlendedPowerupLights = new BlendedImage[14];

		// Token: 0x04000CB0 RID: 3248
		private static BlendedImage[] gBlendedBombLights = new BlendedImage[6];

		// Token: 0x04000CB1 RID: 3249
		protected bool mInTunnel;

		// Token: 0x04000CB2 RID: 3250
		protected int mMultOverlayAlpha;

		// Token: 0x04000CB3 RID: 3251
		protected PIEffect mMultFX;

		// Token: 0x04000CB4 RID: 3252
		protected int mId;

		// Token: 0x04000CB5 RID: 3253
		protected int mColorType;

		// Token: 0x04000CB6 RID: 3254
		protected int mDisplayType;

		// Token: 0x04000CB7 RID: 3255
		protected float mWayPoint;

		// Token: 0x04000CB8 RID: 3256
		protected float mLastWayPoint;

		// Token: 0x04000CB9 RID: 3257
		protected float mRotation;

		// Token: 0x04000CBA RID: 3258
		protected float mDestRotation;

		// Token: 0x04000CBB RID: 3259
		protected float mRotationInc;

		// Token: 0x04000CBC RID: 3260
		protected float mX;

		// Token: 0x04000CBD RID: 3261
		protected float mY;

		// Token: 0x04000CBE RID: 3262
		protected float mLastX;

		// Token: 0x04000CBF RID: 3263
		protected float mLastY;

		// Token: 0x04000CC0 RID: 3264
		protected float mDrawScale;

		// Token: 0x04000CC1 RID: 3265
		protected float mRadius;

		// Token: 0x04000CC2 RID: 3266
		protected int mPulseState;

		// Token: 0x04000CC3 RID: 3267
		protected int mPulseTimer;

		// Token: 0x04000CC4 RID: 3268
		private List<Component> mOverlayPulse = new List<Component>();

		// Token: 0x04000CC5 RID: 3269
		private List<Component> mElectricOverlay = new List<Component>();

		// Token: 0x04000CC6 RID: 3270
		private ElectricExplodeOverlay mElectricExplodeOverlay = new ElectricExplodeOverlay();

		// Token: 0x04000CC7 RID: 3271
		protected int mElectricOverlayCel;

		// Token: 0x04000CC8 RID: 3272
		protected List<Ball> mList;

		// Token: 0x04000CC9 RID: 3273
		protected CurveMgr mCurve;

		// Token: 0x04000CCA RID: 3274
		protected bool mCollidesWithNext;

		// Token: 0x04000CCB RID: 3275
		protected bool mNeedCheckCollision;

		// Token: 0x04000CCC RID: 3276
		protected bool mSuckPending;

		// Token: 0x04000CCD RID: 3277
		protected bool mShrinkClear;

		// Token: 0x04000CCE RID: 3278
		protected bool mSuckFromCompacting;

		// Token: 0x04000CCF RID: 3279
		protected bool mExplodingInTunnel;

		// Token: 0x04000CD0 RID: 3280
		protected bool mExploding;

		// Token: 0x04000CD1 RID: 3281
		protected bool mExplodingFromLightning;

		// Token: 0x04000CD2 RID: 3282
		protected int mExplodeFrame;

		// Token: 0x04000CD3 RID: 3283
		protected bool mShouldRemove;

		// Token: 0x04000CD4 RID: 3284
		protected bool mSpeedy;

		// Token: 0x04000CD5 RID: 3285
		protected bool mSuckBack;

		// Token: 0x04000CD6 RID: 3286
		protected int mPowerGracePeriod;

		// Token: 0x04000CD7 RID: 3287
		protected PowerType mLastPowerType;

		// Token: 0x04000CD8 RID: 3288
		protected int mCannonFrame;

		// Token: 0x04000CD9 RID: 3289
		protected bool mIsCannon;

		// Token: 0x04000CDA RID: 3290
		protected bool mDoLaserAnim;

		// Token: 0x04000CDB RID: 3291
		protected int mUpdateCount;

		// Token: 0x04000CDC RID: 3292
		protected int mCel;

		// Token: 0x04000CDD RID: 3293
		public Bullet mBullet;

		// Token: 0x04000CDE RID: 3294
		protected int mSuckCount;

		// Token: 0x04000CDF RID: 3295
		protected int mBackwardsCount;

		// Token: 0x04000CE0 RID: 3296
		protected float mBackwardsSpeed;

		// Token: 0x04000CE1 RID: 3297
		protected int mComboCount;

		// Token: 0x04000CE2 RID: 3298
		protected int mComboScore;

		// Token: 0x04000CE3 RID: 3299
		protected int mStartFrame;

		// Token: 0x04000CE4 RID: 3300
		protected int mPowerCount;

		// Token: 0x04000CE5 RID: 3301
		protected int mPowerFade;

		// Token: 0x04000CE6 RID: 3302
		protected ushort mGapBonus;

		// Token: 0x04000CE7 RID: 3303
		protected ushort mNumGaps;

		// Token: 0x04000CE8 RID: 3304
		protected float mIconAppearScale;

		// Token: 0x04000CE9 RID: 3305
		protected float mIconScaleRate;

		// Token: 0x04000CEA RID: 3306
		protected int mIconCel;

		// Token: 0x04000CEB RID: 3307
		protected int mMultBallCel;

		// Token: 0x04000CEC RID: 3308
		protected int mMultBallCel2;

		// Token: 0x04000CED RID: 3309
		protected List<Ball.Particle> mParticles;

		// Token: 0x04000CEE RID: 3310
		protected PowerType mPowerType;

		// Token: 0x04000CEF RID: 3311
		protected PowerType mDestPowerType;

		// Token: 0x04000CF0 RID: 3312
		public bool mHilightPulse;

		// Token: 0x04000CF1 RID: 3313
		public bool mDebugDrawID;

		// Token: 0x04000CF2 RID: 3314
		public bool mDoBossPulse;

		// Token: 0x04000CF3 RID: 3315
		public int mBossBlinkTimer;

		// Token: 0x04000CF4 RID: 3316
		public int mLastFrame;

		// Token: 0x04000CF5 RID: 3317
		public Gun mFrog;

		// Token: 0x020000BD RID: 189
		protected struct Particle
		{
			// Token: 0x040016C9 RID: 5833
			public float x;

			// Token: 0x040016CA RID: 5834
			public float y;

			// Token: 0x040016CB RID: 5835
			public float vx;

			// Token: 0x040016CC RID: 5836
			public float vy;

			// Token: 0x040016CD RID: 5837
			public int mSize;
		}
	}
}
