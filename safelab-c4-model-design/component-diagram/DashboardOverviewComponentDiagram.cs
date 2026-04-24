using Structurizr;

namespace safelab_c4_model_design
{
    public class DashboardOverviewComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "DashboardOverviewComponent";

        public Component dashboard_controller { get; private set; }
        public Component overview_controller { get; private set; }
        public Component dashboard_service { get; private set; }
        public Component kpi_service { get; private set; }
        public Component overview_query_service { get; private set; }
        public Component dashboard_repository { get; private set; }

        public DashboardOverviewComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            dashboard_controller = containerDiagram.rest_api.AddComponent(
                "Dashboard Controller",
                "Handles requests for SafeLab dashboard summaries and real-time overview data.",
                "ASP.NET Core Controller"
            );

            overview_controller = containerDiagram.rest_api.AddComponent(
                "Overview Controller",
                "Handles requests for platform status, asset summaries, and alert overviews.",
                "ASP.NET Core Controller"
            );

            dashboard_service = containerDiagram.rest_api.AddComponent(
                "Dashboard Service",
                "Coordinates dashboard information from monitoring, assets, alerts, and reports.",
                "C# Service"
            );

            kpi_service = containerDiagram.rest_api.AddComponent(
                "KPI Service",
                "Calculates operational indicators such as active alerts, equipment status, and sensor stability.",
                "C# Service"
            );

            overview_query_service = containerDiagram.rest_api.AddComponent(
                "Overview Query Service",
                "Builds optimized read models for dashboard and overview screens.",
                "C# Query Service"
            );

            dashboard_repository = containerDiagram.rest_api.AddComponent(
                "Dashboard Repository",
                "Reads dashboard metrics, summaries, and historical overview data.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                dashboard_controller,
                "Views laboratory dashboard"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                dashboard_controller,
                "Views compliance and asset overview"
            );

            contextDiagram.safelab_administrator.Uses(
                overview_controller,
                "Monitors platform overview"
            );

            dashboard_controller.Uses(
                dashboard_service,
                "Delegates dashboard logic"
            );

            overview_controller.Uses(
                dashboard_service,
                "Requests overview data"
            );

            dashboard_service.Uses(
                kpi_service,
                "Calculates dashboard indicators"
            );

            dashboard_service.Uses(
                overview_query_service,
                "Builds dashboard read models"
            );

            kpi_service.Uses(
                dashboard_repository,
                "Reads metric source data"
            );

            overview_query_service.Uses(
                dashboard_repository,
                "Reads overview source data"
            );

            dashboard_repository.Uses(
                containerDiagram.database,
                "Reads dashboard and overview data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#455a64",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            dashboard_controller.AddTags(componentTag);
            overview_controller.AddTags(componentTag);
            dashboard_service.AddTags(componentTag);
            kpi_service.AddTags(componentTag);
            overview_query_service.AddTags(componentTag);
            dashboard_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-dashboard-overview",
                "Component Diagram - Dashboard & Overview Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Dashboard & Overview";

            componentView.Add(dashboard_controller);
            componentView.Add(overview_controller);
            componentView.Add(dashboard_service);
            componentView.Add(kpi_service);
            componentView.Add(overview_query_service);
            componentView.Add(dashboard_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
