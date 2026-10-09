using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Network.Server
{
    public sealed class CSocketEventPool
    {
        private readonly Stack<SocketAsyncEventArgs> m_pPool;

        public CSocketEventPool(int capacity)
        {
            m_pPool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        // The number of SocketAsyncEventArgs instances in the pool  
        public int Count => m_pPool.Count;

        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            lock (m_pPool)
            {
                m_pPool.Push(item);
            }
        }

        // Removes a SocketAsyncEventArgs instance from the pool  
        // and returns the object removed from the pool  
        public SocketAsyncEventArgs Pop()
        {
            lock (m_pPool)
            {
                return m_pPool.Pop();
            }
        }
    }
}