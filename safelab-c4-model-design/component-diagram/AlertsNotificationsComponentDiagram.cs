using Structurizr;

namespace safelab_c4_model_design
{
    public class AlertsNotificationsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AlertsNotificationsComponent";

        public Component alert_controller { get; private set; }
        public Component notification_controller { get; private set; }
        public Component alert_service { get; private set; }
        public Component notification_dispatcher { get; private set; }
        public Component escalation_service { get; private set; }
        public Component alert_repository { get; private set; }
        public Component notification_service_adapter { get; private set; }

        public AlertsNotificationsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            alert_controller = containerDiagram.rest_api.AddComponent(
                "Alert Controller",
                "Handles requests for active alerts, alert history, acknowledgements, and severity filters.",
                "ASP.NET Core Controller"
            );

            notification_controller = containerDiagram.rest_api.AddComponent(
                "Notification Controller",
                "Handles requests for notification status, delivery preferences, and alert recipients.",
                "ASP.NET Core Controller"
            );

            alert_service = containerDiagram.rest_api.AddComponent(
                "Alert Service",
                "Creates, updates, and classifies alerts based on monitored conditions and incidents.",
                "C# Service"
            );

            notification_dispatcher = containerDiagram.rest_api.AddComponent(
                "Notification Dispatcher",
                "Routes alert notifications to the correct users based on role, shift, and severity.",
                "C# Service"
            );

            escalation_service = containerDiagram.rest_api.AddComponent(
                "Escalation Service",
                "Escalates unresolved critical alerts to supervisors or administrators.",
                "C# Service"
            );

            alert_repository = containerDiagram.rest_api.AddComponent(
                "Alert Repository",
                "Reads and writes alerts, notification records, recipients, and acknowledgement data.",
                "C# Repository"
            );

            notification_service_adapter = containerDiagram.rest_api.AddComponent(
                "Notification Service Adapter",
                "Integrates SafeLab alerts with the external notification service.",
                "C# Adapter"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                alert_controller,
                "Reviews and acknowledges alerts"
            );

            contextDiagram.laboratory_staff.Uses(
                notification_controller,
                "Reviews alert notifications"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                alert_controller,
                "Reviews critical storage alerts"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                notification_controller,
                "Reviews compliance notifications"
            );

            contextDiagram.safelab_administrator.Uses(
                alert_controller,
                "Monitors platform alerts"
            );

            contextDiagram.safelab_administrator.Uses(
                notification_controller,
                "Configures notification delivery"
            );

            alert_controller.Uses(
                alert_service,
                "Delegates alert logic"
            );

            notification_controller.Uses(
                notification_dispatcher,
                "Delegates notification logic"
            );

            alert_service.Uses(
                escalation_service,
                "Escalates unresolved critical alerts"
            );

            alert_service.Uses(
                alert_repository,
                "Persists alert records"
            );

            notification_dispatcher.Uses(
                alert_repository,
                "Reads recipients and notification rules"
            );

            notification_dispatcher.Uses(
                notification_service_adapter,
                "Sends alert messages"
            );

            escalation_service.Uses(
                alert_repository,
                "Reads unresolved critical alerts"
            );

            notification_service_adapter.Uses(
                contextDiagram.notification_service,
                "Delivers alerts and notifications",
                "JSON/HTTPS"
            );

            alert_repository.Uses(
                containerDiagram.database,
                "Reads and writes alert data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#b71c1c",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            alert_controller.AddTags(componentTag);
            notification_controller.AddTags(componentTag);
            alert_service.AddTags(componentTag);
            notification_dispatcher.AddTags(componentTag);
            escalation_service.AddTags(componentTag);
            alert_repository.AddTags(componentTag);
            notification_service_adapter.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-alerts-notifications",
                "Component Diagram - Alerts & Notifications Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Alerts & Notifications";

            componentView.Add(alert_controller);
            componentView.Add(notification_controller);
            componentView.Add(alert_service);
            componentView.Add(notification_dispatcher);
            componentView.Add(escalation_service);
            componentView.Add(alert_repository);
            componentView.Add(notification_service_adapter);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(contextDiagram.notification_service);
            componentView.Add(containerDiagram.database);
        }
    }
}
