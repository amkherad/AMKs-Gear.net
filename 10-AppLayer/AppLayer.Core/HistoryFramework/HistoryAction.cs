using System;

namespace AMKsGear.AppLayer.Core.HistoryFramework
{
    public interface IHistoryAction<TAction, TSender, TState>
    {
        TAction Action { get; }
        TSender Sender { get; }
        TState State { get; }
        object Tag { get; }

        Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> UndoAction { get; }
        Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> RedoAction { get; }
    }
    public class HistoryAction<TAction, TSender, TState> : IHistoryAction<TAction, TSender, TState>
    {
        public TAction Action { get; protected set; }
        public TSender Sender { get; protected set; }
        public TState State { get; protected set; }
        public object Tag { get; set; }

        public Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> UndoAction { get; }
        public Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> RedoAction { get; }

        public HistoryAction(TAction action, TSender sender, TState state,
            Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> undoAction,
            Action<IHistoryAction<TAction, TSender, TState>, IHistoryAction<TAction, TSender, TState>> redoAction)
        {
            //if (undoAction == null) throw new ArgumentNullException(nameof(undoAction));
            //if (redoAction == null) throw new ArgumentNullException(nameof(redoAction));

            Action = action;
            Sender = sender;
            State = state;
            UndoAction = undoAction;
            RedoAction = redoAction;
        }
    }
    public class HistoryAction<TAction, TState> : HistoryAction<TAction, object, TState>
    {
        public HistoryAction(TAction action, TState state,
            Action<IHistoryAction<TAction, object, TState>, IHistoryAction<TAction, object, TState>> undoAction,
            Action<IHistoryAction<TAction, object, TState>, IHistoryAction<TAction, object, TState>> redoAction)
            : base(action, null, state, undoAction, redoAction)
        { }
    }
    public class HistoryAction<TAction> : HistoryAction<TAction, object>
    {
        public HistoryAction(TAction action,
            Action<IHistoryAction<TAction, object, object>, IHistoryAction<TAction, object, object>> undoAction,
            Action<IHistoryAction<TAction, object, object>, IHistoryAction<TAction, object, object>> redoAction)
            : base(action, null, undoAction, redoAction)
        { }
    }
}
