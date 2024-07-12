namespace You.Archi.HtmlToImage
{
    public class HtmlToImageConverter
    {
        private static readonly string _toolFileName = "wkhtmltoimage.exe";

        private readonly string _toolFilePath;

        public HtmlToImageConverter(string toolFilePath)
        {
            _toolFilePath = toolFilePath;
        }



    }
}
