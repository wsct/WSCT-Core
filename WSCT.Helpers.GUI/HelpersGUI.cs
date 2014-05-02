using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using WSCT.Helpers.BasicEncodingRules;
using WSCT.Helpers.Security;

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

        private void ConvertAndOutput(Func<string> conversion)
        {
            try
            {
                textTarget.Text = conversion();
            }
            catch (Exception exception)
            {
                textTarget.Text = exception.Message;
            }
        }

        private bool TryAndOutput(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception exception)
            {
                textTarget.Text = exception.Message;
                return false;
            }
        }

        private void textSource_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable Ctrl A to select all text
            if (e.Control & e.KeyCode == Keys.A)
            {
                textSource.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        private void textTarget_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable Ctrl A to select all text
            if (e.Control & e.KeyCode == Keys.A)
            {
                textTarget.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        private void buttonTlvToXml_Click(object sender, EventArgs e)
        {
            TlvData tlv = null;

            var isDone = TryAndOutput(() => tlv = textSource.Text.Replace("\r\n", "").ToTlvData());

            if (!isDone)
            {
                return;
            }

            ConvertAndOutput(() => tlv.ToXmlString());
        }

        private void buttonXmlToTlv_Click(object sender, EventArgs e)
        {
            TlvData tlv = null;

            var isDone = TryAndOutput(() =>
            {
                var serializer = new XmlSerializer(typeof(TlvData));
                tlv = (TlvData)serializer.Deserialize(new StringReader(textSource.Text));
            });

            if (!isDone)
            {
                return;
            }

            ConvertAndOutput(() => tlv.ToByteArray().ToHexa());
        }

        private void buttonAnalyzeTLV_Click(object sender, EventArgs e)
        {
            TlvData tlv = null;

            var isDone = TryAndOutput(() => tlv = textSource.Text.Replace(Environment.NewLine, String.Empty).ToTlvData());

            if (!isDone)
            {
                return;
            }

            ConvertAndOutput(() =>
            {
                var text = String.Empty;
                var tagsManager = SerializedObject<TlvDictionary>.LoadFromXml(@"Dictionary.HelpersTags.xml");
                foreach (TlvData tlvData in tlv.GetTags())
                {
                    var tagObject = tagsManager.CreateInstance(tlvData);
                    if (tagObject != null)
                    {
                        text += String.Format("{0:N} ({1:T}): {0}\r\n", tagObject, tlvData);
                    }
                    else
                    {
                        text += String.Format("! Unknow tlvDesc {0:T}: {0:V}\r\n", tlvData);
                    }
                }
                return text;
            });
        }

        private void buttonHexaToString_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => textSource.Text.FromHexa().ToAsciiString());
        }

        private void buttonStringToHexa_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => textSource.Text.FromString().ToHexa());
        }

        private void buttonBCDToHexa_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => textSource.Text.FromBcd().ToHexa());
        }

        private void buttonHexaToBCD_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => textSource.Text.FromHexa().ToBcdString(' '));
        }

        private void buttonBase64ToHexa_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => Convert.FromBase64String(textSource.Text).ToHexa());
        }

        private void buttonHexaToBase64_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => Convert.ToBase64String(textSource.Text.FromHexa()));
        }

        private void buttonAnalyzeSshPublicKey_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => Ssh1PublicKeyBody.Create(textSource.Text).ToString());
        }

        private void buttonAnalyzeSshPrivateKey_Click(object sender, EventArgs e)
        {
            ConvertAndOutput(() => PuTTyPrivateKeyBody.Create(textSource.Text).ToString());
        }
    }
}