namespace AMKsGear.Architecture.Modeling
{
    public interface IView
    {

    }
    public interface IView<TModel>
        where TModel : IModel
    {

    }
}