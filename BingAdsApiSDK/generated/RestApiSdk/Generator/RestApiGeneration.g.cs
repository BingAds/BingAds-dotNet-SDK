namespace Microsoft.BingAds;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public static class RestApiGeneration
{
    public static void Apply(JsonSerializerOptions jsonSerializationOptions, Func<string, Exception> createUnsupportedTypeValueException)
    {
        var modifiers = ((DefaultJsonTypeInfoResolver)jsonSerializationOptions.TypeInfoResolver).Modifiers;

        modifiers.Add(Microsoft.BingAds.V13.AdInsight.EntityModifiers.CustomizeEntities);
        modifiers.Add(Microsoft.BingAds.V13.Bulk.EntityModifiers.CustomizeEntities);
        modifiers.Add(Microsoft.BingAds.V13.CampaignManagement.EntityModifiers.CustomizeEntities);
        modifiers.Add(Microsoft.BingAds.V13.CustomerBilling.EntityModifiers.CustomizeEntities);
        modifiers.Add(Microsoft.BingAds.V13.CustomerManagement.EntityModifiers.CustomizeEntities);
        modifiers.Add(Microsoft.BingAds.V13.Reporting.EntityModifiers.CustomizeEntities);

        Microsoft.BingAds.V13.AdInsight.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
        Microsoft.BingAds.V13.Bulk.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
        Microsoft.BingAds.V13.CampaignManagement.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
        Microsoft.BingAds.V13.CustomerBilling.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
        Microsoft.BingAds.V13.CustomerManagement.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
        Microsoft.BingAds.V13.Reporting.AllPolymorphicConverters.AddTo(jsonSerializationOptions, createUnsupportedTypeValueException);
    }
}