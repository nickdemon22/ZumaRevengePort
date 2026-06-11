using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	// Token: 0x02000007 RID: 7
	public class TextManager
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00008383 File Offset: 0x00006583
		public static TextManager getInstance()
		{
			return TextManager.instance;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000838C File Offset: 0x0000658C
		protected TextManager()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008424 File Offset: 0x00006624
		public bool init()
		{
			Localization.LanguageType currentLanguage = Localization.GetCurrentLanguage();
			CultureInfo currentCulture = new CultureInfo(this.sLangFiles[(int)currentLanguage]);
			Thread.CurrentThread.CurrentCulture = currentCulture;
			this.releaseTextKit();
			string textFile = "text/text" + Localization.GetLanguageSuffix(currentLanguage) + ".txt";
			if (!this.LoadTextKit(textFile))
			{
				this.LoadTextKit("text/text_EN.txt");
			}
			return this.mStringList.Count > 0;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008454 File Offset: 0x00006654
		public bool LoadTextKitFromStream(Stream s)
		{
			bool result = true;
			try
			{
				using (StreamReader streamReader = new StreamReader(s))
				{
					for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
					{
						text = text.Replace("\\n", "\n");
						text = text.Replace("&cr;", "\n");
						this.mStringList.Add(text);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000084DC File Offset: 0x000066DC
		public bool LoadTextKit(string file)
		{
			bool result = true;
			Stream stream = null;
			try
			{
				stream = TitleContainer.OpenStream("Content\\" + file);
				using (StreamReader streamReader = new StreamReader(stream))
				{
					for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
					{
						text = text.Replace("\\n", "\n");
						text = text.Replace("&cr;", "\n");
						this.mStringList.Add(text);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008590 File Offset: 0x00006790
		public void releaseTextKit()
		{
			this.mStringList.Clear();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000085A0 File Offset: 0x000067A0
		public string getString(int id)
		{
			if (id >= 0 && id < this.mStringList.Count)
			{
				return this.mStringList[id];
			}
			return string.Empty;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000085CC File Offset: 0x000067CC
		public int getIdByString(string s)
		{
			if (s == "")
			{
				return -1;
			}
			for (int i = 0; i < this.mStringList.Count; i++)
			{
				if (this.mStringList[i].Trim() == s.Trim())
				{
					return i;
				}
			}
			throw new Exception("failed to find string - " + s);
		}

		// Token: 0x04000806 RID: 2054
		protected static TextManager instance = new TextManager();

		// Token: 0x04000807 RID: 2055
		private string[] sLangFiles = new string[]
		{
			"en-US",
			"fr-FR",
			"it-IT",
			"de-DE",
			"es-ES",
			"zh-CN",
			"ru-RU",
			"pl-PL",
			"pt-PT",
			"es-CO",
			"zh-TW",
			"pt-BR"
		};

		// Token: 0x04000808 RID: 2056
		protected List<string> mStringList = new List<string>(300);
	}
}
