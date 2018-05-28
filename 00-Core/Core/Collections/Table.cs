using System;
using System.Collections.Generic;
using System.Linq;

namespace AMKsGear.Core.Collections
{
    public interface ITable
    {
        IEnumerable<object> LeftColumns { get; }
        IEnumerable<object> RightColumns { get; }

        IEnumerable<TableRow<object, object>> AllRows { get; set; }
    }

    public class TableRow<TLeft, TRight>
    {
        public TLeft Left;
        public TRight Right;
    }
    public class Table<TLeft, TRight> : ITable
    {
        protected readonly List<TableRow<TLeft, TRight>> Rows = new List<TableRow<TLeft, TRight>>();

        public bool AllowMulti { get; protected set; }
        public IEqualityComparer<TLeft> LeftComparer { get; protected set; }
        public IEqualityComparer<TRight> RightComparer { get; protected set; }


        public Table()
        {
            LeftComparer = EqualityComparer<TLeft>.Default;
            RightComparer = EqualityComparer<TRight>.Default;
        }
        public Table(Dictionary<TLeft, TRight> initialDictionary)
        {
            AddRange(initialDictionary.Select(x => new TableRow<TLeft, TRight>
            {
                Left = x.Key,
                Right = x.Value
            }));
        }


        public bool Contains(TLeft left, TRight right)
        {
            foreach (var row in Rows)
                if (LeftComparer.Equals(row.Left, left) && RightComparer.Equals(row.Right, right))
                    return true;
            return false;
        }
        public bool Contains(TableRow<TLeft, TRight> crow)
        {
            foreach (var row in Rows)
                if (LeftComparer.Equals(row.Left, crow.Left) && RightComparer.Equals(row.Right, crow.Right))
                    return true;
            return false;
        }
        public void Add(TLeft left, TRight right)
        {
            if (Contains(left, right) && !AllowMulti) throw new InvalidOperationException("Row with same left and right already exists.");
            Rows.Add(new TableRow<TLeft, TRight>
            {
                Left = left,
                Right = right
            });
        }
        public void AddRange(IEnumerable<TableRow<TLeft, TRight>> rows)
        {
            foreach (var row in rows)
            {
                if (Contains(row) && !AllowMulti) throw new InvalidOperationException("Row with same left and right already exists.");
                Rows.Add(new TableRow<TLeft, TRight>
                {
                    Left = row.Left,
                    Right = row.Right
                });
            }
        }

        IEnumerable<object> ITable.LeftColumns { get { yield return LeftColumns; } }
        public IEnumerable<TLeft> LeftColumns
        {
            get
            {
                foreach (var row in Rows)
                    yield return row.Left;
            }
        }
        IEnumerable<object> ITable.RightColumns { get { yield return RightColumns; } }

        public IEnumerable<TableRow<object, object>> AllRows
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<TRight> RightColumns
        {
            get
            {
                foreach (var row in Rows)
                    yield return row.Right;
            }
        }
    }
}