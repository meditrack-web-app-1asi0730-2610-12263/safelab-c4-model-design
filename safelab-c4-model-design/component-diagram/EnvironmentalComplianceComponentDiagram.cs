using Structurizr;

namespace safelab_c4_model_design
{
    public class EnvironmentalComplianceComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "EnvironmentalComplianceComponent";

        public Component compliance_controller { get; private set; }
        public Component rule_controller { get; private set; }
        public Component compliance_service { get; private set; }
        public Component condition_validation_service { get; private set; }
        public Component violation_detection_service { get; private set; }
        public Component compliance_repository { get; private set; }

        public EnvironmentalComplianceComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            compliance_controller = containerDiagram.rest_api.AddComponent(
                "Compliance Controller",
                "Handles requests for compliance status, environmental validations, and condition results.",
                "ASP.NET Core Controller"
            );

            rule_controller = containerDiagram.rest_api.AddComponent(
                "Rule Controller",
                "Handles requests for configuring compliance rules and allowed storage ranges.",
                "ASP.NET Core Controller"
            );

            compliance_service = containerDiagram.rest_api.AddComponent(
                "Compliance Service",
                "Coordinates compliance checks for laboratory and pharmaceutical storage conditions.",
                "C# Service"
            );

            condition_validation_service = containerDiagram.rest_api.AddComponent(
                "Condition Validation Service",
                "Validates temperature, humidity, and environmental readings against safe ranges.",
                "C# Service"
            );

            violation_detection_service = containerDiagram.rest_api.AddComponent(
                "Violation Detection Service",
                "Detects compliance violations and classifies them by severity and affected asset.",
                "C# Service"
            );

            compliance_repository = containerDiagram.rest_api.AddComponent(
                "Compliance Repository",
                "Reads and writes compliance rules, validation results, and violation records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                compliance_controller,
                "Reviews compliance status"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                compliance_controller,
                "Reviews storage compliance"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                rule_controller,
                "Configures compliance rules"
            );

            contextDiagram.safelab_administrator.Uses(
                rule_controller,
                "Manages default rule settings"
            );

            compliance_controller.Uses(
                compliance_service,
                "Delegates compliance logic"
            );

            rule_controller.Uses(
                compliance_service,
                "Delegates rule management"
            );

            compliance_service.Uses(
                condition_validation_service,
                "Validates environmental conditions"
            );

            condition_validation_service.Uses(
                violation_detection_service,
                "Reports unsafe conditions"
            );

            compliance_service.Uses(
                compliance_repository,
                "Persists compliance rules and results"
            );

            violation_detection_service.Uses(
                compliance_repository,
                "Persists violation records"
            );

            compliance_repository.Uses(
                containerDiagram.database,
                "Reads and writes compliance data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#7b1fa2",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            compliance_controller.AddTags(componentTag);
            rule_controller.AddTags(componentTag);
            compliance_service.AddTags(componentTag);
            condition_validation_service.AddTags(componentTag);
            violation_detection_service.AddTags(componentTag);
            compliance_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-environmental-compliance",
                "Component Diagram - Environmental Compliance Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Environmental Compliance";

            componentView.Add(compliance_controller);
            componentView.Add(rule_controller);
            componentView.Add(compliance_service);
            componentView.Add(condition_validation_service);
            componentView.Add(violation_detection_service);
            componentView.Add(compliance_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
