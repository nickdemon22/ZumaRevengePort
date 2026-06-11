using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x0200000E RID: 14
	public class DataSync : DataSyncBase
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0002EA80 File Offset: 0x0002CC80
		public DataSync(SexyFramework.Misc.Buffer buffer, bool isRead)
		{
			this.ResetPointerTable();
			this.m_buffer = buffer;
			this.m_isRead = isRead;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0002EB11 File Offset: 0x0002CD11
		public SexyFramework.Misc.Buffer GetBuffer()
		{
			return this.m_buffer;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0002EB19 File Offset: 0x0002CD19
		public bool isRead()
		{
			return this.m_isRead;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0002EB21 File Offset: 0x0002CD21
		public bool isWrite()
		{
			return !this.isRead();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0002EB2C File Offset: 0x0002CD2C
		public void SyncBoolean(ref bool theBool)
		{
			if (this.m_isRead)
			{
				theBool = this.m_buffer.ReadBoolean();
				return;
			}
			this.m_buffer.WriteBoolean(theBool);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0002EB51 File Offset: 0x0002CD51
		public void SyncShort(ref short theInt)
		{
			if (this.m_isRead)
			{
				theInt = this.m_buffer.ReadShort();
				return;
			}
			this.m_buffer.WriteShort(theInt);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0002EB76 File Offset: 0x0002CD76
		public override void SyncLong(ref int theInt)
		{
			if (this.m_isRead)
			{
				theInt = (int)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)theInt);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0002EB9D File Offset: 0x0002CD9D
		public void SyncLong(ref uint theInt)
		{
			if (this.m_isRead)
			{
				theInt = (uint)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)((ulong)theInt));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public void SyncLong(ref ushort theInt)
		{
			if (this.m_isRead)
			{
				theInt = (ushort)this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong((long)((ulong)theInt));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0002EBEB File Offset: 0x0002CDEB
		public void SyncLong(ref long theLong)
		{
			if (this.m_isRead)
			{
				theLong = this.m_buffer.ReadLong();
				return;
			}
			this.m_buffer.WriteLong(theLong);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0002EC10 File Offset: 0x0002CE10
		public override void SyncFloat(ref float theFloat)
		{
			if (this.m_isRead)
			{
				theFloat = this.m_buffer.ReadFloat();
				return;
			}
			this.m_buffer.WriteFloat(theFloat);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0002EC38 File Offset: 0x0002CE38
		public override void SyncListInt(List<int> theList)
		{
			if (this.m_isRead)
			{
				theList.Clear();
				long num = this.m_buffer.ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					theList.Add((int)this.m_buffer.ReadLong());
					num2++;
				}
				return;
			}
			this.m_buffer.WriteLong((long)theList.Count);
			foreach (int num3 in theList)
			{
				this.m_buffer.WriteLong((long)num3);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0002ECD8 File Offset: 0x0002CED8
		public override void SyncListFloat(List<float> theList)
		{
			if (this.m_isRead)
			{
				theList.Clear();
				long num = this.m_buffer.ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					theList.Add(this.m_buffer.ReadFloat());
					num2++;
				}
				return;
			}
			this.m_buffer.WriteLong((long)theList.Count);
			foreach (float num3 in theList)
			{
				float num4 = num3;
				this.m_buffer.WriteFloat(num4);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0002ED78 File Offset: 0x0002CF78
		private void ResetPointerTable()
		{
			this.mCurPointerIndex = 2;
			this.mIntToPointerMap_CurveMgr.Clear();
			this.mIntToPointerMap_Ball.Clear();
			this.mIntToPointerMap_Bullet.Clear();
			this.mPointerToIntMap_CurveMgr.Clear();
			this.mPointerToIntMap_Ball.Clear();
			this.mPointerToIntMap_Bullet.Clear();
			this.mPointerSyncList_ReversePowerEffect.Clear();
			this.mPointerSyncList_Ball.Clear();
			this.mPointerSyncList_Bullet.Clear();
			this.mIntToPointerMap_CurveMgr.Add(0, null);
			this.mIntToPointerMap_Ball.Add(0, null);
			this.mIntToPointerMap_Bullet.Add(0, null);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0002EE18 File Offset: 0x0002D018
		public bool RegisterPointer(CurveMgr thePtr)
		{
			if (!this.mPointerToIntMap_CurveMgr.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_CurveMgr.Add(thePtr, num);
				this.mIntToPointerMap_CurveMgr.Add(num, thePtr);
				return true;
			}
			return false;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0002EE64 File Offset: 0x0002D064
		public bool RegisterPointer(Ball thePtr)
		{
			if (!this.mPointerToIntMap_Ball.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_Ball.Add(thePtr, num);
				this.mIntToPointerMap_Ball.Add(num, thePtr);
				return true;
			}
			return false;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
		public bool RegisterPointer(Bullet thePtr)
		{
			if (!this.mPointerToIntMap_Bullet.ContainsKey(thePtr))
			{
				int num = this.mCurPointerIndex++;
				this.mPointerToIntMap_Bullet.Add(thePtr, num);
				this.mIntToPointerMap_Bullet.Add(num, thePtr);
				return true;
			}
			return false;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0002EEFA File Offset: 0x0002D0FA
		public void SyncPointer(ReversePowerEffect thePtr)
		{
			this.mPointerSyncList_ReversePowerEffect.Add(thePtr);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0002EF08 File Offset: 0x0002D108
		public void SyncPointer(Ball thePtr)
		{
			this.mPointerSyncList_Ball.Add(thePtr);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0002EF16 File Offset: 0x0002D116
		public void SyncPointer(Bullet thePtr)
		{
			this.mPointerSyncList_Bullet.Add(thePtr);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0002EF24 File Offset: 0x0002D124
		public void SyncPointers()
		{
			if (this.m_isRead)
			{
				foreach (ReversePowerEffect reversePowerEffect in this.mPointerSyncList_ReversePowerEffect)
				{
					int num = (int)this.m_buffer.ReadLong();
					reversePowerEffect.mCurve = this.mIntToPointerMap_CurveMgr[num];
				}
				foreach (Ball ball in this.mPointerSyncList_Ball)
				{
					int num2 = (int)this.m_buffer.ReadLong();
					ball.mBullet = this.mIntToPointerMap_Bullet[num2];
				}
				using (List<Bullet>.Enumerator enumerator3 = this.mPointerSyncList_Bullet.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Bullet bullet = enumerator3.Current;
						int num3 = (int)this.m_buffer.ReadLong();
						bullet.mHitBall = this.mIntToPointerMap_Ball[num3];
					}
					goto IL_258;
				}
			}
			foreach (ReversePowerEffect reversePowerEffect2 in this.mPointerSyncList_ReversePowerEffect)
			{
				int num4 = 0;
				if (reversePowerEffect2.mCurve != null && this.mPointerToIntMap_CurveMgr.ContainsKey(reversePowerEffect2.mCurve))
				{
					num4 = this.mPointerToIntMap_CurveMgr[reversePowerEffect2.mCurve];
				}
				this.m_buffer.WriteLong((long)num4);
			}
			foreach (Ball ball2 in this.mPointerSyncList_Ball)
			{
				int num5 = 0;
				if (ball2.mBullet != null && this.mPointerToIntMap_Bullet.ContainsKey(ball2.mBullet))
				{
					num5 = this.mPointerToIntMap_Bullet[ball2.mBullet];
				}
				this.m_buffer.WriteLong((long)num5);
			}
			foreach (Bullet bullet2 in this.mPointerSyncList_Bullet)
			{
				int num6 = 0;
				if (bullet2.mHitBall != null && this.mPointerToIntMap_Ball.ContainsKey(bullet2.mHitBall))
				{
					num6 = this.mPointerToIntMap_Ball[bullet2.mHitBall];
				}
				this.m_buffer.WriteLong((long)num6);
			}
			IL_258:
			this.ResetPointerTable();
		}

		// Token: 0x04000A7C RID: 2684
		private SexyFramework.Misc.Buffer m_buffer;

		// Token: 0x04000A7D RID: 2685
		private bool m_isRead = true;

		// Token: 0x04000A7E RID: 2686
		private int mCurPointerIndex;

		// Token: 0x04000A7F RID: 2687
		private Dictionary<CurveMgr, int> mPointerToIntMap_CurveMgr = new Dictionary<CurveMgr, int>();

		// Token: 0x04000A80 RID: 2688
		private Dictionary<int, CurveMgr> mIntToPointerMap_CurveMgr = new Dictionary<int, CurveMgr>();

		// Token: 0x04000A81 RID: 2689
		private List<ReversePowerEffect> mPointerSyncList_ReversePowerEffect = new List<ReversePowerEffect>();

		// Token: 0x04000A82 RID: 2690
		private Dictionary<Ball, int> mPointerToIntMap_Ball = new Dictionary<Ball, int>();

		// Token: 0x04000A83 RID: 2691
		private Dictionary<int, Ball> mIntToPointerMap_Ball = new Dictionary<int, Ball>();

		// Token: 0x04000A84 RID: 2692
		private List<Bullet> mPointerSyncList_Bullet = new List<Bullet>();

		// Token: 0x04000A85 RID: 2693
		private Dictionary<Bullet, int> mPointerToIntMap_Bullet = new Dictionary<Bullet, int>();

		// Token: 0x04000A86 RID: 2694
		private Dictionary<int, Bullet> mIntToPointerMap_Bullet = new Dictionary<int, Bullet>();

		// Token: 0x04000A87 RID: 2695
		private List<Ball> mPointerSyncList_Ball = new List<Ball>();
	}
}
