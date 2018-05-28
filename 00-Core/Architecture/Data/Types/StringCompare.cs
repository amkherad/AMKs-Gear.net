namespace AMKsGear.Architecture.Data.Types
{
    public enum StringCompare
    {
        Equal,
        NotEqual,

        Contains,
        NotContains,

        RegexLike,

        StartsWith,
        NotStartsWith,

        EndsWith,
        NotEndsWith,

        Default = Contains,
    }
}