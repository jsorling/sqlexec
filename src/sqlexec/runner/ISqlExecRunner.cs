using Sorling.SqlExec.mapper.commands;
using Sorling.SqlExec.mapper.results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sorling.SqlExec.runner;

public interface ISqlExecRunner
{
   public IEnumerable<T> Query<T, P>(P parameters) where P : SqlExecBaseCommand where T : SqlExecBaseResult;

   public T? QueryFirstRow<T, P>(P parameters) where P : SqlExecBaseCommand where T : SqlExecBaseResult;

   public Task<IEnumerable<T>> QueryAsync<T, P>(P parameters, CancellationToken cancellationToken = default)
      where P : SqlExecBaseCommand where T : SqlExecBaseResult;

   public Task<T?> QueryFirstRowAsync<T, P>(P parameters, CancellationToken cancellationToken = default)
      where P : SqlExecBaseCommand where T : SqlExecBaseResult;

   public int Execute<P>(P parameters) where P : SqlExecBaseCommand;

   public int ExecuteGOScript<P>(P parameters) where P : SqlExecBaseCommand;

   public Task<int> ExecuteAsync<P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand;

   public Task<int> ExecuteGOScriptAsync<P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand;

   public T? ExecuteScalar<T, P>(P parameters) where P : SqlExecBaseCommand;

   public Task<T?> ExecuteScalarAsync<T, P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>)> QueryAsync<T, T1, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryAsync<T, T1, T2, T3, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryAsync<T, T1, T2, T3, T4, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)> QueryAsync<T, T1, T2, T3, T4, T5, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)> QueryAsync<T, T1, T2, T3, T4, T5, T6, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>)> QueryAsync<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>) Query<T, T1, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) Query<T, T1, T2, T3, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) Query<T, T1, T2, T3, T4, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>) Query<T, T1, T2, T3, T4, T5, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>) Query<T, T1, T2, T3, T4, T5, T6, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where P : SqlExecBaseCommand;

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>) Query<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand;
}
