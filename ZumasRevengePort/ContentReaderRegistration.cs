using Microsoft.Xna.Framework.Content;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	internal static class ContentReaderRegistration
	{
		private static bool sRegistered;

		public static void RegisterAll()
		{
			if (sRegistered)
			{
				return;
			}
			sRegistered = true;

			ContentTypeReaderManager.AddTypeCreator(
				"SexyFramework.Resource.ResourceXmlReader, SexyFramework",
				() => new ResourceXmlReader());

			// Original WP7 XNB files reference an invalid assembly name "Zuma's Revenge!".
			ContentTypeReaderManager.AddTypeCreator(
				"ZumasRevenge.LevelsXmlReader, Zuma's Revenge!",
				() => new LevelsXmlReader());
			ContentTypeReaderManager.AddTypeCreator(
				"ZumasRevenge.LevelsXmlReader, ZumasRevenge",
				() => new LevelsXmlReader());
			ContentTypeReaderManager.AddTypeCreator(
				"ZumasRevenge.LevelsXmlReader, ZumasRevengeApp",
				() => new LevelsXmlReader());
		}
	}
}
