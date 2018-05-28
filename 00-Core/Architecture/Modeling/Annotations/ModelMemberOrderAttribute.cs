using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    [AttributeUsage(ConstantTable.AllMembers, AllowMultiple = false, Inherited = true)]
    public class ModelMemberOrderAttribute : ModelAnnotationAttribute
    {
        public int Order { get; protected set; }

        protected ModelMemberOrderAttribute() { }
        public ModelMemberOrderAttribute(int order)
        {
            Order = order;
        }
    }
    public class ModelMemberOrderFirstAttribute : ModelMemberOrderAttribute
    {
        public ModelMemberOrderFirstAttribute()
        {
            Order = 0;
        }
    }
    public class ModelMemberOrderLastAttribute : ModelMemberOrderAttribute
    {
        public ModelMemberOrderLastAttribute()
        {
            Order = -1;
        }
    }

    public class ModelMemberOrderInsertBeforeAttribute : ModelMemberOrderAttribute
    {
        public string MemberName { get; }
        public StringComparer NameComparer { get; }

        public ModelMemberOrderInsertBeforeAttribute(string memberName)
        {
            Order = -1;
            MemberName = memberName;
            NameComparer = StringComparer.CurrentCultureIgnoreCase;
        }
        public ModelMemberOrderInsertBeforeAttribute(string memberName, StringComparer comparer)
        {
            Order = -1;
            MemberName = memberName;
            NameComparer = comparer ?? StringComparer.CurrentCultureIgnoreCase;
        }
    }
}
