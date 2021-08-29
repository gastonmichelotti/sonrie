namespace netCoreNew.ViewModels
{
    public class BreadcrumVM
    {
        public string Back { get; set; }
        public string UrlBack { get; set; }
        public string UrlCurrent { get; set; }
        public string Current { get; set; }
    }

    public class ErrorVM
    {
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descripcion { get; set; }
    }
}