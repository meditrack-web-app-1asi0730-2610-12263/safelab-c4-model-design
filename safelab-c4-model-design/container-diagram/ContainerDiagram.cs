using Structurizr;

namespace safelab_c4_model_design
{
    public class ContainerDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;

        // Containers
        public Container mobile_application { get; private set; }
        public Container business_website { get; private set; }
        public Container web_application { get; private set; }
        public Container rest_api { get; private set; }
        public Container database { get; private set; }

        // Constructor
        public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
        }

        // Generate Method
        public void Generate()
        {
            AddContainers();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Containers
        private void AddContainers()
        {
            mobile_application = contextDiagram.safelab.AddContainer(
                "Mobile App",
                "Mobile application for monitoring alerts, assets, and laboratory conditions remotely.",
                "Flutter"
            );

            business_website = contextDiagram.safelab.AddContainer(
                "Business Website",
                "Public business website to inform about safelab, its features, and benefits. Allows potential customers to register, subscribe to plans, and contact the company.",
                "HTML5, CSS3, JavaScript"
            );

            web_application = contextDiagram.safelab.AddContainer(
                "Web App",
                "Web application for monitoring assets, alerts, storage conditions, and platform operations.",
                "Vue.js + PrimeVue"
            );

            rest_api = contextDiagram.safelab.AddContainer(
                "REST API",
                "Provides secure endpoints for monitoring, alerts, authentication, reports, and system management.",
                "ASP.NET Core With C#"
            );

            database = contextDiagram.safelab.AddContainer(
                "database",
                "Stores users, devices, alerts, subscriptions, reports, and operational data.",
                "SQL Server"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            // People to Containers
            contextDiagram.laboratory_staff.Uses(
                mobile_application,
                "Uses mobile monitoring features"
            );

            contextDiagram.laboratory_staff.Uses(
                business_website,
                "Views public information"
            );

            contextDiagram.laboratory_staff.Uses(
                web_application,
                "Uses monitoring dashboard"
            );


            contextDiagram.pharmaceutical_companies.Uses(
                mobile_application,
                "Uses mobile monitoring features"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                business_website,
                "Views service information"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                web_application,
                "Uses monitoring dashboard"
            );
            

            contextDiagram.safelab_administrator.Uses(
                mobile_application,
                "Uses mobile administration features"
            );

            contextDiagram.safelab_administrator.Uses(
                business_website,
                "Views website content"
            );

            contextDiagram.safelab_administrator.Uses(
                web_application,
                "Administers the platform"
            );


            contextDiagram.visitor.Uses(
                business_website,
                "Browses service information and features"
            );

            // Containers to Containers
            business_website.Uses(web_application,
                "Delivers static assets (HTML, JS, CSS) and redirects users to the single-page application for interactive features.",
                "HTTPS"
            );

            business_website.Uses(mobile_application,
                "Provides download and access links for the mobile application through the business website.",
                "HTTPS"
            );


            mobile_application.Uses(
                rest_api,
                "Consumes the API for real-time data, user authentication, and interactive features",
                "JSON/HTTPS"
            );

            web_application.Uses(
                rest_api,
                "Consumes the API for real-time data, user authentication, and interactive features",
                "JSON/HTTPS"
            );


            rest_api.Uses(
                database,
                "Reads and writes system information",
                "SQL Server"
            );

            // Containers to Software System
            rest_api.Uses(
                contextDiagram.iot_sensor,
                "Receives sensor data",
                "JSON/HTTPS"
            );

            rest_api.Uses(
                contextDiagram.payment_gateway,
                "Processes payments and subscriptions",
                "JSON/HTTPS"
            );

            rest_api.Uses(
                contextDiagram.notification_service,
                "Sends notifications to users",
                "JSON/HTTPS"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // Containers
            styles.Add(new ElementStyle(nameof(mobile_application))
            {
                Background = "#009688",
                Color = "#ffffff",
                Shape = Shape.MobileDevicePortrait
            });

            styles.Add(new ElementStyle(nameof(business_website))
            {
                Background = "#ef6c00",
                Color = "#ffffff",
                Shape = Shape.Folder
            });

            styles.Add(new ElementStyle(nameof(web_application))
            {
                Background = "#1565c0",
                Color = "#ffffff",
                Shape = Shape.WebBrowser
            });

            styles.Add(new ElementStyle(nameof(rest_api))
            {
                Background = "#455a64",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(database))
            {
                Background = "#303f9f",
                Color = "#ffffff",
                Shape = Shape.Cylinder
            });
        }

        // Set Tags
        private void SetTags()
        {
            // Containers
            mobile_application.AddTags(nameof(mobile_application));
            business_website.AddTags(nameof(business_website));
            web_application.AddTags(nameof(web_application));
            rest_api.AddTags(nameof(rest_api));
            database.AddTags(nameof(database));
        }

        // Create View
        private void CreateView()
        {
            ContainerView containerView = c4.ViewSet.CreateContainerView(
                contextDiagram.safelab,
                "safelab-container",
                "Container Diagram - SafeLab System"
            );

            // Add all people, software systems, and containers to the view
            containerView.AddAllElements();
        }
    }
}
