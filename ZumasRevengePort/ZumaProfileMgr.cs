using System;
using System.Collections.Generic;
using SexyFramework.Drivers.Profile;

namespace ZumasRevenge
{
	// Token: 0x0200002B RID: 43
	public class ZumaProfileMgr : ProfileManager
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x000425CC File Offset: 0x000407CC
		public ZumaProfileMgr() : base(GameApp.gApp)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000425DC File Offset: 0x000407DC
		public void RenameTempProfile(string new_name)
		{
			if (this.GetProfile(".temp") != null)
			{
				bool flag = this.RenameProfile(".temp", new_name);
				if (flag)
				{
					return;
				}
			}
			this.AddProfile(new_name);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0004260F File Offset: 0x0004080F
		public bool HasTempProfile()
		{
			return this.GetProfile(".temp") != null;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00042624 File Offset: 0x00040824
		public void GetListOfUserNames(List<string> user_vec)
		{
			if (user_vec == null)
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)this.GetNumProfiles()))
			{
				UserProfile profile = this.GetProfile(num);
				user_vec.Insert(user_vec.Count, profile.GetName());
				num++;
			}
		}
	}
}
