namespace AMKsGear.Architecture.Patterns
{
    public interface IFactory
    {
        object CreateDefault();
        object CreateDefault(object arg);
        object CreateDefault(params object[] args);
    }
    public abstract class Factory<TObject> : IFactory
    {
        public virtual object CreateDefault() { return Create(new object[0]); }
        public virtual object CreateDefault(object arg) { return Create(new[] { arg }); }
        public virtual object CreateDefault(params object[] args) { return Create(args); }

        protected abstract TObject Create(object[] args);
    }
}