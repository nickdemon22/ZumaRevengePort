using System;
using SexyFramework.Drivers.Profile;

namespace ZumasRevenge.Profile
{
	// Token: 0x02000032 RID: 50
	public class Profile
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00046225 File Offset: 0x00044425
		public void loadProfile()
		{
			this.fspd.LoadDetails();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00046233 File Offset: 0x00044433
		public void saveAll()
		{
			this.fspd.SaveDetails();
		}

		// Token: 0x04000CA6 RID: 3238
		private FilesystemProfileData fspd = new FilesystemProfileData(new UserProfile());

		// Token: 0x04000CA7 RID: 3239
		private int aa;
	}
}
