﻿using AlexMalyutinDev.ViewModelBinding;

namespace Tests
{
    [ViewModelRoot]
    public partial class TestRootViewModel : ViewModelRoot
    {
        [ViewModelProperty("FloatProp")]
        private PropertyView<float> _floatProp;
        private PropertyView<string> _stringProp;
    }
}
