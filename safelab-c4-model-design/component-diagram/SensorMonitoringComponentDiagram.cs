using Structurizr;

namespace safelab_c4_model_design
{
    public class SensorMonitoringComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "SensorMonitoringComponent";

        public Component sensor_controller { get; private set; }
        public Component telemetry_controller { get; private set; }
        public Component sensor_service { get; private set; }
        public Component telemetry_processing_service { get; private set; }
        public Component threshold_evaluation_service { get; private set; }
        public Component sensor_repository { get; private set; }

        public SensorMonitoringComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            sensor_controller = containerDiagram.rest_api.AddComponent(
                "Sensor Controller",
                "Handles requests for sensor registration, status, and configuration.",
                "ASP.NET Core Controller"
            );

            telemetry_controller = containerDiagram.rest_api.AddComponent(
                "Telemetry Controller",
                "Receives and exposes real-time sensor readings from monitored environments.",
                "ASP.NET Core Controller"
            );

            sensor_service = containerDiagram.rest_api.AddComponent(
                "Sensor Service",
                "Manages sensor metadata, assigned assets, and operational availability.",
                "C# Service"
            );

            telemetry_processing_service = containerDiagram.rest_api.AddComponent(
                "Telemetry Processing Service",
                "Processes temperature, humidity, and device telemetry received from sensors.",
                "C# Service"
            );

            threshold_evaluation_service = containerDiagram.rest_api.AddComponent(
                "Threshold Evaluation Service",
                "Evaluates sensor readings against configured safe ranges and threshold rules.",
                "C# Service"
            );

            sensor_repository = containerDiagram.rest_api.AddComponent(
                "Sensor Repository",
                "Reads and writes sensor, telemetry, and threshold records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                sensor_controller,
                "Reviews sensor status"
            );

            contextDiagram.laboratory_staff.Uses(
                telemetry_controller,
                "Monitors live readings"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                telemetry_controller,
                "Monitors storage conditions"
            );

            contextDiagram.safelab_administrator.Uses(
                sensor_controller,
                "Configures sensor devices"
            );

            contextDiagram.iot_sensor.Uses(
                telemetry_controller,
                "Sends real-time telemetry",
                "JSON/HTTPS"
            );

            sensor_controller.Uses(
                sensor_service,
                "Delegates sensor logic"
            );

            telemetry_controller.Uses(
                telemetry_processing_service,
                "Delegates telemetry processing"
            );

            sensor_service.Uses(
                sensor_repository,
                "Persists sensor metadata"
            );

            telemetry_processing_service.Uses(
                threshold_evaluation_service,
                "Validates safe ranges"
            );

            telemetry_processing_service.Uses(
                sensor_repository,
                "Persists telemetry readings"
            );

            threshold_evaluation_service.Uses(
                sensor_repository,
                "Reads threshold rules"
            );

            sensor_repository.Uses(
                containerDiagram.database,
                "Reads and writes sensor data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#00695c",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            sensor_controller.AddTags(componentTag);
            telemetry_controller.AddTags(componentTag);
            sensor_service.AddTags(componentTag);
            telemetry_processing_service.AddTags(componentTag);
            threshold_evaluation_service.AddTags(componentTag);
            sensor_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-sensor-monitoring",
                "Component Diagram - Sensor Monitoring Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Sensor Monitoring";

            componentView.Add(sensor_controller);
            componentView.Add(telemetry_controller);
            componentView.Add(sensor_service);
            componentView.Add(telemetry_processing_service);
            componentView.Add(threshold_evaluation_service);
            componentView.Add(sensor_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(contextDiagram.iot_sensor);
            componentView.Add(containerDiagram.database);
        }
    }
}
