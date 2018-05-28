using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AMKsGear.Core.Automation.Support;

namespace AMKsGear.AppLayer.Core.HistoryFramework
{
    public delegate void HistoryActionEventHandler<TAction, TSender, TState>(
        HistoryManager<TAction, TSender, TState> sender,
        IHistoryAction<TAction, TSender, TState> historyAction);

    public class HistoryManager<TAction, TSender, TState>
    {
        public event EventHandler HistoryRewiseNeeded;
        public event HistoryActionEventHandler<TAction, TSender, TState> ActionAdded;
        public event HistoryActionEventHandler<TAction, TSender, TState> UndoOccured;
        public event HistoryActionEventHandler<TAction, TSender, TState> RedoOccured;
        public event EventHandler ActionOccured;

        private readonly Stack<IHistoryAction<TAction, TSender, TState>> _liveActions = new Stack<IHistoryAction<TAction, TSender, TState>>();
        private readonly Stack<IHistoryAction<TAction, TSender, TState>> _undoneActions = new Stack<IHistoryAction<TAction, TSender, TState>>();

        public virtual bool CanUndo => _liveActions.Count > 1;
        public virtual bool CanRedo => _undoneActions.Count > 0;

        public virtual int UndoCount => _liveActions.Count;
        public virtual int RedoCount => _undoneActions.Count;

        public virtual bool ClearFrontActionsOnNewAction { get; set; }
        public virtual bool BackToActionOnUndo { get; set; } = false;
        public virtual bool KeepRootOnClear { get; set; } = false;

        private readonly AutoCommand _undoCommand;
        private readonly AutoCommand _redoCommand;

        public virtual ICommand UndoCommand => _undoCommand;
        public virtual ICommand RedoCommand => _redoCommand;

        private IHistoryAction<TAction, TSender, TState> _currentAction;

        public HistoryManager()
        {
            _undoCommand = new AutoCommand(UndoCommand_Execute, () => CanUndo);
            _redoCommand = new AutoCommand(RedoCommand_Execute, () => CanRedo);
        }

        private void RedoCommand_Execute() => Redo();
        private void UndoCommand_Execute() => Undo();


        public virtual IHistoryAction<TAction, TSender, TState> CurrentAction => _currentAction;
        //public HistoryManager()
        //{

        //}

        public virtual void PushHistoryAction(IHistoryAction<TAction, TSender, TState> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            _liveActions.Push(action);
            _currentAction = action;

            if (ClearFrontActionsOnNewAction)
                _undoneActions.Clear();

            _undoCommand.OnCanExecuteChanged();

            OnActionAdded(action);
        }

        public virtual void ClearHistory()
        {
            var keepRoot = _liveActions.Count > 0 && KeepRootOnClear;
            var root = keepRoot
                ? _liveActions.LastOrDefault()
                : null;

            _liveActions.Clear();
            _undoneActions.Clear();

            if (keepRoot)
                _liveActions.Push(root);

            _undoCommand.OnCanExecuteChanged();
            _redoCommand.OnCanExecuteChanged();

            _currentAction = null;
        }

        public virtual IHistoryAction<TAction, TSender, TState> GetLiveAction(int index) => _liveActions.ElementAt(index);
        public virtual IHistoryAction<TAction, TSender, TState> GetUndoneAction(int index) => _undoneActions.ElementAt(index);

        public virtual IHistoryAction<TAction, TSender, TState> Undo()
        {
            var count = _liveActions.Count;
            if (count > 1)
            {
                var pop = _liveActions.Pop();
                if (!BackToActionOnUndo)
                    _undoneActions.Push(pop);

                var top = _liveActions.Peek();
                top.UndoAction?.Invoke(top, _currentAction);
                UndoOccured?.Invoke(this, top);
                ActionOccured?.Invoke(this, EventArgs.Empty);
                _undoCommand.OnCanExecuteChanged();
                _currentAction = top;
                return top;
            }
            return null;
        }
        public virtual IHistoryAction<TAction, TSender, TState> Redo()
        {
            if (_undoneActions.Count > 0)
            {
                _liveActions.Push(_undoneActions.Pop());

                var top = _undoneActions.Peek();
                top.RedoAction?.Invoke(top, _currentAction);
                RedoOccured?.Invoke(this, top);
                ActionOccured?.Invoke(this, EventArgs.Empty);
                _redoCommand.OnCanExecuteChanged();
                _currentAction = top;
                return top;
            }
            return null;
        }

        public virtual IHistoryAction<TAction, TSender, TState> PeekUndo()
        {
            if (_liveActions.Count > 0)
            {
                var top = _liveActions.Peek();
                return top;
            }
            return null;
        }
        public virtual IHistoryAction<TAction, TSender, TState> PeekRedo()
        {
            if (_undoneActions.Count > 0)
            {
                var top = _undoneActions.Peek();
                return top;
            }
            return null;
        }

        protected virtual void OnActionAdded(IHistoryAction<TAction, TSender, TState> historyAction)
                => ActionAdded?.Invoke(this, historyAction);
        protected virtual void OnHistoryRewiseNeeded() => HistoryRewiseNeeded?.Invoke(this, EventArgs.Empty);
    }
}