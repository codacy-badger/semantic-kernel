# Copyright (c) Microsoft. All rights reserved.

from typing import TYPE_CHECKING, Any

from openai import AsyncOpenAI
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
from openai.types.beta.threads.image_file_content_block import ImageFileContentBlock
from openai.types.beta.threads.text_content_block import TextContentBlock
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
from openai.types.beta.threads.image_file_content_block import ImageFileContentBlock
from openai.types.beta.threads.text_content_block import TextContentBlock
=======
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
from openai.types.beta.threads.image_file_content_block import ImageFileContentBlock
from openai.types.beta.threads.text_content_block import TextContentBlock
=======
>>>>>>> Stashed changes
from openai.types.beta.threads.file_citation_delta_annotation import FileCitationDeltaAnnotation
from openai.types.beta.threads.file_path_delta_annotation import FilePathDeltaAnnotation
from openai.types.beta.threads.image_file_content_block import ImageFileContentBlock
from openai.types.beta.threads.image_file_delta_block import ImageFileDeltaBlock
from openai.types.beta.threads.message_delta_event import MessageDeltaEvent
from openai.types.beta.threads.runs.code_interpreter_tool_call import CodeInterpreter
from openai.types.beta.threads.text_content_block import TextContentBlock
from openai.types.beta.threads.text_delta_block import TextDeltaBlock
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

from semantic_kernel.contents.annotation_content import AnnotationContent
from semantic_kernel.contents.chat_message_content import ChatMessageContent
from semantic_kernel.contents.file_reference_content import FileReferenceContent
from semantic_kernel.contents.function_call_content import FunctionCallContent
from semantic_kernel.contents.function_result_content import FunctionResultContent
from semantic_kernel.contents.image_content import ImageContent
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
from semantic_kernel.contents.text_content import TextContent
from semantic_kernel.contents.utils.author_role import AuthorRole
from semantic_kernel.exceptions.agent_exceptions import AgentExecutionException
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
from semantic_kernel.contents.text_content import TextContent
from semantic_kernel.contents.utils.author_role import AuthorRole
from semantic_kernel.exceptions.agent_exceptions import AgentExecutionException
=======
<<<<<<< Updated upstream
=======
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes
from semantic_kernel.contents.streaming_annotation_content import StreamingAnnotationContent
from semantic_kernel.contents.streaming_chat_message_content import StreamingChatMessageContent
from semantic_kernel.contents.streaming_file_reference_content import StreamingFileReferenceContent
from semantic_kernel.contents.streaming_text_content import StreamingTextContent
from semantic_kernel.contents.text_content import TextContent
from semantic_kernel.contents.utils.author_role import AuthorRole
from semantic_kernel.exceptions.agent_exceptions import AgentExecutionException
from semantic_kernel.utils.experimental_decorator import experimental_function
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

if TYPE_CHECKING:
    from openai.resources.beta.threads.messages import Message
    from openai.resources.beta.threads.runs.runs import Run
    from openai.types.beta.threads.annotation import Annotation
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    from openai.types.beta.threads.runs.tool_call import ToolCall
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
    from openai.types.beta.threads.runs.tool_call import ToolCall
=======
    from openai.types.beta.threads.runs import RunStep
    from openai.types.beta.threads.runs.tool_call import ToolCall
    from openai.types.beta.threads.runs.tool_calls_step_details import ToolCallsStepDetails
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
    from openai.types.beta.threads.runs import RunStep
    from openai.types.beta.threads.runs.tool_call import ToolCall
    from openai.types.beta.threads.runs.tool_calls_step_details import ToolCallsStepDetails
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
>>>>>>> Stashed changes


###################################################################
# The methods in this file are used with OpenAIAssistantAgent     #
# related code. They are used to create chat messages, or         #
# generate message content.                                       #
###################################################################


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
@experimental_function
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
@experimental_function
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
@experimental_function
>>>>>>> main
>>>>>>> Stashed changes
async def create_chat_message(
    client: AsyncOpenAI,
    thread_id: str,
    message: "ChatMessageContent",
    allowed_message_roles: list[str] = [AuthorRole.USER, AuthorRole.ASSISTANT],
) -> "Message":
    """Class method to add a chat message, callable from class or instance.

    Args:
        client: The client to use for creating the message.
        thread_id: The thread id.
        message: The chat message.
        allowed_message_roles: The allowed message roles.

    Returns:
        Message: The message.
    """
    if message.role.value not in allowed_message_roles and message.role != AuthorRole.TOOL:
        raise AgentExecutionException(
            f"Invalid message role `{message.role.value}`. Allowed roles are {allowed_message_roles}."
        )

    message_contents: list[dict[str, Any]] = get_message_contents(message=message)

    return await client.beta.threads.messages.create(
        thread_id=thread_id,
        role="assistant" if message.role == AuthorRole.TOOL else message.role.value,  # type: ignore
        content=message_contents,  # type: ignore
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
@experimental_function
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
@experimental_function
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
@experimental_function
>>>>>>> main
>>>>>>> Stashed changes
def get_message_contents(message: "ChatMessageContent") -> list[dict[str, Any]]:
    """Get the message contents.

    Args:
        message: The message.
    """
    contents: list[dict[str, Any]] = []
    for content in message.items:
        if isinstance(content, TextContent):
            contents.append({"type": "text", "text": content.text})
        elif isinstance(content, ImageContent) and content.uri:
            contents.append(content.to_dict())
        elif isinstance(content, FileReferenceContent):
            contents.append({
                "type": "image_file",
                "image_file": {"file_id": content.file_id},
            })
        elif isinstance(content, FunctionResultContent):
            contents.append({"type": "text", "text": content.result})
    return contents


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
def generate_message_content(assistant_name: str, message: "Message") -> ChatMessageContent:
    """Generate message content."""
    role = AuthorRole(message.role)

    content: ChatMessageContent = ChatMessageContent(role=role, name=assistant_name)  # type: ignore
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
@experimental_function
def generate_message_content(
    assistant_name: str, message: "Message", completed_step: "RunStep | None" = None
) -> ChatMessageContent:
    """Generate message content."""
    role = AuthorRole(message.role)

    metadata = (
        {
            "created_at": completed_step.created_at,
            "message_id": message.id,  # message needs to be defined in context
            "step_id": completed_step.id,
            "run_id": completed_step.run_id,
            "thread_id": completed_step.thread_id,
            "assistant_id": completed_step.assistant_id,
            "usage": completed_step.usage,
        }
        if completed_step is not None
        else None
    )

    content: ChatMessageContent = ChatMessageContent(role=role, name=assistant_name, metadata=metadata)  # type: ignore
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

    for item_content in message.content:
        if item_content.type == "text":
            assert isinstance(item_content, TextContentBlock)  # nosec
            content.items.append(
                TextContent(
                    text=item_content.text.value,
                )
            )
            for annotation in item_content.text.annotations:
                content.items.append(generate_annotation_content(annotation))
        elif item_content.type == "image_file":
            assert isinstance(item_content, ImageFileContentBlock)  # nosec
            content.items.append(
                FileReferenceContent(
                    file_id=item_content.image_file.file_id,
                )
            )
    return content


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
@experimental_function
def generate_streaming_message_content(
    assistant_name: str, message_delta_event: "MessageDeltaEvent"
) -> StreamingChatMessageContent:
    """Generate streaming message content from a MessageDeltaEvent."""
    delta = message_delta_event.delta

    # Determine the role
    role = AuthorRole(delta.role) if delta.role is not None else AuthorRole("assistant")

    items: list[StreamingTextContent | StreamingAnnotationContent | StreamingFileReferenceContent] = []

    # Process each content block in the delta
    for delta_block in delta.content or []:
        if delta_block.type == "text":
            assert isinstance(delta_block, TextDeltaBlock)  # nosec
            if delta_block.text and delta_block.text.value:  # Ensure text is not None
                text_value = delta_block.text.value
                items.append(
                    StreamingTextContent(
                        text=text_value,
                        choice_index=delta_block.index,
                    )
                )
                # Process annotations if any
                if delta_block.text.annotations:
                    for annotation in delta_block.text.annotations or []:
                        if isinstance(annotation, (FileCitationDeltaAnnotation, FilePathDeltaAnnotation)):
                            items.append(generate_streaming_annotation_content(annotation))
        elif delta_block.type == "image_file":
            assert isinstance(delta_block, ImageFileDeltaBlock)  # nosec
            if delta_block.image_file and delta_block.image_file.file_id:
                file_id = delta_block.image_file.file_id
                items.append(
                    StreamingFileReferenceContent(
                        file_id=file_id,
                    )
                )

    return StreamingChatMessageContent(role=role, name=assistant_name, items=items, choice_index=0)  # type: ignore


@experimental_function
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
def generate_function_call_content(agent_name: str, fccs: list[FunctionCallContent]) -> ChatMessageContent:
    """Generate function call content.

    Args:
        agent_name: The agent name.
        fccs: The function call contents.

    Returns:
        ChatMessageContent: The chat message content containing the function call content as the items.
    """
    return ChatMessageContent(role=AuthorRole.TOOL, name=agent_name, items=fccs)  # type: ignore


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
@experimental_function
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
@experimental_function
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
@experimental_function
>>>>>>> main
>>>>>>> Stashed changes
def generate_function_result_content(
    agent_name: str, function_step: FunctionCallContent, tool_call: "ToolCall"
) -> ChatMessageContent:
    """Generate function result content."""
    function_call_content: ChatMessageContent = ChatMessageContent(role=AuthorRole.TOOL, name=agent_name)  # type: ignore
    function_call_content.items.append(
        FunctionResultContent(
            function_name=function_step.function_name,
            plugin_name=function_step.plugin_name,
            id=function_step.id,
            result=tool_call.function.output,  # type: ignore
        )
    )
    return function_call_content


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
@experimental_function
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
@experimental_function
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
@experimental_function
>>>>>>> main
>>>>>>> Stashed changes
def get_function_call_contents(run: "Run", function_steps: dict[str, FunctionCallContent]) -> list[FunctionCallContent]:
    """Extract function call contents from the run.

    Args:
        run: The run.
        function_steps: The function steps

    Returns:
        The list of function call contents.
    """
    function_call_contents: list[FunctionCallContent] = []
    required_action = getattr(run, "required_action", None)
    if not required_action or not getattr(required_action, "submit_tool_outputs", False):
        return function_call_contents
    for tool in required_action.submit_tool_outputs.tool_calls:
        fcc = FunctionCallContent(
            id=tool.id,
            index=getattr(tool, "index", None),
            name=tool.function.name,
            arguments=tool.function.arguments,
        )
        function_call_contents.append(fcc)
        function_steps[tool.id] = fcc
    return function_call_contents


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
@experimental_function
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
@experimental_function
>>>>>>> eab985c52d058dc92abc75034bc790079131ce75
=======
=======
@experimental_function
>>>>>>> main
>>>>>>> Stashed changes
def generate_code_interpreter_content(agent_name: str, code: str) -> "ChatMessageContent":
    """Generate code interpreter content.

    Args:
        agent_name: The agent name.
        code: The code.

    Returns:
        ChatMessageContent: The chat message content.
    """
    return ChatMessageContent(
        role=AuthorRole.ASSISTANT,
        content=code,
        name=agent_name,
        metadata={"code": True},
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
@experimental_function
def generate_streaming_tools_content(
    agent_name: str, step_details: "ToolCallsStepDetails"
) -> "StreamingChatMessageContent | None":
    """Generate code interpreter content.

    Args:
        agent_name: The agent name.
        step_details: The current step details.

    Returns:
        StreamingChatMessageContent: The chat message content.
    """
    items: list[StreamingTextContent | StreamingFileReferenceContent] = []

    metadata: dict[str, bool] = {}
    for index, tool in enumerate(step_details.tool_calls):
        if tool.type != "code_interpreter":
            continue
        if tool.code_interpreter.input:
            items.append(
                StreamingTextContent(
                    choice_index=index,
                    text=tool.code_interpreter.input,
                )
            )
            metadata["code"] = True
        if len(tool.code_interpreter.outputs) > 0:
            for output in tool.code_interpreter.outputs:
                assert isinstance(output, CodeInterpreter)  # nosec
                if output.image.file_id:
                    items.append(
                        StreamingFileReferenceContent(
                            file_id=output.image.file_id,
                        )
                    )

    return (
        StreamingChatMessageContent(
            role=AuthorRole.TOOL,
            name=agent_name,
            items=items,  # type: ignore
            choice_index=0,
            metadata=metadata if metadata else None,
        )
        if len(items) > 0
        else None
    )


@experimental_function
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
def generate_annotation_content(annotation: "Annotation") -> AnnotationContent:
    """Generate annotation content."""
    file_id = None
    if hasattr(annotation, "file_path"):
        file_id = annotation.file_path.file_id
    elif hasattr(annotation, "file_citation"):
        file_id = annotation.file_citation.file_id

    return AnnotationContent(
        file_id=file_id,
        quote=annotation.text,
        start_index=annotation.start_index,
        end_index=annotation.end_index,
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


@experimental_function
def generate_streaming_annotation_content(annotation: "Annotation") -> StreamingAnnotationContent:
    """Generate streaming annotation content."""
    file_id = None
    if hasattr(annotation, "file_path") and annotation.file_path:
        file_id = annotation.file_path.file_id if annotation.file_path.file_id else None
    elif hasattr(annotation, "file_citation") and annotation.file_citation:
        file_id = annotation.file_citation.file_id if annotation.file_citation.file_id else None

    return StreamingAnnotationContent(
        file_id=file_id,
        quote=annotation.text,
        start_index=annotation.start_index,
        end_index=annotation.end_index,
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
