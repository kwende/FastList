using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.CV.Collections
{
    public abstract class BaseValueList<T> : IList<T>, IDisposable
    {
        #region privates
        protected const int DefaultElementCount = 512 * 424;
        public IntPtr InternalArray { get; private set; }
        protected int _capacity = 0, _numberOfElements = 0;
        protected int ItemLength { get; private set; }
        #endregion

        public BaseValueList()
        {
            ItemLength = Marshal.SizeOf<T>(); 
        }

        #region IList<T>
        public abstract T this[int index]
        {
            get;set;
        }

        public int Count
        {
            get
            {
                return _numberOfElements; 
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true; 
            }
        }

        public abstract void Add(T item);

        public abstract void Clear();

        public abstract bool Contains(T item);

        public abstract void CopyTo(T[] array, int arrayIndex); 

        public abstract IEnumerator<T> GetEnumerator(); 

        public abstract int IndexOf(T item);

        public abstract void Insert(int index, T item); 

        public abstract bool Remove(T item);

        public abstract void RemoveAt(int index); 

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIEnumerableEnumerator(); 
        }

        protected abstract IEnumerator GetIEnumerableEnumerator();

        protected abstract void CopyBuffers(IntPtr source, IntPtr destination, int numberOfElements); 
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(false); 
        }
        protected void Dispose(bool fromGC)
        {
            if(fromGC)
            {
                GC.SuppressFinalize(this); 
            }

            if(InternalArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(InternalArray); 
            }
        }
        #endregion

        #region Allocators
        protected IntPtr AllocateBuffer(int capacity)
        {
            InternalArray = Marshal.AllocHGlobal(ItemLength * capacity);
            _capacity = capacity; 
            return InternalArray; 
        }
        protected IntPtr ReallocateBuffer(int newCapacity,
            int currentNumberOfElements)
        {
            IntPtr newBuffer = Marshal.AllocHGlobal(ItemLength * newCapacity);

            CopyBuffers(InternalArray, newBuffer, currentNumberOfElements); 

            Marshal.FreeHGlobal(InternalArray);

            InternalArray = newBuffer;
            _capacity = newCapacity;

            return newBuffer; 
        }
        #endregion
    }
}
