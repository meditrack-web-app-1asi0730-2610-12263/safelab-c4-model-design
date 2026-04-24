using Structurizr;

namespace safelab_c4_model_design
{
    public class IncidentManagementComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "IncidentManagementComponent";

        public Component incident_controller { get; private set; }
        public Component resolution_controller { get; private set; }
        public Component incident_service { get; private set; }
        public Component assignment_service { get; private set; }
        public Component resolution_workflow_service { get; private set; }
        public Component incident_repository { get; private set; }

        public IncidentManagementComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            incident_controller = containerDiagram.rest_api.AddComponent(
                "Incident Controller",
                "Handles requests for incident registration, status tracking, and incident details.",
                "ASP.NET Core Controller"
            );

            resolution_controller = containerDiagram.rest_api.AddComponent(
                "Resolution Controller",
                "Handles requests for incident resolution, corrective actions, and closure records.",
                "ASP.NET Core Controller"
            );

            incident_service = containerDiagram.rest_api.AddComponent(
                "Incident Service",
                "Creates, classifies, and tracks incidents caused by alerts, failures, or supply issues.",
                "C# Service"
            );

            assignment_service = containerDiagram.rest_api.AddComponent(
                "Assignment Service",
                "Assigns incidents to responsible users based on role, shift, and severity.",
                "C# Service"
            );

            resolution_workflow_service = containerDiagram.rest_api.AddComponent(
                "Resolution Workflow Service",
                "Manages incident lifecycle, corrective actions, resolution evidence, and closure.",
                "C# Service"
            );

            incident_repository = containerDiagram.rest_api.AddComponent(
                "Incident Repository",
                "Reads and writes incidents, assignments, resolution notes, and closure records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                incident_controller,
                "Reports and tracks incidents"
            );

            contextDiagram.laboratory_staff.Uses(
                resolution_controller,
                "Registers corrective actions"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                incident_controller,
                "Reviews critical incidents"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                resolution_controller,
                "Reviews incident resolutions"
            );

            contextDiagram.safelab_administrator.Uses(
                incident_controller,
                "Monitors platform incidents"
            );

            contextDiagram.safelab_administrator.Uses(
                resolution_controller,
                "Audits incident closure"
            );

            incident_controller.Uses(
                incident_service,
                "Delegates incident logic"
            );

            resolution_controller.Uses(
                resolution_workflow_service,
                "Delegates resolution workflow"
            );

            incident_service.Uses(
                assignment_service,
                "Assigns responsible users"
            );

            incident_service.Uses(
                incident_repository,
                "Persists incident records"
            );

            assignment_service.Uses(
                incident_repository,
                "Reads assignee and shift data"
            );

            resolution_workflow_service.Uses(
                incident_repository,
                "Persists resolution and closure data"
            );

            incident_repository.Uses(
                containerDiagram.database,
                "Reads and writes incident data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#c62828",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            incident_controller.AddTags(componentTag);
            resolution_controller.AddTags(componentTag);
            incident_service.AddTags(componentTag);
            assignment_service.AddTags(componentTag);
            resolution_workflow_service.AddTags(componentTag);
            incident_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-incident-management",
                "Component Diagram - Incident Management Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Incident Management";

            componentView.Add(incident_controller);
            componentView.Add(resolution_controller);
            componentView.Add(incident_service);
            componentView.Add(assignment_service);
            componentView.Add(resolution_workflow_service);
            componentView.Add(incident_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
