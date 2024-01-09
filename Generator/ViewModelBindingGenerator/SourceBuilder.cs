using System.Text;

namespace AlexMalyutinDev.ViewModelBinding.Generator;

public class SourceBuilder
{
    private StringBuilder _sb = new();
    private int _indent = 0;

    public IDisposable Namespace(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return new EmptyDisposable();
        }

        _sb.Append("namespace ").AppendLine(name).BeginBlock(ref _indent);
        return new DisposableAction(EndBlock);
    }

    public IDisposable Class(string modifiers, string name)
    {
        _sb.BeginClass(modifiers, name, ref _indent);
        return new DisposableAction(EndBlock);
    }

    public IDisposable Method(string methodDeclaration)
    {
        _sb.BeginMethod(methodDeclaration, ref _indent);
        return new DisposableAction(EndBlock);
    }

    private void EndBlock() => _sb.EndBlock(ref _indent);

    public override string ToString()
    {
        return _sb.ToString();
    }

    private readonly struct DisposableAction : IDisposable
    {
        private readonly Action _onDispose;

        public DisposableAction(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose();
        }
    }

    private readonly struct EmptyDisposable : IDisposable
    {
        public void Dispose() { }
    }

    public SourceBuilder BeginLine(string value)
    {
         _sb.Tab(_indent).Append(value);
         return this;
    }
    
    public SourceBuilder Append(string value)
    {
        _sb.Append(value);
        return this;
    }
    
    public SourceBuilder Append(char value)
    {
        _sb.Append(value);
        return this;
    }

    public void EndLine()
    {
        _sb.AppendLine(";");
    }

    public void AppendLine(string value)
    {
        _sb.Tab(_indent).AppendLine(value);
    }
}
