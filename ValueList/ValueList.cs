using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.CV.Collections
{
    public class ValueList<T> : IList<T>, IDisposable where T : struct
    {
        #region Members
        const int DefaultElementCount = 512 * 424;
        public IntPtr InternalArray { get; private set; }
        private int _capacity = 0, _numberOfElements = 0; 
        #endregion

        #region Constructors & Destructors
        public ValueList(int capacity)
        {
            _capacity = capacity;
            InternalArray = AllocateBuffer(_capacity); 
        }

        public ValueList()
        {
            _capacity = DefaultElementCount;
            InternalArray = AllocateBuffer(_capacity); 
        }

        ~ValueList()
        {
            Dispose(true); 
        }
        #endregion

        #region Privates
        private static IntPtr AllocateBuffer(int numberOfElements)
        {
            int tSize = Marshal.SizeOf<T>();
            return Marshal.AllocHGlobal(tSize * numberOfElements);
        }
        private static void ReallocateBuffer(int newNumberOfElements, 
            int currentNumberOfElements, IntPtr currentBuffer)
        {
            IntPtr newBuffer = AllocateBuffer(newNumberOfElements);

            unsafe
            {
                int numberOfBytes = Marshal.SizeOf<T>() * currentNumberOfElements;

                Byte* currentPointer = (Byte*)currentBuffer.ToPointer();
                Byte* newPointer = (Byte*)newBuffer.ToPointer(); 

                for(int c=0;c<numberOfBytes;c++)
                {
                    newPointer[c] = currentPointer[c]; 
                }
            }

            Marshal.FreeHGlobal(currentBuffer); 
        }
        #endregion

        #region List<T>
        public T this[int index]
        {
            get
            {
                unsafe
                {
                    int tSize = Marshal.SizeOf<T>();
                    int offset = index * tSize;
                    return Marshal.PtrToStructure<T>(IntPtr.Add(InternalArray, offset)); 
                }
            }

            set
            {
                int tSize = Marshal.SizeOf<T>();
                int offset = index * tSize;
                Marshal.StructureToPtr<T>(value, IntPtr.Add(InternalArray, offset), false); 
            }
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            _numberOfElements++; 

            int tSize = Marshal.SizeOf<T>();
            int offset = _numberOfElements * tSize;
            Marshal.StructureToPtr<T>(item, IntPtr.Add(InternalArray, offset), false);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

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
        private void Dispose(bool fromGC)
        {
            if(!fromGC)
            {
                GC.SuppressFinalize(this); 
            }

            if (InternalArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(InternalArray);
                InternalArray = IntPtr.Zero; 
            }
        }
        #endregion
    }
}
