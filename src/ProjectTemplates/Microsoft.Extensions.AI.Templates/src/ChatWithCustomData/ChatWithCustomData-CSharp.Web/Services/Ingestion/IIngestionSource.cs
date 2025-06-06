﻿using Microsoft.Extensions.AI;

namespace ChatWithCustomData_CSharp.Web.Services.Ingestion;

public interface IIngestionSource
{
    string SourceId { get; }

    Task<IEnumerable<IngestedDocument>> GetNewOrModifiedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments);

    Task<IEnumerable<IngestedDocument>> GetDeletedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments);

    Task<IEnumerable<IngestedChunk>> CreateChunksForDocumentAsync(IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator, IngestedDocument document);
}
