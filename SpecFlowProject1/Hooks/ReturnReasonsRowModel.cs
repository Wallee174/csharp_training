using DigitalPaymentsCommon.Models.Elements.Tables;

namespace ProcessOneCommon.UnicornPages.Administration
{
    public class ReturnReasonsRowModel: IRowModel
    {
        public string ReturnReasonsType { get; set; }
        
        public string Category { get; set; }
    }
}