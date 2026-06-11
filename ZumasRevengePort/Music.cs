using System;
using SexyFramework;
using SexyFramework.Drivers.App;

namespace ZumasRevenge
{
	// Token: 0x02000028 RID: 40
	public class Music : IDisposable
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x000407F0 File Offset: 0x0003E9F0
		public Music(MusicInterface inMusicInterface)
		{
			this.mMusicInterface = inMusicInterface;
			this.mEnabled = false;
			this.mCurrentSong = Song.DefaultSong;
			this.mNextSong = Song.DefaultSong;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0004083D File Offset: 0x0003EA3D
		public void RegisterCallBack()
		{
			this.mMusicInterface.RegisterCallback(new SongChangedEventHandle(this.OnSongChanged));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00040856 File Offset: 0x0003EA56
		public void OnSongChanged(object sender, SongChangedEventArgs args)
		{
			this.mCurrentSong = new Song(args.songID, args.loop, 1f);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00040874 File Offset: 0x0003EA74
		public void Dispose()
		{
			this.mMusicInterface.UnloadAllMusic();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00040881 File Offset: 0x0003EA81
		public void Enable(bool inEnable)
		{
			if (this.mEnabled && !inEnable)
			{
				this.mNextSong = this.mCurrentSong;
				this.mCurrentSong = Song.DefaultSong;
				this.mMusicInterface.StopAllMusic();
			}
			this.mEnabled = inEnable;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000408B7 File Offset: 0x0003EAB7
		public void LoadMusic(int inSongID, string inFileName)
		{
			this.mMusicInterface.LoadMusic(inSongID, inFileName, WP7AppDriver.sWP7AppDriverInstance.mContentManager);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000408D1 File Offset: 0x0003EAD1
		public void PlaySong(int inSongID, float inFadeSpeed, bool inLoop)
		{
			this.PlaySong(inSongID, inFadeSpeed, inLoop, false);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000408E0 File Offset: 0x0003EAE0
		public void PlaySongNoDelay(int inSongID, bool inLoop)
		{
			if (this.IsPlaying(inSongID, false))
			{
				return;
			}
			this.mCurrentSong = new Song(inSongID, inLoop, 1f);
			this.mMusicInterface.PlayMusic(this.mCurrentSong.mID, 0, !this.mCurrentSong.mLoop, 0L);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00040934 File Offset: 0x0003EB34
		public void PlaySong(int inSongID, float inFadeSpeed, bool inLoop, bool inForce)
		{
			if (this.IsPlaying(inSongID, inForce))
			{
				return;
			}
			if (this.DelaySong(inSongID, inFadeSpeed, inLoop))
			{
				return;
			}
			this.mCurrentSong = new Song(inSongID, inLoop, 1f);
			this.mMusicInterface.PlayMusic(this.mCurrentSong.mID, 0, !this.mCurrentSong.mLoop, 0L);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00040992 File Offset: 0x0003EB92
		public void FadeOut()
		{
			this.mCurrentSong = Song.DefaultSong;
			this.mNextSong = this.mCurrentSong;
			this.mMusicInterface.FadeOutAll();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000409B6 File Offset: 0x0003EBB6
		public void StopAll()
		{
			this.mMusicInterface.StopAllMusic();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000409C4 File Offset: 0x0003EBC4
		public void Update()
		{
			if (!this.mEnabled || this.mMusicInterface.IsPlaying(this.mCurrentSong.mID))
			{
				return;
			}
			if (this.mNextSong.mID != -1)
			{
				this.mMusicInterface.FadeIn(this.mNextSong.mID, 0, (double)this.mNextSong.mFadeSpeed, !this.mNextSong.mLoop);
			}
			this.mCurrentSong = this.mNextSong;
			this.mNextSong = Song.DefaultSong;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00040A48 File Offset: 0x0003EC48
		private bool IsPlaying(int inSongID, bool inForceStop)
		{
			return this.mMusicInterface.IsPlaying(inSongID) && !inForceStop;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00040A60 File Offset: 0x0003EC60
		private bool DelaySong(int inSongID, float inFadeSpeed, bool inLoop)
		{
			if (this.mEnabled && !this.mMusicInterface.IsPlaying(this.mCurrentSong.mID))
			{
				return false;
			}
			this.mNextSong = new Song(inSongID, inLoop, inFadeSpeed);
			if (this.mEnabled)
			{
				this.mMusicInterface.FadeOut(this.mCurrentSong.mID, true, (double)inFadeSpeed);
			}
			return true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public bool IsUserMusicPlaying()
		{
			return this.mMusicInterface.isPlayingUserMusic();
		}

		// Token: 0x04000BE8 RID: 3048
		private MusicInterface mMusicInterface;

		// Token: 0x04000BE9 RID: 3049
		private bool mEnabled;

		// Token: 0x04000BEA RID: 3050
		private Song mCurrentSong = Song.DefaultSong;

		// Token: 0x04000BEB RID: 3051
		private Song mNextSong = Song.DefaultSong;
	}
}
