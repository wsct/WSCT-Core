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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.tabTLV = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonXmlToTlv = new System.Windows.Forms.Button();
            this.buttonAnalyzeTLV = new System.Windows.Forms.Button();
            this.buttonTlvToXml = new System.Windows.Forms.Button();
            this.textTLVDecoded = new System.Windows.Forms.TextBox();
            this.textTLVHexa = new System.Windows.Forms.TextBox();
            this.tabArrayOfBytes = new System.Windows.Forms.TabPage();
            this.buttonHexaToBCD = new System.Windows.Forms.Button();
            this.buttonBCDToHexa = new System.Windows.Forms.Button();
            this.buttonHexaToString = new System.Windows.Forms.Button();
            this.buttonStringToHexa = new System.Windows.Forms.Button();
            this.textArrayOfBytesInterpreted = new System.Windows.Forms.TextBox();
            this.textArrayOfBytesSource = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.tabTLV.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabArrayOfBytes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(97, 13);
            label1.TabIndex = 0;
            label1.Text = "Hexadecimal value";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 125);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 13);
            label2.TabIndex = 4;
            label2.Text = "Interpreted value";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 3);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(70, 13);
            label3.TabIndex = 0;
            label3.Text = "Source value";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 125);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(87, 13);
            label4.TabIndex = 6;
            label4.Text = "Interpreted value";
            // 
            // tabTLV
            // 
            this.tabTLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTLV.Controls.Add(this.tabPage1);
            this.tabTLV.Controls.Add(this.tabArrayOfBytes);
            this.tabTLV.Location = new System.Drawing.Point(0, 0);
            this.tabTLV.Name = "tabTLV";
            this.tabTLV.SelectedIndex = 0;
            this.tabTLV.Size = new System.Drawing.Size(624, 444);
            this.tabTLV.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonXmlToTlv);
            this.tabPage1.Controls.Add(label2);
            this.tabPage1.Controls.Add(this.buttonAnalyzeTLV);
            this.tabPage1.Controls.Add(label1);
            this.tabPage1.Controls.Add(this.buttonTlvToXml);
            this.tabPage1.Controls.Add(this.textTLVDecoded);
            this.tabPage1.Controls.Add(this.textTLVHexa);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TLV Tools";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonXmlToTlv
            // 
            this.buttonXmlToTlv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXmlToTlv.Location = new System.Drawing.Point(510, 112);
            this.buttonXmlToTlv.Name = "buttonXmlToTlv";
            this.buttonXmlToTlv.Size = new System.Drawing.Size(100, 23);
            this.buttonXmlToTlv.TabIndex = 6;
            this.buttonXmlToTlv.Text = "XML to TLV";
            this.buttonXmlToTlv.UseVisualStyleBackColor = true;
            this.buttonXmlToTlv.Click += new System.EventHandler(this.buttonXmlToTlv_Click);
            // 
            // buttonAnalyzeTLV
            // 
            this.buttonAnalyzeTLV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalyzeTLV.Location = new System.Drawing.Point(298, 112);
            this.buttonAnalyzeTLV.Name = "buttonAnalyzeTLV";
            this.buttonAnalyzeTLV.Size = new System.Drawing.Size(100, 23);
            this.buttonAnalyzeTLV.TabIndex = 3;
            this.buttonAnalyzeTLV.Text = "Analyze TLV";
            this.buttonAnalyzeTLV.UseVisualStyleBackColor = true;
            this.buttonAnalyzeTLV.Click += new System.EventHandler(this.buttonAnalyzeTLV_Click);
            // 
            // buttonTlvToXml
            // 
            this.buttonTlvToXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTlvToXml.Location = new System.Drawing.Point(404, 112);
            this.buttonTlvToXml.Name = "buttonTlvToXml";
            this.buttonTlvToXml.Size = new System.Drawing.Size(100, 23);
            this.buttonTlvToXml.TabIndex = 2;
            this.buttonTlvToXml.Text = "TLV to XML";
            this.buttonTlvToXml.UseVisualStyleBackColor = true;
            this.buttonTlvToXml.Click += new System.EventHandler(this.buttonTlvToXml_Click);
            // 
            // textTLVDecoded
            // 
            this.textTLVDecoded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textTLVDecoded.Location = new System.Drawing.Point(6, 141);
            this.textTLVDecoded.Multiline = true;
            this.textTLVDecoded.Name = "textTLVDecoded";
            this.textTLVDecoded.ReadOnly = true;
            this.textTLVDecoded.Size = new System.Drawing.Size(604, 271);
            this.textTLVDecoded.TabIndex = 5;
            // 
            // textTLVHexa
            // 
            this.textTLVHexa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textTLVHexa.Location = new System.Drawing.Point(6, 19);
            this.textTLVHexa.Multiline = true;
            this.textTLVHexa.Name = "textTLVHexa";
            this.textTLVHexa.Size = new System.Drawing.Size(604, 87);
            this.textTLVHexa.TabIndex = 1;
            // 
            // tabArrayOfBytes
            // 
            this.tabArrayOfBytes.Controls.Add(this.buttonHexaToBCD);
            this.tabArrayOfBytes.Controls.Add(this.buttonBCDToHexa);
            this.tabArrayOfBytes.Controls.Add(this.buttonHexaToString);
            this.tabArrayOfBytes.Controls.Add(this.buttonStringToHexa);
            this.tabArrayOfBytes.Controls.Add(label4);
            this.tabArrayOfBytes.Controls.Add(this.textArrayOfBytesInterpreted);
            this.tabArrayOfBytes.Controls.Add(label3);
            this.tabArrayOfBytes.Controls.Add(this.textArrayOfBytesSource);
            this.tabArrayOfBytes.Location = new System.Drawing.Point(4, 22);
            this.tabArrayOfBytes.Name = "tabArrayOfBytes";
            this.tabArrayOfBytes.Padding = new System.Windows.Forms.Padding(3);
            this.tabArrayOfBytes.Size = new System.Drawing.Size(616, 418);
            this.tabArrayOfBytes.TabIndex = 1;
            this.tabArrayOfBytes.Text = "ArrayOfBytes";
            this.tabArrayOfBytes.UseVisualStyleBackColor = true;
            // 
            // buttonHexaToBCD
            // 
            this.buttonHexaToBCD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexaToBCD.Location = new System.Drawing.Point(192, 112);
            this.buttonHexaToBCD.Name = "buttonHexaToBCD";
            this.buttonHexaToBCD.Size = new System.Drawing.Size(100, 23);
            this.buttonHexaToBCD.TabIndex = 2;
            this.buttonHexaToBCD.Text = "Hexa to BCD";
            this.buttonHexaToBCD.UseVisualStyleBackColor = true;
            this.buttonHexaToBCD.Click += new System.EventHandler(this.buttonHexaToBCD_Click);
            // 
            // buttonBCDToHexa
            // 
            this.buttonBCDToHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBCDToHexa.Location = new System.Drawing.Point(298, 112);
            this.buttonBCDToHexa.Name = "buttonBCDToHexa";
            this.buttonBCDToHexa.Size = new System.Drawing.Size(100, 23);
            this.buttonBCDToHexa.TabIndex = 3;
            this.buttonBCDToHexa.Text = "BCD to Hexa";
            this.buttonBCDToHexa.UseVisualStyleBackColor = true;
            this.buttonBCDToHexa.Click += new System.EventHandler(this.buttonBCDToHexa_Click);
            // 
            // buttonHexaToString
            // 
            this.buttonHexaToString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexaToString.Location = new System.Drawing.Point(510, 112);
            this.buttonHexaToString.Name = "buttonHexaToString";
            this.buttonHexaToString.Size = new System.Drawing.Size(100, 23);
            this.buttonHexaToString.TabIndex = 5;
            this.buttonHexaToString.Text = "Hexa to String";
            this.buttonHexaToString.UseVisualStyleBackColor = true;
            this.buttonHexaToString.Click += new System.EventHandler(this.buttonHexaToString_Click);
            // 
            // buttonStringToHexa
            // 
            this.buttonStringToHexa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStringToHexa.Location = new System.Drawing.Point(404, 112);
            this.buttonStringToHexa.Name = "buttonStringToHexa";
            this.buttonStringToHexa.Size = new System.Drawing.Size(100, 23);
            this.buttonStringToHexa.TabIndex = 4;
            this.buttonStringToHexa.Text = "String to Hexa";
            this.buttonStringToHexa.UseVisualStyleBackColor = true;
            this.buttonStringToHexa.Click += new System.EventHandler(this.buttonStringToHexa_Click);
            // 
            // textArrayOfBytesInterpreted
            // 
            this.textArrayOfBytesInterpreted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textArrayOfBytesInterpreted.Location = new System.Drawing.Point(6, 141);
            this.textArrayOfBytesInterpreted.Multiline = true;
            this.textArrayOfBytesInterpreted.Name = "textArrayOfBytesInterpreted";
            this.textArrayOfBytesInterpreted.ReadOnly = true;
            this.textArrayOfBytesInterpreted.Size = new System.Drawing.Size(604, 271);
            this.textArrayOfBytesInterpreted.TabIndex = 7;
            // 
            // textArrayOfBytesSource
            // 
            this.textArrayOfBytesSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textArrayOfBytesSource.Location = new System.Drawing.Point(6, 19);
            this.textArrayOfBytesSource.Multiline = true;
            this.textArrayOfBytesSource.Name = "textArrayOfBytesSource";
            this.textArrayOfBytesSource.Size = new System.Drawing.Size(604, 87);
            this.textArrayOfBytesSource.TabIndex = 1;
            // 
            // HelpersGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.tabTLV);
            this.Name = "HelpersGui";
            this.Text = "Helpers GUI";
            this.tabTLV.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabArrayOfBytes.ResumeLayout(false);
            this.tabArrayOfBytes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabTLV;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabArrayOfBytes;
        private System.Windows.Forms.TextBox textTLVHexa;
        private System.Windows.Forms.TextBox textTLVDecoded;
        private System.Windows.Forms.Button buttonTlvToXml;
        private System.Windows.Forms.Button buttonAnalyzeTLV;
        private System.Windows.Forms.TextBox textArrayOfBytesSource;
        private System.Windows.Forms.TextBox textArrayOfBytesInterpreted;
        private System.Windows.Forms.Button buttonHexaToString;
        private System.Windows.Forms.Button buttonStringToHexa;
        private System.Windows.Forms.Button buttonHexaToBCD;
        private System.Windows.Forms.Button buttonBCDToHexa;
        private System.Windows.Forms.Button buttonXmlToTlv;
    }
}

