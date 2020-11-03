using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HelpTool
{
    public partial class version2_模块小工具 : Form
    {
        public version2_模块小工具()
        {
            InitializeComponent();
        }
        string mStr_PB_Path = "";
        private void Form1_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;//使最大化窗口失效                                   
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;  //下一句用来禁止对窗口大小进行拖拽
            if (File.Exists("C:/__保存Txt数据.txt") == false)
            {
                MessageBox.Show("1.[选择prtocal的文件位置],使用此按钮,选择一下后端生成pb文件夹的路径\r\n2.后端给的CS请求,copy文本框中去,若CS有pb文件且有参数,工具会生带参数的方法 \r\n3.后端给的SC方法也只生字符串.\r\n4.[显示字符串]生成字符串在右边可手动copy走,[生成Lua脚本]在C盘根目录找\r\n5.关闭这个小工具,会在C盘根目录生成[__保存Txt数据.txt]文件 \r\n6.根目录也会生成模块名的文件夹.", "tip首次");
                return;
            }
            try
            {
                var tStr = File.ReadAllLines("C:/__保存Txt数据.txt", System.Text.Encoding.UTF8);
                var txts = tStr[0].Split('&');
                this.txtModuleName.Text = txts[0];
                this.txtCS.Text = txts[1];
                this.txtSC.Text = txts[2];
                this.txtViews.Text = txts[3];
                mStr_PB_Path = txts[4];
                if (string.IsNullOrEmpty(mStr_PB_Path) == true || Directory.Exists(mStr_PB_Path) == false)
                {
                    MessageBox.Show("请选选择pb的文件路径", "pb");
                }
                txtModuleName_Leave(null, null);
                this.Text = "模块小工具_protocal的pbc文件位置_" + mStr_PB_Path;
            }
            catch (Exception)
            {
                MessageBox.Show("请选选择pb的文件路径", "pb");
            }
        }

        //---------------------------------存下旧数据------------------------------
        void SaveOldTxtValue()
        {
            var tSaveTxt = this.txtModuleName.Text.Trim() + "&" + this.txtCS.Text.Trim() + "&" + this.txtSC.Text.Trim() + "&" + this.txtViews.Text.Trim() + "&" + mStr_PB_Path;
            if (tSaveTxt.Contains("&&&&&"))
            {
                return;
            }
            File.WriteAllText("C:/__保存Txt数据.txt", tSaveTxt, new UTF8Encoding(false));
        }

        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                SaveOldTxtValue();
            }
            base.WndProc(ref msg);
        }


        #region Protocal
        void LoadProtocalTxt()
        {
            StringBuilder sbCSRes = new StringBuilder();
            StringBuilder sbCSForMethod = new StringBuilder();
            if (string.IsNullOrEmpty(this.txtCS.Text) == false)
            {
                var tCSs = this.txtCS.Text.Trim().Split(';');
                var tListParams = GetCSParams();

                for (int i = 0; i < tCSs.Length; i++)
                {
                    if (File.Exists(mStr_PB_Path + tCSs[i].Trim() + "_pb.lua"))
                    {
                        //  sbCSRes.AppendLine(string.Format(mNewCSStr, tCSs[i].Trim()));
                    }

                    var tCSParams = tListParams[i];
                    if (string.IsNullOrEmpty(tCSParams) == false)
                    {
                        var tCSData3s = tCSParams.Split(',');
                        StringBuilder sbData3 = new StringBuilder();
                        for (int j = 0; j < tCSData3s.Length; j++)
                        {
                            sbData3.AppendLine(string.Format(m_CSMethod_FileParams, tCSData3s[j], tCSs[i].Trim()));
                        }
                        sbCSForMethod.AppendLine(string.Format(m_CSMethod_File, mProtocalName, tCSs[i].Trim(), tCSParams, sbData3.ToString()));
                    }
                    else
                    {
                        sbCSForMethod.AppendLine(string.Format(m_CSMethod_NotFile, mProtocalName, tCSs[i].Trim()));
                    }
                }
            }

            StringBuilder sbSCInit = new StringBuilder();
            StringBuilder sbSCMethod = new StringBuilder();
            if (string.IsNullOrEmpty(this.txtSC.Text) == false)
            {
                var tSCs = this.txtSC.Text.Split(';');
                for (int i = 0; i < tSCs.Length; i++)
                {
                    sbSCInit.AppendLine(string.Format(mSCAddRegister, tSCs[i].Trim()));
                    Console.WriteLine("cwww", mProtocalName, tSCs[i]);

                    if (File.Exists(mStr_PB_Path + tSCs[i].Trim() + "_pb.lua"))
                    {
                        sbSCMethod.AppendLine(string.Format(mSCMethod_File, mProtocalName, tSCs[i]));
                    }
                    else
                    {
                        sbSCMethod.AppendLine(string.Format(mSCMethod_NotFile, mProtocalName, tSCs[i]));
                    }
                }
            }


            if (string.IsNullOrEmpty(mProtocalName) == false)
            {
                string str = sbCSRes.ToString() + sbSCInit.ToString();
                string strMethod = "\r\n----------CS区域----------\r\n" + sbCSForMethod.ToString() + "\r\n----------SC区域----------\r\n" + sbSCMethod.ToString();
                mGenerate = string.Format(mInstanceStr, mProtocalName, str, strMethod);
            }
        }
        private void btnGenerateLua_Click(object sender, EventArgs e)
        {
            LoadProtocalTxt();
            File.WriteAllText(mLuaPathDir_protocal, mGenerate, new UTF8Encoding(false));
        }
        string mGenerate;
        string mInstanceStr = "{0} = {0} or BaseClass(ProtocalBase)\r\nfunction {0}:__init()\r\n{1}   {0}.instance = self\r\nend\r\n\r\nfunction {0}:GetInstance()\r\n   if not {0}.instance then\r\n      {0}.New()\r\n   end\r\n   return {0}.instance\r\nend\r\nfunction {0}:Destroy()\r\n   if {0}.instance then\r\n     {0}.instance:DeleteMe()\r\n     {0}.instance = nil\r\n  end\r\nend\r\n{2}\r\n";
        string mSCAddRegister = "    self:RegisterProtocal(g_MsgID.{0},\"{0}\")--{0}监听 ";


        string mSCMethod_File = "--g_MsgID.{1}\r\n function {0}:{1}(dataType, content)\r\n   local dataLog= require(\"gamenet.protocal.pbc.{1}_pb\").{1}()  dataLog:ParseFromString(content)     loggZSX(\"{1}\",dataLog) \r\n   local data = self:decode_pbc('com.server.game.protobuf.proto.{1}', content) \r\n  --todo\r\n end";
        string mSCMethod_NotFile = "--g_MsgID.{1}\r\nfunction {0}:{1}(dataType, content)\r\n  loggZSX(\"{1}消息到了\")\r\nend";


        string m_CSMethod_File = "--g_MsgID.{1}\r\nlocal {1}_table\r\nfunction {0}:{1}({2})\r\n  if not {1}_table then\r\n     {1}_table={{}}\r\n   end \r\n   {3}  self:Send_pbc(GameNet.Instance:GetLoginNetId(), g_MsgID.{1},'com.server.game.protobuf.proto.{1}',{1}_table)\r\nend";
        string m_CSMethod_FileParams = "{1}_table.{0}={0}";

        string m_CSMethod_NotFile = "--g_MsgID.{1}\r\nfunction {0}:{1}()\r\n    self:Send_pbc(GameNet.Instance:GetLoginNetId(),g_MsgID.{1},'com.server.game.protobuf.proto.{1}',{{}})\r\nend";


        private void btnGenerateStr_Click(object sender, EventArgs e)
        {
            LoadProtocalTxt();
            this.rtbTxt.Text = mGenerate;
        }
        #endregion

        //-------------------------------页面-------------------------
        #region View          
        string mViewLua = "local UIView = require('UI.UIView')\r\nlocal {0} = fgui.window_class(UIView)\r\nlocal EventManager = require('Event.EventManager')\r\nfunction {0}:OnShown()\r\n    --EventManager.Register()\r\nend\r\n\r\nfunction {0}:OnHide()\r\n    --EventManager.UnRegister()\r\nend\r\n\r\nfunction {0}:OnLoadFinished()\r\n\r\nend\r\n\r\nfunction {0}:RefreshViewAll()\r\n\r\nend\r\n\r\nfunction {0}:OnNetMessage(moduleID, msgID, data)\r\n\r\nend\r\nreturn {0}\r\n\r\n\r\n\r\n\r\n--local {1} = {{}}\r\n--{1}[UI_CONFIG_CONTEXT_CLASS_NAME] = '{2}.{0}'\r\n--{1}[UI_CONFIG_CONTEXT_PAKAGE_NAME] = '{2}'\r\n--{1}[UI_CONFIG_CONTEXT_PANEL_NAME] = '{0}'\r\n--{1}[UI_CONFIG_CONTEXT_PARET_LAYER] = UI_LAYER_MAIN\r\n--{1}[UI_CONFIG_CONTEXT_SORTING_ORDER] = 1\r\n--{1}[UI_CONFIG_CONTEXT_CACHE_TIME] = 3\r\n--uiConfig.{0} = {1}";
        private void btnViewGenStr_Click(object sender, EventArgs e)
        {
            StringBuilder mSbStrView = new StringBuilder();
            if (string.IsNullOrEmpty(this.txtViews.Text.Trim()) == false)
            {
                var tViewStrs = this.txtViews.Text.Trim().Split(';');
                for (int i = 0; i < tViewStrs.Length; i++)
                {
                    var name= tViewStrs[i].First().ToString().ToLower() + tViewStrs[i].Substring(1);
                    mSbStrView.AppendLine(string.Format(mViewLua, tViewStrs[i].Trim(),name,this.txtModuleName.Text));
                }
                this.rtbTxt.Text = mSbStrView.ToString();
            }
        }


        private void btnLuaViewGen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtViews.Text.Trim()) == false)
            {
                var tViewStrs = this.txtViews.Text.Trim().Split(';');
                for (int i = 0; i < tViewStrs.Length; i++)
                {
                    var tContent = string.Format(mViewLua, tViewStrs[i]);
                    File.WriteAllText(string.Format("{0}/{1}.lua", m__PathDir, tViewStrs[i].Trim()), tContent, new UTF8Encoding(false));//utf-bom  utf8
                }
            }
        }
        #endregion

        //----------------------proxy----------------
        #region Proxy
        string mStrStart = "local {0} ={{}}\r\nlocal CustomUIConfig = require('UI.View.CustomUIConfig')\r\nlocal UIManager = require('UI.UIManager')\r\n--require('Recruit.ProxyRecruitModule')\r\n\r\n";
        string mStrLong = "function {0}:OpenView{3}()\r\n  UIManager.OpenWindow(CustomUIConfig.{2})\r\nend\r\nfunction {0}:CloseView{3}()\r\n  UIManager.CloseWindow(CustomUIConfig.{2})\r\nend\r\nfunction {0}:OpenView{3}Data(data)UIManager.OpenWindow(CustomUIConfig.{2},function (code,view)view:SetData(data)end)end\r\n\r\n";
        //local {4} =require(\"game.{1}.{2}\").New()\r\n   {4}:SetData()\r\n   {4}:OpenView()\r\n
        string mEndLong = "return {0}";
        string LoadProxyStrMethod()
        {
            var startStr = string.Format(mStrStart, mProxyModuleName);
            var endStr = string.Format(mEndLong, mProxyModuleName);
            StringBuilder forStr = new StringBuilder();
            if (string.IsNullOrEmpty(this.txtViews.Text.Trim()) == false)
            {
                var tViewStrs = this.txtViews.Text.Trim().Split(';');
                for (int i = 0; i < tViewStrs.Length; i++)
                {
                    var tMethodName = tViewStrs[i].Trim().Replace("View", "");
                    var tSelfName = FirstCharToLower(tMethodName);
                    forStr.AppendLine(string.Format(mStrLong, mProxyModuleName, this.txtModuleName.Text, tViewStrs[i].Trim(), tMethodName, tSelfName));
                }
            }
            return startStr + forStr.ToString()+ endStr;
        }
        private void btnProxyGenStr_Click(object sender, EventArgs e)
        {
            this.rtbTxt.Text = LoadProxyStrMethod();
        }
        public string FirstCharToLower(string pStr)
        {
            if (string.IsNullOrEmpty(pStr))
                return pStr;
            string str = pStr.First().ToString().ToLower() + pStr.Substring(1);
            return str;
        }
        private void btnLuaGen_Click(object sender, EventArgs e)
        {
            File.WriteAllText(mLuaPahtDir_proxyModule, LoadProxyStrMethod(), new UTF8Encoding(false));
        }
        #endregion


        //---------------------------Manager------------------
        #region Manager
        string mManagerLua = "local {0} = {{}} \r\nfunction {0}:Get{0}Config()\r\n    --return \r\nend   \r\nreturn {0}";
        string GetGenStr()
        {
            var str = string.Format(mManagerLua, mMangerName);
            return str;
        }
        private void btnShowStr_Click(object sender, EventArgs e)
        {
            this.rtbTxt.Text = GetGenStr();
        }
        private void btnGenManager_Click(object sender, EventArgs e)
        {
            File.WriteAllText(mLuaPathDir_manager, GetGenStr(), new UTF8Encoding(false));
        }
        #endregion


        #region 生成Path 
        string mMangerName = "";
        string mProtocalName = "";
        string mProxyModuleName = "";
        string m__PathDir = "";//-----------------------
        string mLuaPathDir_manager = "";
        string mLuaPathDir_protocal = "";
        string mLuaPahtDir_proxyModule = "";
        private void txtModuleName_Leave(object sender, EventArgs e)
        {
            mMangerName = this.txtModuleName.Text.Trim() + "Manager";
            mProtocalName = "Protocal" + this.txtModuleName.Text.Trim();
            mProxyModuleName = "Proxy" + this.txtModuleName.Text.Trim() + "Module";
            m__PathDir = string.Format("C:/__{0}", this.txtModuleName.Text.Trim());
            if (Directory.Exists(m__PathDir) == false)
            {
                Directory.CreateDirectory(m__PathDir);
            }
            mLuaPathDir_manager = m__PathDir + "/" + mMangerName + ".lua";
            mLuaPathDir_protocal = m__PathDir + "/" + mProtocalName + ".lua";
            mLuaPahtDir_proxyModule = m__PathDir + "/" + mProxyModuleName + ".lua";
        }

        #endregion

        private void btnGenAll_Click(object sender, EventArgs e)
        {
            btnGenerateLua_Click(null, null);
            btnLuaViewGen_Click(null, null);
            btnLuaGen_Click(null, null);
            btnGenManager_Click(null, null);
        }


        List<string> GetCSParams()
        {
            var tListParams = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(this.txtCS.Text) == false)
                {
                    var tCSs = this.txtCS.Text.Trim().Split(';');
                    for (int i = 0; i < tCSs.Length; i++)
                    {
                        string oneParams = "";
                        if (File.Exists(mStr_PB_Path + tCSs[i].Trim() + "_pb.lua"))
                        {
                            var tAllStr = File.ReadAllText(mStr_PB_Path + tCSs[i].Trim() + "_pb.lua", System.Text.Encoding.UTF8);
                            var tCountName = tAllStr.Split(new string[] { ".name = \"" }, StringSplitOptions.None);
                            for (int j = 1; j < tCountName.Length - 1; j++)
                            {
                                var tParams = tCountName[j].Split(new string[] { "\"" }, StringSplitOptions.None)[0];
                                Console.WriteLine("参数 =" + tParams);
                                oneParams += tParams + ",";
                            }
                        }
                        else
                        {

                        }
                        tListParams.Add(oneParams.TrimEnd(','));
                    }

                    return tListParams;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("错误的Pb文件");
                // throw;
            }
            return tListParams;
        }

        private void btnSelectLua_CS_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                mStr_PB_Path = dialog.SelectedPath + "\\";
            }
        }

        //以下是test
        private void testBtn_Click(object sender, EventArgs e)
        {
            List<string> args = new List<string>() { @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\AccompanyBeast\ProtocalAccompanyBeast.lua",
                 @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\BurnExpedition\ProtocalBurnExpedition.lua",
                  @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\CarnivalNight\ProtocalCarnivalNight.lua",
                 @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\DragonTreasure\ProtocalDragonTreasure.lua",
                  @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\Train\ProtocalTrain.lua",
                  @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\Reexamine\ProtocalReexamine.lua",
                @"F:\Wind2Project\client_branch\LuaScripts\lua_source\game\Ranking\ProtocalRanking.lua"


            };
            if (args == null)
                return;

            List<string> pathList = new List<string>();

            var path = args[6];
            if (File.Exists(path))
            {
                pathList.Add(path);
            }
            else if (Directory.Exists(path))
            {
                var arr = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);
                pathList.AddRange(arr);
            }

            int modifyCount = 0;
            pathList.ForEach(filePath =>
            {
                if (filePath.Contains("gamenet/protocal/pbc"))
                    return;

                string txt = File.ReadAllText(filePath);
                if (txt.Contains("gamenet.protocal.pbc"))
                {
                    txt = Process(txt);
                    File.WriteAllText(filePath, txt);
                    modifyCount++;
                }
            });

            Console.WriteLine(pathList.Count + " file " + modifyCount + " processed");

        }
        static Regex r1 = new Regex(@"(self\.\w+) *= *require\((.*?)\)\.\w+.*\r?\n *");
        static Regex r2 = new Regex(@"local *(\w+) *= *require *\((.*?)\)\.\w+.*?\r?\n *");

        static string Process(string txt)
        {
            var mc = r2.Matches(txt);
            var list = new List<Match>();
            foreach (Match m in mc)
            {
                list.Add(m);
            }

            list.Sort((a, b) => b.Index - a.Index);
            list.ForEach(m =>
            {
                string identify = m.Groups[1].Value;
                string proto = m.Groups[2].Value;
                ReplaceCode(ref txt, identify, proto, m.Index, m.Length);
            });

            mc = r1.Matches(txt);
            list.Clear();
            foreach (Match match in mc)
            {
                list.Add(match);
            }

            list.Sort((a, b) => b.Index - a.Index);
            list.ForEach(m =>
            {
                string identify = m.Groups[1].Value;
                string proto = m.Groups[2].Value;

                ReplaceCode(ref txt, identify, proto, m.Index, m.Length);
            });

            return txt;
        }

        private static string template = @"local {0}Func = require({1}).{0}
    local {2} = {0}Func()";

        private static string template2 = @"--local {0}Func = require({1}).{0}
    --local {2} = self.{0}Func()";

        private static int iiii = 0;

        static void ReplaceCode(ref string txt, string identify, string protoPath, int index, int length)
        {
            int a = protoPath.LastIndexOf(".", StringComparison.Ordinal);
            int b = protoPath.LastIndexOf("_", StringComparison.Ordinal);
            string protocalName = protoPath.Substring(a + 1, b - a - 1);

            txt = txt.Remove(index, length);

            Regex r = new Regex($@"local *(\w+) *= *{identify.Replace(".", @"\.")} *\( *\)");

            int start = 0;
            while (true)
            {
                var match = r.Match(txt, start);
                if (match == Match.Empty)
                {
                    break;
                }

                txt = txt.Remove(match.Index, match.Length);
                var localFielName = match.Groups[1].Value;

                string replaceStr;

                bool isComment = false;

                var j = match.Index - 1;
                while (true)
                {
                    if (txt[j] == ' ')
                    {
                        j--;
                        continue;
                    }

                    if (txt[j] == '-' && txt[j - 1] == '-')
                    {
                        isComment = true;
                    }
                    break;
                }

                if (isComment)
                {
                    replaceStr = string.Format(template2, protocalName, protoPath, localFielName);
                }
                else
                {
                    replaceStr = string.Format(template, protocalName, protoPath, localFielName);
                }
                txt = txt.Insert(match.Index, replaceStr);
                start = match.Index + replaceStr.Length;

                iiii++;
                //File.WriteAllText(iiii.ToString() + ".txt", txt);
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            string v_OpenFolderPath = @"C:\__" + txtModuleName.Text.Trim();
            System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
        }
    }
}
