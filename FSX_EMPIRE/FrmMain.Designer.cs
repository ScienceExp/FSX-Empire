namespace FSX_EMPIRE
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.TimerSimConnectRequest = new System.Windows.Forms.Timer(this.components);
            this.txtAirSpeed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAirSpeedSet = new System.Windows.Forms.Button();
            this.btnAirSpeedOff = new System.Windows.Forms.Button();
            this.btnSimRateInc = new System.Windows.Forms.Button();
            this.btnSimRateDec = new System.Windows.Forms.Button();
            this.btnManifoldPressureOff = new System.Windows.Forms.Button();
            this.btnManifoldPressureSet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtManifoldPressure = new System.Windows.Forms.TextBox();
            this.btnPropellerRPMOff = new System.Windows.Forms.Button();
            this.btnPropellerRPMSet = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPropellerRPM = new System.Windows.Forms.TextBox();
            this.btnCowlFlapsOff = new System.Windows.Forms.Button();
            this.btnCowlFlapsSet = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCowlFlaps = new System.Windows.Forms.TextBox();
            this.picGoogleEarthConnect = new System.Windows.Forms.PictureBox();
            this.FSX_SimConnect1 = new FSX_EMPIRE.FSX_SimConnect();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLerpSpeed = new System.Windows.Forms.TextBox();
            this.BtnLerpSpeed = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.webCapture1 = new WebCam.WebCapture();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TimerUpdateCamera = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picGoogleEarthConnect)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(179, 0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(69, 20);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(249, 0);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(69, 20);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // TimerSimConnectRequest
            // 
            this.TimerSimConnectRequest.Interval = 16;
            this.TimerSimConnectRequest.Tick += new System.EventHandler(this.TimerSimConnectRequest_Tick);
            // 
            // txtAirSpeed
            // 
            this.txtAirSpeed.Location = new System.Drawing.Point(108, 3);
            this.txtAirSpeed.Name = "txtAirSpeed";
            this.txtAirSpeed.Size = new System.Drawing.Size(38, 20);
            this.txtAirSpeed.TabIndex = 9;
            this.txtAirSpeed.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Air Speed";
            // 
            // btnAirSpeedSet
            // 
            this.btnAirSpeedSet.Location = new System.Drawing.Point(152, 1);
            this.btnAirSpeedSet.Name = "btnAirSpeedSet";
            this.btnAirSpeedSet.Size = new System.Drawing.Size(37, 23);
            this.btnAirSpeedSet.TabIndex = 11;
            this.btnAirSpeedSet.Text = "Set";
            this.btnAirSpeedSet.UseVisualStyleBackColor = true;
            this.btnAirSpeedSet.Click += new System.EventHandler(this.BtnAirSpeedSet_Click);
            // 
            // btnAirSpeedOff
            // 
            this.btnAirSpeedOff.Location = new System.Drawing.Point(195, 1);
            this.btnAirSpeedOff.Name = "btnAirSpeedOff";
            this.btnAirSpeedOff.Size = new System.Drawing.Size(37, 23);
            this.btnAirSpeedOff.TabIndex = 12;
            this.btnAirSpeedOff.Text = "Off";
            this.btnAirSpeedOff.UseVisualStyleBackColor = true;
            this.btnAirSpeedOff.Click += new System.EventHandler(this.BtnAirSpeedOff_Click);
            // 
            // btnSimRateInc
            // 
            this.btnSimRateInc.Location = new System.Drawing.Point(88, 148);
            this.btnSimRateInc.Name = "btnSimRateInc";
            this.btnSimRateInc.Size = new System.Drawing.Size(81, 23);
            this.btnSimRateInc.TabIndex = 13;
            this.btnSimRateInc.Text = "Sim Rate +";
            this.btnSimRateInc.UseVisualStyleBackColor = true;
            this.btnSimRateInc.Click += new System.EventHandler(this.BtnSimRateInc_Click);
            // 
            // btnSimRateDec
            // 
            this.btnSimRateDec.Location = new System.Drawing.Point(2, 148);
            this.btnSimRateDec.Name = "btnSimRateDec";
            this.btnSimRateDec.Size = new System.Drawing.Size(80, 23);
            this.btnSimRateDec.TabIndex = 14;
            this.btnSimRateDec.Text = "Sim Rate -";
            this.btnSimRateDec.UseVisualStyleBackColor = true;
            this.btnSimRateDec.Click += new System.EventHandler(this.BtnSimRateDec_Click);
            // 
            // btnManifoldPressureOff
            // 
            this.btnManifoldPressureOff.Location = new System.Drawing.Point(195, 30);
            this.btnManifoldPressureOff.Name = "btnManifoldPressureOff";
            this.btnManifoldPressureOff.Size = new System.Drawing.Size(37, 23);
            this.btnManifoldPressureOff.TabIndex = 18;
            this.btnManifoldPressureOff.Text = "Off";
            this.btnManifoldPressureOff.UseVisualStyleBackColor = true;
            this.btnManifoldPressureOff.Click += new System.EventHandler(this.BtnManifoldPressureOff_Click);
            // 
            // btnManifoldPressureSet
            // 
            this.btnManifoldPressureSet.Location = new System.Drawing.Point(152, 30);
            this.btnManifoldPressureSet.Name = "btnManifoldPressureSet";
            this.btnManifoldPressureSet.Size = new System.Drawing.Size(37, 23);
            this.btnManifoldPressureSet.TabIndex = 17;
            this.btnManifoldPressureSet.Text = "Set";
            this.btnManifoldPressureSet.UseVisualStyleBackColor = true;
            this.btnManifoldPressureSet.Click += new System.EventHandler(this.BtnManifoldPressureSet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Manifold Pressure";
            // 
            // txtManifoldPressure
            // 
            this.txtManifoldPressure.Location = new System.Drawing.Point(108, 32);
            this.txtManifoldPressure.Name = "txtManifoldPressure";
            this.txtManifoldPressure.Size = new System.Drawing.Size(38, 20);
            this.txtManifoldPressure.TabIndex = 15;
            this.txtManifoldPressure.Text = "-1";
            // 
            // btnPropellerRPMOff
            // 
            this.btnPropellerRPMOff.Location = new System.Drawing.Point(195, 57);
            this.btnPropellerRPMOff.Name = "btnPropellerRPMOff";
            this.btnPropellerRPMOff.Size = new System.Drawing.Size(37, 23);
            this.btnPropellerRPMOff.TabIndex = 22;
            this.btnPropellerRPMOff.Text = "Off";
            this.btnPropellerRPMOff.UseVisualStyleBackColor = true;
            this.btnPropellerRPMOff.Click += new System.EventHandler(this.BtnPropellerRPMOff_Click);
            // 
            // btnPropellerRPMSet
            // 
            this.btnPropellerRPMSet.Location = new System.Drawing.Point(152, 57);
            this.btnPropellerRPMSet.Name = "btnPropellerRPMSet";
            this.btnPropellerRPMSet.Size = new System.Drawing.Size(37, 23);
            this.btnPropellerRPMSet.TabIndex = 21;
            this.btnPropellerRPMSet.Text = "Set";
            this.btnPropellerRPMSet.UseVisualStyleBackColor = true;
            this.btnPropellerRPMSet.Click += new System.EventHandler(this.BtnPropellerRPMSet_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Propeller RPM";
            // 
            // txtPropellerRPM
            // 
            this.txtPropellerRPM.Location = new System.Drawing.Point(108, 59);
            this.txtPropellerRPM.Name = "txtPropellerRPM";
            this.txtPropellerRPM.Size = new System.Drawing.Size(38, 20);
            this.txtPropellerRPM.TabIndex = 19;
            this.txtPropellerRPM.Text = "-1";
            // 
            // btnCowlFlapsOff
            // 
            this.btnCowlFlapsOff.Location = new System.Drawing.Point(195, 84);
            this.btnCowlFlapsOff.Name = "btnCowlFlapsOff";
            this.btnCowlFlapsOff.Size = new System.Drawing.Size(37, 23);
            this.btnCowlFlapsOff.TabIndex = 26;
            this.btnCowlFlapsOff.Text = "Off";
            this.btnCowlFlapsOff.UseVisualStyleBackColor = true;
            this.btnCowlFlapsOff.Click += new System.EventHandler(this.BtnCowlFlapsOff_Click);
            // 
            // btnCowlFlapsSet
            // 
            this.btnCowlFlapsSet.Location = new System.Drawing.Point(152, 84);
            this.btnCowlFlapsSet.Name = "btnCowlFlapsSet";
            this.btnCowlFlapsSet.Size = new System.Drawing.Size(37, 23);
            this.btnCowlFlapsSet.TabIndex = 25;
            this.btnCowlFlapsSet.Text = "Set";
            this.btnCowlFlapsSet.UseVisualStyleBackColor = true;
            this.btnCowlFlapsSet.Click += new System.EventHandler(this.BtnCowlFlapsSet_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Cowl Flaps";
            // 
            // txtCowlFlaps
            // 
            this.txtCowlFlaps.Location = new System.Drawing.Point(108, 86);
            this.txtCowlFlaps.Name = "txtCowlFlaps";
            this.txtCowlFlaps.Size = new System.Drawing.Size(38, 20);
            this.txtCowlFlaps.TabIndex = 23;
            this.txtCowlFlaps.Text = "-1";
            // 
            // picGoogleEarthConnect
            // 
            this.picGoogleEarthConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picGoogleEarthConnect.Image = global::FSX_EMPIRE.Properties.Resources.Red_Light;
            this.picGoogleEarthConnect.Location = new System.Drawing.Point(90, 2);
            this.picGoogleEarthConnect.Name = "picGoogleEarthConnect";
            this.picGoogleEarthConnect.Size = new System.Drawing.Size(16, 16);
            this.picGoogleEarthConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGoogleEarthConnect.TabIndex = 32;
            this.picGoogleEarthConnect.TabStop = false;
            // 
            // FSX_SimConnect1
            // 
            this.FSX_SimConnect1.Location = new System.Drawing.Point(1, 2);
            this.FSX_SimConnect1.Name = "FSX_SimConnect1";
            this.FSX_SimConnect1.Size = new System.Drawing.Size(76, 17);
            this.FSX_SimConnect1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Google Earth";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(318, 224);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.txtLerpSpeed);
            this.tabPage1.Controls.Add(this.BtnLerpSpeed);
            this.tabPage1.Controls.Add(this.btnCalibrate);
            this.tabPage1.Controls.Add(this.webCapture1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(310, 198);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "WebCam";
            // 
            // txtLerpSpeed
            // 
            this.txtLerpSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLerpSpeed.Location = new System.Drawing.Point(219, 167);
            this.txtLerpSpeed.Name = "txtLerpSpeed";
            this.txtLerpSpeed.Size = new System.Drawing.Size(68, 20);
            this.txtLerpSpeed.TabIndex = 3;
            this.txtLerpSpeed.Text = "0.2";
            // 
            // BtnLerpSpeed
            // 
            this.BtnLerpSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLerpSpeed.Location = new System.Drawing.Point(140, 167);
            this.BtnLerpSpeed.Name = "BtnLerpSpeed";
            this.BtnLerpSpeed.Size = new System.Drawing.Size(73, 22);
            this.BtnLerpSpeed.TabIndex = 2;
            this.BtnLerpSpeed.Text = "Lerp Speed";
            this.BtnLerpSpeed.UseVisualStyleBackColor = true;
            this.BtnLerpSpeed.Click += new System.EventHandler(this.BtnLerpSpeed_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalibrate.Location = new System.Drawing.Point(231, 6);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(73, 22);
            this.btnCalibrate.TabIndex = 1;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // webCapture1
            // 
            this.webCapture1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webCapture1.BackColor = System.Drawing.Color.White;
            this.webCapture1.CaptureFPS = 30;
            this.webCapture1.CaptureHeight = 720;
            this.webCapture1.CaptureWidth = 1280;
            this.webCapture1.Location = new System.Drawing.Point(3, 2);
            this.webCapture1.Name = "webCapture1";
            this.webCapture1.ShowTrackerDebug = false;
            this.webCapture1.Size = new System.Drawing.Size(304, 194);
            this.webCapture1.TabIndex = 0;
            this.webCapture1.TrackerMarkerSize = 32;
            this.webCapture1.TrackerMinimumMatch = 0.3F;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnManifoldPressureSet);
            this.tabPage2.Controls.Add(this.txtAirSpeed);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnSimRateDec);
            this.tabPage2.Controls.Add(this.btnCowlFlapsOff);
            this.tabPage2.Controls.Add(this.btnSimRateInc);
            this.tabPage2.Controls.Add(this.btnAirSpeedSet);
            this.tabPage2.Controls.Add(this.btnCowlFlapsSet);
            this.tabPage2.Controls.Add(this.btnAirSpeedOff);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtManifoldPressure);
            this.tabPage2.Controls.Add(this.txtCowlFlaps);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnPropellerRPMOff);
            this.tabPage2.Controls.Add(this.btnManifoldPressureOff);
            this.tabPage2.Controls.Add(this.btnPropellerRPMSet);
            this.tabPage2.Controls.Add(this.txtPropellerRPM);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(310, 198);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CoPilot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TimerUpdateCamera
            // 
            this.TimerUpdateCamera.Interval = 34;
            this.TimerUpdateCamera.Tick += new System.EventHandler(this.TimerUpdateCamera_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 260);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picGoogleEarthConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FSX_SimConnect1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "FSX Empire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picGoogleEarthConnect)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Timer TimerSimConnectRequest;
        private FSX_SimConnect FSX_SimConnect1;
        private System.Windows.Forms.TextBox txtAirSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAirSpeedSet;
        private System.Windows.Forms.Button btnAirSpeedOff;
        private System.Windows.Forms.Button btnSimRateInc;
        private System.Windows.Forms.Button btnSimRateDec;
        private System.Windows.Forms.Button btnManifoldPressureOff;
        private System.Windows.Forms.Button btnManifoldPressureSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtManifoldPressure;
        private System.Windows.Forms.Button btnPropellerRPMOff;
        private System.Windows.Forms.Button btnPropellerRPMSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPropellerRPM;
        private System.Windows.Forms.Button btnCowlFlapsOff;
        private System.Windows.Forms.Button btnCowlFlapsSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCowlFlaps;
        private System.Windows.Forms.PictureBox picGoogleEarthConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private WebCam.WebCapture webCapture1;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Timer TimerUpdateCamera;
        private System.Windows.Forms.TextBox txtLerpSpeed;
        private System.Windows.Forms.Button BtnLerpSpeed;
    }
}

