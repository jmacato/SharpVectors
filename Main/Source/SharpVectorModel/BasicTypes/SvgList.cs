// <developer>kevin@kevlindev.com</developer>
// <completed>100</completed>
using System.Collections.Generic;
namespace SharpVectors.Dom.Svg
    /// Note we're using <see cref="List{T}"/> (as opposed to deriving from) to hide unneeded <see cref="List{T}"/> methods
	public abstract class SvgList<T> : IEnumerable<T>

        private static Hashtable itemOwnerMap;
        #region Constructor
            get { return (uint) items.Count; }
        }

        /// <summary>
        public void Clear()
        {
            // Note that we cannot use List<T>'s Clear method since we need to
            // remove all items from the itemOwnerMap
            while ( items.Count > 0 ) 
                RemoveItem(0);
        }

        /// <summary>
        public T Initialize(T newItem)
        {
            Clear();
            return AppendItem(newItem);
        }

        /// <summary>
        public T GetItem(uint index)
        {
            if ( index < 0 || items.Count <= index )
                throw new DomException(DomExceptionType.IndexSizeErr);

            return items[(int) index];
        }

        /// <summary>
        public T InsertItemBefore(T newItem, uint index)
        {
            if ( index < 0 || items.Count <= index )
                throw new DomException(DomExceptionType.IndexSizeErr);

            // cache cast
            int i = (int) index;

            // if newItem exists in a list, remove it from that list
            if ( SvgList<T>.itemOwnerMap.ContainsKey(newItem) )
                ((SvgList<T>)SvgList<T>.itemOwnerMap[newItem]).RemoveItem(newItem);

            // insert item into this list
            items.Insert(i, newItem);

            // update the itemOwnerMap to associate newItem with this list
            SvgList<T>.itemOwnerMap[newItem] = this;

            return items[i];
        }

        /// <summary>
        public T ReplaceItem(T newItem, uint index)
        {
            if ( index < 0 || items.Count <= index )
                throw new DomException(DomExceptionType.IndexSizeErr);

            // cache cast
            int i = (int) index;

            // if newItem exists in a list, remove it from that list
            if (SvgList<T>.itemOwnerMap.ContainsKey(newItem))
                ((SvgList<T>)SvgList<T>.itemOwnerMap[newItem]).RemoveItem(newItem);

            // remove oldItem from itemOwnerMap
            SvgList<T>.itemOwnerMap.Remove(items[i]);

            // update the itemOwnerMap to associate newItem with this list
            SvgList<T>.itemOwnerMap[newItem] = this;

            // store newItem and return
            return items[i] = newItem;
        }

        /// <summary>
        public T RemoveItem(uint index)
        {
            if ( index < 0 || items.Count <= index )
                throw new DomException(DomExceptionType.IndexSizeErr);

            // cache cast
            int i = (int) index;

            // save removed item so we can return it
            T result = items[i];

            // item is longer associated with this list, so remove item from itemOwnerMap
            SvgList<T>.itemOwnerMap.Remove(result);

            // remove item from this list
            items.RemoveAt(i);

            // return removed item
            return result;
        }

        /// <summary>
        public T AppendItem(T newItem)
            if (SvgList<T>.itemOwnerMap.ContainsKey(newItem))
                ((SvgList<T>)SvgList<T>.itemOwnerMap[newItem]).RemoveItem(newItem);
            // update the itemOwnerMap to associate newItem with this list
            SvgList<T>.itemOwnerMap[newItem] = this;
            // add item and return
            return newItem;
        #region IEnumerable Interface
            return items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

            int index = items.IndexOf(item);
                RemoveItem((uint)index);
    }