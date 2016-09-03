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
        #endregion

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
            throw new NotImplementedException(); 
        }
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
        protected static IntPtr AllocateBuffer(int numberOfElements)
        {
            int tSize = Marshal.SizeOf<T>();
            return Marshal.AllocHGlobal(tSize * numberOfElements);
        }
        protected static IntPtr ReallocateBuffer(int newNumberOfElements,
            int currentNumberOfElements, IntPtr currentBuffer)
        {
            IntPtr newBuffer = AllocateBuffer(newNumberOfElements);

            unsafe
            {
                Byte* currentPointer = (Byte*)currentBuffer.ToPointer();
                Byte* newPointer = (Byte*)newBuffer.ToPointer();
                int tSize = Marshal.SizeOf<T>();

                for (int c = 0; c < currentNumberOfElements * tSize; c++)
                {
                    newPointer[c] = currentPointer[c];
                }
            }

            Marshal.FreeHGlobal(currentBuffer);

            return newBuffer; 
        }
        #endregion
    }
}
