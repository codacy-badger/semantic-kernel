# Copyright (c) Microsoft. All rights reserved.

import logging
import sys
from typing import Any, Literal

if sys.version_info >= (3, 11):
    from typing import Self  # pragma: no cover
else:
    from typing_extensions import Self  # pragma: no cover

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
from pydantic import Field, field_validator, model_validator
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
from pydantic import Field, field_validator, model_validator
=======
from pydantic import BaseModel, Field, field_validator, model_validator
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
from pydantic import BaseModel, Field, field_validator, model_validator
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes

from semantic_kernel.connectors.ai.function_call_behavior import FunctionCallBehavior
from semantic_kernel.connectors.ai.prompt_execution_settings import (
    PromptExecutionSettings,
)
from semantic_kernel.exceptions import ServiceInvalidExecutionSettingsError

logger = logging.getLogger(__name__)


class OpenAIPromptExecutionSettings(PromptExecutionSettings):
    """Common request settings for (Azure) OpenAI services."""

    ai_model_id: str | None = Field(None, serialization_alias="model")
    frequency_penalty: float | None = Field(None, ge=-2.0, le=2.0)
    logit_bias: dict[str | int, float] | None = None
    max_tokens: int | None = Field(None, gt=0)
    number_of_responses: int | None = Field(None, ge=1, le=128, serialization_alias="n")
    presence_penalty: float | None = Field(None, ge=-2.0, le=2.0)
    seed: int | None = None
    stop: str | list[str] | None = None
    stream: bool = False
    temperature: float | None = Field(None, ge=0.0, le=2.0)
    top_p: float | None = Field(None, ge=0.0, le=1.0)
    user: str | None = None


class OpenAITextPromptExecutionSettings(OpenAIPromptExecutionSettings):
    """Specific settings for the completions endpoint."""

    prompt: str | None = None
    best_of: int | None = Field(None, ge=1)
    echo: bool = False
    logprobs: int | None = Field(None, ge=0, le=5)
    suffix: str | None = None

    @model_validator(mode="after")
    def check_best_of_and_n(self) -> "OpenAITextPromptExecutionSettings":
        """Check that the best_of parameter is not greater than the number_of_responses parameter."""
        best_of = self.best_of or self.extension_data.get("best_of")
        number_of_responses = self.number_of_responses or self.extension_data.get(
            "number_of_responses"
        )

        if (
            best_of is not None
            and number_of_responses is not None
            and best_of < number_of_responses
        ):
            raise ServiceInvalidExecutionSettingsError(
                "When used with number_of_responses, best_of controls the number of candidate completions and n specifies how many to return, therefore best_of must be greater than number_of_responses."  # noqa: E501
            )

        return self


class OpenAIChatPromptExecutionSettings(OpenAIPromptExecutionSettings):
    """Specific settings for the Chat Completion endpoint."""

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    response_format: dict[Literal["type"], Literal["text", "json_object"]] | None = None
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
    response_format: dict[Literal["type"], Literal["text", "json_object"]] | None = None
=======
    response_format: (
        dict[Literal["type"], Literal["text", "json_object"]] | dict[str, Any] | type[BaseModel] | type | None
    ) = None
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
    response_format: (
        dict[Literal["type"], Literal["text", "json_object"]] | dict[str, Any] | type[BaseModel] | type | None
    ) = None
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
    function_call: str | None = None
    functions: list[dict[str, Any]] | None = None
    messages: list[dict[str, Any]] | None = None
    function_call_behavior: FunctionCallBehavior | None = Field(None, exclude=True)
    tools: list[dict[str, Any]] | None = Field(
        None,
        max_length=64,
        description="Do not set this manually. It is set by the service based on the function choice configuration.",
    )
    tool_choice: str | None = Field(
        None,
        description="Do not set this manually. It is set by the service based on the function choice configuration.",
    )
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
    structured_json_response: bool = Field(False, description="Do not set this manually. It is set by the service.")
    stream_options: dict[str, Any] | None = Field(
        None,
        description="Additional options to pass when streaming is used. Do not set this manually.",
    )
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

    @field_validator("functions", "function_call", mode="after")
    @classmethod
    def validate_function_call(cls, v: str | list[dict[str, Any]] | None = None):
        """Validate the function_call and functions parameters."""
        if v is not None:
            logger.warning(
                "The function_call and functions parameters are deprecated. Please use the tool_choice and tools parameters instead."  # noqa: E501
            )
        return v

    @model_validator(mode="before")
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
    def validate_response_format_and_set_flag(cls, values) -> Any:
        """Validate the response_format and set structured_json_response accordingly."""
        response_format = values.get("response_format", None)

        if response_format is None:
            return values

        if isinstance(response_format, dict):
            if response_format.get("type") == "json_object":
                return values
            if response_format.get("type") == "json_schema":
                json_schema = response_format.get("json_schema")
                if isinstance(json_schema, dict):
                    values["structured_json_response"] = True
                    return values
                raise ServiceInvalidExecutionSettingsError(
                    "If response_format has type 'json_schema', 'json_schema' must be a valid dictionary."
                )
        if isinstance(response_format, type):
            if issubclass(response_format, BaseModel):
                values["structured_json_response"] = True
            else:
                values["structured_json_response"] = True
        else:
            raise ServiceInvalidExecutionSettingsError(
                "response_format must be a dictionary, a subclass of BaseModel, a Python class/type, or None"
            )

        return values

    @model_validator(mode="before")
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
    @classmethod
    def validate_function_calling_behaviors(cls, data) -> Any:
        """Check if function_call_behavior is set and if so, move to use function_choice_behavior instead."""
        # In an attempt to phase out the use of `function_call_behavior` in favor of `function_choice_behavior`,
        # we are syncing the `function_call_behavior` with `function_choice_behavior` if the former is set.
        # This allows us to make decisions off of `function_choice_behavior`. Anytime the `function_call_behavior`
        # is updated, this validation will run to ensure the `function_choice_behavior` stays in sync.
<<<<<<< main
        from semantic_kernel.connectors.ai.function_choice_behavior import (
            FunctionChoiceBehavior,
        )

        if isinstance(data, dict) and "function_call_behavior" in data.get(
            "extension_data", {}
        ):
            data["function_choice_behavior"] = (
                FunctionChoiceBehavior.from_function_call_behavior(
                    data.get("extension_data", {}).get("function_call_behavior")
                )
=======
        from semantic_kernel.connectors.ai.function_choice_behavior import FunctionChoiceBehavior

        if isinstance(data, dict) and "function_call_behavior" in data.get("extension_data", {}):
            data["function_choice_behavior"] = FunctionChoiceBehavior.from_function_call_behavior(
                data.get("extension_data", {}).get("function_call_behavior")
                data.get("extension_data").get("function_call_behavior")
>>>>>>> origin/PR
            )
        return data

    @field_validator("function_call_behavior", mode="after")
    @classmethod
    def check_for_function_call_behavior(cls, v) -> Self:
        """Check if function_choice_behavior is set, if not, set it to default."""
        if v is not None:
            logger.warning(
                "The `function_call_behavior` parameter is deprecated. Please use the `function_choice_behavior` parameter instead."  # noqa: E501
            )
        return v


class OpenAIEmbeddingPromptExecutionSettings(PromptExecutionSettings):
    """Specific settings for the text embedding endpoint."""

    input: str | list[str] | list[int] | list[list[int]] | None = None
    ai_model_id: str | None = Field(None, serialization_alias="model")
    encoding_format: Literal["float", "base64"] | None = None
    user: str | None = None
    extra_headers: dict | None = None
    extra_query: dict | None = None
    extra_body: dict | None = None
    timeout: float | None = None
    dimensions: int | None = Field(None, gt=0, le=3072)
