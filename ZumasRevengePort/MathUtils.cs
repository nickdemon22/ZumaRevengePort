using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200004A RID: 74
	public static class MathUtils
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x0005642A File Offset: 0x0005462A
		public static int SafeRand()
		{
			return MathUtils.mRandomGen.Next();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00056436 File Offset: 0x00054636
		public static int Rand(int range)
		{
			return MathUtils.mRandomGen.Next() % range;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00056444 File Offset: 0x00054644
		public static void Seed(int seed)
		{
			MathUtils.mRandomGen = new Random(seed);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00056451 File Offset: 0x00054651
		public static int Rand()
		{
			return MathUtils.mRandomGen.Next();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0005645D File Offset: 0x0005465D
		public static float RadiansToDegrees(float pRads)
		{
			return pRads * 57.29694f;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00056466 File Offset: 0x00054666
		public static float DegreesToRadians(float pDegs)
		{
			return pDegs * 0.017452938f;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0005646F File Offset: 0x0005466F
		public static int Sign(int val)
		{
			if (val >= 0)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00056478 File Offset: 0x00054678
		public static float Sign(float val)
		{
			if (val >= 0f)
			{
				return 1f;
			}
			return -1f;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0005648D File Offset: 0x0005468D
		public static bool _eq(float n1, float n2, float tolerance)
		{
			return Math.Abs(n1 - n2) <= tolerance;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0005649D File Offset: 0x0005469D
		public static bool _leq(float n1, float n2, float tolerance)
		{
			return MathUtils._eq(n1, n2, tolerance) || n1 < n2;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000564AF File Offset: 0x000546AF
		public static bool _geq(float n1, float n2, float tolerance)
		{
			return MathUtils._eq(n1, n2, tolerance) || n1 > n2;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000564C1 File Offset: 0x000546C1
		public static bool _eq(float n1, float n2)
		{
			return Math.Abs(n1 - n2) <= float.Epsilon;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000564D5 File Offset: 0x000546D5
		public static bool _leq(float n1, float n2)
		{
			return MathUtils._eq(n1, n2, float.Epsilon) || n1 < n2;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000564EB File Offset: 0x000546EB
		public static bool _geq(float n1, float n2)
		{
			return MathUtils._eq(n1, n2, float.Epsilon) || n1 > n2;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00056501 File Offset: 0x00054701
		public static int IntRange(int min_val, int max_val)
		{
			if (min_val == max_val)
			{
				return min_val;
			}
			if (min_val < 0 && max_val < 0)
			{
				return min_val + MathUtils.SafeRand() % (Math.Abs(min_val) - Math.Abs(max_val));
			}
			return min_val + MathUtils.SafeRand() % (max_val - min_val + 1);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00056534 File Offset: 0x00054734
		public static float FloatRange(float min_val, float max_val)
		{
			if (min_val == max_val)
			{
				return min_val;
			}
			if (min_val < 0f && max_val < 0f)
			{
				return min_val + (float)(MathUtils.SafeRand() % (int)((Math.Abs(min_val) - Math.Abs(max_val)) * 100000000f + 1f)) / 100000000f;
			}
			return min_val + (float)(MathUtils.SafeRand() % (int)((max_val - min_val) * 100000000f + 1f)) / 100000000f;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000565A0 File Offset: 0x000547A0
		public static void Clamp(ref int value, int min_val, int max_val)
		{
			if (value < min_val)
			{
				value = min_val;
				return;
			}
			if (value > max_val)
			{
				value = max_val;
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000565B3 File Offset: 0x000547B3
		public static bool IncrementAndClamp(ref float val, float target, float inc)
		{
			val += inc;
			if (inc > 0f && val >= target)
			{
				val = target;
				return true;
			}
			if (inc < 0f && val <= target)
			{
				val = target;
				return true;
			}
			return false;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000565E0 File Offset: 0x000547E0
		public static int GetClosestPowerOf2Above(int theNum)
		{
			int i;
			for (i = 1; i < theNum; i <<= 1)
			{
			}
			return i;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000565FC File Offset: 0x000547FC
		public static bool IsPowerOf2(int theNum)
		{
			int num = 0;
			while (theNum > 0)
			{
				num += (theNum & 1);
				theNum >>= 1;
			}
			return num == 1;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00056620 File Offset: 0x00054820
		public static float Distance(Point p1, Point p2, bool sqrt)
		{
			float num = (float)(p2.mX - p1.mX);
			float num2 = (float)(p2.mY - p1.mY);
			float num3 = num * num + num2 * num2;
			if (!sqrt)
			{
				return num3;
			}
			return (float)Math.Sqrt((double)num3);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00056660 File Offset: 0x00054860
		public static float Distance(Point p1, Point p2)
		{
			return MathUtils.Distance(p1, p2, true);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0005666C File Offset: 0x0005486C
		public static float Distance(float p1x, float p1y, float p2x, float p2y, bool sqrt)
		{
			float num = p2x - p1x;
			float num2 = p2y - p1y;
			float num3 = num * num + num2 * num2;
			if (!sqrt)
			{
				return num3;
			}
			return (float)Math.Sqrt((double)num3);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00056697 File Offset: 0x00054897
		public static float Distance(float p1x, float p1y, float p2x, float p2y)
		{
			return MathUtils.Distance(p1x, p1y, p2x, p2y, true);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000566A4 File Offset: 0x000548A4
		public static bool CirclesIntersect(float x1, float y1, float x2, float y2, float total_radius, ref float seperation)
		{
			float num = x1 - x2;
			float num2 = y1 - y2;
			float num3 = num * num + num2 * num2;
			seperation = num3;
			return num3 < total_radius * total_radius;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000566D0 File Offset: 0x000548D0
		public static bool CirclesIntersect(float x1, float y1, float x2, float y2, float total_radius)
		{
			float num = 0f;
			return MathUtils.CirclesIntersect(x1, y1, x2, y2, total_radius, ref num);
		}

		// Token: 0x0400114F RID: 4431
		public const float EPSILON = 1E-06f;

		// Token: 0x04001150 RID: 4432
		public const float JL_PI = 3.1415927f;

		// Token: 0x04001151 RID: 4433
		private static Random mRandomGen = new Random();
	}
}
