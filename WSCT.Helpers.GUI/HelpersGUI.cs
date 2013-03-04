using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

using WSCT.Helpers;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helpers.GUI
{
    /// <summary>
    /// Main form of the tool
    /// </summary>
    public partial class HelpersGUI : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HelpersGUI()
        {
            InitializeComponent();
        }

        private void buttonTlvToXml_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TLVData tlv;

            try
            {
                tlv = textTLVHexa.Text.Replace("\r\n", "").toTLVData();
            }
            catch (Exception eTLV)
            {
                textTLVDecoded.Text = eTLV.Message;
                return;
            }
            try
            {
                textTLVDecoded.Text = tlv.toXmlString();
            }
            catch (Exception eXML)
            {
                textTLVDecoded.Text = eXML.Message;
                return;
            }
        }

        private void buttonXmlToTlv_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TLVData tlv;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TLVData));
                tlv = (TLVData)serializer.Deserialize(new StringReader(textTLVHexa.Text));
            }
            catch (Exception eTLV)
            {
                textTLVDecoded.Text = eTLV.Message;
                return;
            }
            try
            {
                textTLVDecoded.Text = tlv.toByteArray().toHexa();
            }
            catch (Exception eXML)
            {
                textTLVDecoded.Text = eXML.Message;
                return;
            }
        }

        private void buttonAnalyzeTLV_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TLVData tlv;

            try
            {
                tlv = textTLVHexa.Text.Replace("\r\n", "").toTLVData();
            }
            catch (Exception eTLV)
            {
                textTLVDecoded.Text = eTLV.Message;
                return;
            }
            try
            {
                TLVDictionary tagsManager = SerializedObject<TLVDictionary>.loadFromXml(@"Dictionary.HelpersTags.xml");
                foreach (TLVData tlvData in tlv.getTags())
                {
                    AbstractTLVObject tagObject = tagsManager.createInstance(tlvData);
                    if (tagObject != null)
                        textTLVDecoded.Text += String.Format("{0:N} ({1:T}): {0}\r\n", tagObject, tlvData);
                    else
                        textTLVDecoded.Text += String.Format("! Unknow tlvDesc {0:T}: {0:V}\r\n", tlvData);
                }

            }
            catch (Exception eAnalyze)
            {
                textTLVDecoded.Text = eAnalyze.Message;
                return;
            }

        }

        private void buttonHexaToString_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.fromHexa().toString();
            }
            catch (Exception eHexa)
            {
                textArrayOfBytesInterpreted.Text = eHexa.Message;
                return;
            }
        }

        private void buttonStringToHexa_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.fromString().toHexa();
            }
            catch (Exception eHexa)
            {
                textArrayOfBytesInterpreted.Text = eHexa.Message;
                return;
            }
        }

        private void buttonBCDToHexa_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.fromBCD().toHexa();
            }
            catch (Exception eBCD)
            {
                textArrayOfBytesInterpreted.Text = eBCD.Message;
                return;
            }
        }

        private void buttonHexaToBCD_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.fromHexa().toBCD(' ');
            }
            catch (Exception eBCD)
            {
                textArrayOfBytesInterpreted.Text = eBCD.Message;
                return;
            }
        }
    }
}