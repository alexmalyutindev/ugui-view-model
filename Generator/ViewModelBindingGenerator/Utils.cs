using System.Text;

namespace AlexMalyutinDev.ViewModelBinding.Generator;

public static class Utils
{
    public static StringBuilder Tab(this StringBuilder sb, int count)
    {
        return sb.Append('\t', count);
    }

    public static StringBuilder BeginClass(this StringBuilder sb, string modifiers, string name, ref int indent)
    {
        sb.Tab(indent).Append(modifiers).Append(" class ").AppendLine(name);
        sb.BeginBlock(ref indent);
        return sb;
    }

    public static StringBuilder EndClass(this StringBuilder sb, ref int indent)
    {
        return sb.EndBlock(ref indent);
    }

    public static StringBuilder BeginMethod(this StringBuilder sb, string methodDeclaration, ref int indent)
    {
        sb.Tab(indent).AppendLine(methodDeclaration);
        sb.BeginBlock(ref indent);
        return sb;
    }

    public static StringBuilder EndMethod(this StringBuilder sb, ref int indent)
    {
        return sb.EndBlock(ref indent);
    }

    public static StringBuilder BeginBlock(this StringBuilder sb, ref int indent) => sb.Tab(indent++).AppendLine("{");
    public static StringBuilder EndBlock(this StringBuilder sb, ref int indent) => sb.Tab(--indent).AppendLine("}");
}
