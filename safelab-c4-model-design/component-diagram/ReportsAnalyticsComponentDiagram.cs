using Structurizr;

namespace safelab_c4_model_design
{
    public class ReportsAnalyticsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "ReportsAnalyticsComponent";

        public Component report_controller { get; private set; }
        public Component analytics_controller { get; private set; }
        public Component report_service { get; private set; }
        public Component trend_analysis_service { get; private set; }
        public Component export_service { get; private set; }
        public Component report_repository { get; private set; }

        public ReportsAnalyticsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            report_controller = containerDiagram.rest_api.AddComponent(
                "Report Controller",
                "Handles requests for operational, compliance, and monitoring reports.",
                "ASP.NET Core Controller"
            );

            analytics_controller = containerDiagram.rest_api.AddComponent(
                "Analytics Controller",
                "Handles requests for trends, historical analysis, and performance indicators.",
                "ASP.NET Core Controller"
            );

            report_service = containerDiagram.rest_api.AddComponent(
                "Report Service",
                "Generates reports from monitoring, asset, alert, incident, and compliance data.",
                "C# Service"
            );

            trend_analysis_service = containerDiagram.rest_api.AddComponent(
                "Trend Analysis Service",
                "Analyzes historical temperature, humidity, equipment, and alert variations.",
                "C# Service"
            );

            export_service = containerDiagram.rest_api.AddComponent(
                "Export Service",
                "Exports reports and analytics results in formats such as PDF, Excel, or CSV.",
                "C# Service"
            );

            report_repository = containerDiagram.rest_api.AddComponent(
                "Report Repository",
                "Reads and writes report definitions, generated reports, analytics data, and export records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                report_controller,
                "Generates laboratory reports"
            );

            contextDiagram.laboratory_staff.Uses(
                analytics_controller,
                "Reviews operational trends"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                report_controller,
                "Generates compliance reports"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                analytics_controller,
                "Reviews storage condition trends"
            );

            contextDiagram.safelab_administrator.Uses(
                analytics_controller,
                "Reviews platform analytics"
            );

            report_controller.Uses(
                report_service,
                "Delegates report generation"
            );

            analytics_controller.Uses(
                trend_analysis_service,
                "Requests trend analysis"
            );

            report_service.Uses(
                trend_analysis_service,
                "Includes analytical insights"
            );

            report_service.Uses(
                export_service,
                "Exports report outputs"
            );

            report_service.Uses(
                report_repository,
                "Persists report records"
            );

            trend_analysis_service.Uses(
                report_repository,
                "Reads historical analysis data"
            );

            export_service.Uses(
                report_repository,
                "Persists export records"
            );

            report_repository.Uses(
                containerDiagram.database,
                "Reads and writes report and analytics data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#3949ab",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            report_controller.AddTags(componentTag);
            analytics_controller.AddTags(componentTag);
            report_service.AddTags(componentTag);
            trend_analysis_service.AddTags(componentTag);
            export_service.AddTags(componentTag);
            report_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-reports-analytics",
                "Component Diagram - Reports & Analytics Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Reports & Analytics";

            componentView.Add(report_controller);
            componentView.Add(analytics_controller);
            componentView.Add(report_service);
            componentView.Add(trend_analysis_service);
            componentView.Add(export_service);
            componentView.Add(report_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
