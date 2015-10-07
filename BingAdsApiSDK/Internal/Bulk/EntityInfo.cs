using System;
using Microsoft.BingAds.Internal.Bulk.Entities;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal class EntityInfo
    {
        public Func<SingleRecordBulkEntity> CreateFunc { get; private set; }

        public string DeleteAllColumnName { get; private set; }

        public Func<BulkEntityIdentifier> CreateIdentifierFunc { get; private set; }

        public EntityInfo(Func<SingleRecordBulkEntity> createFunc, string deleteAllColumnName = null, Func<BulkEntityIdentifier> createIdentifierFunc = null)
        {
            CreateFunc = createFunc;
            DeleteAllColumnName = deleteAllColumnName;
            CreateIdentifierFunc = createIdentifierFunc;
        }
    }
}