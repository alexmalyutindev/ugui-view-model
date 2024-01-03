using System.Text.Json.Serialization;

public class PropertyView<T> { }

namespace Tests
{
    [ViewModelRoot]
    public partial class TestRootViewModel : RootViewModelBase
    {
        public int headerName;
        public float floatSlider;

        public PropertyView<float> TestProp;
    }
}
