namespace PdfMerger
{
    internal class ApplicationArguments
    {
        public string OddPagesFilePath { get; set; }
        public string EvenPagesFilePath { get; set; }
        public string DestinationFilePath { get; set; }
        public bool OverwriteExisting { get; set; }
    }
}
