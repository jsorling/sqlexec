using Sorling.SqlExec.mapper;
using Sorling.SqlExec.mapper.commands;
using Sorling.SqlExec.mapper.extensions;
using Sorling.SqlExec.mapper.results;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sorling.SqlExec.runner;

public partial class SqlExecRunner(string sqlConnectionString) : ISqlExecRunner
{
   private readonly string _sqlconnectionstring = sqlConnectionString
      ?? throw new ArgumentNullException(nameof(sqlConnectionString));

   protected virtual SqlConnection GetSqlConnection()
      => new(_sqlconnectionstring);

   protected virtual SqlCommand CreatePreparedCommand<P>(SqlConnection sqlConnection, P parameters)
      where P : SqlExecBaseCommand
      => sqlConnection.SqlExecCreatePreparedCommand(parameters);

   protected virtual void OpenConnection(SqlConnection sqlConnection)
      => sqlConnection.Open();

   protected virtual async Task OpenConnectionAsync(SqlConnection sqlConnection, CancellationToken cancellationToken)
      => await sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);

   public IEnumerable<T> Query<T, P>(P parameters)
   where T : SqlExecBaseResult
   where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      OpenConnection(sqlconnection);
      using SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
      List<T> tor = [];
      while (sqldatareader.Read())
         tor.Add(sqldatareader.SqlExecMapRow<T>());

      return tor;
   }

   public T? QueryFirstRow<T, P>(P parameters)
      where T : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      OpenConnection(sqlconnection);
      using SqlDataReader sqldatareader = sqlcommand.ExecuteReader();

      return sqldatareader.Read() ? sqldatareader.SqlExecMapRow<T>() : null;
   }

   public async Task<IEnumerable<T>> QueryAsync<T, P>(P parameters, CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);
      using SqlDataReader sqldatareader = await sqlcommand
         .ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

      List<T> tor = [];
      while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
         tor.Add(sqldatareader.SqlExecMapRow<T>());

      return tor;
   }

   public (IEnumerable<T>, IEnumerable<T1>) Query<T, T1, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, _, _, _, _, _, _) = QueryMultiSets<T, T1, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>) Query<T, T1, T2, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, _, _, _, _, _) = QueryMultiSets<T, T1, T2, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2, tor3!);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) Query<T, T1, T2, T3, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, _, _, _, _)
         = QueryMultiSets<T, T1, T2, T3, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2, tor3!, tor4!);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) Query<T, T1, T2, T3, T4, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, _, _, _)
         = QueryMultiSets<T, T1, T2, T3, T4, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2, tor3!, tor4!, tor5!);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>) Query<T, T1, T2, T3, T4, T5, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, _, _)
         = QueryMultiSets<T, T1, T2, T3, T4, T5, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>) Query<T, T1, T2, T3, T4, T5, T6, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, IEnumerable<T6>? tor7, _)
         = QueryMultiSets<T, T1, T2, T3, T4, T5, T6, TQueryEmptyPlaceHolder, P>(parameters);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!, tor7!);
   }

   public (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>) Query<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, IEnumerable<T6>? tor7, IEnumerable<T7>? tor8)
         = QueryMultiSets<T, T1, T2, T3, T4, T5, T6, T7, P>(parameters);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!, tor7!, tor8!);
   }

   private (IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>?, IEnumerable<T3>?, IEnumerable<T4>?, IEnumerable<T5>?
      , IEnumerable<T6>?, IEnumerable<T7>?) QueryMultiSets<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      OpenConnection(sqlconnection);
      using SqlDataReader sqldatareader = sqlcommand
         .ExecuteReader();

      List<T> tor = [];
      while (sqldatareader.Read())
         tor.Add(sqldatareader.SqlExecMapRow<T>());

      List<T1> tor1 = [];
      if (typeof(T1) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a second record set in the query");
         }

         while (sqldatareader.Read())
            tor1.Add(sqldatareader.SqlExecMapRow<T1>());
      }
      else
         return (tor, tor1, null, null, null, null, null, null);

      List<T2> tor2 = [];
      if (typeof(T2) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a third record set in the query");
         }

         while (sqldatareader.Read())
            tor2.Add(sqldatareader.SqlExecMapRow<T2>());
      }
      else
         return (tor, tor1, tor2, null, null, null, null, null);

      List<T3> tor3 = [];
      if (typeof(T3) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a fourth record set in the query");
         }

         while (sqldatareader.Read())
            tor3.Add(sqldatareader.SqlExecMapRow<T3>());
      }
      else
         return (tor, tor1, tor2, tor3, null, null, null, null);

      List<T4> tor4 = [];
      if (typeof(T4) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a fifth record set in the query");
         }

         while (sqldatareader.Read())
            tor4.Add(sqldatareader.SqlExecMapRow<T4>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, null, null, null);

      List<T5> tor5 = [];
      if (typeof(T5) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a sixth record set in the query");
         }

         while (sqldatareader.Read())
            tor5.Add(sqldatareader.SqlExecMapRow<T5>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, tor5, null, null);

      List<T6> tor6 = [];
      if (typeof(T6) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a seventh record set in the query");
         }

         while (sqldatareader.Read())
            tor6.Add(sqldatareader.SqlExecMapRow<T6>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, tor5, tor6, null);

      List<T7> tor7 = [];
      if (typeof(T7) != typeof(TQueryEmptyPlaceHolder)) {
         if (!sqldatareader.NextResult()) {
            throw new InvalidOperationException("There isn't a eighth record set in the query");
         }

         while (sqldatareader.Read())
            tor7.Add(sqldatareader.SqlExecMapRow<T7>());
      }

      return (tor, tor1, tor2, tor3, tor4, tor5, tor6, tor7);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>)> QueryAsync<T, T1, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, _, _, _, _, _, _) = await QueryMultiSetsAsync<T, T1, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>)> QueryAsync<T, T1, T2, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, _, _, _, _, _) = await QueryMultiSetsAsync<T, T1, T2, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> QueryAsync<T, T1, T2, T3, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, _, _, _, _)
         = await QueryMultiSetsAsync<T, T1, T2, T3, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!, tor4!);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryAsync<T, T1, T2, T3, T4, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, _, _, _)
         = await QueryMultiSetsAsync<T, T1, T2, T3, T4, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!, tor4!, tor5!);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)> QueryAsync<T, T1, T2, T3, T4, T5, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, _, _)
         = await QueryMultiSetsAsync<T, T1, T2, T3, T4, T5, TQueryEmptyPlaceHolder, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>)> QueryAsync<T, T1, T2, T3, T4, T5, T6, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, IEnumerable<T6>? tor7, _)
         = await QueryMultiSetsAsync<T, T1, T2, T3, T4, T5, T6, TQueryEmptyPlaceHolder, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!, tor7!);
   }

   public async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>)> QueryAsync<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      (IEnumerable<T> tor1, IEnumerable<T1> tor2, IEnumerable<T2>? tor3, IEnumerable<T3>? tor4, IEnumerable<T4>? tor5, IEnumerable<T5>? tor6, IEnumerable<T6>? tor7, IEnumerable<T7>? tor8)
         = await QueryMultiSetsAsync<T, T1, T2, T3, T4, T5, T6, T7, P>(parameters, cancellationToken).ConfigureAwait(false);
      return (tor1, tor2, tor3!, tor4!, tor5!, tor6!, tor7!, tor8!);
   }

   private async Task<(IEnumerable<T>, IEnumerable<T1>, IEnumerable<T2>?, IEnumerable<T3>?, IEnumerable<T4>?, IEnumerable<T5>?
      , IEnumerable<T6>?, IEnumerable<T7>?)> QueryMultiSetsAsync<T, T1, T2, T3, T4, T5, T6, T7, P>(P parameters
      , CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where T1 : SqlExecBaseResult
      where T2 : SqlExecBaseResult
      where T3 : SqlExecBaseResult
      where T4 : SqlExecBaseResult
      where T5 : SqlExecBaseResult
      where T6 : SqlExecBaseResult
      where T7 : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);
      using SqlDataReader sqldatareader = await sqlcommand
         .ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

      List<T> tor = [];
      while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
         tor.Add(sqldatareader.SqlExecMapRow<T>());

      List<T1> tor1 = [];
      if (typeof(T1) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a second record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor1.Add(sqldatareader.SqlExecMapRow<T1>());
      }
      else
         return (tor, tor1, null, null, null, null, null, null);

      List<T2> tor2 = [];
      if (typeof(T2) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a third record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor2.Add(sqldatareader.SqlExecMapRow<T2>());
      }
      else
         return (tor, tor1, tor2, null, null, null, null, null);

      List<T3> tor3 = [];
      if (typeof(T3) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a fourth record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor3.Add(sqldatareader.SqlExecMapRow<T3>());
      }
      else
         return (tor, tor1, tor2, tor3, null, null, null, null);

      List<T4> tor4 = [];
      if (typeof(T4) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a fifth record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor4.Add(sqldatareader.SqlExecMapRow<T4>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, null, null, null);

      List<T5> tor5 = [];
      if (typeof(T5) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a sixth record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor5.Add(sqldatareader.SqlExecMapRow<T5>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, tor5, null, null);

      List<T6> tor6 = [];
      if (typeof(T6) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a seventh record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor6.Add(sqldatareader.SqlExecMapRow<T6>());
      }
      else
         return (tor, tor1, tor2, tor3, tor4, tor5, tor6, null);

      List<T7> tor7 = [];
      if (typeof(T7) != typeof(TQueryEmptyPlaceHolder)) {
         if (!await sqldatareader.NextResultAsync(cancellationToken).ConfigureAwait(false)) {
            throw new InvalidOperationException("There isn't a eighth record set in the query");
         }

         while (await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false))
            tor7.Add(sqldatareader.SqlExecMapRow<T7>());
      }

      return (tor, tor1, tor2, tor3, tor4, tor5, tor6, tor7);
   }

   public async Task<T?> QueryFirstRowAsync<T, P>(P parameters, CancellationToken cancellationToken = default)
      where T : SqlExecBaseResult
      where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);
      using SqlDataReader sqldatareader = await sqlcommand
         .ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

      return await sqldatareader.ReadAsync(cancellationToken).ConfigureAwait(false)
         ? sqldatareader.SqlExecMapRow<T>() : null;
   }

   public int Execute<P>(P parameters) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      OpenConnection(sqlconnection);
      int tor = sqlcommand.ExecuteNonQuery();
      parameters.SqlExecReturnValue = sqlcommand.GetReturnValue();

      return tor;
   }

   public int ExecuteGOScript<P>(P parameters) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      List<string> scripts = [.. SQLGOScriptSplitRegex()
         .Split(parameters.SqlExecSqlText + "\r\n")
         .Where(w => !string.IsNullOrWhiteSpace(w))];

      if (scripts.Count == 0)
         return 0;

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = sqlconnection.CreateCommand();
      OpenConnection(sqlconnection);

      foreach (string script in scripts) {
         sqlcommand.CommandText = script;
         _ = sqlcommand.ExecuteNonQuery();
      }

      return scripts.Count;
   }

   public async Task<int> ExecuteAsync<P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);
      int tor = await sqlcommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
      parameters.SqlExecReturnValue = sqlcommand.GetReturnValue();

      return tor;
   }

   public async Task<int> ExecuteGOScriptAsync<P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      List<string> scripts = [.. SQLGOScriptSplitRegex()
         .Split(parameters.SqlExecSqlText + "\r\n")
         .Where(w => !string.IsNullOrWhiteSpace(w))];

      if (scripts.Count == 0)
         return 0;

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = sqlconnection.CreateCommand();
      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);

      foreach (string script in scripts) {
         if (cancellationToken.IsCancellationRequested)
            break;
         sqlcommand.CommandText = script;
         _ = await sqlcommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
      }

      return scripts.Count;
   }

   public T? ExecuteScalar<T, P>(P parameters) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      OpenConnection(sqlconnection);
      object tor = sqlcommand.ExecuteScalar();

      return MapScalar<T>.MapScalarValue(tor);
   }

   public async Task<T?> ExecuteScalarAsync<T, P>(P parameters, CancellationToken cancellationToken = default) where P : SqlExecBaseCommand {
      ArgumentNullException.ThrowIfNull(parameters);

      using SqlConnection sqlconnection = GetSqlConnection();
      using SqlCommand sqlcommand = CreatePreparedCommand(sqlconnection, parameters);

      await OpenConnectionAsync(sqlconnection, cancellationToken).ConfigureAwait(false);
      object? tor = await sqlcommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

      return MapScalar<T?>.MapScalarValue(tor);
   }

   [GeneratedRegex(@"\r{0,1}\n(?i)GO\r{0,1}\n", RegexOptions.None, "sv-SE")]
   private static partial Regex SQLGOScriptSplitRegex();
}
