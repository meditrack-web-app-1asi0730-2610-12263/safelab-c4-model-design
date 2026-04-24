using Structurizr;

namespace safelab_c4_model_design
{
    public class WebApplicationComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "WebApplicationComponent";

        // Components
        public Component auth_component { get; private set; }
        public Component profile_component { get; private set; }
        public Component subscription_component { get; private set; }
        public Component dashboard_component { get; private set; }
        public Component asset_component { get; private set; }
        public Component sensor_component { get; private set; }
        public Component compliance_component { get; private set; }
        public Component alert_center_component { get; private set; }

        public Component device_control_component { get; private set; }
        
        public Component analytics_component { get; private set; }
        
        public Component incident_component { get; private set; }
        
        public Component audit_trail_component { get; private set; }
        // Constructor
        public WebApplicationComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
            this.containerDiagram = containerDiagram;
        }

        // Generate Method
        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Components
        private void AddComponents()
        {
            auth_component = containerDiagram.web_application.AddComponent(
                "Authentication Component",
                "Manages user login, registration, password recovery, and session management.",
                "Vue.js Component"
            );

            profile_component = containerDiagram.web_application.AddComponent(
                "Profile Management Component",
                "Allows users to view and update their personal information, preferences, and settings.",
                "Vue.js Component"
            );

            subscription_component = containerDiagram.web_application.AddComponent(
                "Subscription Management Component",
                "Handles subscription management, billing information, and payment processing.",
                "Vue.js Component"
            );
            
            dashboard_component = containerDiagram.web_application.AddComponent(
                "Dashboard Component",
                "Displays key metrics, reports, and analytics for business users and administrators.",
                "Vue.js Component"
            );
            
            asset_component = containerDiagram.web_application.AddComponent(
                "Asset Monitoring Component",
                "Enables real-time tracking of shipments, status updates, and notifications.",
                "Vue.js Component"
            );
            
            sensor_component = containerDiagram.web_application.AddComponent(
                "Sensor Monitoring Component",
                "Facilitates communication between users, including messaging and alerts.",
                "Vue.js Component"
            );
            
            compliance_component = containerDiagram.web_application.AddComponent(
                "Compliance Management Component",
                "Provides administrative functionalities such as user management, system settings, and audit logs.",
                "Vue.js Component"
            );
            
            alert_center_component = containerDiagram.web_application.AddComponent(
                "Alert Center Component",
                "Centralized hub for managing and responding to alerts, notifications, and incidents.",
                "Vue.js Component"
            );

            device_control_component = containerDiagram.web_application.AddComponent(
                "Device Control Component",
                "Allows users to remotely control and configure connected devices and sensors.",
                "Vue.js Component"
            );
            
            analytics_component = containerDiagram.web_application.AddComponent(
                "Reporting & Analytics Component",
                "Provides advanced reporting, data visualization, and analytics features for business users.",
                "Vue.js Component"
            );
            
            incident_component = containerDiagram.web_application.AddComponent(
                "Incident Management Component",
                "Enables users to report, track, and manage incidents related to shipments, devices, or system issues.",
                "Vue.js Component"
            );
            
            audit_trail_component = containerDiagram.web_application.AddComponent(
                "Audit Trail Component",
                "Maintains a comprehensive log of user activities, system events, and changes for compliance and troubleshooting purposes.",
                "Vue.js Component"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // Container to Components
            auth_component.Uses(
                containerDiagram.rest_api,
                "Authenticates users and manages sessions via Identity & Access BC",
                "JSON/HTTPS"
            );

            profile_component.Uses(
                containerDiagram.rest_api,
                "Retrieves and updates user data via Profiles & Preferences BC",
                "JSON/HTTPS"
            );

            subscription_component.Uses(
                containerDiagram.rest_api,
                "Manages billing, plans, and subscriptions via Payments & Subscriptions BC",
                "JSON/HTTPS"
            );

            dashboard_component.Uses(
                containerDiagram.rest_api,
                "Fetches metrics, reports, and analytics data via Reporting & Analytics BC",
                "JSON/HTTPS"
            );

            asset_component.Uses(
                containerDiagram.rest_api,
                "Retrieves real-time shipment and asset data via Asset Tracking BC",
                "JSON/HTTPS"
            );

            sensor_component.Uses(
                containerDiagram.rest_api,
                "Facilitates communication and alerting via Communication & Alerting BC",
                "JSON/HTTPS"
            );

            compliance_component.Uses(
                containerDiagram.rest_api,
                "Handles administrative functions and compliance data via Administration & Compliance BC",
                "JSON/HTTPS"
            );
            
            alert_center_component.Uses(
                containerDiagram.rest_api,
                "Manages alerts and notifications via Communication & Alerting BC",
                "JSON/HTTPS"
            );

            device_control_component.Uses(
                containerDiagram.rest_api,
                "Sends control commands and configurations to devices via Device Management BC",
                "JSON/HTTPS"
            );
            
            analytics_component.Uses(
                containerDiagram.rest_api,
                "Fetches data for reporting and analytics via Reporting & Analytics BC",
                "JSON/HTTPS"
            );
            
            incident_component.Uses(
                containerDiagram.rest_api,
                "Manages incident reporting and tracking via Incident Management BC",
                "JSON/HTTPS"
            );
            
            audit_trail_component.Uses(
                containerDiagram.rest_api,
                "Retrieves and logs audit trail data via Administration & Compliance BC",
                "JSON/HTTPS"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#0C386A",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Components
            auth_component.AddTags(componentTag);
            profile_component.AddTags(componentTag);
            subscription_component.AddTags(componentTag);
            dashboard_component.AddTags(componentTag);
            asset_component.AddTags(componentTag);
            sensor_component.AddTags(componentTag);
            compliance_component.AddTags(componentTag);
            alert_center_component.AddTags(componentTag);
            device_control_component.AddTags(componentTag);
            analytics_component.AddTags(componentTag);
            incident_component.AddTags(componentTag);
            audit_trail_component.AddTags(componentTag);
        }

        // Create View
        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.web_application,
                "safelab-component-web-application",
                "Component Diagram - Web Application interacting with all Bounded Contexts through the REST API"
            );

            // Title
            string title = "SafeLab - Web Application";
            componentView.Title = title;

            // Elements to add to the view
            componentView.Add(auth_component);
            componentView.Add(profile_component);
            componentView.Add(subscription_component);
            componentView.Add(dashboard_component);
            componentView.Add(asset_component);
            componentView.Add(sensor_component);
            componentView.Add(compliance_component);
            componentView.Add(alert_center_component);
            componentView.Add(device_control_component);
            componentView.Add(analytics_component);
            componentView.Add(incident_component);
            componentView.Add(audit_trail_component);

            // People to add to the view
            componentView.Add(containerDiagram.rest_api);
        }
    }
}