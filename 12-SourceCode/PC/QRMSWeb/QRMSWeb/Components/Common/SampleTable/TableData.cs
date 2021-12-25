namespace QRMSWeb.Components.Common.SampleTable
{
    public class TableData
    {
        
    }

    public class TableLabels
    {
        public string label { get; set; } = string.Empty;
        public string dataIndex { get; set; } = string.Empty;
        
        public string dataType { get; set; } = string.Empty;

        public int columnWidth { get; set; } = 0;
        public string align { get; set; } = string.Empty;

        public string[] Link { get; set; } = new string[] { };
        public string[] DialogClickOnLink { get; set; } = new string[] { };
    }
}