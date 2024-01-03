using System;

namespace AlexMalyutinDev.ViewModelBinding
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
