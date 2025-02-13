# Copyright (c) Microsoft. All rights reserved.
from typing import List, Optional
from xml.etree import ElementTree
from xml.etree.ElementTree import Element

from openai.types.chat import ChatCompletion

from semantic_kernel.connectors.ai.open_ai.models.chat_completion.function_call import FunctionCall
from semantic_kernel.connectors.ai.open_ai.models.chat_completion.tool_calls import ToolCall
from xml.etree.ElementTree import Element

from defusedxml import ElementTree
from openai.types.chat import ChatCompletion

from semantic_kernel.connectors.ai.open_ai.contents.function_call import FunctionCall
from semantic_kernel.connectors.ai.open_ai.contents.tool_calls import ToolCall
from semantic_kernel.contents import ChatMessageContent
from semantic_kernel.contents.chat_role import ChatRole


class OpenAIChatMessageContent(ChatMessageContent):
    """This is the class for OpenAI chat message response content.

    Args:
        inner_content: ChatCompletion - The inner content of the response,
            this should hold all the information from the response so even
            when not creating a subclass a developer can leverage the full thing.
        ai_model_id: Optional[str] - The id of the AI model that generated this response.
        metadata: Dict[str, Any] - Any metadata that should be attached to the response.
        role: ChatRole - The role of the chat message.
        content: Optional[str] - The text of the response.
        encoding: Optional[str] - The encoding of the text.
        function_call: Optional[FunctionCall] - The function call that was generated by this response.
        tool_calls: Optional[List[ToolCall]] - The tool calls that were generated by this response.

    Methods:
        __str__: Returns the content of the response.
    """

    inner_content: Optional[ChatCompletion] = None
    function_call: Optional[FunctionCall] = None
    tool_calls: Optional[List[ToolCall]] = None
    tool_call_id: Optional[str] = None

    @staticmethod
    def ToolIdProperty():
        # Directly using the class name and the attribute name as strings
        return f"{ToolCall.__name__}.{ToolCall.id.__name__}"

    def to_prompt(self, root_key: str) -> str:
        """Convert the OpenAIChatMessageContent to a prompt.

        Returns:
            str - The prompt from the ChatMessageContent.
        """

        root = Element(root_key)
        root.set("role", self.role.value)
        if self.function_call:
            root.set("function_call", self.function_call.model_dump_json(exclude_none=True))
        if self.tool_calls:
            root.set("tool_calls", ",".join([call.model_dump_json(exclude_none=True) for call in self.tool_calls]))
        root.text = self.content or ""
        return ElementTree.tostring(root, encoding=self.encoding or "unicode")
            root.set("tool_calls", "|".join([call.model_dump_json(exclude_none=True) for call in self.tool_calls]))
        if self.tool_call_id:
            root.set("tool_call_id", self.tool_call_id)
        root.text = self.content or ""
        return ElementTree.tostring(root, encoding=self.encoding or "unicode", short_empty_elements=False)

    @classmethod
    def from_element(cls, element: Element) -> "ChatMessageContent":
        """Create a new instance of OpenAIChatMessageContent from a prompt.

        Args:
            prompt: str - The prompt to create the ChatMessageContent from.

        Returns:
            ChatMessageContent - The new instance of ChatMessageContent.
        """
        args = {"role": element.get("role", ChatRole.USER.value), "content": element.text}
        if function_call := element.get("function_call"):
            args["function_call"] = FunctionCall.model_validate_json(function_call)
        if tool_calls := element.get("tool_calls"):
            args["tool_calls"] = [ToolCall.model_validate_json(call) for call in tool_calls.split(",")]
            args["tool_calls"] = [ToolCall.model_validate_json(call) for call in tool_calls.split("|")]
        if tool_call_id := element.get("tool_call_id"):
            args["tool_call_id"] = tool_call_id
        return cls(**args)
