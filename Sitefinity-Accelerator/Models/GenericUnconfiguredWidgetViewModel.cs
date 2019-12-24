namespace SitefinityAccelerator.Models
{
    public class GenericUnconfiguredWidgetViewModel
    {
        private readonly string _classes;
        private readonly string _designModeText;
        private readonly string _designModeTextAdditional;
        private readonly string _presentationModeText;
        private readonly bool _withPresentationMode;

        public string Classes => string.IsNullOrWhiteSpace(_classes) ? "sfAddContentLnk sfMvcIcn" : _classes;
        public string DesignModeText => string.IsNullOrWhiteSpace(_designModeText) ? "Configure Widget" : _designModeText;
        public string DesignModeTextAdditional => string.IsNullOrWhiteSpace(_designModeTextAdditional) ? string.Empty : _designModeTextAdditional;
        public string PresentationModeText => string.IsNullOrWhiteSpace(_presentationModeText) ? null : _presentationModeText;
        public bool WithPresentationMode => _withPresentationMode;

        public GenericUnconfiguredWidgetViewModel(string designModeText = null, string designModeTextAdditional = null, string presentationModeText = null, string classes = null, bool withPresentationMode = false)
        {
            _classes = classes;
            _designModeText = designModeText;
            _designModeTextAdditional = designModeTextAdditional;
            _presentationModeText = presentationModeText;
            _withPresentationMode = withPresentationMode;
        }
    }
}