using System;
using System.Threading.Tasks;
using core.Services;

namespace core
{
    public static class AsyncHelpers
    {
        // helper to run an async method synchronously with return type
        public static TResult RunSync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            // run the async method in seperate thread to prevent locking parent thread
            var awaitableTask = Task.Run(asyncFunc);

            // don't aggregate excepetions
            return awaitableTask.GetAwaiter().GetResult();
        }

        // helper to run an async void method synchronously
        public static void RunSync(Func<Task> asyncFunc)
        {
            // run the async method in seperate thread to prevent locking parent thread
            var awaitableTask = Task.Run(asyncFunc);

            // don't aggregate excepetions
            awaitableTask.GetAwaiter().GetResult();
        }
    }
}
