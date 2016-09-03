using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.CV.Collections
{
    public class IntList : BaseValueList<int>, IEnumerator<int>, IEnumerator
    {
        unsafe int* _bufferPointer;
        int _enumeratorCount; 

        #region Constructors and Destructors
        public IntList(int capacity)
        {
            unsafe
            {
                _bufferPointer = (int*)AllocateBuffer(capacity).ToPointer();
            }
        }

        public IntList()
        {
            unsafe
            {
                _bufferPointer = (int*)AllocateBuffer(DefaultElementCount); 
            }
        }

        ~IntList()
        {
            base.Dispose(true); 
        }
        #endregion

        #region abstract
        public override int this[int index]
        {
            get
            {
                unsafe
                {
                    return _bufferPointer[index]; 
                }
            }

            set
            {
                unsafe
                {
                    _bufferPointer[index] = value; 
                }
            }
        }

        public override void Add(int item)
        {
            unsafe
            {
                _bufferPointer[base._numberOfElements++] = item;
            }
        }

        public override void Clear()
        {
            base._numberOfElements = 0; 
        }

        public override bool Contains(int item)
        {
            unsafe
            {
                for(int c=0;c<base._numberOfElements;c++)
                {
                    if (_bufferPointer[c] == item) return true; 
                }
                return false; 
            }
        }

        public override void CopyTo(int[] array, int arrayIndex)
        {
            unsafe
            {
                for (int c = arrayIndex; c < base._numberOfElements; c++)
                {
                    array[c] = _bufferPointer[c];
                }
            }
        }

        public override IEnumerator<int> GetEnumerator()
        {
            return this; 
        }

        public override int IndexOf(int item)
        {
            unsafe
            {
                for(int c=0;c<base._numberOfElements;c++)
                {
                    if (_bufferPointer[c] == item) return c; 
                }
                return -1; 
            }
        }

        public override void Insert(int index, int item)
        {
            unsafe
            {
                _bufferPointer[index] = item; 
            }
        }

        public override bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Enumerable
        public bool MoveNext()
        {
            _enumeratorCount++;
            if(_enumeratorCount < base._numberOfElements)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public void Reset()
        {
            _enumeratorCount = 0; 
        }

        protected override IEnumerator GetIEnumerableEnumerator()
        {
            return this; 
        }

        public int Current
        {
            get
            {
                unsafe
                {
                    return _bufferPointer[_enumeratorCount];
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                unsafe
                {
                    return _bufferPointer[_enumeratorCount];
                }
            }
        }
        #endregion
    }
}
