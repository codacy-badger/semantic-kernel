<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
﻿// Copyright (c) Microsoft. All rights reserved.

using System;
=======
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
﻿// Copyright (c) Microsoft. All rights reserved.

using System;
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
// Copyright (c) Microsoft. All rights reserved.

using System;
using System.ClientModel;
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
using System.ClientModel.Primitives;
using System.Net.Http;
using System.Threading;
using Azure.AI.OpenAI;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
#pragma warning disable IDE0005 // Using directive is unnecessary
using Microsoft.SemanticKernel.Connectors.FunctionCalling;
#pragma warning restore IDE0005 // Using directive is unnecessary
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Http;
using OpenAI;

namespace Microsoft.SemanticKernel.Connectors.AzureOpenAI;

/// <summary>
/// Base class for AI clients that provides common functionality for interacting with Azure OpenAI services.
/// </summary>
internal partial class AzureClientCore : ClientCore
{
    /// <summary>
    /// Gets the key used to store the deployment name in the <see cref="ClientCore.Attributes"/> dictionary.
    /// </summary>
    internal static string DeploymentNameKey => "DeploymentName";

    /// <summary>
    /// Deployment name.
    /// </summary>
    internal string DeploymentName { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureClientCore"/> class.
    /// </summary>
    /// <param name="deploymentName">Azure OpenAI deployment name, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="endpoint">Azure OpenAI deployment URL, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="apiKey">Azure OpenAI API key, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="httpClient">Custom <see cref="HttpClient"/> for HTTP requests.</param>
    /// <param name="logger">The <see cref="ILoggerFactory"/> to use for logging. If null, no logging will be performed.</param>
    internal AzureClientCore(
        string deploymentName,
        string endpoint,
        string apiKey,
        HttpClient? httpClient = null,
        ILogger? logger = null)
    {
        Verify.NotNullOrWhiteSpace(deploymentName);
        Verify.NotNullOrWhiteSpace(endpoint);
        Verify.StartsWith(endpoint, "https://", "The Azure OpenAI endpoint must start with 'https://'");
        Verify.NotNullOrWhiteSpace(apiKey);

        var options = GetAzureOpenAIClientOptions(httpClient);

        this.Logger = logger ?? NullLogger.Instance;
        this.DeploymentName = deploymentName;
        this.Endpoint = new Uri(endpoint);
        this.Client = new AzureOpenAIClient(this.Endpoint, apiKey, options);
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
        this.Client = new AzureOpenAIClient(this.Endpoint, new ApiKeyCredential(apiKey), options);
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
        this.Client = new AzureOpenAIClient(this.Endpoint, new ApiKeyCredential(apiKey), options);
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
        this.Client = new AzureOpenAIClient(this.Endpoint, new ApiKeyCredential(apiKey), options);
>>>>>>> main
>>>>>>> Stashed changes
        this.FunctionCallsProcessor = new FunctionCallsProcessor(this.Logger);

        this.AddAttribute(DeploymentNameKey, deploymentName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureClientCore"/> class.
    /// </summary>
    /// <param name="deploymentName">Azure OpenAI deployment name, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="endpoint">Azure OpenAI deployment URL, see https://learn.microsoft.com/azure/cognitive-services/openai/quickstart</param>
    /// <param name="credential">Token credential, e.g. DefaultAzureCredential, ManagedIdentityCredential, EnvironmentCredential, etc.</param>
    /// <param name="httpClient">Custom <see cref="HttpClient"/> for HTTP requests.</param>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging. If null, no logging will be performed.</param>
    internal AzureClientCore(
        string deploymentName,
        string endpoint,
        TokenCredential credential,
        HttpClient? httpClient = null,
        ILogger? logger = null)
    {
        Verify.NotNullOrWhiteSpace(deploymentName);
        Verify.NotNullOrWhiteSpace(endpoint);
        Verify.StartsWith(endpoint, "https://", "The Azure OpenAI endpoint must start with 'https://'");

        var options = GetAzureOpenAIClientOptions(httpClient);

        this.Logger = logger ?? NullLogger.Instance;
        this.DeploymentName = deploymentName;
        this.Endpoint = new Uri(endpoint);
        this.Client = new AzureOpenAIClient(this.Endpoint, credential, options);
        this.FunctionCallsProcessor = new FunctionCallsProcessor(this.Logger);

        this.AddAttribute(DeploymentNameKey, deploymentName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureClientCore"/> class..
    /// Note: instances created this way might not have the default diagnostics settings,
    /// it's up to the caller to configure the client.
    /// </summary>
    /// <param name="deploymentName">Azure OpenAI deployment name, see https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource</param>
    /// <param name="openAIClient">Custom <see cref="AzureOpenAIClient"/>.</param>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging. If null, no logging will be performed.</param>
    internal AzureClientCore(
        string deploymentName,
        AzureOpenAIClient openAIClient,
        ILogger? logger = null)
    {
        Verify.NotNullOrWhiteSpace(deploymentName);
        Verify.NotNull(openAIClient);

        this.Logger = logger ?? NullLogger.Instance;
        this.DeploymentName = deploymentName;
        this.Client = openAIClient;
        this.FunctionCallsProcessor = new FunctionCallsProcessor(this.Logger);

        this.AddAttribute(DeploymentNameKey, deploymentName);
    }

    /// <summary>Gets options to use for an OpenAIClient</summary>
    /// <param name="httpClient">Custom <see cref="HttpClient"/> for HTTP requests.</param>
    /// <param name="serviceVersion">Optional API version.</param>
    /// <returns>An instance of <see cref="OpenAIClientOptions"/>.</returns>
    internal static AzureOpenAIClientOptions GetAzureOpenAIClientOptions(HttpClient? httpClient, AzureOpenAIClientOptions.ServiceVersion? serviceVersion = null)
    {
        AzureOpenAIClientOptions options = serviceVersion is not null
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            ? new(serviceVersion.Value) { ApplicationId = HttpHeaderConstant.Values.UserAgent }
            : new() { ApplicationId = HttpHeaderConstant.Values.UserAgent };
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
            ? new(serviceVersion.Value) { ApplicationId = HttpHeaderConstant.Values.UserAgent }
            : new() { ApplicationId = HttpHeaderConstant.Values.UserAgent };
=======
            ? new(serviceVersion.Value) { UserAgentApplicationId = HttpHeaderConstant.Values.UserAgent }
            : new() { UserAgentApplicationId = HttpHeaderConstant.Values.UserAgent };
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
            ? new(serviceVersion.Value) { UserAgentApplicationId = HttpHeaderConstant.Values.UserAgent }
            : new() { UserAgentApplicationId = HttpHeaderConstant.Values.UserAgent };
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes

        options.AddPolicy(CreateRequestHeaderPolicy(HttpHeaderConstant.Names.SemanticKernelVersion, HttpHeaderConstant.Values.GetAssemblyVersion(typeof(AzureClientCore))), PipelinePosition.PerCall);

        if (httpClient is not null)
        {
            options.Transport = new HttpClientPipelineTransport(httpClient);
            options.RetryPolicy = new ClientRetryPolicy(maxRetries: 0); // Disable Azure SDK retry policy if and only if a custom HttpClient is provided.
            options.NetworkTimeout = Timeout.InfiniteTimeSpan; // Disable Azure SDK default timeout
        }

        return options;
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

    /// <inheritdoc/>
    protected override string GetClientModelId()
        => this.DeploymentName;
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
}
