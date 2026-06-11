using System;
using System.Collections.Generic;
using System.Globalization;
using JeffLib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	// Token: 0x02000022 RID: 34
	public class Credits
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0003BBCC File Offset: 0x00039DCC
		public Credits(bool isFromMainMenu)
		{
			this.mYScrollAmt = 0f;
			this.mAlpha = 0f;
			this.mTitleFont = null;
			this.mNameFont = null;
			this.mSpaceAfterTitle = 0;
			this.mSpaceAfterName = 0;
			this.mSpaceAfterImage = 0;
			this.mScrollSpeed = 0f;
			this.mFFAlpha = 0f;
			this.mInitialDelay = 0;
			this.mSpeedUp = false;
			this.mFromMainMenu = isFromMainMenu;
			this.mEntries = new List<Credits.CreditEntry>();
			this.FONT_SHAGLOUNGE28_SHADOW = Res.GetFontByID(ResID.FONT_SHAGLOUNGE28_SHADOW);
			this.IMAGE_CREDITS_IMAGES_POLAROID = Res.GetImageByID(ResID.IMAGE_CREDITS_IMAGES_POLAROID);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0003BC72 File Offset: 0x00039E72
		public virtual void Dispose()
		{
			if (GameApp.gApp.mResourceManager.IsGroupLoaded("Credits"))
			{
				GameApp.gApp.mResourceManager.DeleteResources("Credits");
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0003BC9E File Offset: 0x00039E9E
		private bool GetAttribute(XMLElement elem, string theName, ref string theValue)
		{
			if (elem.GetAttributeMap().ContainsKey(theName))
			{
				theValue = elem.GetAttributeMap()[theName];
				return true;
			}
			return false;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0003BCC0 File Offset: 0x00039EC0
		public void Init(bool advmode)
		{
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("Credits"))
			{
				GameApp.gApp.mResourceManager.LoadResources("Credits");
			}
			XMLParser xmlparser = new XMLParser();
			string languageSuffix = Localization.GetLanguageSuffix(Localization.GetCurrentLanguage());
			string text = "properties/credits/credits" + languageSuffix + ".xml";
			xmlparser.OpenFile(text);
			XMLElement xmlelement = new XMLElement();
			while (!xmlparser.HasFailed() && xmlparser.NextElement(xmlelement))
			{
				if ((int)xmlelement.mType == 1)
				{
					if (xmlelement.mValue.ToString() != this._S("Credits"))
					{
						break;
					}
					while (xmlparser.NextElement(xmlelement))
					{
						if ((int)xmlelement.mType == 1)
						{
							if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Defaults")))
							{
								string text2 = "";
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterTitle"), ref text2))
								{
									this.mSpaceAfterTitle = this.StrToInt(text2);
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterName"), ref text2))
								{
									this.mSpaceAfterName = this.StrToInt(text2);
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text2))
								{
									this.mSpaceAfterImage = this.StrToInt(text2);
								}
								if (this.GetAttribute(xmlelement, this._S("ScrollSpeed"), ref text2))
								{
									this.mScrollSpeed = Common._DS(this.StrToFloat(text2));
								}
								if (this.GetAttribute(xmlelement, this._S("TitleColor"), ref text2))
								{
									this.mTitleColor = new Color((int)Common.StrToHex(this.ToString(text2)));
								}
								if (this.GetAttribute(xmlelement, this._S("NameColor"), ref text2))
								{
									this.mNameColor = new Color((int)Common.StrToHex(this.ToString(text2)));
								}
								if (this.GetAttribute(xmlelement, this._S("TitleFont"), ref text2))
								{
									this.mTitleFont = GameApp.gApp.mResourceManager.LoadFont(text2);
								}
								if (this.GetAttribute(xmlelement, this._S("NameFont"), ref text2))
								{
									this.mNameFont = GameApp.gApp.mResourceManager.LoadFont(text2);
								}
							}
							else if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Text")))
							{
								string text3 = "";
								Credits.CreditEntry creditEntry = new Credits.CreditEntry();
								creditEntry.mSpaceAfterTitle = this.mSpaceAfterTitle;
								creditEntry.mSpaceAfterName = this.mSpaceAfterName;
								creditEntry.mSpaceAfterImage = this.mSpaceAfterImage;
								creditEntry.mTitleColor = this.mTitleColor;
								creditEntry.mNameColor = this.mNameColor;
								creditEntry.mTitleFont = this.mTitleFont;
								creditEntry.mNameFont = this.mNameFont;
								if (this.GetAttribute(xmlelement, this._S("mode"), ref text3))
								{
									creditEntry.mAlwaysShow = false;
									creditEntry.mAdvMode = this.StrEquals(text3, this._S("adventure"));
								}
								if (creditEntry.mAdvMode == advmode || creditEntry.mAlwaysShow)
								{
									if (this.GetAttribute(xmlelement, this._S("Title"), ref text3))
									{
										creditEntry.mTitle = text3;
									}
									if (this.GetAttribute(xmlelement, this._S("Name"), ref text3))
									{
										creditEntry.mName = text3;
									}
									if (this.GetAttribute(xmlelement, this._S("TitleFont"), ref text3))
									{
										creditEntry.mTitleFont = GameApp.gApp.mResourceManager.LoadFont(text3);
									}
									if (this.GetAttribute(xmlelement, this._S("NameFont"), ref text3))
									{
										creditEntry.mNameFont = GameApp.gApp.mResourceManager.LoadFont(text3);
									}
									if (this.GetAttribute(xmlelement, this._S("YOff"), ref text3))
									{
										creditEntry.mYOff = this.StrToInt(text3);
									}
									if (this.GetAttribute(xmlelement, this._S("XCenterOff"), ref text3))
									{
										creditEntry.mXCenterOff = Common._S(this.StrToInt(text3));
									}
									if (this.GetAttribute(xmlelement, this._S("TitleColor"), ref text3))
									{
										creditEntry.mTitleColor = new Color((int)Common.StrToHex(this.ToString(text3)));
									}
									if (this.GetAttribute(xmlelement, this._S("NameColor"), ref text3))
									{
										creditEntry.mNameColor = new Color((int)Common.StrToHex(this.ToString(text3)));
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterTitle"), ref text3))
									{
										creditEntry.mSpaceAfterTitle = this.StrToInt(text3);
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterName"), ref text3))
									{
										creditEntry.mSpaceAfterName = this.StrToInt(text3);
									}
									if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text3))
									{
										creditEntry.mSpaceAfterImage = this.StrToInt(text3);
									}
									this.mEntries.Add(creditEntry);
								}
							}
							else if (this.StrEquals(xmlelement.mValue.ToString(), this._S("Image")))
							{
								string text4 = "";
								Credits.CreditEntry creditEntry2 = new Credits.CreditEntry();
								if (this.GetAttribute(xmlelement, this._S("resid"), ref text4))
								{
									creditEntry2.mImage = GameApp.gApp.mResourceManager.LoadImage(text4).GetImage();
								}
								if (this.GetAttribute(xmlelement, this._S("YOff"), ref text4))
								{
									creditEntry2.mYOff = this.StrToInt(text4);
								}
								if (this.GetAttribute(xmlelement, this._S("xflip"), ref text4))
								{
									creditEntry2.mXFlip = this.StrToBool(text4);
								}
								if (this.GetAttribute(xmlelement, this._S("polaroid"), ref text4))
								{
									creditEntry2.mDoPolaroid = this.StrToBool(text4);
									if (!creditEntry2.mDoPolaroid)
									{
										creditEntry2.mImgAlpha = 255f;
									}
								}
								if (this.GetAttribute(xmlelement, this._S("SpaceAfterPic"), ref text4))
								{
									creditEntry2.mSpaceAfterImage = this.StrToInt(text4);
								}
								if (this.GetAttribute(xmlelement, this._S("x"), ref text4))
								{
									if (this.StrEquals(text4, this._S("center")))
									{
										creditEntry2.mXCenterOff = -creditEntry2.mImage.mWidth / 2;
									}
									else
									{
										creditEntry2.mXCenterOff = -GameApp.gApp.mWidth / 2 + Common._S(this.StrToInt(text4));
									}
								}
								this.mEntries.Add(creditEntry2);
							}
						}
					}
				}
			}
			xmlparser.CloseFile();
			int num = GameApp.gApp.mHeight;
			for (int i = 0; i < this.mEntries.Count; i++)
			{
				Credits.CreditEntry creditEntry3 = this.mEntries[i];
				num += Common._S(creditEntry3.mYOff);
				creditEntry3.mInitialY = num;
				if (creditEntry3.mImage == null)
				{
					if (creditEntry3.mTitle.Length > 0)
					{
						num += creditEntry3.mTitleFont.GetHeight() + Common._S(creditEntry3.mSpaceAfterTitle);
					}
					if (creditEntry3.mName.Length > 0)
					{
						num += creditEntry3.mNameFont.GetHeight() + Common._S(creditEntry3.mSpaceAfterName);
					}
				}
				else
				{
					num += Common._S(creditEntry3.mSpaceAfterImage) + creditEntry3.mImage.mHeight;
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0003C404 File Offset: 0x0003A604
		public bool AtEnd()
		{
			Credits.CreditEntry creditEntry = this.mEntries[this.mEntries.Count - 1];
			return (creditEntry.mImage != null && (float)creditEntry.mInitialY + this.mYScrollAmt <= (float)(GameApp.gApp.mHeight / 2 - creditEntry.mImage.mHeight / 2 - Common._DS(Common._M(200)))) || (creditEntry.mImage == null && (float)creditEntry.mInitialY + this.mYScrollAmt <= (float)(GameApp.gApp.mHeight / 2 - creditEntry.mTitleFont.mHeight / 2));
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0003C4A8 File Offset: 0x0003A6A8
		public void Update()
		{
			if (GameApp.gApp.IsHardwareBackButtonPressed() && !this.mFromMainMenu)
			{
				this.ProcessHardwareBackButton();
			}
			if (this.mAlpha < 255f)
			{
				this.mAlpha += Common._M(8f);
				if (this.mAlpha > 255f)
				{
					this.mAlpha = 255f;
					return;
				}
			}
			else
			{
				for (int i = 0; i < this.mEntries.Count; i++)
				{
					Credits.CreditEntry creditEntry = this.mEntries[i];
					int num = (int)((float)creditEntry.mInitialY + this.mYScrollAmt);
					if (creditEntry.mImage != null && creditEntry.mDoPolaroid && num <= Common._DS(Common._M(900)))
					{
						if (creditEntry.mImgAlpha < 255f)
						{
							creditEntry.mImgAlpha += Common._M(0.5f);
						}
						if (creditEntry.mImgAlpha > 255f)
						{
							creditEntry.mImgAlpha = 255f;
						}
					}
				}
				if (!this.AtEnd())
				{
					if (++this.mInitialDelay >= Common._M(100))
					{
						this.mYScrollAmt -= this.mScrollSpeed * (float)(this.mSpeedUp ? Common._M(4) : 1);
					}
					if (this.mInitialDelay >= Common._M(300))
					{
						this.mFFAlpha += Common._M(2f) * (float)(this.mSpeedUp ? Common._M1(4) : 1);
						if (this.mFFAlpha > 255f)
						{
							this.mFFAlpha = 255f;
							return;
						}
					}
				}
				else
				{
					this.mFFAlpha -= Common._M(2f);
					if (this.mFFAlpha < 0f)
					{
						this.mFFAlpha = 0f;
					}
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0003C674 File Offset: 0x0003A874
		public void Draw(SexyGraphics g)
		{
			g.SetColor(new Color(0, 0, 0, (int)this.mAlpha));
			g.FillRect(Common._S(-80), 0, GameApp.gApp.mWidth + Common._S(160), GameApp.gApp.mHeight);
			for (int i = 0; i < this.mEntries.Count; i++)
			{
				Credits.CreditEntry creditEntry = this.mEntries[i];
				int num = (int)((float)creditEntry.mInitialY + this.mYScrollAmt);
				if (this.AtEnd() && i == this.mEntries.Count - 1)
				{
					num = ((creditEntry.mImage != null) ? ((GameApp.gApp.mHeight - creditEntry.mImage.mHeight) / 2 - Common._DS(Common._M(200))) : ((GameApp.gApp.mHeight - creditEntry.mTitleFont.mHeight) / 2));
				}
				g.PushState();
				bool flag = GameApp.gApp.mUserProfile.mAdvModeVars.mHighestZoneBeat >= 6 || !this.mFromMainMenu;
				if (flag && creditEntry.mImage != null)
				{
					if (num > -350 && num < 700)
					{
						g.PushState();
						float num2 = (float)(creditEntry.mXFlip ? Common._DS(Common._M(this.mRoll)) : 0);
						if (creditEntry.mDoPolaroid)
						{
							g.DrawImageMirror(this.IMAGE_CREDITS_IMAGES_POLAROID, (int)((float)(GameApp.gApp.mWidth / 2 + creditEntry.mXCenterOff - Common._DS(Common._M(60))) - num2), num - Common._DS(Common._M1(36)), creditEntry.mXFlip);
							g.SetColorizeImages(true);
							g.SetColor(new Color(255, 255, 255, (int)creditEntry.mImgAlpha));
						}
						g.DrawImageMirror(creditEntry.mImage, GameApp.gApp.mWidth / 2 + creditEntry.mXCenterOff, num, creditEntry.mXFlip);
						g.PopState();
					}
				}
				else if (num > -100 && num < 700)
				{
					int num3 = 255;
					if (creditEntry.mTitle.Length > 0)
					{
						g.SetFont(creditEntry.mTitleFont);
						g.SetColor(new Color(creditEntry.mTitleColor.mRed, creditEntry.mTitleColor.mGreen, creditEntry.mTitleColor.mBlue, num3));
						g.WriteString(creditEntry.mTitle, creditEntry.mXCenterOff, num + creditEntry.mTitleFont.GetAscent(), GameApp.gApp.mWidth, 0);
						num += creditEntry.mSpaceAfterTitle + creditEntry.mTitleFont.GetHeight();
					}
					if (creditEntry.mName.Length > 0)
					{
						g.SetFont(creditEntry.mNameFont);
						g.SetColor(new Color(creditEntry.mNameColor.mRed, creditEntry.mNameColor.mGreen, creditEntry.mNameColor.mBlue, num3));
						g.WriteString(creditEntry.mName, creditEntry.mXCenterOff, num + creditEntry.mNameFont.GetAscent(), GameApp.gApp.mWidth, 0);
					}
				}
				g.PopState();
			}
			g.SetFont(this.FONT_SHAGLOUNGE28_SHADOW);
			if (!this.AtEnd())
			{
				g.SetColor(new Color(Common._M(255), Common._M1(255), Common._M2(255), (int)(this.mFFAlpha * Common._M3(0.5f))));
				g.DrawString(TextManager.getInstance().getString(435), Common._DS(Common._M(750)), Common._DS(Common._M1(1176)));
				return;
			}
			g.SetColor(new Color(Common._M(255), Common._M1(255), Common._M2(255), 200));
			g.DrawString(TextManager.getInstance().getString(433), Common._DS(Common._M(750)), Common._DS(Common._M1(1176)));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0003CA79 File Offset: 0x0003AC79
		public void ProcessHardwareBackButton()
		{
			GameApp.gApp.ReturnFromCredits();
			GameApp.gApp.OnHardwareBackButtonPressProcessed();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0003CA8F File Offset: 0x0003AC8F
		private float StrToFloat(string str)
		{
			if (str.Length == 0)
			{
				return 0f;
			}
			return float.Parse(str, NumberStyles.Float,  CultureInfo.InvariantCulture);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0003CAAF File Offset: 0x0003ACAF
		private int StrToInt(string str)
		{
			if (str.Length == 0)
			{
				return 0;
			}
			return int.Parse(str);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0003CAC1 File Offset: 0x0003ACC1
		private bool StrToBool(string str)
		{
			return str.Length != 0 && bool.Parse(str);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0003CAD3 File Offset: 0x0003ACD3
		private string ToString(string str)
		{
			return str;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
		private string _S(string str)
		{
			return str;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0003CAD9 File Offset: 0x0003ACD9
		private int sexyatoi(string str)
		{
			return this.StrToInt(str);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0003CAE2 File Offset: 0x0003ACE2
		private float sexyatof(string str)
		{
			return this.StrToFloat(str);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0003CAEB File Offset: 0x0003ACEB
		private bool StrEquals(string str, string cmp)
		{
			return str == cmp;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0003CAF4 File Offset: 0x0003ACF4
		private string StringToUpper(string str)
		{
			return str.ToUpper();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0003CAFC File Offset: 0x0003ACFC
		private string StringToLower(string str)
		{
			return str.ToLower();
		}

		// Token: 0x04000B98 RID: 2968
		public List<Credits.CreditEntry> mEntries;

		// Token: 0x04000B99 RID: 2969
		public float mYScrollAmt;

		// Token: 0x04000B9A RID: 2970
		public float mAlpha;

		// Token: 0x04000B9B RID: 2971
		public float mFFAlpha;

		// Token: 0x04000B9C RID: 2972
		public Font mTitleFont;

		// Token: 0x04000B9D RID: 2973
		public Font mNameFont;

		// Token: 0x04000B9E RID: 2974
		public int mSpaceAfterTitle;

		// Token: 0x04000B9F RID: 2975
		public int mSpaceAfterName;

		// Token: 0x04000BA0 RID: 2976
		public int mSpaceAfterImage;

		// Token: 0x04000BA1 RID: 2977
		public Color mTitleColor;

		// Token: 0x04000BA2 RID: 2978
		public Color mNameColor;

		// Token: 0x04000BA3 RID: 2979
		public int mRoll = -12;

		// Token: 0x04000BA4 RID: 2980
		public float mScrollSpeed;

		// Token: 0x04000BA5 RID: 2981
		public int mInitialDelay;

		// Token: 0x04000BA6 RID: 2982
		public bool mSpeedUp;

		// Token: 0x04000BA7 RID: 2983
		public bool mFromMainMenu;

		// Token: 0x04000BA8 RID: 2984
		public bool mTapDown;

		// Token: 0x04000BA9 RID: 2985
		private Font FONT_SHAGLOUNGE28_SHADOW;

		// Token: 0x04000BAA RID: 2986
		private Image IMAGE_CREDITS_IMAGES_POLAROID;

		// Token: 0x020000AD RID: 173
		public class CreditEntry
		{
			// Token: 0x06000DE7 RID: 3559 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
			public CreditEntry()
			{
				this.mImage = null;
				this.mTitleFont = null;
				this.mXFlip = false;
				this.mImgAlpha = 0f;
				this.mDoPolaroid = true;
				this.mNameFont = null;
				this.mYOff = 0;
				this.mSpaceAfterTitle = 0;
				this.mSpaceAfterName = 0;
				this.mSpaceAfterImage = 0;
				this.mAdvMode = true;
				this.mAlwaysShow = true;
				this.mXCenterOff = 0;
				this.mInitialY = 0;
				this.mTitle = "";
				this.mName = "";
			}

			// Token: 0x04001649 RID: 5705
			public string mTitle;

			// Token: 0x0400164A RID: 5706
			public string mName;

			// Token: 0x0400164B RID: 5707
			public Image mImage;

			// Token: 0x0400164C RID: 5708
			public Font mTitleFont;

			// Token: 0x0400164D RID: 5709
			public Font mNameFont;

			// Token: 0x0400164E RID: 5710
			public int mXCenterOff;

			// Token: 0x0400164F RID: 5711
			public int mYOff;

			// Token: 0x04001650 RID: 5712
			public int mInitialY;

			// Token: 0x04001651 RID: 5713
			public int mSpaceAfterTitle;

			// Token: 0x04001652 RID: 5714
			public int mSpaceAfterName;

			// Token: 0x04001653 RID: 5715
			public int mSpaceAfterImage;

			// Token: 0x04001654 RID: 5716
			public Color mTitleColor;

			// Token: 0x04001655 RID: 5717
			public Color mNameColor;

			// Token: 0x04001656 RID: 5718
			public bool mAdvMode;

			// Token: 0x04001657 RID: 5719
			public bool mAlwaysShow;

			// Token: 0x04001658 RID: 5720
			public float mImgAlpha;

			// Token: 0x04001659 RID: 5721
			public bool mDoPolaroid;

			// Token: 0x0400165A RID: 5722
			public bool mXFlip;
		}
	}
}
