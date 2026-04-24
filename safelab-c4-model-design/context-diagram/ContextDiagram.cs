using Structurizr;

namespace safelab_c4_model_design
{
    public class ContextDiagram
    {
        private readonly C4 c4;

        // People
        public Person laboratory_staff { get; private set; }
        public Person pharmaceutical_companies { get; private set; }
        public Person safelab_administrator { get; private set; }
        public Person visitor { get; private set; }

        // Software Systems
        public SoftwareSystem safelab { get; private set; }
        public SoftwareSystem iot_sensor  { get; private set; }
        public SoftwareSystem payment_gateway { get; private set; }
        public SoftwareSystem notification_service { get; private set; }


        // Constructor
        public ContextDiagram(C4 c4)
        {
            this.c4 = c4;
        }

        // Generate Method
        public void Generate()
        {
            AddElements();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Elements
        private void AddElements()
        {
            AddPeople();
            AddSoftwareSystems();
        }

        // Add People
        private void AddPeople()
        {
            laboratory_staff = c4.Model.AddPerson(
                "Laboratory Staff",
                "Monitors equipment, supplies, and responds to laboratory alerts and notifications."
            );

            pharmaceutical_companies = c4.Model.AddPerson(
                "Pharmaceutical Companies",
                "Monitors storage conditions, alerts, and compliance of pharmaceutical assets stored in the laboratory."
            );

            safelab_administrator = c4.Model.AddPerson(
                "SafeLab Administrator",
                "Configures and monitors the platform, managing users and ensuring system availability and performance."
            );

            visitor = c4.Model.AddPerson(
                "Visitor",
                "Explores the public website to learn about the platform and its features."
            );
        }

        // Add Software Systems
        private void AddSoftwareSystems()
        {
            safelab = c4.Model.AddSoftwareSystem(
                "SafeLab",
                "Smart platform for monitoring laboratory assets, storage conditions, alerts, and compliance in real time."
            );

            iot_sensor  = c4.Model.AddSoftwareSystem(
                "IoT Sensor Platform",
                "External IoT platform that sends real-time sensor readings and device status data, such as temperature, humidity, and storage conditions, to SafeLab."
            );

            payment_gateway = c4.Model.AddSoftwareSystem(
                "Payment Gateway",
                "Processes secure online payments for transport services."
            );

            notification_service = c4.Model.AddSoftwareSystem(
                "Notification Service",
                "Service that allows sending push notifications to users"
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            laboratory_staff.Uses(
                safelab,
                "Tracks equipment and handles alerts"
            );

            pharmaceutical_companies.Uses(
                safelab,
                "Reviews alerts and compliance data"
            );

            safelab_administrator.Uses(
                safelab,
                "Administers and configures the platform"
            );

            visitor.Uses(
                safelab,
                "Visits the website to explore features"
            );

            // Software Systems to Software Systems
            safelab.Uses(
                iot_sensor ,
                "Provides real-time sensor data and device telemetry"
            );

            safelab.Uses(
                payment_gateway,
                "Processes payments via external payment API"
            );

            safelab.Uses(
                notification_service,
                "Sends push notifications via messaging service"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // People
            styles.Add(new ElementStyle(nameof(laboratory_staff))
            {
                Background = "#8e24aa",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(pharmaceutical_companies))
            {
                Background = "#f9a825",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(safelab_administrator))
            {
                Background = "#4D1D6E",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(visitor))
            {
                Background = "#85324C",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            // Software Systems
            styles.Add(new ElementStyle(nameof(safelab))
            {
                Background = "#2e7d32",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(iot_sensor ))
            {
                Background = "#6d4c41",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(payment_gateway))
            {
                Background = "#19ACFA",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(notification_service))
            {
                Background = "#631818",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });
        }

        // Set Tags
        private void SetTags()
        {
            // People
            laboratory_staff.AddTags(nameof(laboratory_staff));
            pharmaceutical_companies.AddTags(nameof(pharmaceutical_companies));
            safelab_administrator.AddTags(nameof(safelab_administrator));
            visitor.AddTags(nameof(visitor));

            // Software Systems
            safelab.AddTags(nameof(safelab));
            iot_sensor .AddTags(nameof(iot_sensor ));
            payment_gateway.AddTags(nameof(payment_gateway));
            notification_service.AddTags(nameof(notification_service));
        }

        // Create View
        private void CreateView()
        {
            SystemContextView contextView = c4.ViewSet.CreateSystemContextView(
                safelab,
                "safelab-context",
                "Context Diagram - SafeLab System Context"
            );

            // Add all people and software systems to the view
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
        }
    }
}