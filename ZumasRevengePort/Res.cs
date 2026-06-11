using System;
using System.Collections.Generic;
using System.Reflection;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000003 RID: 3
	public static class Res
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000254D File Offset: 0x0000074D
		public static void InitResources(GameApp app)
		{
			Res.mApp = app;
			Res.mResMgr = Res.mApp.mResourceManager;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002564 File Offset: 0x00000764
		public static Image GetImageByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Image;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] == null)
			{
				List<string> allEnum = Res.GetAllEnum(id);
				for (int i = 0; i < allEnum.Count; i++)
				{
					string text2 = allEnum[i];
					Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text2);
					if (Res.mGlobalRes[(int)id] != null)
					{
						break;
					}
				}
			}
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadImage(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Image;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002628 File Offset: 0x00000828
		public static List<string> GetAllEnum(ResID id)
		{
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in typeof(ResID).GetFields())
			{
				if (fieldInfo.IsLiteral && typeof(ResID).GetType() == typeof(ResID).GetType() && (int)fieldInfo.GetRawConstantValue() == (int)typeof(ResID).GetField(id.ToString()).GetRawConstantValue())
				{
					list.Add(fieldInfo.Name);
				}
			}
			return list;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000026C4 File Offset: 0x000008C4
		public static int GetIDByImage(Image img)
		{
			for (int i = 0; i < Res.mGlobalRes.Length; i++)
			{
				if (Res.mGlobalRes[i] != null && Res.mGlobalRes[i].mResObject == img)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002700 File Offset: 0x00000900
		public static Font GetFontByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as Font;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadFont(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as Font;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000277C File Offset: 0x0000097C
		public static int GetSoundByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return (int)Res.mGlobalRes[(int)id].mResObject;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadSound(text);
			}
			return (int)Res.mGlobalRes[(int)id].mResObject;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027F8 File Offset: 0x000009F8
		public static PIEffect GetPIEffectByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null && Res.mGlobalRes[(int)id].mResObject != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PIEffect;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadPIEffect(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PIEffect;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002874 File Offset: 0x00000A74
		public static Effect GetEffectByID(ResID id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000287C File Offset: 0x00000A7C
		public static PopAnim GetPopAnimByID(ResID id)
		{
			if (Res.mGlobalRes[(int)id] != null)
			{
				return Res.mGlobalRes[(int)id].mResObject as PopAnim;
			}
			string text = id.ToString();
			Res.mGlobalRes[(int)id] = Res.mResMgr.RegisterGlobalPtr(text);
			if (Res.mGlobalRes[(int)id] != null)
			{
				Res.mResMgr.LoadPopAnim(text);
			}
			return Res.mGlobalRes[(int)id].mResObject as PopAnim;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028EC File Offset: 0x00000AEC
		public static int GetOffsetXByID(ResID id)
		{
			if (Res.mGlobalResOffset[(int)id] != null)
			{
				return Res.mGlobalResOffset[(int)id].mX;
			}
			string text = id.ToString();
			Point offsetOfImage = Res.mResMgr.GetOffsetOfImage(text);
			if (offsetOfImage != null)
			{
				Res.mGlobalResOffset[(int)id] = new Point(offsetOfImage);
				return Res.mGlobalResOffset[(int)id].mX;
			}
			return 0;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002954 File Offset: 0x00000B54
		public static int GetOffsetYByID(ResID id)
		{
			if (Res.mGlobalResOffset[(int)id] != null)
			{
				return Res.mGlobalResOffset[(int)id].mY;
			}
			string text = id.ToString();
			Point offsetOfImage = Res.mResMgr.GetOffsetOfImage(text);
			if (offsetOfImage != null)
			{
				Res.mGlobalResOffset[(int)id] = new Point(offsetOfImage);
				return Res.mGlobalResOffset[(int)id].mY;
			}
			return 0;
		}

		// Token: 0x0400000A RID: 10
		private static ResGlobalPtr[] mGlobalRes = new ResGlobalPtr[1850];

		// Token: 0x0400000B RID: 11
		private static Point[] mGlobalResOffset = new Point[1850];

		// Token: 0x0400000C RID: 12
		private static GameApp mApp = null;

		// Token: 0x0400000D RID: 13
		private static ResourceManager mResMgr = null;
	}
}
