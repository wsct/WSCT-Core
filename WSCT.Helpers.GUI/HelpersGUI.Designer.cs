namespace WSCT.Helpers.GUI
{
    partial class HelpersGui
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.SplitContainer splitInputOutputContainer;
            this.buttonAnalyzeSshPrivateKey = new System.Windows.Forms.Button();
            this.buttonAnalyzeSshPublicKey = new System.Windows.Forms.Button();
            this.buttonHexaToBase64 = new System.Windows.Forms.Button();
            this.buttonHexaToBCD = new System.Windows.Forms.Button();
            this.buttonBase64ToHexa = new System.Windows.Forms.Button();
            this.buttonHexaToString = new System.Windows.Forms.Button();
            this.buttonBCDToHexa = new System.Windows.Forms.Button();
            this.buttonStringToHexa = new System.Windows.Forms.Button();
            this.buttonXmlToTlv = new System.Windows.Forms.Button();
            this.buttonAnalyzeTLV = new System.Windows.Forms.Button();
            this.buttonTlvToXml = new System.Windows.Forms.Button();
            this.textSource = new System.Windows.Forms.TextBox();
            this.textTarget = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            splitInputOutputContainer = new System.Windows.Forms.SplitContainer();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitInputOutputContainer)).BeginInit();
            splitInputOutputContainer.Panel1.SuspendLayout();
            splitInputOutputContainer.Panel2.SuspendLayout();
            splitInputOutputContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 13);
            label1.TabIndex = 0;
            label1.Text = "Source value";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 13);
            label2.TabIndex = 0;
            label2.Text = "Interpreted value";
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(505, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(119, 444);
            panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox3.AutoSize = true;
            groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBox3.Controls.Add(this.buttonAnalyzeSshPrivateKey);
            groupBox3.Controls.Add(this.buttonAnalyzeSshPublicKey);
            groupBox3.Location = new System.Drawing.Point(3, 340);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(113, 90);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "SSH";
            // 
            // buttonAnalyzeSshPrivateKey
            // 
            this.buttonAnalyzeSshPrivateKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalyzeSshPrivateKey.Location = new System.Drawing.Point(4, 48);
            this.buttonAnalyzeSshPrivateKey.Name = "buttonAnalyzeSshPrivateKey";
            this.buttonAnalyzeSshPrivateKey.Size = new System.Drawing.Size(100, 23);
            this.buttonAnalyzeSshPrivateKey.TabIndex = 1;
            this.buttonAnalyzeSshPrivateKey.Text = "Private Key";
            this.buttonAnalyzeSshPrivateKey.UseVisualStyleBackColor = true;
            this.buttonAnalyzeSshPrivateKey.Click += new System.EventHandler(this.buttonAnalyzeSshPrivateKey_Click);
            // 
            // buttonAnalyzeSshPublicKey
            // 
            this.buttonAnalyzeSshPublicKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalyzeSshPublicKey.Location = new System.Drawing.Point(4, 19);
            this.buttonAnalyzeSshPublicKey.Name = "buttonAnalyzeSshPublicKey";
            this.buttonAnalyzeSshPublicKey.Size = new System.Drawing.Size(100, 23);
            this.buttonAnalyzeSshPublicKey.TabIndex = 0;
            this.buttonAnalyzeSshPublicKey.Text = "Public Key";
            this.buttonAnalyzeSshPublicKey.UseVisualStyleBackColor = true;
            this.buttonAnalyzeSshPublicKey.Click += new System.EventHandler(this.buttonAnalyzeSshPublicKey_Click);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(this.buttonHexaToBase64);
            groupBox2.Controls.Add(this.buttonHexaToBCD);
            groupBox2.Controls.Add(this.buttonBase64ToHexa);
            groupBox2.Controls.Add(this.buttonHexaToString);
            groupBox2.Controls.Add(this.buttonBCDToHexa);
            groupBox2.Controls.Add(this.buttonStringToHexa);
            groupBox2.Location = new System.Drawing.Point(3, 128);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(113, 206);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hexa";
            // 
            // buttonHexaToBase64
            // 
            this.buttonHexaToBase64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexaToBase64.Location = new System.Drawing.Point(7, 135);
            this.buttonHexaToBase64.Name = "buttonHexaToBase64";
            this.buttonHexaToBase64.Size = new System.Drawing.Size(100, 23);
            this.buttonHexaToBase64.TabIndex = 4;
            this.buttonHexaToBase64.Text = "Hexa To Base64";
            this.buttonHexaToBase64.UseVisualStyleBackColor = true;
            this.buttonHexaToBase64.Click += new System.EventHandler(this.buttonHexaToBase64_Click);
            // 
            // buttonHexaToBCD
            // 
            this.buttonHexaToBCD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexaToBCD.Location = new System.Drawing.Point(7, 77);
            this.buttonHexaToBCD.Name = "buttonHexaToBCD";
            this.buttonHexaToBCD.Size = new System.Drawing.Size(100, 23);
            this.buttonHexaToBCD.TabIndex = 2;
            this.buttonHexaToBCD.Text = "Hexa to BCD";
            this.buttonHexaToBCD.UseVisualStyleBackColor = true;
            this.buttonHexaToBCD.Click += new System.EventHandler(this.buttonHexaToBCD_Click);
            // 
            // buttonBase64ToHexa
            // 
            this.buttonBase64ToHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBase64ToHexa.Location = new System.Drawing.Point(7, 164);
            this.buttonBase64ToHexa.Name = "buttonBase64ToHexa";
            this.buttonBase64ToHexa.Size = new System.Drawing.Size(100, 23);
            this.buttonBase64ToHexa.TabIndex = 5;
            this.buttonBase64ToHexa.Text = "Base64 to Hexa";
            this.buttonBase64ToHexa.UseVisualStyleBackColor = true;
            this.buttonBase64ToHexa.Click += new System.EventHandler(this.buttonBase64ToHexa_Click);
            // 
            // buttonHexaToString
            // 
            this.buttonHexaToString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexaToString.Location = new System.Drawing.Point(7, 19);
            this.buttonHexaToString.Name = "buttonHexaToString";
            this.buttonHexaToString.Size = new System.Drawing.Size(100, 23);
            this.buttonHexaToString.TabIndex = 0;
            this.buttonHexaToString.Text = "Hexa to String";
            this.buttonHexaToString.UseVisualStyleBackColor = true;
            this.buttonHexaToString.Click += new System.EventHandler(this.buttonHexaToString_Click);
            // 
            // buttonBCDToHexa
            // 
            this.buttonBCDToHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBCDToHexa.Location = new System.Drawing.Point(7, 106);
            this.buttonBCDToHexa.Name = "buttonBCDToHexa";
            this.buttonBCDToHexa.Size = new System.Drawing.Size(100, 23);
            this.buttonBCDToHexa.TabIndex = 3;
            this.buttonBCDToHexa.Text = "BCD to Hexa";
            this.buttonBCDToHexa.UseVisualStyleBackColor = true;
            this.buttonBCDToHexa.Click += new System.EventHandler(this.buttonBCDToHexa_Click);
            // 
            // buttonStringToHexa
            // 
            this.buttonStringToHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStringToHexa.Location = new System.Drawing.Point(7, 48);
            this.buttonStringToHexa.Name = "buttonStringToHexa";
            this.buttonStringToHexa.Size = new System.Drawing.Size(100, 23);
            this.buttonStringToHexa.TabIndex = 1;
            this.buttonStringToHexa.Text = "String to Hexa";
            this.buttonStringToHexa.UseVisualStyleBackColor = true;
            this.buttonStringToHexa.Click += new System.EventHandler(this.buttonStringToHexa_Click);
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(this.buttonXmlToTlv);
            groupBox1.Controls.Add(this.buttonAnalyzeTLV);
            groupBox1.Controls.Add(this.buttonTlvToXml);
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(113, 119);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "TLV";
            // 
            // buttonXmlToTlv
            // 
            this.buttonXmlToTlv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXmlToTlv.Location = new System.Drawing.Point(7, 77);
            this.buttonXmlToTlv.Name = "buttonXmlToTlv";
            this.buttonXmlToTlv.Size = new System.Drawing.Size(100, 23);
            this.buttonXmlToTlv.TabIndex = 2;
            this.buttonXmlToTlv.Text = "XML to TLV";
            this.buttonXmlToTlv.UseVisualStyleBackColor = true;
            this.buttonXmlToTlv.Click += new System.EventHandler(this.buttonXmlToTlv_Click);
            // 
            // buttonAnalyzeTLV
            // 
            this.buttonAnalyzeTLV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalyzeTLV.Location = new System.Drawing.Point(7, 19);
            this.buttonAnalyzeTLV.Name = "buttonAnalyzeTLV";
            this.buttonAnalyzeTLV.Size = new System.Drawing.Size(100, 23);
            this.buttonAnalyzeTLV.TabIndex = 0;
            this.buttonAnalyzeTLV.Text = "Analyze TLV";
            this.buttonAnalyzeTLV.UseVisualStyleBackColor = true;
            this.buttonAnalyzeTLV.Click += new System.EventHandler(this.buttonAnalyzeTLV_Click);
            // 
            // buttonTlvToXml
            // 
            this.buttonTlvToXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTlvToXml.Location = new System.Drawing.Point(7, 48);
            this.buttonTlvToXml.Name = "buttonTlvToXml";
            this.buttonTlvToXml.Size = new System.Drawing.Size(100, 23);
            this.buttonTlvToXml.TabIndex = 1;
            this.buttonTlvToXml.Text = "TLV to XML";
            this.buttonTlvToXml.UseVisualStyleBackColor = true;
            this.buttonTlvToXml.Click += new System.EventHandler(this.buttonTlvToXml_Click);
            // 
            // splitInputOutputContainer
            // 
            splitInputOutputContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            splitInputOutputContainer.Location = new System.Drawing.Point(0, 0);
            splitInputOutputContainer.Name = "splitInputOutputContainer";
            splitInputOutputContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitInputOutputContainer.Panel1
            // 
            splitInputOutputContainer.Panel1.Controls.Add(label1);
            splitInputOutputContainer.Panel1.Controls.Add(this.textSource);
            // 
            // splitInputOutputContainer.Panel2
            // 
            splitInputOutputContainer.Panel2.Controls.Add(label2);
            splitInputOutputContainer.Panel2.Controls.Add(this.textTarget);
            splitInputOutputContainer.Size = new System.Drawing.Size(499, 442);
            splitInputOutputContainer.SplitterDistance = 151;
            splitInputOutputContainer.SplitterWidth = 8;
            splitInputOutputContainer.TabIndex = 0;
            // 
            // textSource
            // 
            this.textSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSource.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSource.Location = new System.Drawing.Point(3, 25);
            this.textSource.Multiline = true;
            this.textSource.Name = "textSource";
            this.textSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textSource.Size = new System.Drawing.Size(493, 123);
            this.textSource.TabIndex = 1;
            this.textSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSource_KeyDown);
            // 
            // textTarget
            // 
            this.textTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTarget.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTarget.Location = new System.Drawing.Point(3, 16);
            this.textTarget.Multiline = true;
            this.textTarget.Name = "textTarget";
            this.textTarget.ReadOnly = true;
            this.textTarget.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textTarget.Size = new System.Drawing.Size(493, 248);
            this.textTarget.TabIndex = 1;
            this.textTarget.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textTarget_KeyDown);
            // 
            // HelpersGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(splitInputOutputContainer);
            this.Controls.Add(panel1);
            this.Name = "HelpersGui";
            this.Text = "Helpers GUI";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            splitInputOutputContainer.Panel1.ResumeLayout(false);
            splitInputOutputContainer.Panel1.PerformLayout();
            splitInputOutputContainer.Panel2.ResumeLayout(false);
            splitInputOutputContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(splitInputOutputContainer)).EndInit();
            splitInputOutputContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textSource;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Button buttonTlvToXml;
        private System.Windows.Forms.Button buttonAnalyzeTLV;
        private System.Windows.Forms.Button buttonHexaToString;
        private System.Windows.Forms.Button buttonStringToHexa;
        private System.Windows.Forms.Button buttonHexaToBCD;
        private System.Windows.Forms.Button buttonBCDToHexa;
        private System.Windows.Forms.Button buttonXmlToTlv;
        private System.Windows.Forms.Button buttonHexaToBase64;
        private System.Windows.Forms.Button buttonBase64ToHexa;
        private System.Windows.Forms.Button buttonAnalyzeSshPublicKey;
        private System.Windows.Forms.Button buttonAnalyzeSshPrivateKey;
    }
}

