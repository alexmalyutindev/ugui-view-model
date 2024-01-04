[WIP] UGUI ViewModel Binding
---
Simple reactive View-ViewModel binding system for Unity's UGUI.

Usage:
---
```csharp
[ViewModelRoot]
public class MyViewModel : ViewModelRoot
{
    [ViewModelProperty("MyFloat")]
    private PropertyView<float> _myFloatProperty;

    private MyModel _model;
    
    public void Initialize(MyModel model)
    {
        _model = model;
        _model.MyFloat.Subscribe(value => _myFloatProperty.Set(value));
    }
}
```

TODOs:
---
- [ ] Add attribute for ViewModel properties
- [ ] Replace field's name binding with unique name or id
- [ ] Setup CI/CD for package