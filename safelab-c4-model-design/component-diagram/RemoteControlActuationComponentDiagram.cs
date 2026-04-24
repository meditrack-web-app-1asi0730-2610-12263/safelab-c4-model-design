using Structurizr;

namespace safelab_c4_model_design
{
    public class RemoteControlActuationComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "RemoteControlActuationComponent";

        public Component control_controller { get; private set; }
        public Component command_controller { get; private set; }
        public Component command_service { get; private set; }
        public Component actuation_service { get; private set; }
        public Component safety_validation_service { get; private set; }
        public Component device_adapter { get; private set; }
        public Component command_repository { get; private set; }

        public RemoteControlActuationComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            control_controller = containerDiagram.rest_api.AddComponent(
                "Control Controller",
                "Handles requests for remote equipment controls and actuator operations.",
                "ASP.NET Core Controller"
            );

            command_controller = containerDiagram.rest_api.AddComponent(
                "Command Controller",
                "Handles requests for command history, retries, and command status.",
                "ASP.NET Core Controller"
            );

            command_service = containerDiagram.rest_api.AddComponent(
                "Command Service",
                "Creates and manages remote commands for monitored devices.",
                "C# Service"
            );

            actuation_service = containerDiagram.rest_api.AddComponent(
                "Actuation Service",
                "Executes validated commands on connected devices and actuators.",
                "C# Service"
            );

            safety_validation_service = containerDiagram.rest_api.AddComponent(
                "Safety Validation Service",
                "Validates commands against safety rules, permissions, and equipment state.",
                "C# Service"
            );

            device_adapter = containerDiagram.rest_api.AddComponent(
                "Device Adapter",
                "Integrates SafeLab commands with external IoT devices and gateways.",
                "C# Adapter"
            );

            command_repository = containerDiagram.rest_api.AddComponent(
                "Command Repository",
                "Reads and writes command logs, execution status, retries, and actuator records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                control_controller,
                "Controls authorized equipment"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                control_controller,
                "Controls storage devices"
            );

            contextDiagram.safelab_administrator.Uses(
                command_controller,
                "Reviews command operations"
            );

            contextDiagram.safelab_administrator.Uses(
                control_controller,
                "Executes administrative controls"
            );

            control_controller.Uses(
                command_service,
                "Delegates control logic"
            );

            command_controller.Uses(
                command_service,
                "Requests command status"
            );

            command_service.Uses(
                safety_validation_service,
                "Validates commands"
            );

            command_service.Uses(
                actuation_service,
                "Executes approved commands"
            );

            actuation_service.Uses(
                device_adapter,
                "Sends device commands"
            );

            command_service.Uses(
                command_repository,
                "Persists command requests"
            );

            actuation_service.Uses(
                command_repository,
                "Persists execution results"
            );

            device_adapter.Uses(
                contextDiagram.iot_sensor,
                "Sends control commands",
                "JSON/HTTPS"
            );

            command_repository.Uses(
                containerDiagram.database,
                "Reads and writes command data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#5d4037",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            control_controller.AddTags(componentTag);
            command_controller.AddTags(componentTag);
            command_service.AddTags(componentTag);
            actuation_service.AddTags(componentTag);
            safety_validation_service.AddTags(componentTag);
            device_adapter.AddTags(componentTag);
            command_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-remote-control-actuation",
                "Component Diagram - Remote Control & Actuation Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Remote Control & Actuation";

            componentView.Add(control_controller);
            componentView.Add(command_controller);
            componentView.Add(command_service);
            componentView.Add(actuation_service);
            componentView.Add(safety_validation_service);
            componentView.Add(device_adapter);
            componentView.Add(command_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(contextDiagram.iot_sensor);
            componentView.Add(containerDiagram.database);
        }
    }
}
