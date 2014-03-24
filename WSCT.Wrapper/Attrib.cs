namespace WSCT.Wrapper
{
    /// <summary>
    /// Enumeration for attributes.
    /// </summary>
    public enum Attrib : uint
    {
        /// <summary>
        /// 
        /// </summary>
        AsyncProtocolTypes = 0x30120,
        /// <summary>
        /// Answer to reset (ATR) string.
        /// </summary>
        AtrString = 0x90303,
        /// <summary>
        /// DWORD encoded as 0xDDDDCCCC, where DDDD = data channel type and CCCC = channel number
        /// </summary>
        ChannelId = 0x20110,
        /// <summary>
        /// DWORD indicating which mechanical characteristics are supported.
        /// If zero, no special characteristics are supported. Note that multiple bits can be set
        /// <list type="bullet">
        ///     <item>0x00000001 Card swallowing mechanism</item>
        ///     <item>0x00000002 Card ejection mechanism</item>
        ///     <item>0x00000004 Card capture mechanism</item>
        /// </list>
        /// </summary>
        Characteristics = 0x60150,
        /// <summary>
        /// Current block waiting time.
        /// </summary>
        CurrentBwt = 0x80209,
        /// <summary>
        /// Current clock rate, in kHz.
        /// </summary>
        CurrentClk = 0x80202,
        /// <summary>
        /// Current character waiting time.
        /// </summary>
        CurrentCwt = 0x80209,
        /// <summary>
        /// Bit rate conversion factor.
        /// </summary>
        CurrentD = 0x80204,
        /// <summary>
        /// Current error block control encoding (T=1).
        /// <list type="bullet">
        ///     <item>0 = longitudinal redundancy check (LRC)</item>
        ///     <item>1 = cyclical redundancy check (CRC)</item>
        /// </list>
        /// </summary>
        CurrentEbcEncoding = 0x8020B,
        /// <summary>
        /// Clock conversion factor.
        /// </summary>
        CurrentF = 0x80203,
        /// <summary>
        /// Current byte size for information field size card.
        /// </summary>
        CurrentIfsc = 0x80207,
        /// <summary>
        /// Current byte size for information field size device.
        /// </summary>
        CurrentIfsd = 0x80208,
        /// <summary>
        /// 
        /// </summary>
        CurrentIoState = 0x90302,
        /// <summary>
        /// 
        /// </summary>
        CurrentN = 0x80205,
        /// <summary>
        /// DWORD encoded as 0x0rrrpppp where rrr is RFU and should be 0x000. pppp encodes the current protocol type. Whichever bit has been set indicates which ISO protocol is currently in use. (For example, if bit 0 is set, T=0 protocol is in effect).
        /// </summary>
        CurrentProtocolType = 0x80201,
        /// <summary>
        /// Current work waiting time.
        /// </summary>
        CurrentW = 0x80206,
        /// <summary>
        /// Default clock rate, in kHz.
        /// </summary>
        DefaultClk = 0x30121,
        /// <summary>
        /// Default data rate, in bps.
        /// </summary>
        DefaultDataRate = 0x30123,
        /// <summary>
        /// Reader's friendly name.
        /// </summary>
        DeviceFriendlyName = 0x7FFF0003,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        DeviceInUse = 0x7FFF0002,
        /// <summary>
        /// Reader's system name.
        /// </summary>
        DeviceSystemName = 0x7FFF0004,
        /// <summary>
        /// Instance of this vendor's reader attached to the computer. The first instance will be device unit 0, the next will be unit 1 (if it is the same brand of reader) and so on. Two different brands of readers will both have 0 for this value.
        /// </summary>
        DeviceUnit = 0x7FFF0001,
        /// <summary>
        /// 
        /// </summary>
        EscReset = 0x7A000,
        /// <summary>
        /// 
        /// </summary>
        EscCancel = 0x7A003,
        /// <summary>
        /// </summary>
        EscAuthRequest = 0x7A005,
        /// <summary>
        /// </summary>
        ExtendedBwt = 0x8020C,
        /// <summary>
        /// Single byte. 0 if smart card electrical contact is not active; non-zero if contact is active.
        /// </summary>
        IccInterfaceStatus = 0x90301,
        /// <summary>
        /// Single byte indicating smart card presence.
        /// <list>
        ///     <item>0 = not present</item>
        ///     <item>1 = card present but not swallowed (applies only if reader supports smart card swallowing)</item>
        ///     <item>2 = card present (and swallowed if reader supports smart card swallowing)</item>
        ///     <item>4 = card confiscated</item>
        /// </list>
        /// </summary>
        IccPresence = 0x90300,
        /// <summary>
        /// Single byte indicating smart card type.
        /// <list type="bullet">
        ///     <item>0 = unknown type</item>
        ///     <item>1 = 7816 Asynchronous</item>
        ///     <item>2 = 7816 Synchronous</item>
        /// </list>
        /// Other values RFU.
        /// </summary>
        IccTypePerAtr = 0x90304,
        /// <summary>
        /// Maximum clock rate, in kHz.
        /// </summary>
        MaxClk = 0x30122,
        /// <summary>
        /// Maximum data rate, in bps.
        /// </summary>
        MaxDataRate = 0x30124,
        /// <summary>
        /// Maximum bytes for information file size device.
        /// </summary>
        MaxIfsd = 0x30125,
        /// <summary>
        /// 
        /// </summary>
        MaxInput = 0x71007,
        /// <summary>
        /// 0 if device does not support power down while smart card is inserted. Non-zero otherwise.
        /// </summary>
        PowerManagementSupport = 0x40131,
        /// <summary>
        /// 
        /// </summary>
        SupressT1IfsRequest = 0x7FFF0007,
        /// <summary>
        /// 
        /// </summary>
        SyncProtocolTypes = 0x30126,
        /// <summary>
        /// 
        /// </summary>
        UserToCardAuthDevice = 0x50140,
        /// <summary>
        /// 
        /// </summary>
        UserAuthInputDevice = 0x50142,
        /// <summary>
        /// Vendor-supplied interface device serial number.
        /// </summary>
        VendorIfdSerialNo = 0x10103,
        /// <summary>
        /// Vendor-supplied interface device type (model designation of reader).
        /// </summary>
        VendorIfdType = 0x10101,
        /// <summary>
        /// Vendor-supplied interface device version (DWORD in the form 0xMMmmbbbb  where MM = major version, mm = minor version, and bbbb = build number).
        /// </summary>
        VendorIfdVersion = 0x10102,
        /// <summary>
        /// Vendor name.
        /// </summary>
        VendorName = 0x10100
    }
}