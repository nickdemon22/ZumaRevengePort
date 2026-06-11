using System;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	// Token: 0x02000030 RID: 48
	public class LegalInfo : DialogEx, SliderListener
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00045938 File Offset: 0x00043B38
		public LegalInfo() : base(null, null, 11, true, "", "", "", 0)
		{
			this.FONT_SHAGLOUNGE28_GREEN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_GREEN);
			this.FONT_SHAGEXOTICA68_BASE = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.FONT_SHAGLOUNGE28_BROWN = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_BROWN);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX);
			this.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK = Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK);
			this.mEndUserLicenseAgreement = null;
			this.mPrivacyPolicy = null;
			this.mTermsOfService = null;
			this.mAboutBtn = null;
			this.mHelpBtn = null;
			this.mOKBtn = null;
			this.mExternalLinkDialog = null;
			int num = Common._DS(Common._M(304));
			int num2 = Common._DS(Common._M(162));
			int num3 = Common._DS(85);
			int num4 = Common._DS(25);
			int num5 = Common._DS(75);
			string @string = TextManager.getInstance().getString(1);
			string string2 = TextManager.getInstance().getString(862);
			string string3 = TextManager.getInstance().getString(480);
			int num6 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string3);
			string string4 = TextManager.getInstance().getString(481);
			int num7 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string4);
			string string5 = TextManager.getInstance().getString(482);
			int num8 = this.FONT_SHAGLOUNGE28_GREEN.StringWidth(string5);
			int num9 = Math.Max(num6, Math.Max(num7, num8));
			this.mAboutBtn = Common.MakeButton(4, this, string2);
			this.mAboutBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mAboutBtn.Resize(num5, Common._DS(Common._M(30)) + num4, num9 + num3, num2);
			this.AddWidget(this.mAboutBtn);
			this.mEndUserLicenseAgreement = Common.MakeButton(0, this, string3);
			this.mEndUserLicenseAgreement.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mEndUserLicenseAgreement.Resize(num5, this.mAboutBtn.mY + this.mAboutBtn.mHeight + num4, num9 + num3, num2);
			int num10 = this.mEndUserLicenseAgreement.mWidth;
			this.AddWidget(this.mEndUserLicenseAgreement);
			this.mPrivacyPolicy = Common.MakeButton(1, this, string4);
			this.mPrivacyPolicy.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mPrivacyPolicy.Resize(num5, this.mEndUserLicenseAgreement.mY + this.mEndUserLicenseAgreement.mHeight + num4, num9 + num3, num2);
			num10 = ((num10 < this.mPrivacyPolicy.mWidth) ? this.mPrivacyPolicy.mWidth : num10);
			this.AddWidget(this.mPrivacyPolicy);
			this.mTermsOfService = Common.MakeButton(2, this, string5);
			this.mTermsOfService.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mTermsOfService.Resize(num5, this.mPrivacyPolicy.mY + this.mPrivacyPolicy.mHeight + num4, num9 + num3, num2);
			num10 = ((num10 < this.mTermsOfService.mWidth) ? this.mTermsOfService.mWidth : num10);
			this.AddWidget(this.mTermsOfService);
			this.mHelpBtn = Common.MakeButton(5, this, @string);
			this.mHelpBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			this.mHelpBtn.Resize(num5, this.mTermsOfService.mY + this.mTermsOfService.mHeight + num4, num9 + num3, num2);
			this.AddWidget(this.mHelpBtn);
			int num11 = num10 + num5 * 2;
			int num12 = (int)((float)(this.mTermsOfService.mY + num4) + (float)num2 * 3f) + 20;
			this.Resize((GameApp.gApp.mWidth - num11) / 2, (GameApp.gApp.GetScreenRect().mHeight - num12) / 2, num11, num12);
			this.mVersionTextY = this.mHelpBtn.mY + this.mHelpBtn.mHeight + num4 + 29;
			this.mOKBtn = Common.MakeButton(3, this, TextManager.getInstance().getString(483));
			this.mOKBtn.SetFont(this.FONT_SHAGLOUNGE28_GREEN);
			int num13 = 10;
			this.mOKBtn.Resize((this.mWidth - num) / 2, this.mHeight - num2 - num13, num, num2);
			this.AddWidget(this.mOKBtn);
			this.mHasTransparencies = (this.mHasAlpha = true);
			this.mClip = false;
			this.mDrawScale.SetCurve(Common._MP("b+0,2,0.033333,1,####        cY### >P###"));
			this.mCurrentLanguage = Localization.GetCurrentLanguage();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00045DA4 File Offset: 0x00043FA4
		public override void Dispose()
		{
			this.RemoveAllWidgets(false, true);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00045DAE File Offset: 0x00043FAE
		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			this.mEndUserLicenseAgreement = null;
			this.mPrivacyPolicy = null;
			this.mTermsOfService = null;
			this.mOKBtn = null;
			this.mAboutBtn = null;
			this.mHelpBtn = null;
			this.mExternalLinkDialog = null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00045DE9 File Offset: 0x00043FE9
		public override void Draw(SexyGraphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00045DFF File Offset: 0x00043FFF
		public override void ButtonPress(int inButtonID)
		{
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			base.ButtonPress(inButtonID);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00045E1C File Offset: 0x0004401C
		public void ProcessHardwareBackButton()
		{
			if (this.mExternalLinkDialog != null)
			{
				this.mExternalLinkDialog.ButtonDepress(1001);
			}
			else if (GameApp.gApp.mGenericHelp != null)
			{
				GameApp.gApp.mGenericHelp.ButtonDepress(1000);
			}
			else
			{
				this.ButtonDepress(3);
			}
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00045E78 File Offset: 0x00044078
		public override void ButtonDepress(int inButtonID)
		{
			if (this.mExternalLinkDialog != null)
			{
				return;
			}
			string text = "";
			switch ((int)this.mCurrentLanguage)
			{
			default:
				text += "en";
				break;
			case 1:
				text += "fr";
				break;
			case 2:
				text += "it";
				break;
			case 3:
				text += "de";
				break;
			case 4:
				text += "es";
				break;
			case 5:
				text += "sc";
				break;
			case 6:
				text += "ru";
				break;
			case 7:
				text += "pl";
				break;
			case 8:
				text += "pt";
				break;
			case 9:
				text += "es";
				break;
			case 10:
				text += "tc";
				break;
			case 11:
				text += "br";
				break;
			}
			if (this.mOKBtn != null && inButtonID == this.mOKBtn.mId)
			{
				this.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
				this.mWidgetFlagsMod.mRemoveFlags |= 16;
				GameApp.gApp.HideLegal();
				return;
			}
			if (this.mEndUserLicenseAgreement != null && inButtonID == this.mEndUserLicenseAgreement.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/mobileeula/US/" + text + "/GM");
				return;
			}
			if (this.mTermsOfService != null && inButtonID == this.mTermsOfService.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/WEBTERMS/US/" + text + "/PC");
				return;
			}
			if (this.mPrivacyPolicy != null && inButtonID == this.mPrivacyPolicy.mId)
			{
				this.ShowExternalLinkInfo("http://tos.ea.com/legalapp/WEBPRIVACY/US/" + text + "/PC/");
				return;
			}
			if (this.mAboutBtn != null && inButtonID == this.mAboutBtn.mId)
			{
				GameApp.gApp.ShowAbout();
				return;
			}
			if (this.mHelpBtn != null && inButtonID == this.mHelpBtn.mId)
			{
				GameApp.gApp.mGenericHelp = new GenericHelp();
				GameApp.gApp.AddDialog(GameApp.gApp.mGenericHelp);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000460A8 File Offset: 0x000442A8
		public override void MouseDrag(int x, int y)
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000460AA File Offset: 0x000442AA
		private void ShowExternalLinkInfo(string theURL)
		{
			if (this.mExternalLinkDialog == null)
			{
				this.mExternalLinkDialog = new LegalInfo.ExternalLinkDialog(this, theURL);
				Common.SetupDialog(this.mExternalLinkDialog);
				GameApp.gApp.AddDialog(this.mExternalLinkDialog);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000460DC File Offset: 0x000442DC
		public void HideExternalLinkInfo()
		{
			this.mExternalLinkDialog.mDrawScale.SetCurve(Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mExternalLinkDialog.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mExternalLinkDialog = null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00046118 File Offset: 0x00044318
		public void SliderVal(int theId, double theVal)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0004611A File Offset: 0x0004431A
		public void SliderReleased(int theId, double theVal)
		{
		}

		// Token: 0x04000C97 RID: 3223
		private DialogButton mEndUserLicenseAgreement;

		// Token: 0x04000C98 RID: 3224
		private DialogButton mPrivacyPolicy;

		// Token: 0x04000C99 RID: 3225
		private DialogButton mTermsOfService;

		// Token: 0x04000C9A RID: 3226
		private DialogButton mAboutBtn;

		// Token: 0x04000C9B RID: 3227
		private DialogButton mHelpBtn;

		// Token: 0x04000C9C RID: 3228
		private DialogButton mOKBtn;

		// Token: 0x04000C9D RID: 3229
		private int mVersionTextY;

		// Token: 0x04000C9E RID: 3230
		private LegalInfo.ExternalLinkDialog mExternalLinkDialog;

		// Token: 0x04000C9F RID: 3231
		private Font FONT_SHAGLOUNGE28_GREEN;

		// Token: 0x04000CA0 RID: 3232
		private Font FONT_SHAGEXOTICA68_BASE;

		// Token: 0x04000CA1 RID: 3233
		private Font FONT_SHAGLOUNGE28_BROWN;

		// Token: 0x04000CA2 RID: 3234
		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_CROWN_BOX;

		// Token: 0x04000CA3 RID: 3235
		private Image IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDEBOXBACK;

		// Token: 0x04000CA4 RID: 3236
		private Localization.LanguageType mCurrentLanguage;

		// Token: 0x0200013D RID: 317
		private enum LegalButtonIDs
		{
			// Token: 0x04001A5B RID: 6747
			Legal_EndUserLicenseAgreementID,
			// Token: 0x04001A5C RID: 6748
			Legal_PrivacyPolicyID,
			// Token: 0x04001A5D RID: 6749
			Legal_TermsOfServiceID,
			// Token: 0x04001A5E RID: 6750
			Legal_OKID,
			// Token: 0x04001A5F RID: 6751
			Legal_AboutID,
			// Token: 0x04001A60 RID: 6752
			Legal_HelpID,
			// Token: 0x04001A61 RID: 6753
			Legal_MetricsSharingID
		}

		// Token: 0x0200013E RID: 318
		private class ExternalLinkDialog : ZumaDialog
		{
			// Token: 0x06000FFA RID: 4090 RVA: 0x000A3797 File Offset: 0x000A1997
			public ExternalLinkDialog(LegalInfo theLegalInfo, string theURL) : base(13, true, TextManager.getInstance().getString(486), TextManager.getInstance().getString(487), "", 2)
			{
				this.mLegalInfo = theLegalInfo;
				this.mURL = theURL;
			}

			// Token: 0x06000FFB RID: 4091 RVA: 0x000A37D4 File Offset: 0x000A19D4
			~ExternalLinkDialog()
			{
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x000A37FC File Offset: 0x000A19FC
			public override void Resize(int x, int y, int w, int h)
			{
				base.Resize(x, y, w, h);
				ButtonWidget[] inButtons = new ButtonWidget[]
				{
					this.mYesButton,
					this.mNoButton
				};
				Common.SizeButtonsToLabel(inButtons, 2, Common._S(20));
			}

			// Token: 0x06000FFD RID: 4093 RVA: 0x000A3840 File Offset: 0x000A1A40
			public override void ButtonDepress(int id)
			{
				if (id == 2000 + this.mId || id == 1000)
				{
					GameApp.gApp.OpenURL(this.mURL);
					this.mLegalInfo.HideExternalLinkInfo();
					return;
				}
				if (id == 3000 + this.mId || id == 1001)
				{
					this.mLegalInfo.HideExternalLinkInfo();
				}
			}

			// Token: 0x04001A62 RID: 6754
			private string mURL;

			// Token: 0x04001A63 RID: 6755
			private LegalInfo mLegalInfo;
		}
	}
}
