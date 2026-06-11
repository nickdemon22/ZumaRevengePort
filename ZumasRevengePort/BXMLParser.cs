using System;
using SexyFramework;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	// Token: 0x020000CB RID: 203
	public class BXMLParser
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x0008F11C File Offset: 0x0008D31C
		protected string UnpackString()
		{
			string text = "";
			for (int num = this.mSexyBuffer.ReadInt32(); num != 0; num = this.mSexyBuffer.ReadInt32())
			{
				text += (char)num;
			}
			return text;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0008F15C File Offset: 0x0008D35C
		protected short UnpackShort()
		{
			return this.mSexyBuffer.ReadShort();
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0008F176 File Offset: 0x0008D376
		public BXMLParser()
		{
			this.mSexyBuffer = null;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0008F185 File Offset: 0x0008D385
		public virtual void Dispose()
		{
			this.mSexyBuffer = null;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0008F190 File Offset: 0x0008D390
		public virtual bool OpenFile(string filename)
		{
			PFILE pfile = new PFILE(filename, "rb");
			if (!pfile.Open())
			{
				return false;
			}
			byte[] data = pfile.GetData();
			this.mSexyBuffer = new SexyFramework.Misc.Buffer();
			this.mSexyBuffer.SetData(data, data.Length);
			return true;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0008F1D8 File Offset: 0x0008D3D8
		public virtual bool OpenStream(string filename)
		{
			this.mSexyBuffer = new SexyFramework.Misc.Buffer();
			return GlobalMembers.gSexyApp.ReadBufferFromStream(filename, ref this.mSexyBuffer);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0008F208 File Offset: 0x0008D408
		public virtual bool OpenBuffer(SexyFramework.Misc.Buffer buffer)
		{
			this.mSexyBuffer = buffer;
			return true;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0008F214 File Offset: 0x0008D414
		public virtual bool NextElement(ref BXMLElement theElement)
		{
			if (this.mSexyBuffer.AtEnd())
			{
				return false;
			}
			theElement.mType = 0;
			theElement.mValue = "";
			theElement.mAttributes.Clear();
			theElement.mType = (int)this.UnpackShort();
			theElement.mValue = this.UnpackString();
			int num = (int)this.UnpackShort();
			while (num-- > 0)
			{
				string text = this.UnpackString().ToLower();
				string text2 = this.UnpackString();
				theElement.mAttributes[text] = text2;
			}
			return true;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0008F29D File Offset: 0x0008D49D
		public static bool CompileXML(string theSrcName, string theSrcDestName)
		{
			return true;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0008F2A0 File Offset: 0x0008D4A0
		public bool HasFailed()
		{
			return false;
		}

		// Token: 0x04001726 RID: 5926
		private SexyFramework.Misc.Buffer mSexyBuffer;
	}
}
