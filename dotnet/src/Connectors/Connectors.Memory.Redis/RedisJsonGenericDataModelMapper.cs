﻿// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel.Data;

namespace Microsoft.SemanticKernel.Connectors.Redis;

/// <summary>
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
/// A mapper that maps between the generic semantic kernel data model and the model that the data is stored in in Redis when using JSON.
/// </summary>
internal class RedisJsonGenericDataModelMapper : IVectorStoreRecordMapper<VectorStoreGenericDataModel<string>, (string Key, JsonNode Node)>
{
    /// <summary>A <see cref="VectorStoreRecordDefinition"/> that defines the schema of the data in the database.</summary>
    private readonly VectorStoreRecordDefinition _vectorStoreRecordDefinition;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
>>>>>>> Stashed changes
/// A mapper that maps between the generic Semantic Kernel data model and the model that the data is stored under, within Redis when using JSON.
/// </summary>
internal class RedisJsonGenericDataModelMapper : IVectorStoreRecordMapper<VectorStoreGenericDataModel<string>, (string Key, JsonNode Node)>
{
    /// <summary>All the properties from the record definition.</summary>
    private readonly IReadOnlyList<VectorStoreRecordProperty> _properties;
<<<<<<< Updated upstream
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> main
>>>>>>> Stashed changes

    /// <summary>The JSON serializer options to use when converting between the data model and the Redis record.</summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    /// <summary>A dictionary that maps from a property name to the storage name that should be used when serializing it to json for data and vector properties.</summary>
    public readonly Dictionary<string, string> _storagePropertyNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisJsonGenericDataModelMapper"/> class.
    /// </summary>
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    /// <param name="vectorStoreRecordDefinition">A <see cref="VectorStoreRecordDefinition"/> that defines the schema of the data in the database.</param>
    /// <param name="jsonSerializerOptions">The JSON serializer options to use when converting between the data model and the Redis record.</param>
    public RedisJsonGenericDataModelMapper(
        VectorStoreRecordDefinition vectorStoreRecordDefinition,
        JsonSerializerOptions jsonSerializerOptions)
    {
        Verify.NotNull(vectorStoreRecordDefinition);
        Verify.NotNull(jsonSerializerOptions);

        this._vectorStoreRecordDefinition = vectorStoreRecordDefinition;
        this._jsonSerializerOptions = jsonSerializerOptions;

        // Create a dictionary that maps from the data model property name to the storage property name.
        this._storagePropertyNames = vectorStoreRecordDefinition.Properties.Select(x =>
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
>>>>>>> Stashed changes
    /// <param name="properties">All the properties from the record definition.</param>
    /// <param name="jsonSerializerOptions">The JSON serializer options to use when converting between the data model and the Redis record.</param>
    public RedisJsonGenericDataModelMapper(
        IReadOnlyList<VectorStoreRecordProperty> properties,
        JsonSerializerOptions jsonSerializerOptions)
    {
        Verify.NotNull(properties);
        Verify.NotNull(jsonSerializerOptions);

        this._properties = properties;
        this._jsonSerializerOptions = jsonSerializerOptions;

        // Create a dictionary that maps from the data model property name to the storage property name.
        this._storagePropertyNames = properties.Select(x =>
<<<<<<< Updated upstream
<<<<<<< HEAD
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> main
>>>>>>> Stashed changes
        {
            if (x.StoragePropertyName is not null)
            {
                return new KeyValuePair<string, string>(
                    x.DataModelPropertyName,
                    x.StoragePropertyName);
            }

            if (jsonSerializerOptions.PropertyNamingPolicy is not null)
            {
                return new KeyValuePair<string, string>(
                    x.DataModelPropertyName,
                    jsonSerializerOptions.PropertyNamingPolicy.ConvertName(x.DataModelPropertyName));
            }

            return new KeyValuePair<string, string>(
                x.DataModelPropertyName,
                x.DataModelPropertyName);
        }).ToDictionary(x => x.Key, x => x.Value);
    }

    /// <inheritdoc />
    public (string Key, JsonNode Node) MapFromDataToStorageModel(VectorStoreGenericDataModel<string> dataModel)
    {
        var jsonObject = new JsonObject();

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        foreach (var property in this._vectorStoreRecordDefinition.Properties)
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
        foreach (var property in this._vectorStoreRecordDefinition.Properties)
=======
        foreach (var property in this._properties)
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
        foreach (var property in this._properties)
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
        {
            var storagePropertyName = this._storagePropertyNames[property.DataModelPropertyName];
            var sourceDictionary = property is VectorStoreRecordDataProperty ? dataModel.Data : dataModel.Vectors;

            // Only map properties across that actually exist in the input.
            if (sourceDictionary is null || !sourceDictionary.TryGetValue(property.DataModelPropertyName, out var sourceValue))
            {
                continue;
            }

            // Replicate null if the property exists but is null.
            if (sourceValue is null)
            {
                jsonObject.Add(storagePropertyName, null);
                continue;
            }

            jsonObject.Add(storagePropertyName, JsonSerializer.SerializeToNode(sourceValue, property.PropertyType));
        }

        return (dataModel.Key, jsonObject);
    }

    /// <inheritdoc />
    public VectorStoreGenericDataModel<string> MapFromStorageToDataModel((string Key, JsonNode Node) storageModel, StorageToDataModelMapperOptions options)
    {
        var dataModel = new VectorStoreGenericDataModel<string>(storageModel.Key);

        // The redis result can be either a single object or an array with a single object in the case where we are doing an MGET.
        JsonObject jsonObject;
        if (storageModel.Node is JsonObject topLevelJsonObject)
        {
            jsonObject = topLevelJsonObject;
        }
        else if (storageModel.Node is JsonArray jsonArray && jsonArray.Count == 1 && jsonArray[0] is JsonObject arrayEntryJsonObject)
        {
            jsonObject = arrayEntryJsonObject;
        }
        else
        {
            throw new VectorStoreRecordMappingException($"Invalid data format for document with key '{storageModel.Key}'");
        }

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        foreach (var property in this._vectorStoreRecordDefinition.Properties)
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
        foreach (var property in this._vectorStoreRecordDefinition.Properties)
=======
        foreach (var property in this._properties)
>>>>>>> main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
        foreach (var property in this._properties)
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
        {
            var storagePropertyName = this._storagePropertyNames[property.DataModelPropertyName];
            var targetDictionary = property is VectorStoreRecordDataProperty ? dataModel.Data : dataModel.Vectors;

            // Only map properties across that actually exist in the input.
            if (!jsonObject.TryGetPropertyValue(storagePropertyName, out var sourceValue))
            {
                continue;
            }

            // Replicate null if the property exists but is null.
            if (sourceValue is null)
            {
                targetDictionary.Add(property.DataModelPropertyName, null);
                continue;
            }

            // Map data and vector values.
            if (property is VectorStoreRecordDataProperty || property is VectorStoreRecordVectorProperty)
            {
                targetDictionary.Add(property.DataModelPropertyName, JsonSerializer.Deserialize(sourceValue, property.PropertyType));
            }
        }

        return dataModel;
    }
}
