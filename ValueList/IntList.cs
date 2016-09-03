using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.CV.Collections
{
    public class IntList : BaseValueList<int>
    {
        unsafe int* _bufferPointer;

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
            throw new NotImplementedException();
        }

        public override bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public override void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
