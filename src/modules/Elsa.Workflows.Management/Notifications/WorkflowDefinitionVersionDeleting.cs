using Elsa.Mediator.Contracts;
using Elsa.Workflows.Management.Entities;
using JetBrains.Annotations;

namespace Elsa.Workflows.Management.Notifications;

/// <summary>
/// A notification that is sent when a workflow definition version is about to be deleted.
/// </summary>
[PublicAPI]
public record WorkflowDefinitionVersionDeleting(WorkflowDefinition WorkflowDefinition) : INotification;