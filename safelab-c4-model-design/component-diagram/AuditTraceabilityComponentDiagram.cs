using Structurizr;

namespace safelab_c4_model_design
{
    public class AuditTraceabilityComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AuditTraceabilityComponent";

        public Component audit_controller { get; private set; }
        public Component traceability_controller { get; private set; }
        public Component audit_service { get; private set; }
        public Component activity_log_service { get; private set; }
        public Component traceability_service { get; private set; }
        public Component audit_repository { get; private set; }

        public AuditTraceabilityComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddComponents()
        {
            audit_controller = containerDiagram.rest_api.AddComponent(
                "Audit Controller",
                "Handles requests for audit logs, user activity history, and system change records.",
                "ASP.NET Core Controller"
            );

            traceability_controller = containerDiagram.rest_api.AddComponent(
                "Traceability Controller",
                "Handles requests for asset, equipment, incident, and command traceability records.",
                "ASP.NET Core Controller"
            );

            audit_service = containerDiagram.rest_api.AddComponent(
                "Audit Service",
                "Coordinates audit operations for users, permissions, alerts, incidents, and system changes.",
                "C# Service"
            );

            activity_log_service = containerDiagram.rest_api.AddComponent(
                "Activity Log Service",
                "Records user actions, configuration changes, access events, and operational events.",
                "C# Service"
            );

            traceability_service = containerDiagram.rest_api.AddComponent(
                "Traceability Service",
                "Builds traceability timelines for assets, sensors, incidents, and remote commands.",
                "C# Service"
            );

            audit_repository = containerDiagram.rest_api.AddComponent(
                "Audit Repository",
                "Reads and writes audit logs, traceability records, activity events, and change history.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                traceability_controller,
                "Reviews equipment history"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                traceability_controller,
                "Reviews compliance traceability"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                audit_controller,
                "Reviews audit evidence"
            );

            contextDiagram.safelab_administrator.Uses(
                audit_controller,
                "Audits platform activity"
            );

            contextDiagram.safelab_administrator.Uses(
                traceability_controller,
                "Reviews operational traceability"
            );

            audit_controller.Uses(
                audit_service,
                "Delegates audit logic"
            );

            traceability_controller.Uses(
                traceability_service,
                "Delegates traceability logic"
            );

            audit_service.Uses(
                activity_log_service,
                "Collects activity events"
            );

            audit_service.Uses(
                audit_repository,
                "Persists audit records"
            );

            activity_log_service.Uses(
                audit_repository,
                "Persists activity logs"
            );

            traceability_service.Uses(
                audit_repository,
                "Reads traceability history"
            );

            audit_repository.Uses(
                containerDiagram.database,
                "Reads and writes audit data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#263238",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            audit_controller.AddTags(componentTag);
            traceability_controller.AddTags(componentTag);
            audit_service.AddTags(componentTag);
            activity_log_service.AddTags(componentTag);
            traceability_service.AddTags(componentTag);
            audit_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-audit-traceability",
                "Component Diagram - Audit & Traceability Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Audit & Traceability";

            componentView.Add(audit_controller);
            componentView.Add(traceability_controller);
            componentView.Add(audit_service);
            componentView.Add(activity_log_service);
            componentView.Add(traceability_service);
            componentView.Add(audit_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
