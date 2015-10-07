using System;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface ICsvReader : IDisposable
    {
        RowValues ReadNextRow();
    }
}