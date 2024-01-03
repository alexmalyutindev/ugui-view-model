using System;

namespace AlexMalyutinDev.ViewModelBinding
{
    public interface IPropertyView
    {
        public PropertyView<T> As<T>();
    }

    public interface IPropertyView<out T> : IPropertyView
    {
        public T Value { get; }
        public T OldValue { get; }
    }

    public abstract class PropertyView : IPropertyView
    {
        public PropertyView<T> As<T>()
        {
            return (PropertyView<T>) this;
        }
    }

    // TODO: Inverse INotifiableModelSide and INotifiableViewSide to be consistent:
    // TODO: - Remove crossing of Changed event and Set method.

    // Interface to use on Model side to be notified from view and notify view from model
    public interface INotifiableModelSide<T> : IPropertyView<T>
    {
        // Listen to view changes
        event Action<PropertyView<T>> Changed;

        // Update model from view side
        public void Set(T value);
    }

    // Interface to use on View side to be notified from model and notify model from view
    public interface INotifiableViewSide<T> : IPropertyView<T>
    {
        // Listen to model changes
        event Action<PropertyView<T>> Changed;

        // Update model from view side
        public void Set(T value);
    }

    public class PropertyView<T> : PropertyView, INotifiableModelSide<T>, INotifiableViewSide<T>
    {
        // Notifies when model changes from view side.
        public event Action<PropertyView<T>> Changed;

        event Action<PropertyView<T>> INotifiableViewSide<T>.Changed
        {
            add => ChangedFromModel += value;
            remove => ChangedFromModel -= value;
        }

        private event Action<PropertyView<T>> ChangedFromModel;

        public T Value => _value;
        public T OldValue => _oldValue;


        private T _value;
        private T _oldValue;

        void INotifiableViewSide<T>.Set(T value)
        {
            _oldValue = _value;
            _value = value;
            Changed?.Invoke(this);
            // NOTE: Call this to update other view. Bridge that causes update is muted for one call.
            ChangedFromModel?.Invoke(this);
        }

        // Sets value from model side and notify view.
        public void Set(T value)
        {
            _oldValue = _value;
            _value = value;
            ChangedFromModel?.Invoke(this);
        }
    }
}
