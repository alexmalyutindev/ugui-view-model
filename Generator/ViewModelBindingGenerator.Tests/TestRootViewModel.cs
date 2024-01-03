namespace Tests
{
    [ViewModelRoot]
    public partial class TestRootViewModel : ViewModelRoot
    {
        [ViewModelProperty()]
        private PropertyView<float> _floatProp;
        private PropertyView<string> _stringProp;
    }
}
