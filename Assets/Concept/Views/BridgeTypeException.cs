using System;

public class BridgeTypeException : Exception
{
    public BridgeTypeException(string propertyName, PropertyView propertyView, ViewDataBridge dataBridge) : base(
        $"bridge '{dataBridge.GetType().Name}' doesn't support property '{propertyName}' of type '{propertyView.GetType().GenericTypeArguments[0].Name}'!"
    ) { }
}