// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Net.Http;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Functions.OpenAPI.Authentication;

namespace Microsoft.SemanticKernel.Functions.OpenAPI.Extensions;

/// <summary>
/// OpenAPI function execution parameters.
/// </summary>
public class OpenApiFunctionExecutionParameters
{
    /// <summary>
    /// HttpClient to use for sending HTTP requests.
    /// </summary>
    public HttpClient? HttpClient { get; set; }

    /// <summary>
    /// Callback for adding authentication data to HTTP requests.
    /// </summary>
    [Obsolete("This property will be removed in one of the next releases. Please use the AuthenticateCallbackProvider property instead.")]
    public AuthenticateRequestAsyncCallback? AuthCallback { get; set; }

    /// <summary>
    /// Provider to get authentication callback.
    /// </summary>
    public AuthenticateCallbackProvider? AuthenticateCallbackProvider { get; set; }

    /// <summary>
    /// Override for REST API operation server url.
    /// </summary>
    public Uri? ServerUrlOverride { get; set; }

    /// <summary>
    /// Flag indicating whether to ignore non-compliant errors or not.
    /// If set to true, the operation execution will not throw exceptions for non-compliant documents.
    /// Please note that enabling this option may result in incomplete or inaccurate execution results.
    /// </summary>
    public bool IgnoreNonCompliantErrors { get; set; }

    /// <summary>
    /// Optional user agent header value.
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// Determines whether the operation payload is constructed dynamically based on operation payload metadata.
    /// If false, the operation payload must be provided via the 'payload' context variable.
    /// </summary>
    public bool EnableDynamicPayload { get; set; }

    /// <summary>
    /// Determines whether payload parameter names are augmented with namespaces.
    /// Namespaces prevent naming conflicts by adding the parent parameter name as a prefix, separated by dots.
    /// For instance, without namespaces, the 'email' parameter for both the 'sender' and 'receiver' parent parameters
    /// would be resolved from the same 'email' argument, which is incorrect. However, by employing namespaces,
    /// the parameters 'sender.email' and 'sender.receiver' will be correctly resolved from arguments with the same names.
    /// </summary>
    public bool EnablePayloadNamespacing { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenApiFunctionExecutionParameters"/> class.
    /// </summary>
    /// <param name="httpClient">The HttpClient to use for sending HTTP requests.</param>
    /// <param name="serverUrlOverride">The override for the REST API operation server URL.</param>
    /// <param name="authCallbackProvider">The provider to return callback for adding authentication data to HTTP requests.</param>
    /// <param name="userAgent">Optional user agent header value.</param>
    /// <param name="ignoreNonCompliantErrors">A flag indicating whether to ignore non-compliant errors or not
    /// If set to true, the operation execution will not throw exceptions for non-compliant documents.
    /// Please note that enabling this option may result in incomplete or inaccurate execution results.</param>
    /// <param name="enableDynamicOperationPayload">Determines whether the operation payload is constructed dynamically based on operation payload metadata.
    /// If false, the operation payload must be provided via the 'payload' context variable.</param>
    /// <param name="enablePayloadNamespacing">Determines whether payload parameter names are augmented with namespaces.
    /// Namespaces prevent naming conflicts by adding the parent parameter name as a prefix, separated by dots.</param>
    public OpenApiFunctionExecutionParameters(
        HttpClient? httpClient = null,
        Uri? serverUrlOverride = null,
        AuthenticateCallbackProvider? authCallbackProvider = null,
        string userAgent = Telemetry.HttpUserAgent,
        bool ignoreNonCompliantErrors = false,
        bool enableDynamicOperationPayload = false,
        bool enablePayloadNamespacing = false)
    {
        this.HttpClient = httpClient;
        this.AuthenticateCallbackProvider = authCallbackProvider;
        this.ServerUrlOverride = serverUrlOverride;
        this.UserAgent = userAgent;
        this.IgnoreNonCompliantErrors = ignoreNonCompliantErrors;
        this.EnableDynamicPayload = enableDynamicOperationPayload;
        this.EnablePayloadNamespacing = enablePayloadNamespacing;
    }
}
