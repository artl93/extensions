# Release History

## 9.4.4-preview.1.25259.16

- Added an `AsIEmbeddingGenerator` extension method for `ImageEmbeddingsClient`.
- Updated to accomodate the changes in `Microsoft.Extensions.AI.Abstractions`.

## 9.4.3-preview.1.25230.7

- Updated to accomodate the changes in `Microsoft.Extensions.AI.Abstractions`.

## 9.4.0-preview.1.25207.5

- Updated to Azure.AI.Inference 1.0.0-beta.4.
- Renamed `AsChatClient`/`AsEmbeddingGenerator` extension methods to `AsIChatClient`/`AsIEmbeddingGenerator`.
- Removed the public `AzureAIInferenceChatClient`/`AzureAIInferenceEmbeddingGenerator` types. These are only created now via the extension methods.
- Updated to accomodate the changes in `Microsoft.Extensions.AI.Abstractions`.

## 9.3.0-preview.1.25161.3

- Updated to accomodate the changes in `Microsoft.Extensions.AI.Abstractions`.

## 9.3.0-preview.1.25114.11

- Updated to use Azure.AI.Inference 1.0.0-beta.3, adding support for structured output and audio input.

## 9.1.0-preview.1.25064.3

- Fixed handling of text-only user messages.

## 9.0.1-preview.1.24570.5

  - Made the `ToolCallJsonSerializerOptions` property non-nullable.

## 9.0.0-preview.9.24556.5

- Fixed `AzureAIInferenceEmbeddingGenerator` to respect `EmbeddingGenerationOptions.Dimensions`.

## 9.0.0-preview.9.24525.1

- Lowered the required version of System.Text.Json to 8.0.5 when targeting net8.0 or older.
- Updated to use Azure.AI.Inference 1.0.0-beta.2.
- Added `AzureAIInferenceEmbeddingGenerator` and corresponding `AsEmbeddingGenerator` extension method.
- Improved handling of assistant messages that include both text and function call content.

## 9.0.0-preview.9.24507.7

- Initial Preview
