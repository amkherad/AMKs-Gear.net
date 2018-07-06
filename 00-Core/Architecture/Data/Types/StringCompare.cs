namespace AMKsGear.Architecture.Data.Types
{
    public enum StringCompare
    {
        Equals,
        NotEquals,

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