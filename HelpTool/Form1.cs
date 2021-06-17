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
                    var tAnnotation = string.Empty;
                    if (File.Exists(mStr_PB_Path + tCSs[i].Trim() + ".proto"))
                    {
                        Console.WriteLine(mStr_PB_Path + tCSs[i].Trim() + ".proto");
                        var tStr = File.ReadAllLines(mStr_PB_Path + tCSs[i].Trim() + ".proto");
                        string str = string.Empty;
                        string item = string.Empty;
                        for (int tt = 0; tt < tStr.Length; tt++)
                        {
                            item = tStr[tt];
                            if (item.Contains("repeated") || item.Contains("optional"))
                            {
                                var reg = Regex.Replace(item, @"[\s]+", "~");
                                var value = (reg.Split('~')[3]);
                                str = value + ",";
                            }
                            if (item.Contains("message"))
                            {
                                tAnnotation = tStr[tt - 1];
                            }
                        }
                        str = str.TrimEnd(',');
                        Console.WriteLine(str);
                    }

                    var tCSParams = tListParams[i];
                    if (string.IsNullOrEmpty(tCSParams) == false)
                    {
                        var tCSData3s = tCSParams.Split(',');
                        StringBuilder sbData3 = new StringBuilder();
                        for (int j = 0; j < tCSData3s.Length; j++)
                        {
                            sbData3.Append(string.Format(m_CSMethod_FileParams, tCSData3s[j], tCSs[i].Trim()));
                        }
                        sbCSForMethod.AppendLine(string.Format(m_CSMethod_File, mProtocalName, tCSs[i].Trim(), tCSParams, sbData3.ToString(), tAnnotation));
                    }
                    else
                    {
                        sbCSForMethod.AppendLine(string.Format(m_CSMethod_NotFile, mProtocalName, tCSs[i].Trim(), tAnnotation));
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
                    //Console.WriteLine("cwww", mProtocalName, tSCs[i]);

                    if (File.Exists(mStr_PB_Path + tSCs[i].Trim() + ".proto"))
                    {
                        var tStr = File.ReadAllLines(mStr_PB_Path + tSCs[i].Trim() + ".proto");
                        string tAnnotation = string.Empty;

                        for (int tt = 0; tt < tStr.Length; tt++)
                        {
                            if (tStr[tt].Contains("message"))
                            {
                                tAnnotation = tStr[tt - 1];
                            }
                        }
                        sbSCMethod.AppendLine(string.Format(mSCMethod_File, mProtocalName, tSCs[i], tAnnotation,this.txtModuleName.Text));
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
                mGenerate = string.Format(mInstanceStr, mProtocalName, str, strMethod, this.txtModuleName.Text);
            }
        }
        private void btnGenerateLua_Click(object sender, EventArgs e)
        {
            LoadProtocalTxt();
            File.WriteAllText(mLuaPathDir_protocal, mGenerate, new UTF8Encoding(false));
        }
        string mGenerate;
        string mInstanceStr = "--local {0}=require('{3}.{0}') \r\n local g_MsgID = g_MsgID\r\n local table = table\r\n local {0} = {{}}\r\nfunction {0}:Init()\r\n return {{\r\n {1}   }}\r\nend\r\n{2}\r\n return {0}";
        string mSCAddRegister = "    [g_MsgID.{0}]=\"{0}\", ";


        //string mSCMethod_File = "--g_MsgID.{1}{2}\r\n function {0}:{1}(msg)\r\n       loggZSXWarning(\"{1} 消息到了 \"..table.tostring(msg)) \r\nend";
        string mSCMethod_File = "--g_MsgID.{1}{2}\r\n function {0}:{1}(msg)\r\n      --{3}Manager:Set{1}(msg) \r\nend";
        string mSCMethod_NotFile = "--g_MsgID.{1}\r\nfunction {0}:{1}(msg)\r\n  loggZSXWarning(\"{1} 消息到了 \"..table.tostring(msg))\r\nend";


        string m_CSMethod_File = "--g_MsgID.{1}{4}\r\nfunction {0}:{1}({2})\r\n  local data={{{3}}}   \r\n  self.send(g_MsgID.{1},data)\r\n end";
        // string m_CSMethod_File = "--g_MsgID.{1}{4}\r\nfunction {0}:{1}({2})\r\n  local data={{{3}}}   \r\n self.send(g_MsgID.{1},data)\r\n   loggZSXWarning('发送 {1} ', table.tostring(data{1}))\r\nend";
        string m_CSMethod_FileParams = "{0}={0},";

        string m_CSMethod_NotFile = "--g_MsgID.{1}{2}\r\nfunction {0}:{1}()\r\n     local data={{}}\r\n    self.send(g_MsgID.{1},data)\r\n end";


        private void btnGenerateStr_Click(object sender, EventArgs e)
        {
            LoadProtocalTxt();
            this.rtbTxt.Text = mGenerate;
        }
        #endregion

        //-------------------------------页面-------------------------
        #region View          
        string mViewLua = "local UIView = require('UI.UIView')\r\nlocal {0} = fgui.window_class(UIView)\r\nlocal EventManager = require('Event.EventManager')\r\nlocal GameEvent = require('Event.GameEvent')\r\nfunction {0}:OnShown()\r\n   UIView.OnShown(self) \r\n    --event1=EventManager.Register(GameEvent.test, handler(self, self.Test), 9)\r\nend\r\n\r\nfunction {0}:OnHide()\r\n  UIView.OnHide(self)  \r\n --    if event1 then EventManager.UnRegister(event1) event1=nil end\r\nend\r\n\r\nfunction {0}:OnLoadFinished()\r\n  self.uiComs = require('UI.Packages._{1}.UI_{0}'):OnConstruct(self.contentPane)\r\nend\r\n\r\nfunction {0}:RefreshViewAll()\r\nend\r\n\r\nfunction {0}:OnNetMessage(msgID, data)\r\nend\r\nreturn {0}\r\n\r\n--	{0} = {{\r\n--        [CLASS_NAME] = '{1}.{0}',\r\n--        [PAKAGE_NAME] = '{1}',\r\n--        [PANEL_NAME] = '{0}',\r\n--        [PARET_LAYER] = UI_LAYER_MAIN,\r\n--        [SORTING_ORDER] = 1,\r\n--        [CACHE_TIME] = 3,\r\n--        [UI_CONFIG_CONTEXT_MODAL] = true,\r\n--        [UI_TWEEN_TYPE_OPEN] = 1\r\n--   }},\r\n";
        private void btnViewGenStr_Click(object sender, EventArgs e)
        {
            StringBuilder mSbStrView = new StringBuilder();
            if (string.IsNullOrEmpty(this.txtViews.Text.Trim()) == false)
            {
                var tViewStrs = this.txtViews.Text.Trim().Split(';');
                for (int i = 0; i < tViewStrs.Length; i++)
                {
                    var name = tViewStrs[i].First().ToString().ToLower() + tViewStrs[i].Substring(1);
                    mSbStrView.AppendLine(string.Format(mViewLua, tViewStrs[i].Trim(), this.txtModuleName.Text));
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
                    var tContent = string.Format(mViewLua, tViewStrs[i], this.txtModuleName.Text);
                    File.WriteAllText(string.Format("{0}/{1}.lua", m__PathDir, tViewStrs[i].Trim()), tContent, new UTF8Encoding(false));//utf-bom  utf8
                }
            }
        }
        #endregion

        //----------------------proxy----------------
        #region Proxy
        string mStrStart = "--  local {0}=require('{1}.{0}')\r\nlocal {0} ={{}}\r\nlocal CustomUIConfig = require('ViewConfig.CustomUIConfig')\r\nlocal UIManager = require('UI.UIManager')\r\n\r\n";
        string mStrLong = "function {0}:OpenView{3}()\r\n  UIManager.OpenWindow(CustomUIConfig.{2})\r\nend\r\nfunction {0}:CloseView{3}()\r\n  UIManager.CloseWindow(CustomUIConfig.{2},true)\r\nend\r\nfunction {0}:OpenView{3}Data(data)UIManager.OpenWindow(CustomUIConfig.{2},function (code,view)view:SetData(data)end)end\r\n\r\n";
        //local {4} =require(\"game.{1}.{2}\").New()\r\n   {4}:SetData()\r\n   {4}:OpenView()\r\n
        string mEndLong = "return {0}";
        string LoadProxyStrMethod()
        {
            var startStr = string.Format(mStrStart, mProxyModuleName, this.txtModuleName.Text);
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
            return startStr + forStr.ToString() + endStr;
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
                        if (File.Exists(mStr_PB_Path + tCSs[i].Trim() + ".proto"))
                        {

                            var tStr = File.ReadAllLines(mStr_PB_Path + tCSs[i].Trim() + ".proto");

                            foreach (string item in tStr)
                            {
                                if (item.Contains("repeated") || item.Contains("optional"))
                                {
                                    var reg = Regex.Replace(item, @"[\s]+", "~");
                                    var value = (reg.Split('~')[3]);
                                    oneParams += value + ",";
                                }
                            }
                            oneParams = oneParams.TrimEnd(',');
                            Console.WriteLine(oneParams);
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
        string formats = "local {0}Tab = require('Tables.{0}Config')\r\n";
        private void tabButton_Click(object sender, EventArgs e)
        {
            var values = this.tableTxt.Text.Split(';');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(string.Format(formats, values[i]));
            }
            this.rtbTxt.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = "repeated GeneralCardInfo generalCards   = 1;";
            var t = Regex.Replace(a, @"[\s]+", "~");
            Console.WriteLine(t);
        }
    }
}
