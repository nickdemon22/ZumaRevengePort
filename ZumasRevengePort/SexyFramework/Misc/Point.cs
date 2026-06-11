using System;

namespace SexyFramework.Misc
{
	// Token: 0x0200013A RID: 314
	public class Point
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x00035058 File Offset: 0x00033258
		public Point(int theX, int theY)
		{
			this.mX = theX;
			this.mY = theY;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0003506E File Offset: 0x0003326E
		public Point(Point theTPoint)
		{
			this.mX = theTPoint.mX;
			this.mY = theTPoint.mY;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0003508E File Offset: 0x0003328E
		public Point()
		{
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00035096 File Offset: 0x00033296
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000350A0 File Offset: 0x000332A0
		public override bool Equals(object obj)
		{
			Point point;
			return obj != null && (point = (obj as Point)) != null && point.mX == this.mX && point.mY == this.mY;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000350E2 File Offset: 0x000332E2
		public static bool operator ==(Point ImpliedObject, Point p)
		{
			if (ImpliedObject is null)
			{
				return p is null;
			}
			return ImpliedObject.Equals(p);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000350F3 File Offset: 0x000332F3
		public static bool operator !=(Point ImpliedObject, Point p)
		{
			return !(ImpliedObject == p);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000350FF File Offset: 0x000332FF
		public int Magnitude()
		{
			return (int)Math.Sqrt((double)(this.mX * this.mX + this.mY * this.mY));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00035123 File Offset: 0x00033323
		public static Point operator +(Point ImpliedObject, Point p)
		{
			ImpliedObject.mX += p.mX;
			ImpliedObject.mY += p.mY;
			return ImpliedObject;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0003514C File Offset: 0x0003334C
		public static Point operator -(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX - p.mX, ImpliedObject.mY - p.mY);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003516D File Offset: 0x0003336D
		public static Point operator *(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX * p.mX, ImpliedObject.mY * p.mY);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0003518E File Offset: 0x0003338E
		public static Point operator /(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX / p.mX, ImpliedObject.mY / p.mY);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000351AF File Offset: 0x000333AF
		public static Point operator *(Point ImpliedObject, int s)
		{
			return new Point(ImpliedObject.mX * s, ImpliedObject.mY * s);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000351C6 File Offset: 0x000333C6
		public static Point operator *(Point ImpliedObject, double s)
		{
			return new Point((int)((double)ImpliedObject.mX * s), (int)((double)ImpliedObject.mY * s));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000351E1 File Offset: 0x000333E1
		public static Point operator *(Point ImpliedObject, float s)
		{
			return new Point((int)((float)ImpliedObject.mX * s), (int)((float)ImpliedObject.mY * s));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000351FC File Offset: 0x000333FC
		public static Point operator /(Point ImpliedObject, float s)
		{
			return new Point((int)((float)ImpliedObject.mX / s), (int)((float)ImpliedObject.mY / s));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00035217 File Offset: 0x00033417
		public static Point operator /(Point ImpliedObject, double s)
		{
			return new Point((int)((double)ImpliedObject.mX / s), (int)((double)ImpliedObject.mY / s));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00035232 File Offset: 0x00033432
		public static Point operator /(Point ImpliedObject, int s)
		{
			return new Point(ImpliedObject.mX / s, ImpliedObject.mY / s);
		}

		// Token: 0x0400091F RID: 2335
		public int mX;

		// Token: 0x04000920 RID: 2336
		public int mY;
	}
}
