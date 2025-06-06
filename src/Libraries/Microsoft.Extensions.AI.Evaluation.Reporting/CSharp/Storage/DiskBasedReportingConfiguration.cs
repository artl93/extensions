﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Extensions.AI.Evaluation.Reporting.Storage;

/// <summary>
/// Contains factory method for creating a <see cref="ReportingConfiguration"/> that persists
/// <see cref="ScenarioRunResult"/>s to disk and also uses the disk to cache AI responses. 
/// </summary>
public static class DiskBasedReportingConfiguration
{
    /// <summary>
    /// Creates a <see cref="ReportingConfiguration"/> that persists <see cref="ScenarioRunResult"/>s to disk and also
    /// uses the disk to cache AI responses.
    /// </summary>
    /// <param name="storageRootPath">
    /// The path to a directory on disk under which the <see cref="ScenarioRunResult"/>s and all cached AI responses
    /// should be stored.
    /// </param>
    /// <param name="evaluators">
    /// The set of <see cref="IEvaluator"/>s that should be invoked to evaluate AI responses.
    /// </param>
    /// <param name="chatConfiguration">
    /// A <see cref="ChatConfiguration"/> that specifies the <see cref="IChatClient"/> that is used by AI-based
    /// <paramref name="evaluators"/> included in the returned <see cref="ReportingConfiguration"/>. Can be omitted if
    /// none of the included <paramref name="evaluators"/> are AI-based.
    /// </param>
    /// <param name="enableResponseCaching">
    /// <see langword="true"/> to enable caching of AI responses; <see langword="false"/> otherwise.
    /// </param>
    /// <param name="timeToLiveForCacheEntries">
    /// An optional <see cref="TimeSpan"/> that specifies the maximum amount of time that cached AI responses should
    /// survive in the cache before they are considered expired and evicted.
    /// </param>
    /// <param name="cachingKeys">
    /// An optional collection of unique strings that should be hashed when generating the cache keys for cached AI
    /// responses. See <see cref="ReportingConfiguration.CachingKeys"/> for more information about this concept.
    /// </param>
    /// <param name="executionName">
    /// The name of the current execution. See <see cref="ScenarioRun.ExecutionName"/> for more information about this
    /// concept. Uses a fixed default value <c>"Default"</c> if omitted.
    /// </param>
    /// <param name="evaluationMetricInterpreter">
    /// An optional function that can be used to override <see cref="EvaluationMetricInterpretation"/>s for
    /// <see cref="EvaluationMetric"/>s returned from evaluations that use the returned
    /// <see cref="ReportingConfiguration"/>. The supplied function can either return a new
    /// <see cref="EvaluationMetricInterpretation"/> for any <see cref="EvaluationMetric"/> that is supplied to it, or
    /// return <see langword="null"/> if the <see cref="EvaluationMetric.Interpretation"/> should be left unchanged.
    /// </param>
    /// <param name="tags">
    /// A optional set of text tags applicable to all <see cref="ScenarioRun"/>s created using the returned
    /// <see cref="ReportingConfiguration"/>.
    /// </param>
    /// <returns>
    /// A <see cref="ReportingConfiguration"/> that persists <see cref="ScenarioRunResult"/>s to disk and also uses the
    /// disk to cache AI responses.
    /// </returns>
#pragma warning disable S107 // Methods should not have too many parameters
    public static ReportingConfiguration Create(
        string storageRootPath,
        IEnumerable<IEvaluator> evaluators,
        ChatConfiguration? chatConfiguration = null,
        bool enableResponseCaching = true,
        TimeSpan? timeToLiveForCacheEntries = null,
        IEnumerable<string>? cachingKeys = null,
        string executionName = Defaults.DefaultExecutionName,
        Func<EvaluationMetric, EvaluationMetricInterpretation?>? evaluationMetricInterpreter = null,
        IEnumerable<string>? tags = null)
#pragma warning restore S107
    {
        storageRootPath = Path.GetFullPath(storageRootPath);

        IEvaluationResponseCacheProvider? responseCacheProvider =
            chatConfiguration is not null && enableResponseCaching
                ? new DiskBasedResponseCacheProvider(storageRootPath, timeToLiveForCacheEntries)
                : null;

        IEvaluationResultStore resultStore = new DiskBasedResultStore(storageRootPath);

        return new ReportingConfiguration(
            evaluators,
            resultStore,
            chatConfiguration,
            responseCacheProvider,
            cachingKeys,
            executionName,
            evaluationMetricInterpreter,
            tags);
    }
}
