/*
* NAME : CancelToken
* PURPOSE : The CancelToken is a simplified version of CancellationTokenSource that help close the write thread by the monitor thread.
*           It also serves as the file lock.
*/

namespace A_04
{
    class CancelToken
    {
        public bool Run { set; get; }

        public CancelToken()
        {
            this.Run = true;
        }
    }
}
