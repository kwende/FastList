using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.CV.Collections
{
    public class IntList : IList<int>, IDisposable
    {
        #region Members
        const int DefaultElementCount = 512 * 424;
        public IntPtr InternalArray { get; private set; }
        private int _capacity = 0, _numberOfElements = 0;
        unsafe int* _buffer; 
        #endregion

        #region Constructors & Destructors
        public IntList(int capacity)
        {
            _capacity = capacity;
            InternalArray = AllocateBuffer(_capacity);
            unsafe
            {
                _buffer = (int*)InternalArray.ToPointer();
            }
        }

        public IntList()
        {
            _capacity = DefaultElementCount;
            InternalArray = AllocateBuffer(_capacity);
            unsafe
            {
                _buffer = (int*)InternalArray.ToPointer(); 
            }
        }

        ~IntList()
        {
            Dispose(true);
        }
        #endregion

        #region Privates
        private static IntPtr AllocateBuffer(int numberOfElements)
        {
            int tSize = Marshal.SizeOf<int>();
            return Marshal.AllocHGlobal(tSize * numberOfElements);
        }
        private static void ReallocateBuffer(int newNumberOfElements,
            int currentNumberOfElements, IntPtr currentBuffer)
        {
            IntPtr newBuffer = AllocateBuffer(newNumberOfElements);

            unsafe
            {
                int* currentPointer = (int*)currentBuffer.ToPointer();
                int* newPointer = (int*)newBuffer.ToPointer();

                for (int c = 0; c < currentNumberOfElements; c++)
                {
                    newPointer[c] = currentPointer[c];
                }
            }

            Marshal.FreeHGlobal(currentBuffer);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(false);
        }
        private void Dispose(bool fromGC)
        {
            if (!fromGC)
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

        #region IList<int>
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

        public int this[int index]
        {
            get
            {
                unsafe
                {
                    int* buffer = (int*)InternalArray.ToPointer();
                    return buffer[index];  
                }
            }

            set
            {
                unsafe
                {
                    int* buffer = (int*)InternalArray.ToPointer();
                    buffer[index] = value; 
                }
            }
        }
        public int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(int item)
        {
            unsafe
            {
                //int* buffer = (int*)InternalArray.ToPointer();
                _buffer[_numberOfElements++] = item;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
