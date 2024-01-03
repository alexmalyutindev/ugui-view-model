using System;

namespace Example
{
    public class ViewModelPropertyAttribute : Attribute
    {
        public string Name { get; }

        public ViewModelPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
