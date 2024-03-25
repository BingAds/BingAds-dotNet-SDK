#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.BingAds.V13.Bulk
{
    public class AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new BatchErrorConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ApplicationFault? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AdApiFaultDetail" => jsonObj.Deserialize<AdApiFaultDetail>(options),
                "ApiFaultDetail" => jsonObj.Deserialize<ApiFaultDetail>(options),
                "ApplicationFault" => jsonObj.Deserialize<ApplicationFault>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ApplicationFault value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AdApiFaultDetail adApiFaultDetail:
                    JsonSerializer.Serialize(writer, adApiFaultDetail, options);
                    break;
                case ApiFaultDetail apiFaultDetail:
                    JsonSerializer.Serialize(writer, apiFaultDetail, options);
                    break;
                case ApplicationFault applicationFault:
                    JsonSerializer.Serialize(writer, applicationFault, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class BatchErrorConverter : JsonConverter<BatchError>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public BatchErrorConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override BatchError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EditorialError" => jsonObj.Deserialize<EditorialError>(options),
                "BatchError" => jsonObj.Deserialize<BatchError>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, BatchError value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case EditorialError editorialError:
                    JsonSerializer.Serialize(writer, editorialError, options);
                    break;
                case BatchError batchError:
                    JsonSerializer.Serialize(writer, batchError, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}