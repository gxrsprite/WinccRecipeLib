using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Ruifei.AppMain
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// The default constructor of the ObservableCollection
        /// </summary>
        public ObservableRangeCollection()
        {
            //Assign the current Dispatcher (owner of the collection)
            _currentDispatcher = Dispatcher.CurrentDispatcher;
        }

        public ObservableRangeCollection(IEnumerable<T> collection)
           : base(collection)
        {
        }
        public ObservableRangeCollection(List<T> list) : base(list)
        {

        }

        private readonly Dispatcher _currentDispatcher;

        /// <summary>
        /// Executes this action in the right thread
        /// </summary>
        ///<param name="action">The action which should be executed</param>
        private void DoDispatchedAction(Action action)
        {
            if (_currentDispatcher.CheckAccess())
                action.Invoke();
            else
                _currentDispatcher.Invoke(DispatcherPriority.DataBind, action);
        }

        /// <summary>
        /// Clears all items
        /// </summary>
        protected override void ClearItems()
        {
            DoDispatchedAction(() => base.ClearItems());
        }

        /// <summary>
        /// Inserts a item at the specified index
        /// </summary>
        ///<param name="index">The index where the item should be inserted</param>
        ///<param name="item">The item which should be inserted</param>
        protected override void InsertItem(int index, T item)
        {
            DoDispatchedAction(() => base.InsertItem(index, item));
        }

        /// <summary>
        /// Moves an item from one index to another
        /// </summary>
        ///<param name="oldIndex">The index of the item which should be moved</param>
        ///<param name="newIndex">The index where the item should be moved</param>
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            DoDispatchedAction(() => base.MoveItem(oldIndex, newIndex));
        }

        /// <summary>
        /// Removes the item at the specified index
        /// </summary>
        ///<param name="index">The index of the item which should be removed</param>
        protected override void RemoveItem(int index)
        {
            DoDispatchedAction(() => base.RemoveItem(index));
        }

        /// <summary>
        /// Sets the item at the specified index
        /// </summary>
        ///<param name="index">The index which should be set</param>
        ///<param name="item">The new item</param>
        protected override void SetItem(int index, T item)
        {
            DoDispatchedAction(() => base.SetItem(index, item));
        }

        /// <summary>
        /// Fires the CollectionChanged Event
        /// </summary>
        ///<param name="e">The additional arguments of the event</param>
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DoDispatchedAction(() => base.OnCollectionChanged(e));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        /// <summary>
        /// Fires the PropertyChanged Event
        /// </summary>
        ///<param name="e">The additional arguments of the event</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            DoDispatchedAction(() => base.OnPropertyChanged(e));
        }

        private void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void LimitedCountAdd(T item, int limited)
        {
            var baseList = base.Items as List<T>;
            if (baseList == null)
            {
                throw new Exception("type of base.Items is not List<T>");
            }
            baseList.Add(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, baseList.Count - 1);
            if (baseList.Count > limited && limited > 0)
            {
                var removeItem = baseList[0];
                baseList.RemoveAt(0);
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, removeItem, 0);
            }
            OnPropertyChanged("Item[]");
            OnPropertyChanged("Count");
        }

        public void AddRange(IEnumerable<T> list)
        {
            var baseList = base.Items as List<T>;
            if (baseList == null)
            {//万一哪一天base.Items不是List<T>了，报错
                throw new Exception("type of base.Items is not List<T>");
            }
            DoDispatchedAction(() =>
            {
                baseList.AddRange(list);
                OnPropertyChanged("Count");
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Add, null, 0);
            });
        }
    }
}
