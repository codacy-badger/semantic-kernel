<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿// Copyright (c) Microsoft. All rights reserved.
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
﻿// Copyright (c) Microsoft. All rights reserved.
=======
// Copyright (c) Microsoft. All rights reserved.
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
// Copyright (c) Microsoft. All rights reserved.
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL;
using Microsoft.SemanticKernel.Data;
using Xunit;
using DistanceFunction = Microsoft.SemanticKernel.Data.DistanceFunction;
using IndexKind = Microsoft.SemanticKernel.Data.IndexKind;

namespace SemanticKernel.IntegrationTests.Connectors.Memory.AzureCosmosDBNoSQL;

/// <summary>
/// Integration tests for <see cref="AzureCosmosDBNoSQLVectorStoreRecordCollection{TRecord}"/> class.
/// </summary>
[Collection("AzureCosmosDBNoSQLVectorStoreCollection")]
public sealed class AzureCosmosDBNoSQLVectorStoreRecordCollectionTests(AzureCosmosDBNoSQLVectorStoreFixture fixture)
{
    private const string? SkipReason = "Azure CosmosDB NoSQL cluster is required";

    [Fact(Skip = SkipReason)]
    public async Task ItCanCreateCollectionAsync()
    {
        // Arrange
        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "test-create-collection");

        // Act
        await sut.CreateCollectionAsync();

        // Assert
        Assert.True(await sut.CollectionExistsAsync());
    }

    [Theory(Skip = SkipReason)]
    [InlineData("sk-test-hotels", true)]
    [InlineData("nonexistentcollection", false)]
    public async Task CollectionExistsReturnsCollectionStateAsync(string collectionName, bool expectedExists)
    {
        // Arrange
        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, collectionName);

        if (expectedExists)
        {
            await fixture.Database!.CreateContainerIfNotExistsAsync(new ContainerProperties(collectionName, "/id"));
        }

        // Act
        var actual = await sut.CollectionExistsAsync();

        // Assert
        Assert.Equal(expectedExists, actual);
    }

    [Theory(Skip = SkipReason)]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public async Task ItCanCreateCollectionUpsertAndGetAsync(bool includeVectors, bool useRecordDefinition)
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";

        var collectionNamePostfix = useRecordDefinition ? "with-definition" : "with-type";
        collectionNamePostfix = includeVectors ? $"{collectionNamePostfix}-with-vectors" : $"{collectionNamePostfix}-without-vectors";
        var collectionName = $"collection-{collectionNamePostfix}";

        var options = new AzureCosmosDBNoSQLVectorStoreRecordCollectionOptions<AzureCosmosDBNoSQLHotel>
        {
            VectorStoreRecordDefinition = useRecordDefinition ? this.GetTestHotelRecordDefinition() : null
        };

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, collectionName);

        var record = this.CreateTestHotel(HotelId);

        // Act
        await sut.CreateCollectionAsync();
        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId, new() { IncludeVectors = includeVectors });

        // Assert
        Assert.True(await sut.CollectionExistsAsync());
        await sut.DeleteCollectionAsync();

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        Assert.Equal(record.HotelId, getResult.HotelId);
        Assert.Equal(record.HotelName, getResult.HotelName);
        Assert.Equal(record.HotelCode, getResult.HotelCode);
        Assert.Equal(record.HotelRating, getResult.HotelRating);
        Assert.Equal(record.ParkingIncluded, getResult.ParkingIncluded);
        Assert.Equal(record.Tags.ToArray(), getResult.Tags.ToArray());
        Assert.Equal(record.Description, getResult.Description);
<<<<<<< HEAD
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
<<<<<<< HEAD
=======
        Assert.Equal(record.Timestamp, getResult.Timestamp);
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
        Assert.Equal(record.Timestamp, getResult.Timestamp);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
        Assert.Equal(record.Timestamp, getResult.Timestamp);
>>>>>>> main
>>>>>>> Stashed changes

        if (includeVectors)
        {
            Assert.NotNull(getResult.DescriptionEmbedding);
            Assert.Equal(record.DescriptionEmbedding!.Value.ToArray(), getResult.DescriptionEmbedding.Value.ToArray());
        }
        else
        {
            Assert.Null(getResult.DescriptionEmbedding);
        }
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanDeleteCollectionAsync()
    {
        // Arrange
        const string TempCollectionName = "test-delete-collection";
        await fixture.Database!.CreateContainerAsync(new ContainerProperties(TempCollectionName, "/id"));

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, TempCollectionName);

        Assert.True(await sut.CollectionExistsAsync());

        // Act
        await sut.DeleteCollectionAsync();

        // Assert
        Assert.False(await sut.CollectionExistsAsync());
    }

    [Theory(Skip = SkipReason)]
    [InlineData("consistent-mode-collection", IndexingMode.Consistent)]
    [InlineData("lazy-mode-collection", IndexingMode.Lazy)]
    [InlineData("none-mode-collection", IndexingMode.None)]
    public async Task ItCanGetAndDeleteRecordAsync(string collectionName, IndexingMode indexingMode)
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(
            fixture.Database!,
            collectionName,
            new() { IndexingMode = indexingMode, Automatic = indexingMode != IndexingMode.None });

        await sut.CreateCollectionAsync();

        var record = this.CreateTestHotel(HotelId);

        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId);

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        // Act
        await sut.DeleteAsync(HotelId);

        getResult = await sut.GetAsync(HotelId);

        // Assert
        Assert.Null(getResult);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanGetAndDeleteRecordWithPartitionKeyAsync()
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        const string HotelName = "Test Hotel Name";

        IVectorStoreRecordCollection<AzureCosmosDBNoSQLCompositeKey, AzureCosmosDBNoSQLHotel> sut =
            new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(
                fixture.Database!,
                "delete-with-partition-key",
                new() { PartitionKeyPropertyName = "HotelName" });

        await sut.CreateCollectionAsync();

        var record = this.CreateTestHotel(HotelId, HotelName);

        var upsertResult = await sut.UpsertAsync(record);

        var key = new AzureCosmosDBNoSQLCompositeKey(record.HotelId, record.HotelName!);
        var getResult = await sut.GetAsync(key);

        Assert.Equal(HotelId, upsertResult.RecordKey);
        Assert.Equal(HotelName, upsertResult.PartitionKey);
        Assert.NotNull(getResult);

        // Act
        await sut.DeleteAsync(key);

        getResult = await sut.GetAsync(key);

        // Assert
        Assert.Null(getResult);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanGetAndDeleteBatchAsync()
    {
        // Arrange
        const string HotelId1 = "11111111-1111-1111-1111-111111111111";
        const string HotelId2 = "22222222-2222-2222-2222-222222222222";
        const string HotelId3 = "33333333-3333-3333-3333-333333333333";

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "get-and-delete-batch");

        await sut.CreateCollectionAsync();

        var record1 = this.CreateTestHotel(HotelId1);
        var record2 = this.CreateTestHotel(HotelId2);
        var record3 = this.CreateTestHotel(HotelId3);

        var upsertResults = await sut.UpsertBatchAsync([record1, record2, record3]).ToListAsync();
        var getResults = await sut.GetBatchAsync([HotelId1, HotelId2, HotelId3]).ToListAsync();

        Assert.Equal([HotelId1, HotelId2, HotelId3], upsertResults);

        Assert.NotNull(getResults.First(l => l.HotelId == HotelId1));
        Assert.NotNull(getResults.First(l => l.HotelId == HotelId2));
        Assert.NotNull(getResults.First(l => l.HotelId == HotelId3));

        // Act
        await sut.DeleteBatchAsync([HotelId1, HotelId2, HotelId3]);

        getResults = await sut.GetBatchAsync([HotelId1, HotelId2, HotelId3]).ToListAsync();

        // Assert
        Assert.Empty(getResults);
    }

    [Fact(Skip = SkipReason)]
    public async Task ItCanUpsertRecordAsync()
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "upsert-record");

        await sut.CreateCollectionAsync();

        var record = this.CreateTestHotel(HotelId);

        var upsertResult = await sut.UpsertAsync(record);
        var getResult = await sut.GetAsync(HotelId);

        Assert.Equal(HotelId, upsertResult);
        Assert.NotNull(getResult);

        // Act
        record.HotelName = "Updated name";
        record.HotelRating = 10;

        upsertResult = await sut.UpsertAsync(record);
        getResult = await sut.GetAsync(HotelId);

        // Assert
        Assert.NotNull(getResult);
        Assert.Equal("Updated name", getResult.HotelName);
        Assert.Equal(10, getResult.HotelRating);
    }

<<<<<<< HEAD
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
<<<<<<< HEAD
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
>>>>>>> Stashed changes
    [Fact(Skip = SkipReason)]
    public async Task VectorizedSearchReturnsValidResultsByDefaultAsync()
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "vector-search-default");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f])).ToListAsync();

        // Assert
        var ids = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal("key1", ids[0]);
        Assert.Equal("key2", ids[1]);
        Assert.Equal("key3", ids[2]);

        Assert.DoesNotContain("key4", ids);

        Assert.Equal(1, searchResults.First(l => l.Record.HotelId == "key1").Score);
    }

    [Fact(Skip = SkipReason)]
    public async Task VectorizedSearchReturnsValidResultsWithOffsetAsync()
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "vector-search-with-offset");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]), new()
        {
            Top = 2,
            Skip = 2
        }).ToListAsync();

        // Assert
        var ids = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal("key3", ids[0]);
        Assert.Equal("key4", ids[1]);

        Assert.DoesNotContain("key1", ids);
        Assert.DoesNotContain("key2", ids);
    }

    [Theory(Skip = SkipReason)]
    [MemberData(nameof(VectorizedSearchWithFilterData))]
    public async Task VectorizedSearchReturnsValidResultsWithFilterAsync(VectorSearchFilter filter, List<string> expectedIds)
    {
        // Arrange
        var hotel1 = this.CreateTestHotel(hotelId: "key1", embedding: new[] { 30f, 31f, 32f, 33f });
        var hotel2 = this.CreateTestHotel(hotelId: "key2", embedding: new[] { 31f, 32f, 33f, 34f });
        var hotel3 = this.CreateTestHotel(hotelId: "key3", embedding: new[] { 20f, 20f, 20f, 20f });
        var hotel4 = this.CreateTestHotel(hotelId: "key4", embedding: new[] { -1000f, -1000f, -1000f, -1000f });

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<AzureCosmosDBNoSQLHotel>(fixture.Database!, "vector-search-with-filter");

        await sut.CreateCollectionIfNotExistsAsync();

        await sut.UpsertBatchAsync([hotel4, hotel2, hotel3, hotel1]).ToListAsync();

        // Act
        var searchResults = await sut.VectorizedSearchAsync(new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]), new()
        {
            Filter = filter,
            Top = 4,
        }).ToListAsync();

        // Assert
        var actualIds = searchResults.Select(l => l.Record.HotelId).ToList();

        Assert.Equal(expectedIds, actualIds);
    }

    public static TheoryData<VectorSearchFilter, List<string>> VectorizedSearchWithFilterData => new()
    {
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "My Hotel key2"),
            ["key2"]
        },
        {
            new VectorSearchFilter()
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "t2"),
            ["key1", "key2", "key3", "key4"]
        },
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "My Hotel key2")
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "t2"),
            ["key2"]
        },
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "non-existent-hotel")
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "non-existent-tag"),
            []
        },
    };

    #region private

    private AzureCosmosDBNoSQLHotel CreateTestHotel(
        string hotelId,
        string? hotelName = null,
        ReadOnlyMemory<float>? embedding = null)
    [Fact(Skip = SkipReason)]
    public async Task ItCanUpsertAndRetrieveUsingTheGenericMapperAsync()
    {
        // Arrange
        const string HotelId = "55555555-5555-5555-5555-555555555555";
        var options = new AzureCosmosDBNoSQLVectorStoreRecordCollectionOptions<VectorStoreGenericDataModel<string>>
        {
            VectorStoreRecordDefinition = this.GetTestHotelRecordDefinition()
        };

        var sut = new AzureCosmosDBNoSQLVectorStoreRecordCollection<VectorStoreGenericDataModel<string>>(fixture.Database!, "generic-mapper", options);

        await sut.CreateCollectionAsync();

        // Act
        var upsertResult = await sut.UpsertAsync(new VectorStoreGenericDataModel<string>(HotelId)
        {
            Data =
            {
                { "HotelName", "Generic Mapper Hotel" },
                { "Description", "This is a generic mapper hotel" },
                { "Tags", new List<string> { "generic" } },
                { "parking_is_included", false },
                { "Timestamp", new DateTimeOffset(1970, 1, 18, 0, 0, 0, TimeSpan.Zero) },
                { "HotelRating", 3.6f }
            },
            Vectors =
            {
                { "DescriptionEmbedding", new ReadOnlyMemory<float>([30f, 31f, 32f, 33f]) }
            }
        });

        var localGetResult = await sut.GetAsync(HotelId, new GetRecordOptions { IncludeVectors = true });

        // Assert
        Assert.NotNull(upsertResult);
        Assert.Equal(HotelId, upsertResult);

        Assert.NotNull(localGetResult);
        Assert.Equal("Generic Mapper Hotel", localGetResult.Data["HotelName"]);
        Assert.Equal("This is a generic mapper hotel", localGetResult.Data["Description"]);
        Assert.Equal(new List<string> { "generic" }, localGetResult.Data["Tags"]);
        Assert.False((bool?)localGetResult.Data["parking_is_included"]);
        Assert.Equal(new DateTimeOffset(1970, 1, 18, 0, 0, 0, TimeSpan.Zero), localGetResult.Data["Timestamp"]);
        Assert.Equal(3.6f, localGetResult.Data["HotelRating"]);
        Assert.Equal(new[] { 30f, 31f, 32f, 33f }, ((ReadOnlyMemory<float>)localGetResult.Vectors["DescriptionEmbedding"]!).ToArray());
    }

    public static TheoryData<VectorSearchFilter, List<string>> VectorizedSearchWithFilterData => new()
    {
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "My Hotel key2"),
            ["key2"]
        },
        {
            new VectorSearchFilter()
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "t2"),
            ["key1", "key2", "key3", "key4"]
        },
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "My Hotel key2")
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "t2"),
            ["key2"]
        },
        {
            new VectorSearchFilter()
                .EqualTo(nameof(AzureCosmosDBNoSQLHotel.HotelName), "non-existent-hotel")
                .AnyTagEqualTo(nameof(AzureCosmosDBNoSQLHotel.Tags), "non-existent-tag"),
            []
        },
    };

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
    #region private

    private AzureCosmosDBNoSQLHotel CreateTestHotel(string hotelId, string? hotelName = null)
    {
        return new AzureCosmosDBNoSQLHotel
        {
            HotelId = hotelId,
            HotelName = hotelName ?? $"My Hotel {hotelId}",
            HotelCode = 42,
            HotelRating = 4.5f,
            ParkingIncluded = true,
            Tags = { "t1", "t2" },
            Description = "This is a great hotel.",
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
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
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
=======
            DescriptionEmbedding = embedding ?? new[] { 30f, 31f, 32f, 33f },
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
            Timestamp = new DateTimeOffset(2024, 9, 24, 17, 28, 32, TimeSpan.Zero),
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
            DescriptionEmbedding = embedding ?? new[] { 30f, 31f, 32f, 33f },
            DescriptionEmbedding = new[] { 30f, 31f, 32f, 33f },
            Timestamp = new DateTimeOffset(2024, 9, 24, 17, 28, 32, TimeSpan.Zero),
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
        };
    }

    private VectorStoreRecordDefinition GetTestHotelRecordDefinition()
    {
        return new()
        {
            Properties =
            [
                new VectorStoreRecordKeyProperty("HotelId", typeof(string)),
                new VectorStoreRecordDataProperty("HotelName", typeof(string)),
                new VectorStoreRecordDataProperty("HotelCode", typeof(int)),
                new VectorStoreRecordDataProperty("parking_is_included", typeof(bool)),
                new VectorStoreRecordDataProperty("HotelRating", typeof(float)),
                new VectorStoreRecordDataProperty("Tags", typeof(List<string>)),
                new VectorStoreRecordDataProperty("Description", typeof(string)),
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                new VectorStoreRecordVectorProperty("DescriptionEmbedding", typeof(ReadOnlyMemory<float>?)) { Dimensions = 4, IndexKind = IndexKind.Flat, DistanceFunction = DistanceFunction.CosineDistance }
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
                new VectorStoreRecordVectorProperty("DescriptionEmbedding", typeof(ReadOnlyMemory<float>?)) { Dimensions = 4, IndexKind = IndexKind.Flat, DistanceFunction = DistanceFunction.CosineDistance }
=======
                new VectorStoreRecordDataProperty("Timestamp", typeof(DateTimeOffset)),
                new VectorStoreRecordVectorProperty("DescriptionEmbedding", typeof(ReadOnlyMemory<float>?)) { Dimensions = 4, IndexKind = IndexKind.Flat, DistanceFunction = DistanceFunction.CosineSimilarity }
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
                new VectorStoreRecordDataProperty("Timestamp", typeof(DateTimeOffset)),
                new VectorStoreRecordVectorProperty("DescriptionEmbedding", typeof(ReadOnlyMemory<float>?)) { Dimensions = 4, IndexKind = IndexKind.Flat, DistanceFunction = DistanceFunction.CosineSimilarity }
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
            ]
        };
    }

    #endregion
}
