import os
import re

root_dir = r"c:\Users\User\source\repos\ZumasRevengePort\ZumasRevengePort"

def modify_file(filepath, modifier_func):
    if not os.path.exists(filepath):
        print(f"Skipping (not found): {filepath}")
        return
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    new_content = modifier_func(content)
    if new_content != content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(new_content)
        print(f"Modified: {filepath}")

# 1. _DEVICEPIXELFORMAT.cs
def modify_devicepixelformat(content):
    return content.replace("[StructLayout(2)]", "[StructLayout(LayoutKind.Explicit)]")

modify_file(os.path.join(root_dir, "SexyFramework", "Graphics", "_DEVICEPIXELFORMAT.cs"), modify_devicepixelformat)

# 2. DataSync.cs
def modify_datasync(content):
    content = content.replace("public DataSync(Buffer buffer", "public DataSync(SexyFramework.Misc.Buffer buffer")
    content = content.replace("public Buffer GetBuffer()", "public SexyFramework.Misc.Buffer GetBuffer()")
    content = content.replace("private Buffer m_buffer;", "private SexyFramework.Misc.Buffer m_buffer;")
    return content

modify_file(os.path.join(root_dir, "DataSync.cs"), modify_datasync)

# 3. BXMLParser.cs
def modify_bxmlparser(content):
    content = content.replace("new Buffer()", "new SexyFramework.Misc.Buffer()")
    content = content.replace("public virtual bool OpenBuffer(Buffer buffer)", "public virtual bool OpenBuffer(SexyFramework.Misc.Buffer buffer)")
    content = content.replace("private Buffer mSexyBuffer;", "private SexyFramework.Misc.Buffer mSexyBuffer;")
    return content

modify_file(os.path.join(root_dir, "BXMLParser.cs"), modify_bxmlparser)

# 4. BetaStats.cs
def modify_betastats(content):
    content = content.replace("protected void Serialize(Buffer b)", "protected void Serialize(SexyFramework.Misc.Buffer b)")
    content = content.replace("protected bool Deserialize(Buffer b)", "protected bool Deserialize(SexyFramework.Misc.Buffer b)")
    content = content.replace("Buffer b = new Buffer();", "SexyFramework.Misc.Buffer b = new SexyFramework.Misc.Buffer();")
    content = content.replace("Buffer buffer = new Buffer();", "SexyFramework.Misc.Buffer buffer = new SexyFramework.Misc.Buffer();")
    return content

modify_file(os.path.join(root_dir, "BetaStats.cs"), modify_betastats)

# 5. Board.cs
def modify_board(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("Buffer buffer = new Buffer();", "SexyFramework.Misc.Buffer buffer = new SexyFramework.Misc.Buffer();")
    content = content.replace("Buffer buffer2 = new Buffer();", "SexyFramework.Misc.Buffer buffer2 = new SexyFramework.Misc.Buffer();")
    
    gamer_services_block = """\t\t\t\tif (GameApp.USE_XBOX_SERVICE && !GameApp.USE_TRIAL_VERSION)
\t\t\t\t{
\t\t\t\t\tSignedInGamer signedInGamer = Gamer.SignedInGamers[0];
\t\t\t\t\tLeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(0, 0);
\t\t\t\t\tLeaderboardEntry leaderboard = signedInGamer.LeaderboardWriter.GetLeaderboard(leaderboardIdentity);
\t\t\t\t\tleaderboard.Rating = (long)this.mApp.mUserProfile.GetAdvModeVars().mCurrentAdvScore;
\t\t\t\t\treturn;
\t\t\t\t}"""
    commented_block = """\t\t\t\t/*
\t\t\t\tif (GameApp.USE_XBOX_SERVICE && !GameApp.USE_TRIAL_VERSION)
\t\t\t\t{
\t\t\t\t\tSignedInGamer signedInGamer = Gamer.SignedInGamers[0];
\t\t\t\t\tLeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(0, 0);
\t\t\t\t\tLeaderboardEntry leaderboard = signedInGamer.LeaderboardWriter.GetLeaderboard(leaderboardIdentity);
\t\t\t\t\tleaderboard.Rating = (long)this.mApp.mUserProfile.GetAdvModeVars().mCurrentAdvScore;
\t\t\t\t\treturn;
\t\t\t\t}
\t\t\t\t*/"""
    content = content.replace(gamer_services_block, commented_block)
    content = content.replace(gamer_services_block.replace("\r\n", "\n"), commented_block)
    return content

modify_file(os.path.join(root_dir, "Board.cs"), modify_board)

# 6. GameMain.cs
def modify_gamemain(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("Guide.SimulateTrialMode = false;", "// Guide.SimulateTrialMode = false;")
    content = content.replace("if (!Guide.IsVisible)\r\n\t\t\t\t{\r\n\t\t\t\t\tbase.Update(gameTime);\r\n\t\t\t\t}", "base.Update(gameTime);")
    content = content.replace("if (!Guide.IsVisible)\n\t\t\t\t{\n\t\t\t\t\tbase.Update(gameTime);\n\t\t\t\t}", "base.Update(gameTime);")
    
    exception_try_block = """\t\t\ttry
\t\t\t{
\t\t\t\tbase.Update(gameTime);
\t\t\t}
\t\t\tcatch (GameUpdateRequiredException ex)
\t\t\t{
\t\t\t\tif (GameApp.USE_XBOX_SERVICE)
\t\t\t\t{
\t\t\t\t\tthis.SexyZuma.HandleGameUpdateRequired(ex);
\t\t\t\t}
\t\t\t}"""
    exception_try_block_replaced = """\t\t\ttry
\t\t\t{
\t\t\t\tbase.Update(gameTime);
\t\t\t}
\t\t\tcatch (Exception ex)
\t\t\t{
\t\t\t}"""
    content = content.replace(exception_try_block, exception_try_block_replaced)
    content = content.replace(exception_try_block.replace("\r\n", "\n"), exception_try_block_replaced)
    
    guide_visible_block = """\t\t\ttry
\t\t\t{
\t\t\t\tif (Guide.IsVisible)
\t\t\t\t{
\t\t\t\t\treturn;
\t\t\t\t}
\t\t\t}
\t\t\tcatch (Exception)
\t\t\t{
\t\t\t}"""
    content = content.replace(guide_visible_block, "")
    content = content.replace(guide_visible_block.replace("\r\n", "\n"), "")
    return content

modify_file(os.path.join(root_dir, "GameMain.cs"), modify_gamemain)

# 7. GameApp.cs
def modify_gameapp(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("GameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;", "// GameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;")
    content = content.replace("SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);", "// SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);")
    
    signed_in_callback = """\t\tprotected void GamerSignedInCallback(object sender, SignedInEventArgs args)
\t\t{
\t\t\tSignedInGamer gamer = args.Gamer;
\t\t\tif (gamer != null)
\t\t\t{
\t\t\t\tthis.m_DefaultProfileName = gamer.Gamertag;
\t\t\t}
\t\t\tif (gamer.IsSignedInToLive)
\t\t\t{
\t\t\t\tif (this.m_XLiveState == GameApp.EXLiveWaiting.E_WaitingForSignIn)
\t\t\t\t{
\t\t\t\t\tgamer.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), gamer);
\t\t\t\t\tthis.m_XLiveState = GameApp.EXLiveWaiting.E_WaitingForAchivements;
\t\t\t\t}
\t\t\t}
\t\t\telse
\t\t\t{
\t\t\t\tthis.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
\t\t\t\tif (this.IsFirstGameLoad(this.m_DefaultProfileName))
\t\t\t\t{
\t\t\t\t\tGameApp.gInitialProfLoadSuccessful = true;
\t\t\t\t\tthis.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
\t\t\t\t\tGameApp.gDDS.ChangeProfile(this.mUserProfile);
\t\t\t\t}
\t\t\t\telse
\t\t\t\t{
\t\t\t\t\tthis.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(GameApp.gApp.m_DefaultProfileName);
\t\t\t\t}
\t\t\t}
\t\t\tGameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;
\t\t}"""
    signed_in_callback_commented = """\t\tprotected void GamerSignedInCallback(object sender, EventArgs args)
\t\t{
\t\t}"""
    content = content.replace(signed_in_callback, signed_in_callback_commented)
    content = content.replace(signed_in_callback.replace("\r\n", "\n"), signed_in_callback_commented)
    
    get_achievements_callback = """\t\tprotected void GetAchievementsCallback(IAsyncResult result)
\t\t{
\t\t\tSignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
\t\t\tif (signedInGamer == null)
\t\t\t{
\t\t\t\treturn;
\t\t\t}
\t\t\tif (this.mUserProfile == null)
\t\t\t{
\t\t\t\tthis.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(0);
\t\t\t}
\t\t\ttry
\t\t\t{
\t\t\t\tthis.mUserProfile.m_AchievementMgr.m_AchievementsXLive = signedInGamer.EndGetAchievements(result);
\t\t\t}
\t\t\tcatch (Exception)
\t\t\t{
\t\t\t}
\t\t\tthis.m_XLiveState = GameApp.EXLiveWaiting.E_Ready;
\t\t}"""
    get_achievements_callback_commented = """\t\tprotected void GetAchievementsCallback(IAsyncResult result)
\t\t{
\t\t}"""
    content = content.replace(get_achievements_callback, get_achievements_callback_commented)
    content = content.replace(get_achievements_callback.replace("\r\n", "\n"), get_achievements_callback_commented)
    
    xlive_state_block = """\t\t\tif (this.m_XLiveState == GameApp.EXLiveWaiting.E_Ready)
\t\t\t{
\t\t\t\tthis.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
\t\t\t\tSignedInGamer signedInGamer = Gamer.SignedInGamers[0];
\t\t\t\tif (signedInGamer != null)
\t\t\t\t{
\t\t\t\t\tthis.m_DefaultProfileName = signedInGamer.Gamertag;
\t\t\t\t}
\t\t\t\tif (!this.IsFirstGameLoad(this.m_DefaultProfileName) || !this.IsFirstGameLoad(this.m_DefaultName))
\t\t\t\t{
\t\t\t\t\tif (!this.IsFirstGameLoad(this.m_DefaultName))
\t\t\t\t\t{
\t\t\t\t\t\tGameApp.gApp.mProfileMgr.RenameProfile(this.m_DefaultName, GameApp.gApp.m_DefaultProfileName);
\t\t\t\t\t}
\t\t\t\t\tthis.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(GameApp.gApp.m_DefaultProfileName);
\t\t\t\t\treturn;
\t\t\t\t}
\t\t\t\tGameApp.gInitialProfLoadSuccessful = true;
\t\t\t\tthis.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
\t\t\t\tGameApp.gDDS.ChangeProfile(this.mUserProfile);
\t\t\t}"""
    xlive_state_block_commented = """\t\t\tif (this.m_XLiveState == GameApp.EXLiveWaiting.E_Ready)
\t\t\t{
\t\t\t\tthis.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
\t\t\t}"""
    content = content.replace(xlive_state_block, xlive_state_block_commented)
    content = content.replace(xlive_state_block.replace("\r\n", "\n"), xlive_state_block_commented)
    return content

modify_file(os.path.join(root_dir, "GameApp.cs"), modify_gameapp)

# 8. LeaderBoards.cs
def modify_leaderboards(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("private LeaderboardReader mLeaderboardReader;", "// private LeaderboardReader mLeaderboardReader;")
    
    read_leaderboard = """\t\tpublic void readLeaderboard()
\t\t{
\t\t\ttry
\t\t\t{
\t\t\t\tSignedInGamer signedInGamer = Gamer.SignedInGamers[0];
\t\t\t\tLeaderboardIdentity leaderboardIdentity = LeaderboardIdentity.Create(0, 0);
\t\t\t\tif (this.mCurrentPage == 0)
\t\t\t\t{
\t\t\t\t\tLeaderboardReader.BeginRead(leaderboardIdentity, 0, 4, new AsyncCallback(this.LeaderboardReadCallback), signedInGamer);
\t\t\t\t}
\t\t\t\telse if (this.mPageUp && this.mLeaderboardReader.CanPageUp)
\t\t\t\t{
\t\t\t\t\tthis.mPageUp = false;
\t\t\t\t\tthis.mLeaderboardReader.BeginPageUp(new AsyncCallback(this.LeaderboardPageUpCallback), signedInGamer);
\t\t\t\t}
\t\t\t\telse if (this.mPageDown && this.mLeaderboardReader.CanPageDown)
\t\t\t\t{
\t\t\t\t\tthis.mPageDown = false;
\t\t\t\t\tthis.mLeaderboardReader.BeginPageDown(new AsyncCallback(this.LeaderboardPageDownCallback), signedInGamer);
\t\t\t\t}
\t\t\t}
\t\t\tcatch (Exception)
\t\t\t{
\t\t\t\tif (GameApp.gApp.mMainMenu != null && GameApp.gApp.mMainMenu.mState == MainMenu_State.State_LeaderBoards)
\t\t\t\t{
\t\t\t\t\tGameApp.gApp.DoGenericDialog("", TextManager.getInstance().getString(59), true, new GameApp.PreBlockCallback(this.ReturnMain), Common._DS(100));
\t\t\t\t}
\t\t\t}
\t\t}"""
    read_leaderboard_replaced = """\t\tpublic void readLeaderboard()
\t\t{
\t\t}"""
    content = content.replace(read_leaderboard, read_leaderboard_replaced)
    content = content.replace(read_leaderboard.replace("\r\n", "\n"), read_leaderboard_replaced)
    
    page_down_cb = """\t\tprotected void LeaderboardPageDownCallback(IAsyncResult result)
\t\t{
\t\t\tSignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
\t\t\tif (signedInGamer != null)
\t\t\t{
\t\t\t\ttry
\t\t\t\t{
\t\t\t\t\tthis.mLeaderboardReader.EndPageDown(result);
\t\t\t\t\tthis.mCanPageUp = this.mLeaderboardReader.CanPageUp;
\t\t\t\t\tthis.mCanPageDown = this.mLeaderboardReader.CanPageDown;
\t\t\t\t\tthis.mUpButton.SetVisible(this.mCanPageUp);
\t\t\t\t\tthis.mDownButton.SetVisible(this.mCanPageDown);
\t\t\t\t\tif (!this.mLeaderBoardsScrollWidget.HasWidget(this.mLeaderBoardsPages))
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, false, this.mLeaderboardReader);
\t\t\t\t\t\tthis.mLeaderBoardsPages.Resize(0, 0, this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mLeaderBoardsPages.mNumPages * 3);
\t\t\t\t\t\tthis.mLeaderBoardsScrollWidget.AddWidget(this.mLeaderBoardsPages);
\t\t\t\t\t}
\t\t\t\t\telse
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, true, this.mLeaderboardReader);
\t\t\t\t\t}
\t\t\t\t}
\t\t\t\tcatch (Exception)
\t\t\t\t{
\t\t\t\t\tif (GameApp.gApp.mMainMenu != null && GameApp.gApp.mMainMenu.mState == MainMenu_State.State_LeaderBoards)
\t\t\t\t\t{
\t\t\t\t\t\tGameApp.gApp.DoGenericDialog("", TextManager.getInstance().getString(59), true, new GameApp.PreBlockCallback(this.ReturnMain), Common._DS(100));
\t\t\t\t\t}
\t\t\t\t}
\t\t\t}
\t\t\tthis.mLoadingDataComplete = true;
\t\t\tthis.mLeaderBoardsScrollWidget.SetDisabled(false);
\t\t\tthis.mLeaderBoardsScrollWidget.SetVisible(true);
\t\t\tthis.mLeaderBoardsScrollWidget.SetPageVertical(1, false);
\t\t}"""
    page_down_cb_replaced = """\t\tprotected void LeaderboardPageDownCallback(IAsyncResult result)
\t\t{
\t\t}"""
    content = content.replace(page_down_cb, page_down_cb_replaced)
    content = content.replace(page_down_cb.replace("\r\n", "\n"), page_down_cb_replaced)

    page_up_cb = """\t\tprotected void LeaderboardPageUpCallback(IAsyncResult result)
\t\t{
\t\t\tSignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
\t\t\tif (signedInGamer != null)
\t\t\t{
\t\t\t\ttry
\t\t\t\t{
\t\t\t\t\tthis.mLeaderboardReader.EndPageUp(result);
\t\t\t\t\tthis.mCanPageUp = this.mLeaderboardReader.CanPageUp;
\t\t\t\t\tthis.mCanPageDown = this.mLeaderboardReader.CanPageDown;
\t\t\t\t\tthis.mUpButton.SetVisible(this.mCanPageUp);
\t\t\t\t\tthis.mDownButton.SetVisible(this.mCanPageDown);
\t\t\t\t\tif (!this.mLeaderBoardsScrollWidget.HasWidget(this.mLeaderBoardsPages))
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, false, this.mLeaderboardReader);
\t\t\t\t\t\tthis.mLeaderBoardsPages.Resize(0, 0, this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mLeaderBoardsPages.mNumPages * 3);
\t\t\t\t\t\tthis.mLeaderBoardsScrollWidget.AddWidget(this.mLeaderBoardsPages);
\t\t\t\t\t}
\t\t\t\t\telse
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, true, this.mLeaderboardReader);
\t\t\t\t\t}
\t\t\t\t}
\t\t\t\tcatch (Exception)
\t\t\t\t{
\t\t\t\t\tif (GameApp.gApp.mMainMenu != null && GameApp.gApp.mMainMenu.mState == MainMenu_State.State_LeaderBoards)
\t\t\t\t\t{
\t\t\t\t\t\tGameApp.gApp.DoGenericDialog("", TextManager.getInstance().getString(59), true, new GameApp.PreBlockCallback(this.ReturnMain), Common._DS(100));
\t\t\t\t\t}
\t\t\t\t}
\t\t\t}
\t\t\tthis.mLoadingDataComplete = true;
\t\t\tthis.mLeaderBoardsScrollWidget.SetDisabled(false);
\t\t\tthis.mLeaderBoardsScrollWidget.SetVisible(true);
\t\t\tthis.mLeaderBoardsScrollWidget.SetPageVertical(1, false);
\t\t}"""
    page_up_cb_replaced = """\t\tprotected void LeaderboardPageUpCallback(IAsyncResult result)
\t\t{
\t\t}"""
    content = content.replace(page_up_cb, page_up_cb_replaced)
    content = content.replace(page_up_cb.replace("\r\n", "\n"), page_up_cb_replaced)

    read_cb = """\t\tprotected void LeaderboardReadCallback(IAsyncResult result)
\t\t{
\t\t\tSignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
\t\t\tif (signedInGamer != null)
\t\t\t{
\t\t\t\ttry
\t\t\t\t{
\t\t\t\t\tthis.mLeaderboardReader = LeaderboardReader.EndRead(result);
\t\t\t\t\tthis.mCanPageUp = this.mLeaderboardReader.CanPageUp;
\t\t\t\t\tthis.mCanPageDown = this.mLeaderboardReader.CanPageDown;
\t\t\t\t\tthis.mUpButton.SetVisible(this.mCanPageUp);
\t\t\t\t\tthis.mDownButton.SetVisible(this.mCanPageDown);
\t\t\t\t\tif (!this.mLeaderBoardsScrollWidget.HasWidget(this.mLeaderBoardsPages))
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, false, this.mLeaderboardReader);
\t\t\t\t\t\tthis.mLeaderBoardsPages.Resize(0, 0, this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.mLeaderBoardsPages.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mLeaderBoardsPages.mNumPages * 3);
\t\t\t\t\t\tthis.mLeaderBoardsScrollWidget.AddWidget(this.mLeaderBoardsPages);
\t\t\t\t\t}
\t\t\t\t\telse
\t\t\t\t\t{
\t\t\t\t\t\tthis.mLeaderBoardsPages.AddPage(this.mCurrentPage, true, this.mLeaderboardReader);
\t\t\t\t\t}
\t\t\t\t}
\t\t\t\tcatch (Exception ex)
\t\t\t\t{
\t\t\t\t\tthis.ShowXboxErrorMessage();
\t\t\t\t}
\t\t\t}
\t\t\tthis.mLeaderBoardsScrollWidget.SetDisabled(false);
\t\t\tthis.mLoadingDataComplete = true;
\t\t\tthis.mLeaderBoardsScrollWidget.SetVisible(true);
\t\t\tthis.mLeaderBoardsScrollWidget.SetPageVertical(1, false);
\t\t}"""
    read_cb_replaced = """\t\tprotected void LeaderboardReadCallback(IAsyncResult result)
\t\t{
\t\t}"""
    content = content.replace(read_cb, read_cb_replaced)
    content = content.replace(read_cb.replace("\r\n", "\n"), read_cb_replaced)
    return content

modify_file(os.path.join(root_dir, "LeaderBoards.cs"), modify_leaderboards)

# 9. LeaderBoardsPages.cs
def modify_leaderboardspages(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("public void AddPage(int page, bool isUpdate, LeaderboardReader reader)", "public void AddPage(int page, bool isUpdate, object reader)")
    content = content.replace("private void SetupLeaderboardsTextXLive(ref int theStartY, int page, bool update, LeaderboardReader reader)", "private void SetupLeaderboardsTextXLive(ref int theStartY, int page, bool update, object reader)")
    
    setup_xlive = """\t\tprivate void SetupLeaderboardsTextXLive(ref int theStartY, int page, bool update, object reader)
\t\t{"""
    start_idx = content.find(setup_xlive)
    if start_idx != -1:
        end_idx = content.find("public void UpdatePage(int page)", start_idx)
        if end_idx != -1:
            body = content[start_idx:end_idx]
            commented_body = f"""\t\tprivate void SetupLeaderboardsTextXLive(ref int theStartY, int page, bool update, object reader)
\t\t{{
\t\t}}

\t\t"""
            content = content.replace(body, commented_body)
    return content

modify_file(os.path.join(root_dir, "LeaderBoardsPages.cs"), modify_leaderboardspages)

# 10. AchievementManager.cs
def modify_achievementmgr(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("public AchievementCollection m_AchievementsXLive;", "// public AchievementCollection m_AchievementsXLive;")
    
    unlock_xlive = """\t\tpublic bool UnlockAchievementXLive(string achievementKey, EAchievementType type)
\t\t{
\t\t\tSignedInGamer signedInGamer = Gamer.SignedInGamers[0];
\t\t\tif (signedInGamer == null)
\t\t\t{
\t\t\t\treturn false;
\t\t\t}
\t\t\ttry
\t\t\t{
\t\t\t\tAchievementUpdateEntry achievementUpdateEntry = new AchievementUpdateEntry();
\t\t\t\tachievementUpdateEntry.mGamer = signedInGamer;
\t\t\t\tachievementUpdateEntry.mType = type;
\t\t\t\tsignedInGamer.BeginAwardAchievement(achievementKey, new AsyncCallback(this.AwardAchievementCallback), achievementUpdateEntry);
\t\t\t}
\t\t\tcatch (Exception)
\t\t\t{
\t\t\t\treturn false;
\t\t\t}
\t\t\treturn true;
\t\t}"""
    unlock_xlive_replaced = """\t\tpublic bool UnlockAchievementXLive(string achievementKey, EAchievementType type)
\t\t{
\t\t\treturn false;
\t\t}"""
    content = content.replace(unlock_xlive, unlock_xlive_replaced)
    content = content.replace(unlock_xlive.replace("\r\n", "\n"), unlock_xlive_replaced)

    award_cb = """\t\tprotected void AwardAchievementCallback(IAsyncResult result)
\t\t{
\t\t\tAchievementUpdateEntry achievementUpdateEntry = result.AsyncState as AchievementUpdateEntry;
\t\t\tSignedInGamer mGamer = achievementUpdateEntry.mGamer;
\t\t\tif (mGamer != null)
\t\t\t{
\t\t\t\tmGamer.EndAwardAchievement(result);
\t\t\t\treturn;
\t\t\t}
\t\t\tthis.ToggleAchievement(achievementUpdateEntry.mType);
\t\t}"""
    award_cb_replaced = """\t\tprotected void AwardAchievementCallback(IAsyncResult result)
\t\t{
\t\t}"""
    content = content.replace(award_cb, award_cb_replaced)
    content = content.replace(award_cb.replace("\r\n", "\n"), award_cb_replaced)

    sync_xlive = """\t\tpublic void SyncAchievementsXLive()
\t\t{
\t\t\tif (this.m_AchievementsXLive == null)
\t\t\t{
\t\t\t\treturn;
\t\t\t}
\t\t\tforeach (Achievement achievement in this.m_AchievementsXLive)
\t\t\t{
\t\t\t\tEAchievementType eachievementType = (EAchievementType)Enum.Parse(typeof(EAchievementType), achievement.Key, false);
\t\t\t\tif (achievement.IsEarned)
\t\t\t\t{
\t\t\t\t\tthis.m_AchievementList[(int)eachievementType].m_Unlocked = true;
\t\t\t\t}
\t\t\t\telse if (this.m_AchievementList[(int)eachievementType].m_Unlocked)
\t\t\t\t{
\t\t\t\t\tthis.UnlockAchievementXLive(achievement.Key, eachievementType);
\t\t\t\t}
\t\t\t}
\t\t}"""
    sync_xlive_replaced = """\t\tpublic void SyncAchievementsXLive()
\t\t{
\t\t}"""
    content = content.replace(sync_xlive, sync_xlive_replaced)
    content = content.replace(sync_xlive.replace("\r\n", "\n"), sync_xlive_replaced)
    return content

modify_file(os.path.join(root_dir, "Achievement", "AchievementManager.cs"), modify_achievementmgr)

# 11. AchievementUpdateEntry.cs
def modify_achievementupdateentry(content):
    content = content.replace("using Microsoft.Xna.Framework.GamerServices;", "// using Microsoft.Xna.Framework.GamerServices;")
    content = content.replace("public SignedInGamer mGamer;", "// public SignedInGamer mGamer;")
    return content

modify_file(os.path.join(root_dir, "Achievement", "AchievementUpdateEntry.cs"), modify_achievementupdateentry)

# 12. Common.cs
def modify_common(content):
    content = content.replace("public static void SerializeParticleSystem(System s, DataSync sync)", "public static void SerializeParticleSystem(SexyFramework.PIL.System s, DataSync sync)")
    content = content.replace("public static System DeserializeParticleSystem(DataSync sync)", "public static SexyFramework.PIL.System DeserializeParticleSystem(DataSync sync)")
    content = content.replace("System system = System.Deserialize", "SexyFramework.PIL.System system = SexyFramework.PIL.System.Deserialize")
    return content

modify_file(os.path.join(root_dir, "Common.cs"), modify_common)

# 13. BossBulletParticleSystem.cs
def modify_bossbullet(content):
    content = content.replace("public System mSystem;", "public SexyFramework.PIL.System mSystem;")
    return content

modify_file(os.path.join(root_dir, "BossBulletParticleSystem.cs"), modify_bossbullet)

# 14. DarkFrogSequence.cs
def modify_darkfrog(content):
    content = content.replace("protected System mGenieSmoke;", "protected SexyFramework.PIL.System mGenieSmoke;")
    content = content.replace("protected System mBoilingSmoke;", "protected SexyFramework.PIL.System mBoilingSmoke;")
    content = content.replace("this.mGenieSmoke = new System(350, 50);", "this.mGenieSmoke = new SexyFramework.PIL.System(350, 50);")
    content = content.replace("this.mBoilingSmoke = new System(100, 50);", "this.mBoilingSmoke = new SexyFramework.PIL.System(100, 50);")
    content = content.replace("new System.FPSCallback(System.FadeParticlesFPSCallback)", "new SexyFramework.PIL.System.FPSCallback(SexyFramework.PIL.System.FadeParticlesFPSCallback)")
    return content

modify_file(os.path.join(root_dir, "DarkFrogSequence.cs"), modify_darkfrog)

# 15. Walk all .cs files recursively to inject type alias using Graphics = SexyFramework.Graphics.Graphics;
for root, dirs, files in os.walk(root_dir):
    if "bin" in dirs:
        dirs.remove("bin")
    if "obj" in dirs:
        dirs.remove("obj")
    
    for filename in files:
        if filename.endswith(".cs") and filename != "Graphics.cs" and filename != "refactor.py":
            filepath = os.path.join(root, filename)
            try:
                with open(filepath, 'r', encoding='utf-8') as f:
                    content = f.read()
            except UnicodeDecodeError:
                # Try with other encodings or ignore binary-like files
                try:
                    with open(filepath, 'r', encoding='latin1') as f:
                        content = f.read()
                except Exception as e:
                    print(f"Skipping {filepath} due to read error: {e}")
                    continue
            
            # Check if it has using SexyFramework.Graphics;
            # AND does not already have using Graphics =
            if "using SexyFramework.Graphics;" in content and "using Graphics = SexyFramework.Graphics.Graphics;" not in content:
                target = "using SexyFramework.Graphics;"
                idx = content.find(target)
                if idx != -1:
                    insert_pos = idx + len(target)
                    new_content = content[:insert_pos] + "\nusing Graphics = SexyFramework.Graphics.Graphics;" + content[insert_pos:]
                    with open(filepath, 'w', encoding='utf-8') as f:
                        f.write(new_content)
                    print(f"Added Graphics type alias to {filename}")

# 16. AppResources.cs — EditorBrowsable(2) is invalid on modern .NET
def modify_appresources(content):
    return content.replace("[EditorBrowsable(2)]", "[EditorBrowsable(EditorBrowsableState.Never)]")

modify_file(os.path.join(root_dir, "AppResources.cs"), modify_appresources)

# 17. GameApp.cs — Profile type vs ZumasRevenge.Profile namespace
def modify_gameapp_profile(content):
    return content.replace(
        "public Profile m_Profile = new Profile();",
        "public ZumasRevenge.Profile.Profile m_Profile = new ZumasRevenge.Profile.Profile();",
    )

modify_file(os.path.join(root_dir, "GameApp.cs"), modify_gameapp_profile)

# 18. Gun.cs — PIL particle System vs System namespace
def modify_gun(content):
    content = content.replace("this.mDizzyStars = new System(50, 50);", "this.mDizzyStars = new SexyFramework.PIL.System(50, 50);")
    content = content.replace("private System mDizzyStars;", "private SexyFramework.PIL.System mDizzyStars;")
    return content

modify_file(os.path.join(root_dir, "Gun.cs"), modify_gun)

print("Refactoring script finished execution.")
