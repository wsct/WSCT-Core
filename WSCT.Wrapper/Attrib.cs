using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for attributes
    /// </summary>
    public enum Attrib : uint
    {
        /// <summary></summary>
        SCARD_ATTR_ASYNC_PROTOCOL_TYPES = 0x30120,
        /// <summary>Answer to reset (ATR) string.</summary>
        SCARD_ATTR_ATR_STRING = 0x90303,
        /// <summary>DWORD encoded as 0xDDDDCCCC, where DDDD = data channel type and CCCC = channel number</summary>
        SCARD_ATTR_CHANNEL_ID = 0x20110,
        /// <summary>
        /// DWORD indicating which mechanical characteristics are supported.
        /// If zero, no special characteristics are supported. Note that multiple bits can be set
        /// <list type="bullet">
        ///     <item>0x00000001 Card swallowing mechanism</item>
        ///     <item>0x00000002 Card ejection mechanism</item>
        ///     <item>0x00000004 Card capture mechanism</item>
        /// </list>
        /// </summary>
        SCARD_ATTR_CHARACTERISTICS = 0x60150,
        /// <summary>Current block waiting time.</summary>
        SCARD_ATTR_CURRENT_BWT = 0x80209,
        /// <summary>Current clock rate, in kHz.</summary>
        SCARD_ATTR_CURRENT_CLK = 0x80202,
        /// <summary>Current character waiting time.</summary>
        SCARD_ATTR_CURRENT_CWT = 0x80209,
        /// <summary>Bit rate conversion factor.</summary>
        SCARD_ATTR_CURRENT_D = 0x80204,
        /// <summary>
        /// Current error block control encoding (T=1).
        /// <list type="bullet">
        ///     <item>0 = longitudinal redundancy check (LRC)</item>
        ///     <item>1 = cyclical redundancy check (CRC)</item>
        /// </list>
        /// </summary>
        SCARD_ATTR_CURRENT_EBC_ENCODING = 0x8020B,
        /// <summary>Clock conversion factor. </summary>
        SCARD_ATTR_CURRENT_F = 0x80203,
        /// <summary>Current byte size for information field size card.</summary>
        SCARD_ATTR_CURRENT_IFSC = 0x80207,
        /// <summary>Current byte size for information field size device.</summary>
        SCARD_ATTR_CURRENT_IFSD = 0x80208,
        /// <summary></summary>
        SCARD_ATTR_CURRENT_IO_STATE = 0x90302,
        /// <summary></summary>
        SCARD_ATTR_CURRENT_N = 0x80205,
        /// <summary>DWORD encoded as 0x0rrrpppp where rrr is RFU and should be 0x000. pppp encodes the current protocol type. Whichever bit has been set indicates which ISO protocol is currently in use. (For example, if bit 0 is set, T=0 protocol is in effect.)</summary>
        SCARD_ATTR_CURRENT_PROTOCOL_TYPE = 0x80201,
        /// <summary>Current work waiting time.</summary>
        SCARD_ATTR_CURRENT_W = 0x80206,
        /// <summary>Default clock rate, in kHz.</summary>
        SCARD_ATTR_DEFAULT_CLK = 0x30121,
        /// <summary>Default data rate, in bps.</summary>
        SCARD_ATTR_DEFAULT_DATA_RATE = 0x30123,
        /// <summary>Reader's friendly name.</summary>
        SCARD_ATTR_DEVICE_FRIENDLY_NAME = 0x7FFF0003,
        /// <summary>Reserved for future use. </summary>
        SCARD_ATTR_DEVICE_IN_USE = 0x7FFF0002,
        /// <summary>Reader's system name.</summary>
        SCARD_ATTR_DEVICE_SYSTEM_NAME = 0x7FFF0004,
        /// <summary>Instance of this vendor's reader attached to the computer. The first instance will be device unit 0, the next will be unit 1 (if it is the same brand of reader) and so on. Two different brands of readers will both have 0 for this value.</summary>
        SCARD_ATTR_DEVICE_UNIT = 0x7FFF0001,
        /// <summary></summary>
        SCARD_ATTR_ESC_RESET = 0x7A000,
        /// <summary></summary>
        SCARD_ATTR_ESC_CANCEL = 0x7A003,
        /// <summary></summary>
        SCARD_ATTR_ESC_AUTHREQUEST = 0x7A005,
        /// <summary></summary>
        SCARD_ATTR_EXTENDED_BWT = 0x8020C,
        /// <summary>Single byte. 0 if smart card electrical contact is not active; non-zero if contact is active.</summary>
        SCARD_ATTR_ICC_INTERFACE_STATUS = 0x90301,
        /// <summary>
        /// Single byte indicating smart card presence.
        /// <list>
        ///     <item>0 = not present</item>
        ///     <item>1 = card present but not swallowed (applies only if reader supports smart card swallowing)</item>
        ///     <item>2 = card present (and swallowed if reader supports smart card swallowing)</item>
        ///     <item>4 = card confiscated</item>
        /// </list>
        /// </summary>
        SCARD_ATTR_ICC_PRESENCE = 0x90300,
        /// <summary>
        /// Single byte indicating smart card type.
        /// <list type="bullet">
        ///     <item>0 = unknown type</item>
        ///     <item>1 = 7816 Asynchronous</item>
        ///     <item>2 = 7816 Synchronous</item>
        /// </list>
        /// Other values RFU.
        /// </summary>
        SCARD_ATTR_ICC_TYPE_PER_ATR = 0x90304,
        /// <summary>Maximum clock rate, in kHz.</summary>
        SCARD_ATTR_MAX_CLK = 0x30122,
        /// <summary>Maximum data rate, in bps.</summary>
        SCARD_ATTR_MAX_DATA_RATE = 0x30124,
        /// <summary>Maximum bytes for information file size device. </summary>
        SCARD_ATTR_MAX_IFSD = 0x30125,
        /// <summary></summary>
        SCARD_ATTR_MAXINPUT = 0x71007,
        /// <summary>0 if device does not support power down while smart card is inserted. Non-zero otherwise.</summary>
        SCARD_ATTR_POWER_MGMT_SUPPORT = 0x40131,
        /// <summary></summary>
        SCARD_ATTR_SUPRESS_T1_IFS_REQUEST = 0x7FFF0007,
        /// <summary></summary>
        SCARD_ATTR_SYNC_PROTOCOL_TYPES = 0x30126,
        /// <summary></summary>
        SCARD_ATTR_USER_TO_CARD_AUTH_DEVICE = 0x50140,
        /// <summary></summary>
        SCARD_ATTR_USER_AUTH_INPUT_DEVICE = 0x50142,
        /// <summary>Vendor-supplied interface device serial number.</summary>
        SCARD_ATTR_VENDOR_IFD_SERIAL_NO = 0x10103,
        /// <summary>Vendor-supplied interface device type (model designation of reader).</summary>
        SCARD_ATTR_VENDOR_IFD_TYPE = 0x10101,
        /// <summary>Vendor-supplied interface device version (DWORD in the form 0xMMmmbbbb  where MM = major version, mm = minor version, and bbbb = build number).</summary>
        SCARD_ATTR_VENDOR_IFD_VERSION = 0x10102,
        /// <summary>Vendor name.</summary>
        SCARD_ATTR_VENDOR_NAME = 0x10100
    }
}