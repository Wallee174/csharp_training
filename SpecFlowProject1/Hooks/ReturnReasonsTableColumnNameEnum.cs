using System.ComponentModel;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public enum ReturnReasonsTableColumnNameEnum
    {
        [Description("TYPE")]
        TYPE = 0,
        
        [Description("CODE")]
        CODE = 1,
        
        [Description("REASON")]
        DESCRIPTION = 2,
        
        [Description("CATEGORY")]
        CARDRETURNREASONSCATEGORYNAME = 3,

        [Description("EFT RETURN ACTION")]
        EFTBLOCKRULE = 4
    }
}