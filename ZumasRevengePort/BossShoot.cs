using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000085 RID: 133
	public class BossShoot : Boss, IDisposable
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000809C5 File Offset: 0x0007EBC5
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x000809D2 File Offset: 0x0007EBD2
		public int mColorVampChanceToMatch2ndBall
		{
			get
			{
				return this.mDColorVampChanceToMatch2ndBall.value;
			}
			set
			{
				this.mDColorVampChanceToMatch2ndBall.value = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000809E0 File Offset: 0x0007EBE0
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x000809ED File Offset: 0x0007EBED
		public int mFrogStunTime
		{
			get
			{
				return this.mDFrogStunTime.value;
			}
			set
			{
				this.mDFrogStunTime.value = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000809FB File Offset: 0x0007EBFB
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00080A08 File Offset: 0x0007EC08
		public int mFrogPoisonTime
		{
			get
			{
				return this.mDFrogPoisonTime.value;
			}
			set
			{
				this.mDFrogPoisonTime.value = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00080A16 File Offset: 0x0007EC16
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00080A23 File Offset: 0x0007EC23
		public int mFrogHallucinateTime
		{
			get
			{
				return this.mDFrogHallucinateTime.value;
			}
			set
			{
				this.mDFrogHallucinateTime.value = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00080A31 File Offset: 0x0007EC31
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00080A3E File Offset: 0x0007EC3E
		public int mFrogSlowTimer
		{
			get
			{
				return this.mDFrogSlowTimer.value;
			}
			set
			{
				this.mDFrogSlowTimer.value = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00080A4C File Offset: 0x0007EC4C
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00080A59 File Offset: 0x0007EC59
		public int mShotDelay
		{
			get
			{
				return this.mDShotDelay.value;
			}
			set
			{
				this.mDShotDelay.value = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00080A67 File Offset: 0x0007EC67
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00080A74 File Offset: 0x0007EC74
		public float mFlightSpeed
		{
			get
			{
				return this.mDFlightSpeed.value;
			}
			set
			{
				this.mDFlightSpeed.value = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00080A82 File Offset: 0x0007EC82
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00080A8F File Offset: 0x0007EC8F
		public int mFlightMinDist
		{
			get
			{
				return this.mDFlightMinDist.value;
			}
			set
			{
				this.mDFlightMinDist.value = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00080A9D File Offset: 0x0007EC9D
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00080AAA File Offset: 0x0007ECAA
		public int mColorVampHealthInc
		{
			get
			{
				return this.mDColorVampHealthInc.value;
			}
			set
			{
				this.mDColorVampHealthInc.value = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00080AB8 File Offset: 0x0007ECB8
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00080AC5 File Offset: 0x0007ECC5
		public int mMinColorChangeTime
		{
			get
			{
				return this.mDMinColorChangeTime.value;
			}
			set
			{
				this.mDMinColorChangeTime.value = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00080AD3 File Offset: 0x0007ECD3
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00080AE0 File Offset: 0x0007ECE0
		public int mMaxColorChangeTime
		{
			get
			{
				return this.mDMaxColorChangeTime.value;
			}
			set
			{
				this.mDMaxColorChangeTime.value = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00080AEE File Offset: 0x0007ECEE
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00080AFB File Offset: 0x0007ECFB
		public float mHomingCorrectionAmt
		{
			get
			{
				return this.mDHomingCorrectionAmt.value;
			}
			set
			{
				this.mDHomingCorrectionAmt.value = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00080B09 File Offset: 0x0007ED09
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x00080B16 File Offset: 0x0007ED16
		public int mMinHoverTime
		{
			get
			{
				return this.mDMinHoverTime.value;
			}
			set
			{
				this.mDMinHoverTime.value = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00080B24 File Offset: 0x0007ED24
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x00080B31 File Offset: 0x0007ED31
		public int mMaxHoverTime
		{
			get
			{
				return this.mDMaxHoverTime.value;
			}
			set
			{
				this.mDMaxHoverTime.value = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00080B3F File Offset: 0x0007ED3F
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00080B4C File Offset: 0x0007ED4C
		public int mMinFireDelay
		{
			get
			{
				return this.mDMinFireDelay.value;
			}
			set
			{
				this.mDMinFireDelay.value = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00080B5A File Offset: 0x0007ED5A
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00080B67 File Offset: 0x0007ED67
		public int mMaxFireDelay
		{
			get
			{
				return this.mDMaxFireDelay.value;
			}
			set
			{
				this.mDMaxFireDelay.value = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00080B75 File Offset: 0x0007ED75
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00080B82 File Offset: 0x0007ED82
		public float mMinBulletSpeed
		{
			get
			{
				return this.mDMinBulletSpeed.value;
			}
			set
			{
				this.mDMinBulletSpeed.value = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00080B90 File Offset: 0x0007ED90
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00080B9D File Offset: 0x0007ED9D
		public float mMaxBulletSpeed
		{
			get
			{
				return this.mDMaxBulletSpeed.value;
			}
			set
			{
				this.mDMaxBulletSpeed.value = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00080BAB File Offset: 0x0007EDAB
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00080BB8 File Offset: 0x0007EDB8
		public int mMaxBulletsToFire
		{
			get
			{
				return this.mDMaxBulletsToFire.value;
			}
			set
			{
				this.mDMaxBulletsToFire.value = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00080BC6 File Offset: 0x0007EDC6
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x00080BD3 File Offset: 0x0007EDD3
		public int mMaxRetaliationBullets
		{
			get
			{
				return this.mDMaxRetaliationBullets.value;
			}
			set
			{
				this.mDMaxRetaliationBullets.value = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00080BE1 File Offset: 0x0007EDE1
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00080BEE File Offset: 0x0007EDEE
		public int mMinSineShotTime
		{
			get
			{
				return this.mDMinSineShotTime.value;
			}
			set
			{
				this.mDMinSineShotTime.value = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00080BFC File Offset: 0x0007EDFC
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00080C09 File Offset: 0x0007EE09
		public int mMaxSineShotTime
		{
			get
			{
				return this.mDMaxSineShotTime.value;
			}
			set
			{
				this.mDMaxSineShotTime.value = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00080C17 File Offset: 0x0007EE17
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00080C24 File Offset: 0x0007EE24
		public float mMinAmp
		{
			get
			{
				return this.mDMinAmp.value;
			}
			set
			{
				this.mDMinAmp.value = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00080C32 File Offset: 0x0007EE32
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00080C3F File Offset: 0x0007EE3F
		public float mMaxAmp
		{
			get
			{
				return this.mDMaxAmp.value;
			}
			set
			{
				this.mDMaxAmp.value = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00080C4D File Offset: 0x0007EE4D
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00080C5A File Offset: 0x0007EE5A
		public float mMinFreq
		{
			get
			{
				return this.mDMinFreq.value;
			}
			set
			{
				this.mDMinFreq.value = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00080C68 File Offset: 0x0007EE68
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00080C75 File Offset: 0x0007EE75
		public float mMaxFreq
		{
			get
			{
				return this.mDMaxFreq.value;
			}
			set
			{
				this.mDMaxFreq.value = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00080C83 File Offset: 0x0007EE83
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00080C90 File Offset: 0x0007EE90
		public float mMaxYInc
		{
			get
			{
				return this.mDMaxYInc.value;
			}
			set
			{
				this.mDMaxYInc.value = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00080C9E File Offset: 0x0007EE9E
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00080CAB File Offset: 0x0007EEAB
		public float mMinYInc
		{
			get
			{
				return this.mDMinYInc.value;
			}
			set
			{
				this.mDMinYInc.value = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00080CB9 File Offset: 0x0007EEB9
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x00080CC6 File Offset: 0x0007EEC6
		public float mMaxXInc
		{
			get
			{
				return this.mDMaxXInc.value;
			}
			set
			{
				this.mDMaxXInc.value = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00080CD4 File Offset: 0x0007EED4
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00080CE1 File Offset: 0x0007EEE1
		public float mMinXInc
		{
			get
			{
				return this.mDMinXInc.value;
			}
			set
			{
				this.mDMinXInc.value = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00080CEF File Offset: 0x0007EEEF
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00080CFC File Offset: 0x0007EEFC
		public float mDefaultSpeed
		{
			get
			{
				return this.mDDefaultSpeed.value;
			}
			set
			{
				this.mDDefaultSpeed.value = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00080D0A File Offset: 0x0007EF0A
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00080D17 File Offset: 0x0007EF17
		public bool mStrafe
		{
			get
			{
				return this.mDStrafe.value;
			}
			set
			{
				this.mDStrafe.value = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00080D25 File Offset: 0x0007EF25
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00080D32 File Offset: 0x0007EF32
		public bool mEndHoverOnHit
		{
			get
			{
				return this.mDEndHoverOnHit.value;
			}
			set
			{
				this.mDEndHoverOnHit.value = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00080D40 File Offset: 0x0007EF40
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00080D4D File Offset: 0x0007EF4D
		public float mMinRetalSpeed
		{
			get
			{
				return this.mDMinRetalSpeed.value;
			}
			set
			{
				this.mDMinRetalSpeed.value = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00080D5B File Offset: 0x0007EF5B
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00080D68 File Offset: 0x0007EF68
		public float mMaxRetalSpeed
		{
			get
			{
				return this.mDMaxRetalSpeed.value;
			}
			set
			{
				this.mDMaxRetalSpeed.value = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00080D76 File Offset: 0x0007EF76
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00080D83 File Offset: 0x0007EF83
		public int mShotType
		{
			get
			{
				return this.mDShotType.value;
			}
			set
			{
				this.mDShotType.value = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00080D91 File Offset: 0x0007EF91
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00080D9E File Offset: 0x0007EF9E
		public int mTeleportMinTime
		{
			get
			{
				return this.mDTeleportMinTime.value;
			}
			set
			{
				this.mDTeleportMinTime.value = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00080DAC File Offset: 0x0007EFAC
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x00080DB9 File Offset: 0x0007EFB9
		public int mTeleportMaxTime
		{
			get
			{
				return this.mDTeleportMaxTime.value;
			}
			set
			{
				this.mDTeleportMaxTime.value = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00080DC7 File Offset: 0x0007EFC7
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00080DD4 File Offset: 0x0007EFD4
		public float mMovementAccel
		{
			get
			{
				return this.mDMovementAccel.value;
			}
			set
			{
				this.mDMovementAccel.value = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00080DE2 File Offset: 0x0007EFE2
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00080DEF File Offset: 0x0007EFEF
		public int mDefaultMovementUpdateDelay
		{
			get
			{
				return this.mDDefaultMovementUpdateDelay.value;
			}
			set
			{
				this.mDDefaultMovementUpdateDelay.value = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00080DFD File Offset: 0x0007EFFD
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00080E0A File Offset: 0x0007F00A
		public int mMovementMode
		{
			get
			{
				return this.mDMovementMode.value;
			}
			set
			{
				this.mDMovementMode.value = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00080E18 File Offset: 0x0007F018
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00080E25 File Offset: 0x0007F025
		public bool mUseShield
		{
			get
			{
				return this.mDUseShield.value;
			}
			set
			{
				this.mDUseShield.value = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00080E33 File Offset: 0x0007F033
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00080E40 File Offset: 0x0007F040
		public float mShieldRotateSpeed
		{
			get
			{
				return this.mDShieldRotateSpeed.value;
			}
			set
			{
				this.mDShieldRotateSpeed.value = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00080E4E File Offset: 0x0007F04E
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00080E5B File Offset: 0x0007F05B
		public int mShieldQuadRespawnTime
		{
			get
			{
				return this.mDShieldQuadRespawnTime.value;
			}
			set
			{
				this.mDShieldQuadRespawnTime.value = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00080E69 File Offset: 0x0007F069
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00080E76 File Offset: 0x0007F076
		public int mShieldPauseTime
		{
			get
			{
				return this.mDShieldPauseTime.value;
			}
			set
			{
				this.mDShieldPauseTime.value = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00080E84 File Offset: 0x0007F084
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00080E91 File Offset: 0x0007F091
		public int mShieldHP
		{
			get
			{
				return this.mDShieldHP.value;
			}
			set
			{
				this.mDShieldHP.value = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00080E9F File Offset: 0x0007F09F
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00080EAC File Offset: 0x0007F0AC
		public int mBallShieldDamage
		{
			get
			{
				return this.mDBallShieldDamage.value;
			}
			set
			{
				this.mDBallShieldDamage.value = value;
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00080EBC File Offset: 0x0007F0BC
		protected void CalcDestX(int min_dist)
		{
			if (this.mStrafe)
			{
				if (this.mX < (float)this.mEndX)
				{
					this.mDestX = (float)this.mEndX;
				}
				else
				{
					this.mDestX = (float)this.mStartX;
				}
			}
			else
			{
				this.mDestX = (float)this.GetMinXDist(min_dist);
			}
			if (this.mDestX > this.mX)
			{
				this.mSpeed = this.mDefaultSpeed;
				return;
			}
			this.mSpeed = -this.mDefaultSpeed;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00080F34 File Offset: 0x0007F134
		protected void CalcDestX()
		{
			this.CalcDestX(100);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00080F40 File Offset: 0x0007F140
		protected void CalcDestY(int min_dist)
		{
			if (this.mStrafe)
			{
				if (this.mY < (float)this.mEndY)
				{
					this.mDestY = (float)this.mEndY;
				}
				else
				{
					this.mDestY = (float)this.mStartY;
				}
			}
			else
			{
				this.mDestY = (float)this.GetMinYDist(min_dist);
			}
			if (this.mDestY > this.mY)
			{
				this.mSpeed = this.mDefaultSpeed;
				return;
			}
			this.mSpeed = -this.mDefaultSpeed;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00080FB8 File Offset: 0x0007F1B8
		protected void CalcDestY()
		{
			this.CalcDestY(100);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00080FC4 File Offset: 0x0007F1C4
		protected int GetMinXDist(int min_dist)
		{
			int num;
			int num2;
			if (this.mX - (float)min_dist - (float)(this.mWidth / 2) <= (float)this.mStartX)
			{
				num = (int)(this.mX + (float)(this.mWidth / 2) + (float)min_dist);
				num2 = this.mEndX;
			}
			else if (this.mX + (float)min_dist + (float)(this.mWidth / 2) >= (float)this.mEndX)
			{
				num2 = (int)(this.mX - (float)min_dist - (float)(this.mWidth / 2));
				num = this.mStartX;
			}
			else if (Common.Rand() % 100 < 50)
			{
				num = this.mStartX;
				num2 = (int)(this.mX - (float)min_dist - (float)(this.mWidth / 2));
			}
			else
			{
				num = (int)(this.mX + (float)min_dist + (float)(this.mWidth / 2));
				num2 = this.mEndX;
			}
			if (num + this.mWidth / 2 > this.mEndX)
			{
				num = this.mEndX - 10;
			}
			else if (num - this.mWidth / 2 < this.mStartX)
			{
				num = this.mStartX + 10;
			}
			if (num > num2)
			{
				num = num2;
				num2 = num;
			}
			return Common.IntRange(num, num2);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000810D7 File Offset: 0x0007F2D7
		protected int GetMinXDist()
		{
			return this.GetMinXDist(100);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000810E4 File Offset: 0x0007F2E4
		protected int GetMinYDist(int min_dist)
		{
			int num;
			int num2;
			if (this.mY - (float)min_dist - (float)(this.mHeight / 2) <= (float)this.mStartY)
			{
				num = (int)(this.mY + (float)(this.mHeight / 2) + (float)min_dist);
				num2 = this.mEndY;
			}
			else if (this.mY + (float)min_dist + (float)(this.mHeight / 2) >= (float)this.mEndY)
			{
				num2 = (int)(this.mY - (float)min_dist - (float)(this.mHeight / 2));
				num = this.mStartY;
			}
			else if (Common.Rand() % 100 < 50)
			{
				num = this.mStartY;
				num2 = (int)(this.mY - (float)min_dist - (float)(this.mHeight / 2));
			}
			else
			{
				num = (int)(this.mY + (float)min_dist + (float)(this.mHeight / 2));
				num2 = this.mEndX;
			}
			if (num > num2)
			{
				num = num2;
				num2 = num;
			}
			return Common.IntRange(num, num2);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000811BD File Offset: 0x0007F3BD
		protected int GetMinYDist()
		{
			return this.GetMinYDist(100);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000811C8 File Offset: 0x0007F3C8
		protected bool AtDest()
		{
			if (this.mSpeed > 0f)
			{
				return (this.mStartX > 0 && this.mX >= this.mDestX) || (this.mStartY > 0 && this.mY >= this.mDestY);
			}
			return (this.mStartX > 0 && this.mX <= this.mDestX) || (this.mStartY > 0 && this.mY <= this.mDestY);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00081250 File Offset: 0x0007F450
		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			int num = (int)this.mHP;
			base.DoHit(b, from_prox_bomb);
			num = (int)((float)num - this.mHP);
			if (num <= 0)
			{
				return false;
			}
			this.mMaxShotIncCounter += num;
			if (this.CanRetaliate())
			{
				this.mRetalShotIncCounter += num;
			}
			if (this.mMaxShotIncCounter >= this.mIncMaxShotHealthAmt && this.mIncMaxShotHealthAmt > 0)
			{
				this.mMaxShotIncCounter = 0;
				this.mMaxBulletsToFire++;
			}
			if (this.mRetalShotIncCounter >= this.mIncRetalMaxShotHealthAmt && this.mIncRetalMaxShotHealthAmt > 0)
			{
				this.mRetalShotIncCounter = 0;
				this.mMaxRetaliationBullets++;
			}
			if (this.mColorVampire)
			{
				int num2 = this.mColorVampShotType;
				if (!this.mAvoidColor && this.mColorVampChanceToMatch2ndBall > 0 && Common.Rand() % 100 <= this.mColorVampChanceToMatch2ndBall)
				{
					if (this.mLevel.mFrog.GetNextBullet() != null)
					{
						num2 = this.mLevel.mFrog.GetNextBullet().GetColorType();
						goto IL_10E;
					}
				}
				while (num2 == this.mColorVampShotType)
				{
					num2 = Common.Rand() % 4;
				}
				IL_10E:
				this.mColorVampShotType = num2;
				this.mColorChangeTimer = Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
			}
			if (this.mEndHoverOnHit)
			{
				this.EndHoverOnHit();
			}
			this.mMinHoverTime -= this.mDecMinHover;
			this.mMaxHoverTime -= this.mDecMaxHover;
			this.mMinFireDelay -= this.mDecMinFire;
			this.mMaxFireDelay -= this.mDecMaxFire;
			if (this.mMaxRetaliationBullets > 0 && this.CanRetaliate() && !base.IsStunned())
			{
				int num3 = 0;
				for (int i = 0; i < this.mMaxRetaliationBullets; i++)
				{
					BossBullet bossBullet = new BossBullet();
					this.mBullets.Add(bossBullet);
					bossBullet.mX = this.mX;
					bossBullet.mY = this.mY;
					bossBullet.mId = ++BossShoot.gLastBulletId;
					bossBullet.mDelay = i * this.mRetalShotDelay;
					if (this.mSinusoidalRetaliation)
					{
						if (!this.FireSinusoidalBullet(bossBullet, (this.mMaxRetaliationBullets == 1) ? (Common.Rand() % 100 < 50) : ((i + 1) % 2 == 0)))
						{
							this.mBullets.RemoveAt(this.mBullets.Count - 1);
						}
						else
						{
							bossBullet.mTargetVX = bossBullet.mVX;
							bossBullet.mTargetVY = bossBullet.mVY;
							num3++;
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS1_FIRE));
						}
					}
					else
					{
						num3++;
						this.FireBulletAtPlayer(bossBullet, Common.FloatRange(this.mMinRetalSpeed, this.mMaxRetalSpeed));
						bossBullet.mTargetVX = bossBullet.mVX;
						bossBullet.mTargetVY = bossBullet.mVY;
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS1_FIRE));
					}
				}
				base.PlaySound(2);
				this.DidRetaliate(num3);
			}
			return true;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00081554 File Offset: 0x0007F754
		protected void GetTargetedVelocity(float speed, float x, float y, ref float vx, ref float vy)
		{
			float num = Common.AngleBetweenPoints(x, y, (float)this.mLevel.mFrog.GetCenterX(), (float)this.mLevel.mFrog.GetCenterY());
			vx = (float)Math.Cos((double)num) * speed;
			vy = -(float)Math.Sin((double)num) * speed;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000815A8 File Offset: 0x0007F7A8
		protected float FireBulletAtPlayer(BossBullet b, float speed, float x, float y)
		{
			float num = Common.AngleBetweenPoints(x, y, (float)this.mLevel.mFrog.GetCenterX(), (float)this.mLevel.mFrog.GetCenterY());
			b.mVX = (float)Math.Cos((double)num) * speed;
			b.mVY = -(float)Math.Sin((double)num) * speed;
			b.mSineMotion = false;
			b.mGravity = 0f;
			b.mInitialSpeed = speed;
			return num;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0008161B File Offset: 0x0007F81B
		protected float FireBulletAtPlayer(BossBullet b, float speed)
		{
			return this.FireBulletAtPlayer(b, speed, this.mX, this.mY);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00081634 File Offset: 0x0007F834
		protected bool FireSinusoidalBullet(BossBullet b, bool negative)
		{
			b.mVX = (b.mVY = 0f);
			if (!this.mSineShotsTargetPlayer)
			{
				b.mSineMotion = true;
				b.mAmp = Common.FloatRange(this.mMinAmp, this.mMaxAmp);
				b.mFreq = Common.FloatRange(this.mMinFreq, this.mMaxFreq);
				if (negative)
				{
					b.mAmp *= -1f;
				}
				if (this.mStartX > 0)
				{
					b.mVY = Common.FloatRange(this.mMinYInc, this.mMaxYInc);
				}
				else
				{
					b.mVX = Common.FloatRange(this.mMinXInc, this.mMaxXInc);
				}
				b.mGravity = 0f;
				return true;
			}
			if (this.mStartY > 0)
			{
				b.mSineMotion = false;
				b.mGravity = Common._M(0.04f);
				if (negative)
				{
					b.mGravity *= -1f;
				}
				int num = Common.IntRange(this.mMinSineShotTime, this.mMaxSineShotTime);
				b.mVX = ((float)this.mLevel.mFrog.GetCenterX() - this.mX) / (float)num;
				b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY - 0.5f * b.mGravity * (float)num * (float)num) / (float)num;
				int num2 = (int)Math.Abs(b.mVY / b.mGravity);
				bool flag = true;
				float num3 = b.mVY * (float)num2 + 0.5f * b.mGravity * (float)num2 * (float)num2 + this.mY;
				while (((num3 < (float)(BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mGravity > 0f) || (num3 > (float)(Common._SS(GameApp.gApp.mHeight) - BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mGravity < 0f)) && num > this.mMinSineShotTime)
				{
					if (flag)
					{
						flag = false;
						num = this.mMaxSineShotTime;
					}
					num -= Common._M(5);
					b.mVX = ((float)this.mLevel.mFrog.GetCenterX() - this.mX) / (float)num;
					b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY - 0.5f * b.mGravity * (float)num * (float)num) / (float)num;
					num2 = (int)Math.Abs(b.mVY / b.mGravity);
					num3 = b.mVY * (float)num2 + 0.5f * b.mGravity * (float)num2 * (float)num2 + this.mY;
				}
				if (num < this.mMinSineShotTime)
				{
					return false;
				}
			}
			else
			{
				b.mSineMotion = false;
				b.mGravity = Common._M(-0.08f);
				if (negative)
				{
					b.mGravity *= -1f;
				}
				int num4 = Common.IntRange(this.mMinSineShotTime, this.mMaxSineShotTime);
				b.mVX = -((float)this.mLevel.mFrog.GetCenterX() - this.mX + 0.5f * b.mGravity * (float)num4 * (float)num4) / (float)num4;
				b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY) / (float)num4;
				int num5 = (int)Math.Abs(b.mVX / b.mGravity);
				bool flag2 = true;
				float num6 = b.mVX * (float)num5 + 0.5f * b.mGravity * (float)num5 * (float)num5 + this.mX;
				while (((num6 < (float)(BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mVX < 0f) || (num6 > (float)(Common._SS(GameApp.gApp.mWidth) - BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mVX > 0f)) && num4 > this.mMinSineShotTime)
				{
					if (flag2)
					{
						flag2 = false;
						num4 = this.mMaxSineShotTime;
					}
					num4 -= Common._M(5);
					b.mVX = -((float)this.mLevel.mFrog.GetCenterX() - this.mX + 0.5f * b.mGravity * (float)num4 * (float)num4) / (float)num4;
					b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY) / (float)num4;
					num5 = (int)Math.Abs(b.mVX / b.mGravity);
					num6 = b.mVX * (float)num5 + 0.5f * b.mGravity * (float)num5 * (float)num5 + this.mX;
				}
				if (num4 <= this.mMinSineShotTime)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00081AB8 File Offset: 0x0007FCB8
		protected override void ReInit()
		{
			base.ReInit();
			if (this.mColorVampire)
			{
				this.mColorVampShotType = Common.Rand() % 4;
				this.mColorChangeTimer = Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
			}
			if (this.mColorVampHealthInc > 0)
			{
				this.mColorVampHealthIncPerHit = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / (float)this.mColorVampHealthInc));
			}
			if (this.mStrafe)
			{
				this.mFireDelay = Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
			}
			this.mSpeed = (float)Math.Sign(this.mSpeed) * this.mDefaultSpeed;
			if (this.mMinRetalSpeed == 0f)
			{
				this.mMinRetalSpeed = this.mMinBulletSpeed;
			}
			if (this.mMaxRetalSpeed == 0f)
			{
				this.mMaxRetalSpeed = this.mMaxBulletSpeed;
			}
			this.mCurShieldPauseTime = this.mShieldPauseTime;
			if (this.mTeleportMinTime != 0 && this.mTeleportMaxTime != 0)
			{
				this.mTeleportTime = Common.IntRange(this.mTeleportMinTime, this.mTeleportMaxTime);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00081BBC File Offset: 0x0007FDBC
		protected virtual void DrawBossSpecificArt(SexyGraphics g)
		{
			int default_BULLET_SIZE = BossShoot.DEFAULT_BULLET_SIZE;
			int default_BULLET_SIZE2 = BossShoot.DEFAULT_BULLET_SIZE;
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					if (this.mBullets[i].mDelay <= 0)
					{
						g.SetColor(Color.White);
						CommonGraphics.DrawCircle(g, Common._S(this.mBullets[i].mX), Common._S(this.mBullets[i].mY), (float)Common._S(Common._M(24)), 30);
					}
				}
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00081C60 File Offset: 0x0007FE60
		protected override void DrawMisc(SexyGraphics g)
		{
			if (this.mIsBerserk)
			{
				this.DrawBerserk(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				int num = (int)Common._S(this.mX) - this.mWidth / 2;
				int num2 = (int)Common._S(this.mY) - this.mHeight / 2;
				int num3 = Common._S(this.mWidth);
				int num4;
				if (this.mTeleportDir == -1)
				{
					num4 = (int)((float)Common._S(this.mHeight) - (float)Common._S(this.mHeight) * this.mTeleportPct);
				}
				else
				{
					num4 = (int)((float)Common._S(this.mHeight) * this.mTeleportPct);
				}
				g.ClipRect(num, num2, num3, num4);
			}
			if (this.mUseShield)
			{
				this.DrawShield(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			base.DrawMisc(g);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00081D38 File Offset: 0x0007FF38
		protected virtual bool PreBulletUpdate(BossBullet b, int index)
		{
			if (b.mDelay > 0)
			{
				if (--b.mDelay == 0)
				{
					b.mX = this.mX;
					b.mY = this.mY;
					base.PlaySound(2);
				}
				return true;
			}
			if (b.mOffscreenPause > 0 && b.mY < (float)Common._M(-305))
			{
				if (--b.mOffscreenPause == 0)
				{
					b.mVY *= -1f;
					int centerX = this.mLevel.mFrog.GetCenterX();
					b.mX = (float)centerX;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00081DDF File Offset: 0x0007FFDF
		protected virtual void BulletErased(int index)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00081DE1 File Offset: 0x0007FFE1
		protected virtual void DidFire()
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00081DE3 File Offset: 0x0007FFE3
		protected virtual void DidRetaliate(int num_shot)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00081DE8 File Offset: 0x0007FFE8
		protected virtual Rect GetBulletRect(BossBullet b)
		{
			int num = (int)((float)BossShoot.DEFAULT_BULLET_SIZE * 0.75f);
			int num2 = (int)((float)BossShoot.DEFAULT_BULLET_SIZE * 0.75f);
			return new Rect((int)b.mX - num / 2, (int)b.mY - num2 / 2, num, num2);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00081E30 File Offset: 0x00080030
		protected virtual Rect GetFrogRect()
		{
			int num = Common._M(1);
			return new Rect(this.mLevel.mFrog.GetCenterX() - num, this.mLevel.mFrog.GetCenterY() - num, num * 2, num * 2);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00081E73 File Offset: 0x00080073
		protected virtual bool CanFire()
		{
			return true;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00081E76 File Offset: 0x00080076
		protected virtual void BulletHitPlayer(BossBullet b)
		{
			base.PlaySound(4);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00081E80 File Offset: 0x00080080
		protected void WarpToPoint(bool play_sound)
		{
			if (this.mPoints.Count == 0)
			{
				return;
			}
			int num;
			do
			{
				num = Common.Rand() % this.mPoints.Count;
			}
			while (num == this.mCurrentLocPoint && this.mPoints.Count > 1);
			this.mX = (float)this.mPoints[num].mX;
			this.mY = (float)this.mPoints[num].mY;
			this.mCurrentLocPoint = num;
			if (play_sound)
			{
				base.PlaySound(8);
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00081F05 File Offset: 0x00080105
		protected void WarpToPoint()
		{
			this.WarpToPoint(true);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00081F10 File Offset: 0x00080110
		protected void EndHoverOnHit()
		{
			this.mHoverTime = 0;
			if (this.mTeleportTime != -1)
			{
				this.mTeleportDir = -1;
				this.mTeleportPct = 0f;
				this.mTeleportTime = 0;
				return;
			}
			if (this.mPoints.Count == 0)
			{
				this.mHoverTime = 0;
				this.mXOff = (this.mYOff = 0);
				if (this.mStartX > 0)
				{
					this.CalcDestX(this.mFlightMinDist);
				}
				else
				{
					this.CalcDestY(this.mFlightMinDist);
				}
				if (this.mFlightSpeed > 0f)
				{
					this.mSpeed = this.mFlightSpeed * (float)Math.Sign(this.mSpeed);
					return;
				}
			}
			else if (this.mEndHoverCountdown == 0)
			{
				this.mEndHoverCountdown = Common._M(300);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00081FD0 File Offset: 0x000801D0
		protected override void BerserkActivated(int health_limit)
		{
			base.BerserkActivated(health_limit);
			if (this.mEnrageShieldRestore)
			{
				for (int i = 0; i < 4; i++)
				{
					this.mShieldQuadrant[i].mTimer = 0;
					this.mShieldQuadrant[i].mHP = this.mShieldHP;
				}
			}
			this.mEnrageDelayTimer = this.mEnrageDelay;
			int j = 0;
			while (j < this.mBerserkMovementVec.Count)
			{
				BossBerserkMovement bossBerserkMovement = this.mBerserkMovementVec[j];
				if (bossBerserkMovement.mHealthLimit == health_limit)
				{
					this.mStartX = bossBerserkMovement.mStartX;
					this.mStartY = bossBerserkMovement.mStartY;
					this.mEndX = bossBerserkMovement.mEndX;
					this.mEndY = bossBerserkMovement.mEndY;
					bool flag = this.mPoints.Count > 0;
					this.mPoints.Clear();
					if (bossBerserkMovement.mPoints.Count > 0)
					{
						this.mPoints.AddRange(bossBerserkMovement.mPoints.ToArray());
					}
					if (bossBerserkMovement.mX != 2147483647)
					{
						this.SetX((float)bossBerserkMovement.mX);
					}
					if (bossBerserkMovement.mY != 2147483647)
					{
						this.SetY((float)bossBerserkMovement.mY);
					}
					if (this.mPoints.Count != 0)
					{
						if (!flag)
						{
							this.mHoverTime = Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							this.WarpToPoint();
						}
						return;
					}
					this.mCurrentLocPoint = -1;
					this.mHoverTime = 0;
					if (this.mStartY <= 0)
					{
						this.CalcDestX();
						return;
					}
					this.CalcDestY();
					return;
				}
				else
				{
					j++;
				}
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0008214F File Offset: 0x0008034F
		protected virtual void DrawBerserk(SexyGraphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				this.mLevel.mBoard.DoingBossIntro();
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00082177 File Offset: 0x00080377
		protected virtual void DrawShield(SexyGraphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				this.mLevel.mBoard.DoingBossIntro();
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0008219F File Offset: 0x0008039F
		protected virtual BossBullet CreateBossBullet()
		{
			return new BossBullet();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000821A6 File Offset: 0x000803A6
		protected virtual void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000821A8 File Offset: 0x000803A8
		protected virtual bool CheckBulletHitPlayer(BossBullet b)
		{
			if (this.mMaxShotBounces > 0 && b.mBouncesLeft <= 0)
			{
				return false;
			}
			Rect bulletRect = this.GetBulletRect(b);
			this.mLevel.mFrog.GetWidth();
			this.mLevel.mFrog.GetHeight();
			float y = (float)(this.mLevel.mFrog.GetCenterY() - 5);
			float x = (float)(this.mLevel.mFrog.GetCenterX() + 2);
			return b.mCanHitPlayer && ((this.mBulletsUseSphereColl && MathUtils.CirclesIntersect(x, y, (float)(bulletRect.mX + bulletRect.mWidth / 2), (float)(bulletRect.mY + bulletRect.mHeight / 2), (float)(40 + this.mBulletRadius))) || (!this.mBulletsUseSphereColl && bulletRect.Intersects(this.GetFrogRect())));
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0008227C File Offset: 0x0008047C
		protected virtual void ShieldQuadrantHit(int quad)
		{
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0008227E File Offset: 0x0008047E
		protected virtual bool CanRetaliate()
		{
			return true;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00082281 File Offset: 0x00080481
		protected virtual void GetShotBounceOffs(BossBullet b, ref int x, ref int y)
		{
			x = 0;
			y = 0;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00082289 File Offset: 0x00080489
		protected virtual void QuadHitByProxBomb(int quad)
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0008228B File Offset: 0x0008048B
		protected virtual void ShotBounced(BossBullet b)
		{
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0008228D File Offset: 0x0008048D
		protected virtual void AppliedSlowTimer()
		{
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00082290 File Offset: 0x00080490
		protected void CopyFrom(BossShoot rhs)
		{
			base.CopyFrom(rhs);
			this.mHitEffectYOff = rhs.mHitEffectYOff;
			this.mPauseMovement = rhs.mPauseMovement;
			this.mPauseShieldRegen = rhs.mPauseShieldRegen;
			this.mDrawHeartsBelowMisc = rhs.mDrawHeartsBelowMisc;
			this.mDrawHeartsBelowBoss = rhs.mDrawHeartsBelowBoss;
			this.mDestX = rhs.mDestX;
			this.mDestY = rhs.mDestY;
			this.mTargetDestX = rhs.mTargetDestX;
			this.mShieldAngle = rhs.mShieldAngle;
			this.mShieldTargetAngle = rhs.mShieldTargetAngle;
			this.mShieldRadius = rhs.mShieldRadius;
			this.mHoverTime = rhs.mHoverTime;
			this.mFireDelay = rhs.mFireDelay;
			this.mXOff = rhs.mXOff;
			this.mYOff = rhs.mYOff;
			this.mCurShieldPauseTime = rhs.mCurShieldPauseTime;
			this.mEndHoverCountdown = rhs.mEndHoverCountdown;
			this.mEnrageDelayTimer = rhs.mEnrageDelayTimer;
			this.mMovementUpdateDelay = rhs.mMovementUpdateDelay;
			this.mAttackDelayAfterHitFrog = rhs.mAttackDelayAfterHitFrog;
			this.mCurrentLocPoint = rhs.mCurrentLocPoint;
			this.mMaxShotBounces = rhs.mMaxShotBounces;
			this.mShieldPauseTime = rhs.mShieldPauseTime;
			this.mShieldRotateSpeed = rhs.mShieldRotateSpeed;
			this.mUseShield = rhs.mUseShield;
			this.mShieldQuadRespawnTime = rhs.mShieldQuadRespawnTime;
			this.mShieldHP = rhs.mShieldHP;
			this.mBallShieldDamage = rhs.mBallShieldDamage;
			this.mTeleportDir = rhs.mTeleportDir;
			this.mTeleportPct = rhs.mTeleportPct;
			this.mTeleportMinTime = rhs.mTeleportMinTime;
			this.mTeleportMaxTime = rhs.mTeleportMaxTime;
			this.mTeleportTime = rhs.mTeleportTime;
			this.mEnrageDelay = rhs.mEnrageDelay;
			this.mBombAppearDelay = rhs.mBombAppearDelay;
			this.mShotDelay = rhs.mShotDelay;
			this.mBombAppearDelay = rhs.mBombAppearDelay;
			this.mShotType = rhs.mShotType;
			this.mStartX = rhs.mStartX;
			this.mStartY = rhs.mStartY;
			this.mEndX = rhs.mEndX;
			this.mEndY = rhs.mEndY;
			this.mMinHoverTime = rhs.mMinHoverTime;
			this.mMaxHoverTime = rhs.mMaxHoverTime;
			this.mMinFireDelay = rhs.mMinFireDelay;
			this.mMaxFireDelay = rhs.mMaxFireDelay;
			this.mFrogStunTime = rhs.mFrogStunTime;
			this.mFrogPoisonTime = rhs.mFrogPoisonTime;
			this.mFrogHallucinateTime = rhs.mFrogHallucinateTime;
			this.mDecMinHover = rhs.mDecMinHover;
			this.mDecMaxHover = rhs.mDecMaxHover;
			this.mDecMinFire = rhs.mDecMinFire;
			this.mDecMaxFire = rhs.mDecMaxFire;
			this.mSubType = rhs.mSubType;
			this.mMaxBulletsToFire = rhs.mMaxBulletsToFire;
			this.mMaxRetaliationBullets = rhs.mMaxRetaliationBullets;
			this.mMinSineShotTime = rhs.mMinSineShotTime;
			this.mColorVampShotType = rhs.mColorVampShotType;
			this.mColorChangeTimer = rhs.mColorChangeTimer;
			this.mMinColorChangeTime = rhs.mMinColorChangeTime;
			this.mMaxColorChangeTime = rhs.mMaxColorChangeTime;
			this.mColorVampHealthInc = rhs.mColorVampHealthInc;
			this.mColorVampHealthIncPerHit = rhs.mColorVampHealthIncPerHit;
			this.mIncMaxShotHealthAmt = rhs.mIncMaxShotHealthAmt;
			this.mIncRetalMaxShotHealthAmt = rhs.mIncRetalMaxShotHealthAmt;
			this.mMaxShotIncCounter = rhs.mMaxShotIncCounter;
			this.mRetalShotIncCounter = rhs.mRetalShotIncCounter;
			this.mDefaultMovementUpdateDelay = rhs.mDefaultMovementUpdateDelay;
			this.mMovementMode = rhs.mMovementMode;
			this.mMovementAccel = rhs.mMovementAccel;
			this.mMinSpots = rhs.mMinSpots;
			this.mMaxSpots = rhs.mMaxSpots;
			this.mMinSpotRad = rhs.mMinSpotRad;
			this.mMaxSpotRad = rhs.mMaxSpotRad;
			this.mMinSpotFade = rhs.mMinSpotFade;
			this.mMaxSpotFade = rhs.mMaxSpotFade;
			this.mInkTargetMode = rhs.mInkTargetMode;
			this.mSpotFadeDelay = rhs.mSpotFadeDelay;
			this.mFlightSpeed = rhs.mFlightSpeed;
			this.mHomingCorrectionAmt = rhs.mHomingCorrectionAmt;
			this.mFlightMinDist = rhs.mFlightMinDist;
			this.mColorVampChanceToMatch2ndBall = rhs.mColorVampChanceToMatch2ndBall;
			this.mBulletRadius = rhs.mBulletRadius;
			this.mSinusoidalRetaliation = rhs.mSinusoidalRetaliation;
			this.mCanShootBullets = rhs.mCanShootBullets;
			this.mSineShotsTargetPlayer = rhs.mSineShotsTargetPlayer;
			this.mEndHoverOnHit = rhs.mEndHoverOnHit;
			this.mColorVampire = rhs.mColorVampire;
			this.mAvoidColor = rhs.mAvoidColor;
			this.mStrafe = rhs.mStrafe;
			this.mEnrageShieldRestore = rhs.mEnrageShieldRestore;
			this.mBulletsUseSphereColl = rhs.mBulletsUseSphereColl;
			this.mMinBulletSpeed = rhs.mMinBulletSpeed;
			this.mMaxBulletSpeed = rhs.mMaxBulletSpeed;
			this.mMinRetalSpeed = rhs.mMinRetalSpeed;
			this.mMaxRetalSpeed = rhs.mMaxRetalSpeed;
			this.mSpeed = rhs.mSpeed;
			this.mDefaultSpeed = rhs.mDefaultSpeed;
			this.mVolcanoOffscreenDelay = rhs.mVolcanoOffscreenDelay;
			this.mMinAmp = rhs.mMinAmp;
			this.mMaxAmp = rhs.mMaxAmp;
			this.mMinYInc = rhs.mMinYInc;
			this.mMaxYInc = rhs.mMaxYInc;
			this.mMinXInc = rhs.mMinXInc;
			this.mMaxXInc = rhs.mMaxXInc;
			this.mMinFreq = rhs.mMinFreq;
			this.mMaxFreq = rhs.mMaxFreq;
			this.mFrogSlowTimer = rhs.mFrogSlowTimer;
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i] = rhs.mShieldQuadrant[i];
			}
			this.mPoints.Clear();
			for (int j = 0; j < rhs.mPoints.Count; j++)
			{
				this.mPoints.Add(new Point(rhs.mPoints[j]));
			}
			this.mBerserkMovementVec.Clear();
			for (int k = 0; k < rhs.mBerserkMovementVec.Count; k++)
			{
				this.mBerserkMovementVec.Add(new BossBerserkMovement(rhs.mBerserkMovementVec[k]));
			}
			this.mBullets.Clear();
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00082858 File Offset: 0x00080A58
		public BossShoot(Level l) : base(l)
		{
			this.mDestX = 0f;
			this.mDestY = 0f;
			this.mSpeed = 0f;
			this.mHoverTime = 0;
			this.mFireDelay = 0;
			this.mStartX = 0;
			this.mEndX = 0;
			this.mStartY = 0;
			this.mEndY = 0;
			this.mMinHoverTime = 0;
			this.mMaxHoverTime = 0;
			this.mPauseShieldRegen = false;
			this.mMinFireDelay = 0;
			this.mMaxFireDelay = 0;
			this.mMinBulletSpeed = 0f;
			this.mMaxBulletSpeed = 0f;
			this.mFrogStunTime = 0;
			this.mDecMinHover = 0;
			this.mDecMaxHover = 0;
			this.mDecMinFire = 0;
			this.mDecMaxFire = 0;
			this.mXOff = 0;
			this.mYOff = 0;
			this.mSubType = 0;
			this.mMaxBulletsToFire = 1;
			this.mMaxRetaliationBullets = 0;
			this.mSinusoidalRetaliation = false;
			this.mMinAmp = 2f;
			this.mMaxAmp = 4f;
			this.mMinYInc = 2f;
			this.mMaxYInc = 4f;
			this.mMinXInc = 2f;
			this.mMaxXInc = 4f;
			this.mMinFreq = 0.04f;
			this.mMaxFreq = 0.04f;
			this.mPauseMovement = false;
			this.mCanShootBullets = false;
			this.mSineShotsTargetPlayer = false;
			this.mMinSineShotTime = 200;
			this.mMaxSineShotTime = 400;
			this.mShotType = 5;
			this.mEndHoverOnHit = false;
			this.mColorVampShotType = -1;
			this.mColorVampire = false;
			this.mAvoidColor = false;
			this.mMinColorChangeTime = 0;
			this.mMaxColorChangeTime = 0;
			this.mColorChangeTimer = 0;
			this.mColorVampHealthInc = 0;
			this.mColorVampHealthIncPerHit = 0;
			this.mStrafe = false;
			this.mMaxShotIncCounter = 0;
			this.mRetalShotIncCounter = 0;
			this.mIncMaxShotHealthAmt = 0;
			this.mIncRetalMaxShotHealthAmt = 0;
			this.mRetalShotDelay = 0;
			this.mMinRetalSpeed = 0f;
			this.mMaxRetalSpeed = 0f;
			this.mColorVampChanceToMatch2ndBall = 0;
			this.mFrogPoisonTime = 0;
			this.mFlightSpeed = 0f;
			this.mFlightMinDist = 100;
			this.mFrogHallucinateTime = 0;
			this.mHomingCorrectionAmt = 0.05f;
			this.mFrogSlowTimer = 0;
			this.mBombAppearDelay = 0;
			this.mCurrentLocPoint = -1;
			this.mUseShield = false;
			this.mShieldPauseTime = 0;
			this.mShieldRotateSpeed = 0f;
			this.mCurShieldPauseTime = 0;
			this.mShieldAngle = 0f;
			this.mShieldTargetAngle = 0f;
			this.mShieldQuadRespawnTime = 0;
			this.mBallShieldDamage = 0;
			this.mShieldHP = 1;
			this.mEnrageShieldRestore = false;
			this.mEndHoverCountdown = 0;
			this.mMinSpotRad = 0;
			this.mMaxSpotRad = 0;
			this.mMinSpots = 0;
			this.mMaxSpots = 0;
			this.mMinSpotFade = 0f;
			this.mMaxSpotFade = 0f;
			this.mSpotFadeDelay = 0;
			this.mInkTargetMode = 0;
			this.mEnrageDelay = 0;
			this.mEnrageDelayTimer = 0;
			this.mMovementMode = 0;
			this.mMovementAccel = 999999f;
			this.mDefaultMovementUpdateDelay = 0;
			this.mMovementUpdateDelay = 0;
			this.mTargetDestX = 0f;
			this.mVolcanoOffscreenDelay = 0;
			this.mBulletsUseSphereColl = true;
			this.mBulletRadius = 0;
			this.mTeleportPct = 0f;
			this.mTeleportTime = -1;
			this.mTeleportMinTime = 0;
			this.mTeleportMaxTime = 0;
			this.mTeleportDir = 0;
			this.mShieldRadius = 120;
			this.mDrawHeartsBelowMisc = true;
			this.mAttackDelayAfterHitFrog = 0;
			this.mMaxShotBounces = 0;
			this.mHitEffectYOff = 40;
			this.mDrawHeartsBelowBoss = false;
			this.mBossRadius = 24;
			this.mWidth = 146;
			this.mHeight = 124;
			Common.Reserve<BossBullet>(this.mBullets, 100);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00082E4F File Offset: 0x0008104F
		public BossShoot() : this(null)
		{
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00082E58 File Offset: 0x00081058
		public override void Dispose()
		{
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00082E5C File Offset: 0x0008105C
		public virtual void DeleteAllBullets()
		{
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				this.BossBulletDestroyed(this.mBullets[i], true);
				this.BulletErased(i);
			}
			this.mBullets.Clear();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00082EA4 File Offset: 0x000810A4
		public virtual void SetDestX(float dx)
		{
			this.mTargetDestX = dx;
			if (this.mMovementUpdateDelay <= 0)
			{
				this.mMovementUpdateDelay = this.mDefaultMovementUpdateDelay;
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00082EC2 File Offset: 0x000810C2
		public virtual void AddBerserkMovement(BossBerserkMovement bbm)
		{
			this.mBerserkMovementVec.Add(bbm);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00082ED0 File Offset: 0x000810D0
		public List<BossBerserkMovement> getBerserkMovementList()
		{
			return this.mBerserkMovementVec;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00082ED8 File Offset: 0x000810D8
		public override void PostInstantiationHook(Boss source_boss)
		{
			base.PostInstantiationHook(source_boss);
			base.AddParamPointer("ColorHelp", this.mDColorVampChanceToMatch2ndBall);
			base.AddParamPointer("FrogStun", this.mDFrogStunTime);
			base.AddParamPointer("stun", this.mDFrogStunTime);
			base.AddParamPointer("FrogPoison", this.mDFrogPoisonTime);
			base.AddParamPointer("FrogHallucinate", this.mDFrogHallucinateTime);
			base.AddParamPointer("hallucinate", this.mDFrogHallucinateTime);
			base.AddParamPointer("poison", this.mDFrogPoisonTime);
			base.AddParamPointer("SlowShot", this.mDFrogSlowTimer);
			base.AddParamPointer("ShotDelay", this.mDShotDelay);
			base.AddParamPointer("flightspeed", this.mDFlightSpeed);
			base.AddParamPointer("minflightdist", this.mDFlightMinDist);
			base.AddParamPointer("VampHealthInc", this.mDColorVampHealthInc);
			base.AddParamPointer("VampColorChangeMin", this.mDMinColorChangeTime);
			base.AddParamPointer("VampColorChangeMax", this.mDMaxColorChangeTime);
			base.AddParamPointer("HomingSpeed", this.mDHomingCorrectionAmt);
			base.AddParamPointer("MinHover", this.mDMinHoverTime);
			base.AddParamPointer("MaxHover", this.mDMaxHoverTime);
			base.AddParamPointer("MinFire", this.mDMinFireDelay);
			base.AddParamPointer("MaxFire", this.mDMaxFireDelay);
			base.AddParamPointer("MinBullet", this.mDMinBulletSpeed);
			base.AddParamPointer("MaxBullet", this.mDMaxBulletSpeed);
			base.AddParamPointer("MaxBullets", this.mDMaxBulletsToFire);
			base.AddParamPointer("Retaliation", this.mDMaxRetaliationBullets);
			base.AddParamPointer("MinSineShotTime", this.mDMinSineShotTime);
			base.AddParamPointer("MaxSineShotTime", this.mDMaxSineShotTime);
			base.AddParamPointer("MinAmp", this.mDMinAmp);
			base.AddParamPointer("MaxAmp", this.mDMaxAmp);
			base.AddParamPointer("MinFreq", this.mDMinFreq);
			base.AddParamPointer("MaxFreq", this.mDMaxFreq);
			base.AddParamPointer("MaxSineYInc", this.mDMaxYInc);
			base.AddParamPointer("MinSineYInc", this.mDMinYInc);
			base.AddParamPointer("MaxSineXInc", this.mDMaxXInc);
			base.AddParamPointer("MinSineXInc", this.mDMinXInc);
			base.AddParamPointer("MoveSpeed", this.mDDefaultSpeed);
			base.AddParamPointer("Strafe", this.mDStrafe);
			base.AddParamPointer("EndHoverOnHit", this.mDEndHoverOnHit);
			base.AddParamPointer("RetalSpeedMin", this.mDMinRetalSpeed);
			base.AddParamPointer("RetalSpeedMax", this.mDMaxRetalSpeed);
			base.AddParamPointer("ShotType", this.mDShotType);
			base.AddParamPointer("TeleportMinTime", this.mDTeleportMinTime);
			base.AddParamPointer("TeleportMaxTime", this.mDTeleportMaxTime);
			base.AddParamPointer("Accel", this.mDMovementAccel);
			base.AddParamPointer("MoveDelay", this.mDDefaultMovementUpdateDelay);
			base.AddParamPointer("MoveMode", this.mDMovementMode);
			base.AddParamPointer("UseShield", this.mDUseShield);
			base.AddParamPointer("ShieldRotSpeed", this.mDShieldRotateSpeed);
			base.AddParamPointer("ShieldRespawnTime", this.mDShieldQuadRespawnTime);
			base.AddParamPointer("ShieldPauseTime", this.mDShieldPauseTime);
			base.AddParamPointer("ShieldHP", this.mDShieldHP);
			base.AddParamPointer("BallShieldDamage", this.mDBallShieldDamage);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00083240 File Offset: 0x00081440
		public override void Init(Level l)
		{
			base.Init(l);
			if (this.mMovementMode == 0)
			{
				if (this.mPoints.Count == 0)
				{
					if (this.mStartY <= 0)
					{
						this.mX = (float)(this.mEndX - this.mStartX) / 2f + (float)this.mStartX;
						this.CalcDestX();
					}
					else
					{
						this.mY = (float)(this.mEndY - this.mStartY) / 2f + (float)this.mStartY;
						this.CalcDestY();
					}
				}
				else
				{
					this.WarpToPoint(false);
					this.mHoverTime = Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
				}
			}
			else
			{
				this.mTargetDestX = (this.mDestX = (this.mX = (float)Common._SS(GameApp.gApp.mWidth) / 2f - (float)GameApp.gApp.mBoardOffsetX));
			}
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i].mHP = this.mShieldHP;
			}
			this.ReInit();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0008334C File Offset: 0x0008154C
		public override void Update(float f)
		{
			BossShoot.gVolcanoBulletCounter = 0;
			base.Update(f);
			bool flag = this.AtDest();
			if (this.mDoDeathExplosions || this.mHP <= 0f)
			{
				return;
			}
			if (this.mTeleportTime > 0)
			{
				this.mTeleportTime--;
			}
			bool flag2 = this.mLevel.AllCurvesAtRolloutPoint();
			if (this.mTeleportTime == 0 && Common._geq(this.mAlphaOverride, 255f) && this.mFireDelay > 0 && !base.IsStunned() && flag2 && this.mTeleportDir == 0)
			{
				this.mTeleportDir = -1;
				this.mTeleportPct = 0f;
			}
			if (this.mTeleportDir != 0)
			{
				this.mTeleportPct += Common._M(0.05f);
				if (this.mTeleportPct >= 1f)
				{
					this.mTeleportPct = 0f;
					if (this.mTeleportDir == -1)
					{
						this.mTeleportDir = 1;
						this.mX = (float)this.GetMinXDist();
						this.CalcDestX();
					}
					else
					{
						this.mTeleportTime = Common.IntRange(this.mTeleportMinTime, this.mTeleportMaxTime);
						this.mTeleportDir = 0;
					}
				}
			}
			if (!base.IsStunned() && flag2 && Common._geq(this.mAlphaOverride, 255f))
			{
				if (base.IsImpatient())
				{
					float num = Common._M(0.0003f);
					this.mMinBulletSpeed += num;
					this.mMaxBulletSpeed += num;
					this.mMinRetalSpeed += num;
					this.mMaxRetalSpeed += num;
					if (this.mFireDelay > 100)
					{
						this.mFireDelay--;
					}
				}
				if (this.mUseShield)
				{
					if (this.mShieldPauseTime > 0 && this.mCurShieldPauseTime > 0 && --this.mCurShieldPauseTime == 0)
					{
						this.mShieldTargetAngle = this.mShieldAngle + 1.570795f;
					}
					if (this.mShieldPauseTime > 0 && this.mCurShieldPauseTime == 0 && this.mShieldAngle < this.mShieldTargetAngle)
					{
						this.mShieldAngle += 1.570795f / (float)Common._M(25);
						if (this.mShieldAngle > this.mShieldTargetAngle)
						{
							this.mCurShieldPauseTime = this.mShieldPauseTime;
							this.mShieldAngle = Common.GetCanonicalAngleRad(this.mShieldTargetAngle);
						}
					}
					if (this.mShieldPauseTime == 0 && (this.mShieldAngle += this.mShieldRotateSpeed) > 6.28318f)
					{
						this.mShieldAngle = Common.GetCanonicalAngleRad(this.mShieldAngle);
					}
					if (!this.mPauseShieldRegen)
					{
						for (int i = 0; i < 4; i++)
						{
							if (this.mShieldQuadrant[i].mTimer > 0)
							{
								this.mShieldQuadrant[i].mTimer--;
							}
						}
					}
				}
				if (this.mColorChangeTimer > 0 && this.mColorVampire && --this.mColorChangeTimer == 0)
				{
					int num2;
					for (num2 = this.mColorVampShotType; num2 == this.mColorVampShotType; num2 = Common.Rand() % 4)
					{
					}
					this.mColorVampShotType = num2;
					this.mColorChangeTimer = Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
				}
				if (this.mTeleportDir == 0 && !this.mPauseMovement)
				{
					if (this.mMovementMode != 0)
					{
						if (this.mMovementUpdateDelay >= 0 && --this.mMovementUpdateDelay <= 0)
						{
							this.mDestX = this.mTargetDestX;
						}
						if (!Common._eq(this.mX, this.mDestX))
						{
							float mX = this.mX;
							if (this.mX < this.mDestX)
							{
								this.mX += this.mMovementAccel;
							}
							else
							{
								this.mX -= this.mMovementAccel;
							}
							if ((mX < this.mDestX && this.mX >= this.mDestX) || (mX > this.mDestX && this.mX <= this.mDestX))
							{
								this.mX = this.mDestX;
							}
						}
					}
					if (this.mHoverTime == 0 && this.mEndHoverCountdown == 0 && this.mEnrageDelayTimer == 0 && !flag && !this.mStrafe && this.mMovementMode == 0)
					{
						if (this.mStartX > 0)
						{
							this.mX += this.mSpeed;
						}
						else
						{
							this.mY += this.mSpeed;
						}
						if (this.AtDest())
						{
							if (this.mStartX > 0)
							{
								this.mX = this.mDestX;
							}
							else
							{
								this.mY = this.mDestY;
							}
							this.mHoverTime = Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							this.mFireDelay = Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
							if (this.mFireDelay > this.mHoverTime)
							{
								this.mFireDelay = this.mHoverTime / 2;
							}
						}
					}
					else if ((this.mHoverTime > 0 || this.mStrafe || this.mMovementMode != 0) && this.mEndHoverCountdown == 0 && this.mEnrageDelayTimer == 0)
					{
						if (!this.mStrafe)
						{
							this.mXOff = (int)((double)Common._M(-9) * Math.Cos((double)((float)(Common._M1(1) * this.mUpdateCount) * 3.14159f / 180f)) + (double)Common._M2(2));
							this.mYOff = (int)((double)Common._M(6) * Math.Sin((double)((float)(Common._M1(2) * this.mUpdateCount) * 3.14159f / 180f)) + (double)Common._M2(3));
						}
						else if (this.mPoints.Count == 0)
						{
							if (this.mStartX > 0)
							{
								this.mX += this.mSpeed;
							}
							else
							{
								this.mY += this.mSpeed;
							}
							if (this.AtDest())
							{
								if (this.mStartX > 0)
								{
									this.mX = this.mDestX;
									this.CalcDestX();
								}
								else
								{
									this.mY = this.mDestY;
									this.CalcDestY();
								}
							}
						}
						if (!this.mStrafe && this.mMovementMode == 0 && --this.mHoverTime == 0)
						{
							if (this.mPoints.Count == 0)
							{
								this.mXOff = (this.mYOff = 0);
								if (this.mStartX > 0)
								{
									this.CalcDestX();
								}
								else
								{
									this.CalcDestY();
								}
							}
							else
							{
								this.WarpToPoint();
								this.mHoverTime = Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							}
						}
						if (this.mFireDelay > 0)
						{
							this.mFireDelay--;
						}
						if (this.mAttackDelayAfterHitFrog > 0)
						{
							this.mAttackDelayAfterHitFrog--;
						}
						bool flag3 = (GameApp.gApp.GetLevelMgr().mBossesCanAttackFuckedFrog || !this.mLevel.mFrog.IsFuckedUp()) && this.mAttackDelayAfterHitFrog == 0;
						if (this.mFireDelay == 0 && flag3 && this.CanFire())
						{
							if (this.mStrafe || this.mMovementMode != 0)
							{
								this.mFireDelay = Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
							}
							BossBullet bossBullet = null;
							BossBullet bossBullet2 = null;
							if (this.mSubType == 0)
							{
								bossBullet = this.CreateBossBullet();
								this.mBullets.Add(bossBullet);
								bossBullet.mBossShoot = this;
								bossBullet.mX = this.mX;
								bossBullet.mY = this.mY;
								this.FireBulletAtPlayer(bossBullet, Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed));
								bossBullet.mId = ++BossShoot.gLastBulletId;
							}
							else
							{
								List<int> list = new List<int>();
								if (this.mShotType == 5)
								{
									for (int j = 0; j < 5; j++)
									{
										list.Add(j);
									}
								}
								else
								{
									for (int k = 0; k < this.mMaxBulletsToFire; k++)
									{
										list.Add(this.mShotType);
									}
								}
								int l = 0;
								while (l < this.mMaxBulletsToFire)
								{
									int num3 = Common.Rand() % list.Count;
									int num4 = list[num3];
									list.RemoveAt(num3);
									l++;
									bossBullet = this.CreateBossBullet();
									this.mBullets.Add(bossBullet);
									if (bossBullet2 == null)
									{
										bossBullet2 = bossBullet;
									}
									bossBullet.mBossShoot = this;
									bossBullet.mX = this.mX;
									bossBullet.mY = this.mY;
									bossBullet.mId = ++BossShoot.gLastBulletId;
									bossBullet.mDelay = (l - 1) * this.mShotDelay;
									bossBullet.mShotType = num4;
									bossBullet.mBouncesLeft = this.mMaxShotBounces;
									if (num4 == 1 || num4 == 3)
									{
										this.FireBulletAtPlayer(bossBullet, Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed));
										bossBullet.mHoming = (num4 == 3);
										bossBullet.mTargetVX = bossBullet.mVX;
										bossBullet.mTargetVY = bossBullet.mVY;
									}
									else if (num4 == 2 && !this.FireSinusoidalBullet(bossBullet, num4 == 1))
									{
										if (bossBullet2 == bossBullet)
										{
											bossBullet2 = null;
										}
										bossBullet = null;
										this.mBullets.RemoveAt(this.mBullets.Count - 1);
									}
									else if (num4 == 0)
									{
										bossBullet.mSineMotion = false;
										if (this.mStartY > 0)
										{
											bossBullet.mVX = Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed);
											bossBullet.mVY = 0f;
											if (this.mX > (float)this.mLevel.mFrog.GetCenterX())
											{
												bossBullet.mVX *= -1f;
											}
										}
										else
										{
											bossBullet.mVX = 0f;
											bossBullet.mVY = Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed);
											if (this.mY > (float)this.mLevel.mFrog.GetCenterY())
											{
												bossBullet.mVY *= -1f;
											}
										}
									}
									else if (num4 == 4)
									{
										bossBullet.mVX = 0f;
										bossBullet.mVY = -Common.FloatRange(this.mMinBulletSpeed, this.mMinBulletSpeed);
										bossBullet.mOffscreenPause = this.mVolcanoOffscreenDelay;
										bossBullet.mVolcanoShot = true;
									}
								}
							}
							if (bossBullet != null)
							{
								bossBullet.mUpdateCount = 0;
								this.mFireDelay = Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
								if (bossBullet2.mDelay == 0)
								{
									base.PlaySound(2);
								}
								this.DidFire();
							}
						}
					}
				}
			}
			if (Common._geq(this.mAlphaOverride, 255f))
			{
				for (int m = 0; m < this.mBullets.Count; m++)
				{
					BossBullet bossBullet3 = this.mBullets[m];
					if (bossBullet3.mDeleteInstantly)
					{
						this.BossBulletDestroyed(bossBullet3, false);
						this.mBullets.RemoveAt(m);
						this.BulletErased(m);
						m--;
					}
					else if (!this.PreBulletUpdate(bossBullet3, m))
					{
						bossBullet3.mUpdateCount++;
						if (this.mStartY > 0)
						{
							bossBullet3.mVY += bossBullet3.mGravity;
						}
						else
						{
							bossBullet3.mVX += bossBullet3.mGravity;
						}
						if (!bossBullet3.mSineMotion)
						{
							float mX2 = bossBullet3.mX;
							float mY = bossBullet3.mY;
							bossBullet3.mX += bossBullet3.mVX;
							bossBullet3.mY += bossBullet3.mVY;
							if (this.mMaxShotBounces > 0)
							{
								bool flag4 = false;
								int num5 = 0;
								int num6 = 0;
								this.GetShotBounceOffs(bossBullet3, ref num5, ref num6);
								if (mY + (float)num6 < (float)Common._SS(GameApp.gApp.mHeight) && bossBullet3.mY + (float)num6 >= (float)Common._SS(GameApp.gApp.mHeight) && bossBullet3.mVY > 0f)
								{
									bossBullet3.mVY = -Math.Abs(bossBullet3.mVY);
									bossBullet3.mY = mY;
									flag4 = true;
								}
								else if (mY > (float)Common._DS(Common._M(-50)) && bossBullet3.mY <= (float)Common._DS(Common._M1(-50)) && bossBullet3.mVY < 0f)
								{
									bossBullet3.mVY = Math.Abs(bossBullet3.mVY);
									bossBullet3.mY = mY;
									flag4 = true;
								}
								if (mX2 + (float)num5 < (float)Common._SS(GameApp.gApp.mWidth) && bossBullet3.mX + (float)num5 >= (float)Common._SS(GameApp.gApp.mWidth) && bossBullet3.mVX > 0f)
								{
									bossBullet3.mVX = -Math.Abs(bossBullet3.mVX);
									bossBullet3.mX = mX2;
									flag4 = true;
								}
								else if (mX2 > (float)Common._DS(Common._M(-40)) && bossBullet3.mX <= (float)Common._DS(Common._M1(-40)) && bossBullet3.mVX < 0f)
								{
									bossBullet3.mVX = Math.Abs(bossBullet3.mVX);
									bossBullet3.mX = mX2;
									flag4 = true;
								}
								if (this.mMaxShotBounces > 0 && bossBullet3.mBouncesLeft <= 0)
								{
									goto IL_10C6;
								}
								if (flag4)
								{
									bossBullet3.mTargetVX = bossBullet3.mVX;
									bossBullet3.mTargetVY = bossBullet3.mVY;
									bossBullet3.mBouncesLeft--;
									this.ShotBounced(bossBullet3);
								}
								if (bossBullet3.mBouncesLeft <= 0)
								{
									goto IL_10C6;
								}
							}
						}
						else if (this.mStartX > 0)
						{
							bossBullet3.mX += bossBullet3.mAmp * (float)Math.Cos((double)((float)bossBullet3.mUpdateCount * bossBullet3.mFreq));
							bossBullet3.mY += bossBullet3.mVY;
						}
						else
						{
							bossBullet3.mY += bossBullet3.mAmp * (float)Math.Cos((double)((float)bossBullet3.mUpdateCount * bossBullet3.mFreq));
							bossBullet3.mX += bossBullet3.mVX;
						}
						if (bossBullet3.mHoming && bossBullet3.mY < (float)this.mLevel.mFrogY[0])
						{
							float num7 = 0f;
							this.GetTargetedVelocity(bossBullet3.mInitialSpeed, bossBullet3.mX, bossBullet3.mY, ref bossBullet3.mTargetVX, ref num7);
						}
						if (!Common._eq(bossBullet3.mVX, bossBullet3.mTargetVX))
						{
							bool flag5 = bossBullet3.mVX < bossBullet3.mTargetVX;
							bossBullet3.mVX += this.mHomingCorrectionAmt * (float)((bossBullet3.mVX < bossBullet3.mTargetVX) ? 1 : -1);
							if ((flag5 && bossBullet3.mVX > bossBullet3.mTargetVX) || (!flag5 && bossBullet3.mVX < bossBullet3.mTargetVX))
							{
								bossBullet3.mVX = bossBullet3.mTargetVX;
							}
						}
						Rect bulletRect = this.GetBulletRect(bossBullet3);
						if (this.CheckBulletHitPlayer(bossBullet3))
						{
							this.mAttackDelayAfterHitFrog = GameApp.gApp.GetLevelMgr().mAttackDelayAfterHittingFrog;
							this.mHulaAmnesty = this.mAttackDelayAfterHitFrog;
							if (this.mFrogStunTime > 0)
							{
								this.mLevel.mFrog.Stun(this.mFrogStunTime);
							}
							else if (this.mFrogPoisonTime > 0)
							{
								this.mLevel.mFrog.Poison(this.mFrogPoisonTime);
							}
							else if (this.mFrogHallucinateTime > 0)
							{
								this.mLevel.mBoard.SetHallucinateTimer(this.mFrogHallucinateTime);
							}
							else if (this.mFrogSlowTimer > 0)
							{
								this.mLevel.mFrog.SetSlowTimer(this.mFrogSlowTimer);
								this.AppliedSlowTimer();
							}
							else if (this.mMinSpots > 0 && this.mMaxSpots > 0 && this.mMinSpotRad > 0 && this.mMaxSpotRad > 0)
							{
								for (int n = 0; n < this.mLevel.mNumCurves; n++)
								{
									this.mLevel.mCurveMgr[n].AddInkSpots(Common.IntRange(this.mMinSpots, this.mMaxSpots), (float)this.mMinSpotRad, (float)this.mMaxSpotRad, this.mMinSpotFade, this.mMaxSpotFade, this.mSpotFadeDelay, this.mInkTargetMode);
								}
							}
							this.BulletHitPlayer(bossBullet3);
							this.BossBulletDestroyed(bossBullet3, false);
							this.mBullets.RemoveAt(m);
							this.BulletErased(m);
							m--;
						}
						else if ((!bulletRect.Intersects(new Rect(0, 0, Common._SS(GameApp.gApp.mWidth), Common._SS(GameApp.gApp.mHeight + 200))) && !bossBullet3.mVolcanoShot && this.mMaxShotBounces == 0) || (bossBullet3.mVolcanoShot && bossBullet3.mY > (float)(Common._SS(GameApp.gApp.mHeight) + 300)))
						{
							this.BossBulletDestroyed(bossBullet3, true);
							this.mBullets.RemoveAt(m);
							this.BulletErased(m);
							m--;
						}
					}
					IL_10C6:;
				}
				if (this.mEndHoverCountdown > 0 && --this.mEndHoverCountdown == 0)
				{
					this.WarpToPoint();
					this.mHoverTime = Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
				}
				if (this.mEnrageDelayTimer > 0)
				{
					this.mEnrageDelayTimer--;
				}
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00084489 File Offset: 0x00082689
		public override void Update()
		{
			this.Update(1f);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00084498 File Offset: 0x00082698
		public override void Draw(SexyGraphics g)
		{
			base.Draw(g);
			if (this.mTeleportDir != 0)
			{
				int num = Common._S(this.mWidth + Common._M(200));
				g.PushState();
				int num2 = (int)(Common._S(this.mX) - (float)(num / 2));
				int num3 = (int)(Common._S(this.mY) - (float)(Common._S(this.mHeight + Common._M(10)) / 2));
				int num4;
				if (this.mTeleportDir == -1)
				{
					num4 = (int)((float)Common._S(this.mHeight) - (float)Common._S(this.mHeight) * this.mTeleportPct);
				}
				else
				{
					num4 = (int)((float)Common._S(this.mHeight) * this.mTeleportPct);
				}
				g.ClipRect(num2, num3, num, num4);
			}
			if (this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
			this.DrawBossSpecificArt(g);
			if (!this.mStrafe && this.mHoverTime <= Common._M(100) && this.mHoverTime > 0 && this.mPoints.Count > 1 && this.mHoverTime / Common._M1(10) % 2 == 0)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, Common._M(128));
				g.SetDrawMode(1);
				this.DrawBossSpecificArt(g);
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mDoExplosion && this.mShouldDoDeathExplosions)
			{
				this.mHitEffect.mDrawTransform.LoadIdentity();
				float num5 = GameApp.DownScaleNum(1f);
				this.mHitEffect.mDrawTransform.Scale(num5, num5);
				this.mHitEffect.mDrawTransform.Translate(Common._S(this.mX) + (float)Common._S(Common._M(9)), Common._S(this.mY) + (float)Common._S(this.mHitEffectYOff));
				this.mHitEffect.Draw(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			if (this.mDrawHeartsBelowMisc && !this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
			this.DrawMisc(g);
			if (!this.mDrawHeartsBelowMisc && !this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000846C4 File Offset: 0x000828C4
		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncBoolean(ref this.mPauseShieldRegen);
			sync.SyncLong(ref this.mCurrentLocPoint);
			sync.SyncFloat(ref this.mSpeed);
			sync.SyncFloat(ref this.mDDefaultSpeed.value);
			sync.SyncFloat(ref this.mDMaxXInc.value);
			sync.SyncFloat(ref this.mDMinXInc.value);
			sync.SyncFloat(ref this.mDMaxYInc.value);
			sync.SyncFloat(ref this.mDMinYInc.value);
			sync.SyncFloat(ref this.mDMinAmp.value);
			sync.SyncFloat(ref this.mDMaxAmp.value);
			sync.SyncFloat(ref this.mDMinFreq.value);
			sync.SyncFloat(ref this.mDMaxFreq.value);
			sync.SyncFloat(ref this.mDHomingCorrectionAmt.value);
			sync.SyncLong(ref this.mDFrogSlowTimer.value);
			sync.SyncLong(ref this.mBombAppearDelay);
			sync.SyncLong(ref this.mEndHoverCountdown);
			sync.SyncLong(ref this.mEnrageDelayTimer);
			sync.SyncLong(ref BossShoot.gLastBulletId);
			sync.SyncBoolean(ref this.mPauseMovement);
			sync.SyncLong(ref this.mAttackDelayAfterHitFrog);
			sync.SyncLong(ref this.mTeleportDir);
			sync.SyncFloat(ref this.mTeleportPct);
			sync.SyncLong(ref this.mTeleportTime);
			sync.SyncLong(ref this.mVolcanoOffscreenDelay);
			sync.SyncLong(ref this.mDMovementMode.value);
			sync.SyncFloat(ref this.mDMovementAccel.value);
			sync.SyncLong(ref this.mDDefaultMovementUpdateDelay.value);
			sync.SyncLong(ref this.mMovementUpdateDelay);
			sync.SyncFloat(ref this.mTargetDestX);
			sync.SyncLong(ref this.mDBallShieldDamage.value);
			sync.SyncLong(ref this.mDShieldHP.value);
			sync.SyncBoolean(ref this.mDUseShield.value);
			sync.SyncFloat(ref this.mDShieldRotateSpeed.value);
			sync.SyncLong(ref this.mDShieldPauseTime.value);
			sync.SyncFloat(ref this.mShieldAngle);
			for (int i = 0; i < 4; i++)
			{
				sync.SyncLong(ref this.mShieldQuadrant[i].mTimer);
				sync.SyncLong(ref this.mShieldQuadrant[i].mHP);
			}
			sync.SyncLong(ref this.mCurShieldPauseTime);
			sync.SyncFloat(ref this.mShieldTargetAngle);
			sync.SyncLong(ref this.mDShieldQuadRespawnTime.value);
			Buffer buffer = sync.GetBuffer();
			if (sync.isRead())
			{
				this.mPoints.Clear();
				int num = (int)buffer.ReadLong();
				for (int j = 0; j < num; j++)
				{
					int num2 = (int)buffer.ReadLong();
					int num3 = (int)buffer.ReadLong();
					this.mPoints.Add(new Point(num2, num3));
				}
			}
			else
			{
				buffer.WriteLong((long)this.mPoints.Count);
				for (int k = 0; k < this.mPoints.Count; k++)
				{
					buffer.WriteLong((long)this.mPoints[k].mX);
					buffer.WriteLong((long)this.mPoints[k].mY);
				}
			}
			sync.SyncLong(ref this.mStartX);
			sync.SyncLong(ref this.mEndX);
			sync.SyncLong(ref this.mStartY);
			sync.SyncLong(ref this.mEndY);
			sync.SyncLong(ref this.mDColorVampChanceToMatch2ndBall.value);
			sync.SyncLong(ref this.mDShotType.value);
			sync.SyncLong(ref this.mDMinSineShotTime.value);
			sync.SyncLong(ref this.mDMaxSineShotTime.value);
			sync.SyncLong(ref this.mDMaxBulletsToFire.value);
			sync.SyncLong(ref this.mDMaxRetaliationBullets.value);
			sync.SyncFloat(ref this.mDMinBulletSpeed.value);
			sync.SyncFloat(ref this.mDMaxBulletSpeed.value);
			sync.SyncFloat(ref this.mDMinRetalSpeed.value);
			sync.SyncFloat(ref this.mDMaxRetalSpeed.value);
			sync.SyncLong(ref this.mDMinColorChangeTime.value);
			sync.SyncLong(ref this.mDMaxColorChangeTime.value);
			sync.SyncLong(ref this.mDFrogStunTime.value);
			sync.SyncLong(ref this.mDShotDelay.value);
			sync.SyncLong(ref this.mDColorVampHealthInc.value);
			sync.SyncFloat(ref this.mDestY);
			sync.SyncFloat(ref this.mDestX);
			sync.SyncLong(ref this.mHoverTime);
			sync.SyncLong(ref this.mFireDelay);
			sync.SyncLong(ref this.mXOff);
			sync.SyncLong(ref this.mYOff);
			sync.SyncLong(ref this.mDMinHoverTime.value);
			sync.SyncLong(ref this.mDMaxHoverTime.value);
			sync.SyncLong(ref this.mDMinFireDelay.value);
			sync.SyncLong(ref this.mDMaxFireDelay.value);
			sync.SyncLong(ref this.mColorChangeTimer);
			sync.SyncLong(ref this.mColorVampShotType);
			sync.SyncLong(ref this.mMaxShotIncCounter);
			sync.SyncLong(ref this.mRetalShotIncCounter);
			sync.SyncLong(ref this.mDMaxBulletsToFire.value);
			sync.SyncLong(ref this.mDMaxRetaliationBullets.value);
			this.SyncListBossBullets(sync, this.mBullets, true);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00084C04 File Offset: 0x00082E04
		private void SyncListBossBullets(DataSync sync, List<BossBullet> theList, bool clear)
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
					BossBullet bossBullet = new BossBullet();
					bossBullet.SyncState(sync);
					theList.Add(bossBullet);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (BossBullet bossBullet2 in theList)
			{
				bossBullet2.SyncState(sync);
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00084CA4 File Offset: 0x00082EA4
		public override Boss Instantiate()
		{
			BossShoot bossShoot = new BossShoot(this.mLevel);
			bossShoot.CopyFrom(this);
			bossShoot.mBullets.Clear();
			return bossShoot;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00084CD0 File Offset: 0x00082ED0
		public override bool Collides(Bullet b)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (this.mUseShield && CommonMath.CircleCircleIntersection(this.mX, this.mY, (float)this.mShieldRadius, b.GetX(), b.GetY(), (float)b.GetRadius(), ref num, ref num2, ref num3, ref num4))
			{
				float num5 = Common.AngleBetweenPoints(num, num2, this.mX, this.mY) + 3.14159f;
				float num6 = Common.AngleBetweenPoints(num3, num4, this.mX, this.mY) + 3.14159f;
				for (int i = 0; i < 4; i++)
				{
					if (this.mShieldQuadrant[i].mTimer <= 0)
					{
						float num7 = this.mShieldAngle + (float)i * 3.14159f / 2f;
						float num8 = this.mShieldAngle + (float)(i + 1) * 3.14159f / 2f;
						if (num7 > 6.28318f && num8 > 6.28318f)
						{
							num7 = Common.GetCanonicalAngleRad(num7);
							num8 = Common.GetCanonicalAngleRad(num8);
						}
						if (num7 > num8)
						{
							float num9 = num7;
							num7 = num8;
							num8 = num9;
						}
						if ((num5 >= num7 && num5 <= num8) || (num6 >= num7 && num6 <= num8))
						{
							this.ShieldQuadrantHit(i);
							if (this.mBallShieldDamage > 0 && --this.mShieldQuadrant[i].mHP == 0)
							{
								this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
								this.mShieldQuadrant[i].mHP = this.mShieldHP;
								base.PlaySound(9);
							}
							return true;
						}
					}
				}
			}
			float num10 = (float)b.GetRadius() * Common._M(0.75f);
			new Rect((int)(b.GetX() - num10), (int)(b.GetY() - num10), (int)(num10 * 2f), (int)(num10 * 2f));
			bool flag = this.AllowFrogToFire() && this.BulletIntersectsBoss(b);
			if (this.mColorVampire && flag && this.mAvoidColor && b.GetColorType() == this.mColorVampShotType)
			{
				if (this.mColorVampHealthInc > 0)
				{
					this.mHP += (float)this.mColorVampHealthInc;
					if (this.mHP > this.mMaxHP)
					{
						this.mHP = this.mMaxHP;
					}
					int num11 = this.mColorVampHealthIncPerHit;
					for (int j = Boss.NUM_HEARTS - 1; j >= 0; j--)
					{
						if (this.mHeartCels[j] > 0)
						{
							int num12 = this.mHeartCels[j];
							if ((this.mHeartCels[j] -= num11) >= 0)
							{
								break;
							}
							this.mHeartCels[j] = 0;
							num11 -= num12;
						}
					}
				}
				return true;
			}
			return (this.mColorVampire && flag && !this.mAvoidColor && b.GetColorType() != this.mColorVampShotType) || base.Collides(b);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00084FCC File Offset: 0x000831CC
		public override void ProximityBombActivated(float x, float y, int radius)
		{
			if (this.mUseShield)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				bool flag = CommonMath.CircleCircleIntersection(this.mX, this.mY, (float)this.mProxBombRadius, x, y, (float)(radius + Common.GetDefaultBallRadius()), ref num, ref num2, ref num3, ref num4);
				if (flag)
				{
					float num5 = Common.AngleBetweenPoints(num, num2, this.mX, this.mY) + 3.14159f;
					float num6 = Common.AngleBetweenPoints(num3, num4, this.mX, this.mY) + 3.14159f;
					for (int i = 0; i < 4; i++)
					{
						if (this.mShieldQuadrant[i].mTimer <= 50)
						{
							float num7 = this.mShieldAngle + (float)i * 3.14159f / 2f;
							float num8 = this.mShieldAngle + (float)(i + 1) * 3.14159f / 2f;
							if (num7 > 6.28318f && num8 > 6.28318f)
							{
								num7 = Common.GetCanonicalAngleRad(num7);
								num8 = Common.GetCanonicalAngleRad(num8);
							}
							if (num7 > num8)
							{
								float num9 = num7;
								num7 = num8;
								num8 = num9;
							}
							if ((num5 >= num7 && num5 <= num8) || (num6 >= num7 && num6 <= num8))
							{
								this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
								this.mShieldQuadrant[i].mHP = this.mShieldHP;
								this.QuadHitByProxBomb(i);
							}
						}
					}
				}
			}
			base.ProximityBombActivated(x, y, radius);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0008514C File Offset: 0x0008334C
		public override void FrogInitialized(Gun g)
		{
			base.FrogInitialized(g);
			if (this.mMovementMode != 0)
			{
				this.mTargetDestX = (this.mDestX = (this.mX = (float)(Common._SS(GameApp.gApp.mWidth) / 2 - GameApp.gApp.mBoardOffsetX)));
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000851A0 File Offset: 0x000833A0
		public virtual void DisableShields()
		{
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
				this.mShieldQuadrant[i].mHP = this.mShieldHP;
			}
		}

		// Token: 0x0400149D RID: 5277
		public Transform mGlobalTranform = new Transform();

		// Token: 0x0400149E RID: 5278
		protected static int gLastBulletId = 0;

		// Token: 0x0400149F RID: 5279
		protected static int PATROL_FLASH_TIMER = 10;

		// Token: 0x040014A0 RID: 5280
		protected static int PATROL_FLASH_COUNT = 20;

		// Token: 0x040014A1 RID: 5281
		protected static int DEFAULT_BULLET_SIZE = 40;

		// Token: 0x040014A2 RID: 5282
		protected static int gVolcanoBulletCounter = 0;

		// Token: 0x040014A3 RID: 5283
		protected ParamData<int> mDColorVampChanceToMatch2ndBall = new ParamData<int>();

		// Token: 0x040014A4 RID: 5284
		protected ParamData<int> mDFrogStunTime = new ParamData<int>();

		// Token: 0x040014A5 RID: 5285
		protected ParamData<int> mDFrogPoisonTime = new ParamData<int>();

		// Token: 0x040014A6 RID: 5286
		protected ParamData<int> mDFrogHallucinateTime = new ParamData<int>();

		// Token: 0x040014A7 RID: 5287
		protected ParamData<int> mDFrogSlowTimer = new ParamData<int>();

		// Token: 0x040014A8 RID: 5288
		protected ParamData<int> mDShotDelay = new ParamData<int>();

		// Token: 0x040014A9 RID: 5289
		protected ParamData<float> mDFlightSpeed = new ParamData<float>();

		// Token: 0x040014AA RID: 5290
		protected ParamData<int> mDFlightMinDist = new ParamData<int>();

		// Token: 0x040014AB RID: 5291
		protected ParamData<int> mDColorVampHealthInc = new ParamData<int>();

		// Token: 0x040014AC RID: 5292
		protected ParamData<int> mDMinColorChangeTime = new ParamData<int>();

		// Token: 0x040014AD RID: 5293
		protected ParamData<int> mDMaxColorChangeTime = new ParamData<int>();

		// Token: 0x040014AE RID: 5294
		protected ParamData<float> mDHomingCorrectionAmt = new ParamData<float>();

		// Token: 0x040014AF RID: 5295
		protected ParamData<int> mDMinHoverTime = new ParamData<int>();

		// Token: 0x040014B0 RID: 5296
		protected ParamData<int> mDMaxHoverTime = new ParamData<int>();

		// Token: 0x040014B1 RID: 5297
		protected ParamData<int> mDMinFireDelay = new ParamData<int>();

		// Token: 0x040014B2 RID: 5298
		protected ParamData<int> mDMaxFireDelay = new ParamData<int>();

		// Token: 0x040014B3 RID: 5299
		protected ParamData<float> mDMinBulletSpeed = new ParamData<float>();

		// Token: 0x040014B4 RID: 5300
		protected ParamData<float> mDMaxBulletSpeed = new ParamData<float>();

		// Token: 0x040014B5 RID: 5301
		protected ParamData<int> mDMaxBulletsToFire = new ParamData<int>();

		// Token: 0x040014B6 RID: 5302
		protected ParamData<int> mDMaxRetaliationBullets = new ParamData<int>();

		// Token: 0x040014B7 RID: 5303
		protected ParamData<int> mDMinSineShotTime = new ParamData<int>();

		// Token: 0x040014B8 RID: 5304
		protected ParamData<int> mDMaxSineShotTime = new ParamData<int>();

		// Token: 0x040014B9 RID: 5305
		protected ParamData<float> mDMinAmp = new ParamData<float>();

		// Token: 0x040014BA RID: 5306
		protected ParamData<float> mDMaxAmp = new ParamData<float>();

		// Token: 0x040014BB RID: 5307
		protected ParamData<float> mDMinFreq = new ParamData<float>();

		// Token: 0x040014BC RID: 5308
		protected ParamData<float> mDMaxFreq = new ParamData<float>();

		// Token: 0x040014BD RID: 5309
		protected ParamData<float> mDMaxYInc = new ParamData<float>();

		// Token: 0x040014BE RID: 5310
		protected ParamData<float> mDMinYInc = new ParamData<float>();

		// Token: 0x040014BF RID: 5311
		protected ParamData<float> mDMaxXInc = new ParamData<float>();

		// Token: 0x040014C0 RID: 5312
		protected ParamData<float> mDMinXInc = new ParamData<float>();

		// Token: 0x040014C1 RID: 5313
		protected ParamData<float> mDDefaultSpeed = new ParamData<float>();

		// Token: 0x040014C2 RID: 5314
		protected ParamData<bool> mDStrafe = new ParamData<bool>();

		// Token: 0x040014C3 RID: 5315
		protected ParamData<bool> mDEndHoverOnHit = new ParamData<bool>();

		// Token: 0x040014C4 RID: 5316
		protected ParamData<float> mDMinRetalSpeed = new ParamData<float>();

		// Token: 0x040014C5 RID: 5317
		protected ParamData<float> mDMaxRetalSpeed = new ParamData<float>();

		// Token: 0x040014C6 RID: 5318
		protected ParamData<int> mDShotType = new ParamData<int>();

		// Token: 0x040014C7 RID: 5319
		protected ParamData<int> mDTeleportMinTime = new ParamData<int>();

		// Token: 0x040014C8 RID: 5320
		protected ParamData<int> mDTeleportMaxTime = new ParamData<int>();

		// Token: 0x040014C9 RID: 5321
		protected ParamData<float> mDMovementAccel = new ParamData<float>();

		// Token: 0x040014CA RID: 5322
		protected ParamData<int> mDDefaultMovementUpdateDelay = new ParamData<int>();

		// Token: 0x040014CB RID: 5323
		protected ParamData<int> mDMovementMode = new ParamData<int>();

		// Token: 0x040014CC RID: 5324
		protected ParamData<bool> mDUseShield = new ParamData<bool>();

		// Token: 0x040014CD RID: 5325
		protected ParamData<float> mDShieldRotateSpeed = new ParamData<float>();

		// Token: 0x040014CE RID: 5326
		protected ParamData<int> mDShieldQuadRespawnTime = new ParamData<int>();

		// Token: 0x040014CF RID: 5327
		protected ParamData<int> mDShieldPauseTime = new ParamData<int>();

		// Token: 0x040014D0 RID: 5328
		protected ParamData<int> mDShieldHP = new ParamData<int>();

		// Token: 0x040014D1 RID: 5329
		protected ParamData<int> mDBallShieldDamage = new ParamData<int>();

		// Token: 0x040014D2 RID: 5330
		protected int mHitEffectYOff;

		// Token: 0x040014D3 RID: 5331
		protected bool mPauseMovement;

		// Token: 0x040014D4 RID: 5332
		protected bool mPauseShieldRegen;

		// Token: 0x040014D5 RID: 5333
		protected bool mDrawHeartsBelowMisc;

		// Token: 0x040014D6 RID: 5334
		protected bool mDrawHeartsBelowBoss;

		// Token: 0x040014D7 RID: 5335
		protected List<BossBerserkMovement> mBerserkMovementVec = new List<BossBerserkMovement>();

		// Token: 0x040014D8 RID: 5336
		protected List<BossBullet> mBullets = new List<BossBullet>();

		// Token: 0x040014D9 RID: 5337
		protected float mDestX;

		// Token: 0x040014DA RID: 5338
		protected float mDestY;

		// Token: 0x040014DB RID: 5339
		protected float mTargetDestX;

		// Token: 0x040014DC RID: 5340
		protected float mShieldAngle;

		// Token: 0x040014DD RID: 5341
		protected float mShieldTargetAngle;

		// Token: 0x040014DE RID: 5342
		protected ShieldQuadrant[] mShieldQuadrant = new ShieldQuadrant[]
		{
			new ShieldQuadrant(),
			new ShieldQuadrant(),
			new ShieldQuadrant(),
			new ShieldQuadrant()
		};

		// Token: 0x040014DF RID: 5343
		protected int mShieldRadius;

		// Token: 0x040014E0 RID: 5344
		protected int mHoverTime;

		// Token: 0x040014E1 RID: 5345
		protected int mFireDelay;

		// Token: 0x040014E2 RID: 5346
		protected int mXOff;

		// Token: 0x040014E3 RID: 5347
		protected int mYOff;

		// Token: 0x040014E4 RID: 5348
		protected int mCurShieldPauseTime;

		// Token: 0x040014E5 RID: 5349
		protected int mEndHoverCountdown;

		// Token: 0x040014E6 RID: 5350
		protected int mEnrageDelayTimer;

		// Token: 0x040014E7 RID: 5351
		protected int mMovementUpdateDelay;

		// Token: 0x040014E8 RID: 5352
		protected int mAttackDelayAfterHitFrog;

		// Token: 0x040014E9 RID: 5353
		public List<Point> mPoints = new List<Point>();

		// Token: 0x040014EA RID: 5354
		public int mCurrentLocPoint;

		// Token: 0x040014EB RID: 5355
		public int mMaxShotBounces;

		// Token: 0x040014EC RID: 5356
		public int mTeleportDir;

		// Token: 0x040014ED RID: 5357
		public float mTeleportPct;

		// Token: 0x040014EE RID: 5358
		public int mTeleportTime;

		// Token: 0x040014EF RID: 5359
		public int mEnrageDelay;

		// Token: 0x040014F0 RID: 5360
		public int mBombAppearDelay;

		// Token: 0x040014F1 RID: 5361
		public int mRetalShotDelay;

		// Token: 0x040014F2 RID: 5362
		public int mStartX;

		// Token: 0x040014F3 RID: 5363
		public int mStartY;

		// Token: 0x040014F4 RID: 5364
		public int mEndX;

		// Token: 0x040014F5 RID: 5365
		public int mEndY;

		// Token: 0x040014F6 RID: 5366
		public int mDecMinHover;

		// Token: 0x040014F7 RID: 5367
		public int mDecMaxHover;

		// Token: 0x040014F8 RID: 5368
		public int mDecMinFire;

		// Token: 0x040014F9 RID: 5369
		public int mDecMaxFire;

		// Token: 0x040014FA RID: 5370
		public int mSubType;

		// Token: 0x040014FB RID: 5371
		public int mColorVampShotType;

		// Token: 0x040014FC RID: 5372
		public int mColorChangeTimer;

		// Token: 0x040014FD RID: 5373
		public int mColorVampHealthIncPerHit;

		// Token: 0x040014FE RID: 5374
		public int mIncMaxShotHealthAmt;

		// Token: 0x040014FF RID: 5375
		public int mIncRetalMaxShotHealthAmt;

		// Token: 0x04001500 RID: 5376
		public int mMaxShotIncCounter;

		// Token: 0x04001501 RID: 5377
		public int mRetalShotIncCounter;

		// Token: 0x04001502 RID: 5378
		public int mMinSpots;

		// Token: 0x04001503 RID: 5379
		public int mMaxSpots;

		// Token: 0x04001504 RID: 5380
		public int mMinSpotRad;

		// Token: 0x04001505 RID: 5381
		public int mMaxSpotRad;

		// Token: 0x04001506 RID: 5382
		public float mMinSpotFade;

		// Token: 0x04001507 RID: 5383
		public float mMaxSpotFade;

		// Token: 0x04001508 RID: 5384
		public int mInkTargetMode;

		// Token: 0x04001509 RID: 5385
		public int mSpotFadeDelay;

		// Token: 0x0400150A RID: 5386
		public int mBulletRadius;

		// Token: 0x0400150B RID: 5387
		public bool mSinusoidalRetaliation;

		// Token: 0x0400150C RID: 5388
		public bool mCanShootBullets;

		// Token: 0x0400150D RID: 5389
		public bool mSineShotsTargetPlayer;

		// Token: 0x0400150E RID: 5390
		public bool mColorVampire;

		// Token: 0x0400150F RID: 5391
		public bool mAvoidColor;

		// Token: 0x04001510 RID: 5392
		public bool mEnrageShieldRestore;

		// Token: 0x04001511 RID: 5393
		public bool mBulletsUseSphereColl;

		// Token: 0x04001512 RID: 5394
		public float mSpeed;

		// Token: 0x020000D4 RID: 212
		public enum Move
		{
			// Token: 0x040017A7 RID: 6055
			Move_Default,
			// Token: 0x040017A8 RID: 6056
			Move_MirrorPlayer,
			// Token: 0x040017A9 RID: 6057
			Move_OppositePlayer
		}

		// Token: 0x020000D5 RID: 213
		public enum ShotType
		{
			// Token: 0x040017AB RID: 6059
			ShotType_Straight,
			// Token: 0x040017AC RID: 6060
			ShotType_TargetedLinear,
			// Token: 0x040017AD RID: 6061
			ShotType_Sine,
			// Token: 0x040017AE RID: 6062
			ShotType_Homing,
			// Token: 0x040017AF RID: 6063
			ShotType_Volcano,
			// Token: 0x040017B0 RID: 6064
			ShotType_Any
		}

		// Token: 0x020000D6 RID: 214
		public enum SubType
		{
			// Token: 0x040017B2 RID: 6066
			SubType_SingleShot,
			// Token: 0x040017B3 RID: 6067
			SubType_MultiShot
		}
	}
}
