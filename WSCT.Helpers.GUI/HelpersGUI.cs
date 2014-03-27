using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helpers.GUI
{
    /// <summary>
    /// Main form of the tool.
    /// </summary>
    public partial class HelpersGui : Form
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public HelpersGui()
        {
            InitializeComponent();
        }

        private void buttonTlvToXml_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TlvData tlv;

            try
            {
                tlv = textTLVHexa.Text.Replace("\r\n", "").ToTlvData();
            }
            catch (Exception exception)
            {
                textTLVDecoded.Text = exception.Message;
                return;
            }
            try
            {
                textTLVDecoded.Text = tlv.ToXmlString();
            }
            catch (Exception exception)
            {
                textTLVDecoded.Text = exception.Message;
            }
        }

        private void buttonXmlToTlv_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TlvData tlv;

            try
            {
                var serializer = new XmlSerializer(typeof(TlvData));
                tlv = (TlvData)serializer.Deserialize(new StringReader(textTLVHexa.Text));
            }
            catch (Exception exception)
            {
                textTLVDecoded.Text = exception.Message;
                return;
            }
            try
            {
                textTLVDecoded.Text = tlv.ToByteArray().ToHexa();
            }
            catch (Exception exception)
            {
                textTLVDecoded.Text = exception.Message;
            }
        }

        private void buttonAnalyzeTLV_Click(object sender, EventArgs e)
        {
            textTLVDecoded.Text = "";
            TlvData tlv;

            try
            {
                tlv = textTLVHexa.Text.Replace("\r\n", "").ToTlvData();
            }
            catch (Exception exception)
            {
                textTLVDecoded.Text = exception.Message;
                return;
            }
            try
            {
                var tagsManager = SerializedObject<TlvDictionary>.LoadFromXml(@"Dictionary.HelpersTags.xml");
                foreach (TlvData tlvData in tlv.GetTags())
                {
                    var tagObject = tagsManager.CreateInstance(tlvData);
                    if (tagObject != null)
                    {
                        textTLVDecoded.Text += String.Format("{0:N} ({1:T}): {0}\r\n", tagObject, tlvData);
                    }
                    else
                    {
                        textTLVDecoded.Text += String.Format("! Unknow tlvDesc {0:T}: {0:V}\r\n", tlvData);
                    }
                }
            }
            catch (Exception eAnalyze)
            {
                textTLVDecoded.Text = eAnalyze.Message;
            }
        }

        private void buttonHexaToString_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.FromHexa().ToAsciiString();
            }
            catch (Exception eHexa)
            {
                textArrayOfBytesInterpreted.Text = eHexa.Message;
            }
        }

        private void buttonStringToHexa_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.FromString().ToHexa();
            }
            catch (Exception eHexa)
            {
                textArrayOfBytesInterpreted.Text = eHexa.Message;
            }
        }

        private void buttonBCDToHexa_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.FromBcd().ToHexa();
            }
            catch (Exception exception)
            {
                textArrayOfBytesInterpreted.Text = exception.Message;
            }
        }

        private void buttonHexaToBCD_Click(object sender, EventArgs e)
        {
            textArrayOfBytesInterpreted.Text = "";

            try
            {
                textArrayOfBytesInterpreted.Text = textArrayOfBytesSource.Text.FromHexa().ToBcdString(' ');
            }
            catch (Exception exception)
            {
                textArrayOfBytesInterpreted.Text = exception.Message;
            }
        }
    }
}