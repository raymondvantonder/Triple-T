using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Models.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;
using TripleT.User.Infrastructure.Persistence.Models;

namespace TripleT.User.Infrastructure.Persistence
{
    public abstract class RepositoryBase<TEntity, TDocument> : IRepository<TEntity>
        where TEntity : EntityBase
        where TDocument : DocumentBase<TDocument>
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ILogger _logger;
        protected readonly DynamoDBOperationConfig _operationConfig;

        protected RepositoryBase(IDynamoDBContext dbContext, ILogger logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _operationConfig = new DynamoDBOperationConfig()
            {
                OverrideTableName = configuration["DYNAMODB_TABLE_NAME"]
            };
        }

        protected abstract string CreatePrimaryKey(TDocument document);

        protected abstract string CreateSortKey(TDocument document);

        protected abstract TDocument MapToDocument(TEntity entity);

        protected abstract TEntity MapFromDocument(TDocument document);

        public virtual async Task<TEntity> AddItemAsync(TEntity item, CancellationToken cancellationToken)
        {
            var document = MapToDocument(item);

            document.PK = CreatePrimaryKey(document);
            document.SK = CreatePrimaryKey(document);
            document.CreatedTime = DateTime.Now;

            await LogQueryTime(async () =>
                    await _dbContext.SaveAsync(document, _operationConfig, cancellationToken), nameof(AddItemAsync)
            );

            return MapFromDocument(document);
        }

        public virtual async Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken)
        {
            var document = MapToDocument(item);

            document.PK = CreatePrimaryKey(document);
            document.SK = CreatePrimaryKey(document);
            document.UpdatedTime = DateTime.Now;

            await LogQueryTime(async () =>
                    await _dbContext.SaveAsync(document, _operationConfig, cancellationToken), nameof(UpdateItemAsync)
            );
        }

        public virtual async Task DeleteItemAsync(string id, CancellationToken cancellationToken)
        {
            await LogQueryTime(async () =>
                    await _dbContext.DeleteAsync<TDocument>(id, _operationConfig, cancellationToken), nameof(DeleteItemAsync)
            );
        }

        public abstract Task<TEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken);

        #region Helper Functions

        protected async Task LogQueryTime(Func<Task> action, string queryName)
        {
            await LogQueryTime(async () =>
            {
                await action();
                return true;
            }, queryName);
        }

        protected async Task<TResult> LogQueryTime<TResult>(Func<Task<TResult>> action, string queryName)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                return await action();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"Query: [{queryName}] took [{stopwatch.ElapsedMilliseconds}]ms");
            }
        }

        private ScanOperator MapToOperator(QueryModel.QueryType queryOperator)
        {
            return queryOperator switch
            {
                QueryModel.QueryType.Equal => ScanOperator.Equal
                , QueryModel.QueryType.NotEqual => ScanOperator.NotEqual
                , QueryModel.QueryType.LessThanOrEqual => ScanOperator.LessThanOrEqual
                , QueryModel.QueryType.LessThan => ScanOperator.LessThan
                , QueryModel.QueryType.GreaterThanOrEqual => ScanOperator.GreaterThanOrEqual
                , QueryModel.QueryType.GreaterThan => ScanOperator.GreaterThan
                , QueryModel.QueryType.IsNotNull => ScanOperator.IsNotNull
                , QueryModel.QueryType.IsNull => ScanOperator.IsNull
                , QueryModel.QueryType.Contains => ScanOperator.Contains
                , QueryModel.QueryType.NotContains => ScanOperator.NotContains
                , QueryModel.QueryType.BeginsWith => ScanOperator.BeginsWith
                , QueryModel.QueryType.In => ScanOperator.In
                , QueryModel.QueryType.Between => ScanOperator.Between
                , _ => throw new ArgumentOutOfRangeException(nameof(queryOperator), queryOperator, null)
            };
        }

        #endregion
    }
}