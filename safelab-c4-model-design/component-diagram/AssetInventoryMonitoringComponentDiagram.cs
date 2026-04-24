using Structurizr;

namespace safelab_c4_model_design
{
    public class AssetInventoryMonitoringComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AssetInventoryMonitoringComponent";

        public Component asset_controller { get; private set; }
        public Component inventory_controller { get; private set; }
        public Component asset_service { get; private set; }
        public Component inventory_service { get; private set; }
        public Component equipment_status_service { get; private set; }
        public Component asset_inventory_repository { get; private set; }

        public AssetInventoryMonitoringComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            asset_controller = containerDiagram.rest_api.AddComponent(
                "Asset Controller",
                "Handles requests for laboratory assets, equipment, and storage units.",
                "ASP.NET Core Controller"
            );

            inventory_controller = containerDiagram.rest_api.AddComponent(
                "Inventory Controller",
                "Handles requests for supplies, critical items, quantities, and availability.",
                "ASP.NET Core Controller"
            );

            asset_service = containerDiagram.rest_api.AddComponent(
                "Asset Service",
                "Manages asset registration, classification, location, and operational status.",
                "C# Service"
            );

            inventory_service = containerDiagram.rest_api.AddComponent(
                "Inventory Service",
                "Tracks supplies, stock levels, availability, and critical inventory conditions.",
                "C# Service"
            );

            equipment_status_service = containerDiagram.rest_api.AddComponent(
                "Equipment Status Service",
                "Evaluates equipment availability, condition, and operational risk.",
                "C# Service"
            );

            asset_inventory_repository = containerDiagram.rest_api.AddComponent(
                "Asset Inventory Repository",
                "Reads and writes asset, equipment, supply, and inventory records.",
                "C# Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.laboratory_staff.Uses(
                asset_controller,
                "Reviews laboratory equipment"
            );

            contextDiagram.laboratory_staff.Uses(
                inventory_controller,
                "Checks supplies and availability"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                asset_controller,
                "Reviews stored assets"
            );

            contextDiagram.pharmaceutical_companies.Uses(
                inventory_controller,
                "Monitors critical supplies"
            );

            contextDiagram.safelab_administrator.Uses(
                asset_controller,
                "Manages asset configuration"
            );

            asset_controller.Uses(
                asset_service,
                "Delegates asset logic"
            );

            inventory_controller.Uses(
                inventory_service,
                "Delegates inventory logic"
            );

            asset_service.Uses(
                equipment_status_service,
                "Evaluates equipment status"
            );

            asset_service.Uses(
                asset_inventory_repository,
                "Persists asset records"
            );

            inventory_service.Uses(
                asset_inventory_repository,
                "Persists inventory records"
            );

            equipment_status_service.Uses(
                asset_inventory_repository,
                "Reads equipment condition data"
            );

            asset_inventory_repository.Uses(
                containerDiagram.database,
                "Reads and writes asset and inventory data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#00897b",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            asset_controller.AddTags(componentTag);
            inventory_controller.AddTags(componentTag);
            asset_service.AddTags(componentTag);
            inventory_service.AddTags(componentTag);
            equipment_status_service.AddTags(componentTag);
            asset_inventory_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "safelab-component-asset-inventory-monitoring",
                "Component Diagram - Asset & Inventory Monitoring Bounded Context (REST API)"
            );

            componentView.Title = "SafeLab - Asset & Inventory Monitoring";

            componentView.Add(asset_controller);
            componentView.Add(inventory_controller);
            componentView.Add(asset_service);
            componentView.Add(inventory_service);
            componentView.Add(equipment_status_service);
            componentView.Add(asset_inventory_repository);

            componentView.Add(contextDiagram.laboratory_staff);
            componentView.Add(contextDiagram.pharmaceutical_companies);
            componentView.Add(contextDiagram.safelab_administrator);
            componentView.Add(containerDiagram.database);
        }
    }
}
