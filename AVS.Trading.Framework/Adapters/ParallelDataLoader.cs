using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;

namespace AVS.Trading.Framework.Adapters
{
    public static class ParallelDataLoader
    {
        /*
        public static async Task<IList<TEntity>> ParallelLoadAsync<T, TEntity>(
            Func<Response<IList<T>>> loadDataFunc,
            Func<IList<TEntity>> readFromDbFunc,
            Func<IList<T>, IList<TEntity>, IList<TEntity>> processFunc)
        {
            //load data from exchange as a primary source
            
            Task<Response<IList<T>>> loadFromExchangeTask = Task.Run(loadDataFunc);
            //load data from DB as a secondary source 
            //that might contain extra properties which are missing in the primary source
            Task<IList<TEntity>> loadFromDbTask = Task.Run(readFromDbFunc);
            
            var response = await loadFromExchangeTask;
            if (response.HasError)
                throw new LoadDataException(response);

            //convert exchange data into domain entities through preprocessor
            return processFunc(response.Data, await loadFromDbTask);
        }

        public static async Task ParallelLoadAsync<T1, T2>(
            Func<Response<IList<T1>>> loadDataFunc1,
            Func<Response<IList<T2>>> loadDataFunc2,
            Action<IList<T1>, IList<T2>> process)
        {
            Task<Response<IList<T1>>> loadTask1 = Task.Run(loadDataFunc1);
            Task<Response<IList<T2>>> loadTask2 = Task.Run(loadDataFunc2);

            await Task.WhenAll(loadTask1, loadTask2);

            var response1 = loadTask1.Result;
            if (response1.HasError)
                throw new LoadDataException(response1);

            var response2 = loadTask2.Result;
            if (response2.HasError)
                throw new LoadDataException(response2);

            process(response1.Data, response2.Data);
        }

        public static async Task ParallelLoadAsync<T1, T2, TEntity>(
            Func<Response<IList<T1>>> loadDataFunc1,
            Func<Response<IList<T2>>> loadDataFunc2,
            Func<IList<TEntity>> readFromDbFunc,
            Action<IList<T1>, IList<T2>, IList<TEntity>> process)
        {
            Task<Response<IList<T1>>> loadTask1 = Task.Run(loadDataFunc1);
            Task<Response<IList<T2>>> loadTask2 = Task.Run(loadDataFunc2);
            Task<IList<TEntity>> readFromDbTask = Task.Run(readFromDbFunc);
            await Task.WhenAll(loadTask1, loadTask2);
            

            var response1 = loadTask1.Result;
            if (response1.HasError)
                throw new LoadDataException(response1);

            var response2 = loadTask2.Result;
            if (response2.HasError)
                throw new LoadDataException(response2);

            process(response1.Data, response2.Data, await readFromDbTask);
        }
        */
        public static void ParallelLoad<T1, T2, TEntity>(
            Func<Response<IList<T1>>> loadDataFunc1,
            Func<Response<IList<T2>>> loadDataFunc2,
            Func<IList<TEntity>> readFromDbFunc,
            Action<IList<T1>, IList<T2>, IList<TEntity>> process)
        {
            Task<Response<IList<T1>>> loadTask1 = Task.Run(loadDataFunc1);
            Task<Response<IList<T2>>> loadTask2 = Task.Run(loadDataFunc2);
            Task<IList<TEntity>> readFromDbTask = Task.Run(readFromDbFunc);

            Task.WaitAll(loadTask1, loadTask2);

            var response1 = loadTask1.Result;
            if (false == response1.Success)
                throw new LoadDataException(response1);

            var response2 = loadTask2.Result;
            if (false == response1.Success)
                throw new LoadDataException(response2);

            process(response1.Data, response2.Data, readFromDbTask.Result);
        }
    }
}