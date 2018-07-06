using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ITagged<TTag>
    {
        TTag Tag { get; set; }
    }
    public interface ITaggedEntity<TTag> : ITagged<TTag>, IEntity { }
    public interface ITaggedModel<TTag> : ITagged<TTag>, IModel { }
    
    public interface IObjectTagged : ITagged<object> { }
    public interface IObjectTaggedEntity : ITagged<object>, IEntity { }
    public interface IObjectTaggedModel : ITagged<object>, IModel { }

    public interface IStringTagged : ITagged<string> { }
    public interface IStringTaggedEntity : ITagged<string>, IEntity { }
    public interface IStringTaggedModel : ITagged<string>, IModel { }
}
