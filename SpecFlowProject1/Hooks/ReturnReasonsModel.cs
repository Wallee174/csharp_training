using Utils.Extensions;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public struct ReturnReasonsModel
    {
        public string Code { get; set; }
        
        public string Reason { get; set; }

        public string EftReturnAction { get; set; }
        
        public string Type { get; set; }
        
        public string Category { get; set; }
    }
}