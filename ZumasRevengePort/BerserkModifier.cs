using System;
using System.Globalization;

namespace ZumasRevenge
{
	// Token: 0x020000B9 RID: 185
	public class BerserkModifier
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x0008E728 File Offset: 0x0008C928
		public BerserkModifier(BerserkModifier rhs)
		{
			this.mParamName = rhs.mParamName;
			this.mStringValue = rhs.mStringValue;
			this.mMinStr = rhs.mMinStr;
			this.mMaxStr = rhs.mMaxStr;
			this.mOverride = rhs.mOverride;
			this.mParamType = rhs.mParamType;
			this.mHasMin = rhs.mHasMin;
			this.mHasMax = rhs.mHasMax;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0008E79C File Offset: 0x0008C99C
		public BerserkModifier(string p, string value, string minval, string maxval, bool _override)
		{
			this.mParamName = p;
			this.mStringValue = value;
			this.mHasMin = (this.mHasMax = false);
			this.mOverride = _override;
			if (minval != null && minval.Length > 0)
			{
				this.mHasMin = true;
				this.mMinStr = minval;
			}
			if (maxval != null && maxval.Length > 0)
			{
				this.mHasMax = true;
				this.mMaxStr = maxval;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0008E80C File Offset: 0x0008CA0C
		public BerserkModifier(string p, string value)
		{
			this.mParamName = p;
			this.mStringValue = value;
			this.mHasMin = (this.mHasMax = false);
			this.mOverride = false;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0008E844 File Offset: 0x0008CA44
		public void AddPointerFloat(object fptr)
		{
			this.mParamType = 1;
			this.mVariablePtr = fptr;
			if (this.mStringValue[0] == '.')
			{
				this.mStringValue = "0" + this.mStringValue;
			}
			double num = 0.0;
			double.TryParse(this.mStringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			this.mValue = num;
			if (this.mHasMin)
			{
				this.mMin = Convert.ToSingle(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToSingle(this.mMaxStr);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
		public void AddPointerInt(object iptr)
		{
			this.mParamType = 0;
			this.mVariablePtr = iptr;
			try
			{
				this.mValue = Convert.ToInt32(this.mStringValue);
			}
			catch (Exception)
			{
				this.mValue = 0;
			}
			if (this.mHasMin)
			{
				this.mMin = Convert.ToInt32(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToInt32(this.mMaxStr);
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0008E980 File Offset: 0x0008CB80
		public void AddPointerBool(object bptr)
		{
			this.mParamType = 2;
			this.mVariablePtr = bptr;
			this.mValue = Convert.ToBoolean(this.mStringValue);
			if (this.mHasMin)
			{
				this.mMin = Convert.ToBoolean(this.mMinStr);
			}
			if (this.mHasMax)
			{
				this.mMax = Convert.ToBoolean(this.mMaxStr);
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0008E9F0 File Offset: 0x0008CBF0
		public void ModifyVariable()
		{
			if (this.mParamType == 1)
			{
				ParamData<float> paramData = this.mVariablePtr as ParamData<float>;
				if (this.mOverride)
				{
					paramData.value = Convert.ToSingle(this.mValue);
					return;
				}
				paramData.value += Convert.ToSingle(this.mValue);
				if (this.mHasMin && paramData.value < Convert.ToSingle(this.mMin))
				{
					paramData.value = Convert.ToSingle(this.mMin);
					return;
				}
				if (this.mHasMax && paramData.value > Convert.ToSingle(this.mMax))
				{
					paramData.value = Convert.ToSingle(this.mMax);
					return;
				}
			}
			else if (this.mParamType == 0)
			{
				ParamData<int> paramData2 = this.mVariablePtr as ParamData<int>;
				if (this.mOverride)
				{
					paramData2.value = Convert.ToInt32(this.mValue);
					return;
				}
				paramData2.value += Convert.ToInt32(this.mValue);
				if (this.mHasMin && paramData2.value < Convert.ToInt32(this.mMin))
				{
					paramData2.value = Convert.ToInt32(this.mMin);
					return;
				}
				if (this.mHasMax && paramData2.value > Convert.ToInt32(this.mMax))
				{
					paramData2.value = Convert.ToInt32(this.mMax);
					return;
				}
			}
			else if (this.mParamType == 2)
			{
				ParamData<bool> paramData3 = this.mVariablePtr as ParamData<bool>;
				paramData3.value = Convert.ToBoolean(this.mValue);
			}
		}

		// Token: 0x040016AD RID: 5805
		public string mParamName;

		// Token: 0x040016AE RID: 5806
		public string mStringValue;

		// Token: 0x040016AF RID: 5807
		public string mMinStr;

		// Token: 0x040016B0 RID: 5808
		public string mMaxStr;

		// Token: 0x040016B1 RID: 5809
		public bool mOverride;

		// Token: 0x040016B2 RID: 5810
		public int mParamType;

		// Token: 0x040016B3 RID: 5811
		protected object mValue;

		// Token: 0x040016B4 RID: 5812
		protected object mVariablePtr;

		// Token: 0x040016B5 RID: 5813
		protected object mMin;

		// Token: 0x040016B6 RID: 5814
		protected object mMax;

		// Token: 0x040016B7 RID: 5815
		protected bool mHasMin;

		// Token: 0x040016B8 RID: 5816
		protected bool mHasMax;

		// Token: 0x020000CA RID: 202
		public enum DataType
		{
			// Token: 0x04001723 RID: 5923
			Type_Int,
			// Token: 0x04001724 RID: 5924
			Type_Float,
			// Token: 0x04001725 RID: 5925
			Type_Bool
		}
	}
}
