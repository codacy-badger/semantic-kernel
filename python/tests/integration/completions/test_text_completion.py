# Copyright (c) Microsoft. All rights reserved.

import os
import sys
from functools import partial, reduce
from typing import Any

import pytest
from openai import AsyncAzureOpenAI

<<<<<<< main
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.chat_completion_client_base import (
    ChatCompletionClientBase,
)
=======
>>>>>>> upstream/main
from semantic_kernel.connectors.ai.google.google_ai.google_ai_prompt_execution_settings import (
    GoogleAITextPromptExecutionSettings,
)
from semantic_kernel.connectors.ai.google.google_ai.services.google_ai_text_completion import (
    GoogleAITextCompletion,
)
from semantic_kernel.connectors.ai.google.vertex_ai.services.vertex_ai_text_completion import (
    VertexAITextCompletion,
)
from semantic_kernel.connectors.ai.google.vertex_ai.vertex_ai_prompt_execution_settings import (
    VertexAITextPromptExecutionSettings,
)
<<<<<<< main
from semantic_kernel.connectors.ai.hugging_face.hf_prompt_execution_settings import (
    HuggingFacePromptExecutionSettings,
)
from semantic_kernel.connectors.ai.hugging_face.services.hf_text_completion import (
    HuggingFaceTextCompletion,
)
from semantic_kernel.connectors.ai.open_ai.prompt_execution_settings.open_ai_prompt_execution_settings import (
    OpenAITextPromptExecutionSettings,
)
from semantic_kernel.connectors.ai.open_ai.services.azure_text_completion import (
    AzureTextCompletion,
)
from semantic_kernel.connectors.ai.open_ai.services.open_ai_text_completion import (
    OpenAITextCompletion,
)
from semantic_kernel.connectors.ai.prompt_execution_settings import (
    PromptExecutionSettings,
)
from semantic_kernel.connectors.ai.text_completion_client_base import (
    TextCompletionClientBase,
)
from semantic_kernel.contents import TextContent
from tests.integration.completions.test_utils import retry
=======
from semantic_kernel.connectors.ai.hugging_face.hf_prompt_execution_settings import HuggingFacePromptExecutionSettings
from semantic_kernel.connectors.ai.hugging_face.services.hf_text_completion import HuggingFaceTextCompletion
from semantic_kernel.connectors.ai.ollama.ollama_prompt_execution_settings import OllamaTextPromptExecutionSettings
from semantic_kernel.connectors.ai.ollama.services.ollama_text_completion import OllamaTextCompletion
from semantic_kernel.connectors.ai.open_ai.prompt_execution_settings.open_ai_prompt_execution_settings import (
    OpenAITextPromptExecutionSettings,
)
from semantic_kernel.connectors.ai.open_ai.services.azure_text_completion import AzureTextCompletion
from semantic_kernel.connectors.ai.open_ai.services.open_ai_text_completion import OpenAITextCompletion
from semantic_kernel.connectors.ai.open_ai.settings.azure_open_ai_settings import AzureOpenAISettings
from semantic_kernel.connectors.ai.text_completion_client_base import TextCompletionClientBase
from semantic_kernel.contents.chat_message_content import ChatMessageContent
from semantic_kernel.contents.text_content import TextContent
>>>>>>> upstream/main

if sys.version_info >= (3, 12):
    from typing import override  # pragma: no cover
else:
    from typing_extensions import override  # pragma: no cover

<<<<<<< main
def setup(
    kernel: Kernel,
    service: str,
    execution_settings_kwargs: dict[str, Any],
    services: dict[str, tuple[TextCompletionClientBase, type[PromptExecutionSettings]]],
):
    kernel.add_service(services[service][0])
    kernel.add_function(
        function_name="text",
        plugin_name="text",
        prompt="If someone asks how you are, always include the word 'well', "
        "if you get a direct question, answer the question. {{$input}}",
        prompt_execution_settings=services[service][1](**execution_settings_kwargs),
    )


@pytest.fixture(scope="module")
def services() -> (
    dict[str, tuple[ChatCompletionClientBase, type[PromptExecutionSettings]]]
):
    return {
        "openai": (OpenAITextCompletion(), OpenAITextPromptExecutionSettings),
        "azure": (AzureTextCompletion(), OpenAITextPromptExecutionSettings),
        "hf_t2t": (
            HuggingFaceTextCompletion(
                service_id="patrickvonplaten/t5-tiny-random",
                ai_model_id="patrickvonplaten/t5-tiny-random",
                task="text2text-generation",
            ),
            HuggingFacePromptExecutionSettings,
        ),
        "hf_summ": (
            HuggingFaceTextCompletion(
                service_id="jotamunz/billsum_tiny_summarization",
                ai_model_id="jotamunz/billsum_tiny_summarization",
                task="summarization",
            ),
            HuggingFacePromptExecutionSettings,
        ),
        "hf_gen": (
            HuggingFaceTextCompletion(
                service_id="HuggingFaceM4/tiny-random-LlamaForCausalLM",
                ai_model_id="HuggingFaceM4/tiny-random-LlamaForCausalLM",
                task="text-generation",
            ),
            HuggingFacePromptExecutionSettings,
        ),
        "google_ai": (GoogleAITextCompletion(), GoogleAITextPromptExecutionSettings),
        "vertex_ai": (VertexAITextCompletion(), VertexAITextPromptExecutionSettings),
    }
=======
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.prompt_execution_settings import PromptExecutionSettings
from tests.integration.completions.completion_test_base import CompletionTestBase, ServiceType
from tests.integration.completions.test_utils import retry

ollama_setup: bool = False
try:
    if os.environ["OLLAMA_MODEL"]:
        ollama_setup = True
except KeyError:
    ollama_setup = False
>>>>>>> upstream/main


pytestmark = pytest.mark.parametrize(
    "service_id, execution_settings_kwargs, inputs, kwargs",
    [
        pytest.param(
            "openai",
            {},
            ["Repeat the word Hello"],
            {},
            id="openai_text_completion",
        ),
        pytest.param(
            "azure",
            {},
            ["Repeat the word Hello"],
            {},
            id="azure_text_completion",
        ),
        pytest.param(
            "hf_t2t",
            {},
            ["translate English to Dutch: Hello"],
            {},
            id="huggingface_text_completion_translation",
        ),
        pytest.param(
            "hf_summ",
            {},
            [
                """Summarize: Whales are fully aquatic, open-ocean animals:
        they can feed, mate, give birth, suckle and raise their young at sea.
        Whales range in size from the 2.6 metres (8.5 ft) and 135 kilograms (298 lb)
        dwarf sperm whale to the 29.9 metres (98 ft) and 190 tonnes (210 short tons) blue whale,
        which is the largest known animal that has ever lived. The sperm whale is the largest
        toothed predator on Earth. Several whale species exhibit sexual dimorphism,
        in that the females are larger than males."""
            ],
            {},
            id="huggingface_text_completion_summarization",
        ),
        pytest.param(
            "hf_gen",
            {},
            ["Hello, I like sleeping and "],
            {},
            id="huggingface_text_completion_generation",
        ),
        pytest.param(
            "ollama",
            {},
            ["Repeat the word Hello"],
            {},
            marks=pytest.mark.skipif(not ollama_setup, reason="Need local Ollama setup"),
            id="ollama_text_completion",
        ),
        pytest.param(
            "google_ai",
            {},
            ["Repeat the word Hello"],
            {},
            marks=pytest.mark.skip(reason="Skipping due to 429s from Google AI."),
            id="google_ai_text_completion",
        ),
        pytest.param(
            "vertex_ai",
            {},
            ["Repeat the word Hello"],
            {},
            id="vertex_ai_text_completion",
        ),
    ],
)


@pytest.mark.asyncio(scope="module")
<<<<<<< main
async def test_text_completion(
    kernel: Kernel,
    service: str,
    execution_settings_kwargs: dict[str, Any],
    inputs: list[str],
    outputs: list[str],
    services: dict[str, tuple[TextCompletionClientBase, type[PromptExecutionSettings]]],
):
    setup(kernel, service, execution_settings_kwargs, services)
    for message, output in zip(inputs, outputs):
        await retry(
            partial(
                execute_invoke,
                kernel=kernel,
                input=message,
                output=output,
                stream=False,
            ),
            retries=5,
        )
=======
class TestTextCompletion(CompletionTestBase):
    """Test class for text completion"""
>>>>>>> upstream/main

    @override
    @pytest.fixture(scope="class")
    def services(self) -> dict[str, tuple[ServiceType, type[PromptExecutionSettings]]]:
        azure_openai_settings = AzureOpenAISettings.create()
        endpoint = azure_openai_settings.endpoint
        deployment_name = azure_openai_settings.text_deployment_name
        ad_token = azure_openai_settings.get_azure_openai_auth_token()
        api_version = azure_openai_settings.api_version
        azure_custom_client = AzureTextCompletion(
            async_client=AsyncAzureOpenAI(
                azure_endpoint=endpoint,
                azure_deployment=deployment_name,
                azure_ad_token=ad_token,
                api_version=api_version,
                default_headers={"Test-User-X-ID": "test"},
            ),
        )

<<<<<<< main
@pytest.mark.asyncio(scope="module")
async def test_streaming_text_completion(
    kernel: Kernel,
    service: str,
    execution_settings_kwargs: dict[str, Any],
    inputs: list[str],
    outputs: list[str],
    services: dict[str, tuple[ChatCompletionClientBase, type[PromptExecutionSettings]]],
):
    setup(kernel, service, execution_settings_kwargs, services)
    for message, output in zip(inputs, outputs):
        await retry(
            partial(
                execute_invoke, kernel=kernel, input=message, output=output, stream=True
            ),
            retries=5,
        )


async def execute_invoke(kernel: Kernel, input: str, output: str, stream: bool) -> None:
    if stream:
        invocation = kernel.invoke_stream(
            function_name="text", plugin_name="text", input=input
        )
        parts = [part[0] async for part in invocation]
        if parts:
            response = reduce(lambda p, r: p + r, parts)
        else:
            raise AssertionError("No response")
    else:
        invocation = await kernel.invoke(
            function_name="text", plugin_name="text", input=input
        )
        assert invocation is not None
        response = invocation.value[0]
    print(response)
    if isinstance(response, TextContent):
        assert response.text is not None
        assert output in response.text
        return
    raise AssertionError(
        f"Unexpected output: response: {invocation}, type: {type(invocation)}"
    )
=======
        return {
            "openai": (OpenAITextCompletion(), OpenAITextPromptExecutionSettings),
            "azure": (AzureTextCompletion(), OpenAITextPromptExecutionSettings),
            "azure_custom_client": (azure_custom_client, OpenAITextPromptExecutionSettings),
            "ollama": (OllamaTextCompletion() if ollama_setup else None, OllamaTextPromptExecutionSettings),
            "google_ai": (GoogleAITextCompletion(), GoogleAITextPromptExecutionSettings),
            "vertex_ai": (VertexAITextCompletion(), VertexAITextPromptExecutionSettings),
            "hf_t2t": (
                HuggingFaceTextCompletion(
                    service_id="patrickvonplaten/t5-tiny-random",
                    ai_model_id="patrickvonplaten/t5-tiny-random",
                    task="text2text-generation",
                ),
                HuggingFacePromptExecutionSettings,
            ),
            "hf_summ": (
                HuggingFaceTextCompletion(
                    service_id="jotamunz/billsum_tiny_summarization",
                    ai_model_id="jotamunz/billsum_tiny_summarization",
                    task="summarization",
                ),
                HuggingFacePromptExecutionSettings,
            ),
            "hf_gen": (
                HuggingFaceTextCompletion(
                    service_id="HuggingFaceM4/tiny-random-LlamaForCausalLM",
                    ai_model_id="HuggingFaceM4/tiny-random-LlamaForCausalLM",
                    task="text-generation",
                ),
                HuggingFacePromptExecutionSettings,
            ),
        }

    async def get_text_completion_response(
        self,
        service: TextCompletionClientBase,
        execution_settings: PromptExecutionSettings,
        prompt: str,
        stream: bool,
    ) -> Any:
        """Get response from the service

        Args:
            kernel (Kernel): Kernel instance.
            service (ChatCompletionClientBase): Chat completion service.
            execution_settings (PromptExecutionSettings): Execution settings.
            prompt (str): Input string.
            stream (bool): Stream flag.
        """
        if stream:
            response = service.get_streaming_text_content(
                prompt=prompt,
                settings=execution_settings,
            )
            parts = [part async for part in response]
            if parts:
                response = reduce(lambda p, r: p + r, parts)
            else:
                raise AssertionError("No response")
        else:
            response = await service.get_text_content(
                prompt=prompt,
                settings=execution_settings,
            )

        return response

    @override
    async def test_completion(
        self,
        kernel: Kernel,
        service_id: str,
        services: dict[str, tuple[ServiceType, type[PromptExecutionSettings]]],
        execution_settings_kwargs: dict[str, Any],
        inputs: list[str | ChatMessageContent | list[ChatMessageContent]],
        kwargs: dict[str, Any],
    ) -> None:
        await self._test_helper(service_id, services, execution_settings_kwargs, inputs, False)

    @override
    async def test_streaming_completion(
        self,
        kernel: Kernel,
        service_id: str,
        services: dict[str, tuple[ServiceType, type[PromptExecutionSettings]]],
        execution_settings_kwargs: dict[str, Any],
        inputs: list[str | ChatMessageContent | list[ChatMessageContent]],
        kwargs: dict[str, Any],
    ):
        await self._test_helper(service_id, services, execution_settings_kwargs, inputs, True)

    @override
    def evaluate(self, test_target: Any, **kwargs):
        print(test_target)
        if isinstance(test_target, TextContent):
            # Test is considered successful if the test_target is not empty
            assert test_target.text, "Error: Empty test target"
            return
        raise AssertionError(f"Unexpected output: {test_target}, type: {type(test_target)}")

    async def _test_helper(
        self,
        service_id: str,
        services: dict[str, tuple[ServiceType, type[PromptExecutionSettings]]],
        execution_settings_kwargs: dict[str, Any],
        inputs: list[str],
        stream: bool,
    ):
        service, settings_type = services[service_id]

        for test_input in inputs:
            response = await retry(
                partial(
                    self.get_text_completion_response,
                    service=service,
                    execution_settings=settings_type(**execution_settings_kwargs),
                    prompt=test_input,
                    stream=stream,
                ),
                retries=5,
            )
            self.evaluate(response)
>>>>>>> upstream/main
