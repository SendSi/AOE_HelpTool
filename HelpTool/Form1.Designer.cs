namespace HelpTool
{
    partial class version2_模块小工具
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerateStr = new System.Windows.Forms.Button();
            this.txtSC = new System.Windows.Forms.TextBox();
            this.监听 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCS = new System.Windows.Forms.TextBox();
            this.rtbTxt = new System.Windows.Forms.RichTextBox();
            this.btnGenerateLua = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnProxyGenStr = new System.Windows.Forms.Button();
            this.btnLuaGen = new System.Windows.Forms.Button();
            this.btnLuaViewGen = new System.Windows.Forms.Button();
            this.txtViews = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnViewGenStr = new System.Windows.Forms.Button();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.lbl_FirstShow = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenManager = new System.Windows.Forms.Button();
            this.btnShowStr = new System.Windows.Forms.Button();
            this.btnGenAll = new System.Windows.Forms.Button();
            this.btnSelectLua_CS = new System.Windows.Forms.Button();
            this.testBtn = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.Button();
            this.tableTxt = new System.Windows.Forms.TextBox();
            this.tabButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerateStr
            // 
            this.btnGenerateStr.Location = new System.Drawing.Point(12, 430);
            this.btnGenerateStr.Name = "btnGenerateStr";
            this.btnGenerateStr.Size = new System.Drawing.Size(93, 33);
            this.btnGenerateStr.TabIndex = 3;
            this.btnGenerateStr.Text = "显示字符串";
            this.btnGenerateStr.UseVisualStyleBackColor = true;
            this.btnGenerateStr.Click += new System.EventHandler(this.btnGenerateStr_Click);
            // 
            // txtSC
            // 
            this.txtSC.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtSC.Location = new System.Drawing.Point(9, 269);
            this.txtSC.Multiline = true;
            this.txtSC.Name = "txtSC";
            this.txtSC.Size = new System.Drawing.Size(423, 155);
            this.txtSC.TabIndex = 2;
            // 
            // 监听
            // 
            this.监听.AutoSize = true;
            this.监听.Location = new System.Drawing.Point(9, 252);
            this.监听.Name = "监听";
            this.监听.Size = new System.Drawing.Size(238, 14);
            this.监听.TabIndex = 16;
            this.监听.Text = "Protocal,监听SC,多条用;分隔(分号)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "Protocal,请求CS,多条用;分隔(分号)";
            // 
            // txtCS
            // 
            this.txtCS.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtCS.Location = new System.Drawing.Point(12, 91);
            this.txtCS.Multiline = true;
            this.txtCS.Name = "txtCS";
            this.txtCS.Size = new System.Drawing.Size(423, 145);
            this.txtCS.TabIndex = 1;
            // 
            // rtbTxt
            // 
            this.rtbTxt.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.rtbTxt.Location = new System.Drawing.Point(442, 12);
            this.rtbTxt.Name = "rtbTxt";
            this.rtbTxt.Size = new System.Drawing.Size(1275, 825);
            this.rtbTxt.TabIndex = 13;
            this.rtbTxt.Text = "";
            // 
            // btnGenerateLua
            // 
            this.btnGenerateLua.Location = new System.Drawing.Point(119, 430);
            this.btnGenerateLua.Name = "btnGenerateLua";
            this.btnGenerateLua.Size = new System.Drawing.Size(93, 33);
            this.btnGenerateLua.TabIndex = 4;
            this.btnGenerateLua.Text = "生成Lua脚本";
            this.btnGenerateLua.UseVisualStyleBackColor = true;
            this.btnGenerateLua.Click += new System.EventHandler(this.btnGenerateLua_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 640);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "proxy_脚本_与模块名相关";
            // 
            // btnProxyGenStr
            // 
            this.btnProxyGenStr.Location = new System.Drawing.Point(14, 658);
            this.btnProxyGenStr.Name = "btnProxyGenStr";
            this.btnProxyGenStr.Size = new System.Drawing.Size(93, 33);
            this.btnProxyGenStr.TabIndex = 8;
            this.btnProxyGenStr.Text = "显示字符串";
            this.btnProxyGenStr.UseVisualStyleBackColor = true;
            this.btnProxyGenStr.Click += new System.EventHandler(this.btnProxyGenStr_Click);
            // 
            // btnLuaGen
            // 
            this.btnLuaGen.Location = new System.Drawing.Point(125, 658);
            this.btnLuaGen.Name = "btnLuaGen";
            this.btnLuaGen.Size = new System.Drawing.Size(93, 33);
            this.btnLuaGen.TabIndex = 9;
            this.btnLuaGen.Text = "生成Lua脚本";
            this.btnLuaGen.UseVisualStyleBackColor = true;
            this.btnLuaGen.Click += new System.EventHandler(this.btnLuaGen_Click);
            // 
            // btnLuaViewGen
            // 
            this.btnLuaViewGen.Location = new System.Drawing.Point(122, 605);
            this.btnLuaViewGen.Name = "btnLuaViewGen";
            this.btnLuaViewGen.Size = new System.Drawing.Size(93, 33);
            this.btnLuaViewGen.TabIndex = 7;
            this.btnLuaViewGen.Text = "生成Lua脚本";
            this.btnLuaViewGen.UseVisualStyleBackColor = true;
            this.btnLuaViewGen.Click += new System.EventHandler(this.btnLuaViewGen_Click);
            // 
            // txtViews
            // 
            this.txtViews.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtViews.Location = new System.Drawing.Point(9, 495);
            this.txtViews.Multiline = true;
            this.txtViews.Name = "txtViews";
            this.txtViews.Size = new System.Drawing.Size(423, 98);
            this.txtViews.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 478);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "UIView_脚本名;分号隔开";
            // 
            // btnViewGenStr
            // 
            this.btnViewGenStr.Location = new System.Drawing.Point(14, 605);
            this.btnViewGenStr.Name = "btnViewGenStr";
            this.btnViewGenStr.Size = new System.Drawing.Size(93, 33);
            this.btnViewGenStr.TabIndex = 6;
            this.btnViewGenStr.Text = "显示字符串";
            this.btnViewGenStr.UseVisualStyleBackColor = true;
            this.btnViewGenStr.Click += new System.EventHandler(this.btnViewGenStr_Click);
            // 
            // txtModuleName
            // 
            this.txtModuleName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtModuleName.Location = new System.Drawing.Point(12, 28);
            this.txtModuleName.Multiline = true;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(423, 34);
            this.txtModuleName.TabIndex = 0;
            this.txtModuleName.Leave += new System.EventHandler(this.txtModuleName_Leave);
            // 
            // lbl_FirstShow
            // 
            this.lbl_FirstShow.AutoSize = true;
            this.lbl_FirstShow.Location = new System.Drawing.Point(12, 12);
            this.lbl_FirstShow.Name = "lbl_FirstShow";
            this.lbl_FirstShow.Size = new System.Drawing.Size(49, 14);
            this.lbl_FirstShow.TabIndex = 14;
            this.lbl_FirstShow.Text = "模块名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 711);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 14);
            this.label6.TabIndex = 19;
            this.label6.Text = "Manager_脚本_与模块名相关";
            // 
            // btnGenManager
            // 
            this.btnGenManager.Location = new System.Drawing.Point(125, 728);
            this.btnGenManager.Name = "btnGenManager";
            this.btnGenManager.Size = new System.Drawing.Size(93, 33);
            this.btnGenManager.TabIndex = 11;
            this.btnGenManager.Text = "生成Lua脚本";
            this.btnGenManager.UseVisualStyleBackColor = true;
            this.btnGenManager.Click += new System.EventHandler(this.btnGenManager_Click);
            // 
            // btnShowStr
            // 
            this.btnShowStr.Location = new System.Drawing.Point(14, 728);
            this.btnShowStr.Name = "btnShowStr";
            this.btnShowStr.Size = new System.Drawing.Size(93, 33);
            this.btnShowStr.TabIndex = 10;
            this.btnShowStr.Text = "显示字符串";
            this.btnShowStr.UseVisualStyleBackColor = true;
            this.btnShowStr.Click += new System.EventHandler(this.btnShowStr_Click);
            // 
            // btnGenAll
            // 
            this.btnGenAll.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btnGenAll.Location = new System.Drawing.Point(14, 780);
            this.btnGenAll.Name = "btnGenAll";
            this.btnGenAll.Size = new System.Drawing.Size(257, 51);
            this.btnGenAll.TabIndex = 12;
            this.btnGenAll.Text = "全部生成";
            this.btnGenAll.UseVisualStyleBackColor = true;
            this.btnGenAll.Click += new System.EventHandler(this.btnGenAll_Click);
            // 
            // btnSelectLua_CS
            // 
            this.btnSelectLua_CS.Location = new System.Drawing.Point(57, 7);
            this.btnSelectLua_CS.Name = "btnSelectLua_CS";
            this.btnSelectLua_CS.Size = new System.Drawing.Size(210, 22);
            this.btnSelectLua_CS.TabIndex = 20;
            this.btnSelectLua_CS.Text = "选择protocal的pbc的文件位置";
            this.btnSelectLua_CS.UseVisualStyleBackColor = true;
            this.btnSelectLua_CS.Click += new System.EventHandler(this.btnSelectLua_CS_Click);
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(325, 784);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(52, 51);
            this.testBtn.TabIndex = 21;
            this.testBtn.Text = "test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(325, 430);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(107, 33);
            this.openFile.TabIndex = 22;
            this.openFile.Text = "打开文件夹";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // tableTxt
            // 
            this.tableTxt.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tableTxt.Location = new System.Drawing.Point(248, 599);
            this.tableTxt.Multiline = true;
            this.tableTxt.Name = "tableTxt";
            this.tableTxt.Size = new System.Drawing.Size(188, 123);
            this.tableTxt.TabIndex = 23;
            // 
            // tabButton
            // 
            this.tabButton.Location = new System.Drawing.Point(343, 728);
            this.tabButton.Name = "tabButton";
            this.tabButton.Size = new System.Drawing.Size(93, 33);
            this.tabButton.TabIndex = 24;
            this.tabButton.Text = "table字符串";
            this.tabButton.UseVisualStyleBackColor = true;
            this.tabButton.Click += new System.EventHandler(this.tabButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 786);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 51);
            this.button1.TabIndex = 25;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // version2_模块小工具
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1720, 843);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabButton);
            this.Controls.Add(this.tableTxt);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.btnSelectLua_CS);
            this.Controls.Add(this.btnGenAll);
            this.Controls.Add(this.btnGenManager);
            this.Controls.Add(this.btnShowStr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_FirstShow);
            this.Controls.Add(this.txtModuleName);
            this.Controls.Add(this.btnLuaViewGen);
            this.Controls.Add(this.txtViews);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnViewGenStr);
            this.Controls.Add(this.btnLuaGen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnProxyGenStr);
            this.Controls.Add(this.btnGenerateLua);
            this.Controls.Add(this.rtbTxt);
            this.Controls.Add(this.txtCS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.监听);
            this.Controls.Add(this.txtSC);
            this.Controls.Add(this.btnGenerateStr);
            this.Name = "version2_模块小工具";
            this.Text = "模块小工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateStr;
        private System.Windows.Forms.TextBox txtSC;
        private System.Windows.Forms.Label 监听;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCS;
        private System.Windows.Forms.RichTextBox rtbTxt;
        private System.Windows.Forms.Button btnGenerateLua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnProxyGenStr;
        private System.Windows.Forms.Button btnLuaGen;
        private System.Windows.Forms.Button btnLuaViewGen;
        private System.Windows.Forms.TextBox txtViews;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnViewGenStr;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label lbl_FirstShow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGenManager;
        private System.Windows.Forms.Button btnShowStr;
        private System.Windows.Forms.Button btnGenAll;
        private System.Windows.Forms.Button btnSelectLua_CS;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.TextBox tableTxt;
        private System.Windows.Forms.Button tabButton;
        private System.Windows.Forms.Button button1;
    }
}

