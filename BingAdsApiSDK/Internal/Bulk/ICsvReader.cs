using System;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal interface ICsvReader : IDisposable
    {
        RowValues ReadNextRow();
    }
}