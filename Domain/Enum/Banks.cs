using System.ComponentModel;

namespace Domain.Enum
{
    public enum Banks
    {
        [Description("TURKIYE IS BANKASI, A.S.")]
        IsBankasi,

        [Description("TURKIYE GARANTI BANKASI A. S.")]
        GarantiBankasi,

        [Description("AKBANK T.A.S.")]
        Akbank,

        [Description("HALK BANKASI A.S")]
        HalkBank,

        [Description("TURKIYE VAKIFLAR BANKASI T.A.O.")]
        Vakifbank,
        
        [Description("YAPI KREDI")]
        YapiVeKrediBankasi,

        [Description("TURK EKONOMI BANKASI, A.S.")]
        TurkEkonomiBankasi,

        [Description("FINANSBANK, A.S.")]
        QnbFinansbank,
        
        [Description("DENIZBANK A.S.")]
        DenizBank,
        
        [Description("ING BANK A.S")]
        IngBank,

        [Description("T.C. ZIRAAT BANKASI, A.S.")]
        ZiraatBankasi,

        [Description("KUVEYT TURK KATILIM BANKASI, A.S.")]
        KuveytTurk
    }
}
